import {test} from 'node:test';
import assert from 'node:assert/strict';
import {DatabaseSync} from 'node:sqlite';
import {readFileSync} from 'node:fs';
import {createHash} from 'node:crypto';
import {sendCompleteConfirmationTest,recordTestIssuance} from '../test-confirmation-gate.mjs';
import {sendPurchaseJob} from '../delivery-dispatch.mjs';

const digest=x=>createHash('sha256').update(JSON.stringify(x)).digest('base64url');
const id='test-order',device='a'.repeat(64),at=1700000000000;
function fixture(){
 const raw=new DatabaseSync(':memory:');
 for(const file of ['../schema.sql','../migrations/0013_test_mail_attempts.sql','../migrations/0014_test_mail_events.sql','../migrations/0016_test_confirmation_supplements.sql','../migrations/0019_test_confirmation_dispatch_proof.sql','../migrations/0021_test_signed_license_artifacts.sql'])raw.exec(readFileSync(new URL(file,import.meta.url),'utf8'));
 const db={raw,async batch(stmts){raw.exec('BEGIN IMMEDIATE');try{const out=stmts.map(s=>s._run());raw.exec('COMMIT');return out;}catch(e){raw.exec('ROLLBACK');throw e;}},prepare(sql){return {bind(...p){const s=raw.prepare(sql);return {_run:()=>s.run(...p),first:async()=>s.get(...p)||null,all:async()=>({results:s.all(...p)}),run:async()=>s.run(...p)};}};}};
 raw.prepare('INSERT INTO orders VALUES(?,?,?,?)').run(id,'claim-hash',at,'cs_test_gate');
 raw.prepare('INSERT INTO checkout_attempts VALUES(?,?)').run(id,'de');
 raw.prepare('INSERT INTO confirmation_obligations VALUES(?,?)').run(id,at);
 raw.prepare('INSERT INTO confirmation_jobs VALUES(?,?)').run(id,at);
 raw.prepare('INSERT INTO entitlements(id,order_id,session_id,payment_intent) VALUES(?,?,?,?)').run('test-license',id,'cs_test_gate','pi_test');
 raw.prepare('INSERT INTO devices VALUES(?,?,1)').run('test-license',device);
 const terms={language:'de',version:'terms-v1',text:'Synthetic terms'};
 raw.prepare('INSERT INTO contract_acceptances VALUES(?,?,?,?,?,?)').run(id,'de',terms.version,terms.text,digest(terms),at);
 for(const [kind,version,text] of [['early_start','early-v1','Synthetic consent'],['withdrawal_ack','ack-v1','Synthetic ack']]){
  const d={kind,language:'de',version,text};raw.prepare('INSERT INTO contract_declarations VALUES(?,?,?,?,?,?,?)').run(id,kind,'de',version,text,digest(d),at);
 }
 for(const [kind,version,text] of [['withdrawal_notice','notice-v1','Synthetic notice'],['withdrawal_form','form-v1','Synthetic form']]){
  const d={kind,language:'de',version,text};raw.prepare('INSERT INTO test_confirmation_supplements VALUES(?,?,?,?,?,?,?)').run(id,kind,'de',version,text,digest(d),at);
 }
 const env={STRIPE_LIVE:'false',FULL_CONFIRMATION_TEST_ENABLED:'true',MAIL_DELIVERY_TEST_ENABLED:'true',LICENSE_DELIVERY_PROOF_TEST_ENABLED:'true',TEST_LICENSE_SIGNATURE_ENABLED:'true',TEST_MAIL_RECIPIENT:'owner@example.test',MAIL_SENDER:'sender@example.test',STRIPE_PRICE_ID:'price_test',STRIPE_PRODUCT_ID:'prod_test'};
 const sent=[],binding={send:async m=>{sent.push(m);return {messageId:'provider-id'};}};
 const session={id:'cs_test_gate',payment_intent:'pi_test',metadata:{orderId:id},customer_details:{email:'owner@example.test'},livemode:false,mode:'payment',payment_status:'paid',currency:'eur',amount_total:2900,line_items:{data:[{quantity:1,price:{id:'price_test',product:'prod_test'}}],has_more:false}};
 const send=()=>sendCompleteConfirmationTest({db,env,binding,jobId:id,loadSession:async()=>session,validateSession:()=>true});
 const issue=()=>recordTestIssuance({db,env,licenseId:'test-license',deviceId:device});
 const event=(kind,ev=kind)=>raw.prepare('INSERT INTO mail_delivery_events VALUES(?,?,?,?)').run(ev,'provider-id',kind,Date.now());
 return {db,env,sent,send,issue,event,binding,session};
}

