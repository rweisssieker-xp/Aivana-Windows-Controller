// Formats only independently rechecked, synthetic Stripe test-session facts.
// It is not an invoice, payment receipt, seller verification, or legal review.
export function formatTestOrderFacts({env,orderId,sessionId,paymentIntent,session,language}={}){
  const fail=()=>{throw new Error('Invalid test order facts');};
  if(env?.FULL_CONFIRMATION_TEST_ENABLED!=='true'||env?.MAIL_DELIVERY_TEST_ENABLED!=='true'||env?.STRIPE_LIVE!=='false'||!['de','en'].includes(language)||typeof orderId!=='string'||!/^[A-Za-z0-9_-]{1,128}$/.test(orderId)||typeof sessionId!=='string'||!sessionId.startsWith('cs_test_')||typeof paymentIntent!=='string'||!paymentIntent.startsWith('pi_')||!env.STRIPE_PRICE_ID||!env.STRIPE_PRODUCT_ID)fail();
  const items=session?.line_items?.data;
  const product=typeof items?.[0]?.price?.product==='string'?items[0].price.product:items?.[0]?.price?.product?.id;
  if(session?.id!==sessionId||session.payment_intent!==paymentIntent||session.metadata?.orderId!==orderId||session.livemode!==false||session.mode!=='payment'||session.payment_status!=='paid'||session.currency!=='eur'||session.amount_total!==2900||!Array.isArray(items)||items.length!==1||session.line_items.has_more!==false||items[0]?.quantity!==1||items[0]?.price?.id!==env.STRIPE_PRICE_ID||product!==env.STRIPE_PRODUCT_ID)fail();
  return language==='de'
    ? `TEST — simulierte Bestellung\nArtikel: MAKRIVO\nMenge: 1\nGesamtbetrag: 29,00 EUR\nZahlungsstatus: TEST – bezahlt simuliert`
    : `TEST — simulated order\nItem: MAKRIVO\nQuantity: 1\nTotal: 29.00 EUR\nPayment status: TEST – paid simulated`;
}
