// Local synthetic experiment only; the self-signed payload is deliberately
// incompatible with the application's signed license schema and trust chain.
import {loadTestFullOrderSnapshot} from './confirmation-package-test.mjs';
import {renderTestConfirmation} from './confirmation-render-test.mjs';

const encoder=new TextEncoder();
const b64=bytes=>btoa(String.fromCharCode(...new Uint8Array(bytes))).replaceAll('+','-').replaceAll('/','_').replaceAll('=','');
const hash=async value=>b64(await crypto.subtle.digest('SHA-256',encoder.encode(value)));

export async function issueSyntheticArtifact({db,env,licenseId,deviceId}={}){
  const blocked={status:'blocked'};
  if(env?.STRIPE_LIVE!=='false'||env?.FULL_CONFIRMATION_TEST_ENABLED!=='true'||env?.MAIL_DELIVERY_TEST_ENABLED!=='true'||env?.LICENSE_DELIVERY_PROOF_TEST_ENABLED!=='true'||env?.TEST_LICENSE_SIGNATURE_ENABLED!=='true'||typeof licenseId!=='string'||!/^synthetic-[A-Za-z0-9_-]{1,118}$/.test(licenseId)||licenseId!==env?.TEST_LICENSE_ID||typeof deviceId!=='string'||!/^[0-9a-f]{64}$/.test(deviceId)||deviceId!==env?.TEST_DEVICE_ID)return blocked;
  try {
    const row=await db.prepare('SELECT e.order_id,a.return_lang FROM entitlements e JOIN checkout_attempts a ON a.order_id=e.order_id WHERE e.id=?').bind(licenseId).first();
    if(!row||!/^synthetic-[A-Za-z0-9_-]{1,118}$/.test(row.order_id))return blocked;
    const proof=await db.prepare('SELECT facts_text FROM test_confirmation_dispatch_proofs WHERE order_id=?').bind(row.order_id).first();
    if(typeof proof?.facts_text!=='string'||!proof.facts_text)return blocked;
    const evidence=await loadTestFullOrderSnapshot(db,env,row.order_id,row.return_lang);
    const textDigest=await hash(renderTestConfirmation({orderId:row.order_id,evidence,facts:proof.facts_text}));

    const keys=await crypto.subtle.generateKey({name:'RSASSA-PKCS1-v1_5',modulusLength:2048,publicExponent:new Uint8Array([1,0,1]),hash:'SHA-256'},true,['sign','verify']);
    const createdAt=Date.now();
    const payload=b64(encoder.encode(JSON.stringify({marker:'MAKRIVO-TEST-ONLY-NOT-A-LICENSE',orderId:row.order_id,licenseId,deviceId,createdAt})));
    const signature=b64(await crypto.subtle.sign('RSASSA-PKCS1-v1_5',keys.privateKey,encoder.encode(payload)));
    const publicKey=b64(await crypto.subtle.exportKey('spki',keys.publicKey));
    const artifact={payload,signature,publicKey},artifactDigest=await hash(JSON.stringify(artifact));

    // The only release point: test confirmation, refund, device and provider
    // signals are all checked in the same conditional database statement.
    const q=`INSERT INTO test_signed_license_artifacts(license_id,device_id,artifact_digest,created_at)
      SELECT ?1,?2,?4,?5 WHERE EXISTS(
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
      ) ON CONFLICT(license_id,device_id) DO NOTHING RETURNING artifact_digest`;
    const recorded=await db.prepare(q).bind(licenseId,deviceId,textDigest,artifactDigest,createdAt).first();
    if(recorded?.artifact_digest!==artifactDigest)return blocked;
    return {status:'test_artifact_issued',artifact};
  } catch {return blocked;}
}
