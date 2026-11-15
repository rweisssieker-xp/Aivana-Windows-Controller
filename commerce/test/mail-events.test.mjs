import {test} from 'node:test';
import assert from 'node:assert/strict';
import {DatabaseSync} from 'node:sqlite';
import {readFileSync} from 'node:fs';
import {recordMailEvent,getMailStatus,consumeTestMailEvents,listTestMailIssues} from '../mail-events.mjs';
import worker from '../worker.mjs';

function database(){const raw=new DatabaseSync(':memory:');raw.exec(readFileSync(new URL('../migrations/0013_test_mail_attempts.sql',import.meta.url),'utf8'));raw.exec(readFileSync(new URL('../migrations/0014_test_mail_events.sql',import.meta.url),'utf8'));return {raw,prepare(sql){return {bind(...args){const s=raw.prepare(sql);return {async first(){return s.get(...args)||null;},async all(){return {results:s.all(...args)};},async run(){return s.run(...args);}};}};}};}
const config={MAIL_EVENT_TEST_ENABLED:'true',STRIPE_LIVE:'false',MAIL_EVENT_ACCOUNT:'account-test',MAIL_EVENT_ZONE:'zone-test',MAIL_EVENT_SUBSCRIPTION:'subscription-test',MAIL_EVENT_DOMAIN:'makrivo.example.test',MAIL_SENDER:'noreply@makrivo.example.test',TEST_MAIL_RECIPIENT:'owner@example.test'};
function event(kind='delivered',id='event-1'){return {type:'cf.email.sending.message.'+kind,source:{type:'email.sending',zoneId:'zone-test',domain:'makrivo.example.test'},payload:{eventId:id,messageId:'msg-test',sender:'noreply@makrivo.example.test',recipient:'owner@example.test',terminal:kind!=='deferred',delivery:{status:kind},subject:'private content must not be stored'},metadata:{accountId:'account-test',eventSubscriptionId:'subscription-test',eventSchemaVersion:1,eventTimestamp:'2026-11-15T11:00:00.000Z'}};}
function attempt(db,messageId='msg-test'){db.raw.prepare("INSERT INTO mail_dispatch_attempts(kind,job_id,state,started_at,finished_at,message_id) VALUES('purchase','job-test','accepted',1,2,?)").run(messageId);}

test('provider delivery event is idempotent and can arrive before the acceptance record',async()=>{
 const db=database();assert.equal((await recordMailEvent(db,config,event())).status,'recorded');assert.equal((await recordMailEvent(db,config,event())).status,'duplicate');
 attempt(db);assert.equal((await getMailStatus(db,'purchase','job-test')).status,'delivered_to_mail_server');
 assert.equal(db.raw.prepare('SELECT count(*) n FROM mail_delivery_events').get().n,1);
 assert.ok(!JSON.stringify(db.raw.prepare('SELECT * FROM mail_delivery_events').get()).includes('private content'));
});

test('delayed and terminal failure events never claim delivery',async()=>{
 for(const kind of ['deferred','bounced','failed','rejected','complained']){
  const db=database();attempt(db);await recordMailEvent(db,config,event(kind));
  assert.equal((await getMailStatus(db,'purchase','job-test')).status,kind==='deferred'?'deferred':'needs_review');
 }
});

test('contradictory terminal events require review even if delivery was reported',async()=>{
 const db=database();attempt(db);await recordMailEvent(db,config,event('delivered','e-1'));await recordMailEvent(db,config,event('bounced','e-2'));
 assert.equal((await getMailStatus(db,'purchase','job-test')).status,'needs_review');
});

