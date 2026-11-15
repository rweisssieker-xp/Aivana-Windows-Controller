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
