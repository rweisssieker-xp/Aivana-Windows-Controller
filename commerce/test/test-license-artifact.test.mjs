import {test} from 'node:test';
import assert from 'node:assert/strict';
import {DatabaseSync} from 'node:sqlite';
import {readFileSync} from 'node:fs';
import {createHash} from 'node:crypto';
import {sendCompleteConfirmationTest} from '../test-confirmation-gate.mjs';
import {issueSyntheticArtifact} from '../test-license-artifact.mjs';
import {recordMailEvent} from '../mail-events.mjs';

const digest=x=>createHash('sha256').update(JSON.stringify(x)).digest('base64url');
const id='synthetic-order',licenseId='synthetic-license',deviceId='a'.repeat(64),at=1700000000000;
function fixture(){
 const raw=new DatabaseSync(':memory:');
 for(const file of ['../schema.sql','../migrations/0013_test_mail_attempts.sql','../migrations/0014_test_mail_events.sql','../migrations/0016_test_confirmation_supplements.sql','../migrations/0019_test_confirmation_dispatch_proof.sql','../migrations/0021_test_signed_license_artifacts.sql'])raw.exec(readFileSync(new URL(file,import.meta.url),'utf8'));
 const db={raw,async batch(stmts){raw.exec('BEGIN IMMEDIATE');try{const out=stmts.map(s=>s._run());raw.exec('COMMIT');return out;}catch(e){raw.exec('ROLLBACK');throw e;}},prepare(sql){return {bind(...p){const s=raw.prepare(sql);return {_run:()=>s.run(...p),first:async()=>s.get(...p)||null,all:async()=>({results:s.all(...p)}),run:async()=>s.run(...p)};}};}};
 raw.prepare('INSERT INTO orders VALUES(?,?,?,?)').run(id,'synthetic-claim',at,'cs_test_gate');
 raw.prepare('INSERT INTO checkout_attempts VALUES(?,?)').run(id,'de');
 raw.prepare('INSERT INTO confirmation_obligations VALUES(?,?)').run(id,at);
 raw.prepare('INSERT INTO confirmation_jobs VALUES(?,?)').run(id,at);
 raw.prepare('INSERT INTO entitlements(id,order_id,session_id,payment_intent) VALUES(?,?,?,?)').run(licenseId,id,'cs_test_gate','pi_test');
 raw.prepare('INSERT INTO devices VALUES(?,?,1)').run(licenseId,deviceId);
 const terms={language:'de',version:'terms-v1',text:'Synthetic terms'};
 raw.prepare('INSERT INTO contract_acceptances VALUES(?,?,?,?,?,?)').run(id,'de',terms.version,terms.text,digest(terms),at);
 for(const [kind,version,text] of [['early_start','early-v1','Synthetic consent'],['withdrawal_ack','ack-v1','Synthetic acknowledgement']]){
  const d={kind,language:'de',version,text};raw.prepare('INSERT INTO contract_declarations VALUES(?,?,?,?,?,?,?)').run(id,kind,'de',version,text,digest(d),at);
 }
 for(const [kind,version,text] of [['withdrawal_notice','notice-v1','Synthetic notice'],['withdrawal_form','form-v1','Synthetic form']]){
  const d={kind,language:'de',version,text};raw.prepare('INSERT INTO test_confirmation_supplements VALUES(?,?,?,?,?,?,?)').run(id,kind,'de',version,text,digest(d),at);
 }
 const env={STRIPE_LIVE:'false',FULL_CONFIRMATION_TEST_ENABLED:'true',MAIL_DELIVERY_TEST_ENABLED:'true',LICENSE_DELIVERY_PROOF_TEST_ENABLED:'true',TEST_LICENSE_SIGNATURE_ENABLED:'true',TEST_LICENSE_ID:licenseId,TEST_DEVICE_ID:deviceId,TEST_MAIL_RECIPIENT:'owner@example.test',MAIL_SENDER:'sender@example.test',STRIPE_PRICE_ID:'price_test',STRIPE_PRODUCT_ID:'prod_test'};
 const session={id:'cs_test_gate',payment_intent:'pi_test',metadata:{orderId:id},customer_details:{email:env.TEST_MAIL_RECIPIENT},livemode:false,mode:'payment',payment_status:'paid',currency:'eur',amount_total:2900,line_items:{data:[{quantity:1,price:{id:'price_test',product:'prod_test'}}],has_more:false}};
 const send=()=>sendCompleteConfirmationTest({db,env,binding:{send:async()=>({messageId:'test-provider-id'})},jobId:id,loadSession:async()=>session,validateSession:()=>true});
 const delivery=(kind='delivered')=>raw.prepare('INSERT INTO mail_delivery_events VALUES(?,?,?,?)').run('event-'+kind,'test-provider-id',kind,Date.now());
 const issue=(overrides={})=>issueSyntheticArtifact({db,env,licenseId,deviceId,...overrides});
 return {db,env,send,delivery,issue};
}

