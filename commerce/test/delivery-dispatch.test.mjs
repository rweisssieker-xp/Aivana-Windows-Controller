import { test } from 'node:test';
import assert from 'node:assert/strict';
import { DatabaseSync } from 'node:sqlite';
import { readFileSync } from 'node:fs';
import { createHash } from 'node:crypto';
import worker, { validateSession } from '../worker.mjs';
import { sendPurchaseJob, sendWithdrawalJob } from '../delivery-dispatch.mjs';

const digest=x=>createHash('sha256').update(JSON.stringify(x)).digest('base64url');
function database(){
  const raw=new DatabaseSync(':memory:');
  raw.exec(readFileSync(new URL('../schema.sql',import.meta.url),'utf8'));
  raw.exec(readFileSync(new URL('../migrations/0010_withdrawal.sql',import.meta.url),'utf8'));
  raw.exec(readFileSync(new URL('../migrations/0013_test_mail_attempts.sql',import.meta.url),'utf8'));
  return {raw,prepare(sql){return {bind(...args){const s=raw.prepare(sql); return {async first(){return s.get(...args)||null;},async all(){return {results:s.all(...args)};},async run(){return s.run(...args);}};}};}};
}
const recipient='owner@example.test',sender='sender@example.test', orderId='synthetic-order';
function seedPurchase(db){
 const o=orderId, at=1700000000000, lang='de';
 db.raw.prepare('INSERT INTO orders VALUES(?,?,?,?)').run(o,'claim-hash',at,'cs_test');
 db.raw.prepare('INSERT INTO checkout_attempts VALUES(?,?)').run(o,lang);
 db.raw.prepare('INSERT INTO confirmation_obligations VALUES(?,?)').run(o,at);
 db.raw.prepare('INSERT INTO confirmation_jobs VALUES(?,?)').run(o,at);
 db.raw.prepare('INSERT INTO entitlements(id,order_id,session_id,payment_intent) VALUES(?,?,?,?)').run('l',o,'cs_test','pi_test');
 const terms={language:lang,version:'v1',text:'Synthetic terms'};
 db.raw.prepare('INSERT INTO contract_acceptances VALUES(?,?,?,?,?,?)').run(o,lang,terms.version,terms.text,digest(terms),at);
 for(const [kind,version,text] of [['early_start','early-v1','Synthetic early'],['withdrawal_ack','ack-v1','Synthetic acknowledgement']]){
  const doc={kind,language:lang,version,text};db.raw.prepare('INSERT INTO contract_declarations VALUES(?,?,?,?,?,?,?)').run(o,kind,lang,version,text,digest(doc),at);
 }
}
function session(){return {id:'cs_test',payment_intent:'pi_test',payment_status:'paid',mode:'payment',currency:'eur',amount_total:2900,livemode:false,customer_details:{email:recipient},metadata:{orderId},line_items:{data:[{quantity:1,price:{id:'price_test',product:'prod_test'}}],has_more:false}};}
const env=()=>({STRIPE_LIVE:'false',STRIPE_PRICE_ID:'price_test',STRIPE_PRODUCT_ID:'prod_test',TEST_MAIL_RECIPIENT:recipient,MAIL_DELIVERY_TEST_ENABLED:'true',MAIL_SENDER:sender});
function seedWithdrawal(db){
 const id='a5a2e137-92c3-4214-952a-91e70d5a81ac';
 db.raw.prepare('INSERT INTO withdrawal_requests VALUES(?,?,?,?,?)').run(id,'Synthetic Name','Synthetic contract',recipient,1700000000000);
 db.raw.prepare("INSERT INTO withdrawal_receipts VALUES(?,'pending')").run(id); return id;
}