test('provider acceptance alone never allows even a test issuance; a matched delivery does',async()=>{
 const f=fixture();assert.equal((await f.send()).status,'accepted_by_provider');
 assert.equal((await f.issue()).status,'blocked');
 f.event('delivered');assert.equal((await f.issue()).status,'test_only_gate_passed');
 for(const text of ['Synthetic terms','Synthetic consent','Synthetic ack','Synthetic notice','Synthetic form'])assert.ok(f.sent[0].text.includes(text));
 assert.equal(f.db.raw.prepare('SELECT count(*) n FROM confirmation_obligations').get().n,1);
});

test('a later bounce or refund blocks a new issuance even after a prior delivery',async()=>{
 const f=fixture();await f.send();f.event('delivered');assert.equal((await f.issue()).status,'test_only_gate_passed');
 f.event('bounced');assert.equal((await f.issue()).status,'blocked');
 const g=fixture();await g.send();g.event('delivered');g.db.raw.prepare("INSERT INTO revoked_payments VALUES('pi_test')").run();assert.equal((await g.issue()).status,'blocked');
});

test('legacy partial dispatch, missing full evidence, or foreign delivery cannot create proof',async()=>{
 const f=fixture();f.db.raw.prepare("INSERT INTO mail_dispatch_attempts VALUES('purchase',?,'accepted',?,?,?)").run(id,at,at,'provider-id');f.event('delivered');
 assert.equal((await f.issue()).status,'blocked');assert.notEqual((await f.send()).status,'accepted_by_provider');assert.equal(f.sent.length,0);
 const g=fixture();g.db.raw.exec('DROP TRIGGER test_confirmation_supplements_no_delete');g.db.raw.prepare("DELETE FROM test_confirmation_supplements WHERE kind='withdrawal_form'").run();assert.equal((await g.send()).status,'not_ready');assert.equal(g.sent.length,0);
});

test('uncertain sender outcome is not retried and live mode never sends or releases',async()=>{
 const f=fixture();let calls=0;const broken={send:async()=>{calls++;throw Error('private provider error');}};
 const args={db:f.db,env:f.env,binding:broken,jobId:id,loadSession:async()=>f.session,validateSession:()=>true};
 assert.equal((await sendCompleteConfirmationTest(args)).status,'outcome_unknown');assert.notEqual((await sendCompleteConfirmationTest(args)).status,'accepted_by_provider');assert.equal(calls,1);
 f.env.STRIPE_LIVE='true';assert.equal((await f.issue()).status,'blocked');assert.equal((await f.send()).status,'not_ready');
});

test('the bound proof and recorded simulated decisions cannot be rewritten',async()=>{
 const f=fixture();await f.send();f.event('delivered');await f.issue();
 assert.throws(()=>f.db.raw.exec("UPDATE test_confirmation_dispatch_proofs SET text_digest='forged'"));
 assert.throws(()=>f.db.raw.exec('DELETE FROM test_confirmation_dispatch_proofs'));
 assert.throws(()=>f.db.raw.exec("UPDATE test_gate_decisions SET decision='block'"));
 assert.deepEqual(f.db.raw.prepare('SELECT decision FROM test_gate_decisions').all().map(x=>x.decision),['allow']);
});