test('only delivered complete test confirmation permits one verifiable synthetic artifact per device',async()=>{
 const f=fixture();await f.send();assert.equal((await f.issue()).status,'blocked');
 f.delivery();const result=await f.issue();assert.equal(result.status,'test_artifact_issued');
 assert.match(result.artifact.payload,/^[A-Za-z0-9_-]+$/);assert.match(result.artifact.signature,/^[A-Za-z0-9_-]+$/);
 const payload=JSON.parse(Buffer.from(result.artifact.payload,'base64url').toString('utf8'));
 assert.equal(payload.marker,'MAKRIVO-TEST-ONLY-NOT-A-LICENSE');assert.equal(payload.deviceId,deviceId);
 const key=await crypto.subtle.importKey('spki',Buffer.from(result.artifact.publicKey,'base64url'),{name:'RSASSA-PKCS1-v1_5',hash:'SHA-256'},false,['verify']);
 assert.equal(await crypto.subtle.verify('RSASSA-PKCS1-v1_5',key,Buffer.from(result.artifact.signature,'base64url'),new TextEncoder().encode(result.artifact.payload)),true);
 assert.equal((await f.issue()).status,'blocked');assert.equal(f.db.raw.prepare('SELECT count(*) n FROM test_signed_license_artifacts').get().n,1);
 assert.equal(f.db.raw.prepare('SELECT count(*) n FROM confirmation_obligations').get().n,1);
});

test('refund, deactivation, negative delivery, wrong config and foreign identifiers block an artifact',async()=>{
 for(const change of [
  f=>f.db.raw.prepare("INSERT INTO revoked_payments VALUES('pi_test')").run(),
  f=>f.db.raw.prepare('UPDATE entitlements SET revoked=1').run(),
  f=>f.db.raw.prepare('UPDATE devices SET active=0').run(),
  f=>f.delivery('bounced'),f=>f.delivery('failed'),f=>f.delivery('rejected'),f=>f.delivery('complained'),
  f=>{f.env.STRIPE_LIVE='true'},f=>{f.env.TEST_LICENSE_SIGNATURE_ENABLED='false'},f=>{f.env.TEST_DEVICE_ID='b'.repeat(64)},
 ]){const f=fixture();await f.send();f.delivery();change(f);assert.equal((await f.issue()).status,'blocked');assert.equal(f.db.raw.prepare('SELECT count(*) n FROM test_signed_license_artifacts').get().n,0);}
});

test('refund or bounce committed immediately before issuance is observed by the atomic write',async()=>{
 for(const signal of ['refund','bounced']){
  const f=fixture();await f.send();f.delivery();const original=f.db.prepare.bind(f.db);let injected=false;
  const interleaved={prepare(sql){if(sql.startsWith('INSERT INTO test_signed_license_artifacts')&&!injected){injected=true;if(signal==='refund')f.db.raw.prepare("INSERT INTO revoked_payments VALUES('pi_test')").run();else f.delivery('bounced');}return original(sql);}};
  assert.equal((await f.issue({db:interleaved})).status,'blocked');assert.equal(injected,true);
  assert.equal(f.db.raw.prepare('SELECT count(*) n FROM test_signed_license_artifacts').get().n,0);
 }
});