test('unknown, foreign, disabled or unallowlisted events are refused without persistence',async()=>{
 for(const mutate of [e=>{e.metadata.accountId='foreign';},e=>{e.source.zoneId='foreign';},e=>{e.source.domain='foreign';},e=>{e.metadata.eventSubscriptionId='foreign';},e=>{e.payload.recipient='other@example.test';},e=>{e.payload.sender='other@example.test';},e=>{e.payload.terminal=false;},e=>{e.type='cf.email.sending.message.fake';}]){
  const db=database(), x=event();mutate(x);await assert.rejects(recordMailEvent(db,config,x));assert.equal(db.raw.prepare('SELECT count(*) n FROM mail_delivery_events').get().n,0);
 }
 const db=database();await assert.rejects(recordMailEvent(db,{...config,STRIPE_LIVE:'true'},event()));await assert.rejects(recordMailEvent(db,{...config,MAIL_EVENT_TEST_ENABLED:'false'},event()));
});

test('same event identifier with conflicting details is detected',async()=>{
 const db=database();await recordMailEvent(db,config,event('delivered','same'));await assert.rejects(recordMailEvent(db,config,event('bounced','same')));assert.equal(db.raw.prepare('SELECT count(*) n FROM mail_delivery_events').get().n,1);
});

test('ambiguous message ID or unfinished provider attempt never claims delivery',async()=>{
 const db=database();attempt(db);db.raw.prepare("INSERT INTO mail_dispatch_attempts(kind,job_id,state,started_at,finished_at,message_id) VALUES('withdrawal','other','accepted',1,2,'msg-test')").run();await recordMailEvent(db,config,event());
 assert.equal((await getMailStatus(db,'purchase','job-test')).status,'needs_review');
});

test('queue acknowledges only persisted or identical events and retries individual failures',async()=>{
 const db=database(), decisions=[];
 const message=(body,label)=>({body,ack(){decisions.push(label+':ack')},retry(){decisions.push(label+':retry')}});
 const invalid=event();invalid.payload.recipient='other@example.test';
 await consumeTestMailEvents({queue:'mail-events-test',messages:[message(event(),'one'),message(event(),'duplicate'),message(invalid,'foreign')]},{...config,MAIL_EVENT_QUEUE:'mail-events-test',DB:db});
 assert.deepEqual(decisions,['one:ack','duplicate:ack','foreign:retry']);
 const fail={prepare(){throw new Error('database unavailable')}};
 decisions.length=0;await consumeTestMailEvents({queue:'mail-events-test',messages:[message(event(),'db')]},{...config,MAIL_EVENT_QUEUE:'mail-events-test',DB:fail});assert.deepEqual(decisions,['db:retry']);
 decisions.length=0;await consumeTestMailEvents({queue:'other',messages:[message(event(),'wrong-queue')]},{...config,MAIL_EVENT_QUEUE:'mail-events-test',DB:db});assert.deepEqual(decisions,['wrong-queue:retry']);
});

test('operator issue list includes unknown, stale and unmatched cases without message content',async()=>{
 const db=database(),now=1800000;
 db.raw.prepare("INSERT INTO mail_dispatch_attempts(kind,job_id,state,started_at,finished_at,message_id) VALUES('purchase','unknown-job','unknown',1,2,NULL),('withdrawal','stuck-job','processing',1,NULL,NULL),('purchase','waiting-job','accepted',1,2,'waiting-id')").run();
 await recordMailEvent(db,config,event('bounced','unmatched'));
 const issues=await listTestMailIssues(db,now);
 assert.deepEqual(new Set(issues.map(x=>x.reason)),new Set(['outcome_unknown','processing_timeout','delivery_timeout','unmatched_event']));
 assert.ok(!JSON.stringify(issues).includes('owner@example.test'));assert.ok(!JSON.stringify(issues).includes('private content'));
});

test('worker routes an isolated test queue and refuses events when the live gate is set',async()=>{
 const db=database(),decisions=[];
 const batch={queue:'mail-events-test',messages:[{body:event(),ack(){decisions.push('ack')},retry(){decisions.push('retry')}}]};
 await worker.queue(batch,{...config,MAIL_EVENT_QUEUE:'mail-events-test',DB:db});
 await worker.queue(batch,{...config,MAIL_EVENT_QUEUE:'mail-events-test',DB:db,STRIPE_LIVE:'true'});
 assert.deepEqual(decisions,['ack','retry']);
});