test('paid purchase sends its own validated historical evidence to the permitted test recipient once',async()=>{
 const db=database();seedPurchase(db);const calls=[];const binding={send:async x=>{calls.push(x);return {messageId:'provider-id'};}};
 const args={db,env:env(),binding,jobId:orderId,loadSession:async()=>session(),validateSession};
 assert.equal((await sendPurchaseJob(args)).status,'accepted_by_provider');
 assert.equal((await sendPurchaseJob(args)).status,'already_claimed');
 assert.equal(calls.length,1);assert.equal(calls[0].to,recipient);
 for(const text of ['Synthetic terms','Synthetic early','Synthetic acknowledgement'])assert.ok(calls[0].text.includes(text));
 assert.equal(db.raw.prepare('SELECT state FROM mail_dispatch_attempts').get().state,'accepted');
 assert.equal(db.raw.prepare('SELECT count(*) n FROM confirmation_obligations').get().n,1);
});

test('withdrawal receipt uses stored declaration and actual receipt timestamp',async()=>{
 const db=database(),id=seedWithdrawal(db),calls=[];
 const result=await sendWithdrawalJob({db,env:env(),binding:{send:async x=>{calls.push(x);return {messageId:'provider-id'};}},jobId:id});
 assert.equal(result.status,'accepted_by_provider');assert.equal(calls[0].to,recipient);
 assert.match(calls[0].text,/Synthetic Name/);assert.match(calls[0].text,/Synthetic contract/);assert.match(calls[0].text,/2023-11-14T22:13:20.000Z/);
 assert.equal(db.raw.prepare('SELECT status FROM withdrawal_receipts').get().status,'pending');
});

test('wrong payment, recipient or evidence cannot trigger a send or claim',async()=>{
 for(const mutate of [s=>{s.payment_status='unpaid';},s=>{s.metadata.orderId='other';},s=>{s.payment_intent='pi_other';},s=>{s.customer_details.email='other@example.test';}]){
  const db=database();seedPurchase(db);let count=0;const s=session();mutate(s);
  await sendPurchaseJob({db,env:env(),binding:{send:async()=>{count++;}},jobId:orderId,loadSession:async()=>s,validateSession});
  assert.equal(count,0);assert.equal(db.raw.prepare('SELECT count(*) n FROM mail_dispatch_attempts').get().n,0);
 }
});

test('live mode, disabled mode and non-allowlisted withdrawal never send',async()=>{
 for(const config of [{...env(),STRIPE_LIVE:'true'},{...env(),MAIL_DELIVERY_TEST_ENABLED:'false'},{...env(),TEST_MAIL_RECIPIENT:'other@example.test'}]){
  const db=database(),id=seedWithdrawal(db);let count=0;
  await sendWithdrawalJob({db,env:config,binding:{send:async()=>{count++;}},jobId:id});
  assert.equal(count,0);assert.equal(db.raw.prepare('SELECT count(*) n FROM mail_dispatch_attempts').get().n,0);
 }
});

test('uncertain provider failure is not retried by a later dispatch',async()=>{
 const db=database(),id=seedWithdrawal(db);let count=0;const args={db,env:env(),binding:{send:async()=>{count++;throw Error('private');}},jobId:id};
 assert.equal((await sendWithdrawalJob(args)).status,'outcome_unknown');
 assert.equal((await sendWithdrawalJob(args)).status,'already_claimed');
 assert.equal(count,1);assert.equal(db.raw.prepare('SELECT state FROM mail_dispatch_attempts').get().state,'unknown');
});

test('concurrent dispatches claim a job atomically before invoking the provider',async()=>{
 const db=database(),id=seedWithdrawal(db);let release,entered;const pause=new Promise(r=>{release=r;}), started=new Promise(r=>{entered=r;});let count=0;
 const args={db,env:env(),binding:{send:async()=>{count++;entered();await pause;return {messageId:'provider-id'};}},jobId:id};
 const first=sendWithdrawalJob(args);await started;
 assert.equal((await sendWithdrawalJob(args)).status,'already_claimed');release();assert.equal((await first).status,'accepted_by_provider');assert.equal(count,1);
});

