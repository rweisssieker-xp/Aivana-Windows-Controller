// Test-only: caller must provide the real Stripe validation function and must
// never expose this module as an unauthenticated email endpoint.
import {sendPreparedEmail} from './cloudflare-mail.mjs';
import {buildConfirmationEvidence} from './confirmation-evidence.mjs';
import {buildTestConfirmationPackage} from './confirmation-package-test.mjs';
import {formatTestOrderFacts} from './confirmation-facts-test.mjs';
import {renderTestConfirmation} from './confirmation-render-test.mjs';
import {formatWithdrawalReceipt,hasWithdrawalLanguageTable} from './withdrawal.mjs';

const allowed = (env, binding) => env?.MAIL_DELIVERY_TEST_ENABLED === 'true' && env?.STRIPE_LIVE === 'false' && typeof env?.TEST_MAIL_RECIPIENT === 'string' && typeof env?.MAIL_SENDER === 'string' && typeof binding?.send === 'function';

async function sendOnce({db,env,binding,kind,jobId,recipient,subject,text}) {
  // The claim and existence check are one statement: concurrent cron runs may
  // prepare the same job, but only one can ever call the provider.
  const query = kind === 'purchase'
    ? "INSERT INTO mail_dispatch_attempts(kind,job_id,state,started_at) SELECT 'purchase',?,'processing',? WHERE EXISTS(SELECT 1 FROM confirmation_jobs WHERE order_id=?) ON CONFLICT(kind,job_id) DO NOTHING RETURNING job_id"
    : "INSERT INTO mail_dispatch_attempts(kind,job_id,state,started_at) SELECT 'withdrawal',?,'processing',? WHERE EXISTS(SELECT 1 FROM withdrawal_receipts WHERE request_id=? AND status='pending') ON CONFLICT(kind,job_id) DO NOTHING RETURNING job_id";
  const claimed = await db.prepare(query).bind(jobId,Date.now(),jobId).first();
  if (!claimed) return {status:'already_claimed'};
  let result;
  try { result=await sendPreparedEmail(binding,{sender:env.MAIL_SENDER,recipient,subject,text}); }
  catch { result={status:'outcome_unknown'}; }
  const accepted = result.status === 'accepted_by_provider';
  const updated=await db.prepare("UPDATE mail_dispatch_attempts SET state=?,finished_at=?,message_id=? WHERE kind=? AND job_id=? AND state='processing' RETURNING job_id")
    .bind(accepted?'accepted':'unknown',Date.now(),accepted?result.messageId:null,kind,jobId).first();
  if (!updated) throw new Error('Mail attempt state could not be recorded');
  return {status:accepted?'accepted_by_provider':'outcome_unknown'};
}

export async function sendPurchaseJob({db,env,binding,jobId,loadSession,validateSession}) {
  if (!allowed(env,binding) || typeof loadSession !== 'function' || typeof validateSession !== 'function') return {status:'not_ready'};
  const order=await db.prepare('SELECT o.id,o.session_id,a.return_lang,e.payment_intent FROM orders o JOIN checkout_attempts a ON a.order_id=o.id JOIN entitlements e ON e.order_id=o.id AND e.session_id=o.session_id JOIN confirmation_jobs j ON j.order_id=o.id WHERE o.id=?').bind(jobId).first();
  if (!order || !order.payment_intent) return {status:'not_ready'};
  let session;
  try { session=await loadSession(order.session_id); } catch { return {status:'not_ready'}; }
  if (!session || session.id!==order.session_id || session.payment_intent!==order.payment_intent || session.metadata?.orderId!==order.id || !validateSession(session,env) || session.customer_details?.email!==env.TEST_MAIL_RECIPIENT) return {status:'not_ready'};
  const acceptance=await db.prepare('SELECT order_id,language,version,text,digest,accepted_at FROM contract_acceptances WHERE order_id=?').bind(order.id).first();
  const declarations=(await db.prepare('SELECT order_id,kind,language,version,text,digest,accepted_at FROM contract_declarations WHERE order_id=? ORDER BY kind').bind(order.id).all()).results;
  let evidence,facts='';
  try {
    const input={orderId:order.id,language:order.return_lang,acceptance,declarations};
    if(env.FULL_CONFIRMATION_TEST_ENABLED==='true'){
      input.supplements=(await db.prepare('SELECT order_id,kind,language,version,text,digest,captured_at FROM test_confirmation_supplements WHERE order_id=?').bind(order.id).all()).results;
      evidence=await buildTestConfirmationPackage(input);
      facts=formatTestOrderFacts({env,session,orderId:order.id,sessionId:order.session_id,paymentIntent:order.payment_intent,language:evidence.language});
    } else evidence=await buildConfirmationEvidence(input);
  } catch { return {status:'not_ready'}; }
  const text=renderTestConfirmation({orderId:order.id,evidence,facts});
  return sendOnce({db,env,binding,kind:'purchase',jobId,recipient:session.customer_details.email,subject:'MAKRIVO - TEST Vertragsinformationen',text});
}

export async function sendWithdrawalJob({db,env,binding,jobId}) {
  if (!allowed(env,binding)) return {status:'not_ready'};
  const localized=await hasWithdrawalLanguageTable(db);
  const sql=localized ? "SELECT r.name,r.contract_reference,r.contact_email,r.received_at,l.language FROM withdrawal_requests r JOIN withdrawal_receipts q ON q.request_id=r.id JOIN withdrawal_test_languages l ON l.request_id=r.id WHERE r.id=? AND q.status='pending'" : "SELECT r.name,r.contract_reference,r.contact_email,r.received_at,'de' AS language FROM withdrawal_requests r JOIN withdrawal_receipts q ON q.request_id=r.id WHERE r.id=? AND q.status='pending'";
  const record=await db.prepare(sql).bind(jobId).first();
  if (!record || record.contact_email!==env.TEST_MAIL_RECIPIENT) return {status:'not_ready'};
  let text; try { text=(record.language==='en'?'TEST — not an actual withdrawal process\n\n':'TEST — keine echte Widerrufsbearbeitung\n\n')+formatWithdrawalReceipt(record,record.language); } catch { return {status:'not_ready'}; }
  return sendOnce({db,env,binding,kind:'withdrawal',jobId,recipient:record.contact_email,subject:record.language==='en'?'MAKRIVO - TEST Withdrawal receipt':'MAKRIVO - TEST Widerrufseingang',text});
}
