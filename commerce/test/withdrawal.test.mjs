import { test } from 'node:test';
import assert from 'node:assert/strict';
import { DatabaseSync } from 'node:sqlite';
import { readFileSync } from 'node:fs';
import { acceptWithdrawal, formatWithdrawalReceipt, withdrawalTestFetch } from '../withdrawal.mjs';
import { runInNewContext } from 'node:vm';

function database() {
  const raw = new DatabaseSync(':memory:');
  raw.exec(readFileSync(new URL('../migrations/0010_withdrawal.sql', import.meta.url), 'utf8'));
  return {
    raw,
    async batch(statements) {
      raw.exec('BEGIN IMMEDIATE');
      try { const results = statements.map(s => s._run()); raw.exec('COMMIT'); return results; }
      catch (e) { raw.exec('ROLLBACK'); throw e; }
    },
    prepare(sql) { return { bind(...values) { const q=raw.prepare(sql); return { _run:()=>q.run(...values), first:async()=>q.get(...values) || null }; } }; }
  };
}
const input = { requestId:'de04a3fa-c3a2-4ef6-b438-f17c021009ae', name:'Alex Beispiel', contract:'MAKRIVO vom 10.11.2026', email:'alex@example.test', confirmed:true };

test('atomic reception records declaration and pending receipt without claiming delivery', async () => {
  const db=database(), before=Date.now(); const result=await acceptWithdrawal(db,input); const after=Date.now();
  const request=db.raw.prepare('SELECT * FROM withdrawal_requests').get();
  const queued=db.raw.prepare('SELECT * FROM withdrawal_receipts').get();
  assert.equal(result.status,'receipt_pending'); assert.equal(result.requestId,input.requestId);
  assert.ok(result.receivedAt >= before && result.receivedAt <= after);
  assert.equal(request.name,input.name); assert.equal(request.contract_reference,input.contract); assert.equal(request.contact_email,input.email);
  assert.equal(request.received_at,result.receivedAt); assert.equal(queued.request_id,input.requestId); assert.equal(queued.status,'pending');
  assert.equal(result.name,undefined); assert.equal(result.email,undefined);
  const text=formatWithdrawalReceipt(request);
  for (const expected of [input.name,input.contract,input.email,new Date(result.receivedAt).toISOString()]) assert.ok(text.includes(expected));
});

test('same request can be retried without changing receipt or adding duplicate work', async () => {
  const db=database(); const first=await acceptWithdrawal(db,input); const second=await acceptWithdrawal(db,input);
  assert.deepEqual(second,first); assert.equal(db.raw.prepare('SELECT count(*) n FROM withdrawal_requests').get().n,1);
  assert.equal(db.raw.prepare('SELECT count(*) n FROM withdrawal_receipts').get().n,1);
  await assert.rejects(()=>acceptWithdrawal(db,{...input,contract:'Anderer Vertrag'}),/conflict/i);
  assert.equal(db.raw.prepare('SELECT contract_reference FROM withdrawal_requests').get().contract_reference,input.contract);
});

test('validation rejects missing confirmation, injected control characters and bad identifiers before writing', async () => {
  const db=database();
  for (const bad of [{...input,confirmed:false},{...input,name:''},{...input,contract:''},{...input,email:'not-an-email'},{...input,email:'x@example.test\nBcc: victim@example.test'},{...input,requestId:'not-a-uuid'}])
    await assert.rejects(()=>acceptWithdrawal(db,bad),/invalid/i);
  assert.equal(db.raw.prepare('SELECT count(*) n FROM withdrawal_requests').get().n,0);
});

test('an outbox write failure rolls back the withdrawal as well', async () => {
  const db=database(); db.raw.exec("CREATE TRIGGER fail_receipt BEFORE INSERT ON withdrawal_receipts BEGIN SELECT RAISE(ABORT,'unavailable'); END;");
  await assert.rejects(()=>acceptWithdrawal(db,input));
  assert.equal(db.raw.prepare('SELECT count(*) n FROM withdrawal_requests').get().n,0);
  assert.equal(db.raw.prepare('SELECT count(*) n FROM withdrawal_receipts').get().n,0);
});