test('scheduled test processor connects both queues and sends each eligible job only once',async()=>{
 const db=database();seedPurchase(db);seedWithdrawal(db);const sent=[];
 const e={...env(),DB:db,EMAIL:{send:async message=>{sent.push(message);return {messageId:'id-'+sent.length};}},STRIPE_SECRET_KEY:'test-secret',STRIPE_WEBHOOK_SECRET:'test-webhook'};
 const originalFetch=globalThis.fetch;globalThis.fetch=async()=>new Response(JSON.stringify(session()),{status:200});
 try {
  await worker.scheduled({},e,{});await worker.scheduled({},e,{});
  assert.equal(sent.length,2);
  assert.ok(sent.some(x=>x.text.includes('Synthetic terms')));
  assert.ok(sent.some(x=>x.text.includes('Synthetic contract')));
  assert.equal(db.raw.prepare("SELECT count(*) n FROM mail_dispatch_attempts WHERE state='accepted'").get().n,2);
  assert.equal(db.raw.prepare('SELECT count(*) n FROM confirmation_obligations').get().n,1);
 } finally {globalThis.fetch=originalFetch;}
});

test('a database failure after provider acceptance leaves processing state and prevents another send',async()=>{
 const db=database(),id=seedWithdrawal(db),original=db.prepare.bind(db);let count=0;
 const broken={prepare(sql){if(sql.startsWith('UPDATE mail_dispatch_attempts'))return {bind(){return {async first(){throw Error('storage failed');}};}};return original(sql);}};
 const binding={send:async()=>{count++;return {messageId:'accepted'};}};
 await assert.rejects(sendWithdrawalJob({db:broken,env:env(),binding,jobId:id}));
 assert.equal(db.raw.prepare('SELECT state FROM mail_dispatch_attempts').get().state,'processing');
 assert.equal((await sendWithdrawalJob({db,env:env(),binding,jobId:id})).status,'already_claimed');assert.equal(count,1);
});

test('scheduler reports an uncertain send without automatically repeating it',async()=>{
 const db=database();seedWithdrawal(db);let count=0;
 const e={...env(),DB:db,EMAIL:{send:async()=>{count++;throw Error('private provider data');}}};
 await assert.rejects(worker.scheduled({},e,{}),/Test mail requires operator review/);
 await assert.rejects(worker.scheduled({},e,{}),/Test mail requires operator review/);
 assert.equal(count,1);assert.equal(db.raw.prepare('SELECT state FROM mail_dispatch_attempts').get().state,'unknown');
});

test('abandoned in-flight attempts remain visible to the operator without resending',async()=>{
 const db=database(),id=seedWithdrawal(db),old=Date.now()-180000;
 db.raw.prepare("INSERT INTO mail_dispatch_attempts(kind,job_id,state,started_at) VALUES('withdrawal',?,'processing',?)").run(id,old);
 let count=0;const e={...env(),DB:db,EMAIL:{send:async()=>{count++;}}};
 await assert.rejects(worker.scheduled({},e,{}),/Test mail requires operator review/);
 assert.equal(count,0);assert.equal(db.raw.prepare('SELECT state FROM mail_dispatch_attempts').get().state,'processing');
});

test('altered evidence and foreign order data never reach the provider',async()=>{
 const db=database();seedPurchase(db);db.raw.exec('DROP TRIGGER contract_acceptances_no_update');
 db.raw.prepare("UPDATE contract_acceptances SET text='tampered' WHERE order_id=?").run(orderId);
 let count=0;const result=await sendPurchaseJob({db,env:env(),binding:{send:async()=>{count++;}},jobId:orderId,loadSession:async()=>session(),validateSession});
 assert.equal(result.status,'not_ready');assert.equal(count,0);assert.equal(db.raw.prepare('SELECT count(*) n FROM mail_dispatch_attempts').get().n,0);
});
