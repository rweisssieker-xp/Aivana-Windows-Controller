import {test} from 'node:test';
import assert from 'node:assert/strict';
import {DatabaseSync} from 'node:sqlite';
import {readFileSync} from 'node:fs';
import {acceptWithdrawal,withdrawalTestFetch} from '../withdrawal.mjs';
import {sendWithdrawalJob} from '../delivery-dispatch.mjs';

function database({legacy=false,migrate=true}={}){
 const raw=new DatabaseSync(':memory:');
 raw.exec(readFileSync(new URL('../migrations/0010_withdrawal.sql',import.meta.url),'utf8'));
 raw.exec(readFileSync(new URL('../migrations/0013_test_mail_attempts.sql',import.meta.url),'utf8'));
 if(legacy){raw.prepare('INSERT INTO withdrawal_requests VALUES(?,?,?,?,?)').run('legacy','Legacy','Old','owner@example.test',1700000000000);raw.prepare("INSERT INTO withdrawal_receipts VALUES('legacy','pending')").run();}
 if(migrate)raw.exec(readFileSync(new URL('../migrations/0018_test_withdrawal_languages.sql',import.meta.url),'utf8'));
 return {raw,async batch(statements){raw.exec('BEGIN IMMEDIATE');try{const r=statements.map(s=>s._run());raw.exec('COMMIT');return r;}catch(e){raw.exec('ROLLBACK');throw e;}},prepare(sql){return {bind(...v){const q=raw.prepare(sql);return {_run:()=>q.run(...v),run:async()=>q.run(...v),first:async()=>q.get(...v)||null,all:async()=>({results:q.all(...v)})};}};}};
}
const input={requestId:'de04a3fa-c3a2-4ef6-b438-f17c021009ae',name:'Test Person',contract:'Synthetic',email:'owner@example.test',confirmed:true,language:'en'};
const environment={STRIPE_LIVE:'false',MAIL_DELIVERY_TEST_ENABLED:'true',TEST_MAIL_RECIPIENT:'owner@example.test',MAIL_SENDER:'sender@example.test'};

test('migration preserves legacy German-only test records as German',()=>{
 const db=database({legacy:true});
 assert.ok(db.raw.prepare("SELECT name FROM sqlite_master WHERE type='table' AND name='withdrawal_test_languages'").get());
 assert.equal(db.raw.prepare('SELECT language FROM withdrawal_test_languages WHERE request_id=?').get('legacy').language,'de');
});

test('language is saved with the test declaration and bound to idempotent retries',async()=>{
 const db=database(),first=await acceptWithdrawal(db,input);const again=await acceptWithdrawal(db,input);
 assert.deepEqual(first,again);
 assert.ok(db.raw.prepare("SELECT name FROM sqlite_master WHERE type='table' AND name='withdrawal_test_languages'").get());
 assert.equal(db.raw.prepare('SELECT language FROM withdrawal_test_languages WHERE request_id=?').get(input.requestId).language,'en');
 await assert.rejects(()=>acceptWithdrawal(db,{...input,language:'de'}),/conflict/i);
 assert.equal(db.raw.prepare('SELECT count(*) n FROM withdrawal_receipts').get().n,1);
});

test('English test receipt uses the saved language, never a caller supplied substitute',async()=>{
 const db=database();await acceptWithdrawal(db,input);const sent=[];
 const result=await sendWithdrawalJob({db,env:environment,binding:{send:async message=>{sent.push(message);return {messageId:'test'};}},jobId:input.requestId});
 assert.equal(result.status,'accepted_by_provider');assert.match(sent[0].text,/I withdraw/);assert.match(sent[0].subject,/Withdrawal/);
});

test('failure to save language rolls back the entire declaration and outbox',async()=>{
 const db=database();
 assert.ok(db.raw.prepare("SELECT name FROM sqlite_master WHERE type='table' AND name='withdrawal_test_languages'").get());
 db.raw.exec("CREATE TRIGGER fail_language BEFORE INSERT ON withdrawal_test_languages BEGIN SELECT RAISE(ABORT,'unavailable'); END;");
 await assert.rejects(()=>acceptWithdrawal(db,input));
 assert.equal(db.raw.prepare('SELECT count(*) n FROM withdrawal_requests').get().n,0);
 assert.equal(db.raw.prepare('SELECT count(*) n FROM withdrawal_receipts').get().n,0);
});

test('old test setup without migration retains German behavior but cannot accept English',async()=>{
 const db=database({migrate:false});
 await assert.rejects(()=>acceptWithdrawal(db,input));
 const result=await acceptWithdrawal(db,{...input,language:'de'});assert.equal(result.status,'receipt_pending');
 const sent=[];await sendWithdrawalJob({db,env:environment,binding:{send:async message=>{sent.push(message);return {messageId:'test'};}},jobId:input.requestId});
 assert.match(sent[0].text,/Widerruf/);
});

test('rerunning migration does not invent German for a later record with missing language',async()=>{
 const db=database();await acceptWithdrawal(db,input);
 db.raw.prepare('DELETE FROM withdrawal_test_languages WHERE request_id=?').run(input.requestId);
 db.raw.exec(readFileSync(new URL('../migrations/0018_test_withdrawal_languages.sql',import.meta.url),'utf8'));
 assert.equal(db.raw.prepare('SELECT language FROM withdrawal_test_languages WHERE request_id=?').get(input.requestId),undefined);
 const sent=[];const outcome=await sendWithdrawalJob({db,env:environment,binding:{send:async message=>{sent.push(message);return {messageId:'test'};}},jobId:input.requestId});
 assert.equal(outcome.status,'not_ready');assert.equal(sent.length,0);
 await assert.rejects(()=>acceptWithdrawal(db,{...input,language:'de'}),/conflict/i);
});

test('throttling replay cannot change the language of a previously accepted declaration',async()=>{
 const db=database();db.raw.exec(readFileSync(new URL('../migrations/0015_test_withdrawal_rate_limits.sql',import.meta.url),'utf8'));
 const origin='https://test.invalid',e={DB:db,PUBLIC_ORIGIN:origin,WITHDRAWAL_TEST_ONLY:'true',STRIPE_LIVE:'false',WITHDRAWAL_TEST_RATE_LIMIT_ENABLED:'true',WITHDRAWAL_TEST_TRUST_PROXY:'true',WITHDRAWAL_TEST_RATE_LIMIT_SECRET:'12345678901234567890123456789012'};
 const post=value=>withdrawalTestFetch(new Request(origin+'/withdrawal',{method:'POST',headers:{Origin:origin,'Content-Type':'application/json','CF-Connecting-IP':'203.0.113.2'},body:JSON.stringify(value)}),e);
 const first=await post(input),original=await first.json();assert.equal(first.status,202);
 assert.equal((await post({...input,requestId:'00000000-0000-4000-8000-000000000002'})).status,202);
 assert.equal((await post({...input,requestId:'00000000-0000-4000-8000-000000000003'})).status,202);
 assert.equal((await post({...input,language:'de'})).status,429);
 const identical=await post(input);assert.equal(identical.status,202);assert.deepEqual(await identical.json(),original);
 assert.equal(db.raw.prepare('SELECT language FROM withdrawal_test_languages WHERE request_id=?').get(input.requestId).language,'en');
});
