"""Read-only, aggregate status for the isolated MAKRIVO mail test environment.

Not a production alarm or heartbeat. No address, message body, secret, or raw
event is retrieved. The command intentionally cannot select another database.
"""
import json
import argparse
import os
import pathlib
import re
import shutil
import subprocess
import sys
import time
import tomllib
import urllib.request
import urllib.error

ACCOUNT = '4b0fd8180d954c7db4b4a0bacbe2594b'
SUBSCRIPTION_PATH = 'event_subscriptions/subscriptions/403f7eb689d842e28bfc0ea53d9b24b6'
SOURCE_METRICS_PATH = 'queues/0b69b458996f40eab73a5cc1f5dc851f/metrics'
DLQ_METRICS_PATH = 'queues/f48444823d6c453ea9f6b130d5017b39/metrics'
DATABASE_PATH = 'd1/database/96c721dd-f51f-4d1b-b6c1-974a7f43d567/query'

QUERY = """
WITH attempt_status AS (
 SELECT a.state, a.started_at, a.finished_at,
   CASE WHEN typeof(a.message_id)='text' AND length(a.message_id) BETWEEN 1 AND 512 AND instr(a.message_id,char(0))=0 AND a.message_id NOT GLOB '*[^!-~]*' THEN 1 ELSE 0 END AS valid_message_id,
   (SELECT COUNT(*) FROM mail_dispatch_attempts b WHERE b.state='accepted' AND b.message_id=a.message_id) AS matches,
   (SELECT COUNT(*) FROM mail_delivery_events e WHERE e.message_id=a.message_id AND e.kind IN ('bounced','failed','rejected','complained')) AS failures,
   (SELECT COUNT(*) FROM mail_delivery_events e WHERE e.message_id=a.message_id AND e.kind='delivered') AS delivered
 FROM mail_dispatch_attempts a
)
SELECT
 COALESCE(SUM(CASE WHEN state='unknown' THEN 1 ELSE 0 END),0) AS unknown_attempts,
 COALESCE(SUM(CASE WHEN state='processing' AND started_at<=? THEN 1 ELSE 0 END),0) AS stuck_attempts,
 COALESCE(SUM(CASE WHEN state='accepted' AND (valid_message_id=0 OR failures>0 OR matches>1) THEN 1 ELSE 0 END),0) AS provider_failures,
 COALESCE(SUM(CASE WHEN state='accepted' AND valid_message_id=1 AND failures=0 AND matches<=1 AND delivered=0 AND COALESCE(finished_at,started_at)<=? THEN 1 ELSE 0 END),0) AS delivery_timeout,
 (SELECT COUNT(*) FROM mail_delivery_events e WHERE NOT EXISTS (SELECT 1 FROM mail_dispatch_attempts a WHERE a.state='accepted' AND a.message_id=e.message_id)) AS unmatched_events
FROM attempt_status
"""

CASES_QUERY = """
WITH attempt_status AS (
 SELECT a.kind, a.job_id, a.state, a.started_at, a.finished_at,
   CASE WHEN typeof(a.message_id)='text' AND length(a.message_id) BETWEEN 1 AND 512 AND instr(a.message_id,char(0))=0 AND a.message_id NOT GLOB '*[^!-~]*' THEN 1 ELSE 0 END AS valid_message_id,
   (SELECT COUNT(*) FROM mail_dispatch_attempts b WHERE b.state='accepted' AND b.message_id=a.message_id) AS matches,
   (SELECT COUNT(*) FROM mail_delivery_events e WHERE e.message_id=a.message_id AND e.kind IN ('bounced','failed','rejected','complained')) AS failures,
   (SELECT COUNT(*) FROM mail_delivery_events e WHERE e.message_id=a.message_id AND e.kind='delivered') AS delivered
 FROM mail_dispatch_attempts a
), cases AS (
 SELECT kind, job_id AS reference,
   CASE
     WHEN state='unknown' THEN 'outcome_unknown'
     WHEN state='processing' AND started_at<=? THEN 'processing_timeout'
     WHEN state='accepted' AND (valid_message_id=0 OR failures>0 OR matches>1) THEN 'provider_or_matching_failure'
     WHEN state='accepted' AND valid_message_id=1 AND failures=0 AND matches<=1 AND delivered=0 AND COALESCE(finished_at,started_at)<=? THEN 'delivery_timeout'
     ELSE NULL
   END AS reason
 FROM attempt_status
 UNION ALL
 SELECT 'event' AS kind, e.event_id AS reference, 'unmatched_event' AS reason
 FROM mail_delivery_events e
 WHERE NOT EXISTS (SELECT 1 FROM mail_dispatch_attempts a WHERE a.state='accepted' AND a.message_id=e.message_id)
)
SELECT kind, reference, reason FROM cases
WHERE reason IS NOT NULL AND (?=0 OR (kind,reference)>(?,?))
ORDER BY kind, reference LIMIT ?
"""

