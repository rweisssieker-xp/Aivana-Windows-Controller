"""Read-only, aggregate status for the isolated MAKRIVO mail test environment.

Not a production alarm or heartbeat. No address, message body, secret, or raw
event is retrieved. The command intentionally cannot select another database.
"""
import json
import argparse
import os
import pathlib
import re
import sys
import time
import tomllib
import urllib.request

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
)
SELECT kind, reference, reason FROM cases WHERE reason IS NOT NULL ORDER BY kind, reference LIMIT ?
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
    if not subscription['enabled']:
        issues.append('subscription_disabled')
    if dead['backlog_count']:
        issues.append('dead_letter_backlog')
    if source['backlog_count'] and (not source['oldest_message_timestamp_ms'] or now-source['oldest_message_timestamp_ms'] >= 300000):
        issues.append('event_queue_backlog')
    return {'status':'attention' if issues else 'ok', 'issues':issues, 'counts':{k:counts[k] for k in labels},
            'queue_counts':{'events':source['backlog_count'],'dead_letter':dead['backlog_count']},
            'scope':'isolated test only; no independent alarm or heartbeat'}

def read_test_cases(api, now, limit=50):
    """List bounded pseudonymous synthetic test references, not customer data."""
    if type(now) is not int or now < 0 or type(limit) is not int or not 1 <= limit <= 100:
        raise ValueError('invalid case parameters')
    response = api(DATABASE_PATH, {'sql':CASES_QUERY,'params':[now-900000,now-900000,limit+1]})
    if len(response) != 1 or response[0].get('success') is not True or not isinstance(response[0].get('results'),list):
        raise ValueError('invalid database result')
    rows = response[0]['results']
    reasons = ('outcome_unknown','processing_timeout','provider_or_matching_failure','delivery_timeout')
    for row in rows:
        if (row.get('kind') not in ('purchase','withdrawal') or row.get('reason') not in reasons or
            not isinstance(row.get('reference'),str) or not re.fullmatch(r'[A-Za-z0-9_-]{1,128}',row['reference'])):
            raise ValueError('invalid test case')
    return {'cases':rows[:limit], 'truncated':len(rows)>limit}

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

def main():
    parser = argparse.ArgumentParser(description='Read-only isolated MAKRIVO mail-test status')
    parser.add_argument('--cases',action='store_true',help='include pseudonymous test cases (never customer content)')
    args = parser.parse_args()
    try:
        config = pathlib.Path(os.environ['APPDATA']) / 'xdg.config' / '.wrangler' / 'config' / 'default.toml'
        with config.open('rb') as file:
            token = tomllib.load(file)['oauth_token']
        if not isinstance(token, str) or not token:
            raise ValueError('credential unavailable')
        api = cloudflare_api(token)
        now = int(time.time()*1000)
        result = read_status(api,now)
        if args.cases:
            result['test_cases'] = read_test_cases(api,now)
            if result['test_cases']['truncated']:
                result['status'] = 'attention'
        print(json.dumps(result, ensure_ascii=False))
        return 0 if result['status'] == 'ok' else 1
    except Exception:
        # Provider errors and bodies may contain secrets or personal data.
        print(json.dumps({'status':'error','message':'Test status could not be verified; operator review required.'}))
        return 2

if __name__ == '__main__':
    sys.exit(main())