test('a refund committed just before the atomic simulated decision is observed',async()=>{
 const f=fixture();await f.send();f.event('delivered');const original=f.db.prepare.bind(f.db);
 const interleaved={prepare(sql){if(sql.startsWith('INSERT INTO test_gate_decisions'))f.db.raw.prepare("INSERT INTO revoked_payments VALUES('pi_test')").run();return original(sql);}};
 assert.equal((await recordTestIssuance({db:interleaved,env:f.env,licenseId:'test-license',deviceId:device})).status,'blocked');
 assert.equal(f.db.raw.prepare('SELECT decision FROM test_gate_decisions').get().decision,'block');
});

test('missing proof storage and ambiguous message identifiers fail closed without sending or issuing',async()=>{
 const f=fixture();f.db.raw.exec('DROP TABLE test_confirmation_dispatch_proofs');assert.equal((await f.send()).status,'not_ready');assert.equal(f.sent.length,0);assert.equal(f.db.raw.prepare('SELECT count(*) n FROM mail_dispatch_attempts').get().n,0);
 const g=fixture();await g.send();g.event('delivered');g.db.raw.prepare("INSERT INTO mail_dispatch_attempts VALUES('withdrawal','other','accepted',?,?,?)").run(at,at,'provider-id');assert.equal((await g.issue()).status,'blocked');
});

test('proof path binds the exact same full test message as the regular full-confirmation dispatcher',async()=>{
 const normal=fixture(),proof=fixture();
 const dispatched=await sendPurchaseJob({db:normal.db,env:normal.env,binding:normal.binding,jobId:id,loadSession:async()=>normal.session,validateSession:()=>true});
 assert.equal(dispatched.status,'accepted_by_provider');assert.equal((await proof.send()).status,'accepted_by_provider');
 assert.equal(proof.sent[0].text,normal.sent[0].text);assert.equal(proof.sent[0].subject,normal.sent[0].subject);
 assert.match(proof.sent[0].text,/TEST — simulierte Bestellung/);
});

test('altered bound facts and incomplete payment facts do not pass the test gate',async()=>{
 const f=fixture();await f.send();f.event('delivered');
 f.db.raw.exec('DROP TRIGGER test_confirmation_dispatch_proofs_no_update');
 f.db.raw.prepare("UPDATE test_confirmation_dispatch_proofs SET facts_text='tampered'").run();
 assert.equal((await f.issue()).status,'blocked');
 const g=fixture();g.session.line_items.has_more=true;
 assert.equal((await g.send()).status,'not_ready');assert.equal(g.sent.length,0);
 assert.equal(g.db.raw.prepare('SELECT count(*) n FROM mail_dispatch_attempts').get().n,0);
});

test('a simulated gate decision cannot reuse proof after the payment intent or session changes',async()=>{
 for(const field of ['payment_intent','session_id']){
  const f=fixture();await f.send();f.event('delivered');
  if(field==='payment_intent'){f.db.raw.prepare("INSERT INTO revoked_payments VALUES('pi_test')").run();f.db.raw.prepare("UPDATE entitlements SET payment_intent='pi_replacement'").run();}
  else f.db.raw.prepare("UPDATE entitlements SET session_id='cs_test_replacement'").run();
  assert.equal((await f.issue()).status,'blocked');
 }
});

test('legacy 0019 test dispatch stays available but its unbound evidence never passes the gate',async()=>{
 const f=fixture();delete f.env.TEST_LICENSE_SIGNATURE_ENABLED;f.db.raw.exec('DROP TABLE test_confirmation_payment_bindings');
 assert.equal((await f.send()).status,'accepted_by_provider');f.event('delivered');
 assert.equal((await f.issue()).status,'blocked');assert.equal(f.sent.length,1);
});