test('isolated test UI previews first, then stores the final declaration with pending receipt', async () => {
  const db=database(); const origin='https://test.invalid'; const env={WITHDRAWAL_TEST_ONLY:'true',STRIPE_LIVE:'false',PUBLIC_ORIGIN:origin,DB:db};
  const page=await withdrawalTestFetch(new Request(origin+'/withdrawal'),env);
  assert.equal(page.status,200); const html=await page.text();
  assert.match(html,/TEST/); assert.match(html,/Vertrag widerrufen/); assert.match(html,/Widerruf bestätigen/);
  const script=html.match(/<script>([\s\S]*?)<\/script>/)[1];
  const elements=Object.fromEntries(['name','contract','email','preview','confirm','details','status'].map(id=>[id,{}]));
  Object.assign(elements.name,{value:input.name}); Object.assign(elements.contract,{value:input.contract}); Object.assign(elements.email,{value:input.email});
  let sent=0; const document={getElementById:id=>elements[id]};
  const fetch=async (path,opts)=>{ sent++; return withdrawalTestFetch(new Request(origin+path,{...opts,headers:{...opts.headers,Origin:origin}}),env); };
  runInNewContext(script,{document,fetch,crypto:{randomUUID:()=>input.requestId}});
  assert.equal(sent,0); elements.preview.onclick(); assert.equal(sent,0); assert.equal(elements.confirm.hidden,false); assert.ok(elements.details.textContent.includes(input.contract));
  await elements.confirm.onclick(); assert.equal(sent,1); assert.match(elements.status.textContent,/TEST/); assert.match(elements.status.textContent,/nicht versendet/i);
  assert.equal(db.raw.prepare('SELECT count(*) n FROM withdrawal_requests').get().n,1); assert.equal(db.raw.prepare('SELECT status FROM withdrawal_receipts').get().status,'pending');
});

test('test route is off by default, rejects live and cross-origin requests without storing anything', async () => {
  const db=database(); const origin='https://test.invalid'; const req=(headers={})=>new Request(origin+'/withdrawal',{method:'POST',headers:{'Content-Type':'application/json',...headers},body:JSON.stringify(input)});
  assert.equal((await withdrawalTestFetch(req({Origin:origin}),{DB:db,PUBLIC_ORIGIN:origin})).status,503);
  assert.equal((await withdrawalTestFetch(req({Origin:origin}),{DB:db,PUBLIC_ORIGIN:origin,WITHDRAWAL_TEST_ONLY:'true',STRIPE_LIVE:'true'})).status,503);
  const env={DB:db,PUBLIC_ORIGIN:origin,WITHDRAWAL_TEST_ONLY:'true',STRIPE_LIVE:'false'};
  assert.equal((await withdrawalTestFetch(req({Origin:'https://evil.test'}),env)).status,403);
  assert.equal((await withdrawalTestFetch(req(),env)).status,403);
  assert.equal(db.raw.prepare('SELECT count(*) n FROM withdrawal_requests').get().n,0);
});

test('preview cannot change while final submission is awaiting a response', async () => {
  const db=database(), origin='https://test.invalid', env={WITHDRAWAL_TEST_ONLY:'true',STRIPE_LIVE:'false',PUBLIC_ORIGIN:origin,DB:db};
  const html=await (await withdrawalTestFetch(new Request(origin+'/withdrawal'),env)).text();
  const elements=Object.fromEntries(['name','contract','email','preview','confirm','details','status'].map(id=>[id,{}]));
  elements.name.value=input.name;elements.contract.value=input.contract;elements.email.value=input.email;
  let release;const fetch=()=>new Promise(resolve=>{release=resolve;});
  runInNewContext(html.match(/<script>([\s\S]*?)<\/script>/)[1],{document:{getElementById:id=>elements[id]},fetch,crypto:{randomUUID:()=>input.requestId}});
  elements.preview.onclick(); const original=elements.details.textContent; const submitting=elements.confirm.onclick();
  elements.contract.value='Andere Bestellung'; elements.preview.onclick(); assert.equal(elements.details.textContent,original);
  release({ok:true}); await submitting;
});

