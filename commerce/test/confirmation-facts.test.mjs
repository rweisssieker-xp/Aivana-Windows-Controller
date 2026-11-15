import {test} from 'node:test';
import assert from 'node:assert/strict';
import {formatTestOrderFacts} from '../confirmation-facts-test.mjs';

const env={FULL_CONFIRMATION_TEST_ENABLED:'true',MAIL_DELIVERY_TEST_ENABLED:'true',STRIPE_LIVE:'false',STRIPE_PRICE_ID:'price_test',STRIPE_PRODUCT_ID:'prod_test'};
function input(language='de'){
 const orderId='synthetic-order',sessionId='cs_test_checkout',paymentIntent='pi_test_paid';
 const session={id:sessionId,payment_intent:paymentIntent,metadata:{orderId},livemode:false,mode:'payment',payment_status:'paid',currency:'eur',amount_total:2900,line_items:{has_more:false,data:[{quantity:1,price:{id:'price_test',product:'prod_test'}}]},url:'secret-url',customer_details:{email:'private@example.test'}};
 return {env,orderId,sessionId,paymentIntent,session,language,recoveryCode:'secret-code'};
}
test('test facts reproduce verified paid item and price in both supported languages without customer secrets',()=>{
 for(const language of ['de','en']){
  const result=formatTestOrderFacts(input(language));
  assert.match(result,/MAKRIVO/);assert.match(result,language==='de'?/29,00 EUR/:/29\.00 EUR/);assert.match(result,language==='de'?/TEST.*simuliert/:/TEST.*simulated/i);
  for(const secret of ['private@example.test','secret-url','secret-code','pi_test_paid','cs_test_checkout'])assert.ok(!result.includes(secret));
 }
});
test('unpaid, wrong order, wrong product or partial line items cannot become test confirmation facts',()=>{
 for(const change of [
  x=>{x.session.payment_status='unpaid'},x=>{x.session.metadata.orderId='other'},x=>{x.session.id='cs_other'},x=>{x.session.payment_intent='pi_other'},
  x=>{x.session.livemode=true},x=>{x.session.currency='usd'},x=>{x.session.amount_total=2800},x=>{x.session.mode='subscription'},
  x=>{x.session.line_items.has_more=true},x=>{delete x.session.line_items.has_more},x=>{x.session.line_items.data[0].quantity=2},
  x=>{x.session.line_items.data[0].price.id='price_other'},x=>{x.session.line_items.data[0].price.product='prod_other'},
  x=>{x.env={...x.env,STRIPE_LIVE:'true'}},x=>{x.env={...x.env,MAIL_DELIVERY_TEST_ENABLED:'false'}},x=>{x.env={...x.env,FULL_CONFIRMATION_TEST_ENABLED:'false'}},
  x=>{x.language='fr'},x=>{x.orderId='bad\norder'},
 ]){const x=input();change(x);assert.throws(()=>formatTestOrderFacts(x));}
});
test('expanded Stripe product is checked by id without copying provider metadata',()=>{
 const x=input();x.session.line_items.data[0].price.product={id:'prod_test',description:'private provider text'};
 const result=formatTestOrderFacts(x);assert.ok(!result.includes('private provider text'));assert.match(result,/MAKRIVO/);
});