def read_status(api, now):
    if type(now) is not int or now < 0:
        raise ValueError('invalid clock')
    subscription = api(SUBSCRIPTION_PATH)
    if type(subscription.get('enabled')) is not bool:
        raise ValueError('invalid subscription status')
    source = api(SOURCE_METRICS_PATH)
    dead = api(DLQ_METRICS_PATH)
    for metrics in (source, dead):
        for key in ('backlog_count', 'oldest_message_timestamp_ms'):
            if type(metrics.get(key)) is not int or metrics[key] < 0:
                raise ValueError('invalid queue metrics')
        if metrics['oldest_message_timestamp_ms'] > now:
            raise ValueError('invalid queue timestamp')
    result = api(DATABASE_PATH, {'sql': QUERY, 'params': [now-900000, now-900000]})
    if len(result) != 1 or result[0].get('success') is not True or len(result[0].get('results', [])) != 1:
        raise ValueError('invalid database result')
    counts = result[0]['results'][0]
    labels = ('unknown_attempts','stuck_attempts','provider_failures','delivery_timeout','unmatched_events')
    if any(type(counts.get(k)) is not int or counts[k] < 0 for k in labels):
        raise ValueError('invalid database counts')
    issues = [label for label in labels if counts[label]]
    # This intentionally isolated database has no purchase/withdrawal queues.
    # A green result would conceal that unclaimed jobs are not checked here.
    issues.append('unclaimed_jobs_not_monitored')
    if not subscription['enabled']:
        issues.append('subscription_disabled')
    if dead['backlog_count']:
        issues.append('dead_letter_backlog')
    if source['backlog_count'] and (not source['oldest_message_timestamp_ms'] or now-source['oldest_message_timestamp_ms'] >= 300000):
        issues.append('event_queue_backlog')
    return {'status':'attention' if issues else 'ok', 'issues':issues, 'counts':{k:counts[k] for k in labels},
            'queue_counts':{'events':source['backlog_count'],'dead_letter':dead['backlog_count']},
            'scope':'isolated test only; no independent alarm or heartbeat'}

def read_test_cases(api, now, limit=50, cursor=None):
    """List bounded pseudonymous synthetic test references, not customer data."""
    if type(now) is not int or now < 0 or type(limit) is not int or not 1 <= limit <= 100:
        raise ValueError('invalid case parameters')
    kinds = ('purchase','withdrawal','event')
    def safe_reference(value):
        return isinstance(value,str) and re.fullmatch(r'[A-Za-z0-9._:-]{1,128}',value) is not None
    if cursor is not None and (not isinstance(cursor,dict) or set(cursor)!= {'kind','reference','as_of'} or
                               cursor['kind'] not in kinds or not safe_reference(cursor['reference']) or
                               type(cursor['as_of']) is not int or cursor['as_of'] != now):
        raise ValueError('invalid cursor')
    response = api(DATABASE_PATH, {'sql':CASES_QUERY,'params':[now-900000,now-900000,1 if cursor else 0,
                   cursor['kind'] if cursor else '',cursor['reference'] if cursor else '',limit+1]})
    if len(response) != 1 or response[0].get('success') is not True or not isinstance(response[0].get('results'),list):
        raise ValueError('invalid database result')
    rows = response[0]['results']
    reasons = ('outcome_unknown','processing_timeout','provider_or_matching_failure','delivery_timeout','unmatched_event')
    for row in rows:
        if (row.get('kind') not in kinds or row.get('reason') not in reasons or
            (row.get('kind') == 'event') != (row.get('reason') == 'unmatched_event') or
            not safe_reference(row.get('reference'))):
            raise ValueError('invalid test case')
    visible=rows[:limit]
    next_cursor={'kind':visible[-1]['kind'],'reference':visible[-1]['reference'],'as_of':now} if len(rows)>limit else None
    return {'cases':visible, 'truncated':next_cursor is not None, 'next_cursor':next_cursor}