test('unclaimed purchase and withdrawal jobs past the threshold appear without exposing contact details',async()=>{
 const db=database(),now=1800000;
 db.raw.exec("CREATE TABLE confirmation_jobs(order_id TEXT PRIMARY KEY,created_at INTEGER NOT NULL);CREATE TABLE withdrawal_requests(id TEXT PRIMARY KEY,received_at INTEGER NOT NULL,contact_email TEXT);CREATE TABLE withdrawal_receipts(request_id TEXT PRIMARY KEY,status TEXT NOT NULL)");
 db.raw.exec("INSERT INTO confirmation_jobs VALUES('old-purchase',1),('new-purchase',1799999),('already-attempted',1);INSERT INTO withdrawal_requests VALUES('old-withdrawal',1,'private@example.test'),('already-sent',1,'private@example.test');INSERT INTO withdrawal_receipts VALUES('old-withdrawal','pending'),('already-sent','sent');INSERT INTO mail_dispatch_attempts VALUES('purchase','already-attempted','unknown',1,2,NULL)");
 const {listUnclaimedTestMailJobs}=await import('../mail-events.mjs');
 const issues=await listUnclaimedTestMailJobs(db,now);
 assert.deepEqual(issues,[{kind:'purchase',jobId:'old-purchase',reason:'dispatch_not_started'},{kind:'withdrawal',jobId:'old-withdrawal',reason:'dispatch_not_started'}]);
 assert.ok(!JSON.stringify(issues).includes('private@example.test'));
});

test('issue classification uses a constant number of database queries for many dispatches',async()=>{
 const db=database();
 for(let i=0;i<20;i++) db.raw.prepare("INSERT INTO mail_dispatch_attempts VALUES('purchase',?,'accepted',1,2,?)").run('job-'+i,'msg-'+i);
 let count=0;const measured={prepare(sql){count++;return db.prepare(sql)}};
 const issues=await listTestMailIssues(measured,1800000);
 assert.equal(issues.length,20);assert.ok(issues.every(x=>x.reason==='delivery_timeout'));
 assert.ok(count<=3,`too many queries: ${count}`);
});

test('aggregated issue list reports ambiguous and failed delivery but excludes confirmed and recent attempts',async()=>{
 const db=database();
 db.raw.exec("INSERT INTO mail_dispatch_attempts VALUES('purchase','delivered','accepted',1,2,'message-delivered'),('purchase','failed','accepted',1,2,'message-failed'),('purchase','ambiguous-one','accepted',1,2,'shared'),('withdrawal','ambiguous-two','accepted',1,2,'shared'),('purchase','recent','accepted',1799999,1799999,'message-recent')");
 db.raw.exec("INSERT INTO mail_delivery_events VALUES('delivered-event','message-delivered','delivered',100),('failed-event','message-failed','bounced',100)");
 const issues=await listTestMailIssues(db,1800000);
 assert.deepEqual(new Set(issues.map(x=>x.jobId)),new Set(['failed','ambiguous-one','ambiguous-two']));
 assert.ok(issues.every(x=>x.reason==='provider_or_matching_failure'));
});

test('unclaimed queue report signals truncation without hiding an overflow',async()=>{
 const db=database();
 db.raw.exec('CREATE TABLE confirmation_jobs(order_id TEXT PRIMARY KEY,created_at INTEGER NOT NULL);CREATE TABLE withdrawal_requests(id TEXT PRIMARY KEY,received_at INTEGER NOT NULL);CREATE TABLE withdrawal_receipts(request_id TEXT PRIMARY KEY,status TEXT NOT NULL)');
 const insert=db.raw.prepare('INSERT INTO confirmation_jobs VALUES(?,1)');for(let i=0;i<501;i++)insert.run('purchase-'+String(i).padStart(4,'0'));
 const {listUnclaimedTestMailJobs}=await import('../mail-events.mjs');
 const issues=await listUnclaimedTestMailJobs(db,1800000);
 assert.equal(issues.length,501);assert.equal(issues.at(-1).reason,'additional_unclaimed_jobs');
});