test('parallel attempts cannot emit two artifacts and stored evidence cannot be rewritten',async()=>{
 const f=fixture();await f.send();f.delivery();
 const results=await Promise.all([f.issue(),f.issue()]);assert.equal(results.filter(x=>x.status==='test_artifact_issued').length,1);
 assert.equal(f.db.raw.prepare('SELECT count(*) n FROM test_signed_license_artifacts').get().n,1);
 assert.throws(()=>f.db.raw.exec("UPDATE test_signed_license_artifacts SET artifact_digest='changed'"));
 assert.throws(()=>f.db.raw.exec('DELETE FROM test_signed_license_artifacts'));
});

test('a proof for a different payment or checkout session can never authorize an artifact',async()=>{
 for(const field of ['payment_intent','session_id']){
  const f=fixture();await f.send();f.delivery();
  if(field==='payment_intent'){
   f.db.raw.prepare("INSERT INTO revoked_payments VALUES('pi_test')").run();
   f.db.raw.prepare("UPDATE entitlements SET payment_intent='pi_replacement' WHERE id=?").run(licenseId);
  } else f.db.raw.prepare("UPDATE entitlements SET session_id='cs_test_replacement' WHERE id=?").run(licenseId);
  assert.equal((await f.issue()).status,'blocked');assert.equal(f.db.raw.prepare('SELECT count(*) n FROM test_signed_license_artifacts').get().n,0);
 }
});

test('payment binding is recorded atomically with the dispatch claim and proof',async()=>{
 const f=fixture();f.db.raw.exec('DROP TABLE test_confirmation_payment_bindings');
 assert.equal((await f.send()).status,'not_ready');
 assert.equal(f.db.raw.prepare('SELECT count(*) n FROM test_confirmation_dispatch_proofs').get().n,0);
 assert.equal(f.db.raw.prepare('SELECT count(*) n FROM mail_dispatch_attempts').get().n,0);
});

test('a lost database reply after committing an artifact never emits a second artifact',async()=>{
 const f=fixture();await f.send();f.delivery();const original=f.db.prepare.bind(f.db);
 const lost={prepare(sql){const p=original(sql);if(!sql.startsWith('INSERT INTO test_signed_license_artifacts'))return p;return {bind(...args){const stmt=p.bind(...args);return {...stmt,async first(){await stmt.first();throw Error('synthetic lost response');}};}};}};
 assert.equal((await f.issue({db:lost})).status,'blocked');
 assert.equal(f.db.raw.prepare('SELECT count(*) n FROM test_signed_license_artifacts').get().n,1);
 assert.equal((await f.issue()).status,'blocked');
});

test('binding rows are immutable and cannot be replaced by another session',async()=>{
 const f=fixture();await f.send();
 assert.throws(()=>f.db.raw.exec("UPDATE test_confirmation_payment_bindings SET payment_intent='pi_other'"));
 assert.throws(()=>f.db.raw.exec('DELETE FROM test_confirmation_payment_bindings'));
 const row=f.db.raw.prepare('SELECT * FROM test_confirmation_payment_bindings').get();
 assert.throws(()=>f.db.raw.prepare('INSERT OR REPLACE INTO test_confirmation_payment_bindings VALUES(?,?,?,?)').run(row.order_id,'cs_test_other',row.payment_intent,row.created_at));
});

test('foreign provider event is rejected; only the validated matching test event can open the test gate',async()=>{
 const f=fixture();await f.send();
 Object.assign(f.env,{MAIL_EVENT_TEST_ENABLED:'true',MAIL_EVENT_ACCOUNT:'test-account',MAIL_EVENT_ZONE:'test-zone',MAIL_EVENT_SUBSCRIPTION:'test-subscription',MAIL_EVENT_DOMAIN:'example.test'});
 const event={type:'cf.email.sending.message.delivered',source:{type:'email.sending',zoneId:'test-zone',domain:'example.test'},payload:{eventId:'synthetic-event',messageId:'test-provider-id',sender:'sender@example.test',recipient:'other@example.test',terminal:true,delivery:{status:'delivered'}},metadata:{accountId:'test-account',eventSubscriptionId:'test-subscription',eventSchemaVersion:1,eventTimestamp:'2026-11-15T11:00:00.000Z'}};
 await assert.rejects(recordMailEvent(f.db,f.env,event));assert.equal((await f.issue()).status,'blocked');
 event.payload.recipient='owner@example.test';assert.equal((await recordMailEvent(f.db,f.env,event)).status,'recorded');
 assert.equal((await f.issue()).status,'test_artifact_issued');
});
