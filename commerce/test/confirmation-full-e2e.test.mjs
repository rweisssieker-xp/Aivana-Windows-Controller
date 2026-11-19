import {test} from 'node:test';
import assert from 'node:assert/strict';
import {DatabaseSync} from 'node:sqlite';
import {readFileSync} from 'node:fs';
import {createHash,createHmac} from 'node:crypto';
import worker from '../worker.mjs';
import {readTestSupplementDocuments} from '../confirmation-package-test.mjs';

const digest=x=>createHash('sha256').update(JSON.stringify(x)).digest('base64url');
function database(){
 const raw=new DatabaseSync(':memory:');
 for(const file of ['../schema.sql','../migrations/0010_withdrawal.sql','../migrations/0013_test_mail_attempts.sql','../migrations/0016_test_confirmation_supplements.sql'])raw.exec(readFileSync(new URL(file,import.meta.url),'utf8'));
 return {raw,prepare(sql){return {bind(...args){const s=raw.prepare(sql);return {_run(){return s.run(...args)},async first(){return s.get(...args)||null},async all(){return {results:s.all(...args)}},async run(){return s.run(...args)}};}};},async batch(statements){raw.exec('BEGIN');try{const out=statements.map(s=>s._run());raw.exec('COMMIT');return out;}catch(e){raw.exec('ROLLBACK');throw e;}}};
}

test('synthetic full checkout, payment webhook and test confirmation retain all historical documents and verified price',async()=>{
 const db=database(),messages=[];
 const pair=await crypto.subtle.generateKey({name:'RSASSA-PKCS1-v1_5',modulusLength:3072,publicExponent:new Uint8Array([1,0,1]),hash:'SHA-256'},true,['sign','verify']);
 const pem='-----BEGIN PRIVATE KEY-----\n'+Buffer.from(await crypto.subtle.exportKey('pkcs8',pair.privateKey)).toString('base64')+'\n-----END PRIVATE KEY-----';
 const e={DB:db,FULL_CONFIRMATION_TEST_ENABLED:'true',MAIL_DELIVERY_TEST_ENABLED:'true',STRIPE_LIVE:'false',SALES_ENABLED:'true',LEGAL_APPROVED:'true',PUBLIC_ORIGIN:'https://buyer.example.test',LEGAL_TERMS_URL:'https://buyer.example.test/terms',PRIVACY_URL:'https://buyer.example.test/privacy',SELLER_IMPRINT_URL:'https://buyer.example.test/imprint',STRIPE_SECRET_KEY:'sk_test_synthetic',STRIPE_WEBHOOK_SECRET:'whsec_test_synthetic',STRIPE_PRICE_ID:'price_test',STRIPE_PRODUCT_ID:'prod_test',LICENSE_PRIVATE_KEY_PEM:pem,LICENSE_KEY_ID:'test',TEST_MAIL_RECIPIENT:'owner@example.test',MAIL_SENDER:'sender@example.test',EMAIL:{send:async message=>{messages.push(message);return {messageId:'test-provider-id'}}}};
 for(const lang of ['de','en']){
  const suffix=lang.toUpperCase();
  for(const [key,version,text] of [['CONTRACT_VERSION_','terms-v1',`Synthetic ${lang} terms`],['CONSENT_EARLY_VERSION_','early-v1',`Synthetic ${lang} early`],['CONSENT_ACK_VERSION_','ack-v1',`Synthetic ${lang} ack`],['WITHDRAWAL_NOTICE_VERSION_','notice-v1',`Synthetic ${lang} notice`],['WITHDRAWAL_FORM_VERSION_','form-v1',`Synthetic ${lang} form`]]){
   e[key+suffix]=version;db.raw.prepare('INSERT INTO contract_documents VALUES(?,?,?)').run(lang,version,text);
  }
 }
 const shown=await readTestSupplementDocuments(db,e,'de');
 const term={language:'de',version:'terms-v1',text:'Synthetic de terms'};
 const early={kind:'early_start',language:'de',version:'early-v1',text:'Synthetic de early'};
 const ack={kind:'withdrawal_ack',language:'de',version:'ack-v1',text:'Synthetic de ack'};
 const post=(path,body)=>worker.fetch(new Request(e.PUBLIC_ORIGIN+path,{method:'POST',headers:{Origin:e.PUBLIC_ORIGIN},body:JSON.stringify(body)}),e);
 const rawPrepare=await post('/api/checkout/prepare',{acceptTerms:true,acceptEarlyStart:true,acknowledgeWithdrawal:true,lang:'de',contractDigest:digest(term),earlyStartDigest:digest(early),withdrawalAckDigest:digest(ack),noticeDigest:shown[0].digest,formDigest:shown[1].digest});
 assert.equal(rawPrepare.status,200);const prepared=await rawPrepare.json();
 const originalFetch=globalThis.fetch;let session,paid=false;
 try {
  globalThis.fetch=async(url,options)=>{
   if(String(url).includes('/prices/'))return Response.json({livemode:false,active:true,type:'one_time',currency:'eur',unit_amount:2900,tax_behavior:'inclusive',product:'prod_test'});
   if(options?.method==='POST')session={id:'cs_test_synthetic_e2e',mode:'payment',livemode:false,status:'open',payment_status:'unpaid',currency:'eur',amount_total:2900,url:'https://checkout.stripe.com/c/pay/test',payment_intent:'pi_test_synthetic_e2e',metadata:{orderId:new URLSearchParams(options.body).get('metadata[orderId]')},line_items:{has_more:false,data:[{quantity:1,price:{id:'price_test',product:'prod_test'}}]},customer_details:{email:'owner@example.test'}};
   if(!session)throw Error('unexpected synthetic provider call');
   return Response.json({...session,payment_status:paid?'paid':'unpaid'});
  };
  const payment=await post('/api/checkout',{acceptTerms:true,recoveryCode:prepared.recoveryCode,checkoutToken:prepared.checkoutToken});assert.equal(payment.status,200);
  paid=true;const event=JSON.stringify({id:'evt_test_synthetic',type:'checkout.session.completed',livemode:false,data:{object:{id:session.id}}});
  const ts=Math.floor(Date.now()/1000),signature=createHmac('sha256',e.STRIPE_WEBHOOK_SECRET).update(`${ts}.${event}`).digest('hex');
  const wh=await worker.fetch(new Request(e.PUBLIC_ORIGIN+'/api/stripe/webhook',{method:'POST',headers:{'Stripe-Signature':`t=${ts},v1=${signature}`},body:event}),e);assert.equal(wh.status,200);
  await worker.scheduled({},e,{});await worker.scheduled({},e,{});
  assert.equal(messages.length,1);assert.equal(messages[0].to,'owner@example.test');
  for(const text of ['Synthetic de terms','Synthetic de early','Synthetic de ack','Synthetic de notice','Synthetic de form','29,00 EUR','bezahlt simuliert'])assert.ok(messages[0].text.includes(text),text);
  for(const secret of [prepared.recoveryCode,e.STRIPE_SECRET_KEY,e.STRIPE_WEBHOOK_SECRET,pem,session.payment_intent,session.id])assert.ok(!messages[0].text.includes(secret));
  assert.equal(db.raw.prepare('SELECT COUNT(*) n FROM confirmation_obligations').get().n,1);
  assert.equal(db.raw.prepare("SELECT state FROM mail_dispatch_attempts WHERE kind='purchase'").get().state,'accepted');
 } finally {globalThis.fetch=originalFetch;}
});
