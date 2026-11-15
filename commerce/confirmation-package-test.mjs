// Isolated preparation for synthetic snapshots. It neither proves that the
// information was shown at checkout nor produces a legally approved contract.
import {buildConfirmationEvidence} from './confirmation-evidence.mjs';
const encoder=new TextEncoder();
const version=value=>typeof value==='string'&&/^[A-Za-z0-9._-]{1,64}$/.test(value);
const text=value=>typeof value==='string'&&Boolean(value.trim())&&value.length<=20000&&!/[\u0000-\u0009\u000B-\u001F\u007F]/.test(value);
async function digest(doc){
  const bytes=new Uint8Array(await crypto.subtle.digest('SHA-256',encoder.encode(JSON.stringify(doc))));
  return btoa(String.fromCharCode(...bytes)).replaceAll('+','-').replaceAll('/','_').replaceAll('=','');
}
export async function buildTestConfirmationPackage(input){
  const evidence=await buildConfirmationEvidence(input);
  const fail=()=>{throw new Error('Invalid confirmation supplement');};
  if(!Array.isArray(input.supplements)||input.supplements.length!==2)fail();
  const supplements=[];
  for(const kind of ['withdrawal_notice','withdrawal_form']){
    const matching=input.supplements.filter(row=>row?.kind===kind);
    if(matching.length!==1)fail();
    const row=matching[0];
    if(row.order_id!==evidence.orderId||row.language!==evidence.language||row.captured_at!==evidence.terms.acceptedAt||!version(row.version)||!text(row.text))fail();
    const doc={kind,language:row.language,version:row.version,text:row.text};
    if(typeof row.digest!=='string'||row.digest!==await digest(doc))fail();
    supplements.push({...doc,digest:row.digest,capturedAt:row.captured_at});
  }
  return {...evidence,supplements};
}
const allowedTest=(env,language)=>env?.FULL_CONFIRMATION_TEST_ENABLED==='true'&&env?.STRIPE_LIVE==='false'&&['de','en'].includes(language);
export async function calculateTestFullSnapshotDigest(language,digests){
  if(!['de','en'].includes(language)||!Array.isArray(digests)||digests.length!==5||digests.some(d=>typeof d!=='string'||!/^[A-Za-z0-9_-]{43}$/.test(d)))throw new Error('Invalid test checkout snapshot');
  const [termsDigest,earlyDigest,ackDigest,noticeDigest,formDigest]=digests;
  return digest({schema:'test_full_confirmation_v1',language,termsDigest,earlyDigest,ackDigest,noticeDigest,formDigest});
}

// These are the exact documents the test UI must render and hash. Both the
// initial rendering and purchase preparation must call this against immutable
// document versions; the latter compares against the displayed digests.
export async function readTestSupplementDocuments(db,env,language){
  if(!allowedTest(env,language))throw new Error('Test confirmation unavailable');
  const suffix=language.toUpperCase();
  const entries=[['withdrawal_notice',env['WITHDRAWAL_NOTICE_VERSION_'+suffix]],['withdrawal_form',env['WITHDRAWAL_FORM_VERSION_'+suffix]]];
  const docs=[];
  for(const [kind,docVersion] of entries){
    if(!version(docVersion))throw new Error('Test confirmation unavailable');
    const row=await db.prepare('SELECT text FROM contract_documents WHERE language=? AND version=?').bind(language,docVersion).first();
    if(!row||!text(row.text))throw new Error('Test confirmation unavailable');
    const doc={kind,language,version:docVersion,text:row.text};docs.push({...doc,digest:await digest(doc)});
  }
  if(docs[0].version===docs[1].version||docs[0].text===docs[1].text)throw new Error('Test confirmation unavailable');
  return docs;
}

// Caller MUST append these statements to the same db.batch as order,
// acceptance, declarations and checkout authorization; never execute alone.
export async function prepareTestSupplementStatements({db,env,orderId,language,acceptedAt,displayed}){
  if(typeof orderId!=='string'||!/^[A-Za-z0-9_-]{1,128}$/.test(orderId)||!Number.isSafeInteger(acceptedAt)||acceptedAt<=0||acceptedAt>8640000000000000||!Array.isArray(displayed)||displayed.length!==2)throw new Error('Invalid test checkout snapshot');
  const docs=await readTestSupplementDocuments(db,env,language);
  if(docs.some((d,i)=>d.digest!==displayed[i]))throw new Error('Test confirmation changed');
  return docs.map(d=>db.prepare('INSERT INTO test_confirmation_supplements(order_id,kind,language,version,text,digest,captured_at) VALUES(?,?,?,?,?,?,?)').bind(orderId,d.kind,d.language,d.version,d.text,d.digest,acceptedAt));
}

// A resume reads only order snapshots and never chooses current documents.
// Returned digest must be bound into the checkout authorization token, and
// compared again immediately before the provider session is returned.
export async function loadTestFullOrderSnapshot(db,env,orderId,language){
  if(!allowedTest(env,language))throw new Error('Test confirmation unavailable');
  const acceptance=await db.prepare('SELECT order_id,language,version,text,digest,accepted_at FROM contract_acceptances WHERE order_id=?').bind(orderId).first();
  const declarations=(await db.prepare('SELECT order_id,kind,language,version,text,digest,accepted_at FROM contract_declarations WHERE order_id=?').bind(orderId).all()).results;
  const supplements=(await db.prepare('SELECT order_id,kind,language,version,text,digest,captured_at FROM test_confirmation_supplements WHERE order_id=?').bind(orderId).all()).results;
  const p=await buildTestConfirmationPackage({orderId,language,acceptance,declarations,supplements});
  const snapshotDigest=await calculateTestFullSnapshotDigest(p.language,[p.terms.digest,...p.declarations.map(d=>d.digest),...p.supplements.map(d=>d.digest)]);
  return {...p,digest:snapshotDigest};
}
