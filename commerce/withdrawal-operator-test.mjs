// Isolated synthetic operator prototype. Not imported by the public worker.
import {getMailStatus} from './mail-events.mjs';
const uuid=value=>typeof value==='string'&&/^[0-9a-f]{8}-[0-9a-f]{4}-4[0-9a-f]{3}-[89ab][0-9a-f]{3}-[0-9a-f]{12}$/i.test(value);
const validToken=value=>typeof value==='string'&&/^[A-Za-z0-9_-]{43,128}$/.test(value);
const headers={'Cache-Control':'no-store','X-Content-Type-Options':'nosniff','Referrer-Policy':'no-referrer','Content-Type':'application/json;charset=utf-8'};
const json=(value,status=200,additional={})=>new Response(JSON.stringify(value),{status,headers:{...headers,...additional}});

// Comparison through WebCrypto's HMAC verification avoids direct token
// comparison; never record or return either secret or provider error text.
async function authorized(expected,presented){
 if(!validToken(presented))return false;
 const bytes=new TextEncoder(),data=bytes.encode('MAKRIVO test operator authentication v1');
 const key=await crypto.subtle.importKey('raw',bytes.encode(expected),{name:'HMAC',hash:'SHA-256'},false,['verify']);
 const candidate=await crypto.subtle.importKey('raw',bytes.encode(presented),{name:'HMAC',hash:'SHA-256'},false,['sign']);
 const signature=await crypto.subtle.sign('HMAC',candidate,data);
 return crypto.subtle.verify('HMAC',key,signature,data);
}

async function smallJson(req){
 const reader=req.body?.getReader();if(!reader)throw new TypeError('Invalid request');
 let size=0;const chunks=[];
 while(true){const {done,value}=await reader.read();if(done)break;size+=value.length;if(size>2048)throw new RangeError('Request too large');chunks.push(value);}
 const bytes=new Uint8Array(size);let offset=0;for(const chunk of chunks){bytes.set(chunk,offset);offset+=chunk.length;}
 return JSON.parse(new TextDecoder('utf-8',{fatal:true}).decode(bytes));
}

export async function withdrawalOperatorTestFetch(req,env){
 if(env?.WITHDRAWAL_OPERATOR_TEST_ONLY!=='true'||env?.STRIPE_LIVE!=='false'||!env?.DB||!validToken(env?.WITHDRAWAL_OPERATOR_TEST_TOKEN)||typeof env?.TEST_MAIL_RECIPIENT!=='string'||!/^\S+@\S+\.\S+$/.test(env.TEST_MAIL_RECIPIENT))return json({error:'Test operator unavailable'},503);
 let url;try{url=new URL(req.url);if(new URL(env.OPERATOR_TEST_ORIGIN).origin!==env.OPERATOR_TEST_ORIGIN||url.origin!==env.OPERATOR_TEST_ORIGIN)throw Error();}catch{return json({error:'Test operator unavailable'},503);}
 try{
  const match=/^Bearer ([A-Za-z0-9_-]{43,128})$/.exec(req.headers.get('Authorization')||'');
  if(!match||!await authorized(env.WITHDRAWAL_OPERATOR_TEST_TOKEN,match[1]))return json({error:'Unauthorized'},401,{'WWW-Authenticate':'Bearer'});
  if(url.pathname==='/operator/withdrawals'&&req.method==='GET'){
   const rows=(await env.DB.prepare("SELECT r.id,r.received_at,q.status AS receipt_state,a.state AS attempt_state,CASE WHEN l.request_id IS NULL THEN 'missing_language' ELSE 'ready' END AS metadata_state FROM withdrawal_requests r JOIN withdrawal_receipts q ON q.request_id=r.id LEFT JOIN mail_dispatch_attempts a ON a.kind=? AND a.job_id=r.id LEFT JOIN withdrawal_test_languages l ON l.request_id=r.id WHERE r.contact_email=? ORDER BY r.received_at DESC,r.id DESC LIMIT 51").bind('withdrawal',env.TEST_MAIL_RECIPIENT).all()).results;
   return json({cases:rows.slice(0,50),truncated:rows.length>50});
  }
  const parts=url.pathname.match(/^\/operator\/withdrawals\/([^/]+)(\/review)?$/);
  if(!parts||!uuid(parts[1]))return json({error:'Not found'},404);
  const id=parts[1].toLowerCase();
  const record=await env.DB.prepare('SELECT r.name,r.contract_reference,r.contact_email,r.received_at,l.language,q.status AS receipt_state FROM withdrawal_requests r JOIN withdrawal_receipts q ON q.request_id=r.id LEFT JOIN withdrawal_test_languages l ON l.request_id=r.id WHERE r.id=? AND r.contact_email=?').bind(id,env.TEST_MAIL_RECIPIENT).first();
  if(!record)return json({error:'Not found'},404);
  if(!parts[2]&&req.method==='GET'){
   if(!record.language)return json({error:'Test case requires review'},503);
   const delivery=await getMailStatus(env.DB,'withdrawal',id);
   return json({id,name:record.name,contract:record.contract_reference,email:record.contact_email,receivedAt:record.received_at,language:record.language,receiptState:record.receipt_state,deliveryStatus:delivery.status});
  }
  if(parts[2]&&req.method==='GET'){
   const rows=(await env.DB.prepare('SELECT action,created_at FROM withdrawal_test_operator_events WHERE request_id=? ORDER BY created_at DESC,event_id DESC LIMIT 51').bind(id).all()).results;
   return json({events:rows.slice(0,50).map(x=>({action:x.action,createdAt:x.created_at})),truncated:rows.length>50});
  }
  if(parts[2]&&req.method==='POST'){
   if(req.headers.get('Origin')!==env.OPERATOR_TEST_ORIGIN)return json({error:'Invalid origin'},403);
   if(!/^application\/json(?:\s*;|$)/i.test(req.headers.get('Content-Type')||''))return json({error:'Invalid request'},415);
   let input;
   try{input=await smallJson(req);}catch(error){
    if(error instanceof RangeError)return json({error:'Request too large'},413);
    if(error instanceof SyntaxError||error instanceof TypeError)return json({error:'Invalid request'},400);
    throw error;
   }
   if(!input||typeof input!=='object'||Array.isArray(input)||Object.keys(input).sort().join(',')!=='action,eventId'||!uuid(input.eventId)||!['investigating','escalated'].includes(input.action))return json({error:'Invalid request'},400);
   const eventId=input.eventId.toLowerCase(),now=Date.now();
   const inserted=await env.DB.prepare('INSERT INTO withdrawal_test_operator_events(event_id,request_id,action,created_at) SELECT ?,?,?,? WHERE NOT EXISTS(SELECT 1 FROM withdrawal_test_operator_events WHERE event_id=?) ON CONFLICT(event_id) DO NOTHING RETURNING event_id').bind(eventId,id,input.action,now,eventId).first();
   if(inserted)return json({status:'recorded'},201);
   const existing=await env.DB.prepare('SELECT request_id,action FROM withdrawal_test_operator_events WHERE event_id=?').bind(eventId).first();
   if(!existing||existing.request_id!==id||existing.action!==input.action)return json({error:'Conflict'},409);
   return json({status:'already_recorded'});
  }
  return json({error:'Not found'},404);
 }catch{return json({error:'Test operator unavailable'},503);}
}
