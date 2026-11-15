import {test} from 'node:test';
import assert from 'node:assert/strict';
import {runInNewContext} from 'node:vm';
import {withdrawalTestFetch,formatWithdrawalReceipt} from '../withdrawal.mjs';

const origin='https://test.invalid';
const env={DB:{},PUBLIC_ORIGIN:origin,WITHDRAWAL_TEST_ONLY:'true',STRIPE_LIVE:'false'};

test('isolated English test page uses English for the two steps, alternate contact and failure messages',async()=>{
 const response=await withdrawalTestFetch(new Request(origin+'/withdrawal?lang=en'),env);
 const html=await response.text();assert.equal(response.status,200);assert.match(html,/<html lang="en">/);
 for(const phrase of ['Withdraw from contract','Confirm withdrawal','Change details','This test does not send a confirmation','info@aivana-gmbh.ai']) assert.ok(html.includes(phrase),phrase);
 for(const phrase of ['Vertrag widerrufen','Widerruf bestätigen','Angaben ändern','Erklärung gespeichert']) assert.ok(!html.includes(phrase),phrase);
 const elements=Object.fromEntries(['name','contract','email','preview','confirm','details','status','edit','new'].map(id=>[id,{}]));
 elements.name.value='Test Person';elements.contract.value='Order 123';elements.email.value='test@example.test';
 const code=html.match(/<script>([\s\S]*?)<\/script>/)[1];
 runInNewContext(code,{document:{getElementById:id=>elements[id]},crypto:{randomUUID:()=> 'de04a3fa-c3a2-4ef6-b438-f17c021009ae'},fetch:async()=>({status:429})});
 elements.preview.onclick();assert.match(elements.details.textContent,/I withdraw/);await elements.confirm.onclick();assert.match(elements.status.textContent,/temporarily limited/i);assert.match(elements.status.textContent,/info@aivana-gmbh.ai/);
});

test('unknown language defaults to German and English language selector never bypasses the test gate',async()=>{
 const html=await(await withdrawalTestFetch(new Request(origin+'/withdrawal?lang=fr'),env)).text();assert.match(html,/<html lang="de">/);assert.match(html,/Widerruf bestätigen/);
 const response=await withdrawalTestFetch(new Request(origin+'/withdrawal?lang=en'),{...env,STRIPE_LIVE:'true'});assert.equal(response.status,503);
});

test('the two-step preview and edit actions provide an accessible focus destination',async()=>{
 const html=await(await withdrawalTestFetch(new Request(origin+'/withdrawal'),env)).text(),elements=Object.fromEntries(['name','contract','email','preview','confirm','details','status','edit','new'].map(id=>[id,{}]));
 let focus='';for(const [id,el] of Object.entries(elements))el.focus=()=>{focus=id};
 elements.name.value='Test Person';elements.contract.value='Order 123';elements.email.value='test@example.test';
 runInNewContext(html.match(/<script>([\s\S]*?)<\/script>/)[1],{document:{getElementById:id=>elements[id]},crypto:{randomUUID:()=> 'de04a3fa-c3a2-4ef6-b438-f17c021009ae'}});
 elements.preview.onclick();assert.equal(focus,'confirm');elements.edit.onclick();assert.equal(focus,'name');
});

test('the isolated form offers both language links and explains that switching clears the entered details',async()=>{
 for(const language of ['de','en']){
  const html=await(await withdrawalTestFetch(new Request(origin+'/withdrawal?lang='+language),env)).text();
  assert.match(html,/href="\/withdrawal\?lang=de"/);assert.match(html,/href="\/withdrawal\?lang=en"/);
  assert.ok(html.includes(language==='de'?'Sprachwechsel löscht eingegebene Angaben':'Changing language clears entered details'));
 }
});

test('language switching is blocked while a withdrawal submission is in flight',async()=>{
 const html=await(await withdrawalTestFetch(new Request(origin+'/withdrawal?lang=en'),env)).text();
 const elements=Object.fromEntries(['name','contract','email','preview','confirm','details','status','edit','new','langDe','langEn'].map(id=>[id,{}]));
 elements.name.value='Test Person';elements.contract.value='Order 123';elements.email.value='test@example.test';
 let release;const fetch=()=>new Promise(resolve=>{release=resolve});
 runInNewContext(html.match(/<script>([\s\S]*?)<\/script>/)[1],{document:{getElementById:id=>elements[id]},fetch,crypto:{randomUUID:()=> 'de04a3fa-c3a2-4ef6-b438-f17c021009ae'}});
 elements.preview.onclick();const submitting=elements.confirm.onclick();let prevented=false;
 elements.langDe.onclick({preventDefault(){prevented=true}});assert.equal(prevented,true);assert.match(elements.status.textContent,/Wait for the request/);
 release({status:429});await submitting;prevented=false;elements.langDe.onclick({preventDefault(){prevented=true}});assert.equal(prevented,false);
});

test('receipt formatter produces both supported language versions and rejects unsupported languages',()=>{
 const record={name:'Test Person',contract_reference:'Test contract',contact_email:'test@example.test',received_at:1700000000000};
 const english=formatWithdrawalReceipt(record,'en');
 for(const phrase of ['Withdrawal receipt','I withdraw','Test Person','Test contract','test@example.test','2023-11-14T22:13:20.000Z'])assert.ok(english.includes(phrase),phrase);
 assert.doesNotMatch(english,/Widerruf|Eingang/);
 const german=formatWithdrawalReceipt(record,'de');assert.match(german,/Widerrufserklärung/);assert.match(german,/2023-11-14T22:13:20.000Z/);
 assert.equal(formatWithdrawalReceipt(record),german);
 assert.throws(()=>formatWithdrawalReceipt(record,'fr'),/language/i);
});

test('English test form submits its fixed language together with reviewed fields',async()=>{
 const html=await(await withdrawalTestFetch(new Request(origin+'/withdrawal?lang=en'),env)).text();
 const elements=Object.fromEntries(['name','contract','email','preview','confirm','details','status','edit','new'].map(id=>[id,{}]));
 elements.name.value='Test Person';elements.contract.value='Order 123';elements.email.value='test@example.test';let sent;
 const fetch=async(_path,opts)=>{sent=JSON.parse(opts.body);return {status:429}};
 runInNewContext(html.match(/<script>([\s\S]*?)<\/script>/)[1],{document:{getElementById:id=>elements[id]},fetch,crypto:{randomUUID:()=> 'de04a3fa-c3a2-4ef6-b438-f17c021009ae'}});
 elements.preview.onclick();await elements.confirm.onclick();assert.equal(sent.language,'en');assert.equal(sent.contract,'Order 123');assert.equal(sent.confirmed,true);
});
