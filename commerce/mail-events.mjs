// Restricted test-only event reconciliation; no public event endpoint.
const names=new Set(['delivered','deferred','bounced','failed','rejected','complained']);
const id=value=>typeof value==='string' && /^[A-Za-z0-9._:-]{1,128}$/.test(value);
const messageId=value=>typeof value==='string' && value.length>0 && value.length<=512 && /^[\x21-\x7e]+$/.test(value);

export async function recordMailEvent(db,env,event){
  if(env?.MAIL_EVENT_TEST_ENABLED!=='true'||env?.STRIPE_LIVE!=='false'||!env?.MAIL_EVENT_ACCOUNT||!env?.MAIL_EVENT_ZONE||!env?.MAIL_EVENT_SUBSCRIPTION||!env?.MAIL_EVENT_DOMAIN||!env?.MAIL_SENDER||!env?.TEST_MAIL_RECIPIENT) throw new Error('Invalid test event configuration');
  const prefix='cf.email.sending.message.',type=event?.type;
  const kind=typeof type==='string'&&type.startsWith(prefix)?type.slice(prefix.length):'';
  const p=event?.payload,m=event?.metadata,src=event?.source;
  const stamp=m?.eventTimestamp;
  const time=typeof stamp==='string' && /^\d{4}-\d\d-\d\dT\d\d:\d\d:\d\d(?:\.\d{1,3})?Z$/.test(stamp) ? Date.parse(stamp) : NaN;
  if(!names.has(kind)||src?.type!=='email.sending'||src?.zoneId!==env.MAIL_EVENT_ZONE||src?.domain!==env.MAIL_EVENT_DOMAIN||m?.accountId!==env.MAIL_EVENT_ACCOUNT||m?.eventSubscriptionId!==env.MAIL_EVENT_SUBSCRIPTION||m?.eventSchemaVersion!==1||p?.sender!==env.MAIL_SENDER||p?.recipient!==env.TEST_MAIL_RECIPIENT||!id(p?.eventId)||!messageId(p?.messageId)||p?.delivery?.status!==kind||p?.terminal!==(kind!=='deferred')||!Number.isSafeInteger(time)||time<=0) throw new Error('Invalid test delivery event');
  const inserted=await db.prepare('INSERT INTO mail_delivery_events(event_id,message_id,kind,event_at) VALUES(?,?,?,?) ON CONFLICT(event_id) DO NOTHING RETURNING event_id').bind(p.eventId,p.messageId,kind,time).first();
  if(inserted)return {status:'recorded'};
  const old=await db.prepare('SELECT message_id,kind,event_at FROM mail_delivery_events WHERE event_id=?').bind(p.eventId).first();
  if(!old||old.message_id!==p.messageId||old.kind!==kind||old.event_at!==time)throw new Error('Conflicting test delivery event');
  return {status:'duplicate'};
}

export async function getMailStatus(db,kind,jobId){
  if(!['purchase','withdrawal'].includes(kind)||typeof jobId!=='string'||!jobId)return {status:'not_found'};
  const attempt=await db.prepare('SELECT state,message_id FROM mail_dispatch_attempts WHERE kind=? AND job_id=?').bind(kind,jobId).first();
  if(!attempt)return {status:'not_found'};
  if(attempt.state==='unknown')return {status:'needs_review'};
  if(attempt.state!=='accepted'||!messageId(attempt.message_id))return {status:'pending'};
  const shared=await db.prepare("SELECT COUNT(*) AS count FROM mail_dispatch_attempts WHERE state='accepted' AND message_id=?").bind(attempt.message_id).first();
  if(shared?.count!==1)return {status:'needs_review'};
  const rows=(await db.prepare('SELECT kind FROM mail_delivery_events WHERE message_id=?').bind(attempt.message_id).all()).results;
  const events=new Set(rows.map(x=>x.kind));
  if(['bounced','failed','rejected','complained'].some(x=>events.has(x)))return {status:'needs_review'};
  if(events.has('delivered'))return {status:'delivered_to_mail_server'};
  if(events.has('deferred'))return {status:'deferred'};
  return {status:'pending'};
}

