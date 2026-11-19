import {test} from 'node:test';
import assert from 'node:assert/strict';
import {DatabaseSync} from 'node:sqlite';
import {readFileSync} from 'node:fs';
import {createHash} from 'node:crypto';
import {sendPurchaseJob} from '../delivery-dispatch.mjs';
import {validateSession} from '../worker.mjs';

const digest=x=>createHash('sha256').update(JSON.stringify(x)).digest('base64url');
function fixture(withSupplements){
 const raw=new DatabaseSync(':memory:');
 for(const file of ['../schema.sql','../migrations/0013_test_mail_attempts.sql','../migrations/0016_test_confirmation_supplements.sql'])raw.exec(readFileSync(new URL(file,import.meta.url),'utf8'));
 const db={raw,prepare(sql){return {bind(...args){const s=raw.prepare(sql);return {async first(){return s.get(...args)||null},async all(){return {results:s.all(...args)}}};}};}};
 const orderId='synthetic-order',at=1700000000000,language='de';
 raw.prepare('INSERT INTO orders VALUES(?,?,?,?)').run(orderId,'synthetic-claim',at,'cs_test_checkout');
 raw.prepare('INSERT INTO checkout_attempts VALUES(?,?)').run(orderId,language);
 raw.prepare('INSERT INTO confirmation_obligations VALUES(?,?)').run(orderId,at);
 raw.prepare('INSERT INTO confirmation_jobs VALUES(?,?)').run(orderId,at);
 raw.prepare('INSERT INTO entitlements(id,order_id,session_id,payment_intent) VALUES(?,?,?,?)').run('lic',orderId,'cs_test_checkout','pi_test');
 const terms={language,version:'terms-v1',text:'Synthetic terms'};
 raw.prepare('INSERT INTO contract_acceptances VALUES(?,?,?,?,?,?)').run(orderId,language,terms.version,terms.text,digest(terms),at);
 for(const [kind,version,text] of [['early_start','early-v1','Synthetic consent'],['withdrawal_ack','ack-v1','Synthetic acknowledgment']]){
  const d={kind,language,version,text};raw.prepare('INSERT INTO contract_declarations VALUES(?,?,?,?,?,?,?)').run(orderId,kind,language,version,text,digest(d),at);
 }
 if(withSupplements)for(const [kind,version,text] of [['withdrawal_notice','notice-v1','Synthetic notice'],['withdrawal_form','form-v1','Synthetic form']]){
  const d={kind,language,version,text};raw.prepare('INSERT INTO test_confirmation_supplements VALUES(?,?,?,?,?,?,?)').run(orderId,kind,language,version,text,digest(d),at);
 }
 const env={STRIPE_LIVE:'false',STRIPE_PRICE_ID:'price_test',STRIPE_PRODUCT_ID:'prod_test',MAIL_DELIVERY_TEST_ENABLED:'true',FULL_CONFIRMATION_TEST_ENABLED:'true',TEST_MAIL_RECIPIENT:'owner@example.test',MAIL_SENDER:'sender@example.test'};
 const session={id:'cs_test_checkout',payment_intent:'pi_test',metadata:{orderId},livemode:false,mode:'payment',payment_status:'paid',currency:'eur',amount_total:2900,line_items:{has_more:false,data:[{quantity:1,price:{id:'price_test',product:'prod_test'}}]},customer_details:{email:env.TEST_MAIL_RECIPIENT}};
 const messages=[];
 return {db,messages,session,args:{db,env,binding:{send:async message=>{messages.push(message);return {messageId:'provider-test'};}},jobId:orderId,loadSession:async()=>session,validateSession}};
}

test('full test confirmation sends original saved terms and both saved synthetic supplemental texts',async()=>{
 const f=fixture(true);assert.equal((await sendPurchaseJob(f.args)).status,'accepted_by_provider');
 for(const text of ['Synthetic terms','Synthetic consent','Synthetic acknowledgment','Synthetic notice','Synthetic form','29,00 EUR','MAKRIVO','bezahlt simuliert'])assert.ok(f.messages[0].text.includes(text));
 assert.equal(f.db.raw.prepare('SELECT COUNT(*) n FROM confirmation_obligations').get().n,1);
});

test('partial provider line items block full test confirmation even when an external validator is too permissive',async()=>{
 const f=fixture(true);delete f.session.line_items.has_more;f.args.validateSession=()=>true;
 assert.equal((await sendPurchaseJob(f.args)).status,'not_ready');assert.equal(f.messages.length,0);
 assert.equal(f.db.raw.prepare('SELECT COUNT(*) n FROM mail_dispatch_attempts').get().n,0);
});

test('full test confirmation fails closed without its original snapshots before claiming or sending',async()=>{
 const f=fixture(false);assert.equal((await sendPurchaseJob(f.args)).status,'not_ready');
 assert.equal(f.messages.length,0);assert.equal(f.db.raw.prepare('SELECT COUNT(*) n FROM mail_dispatch_attempts').get().n,0);
});

test('changed supplemental text is not delivered or claimed even if database protections were bypassed',async()=>{
 const f=fixture(true);
 f.db.raw.exec('DROP TRIGGER test_confirmation_supplements_no_update');
 f.db.raw.prepare("UPDATE test_confirmation_supplements SET text='tampered' WHERE kind='withdrawal_form'").run();
 assert.equal((await sendPurchaseJob(f.args)).status,'not_ready');assert.equal(f.messages.length,0);
 assert.equal(f.db.raw.prepare('SELECT COUNT(*) n FROM mail_dispatch_attempts').get().n,0);
});

test('missing supplemental storage does not silently send a partial full-test package',async()=>{
 const f=fixture(true);f.db.raw.exec('DROP TABLE test_confirmation_supplements');
 assert.equal((await sendPurchaseJob(f.args)).status,'not_ready');assert.equal(f.messages.length,0);
 assert.equal(f.db.raw.prepare('SELECT COUNT(*) n FROM mail_dispatch_attempts').get().n,0);
});
