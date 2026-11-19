// Isolated synthetic prototype: no license signature, real issuance or route.
import {buildTestConfirmationPackage} from './confirmation-package-test.mjs';
import {sendPreparedEmail} from './cloudflare-mail.mjs';
import {formatTestOrderFacts} from './confirmation-facts-test.mjs';
import {renderTestConfirmation} from './confirmation-render-test.mjs';

const enabled=e=>e?.STRIPE_LIVE==='false'&&e?.FULL_CONFIRMATION_TEST_ENABLED==='true'&&e?.MAIL_DELIVERY_TEST_ENABLED==='true'&&e?.LICENSE_DELIVERY_PROOF_TEST_ENABLED==='true'&&typeof e?.TEST_MAIL_RECIPIENT==='string'&&typeof e?.MAIL_SENDER==='string';
const hash=async text=>{const b=new Uint8Array(await crypto.subtle.digest('SHA-256',new TextEncoder().encode(text)));return btoa(String.fromCharCode(...b)).replaceAll('+','-').replaceAll('/','_').replaceAll('=','');};

async function rendered(db,orderId,language,facts){
  const acceptance=await db.prepare('SELECT order_id,language,version,text,digest,accepted_at FROM contract_acceptances WHERE order_id=?').bind(orderId).first();
  const declarations=(await db.prepare('SELECT order_id,kind,language,version,text,digest,accepted_at FROM contract_declarations WHERE order_id=?').bind(orderId).all()).results;
  const supplements=(await db.prepare('SELECT order_id,kind,language,version,text,digest,captured_at FROM test_confirmation_supplements WHERE order_id=?').bind(orderId).all()).results;
  const p=await buildTestConfirmationPackage({orderId,language,acceptance,declarations,supplements});
  return renderTestConfirmation({orderId,evidence:p,facts});
}

export async function sendCompleteConfirmationTest({db,env,binding,jobId,loadSession,validateSession}){
  if(!enabled(env)||typeof binding?.send!=='function'||typeof loadSession!=='function'||typeof validateSession!=='function')return {status:'not_ready'};
  let text,order;
  try{
    order=await db.prepare('SELECT o.id,o.session_id,a.return_lang,e.payment_intent FROM orders o JOIN checkout_attempts a ON a.order_id=o.id JOIN entitlements e ON e.order_id=o.id AND e.session_id=o.session_id JOIN confirmation_jobs j ON j.order_id=o.id WHERE o.id=?').bind(jobId).first();
    if(!order||!order.payment_intent)return {status:'not_ready'};
    const session=await loadSession(order.session_id);
    if(!session||session.id!==order.session_id||session.payment_intent!==order.payment_intent||session.metadata?.orderId!==order.id||!validateSession(session,env)||session.customer_details?.email!==env.TEST_MAIL_RECIPIENT)return {status:'not_ready'};
    const facts=formatTestOrderFacts({env,session,orderId:order.id,sessionId:order.session_id,paymentIntent:order.payment_intent,language:order.return_lang});
    text=await rendered(db,order.id,order.return_lang,facts);
    const now=Date.now(),textDigest=await hash(text);
    // Both records commit together. Duplicate/competing claims fail without
    // sending, including a pre-existing legacy partial-dispatch attempt.
    await db.batch([
      db.prepare("INSERT INTO mail_dispatch_attempts(kind,job_id,state,started_at) VALUES('purchase',?,'processing',?)").bind(order.id,now),
      db.prepare('INSERT INTO test_confirmation_dispatch_proofs(order_id,text_digest,facts_text,created_at) VALUES(?,?,?,?)').bind(order.id,textDigest,facts,now),
      ...(env.TEST_LICENSE_SIGNATURE_ENABLED==='true'?[db.prepare('INSERT INTO test_confirmation_payment_bindings(order_id,session_id,payment_intent,created_at) VALUES(?,?,?,?)').bind(order.id,order.session_id,order.payment_intent,now)]:[])
    ]);
  }catch{return {status:'not_ready'};}
  let result;try{result=await sendPreparedEmail(binding,{sender:env.MAIL_SENDER,recipient:env.TEST_MAIL_RECIPIENT,subject:'MAKRIVO - TEST Vertragsinformationen',text});}catch{result={status:'outcome_unknown'};}
  const accepted=result.status==='accepted_by_provider';
  const updated=await db.prepare("UPDATE mail_dispatch_attempts SET state=?,finished_at=?,message_id=? WHERE kind='purchase' AND job_id=? AND state='processing' RETURNING job_id").bind(accepted?'accepted':'unknown',Date.now(),accepted?result.messageId:null,order.id).first();
  if(!updated)throw new Error('Test outcome not recorded');
  return {status:accepted?'accepted_by_provider':'outcome_unknown'};
}

export async function recordTestIssuance({db,env,licenseId,deviceId}){
  if(!enabled(env)||typeof licenseId!=='string'||!licenseId||!/^[0-9a-f]{64}$/.test(deviceId||''))return {status:'blocked'};
  try{
    const row=await db.prepare('SELECT e.order_id,a.return_lang FROM entitlements e JOIN checkout_attempts a ON a.order_id=e.order_id WHERE e.id=?').bind(licenseId).first();
    if(!row)return {status:'blocked'};
    const proof=await db.prepare('SELECT facts_text FROM test_confirmation_dispatch_proofs WHERE order_id=?').bind(row.order_id).first();
    if(typeof proof?.facts_text!=='string'||!proof.facts_text)return {status:'blocked'};
    const textDigest=await hash(await rendered(db,row.order_id,row.return_lang,proof.facts_text));
    // One conditional INSERT records the simulated decision at its database
    // ordering point. It never updates an actual device or returns a license.
    const q=`INSERT INTO test_gate_decisions(license_id,device_id,decision,created_at)
    SELECT ?1,?2,CASE WHEN EXISTS(
      SELECT 1 FROM entitlements e
      JOIN devices d ON d.license_id=e.id AND d.device_id=?2 AND d.active=1
      JOIN orders ordered ON ordered.id=e.order_id AND ordered.session_id=e.session_id
      JOIN confirmation_obligations o ON o.order_id=e.order_id
      JOIN test_confirmation_dispatch_proofs p ON p.order_id=o.order_id AND p.text_digest=?3
      JOIN test_confirmation_payment_bindings binding ON binding.order_id=e.order_id AND binding.session_id=e.session_id AND binding.payment_intent=e.payment_intent AND binding.created_at=p.created_at
      JOIN mail_dispatch_attempts a ON a.kind='purchase' AND a.job_id=o.order_id AND a.state='accepted' AND a.started_at=p.created_at
      WHERE e.id=?1 AND e.revoked=0 AND e.payment_intent IS NOT NULL
        AND NOT EXISTS(SELECT 1 FROM revoked_payments r WHERE r.payment_intent=e.payment_intent)
        AND length(a.message_id)>0
        AND (SELECT COUNT(*) FROM mail_dispatch_attempts x WHERE x.state='accepted' AND x.message_id=a.message_id)=1
        AND EXISTS(SELECT 1 FROM mail_delivery_events v WHERE v.message_id=a.message_id AND v.kind='delivered')
        AND NOT EXISTS(SELECT 1 FROM mail_delivery_events v WHERE v.message_id=a.message_id AND v.kind IN ('bounced','failed','rejected','complained'))
    ) THEN 'allow' ELSE 'block' END,?4 RETURNING decision`;
    const decision=await db.prepare(q).bind(licenseId,deviceId,textDigest,Date.now()).first();
    return {status:decision?.decision==='allow'?'test_only_gate_passed':'blocked'};
  }catch{return {status:'blocked'};}
}