// Queue bodies can contain personal data. Never log bodies or exception text.
// Acknowledge only after durable insertion or verification of an exact replay.
export async function consumeTestMailEvents(batch,env){
  for(const message of batch.messages){
    try {
      if(!env?.MAIL_EVENT_QUEUE||batch.queue!==env.MAIL_EVENT_QUEUE) throw new Error('Wrong queue');
      await recordMailEvent(env.DB,env,message.body);
      message.ack();
    } catch { message.retry(); }
  }
}

// Restricted, read-only operator data. The caller must authenticate operators;
// this function is intentionally not exposed as a public HTTP endpoint.
export async function listTestMailIssues(db,now=Date.now()){
  if(!Number.isSafeInteger(now)||now<0) throw new TypeError('Invalid clock');
  const cutoff=now-15*60*1000;
  const attempts=(await db.prepare(`WITH accepted AS (
    SELECT message_id,COUNT(*) AS matches FROM mail_dispatch_attempts WHERE state='accepted' GROUP BY message_id
  ), signals AS (
    SELECT message_id, MAX(kind IN ('bounced','failed','rejected','complained')) AS failure,
      MAX(kind='delivered') AS delivered, MAX(kind='deferred') AS deferred
    FROM mail_delivery_events GROUP BY message_id
  ) SELECT a.kind,a.job_id,a.state,a.started_at,a.finished_at,a.message_id,
    COALESCE(accepted.matches,0) AS matches,COALESCE(signals.failure,0) AS failure,
    COALESCE(signals.delivered,0) AS delivered,COALESCE(signals.deferred,0) AS deferred
    FROM mail_dispatch_attempts a LEFT JOIN accepted ON accepted.message_id=a.message_id
    LEFT JOIN signals ON signals.message_id=a.message_id ORDER BY a.started_at`).bind().all()).results;
  const issues=[];
  for(const a of attempts){
    let reason;
    if(a.state==='unknown') reason='outcome_unknown';
    else if(a.state==='processing'&&a.started_at<=cutoff) reason='processing_timeout';
    else if(a.state==='accepted'){
      let status='pending';
      if(messageId(a.message_id)){
        if(a.matches!==1||a.failure)status='needs_review';
        else if(a.delivered)status='delivered_to_mail_server';
        else if(a.deferred)status='deferred';
      }
      if(status==='needs_review') reason='provider_or_matching_failure';
      else if((status==='pending'||status==='deferred')&&(a.finished_at??a.started_at)<=cutoff) reason='delivery_timeout';
    }
    if(reason)issues.push({kind:a.kind,jobId:a.job_id,reason});
  }
  const unmatched=(await db.prepare("SELECT e.event_id FROM mail_delivery_events e WHERE NOT EXISTS (SELECT 1 FROM mail_dispatch_attempts a WHERE a.state='accepted' AND a.message_id=e.message_id) ORDER BY e.event_at").bind().all()).results;
  for(const row of unmatched) issues.push({eventId:row.event_id,reason:'unmatched_event'});
  return issues;
}

// Detect jobs that never acquired a dispatch attempt, including jobs which
// fail preparation indefinitely. This is read-only and does not send mail.
export async function listUnclaimedTestMailJobs(db,now=Date.now()){
  if(!Number.isSafeInteger(now)||now<0) throw new TypeError('Invalid clock');
  const cutoff=now-15*60*1000;
  const rows=(await db.prepare(`SELECT 'purchase' AS kind,j.order_id AS job_id
    FROM confirmation_jobs j LEFT JOIN mail_dispatch_attempts a ON a.kind='purchase' AND a.job_id=j.order_id
    WHERE a.job_id IS NULL AND j.created_at<=?
    UNION ALL
    SELECT 'withdrawal' AS kind,q.request_id AS job_id
    FROM withdrawal_receipts q JOIN withdrawal_requests r ON r.id=q.request_id
    LEFT JOIN mail_dispatch_attempts a ON a.kind='withdrawal' AND a.job_id=q.request_id
    WHERE q.status='pending' AND a.job_id IS NULL AND r.received_at<=?
    ORDER BY kind,job_id LIMIT 501`).bind(cutoff,cutoff).all()).results;
  const issues=rows.slice(0,500).map(row=>({kind:row.kind,jobId:row.job_id,reason:'dispatch_not_started'}));
  if(rows.length>500)issues.push({reason:'additional_unclaimed_jobs'});
  return issues;
}