test('oversized, malformed and conflicting submissions do not expose stored details', async () => {
  const db=database(), origin='https://test.invalid', env={WITHDRAWAL_TEST_ONLY:'true',STRIPE_LIVE:'false',PUBLIC_ORIGIN:origin,DB:db};
  const post=body=>withdrawalTestFetch(new Request(origin+'/withdrawal',{method:'POST',headers:{Origin:origin,'Content-Type':'application/json'},body}),env);
  assert.equal((await post('x'.repeat(8193))).status,413);
  assert.equal((await post('{')).status,400);
  const accepted=await post(JSON.stringify(input)); assert.equal(accepted.status,202);
  const result=await accepted.json(); assert.equal(result.name,undefined); assert.equal(result.email,undefined);
  const conflict=await post(JSON.stringify({...input,email:'other@example.test'})); assert.equal(conflict.status,409);
  assert.doesNotMatch(await conflict.text(),/Alex Beispiel|alex@example\.test/);
  assert.equal(db.raw.prepare('SELECT count(*) n FROM withdrawal_requests').get().n,1);
});

test('preview treats consumer-supplied markup as plain text', async () => {
  const db=database(), origin='https://test.invalid', env={WITHDRAWAL_TEST_ONLY:'true',STRIPE_LIVE:'false',PUBLIC_ORIGIN:origin,DB:db};
  const html=await (await withdrawalTestFetch(new Request(origin+'/withdrawal'),env)).text();
  const elements=Object.fromEntries(['name','contract','email','preview','confirm','details','status'].map(id=>[id,{}]));
  elements.name.value='<img src=x onerror=alert(1)>';elements.contract.value=input.contract;elements.email.value=input.email;
  runInNewContext(html.match(/<script>([\s\S]*?)<\/script>/)[1],{document:{getElementById:id=>elements[id]},crypto:{randomUUID:()=>input.requestId}});
  elements.preview.onclick(); assert.match(elements.details.textContent,/<img/); assert.equal(elements.details.innerHTML,undefined);
});

test('a lost successful response can be retried with the same request ID', async () => {
  const db=database(), origin='https://test.invalid', env={WITHDRAWAL_TEST_ONLY:'true',STRIPE_LIVE:'false',PUBLIC_ORIGIN:origin,DB:db};
  const html=await (await withdrawalTestFetch(new Request(origin+'/withdrawal'),env)).text();
  const elements=Object.fromEntries(['name','contract','email','preview','confirm','details','status'].map(id=>[id,{}]));
  elements.name.value=input.name;elements.contract.value=input.contract;elements.email.value=input.email;
  let calls=0;const fetch=async(path,opts)=>{const res=await withdrawalTestFetch(new Request(origin+path,{...opts,headers:{...opts.headers,Origin:origin}}),env); if(++calls===1)throw Error('network response lost'); return res;};
  runInNewContext(html.match(/<script>([\s\S]*?)<\/script>/)[1],{document:{getElementById:id=>elements[id]},fetch,crypto:{randomUUID:()=>input.requestId}});
  elements.preview.onclick();await elements.confirm.onclick();assert.equal(elements.confirm.disabled,false);assert.match(elements.status.textContent,/Erneut versuchen/);
  await elements.confirm.onclick();assert.match(elements.status.textContent,/nicht versendet/i);
  assert.equal(db.raw.prepare('SELECT count(*) n FROM withdrawal_requests').get().n,1);assert.equal(db.raw.prepare('SELECT count(*) n FROM withdrawal_receipts').get().n,1);
});
