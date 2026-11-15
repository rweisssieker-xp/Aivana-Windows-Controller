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
  const attempts=(await db.prepare('SELECT kind,job_id,state,started_at,finished_at FROM mail_dispatch_attempts ORDER BY started_at').bind().all()).results;
  const issues=[];
  for(const a of attempts){
    let reason;
    if(a.state==='unknown') reason='outcome_unknown';
    else if(a.state==='processing'&&a.started_at<=cutoff) reason='processing_timeout';
    else if(a.state==='accepted'){
      const status=(await getMailStatus(db,a.kind,a.job_id)).status;
      if(status==='needs_review') reason='provider_or_matching_failure';
      else if((status==='pending'||status==='deferred')&&(a.finished_at??a.started_at)<=cutoff) reason='delivery_timeout';
    }
    if(reason)issues.push({kind:a.kind,jobId:a.job_id,reason});
  }
  const unmatched=(await db.prepare("SELECT e.event_id FROM mail_delivery_events e WHERE NOT EXISTS (SELECT 1 FROM mail_dispatch_attempts a WHERE a.state='accepted' AND a.message_id=e.message_id) ORDER BY e.event_at").bind().all()).results;
  for(const row of unmatched) issues.push({eventId:row.event_id,reason:'unmatched_event'});
  return issues;
}