def cloudflare_api(token):
    base = 'https://api.cloudflare.com/client/v4/accounts/' + ACCOUNT + '/'
    def call(path, body=None):
        if path not in (SUBSCRIPTION_PATH,SOURCE_METRICS_PATH,DLQ_METRICS_PATH,DATABASE_PATH):
            raise ValueError('unexpected endpoint')
        data = None if body is None else json.dumps(body).encode('utf-8')
        request = urllib.request.Request(base+path, data=data, method='GET' if data is None else 'POST',
                headers={'Authorization':'Bearer '+token,'Content-Type':'application/json'})
        with urllib.request.urlopen(request, timeout=15) as response:
            payload = json.load(response)
        if payload.get('success') is not True or 'result' not in payload:
            raise ValueError('provider query failed')
        return payload['result']
    return call

def load_wranger_token():
    config = pathlib.Path(os.environ['APPDATA']) / 'xdg.config' / '.wrangler' / 'config' / 'default.toml'
    with config.open('rb') as file:
        token = tomllib.load(file)['oauth_token']
    if not isinstance(token,str) or not token:
        raise ValueError('credential unavailable')
    return token

def refresh_wranger_session():
    # Wrangler owns OAuth refresh and credential storage. Never print its
    # output: even whoami may contain account or email information.
    node = shutil.which('node')
    if not node:
        raise RuntimeError('Node not installed')
    npx = pathlib.Path(node).resolve().parent / 'node_modules' / 'npm' / 'bin' / 'npx-cli.js'
    if not npx.is_file():
        raise RuntimeError('npx not installed')
    config = pathlib.Path(__file__).resolve().parent / 'wrangler.aivana.jsonc'
    environment = os.environ.copy()
    # Do not let an unrelated ambient API key replace this OAuth session or
    # redirect the authentication request to an unexpected API endpoint.
    for key in ('CLOUDFLARE_API_TOKEN','CF_API_TOKEN','CLOUDFLARE_API_KEY',
                'CLOUDFLARE_EMAIL','CLOUDFLARE_API_USER_SERVICE_KEY','CLOUDFLARE_API_BASE_URL'):
        environment.pop(key,None)
    subprocess.run([node,str(npx),'--no-install','--package=wrangler@4.153.0','wrangler','whoami','--config',str(config)],
                   stdin=subprocess.DEVNULL,stdout=subprocess.DEVNULL,stderr=subprocess.DEVNULL,
                   timeout=60,check=True,creationflags=getattr(subprocess,'CREATE_NO_WINDOW',0),env=environment)

def run_check(load_token,refresh,make_api,now,include_cases=False,case_cursor=None):
    # Restart all read-only checks once after a 401, avoiding a mixed snapshot
    # from two sessions. 403 is a permission problem and must never be retried
    # as an authentication refresh.
    if case_cursor is not None and (not include_cases or not isinstance(case_cursor,dict) or
       type(case_cursor.get('as_of')) is not int or not 0<=case_cursor['as_of']<=now):
        raise ValueError('invalid case cursor')
    for attempt in range(2):
        token=load_token()
        try:
            api=make_api(token)
            result=read_status(api,now)
            if include_cases:
                result['test_cases']=read_test_cases(api,case_cursor['as_of'] if case_cursor else now,cursor=case_cursor)
                if result['test_cases']['truncated']:
                    result['status']='attention'
            return result
        except urllib.error.HTTPError as error:
            if error.code!=401 or attempt:
                raise
            refresh()
    raise RuntimeError('authentication unavailable')

def main():
    parser = argparse.ArgumentParser(description='Read-only isolated MAKRIVO mail-test status')
    parser.add_argument('--cases',action='store_true',help='include pseudonymous test cases (never customer content)')
    parser.add_argument('--after-kind',choices=('purchase','withdrawal','event'))
    parser.add_argument('--after-reference')
    parser.add_argument('--after-as-of',type=int)
    args = parser.parse_args()
    try:
        now = int(time.time()*1000)
        parts=(args.after_kind,args.after_reference,args.after_as_of)
        if any(value is not None for value in parts) and (not args.cases or any(value is None for value in parts)):
            raise ValueError('incomplete cursor')
        cursor={'kind':parts[0],'reference':parts[1],'as_of':parts[2]} if parts[0] is not None else None
        result = run_check(load_wranger_token,refresh_wranger_session,cloudflare_api,now,args.cases,cursor)
        print(json.dumps(result, ensure_ascii=False))
        return 0 if result['status'] == 'ok' else 1
    except Exception:
        # Provider errors and bodies may contain secrets or personal data.
        print(json.dumps({'status':'error','message':'Test status could not be verified; operator review required.'}))
        return 2

if __name__ == '__main__':
    sys.exit(main())
