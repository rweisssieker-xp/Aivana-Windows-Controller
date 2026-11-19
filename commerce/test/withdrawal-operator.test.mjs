import {test} from 'node:test';
import assert from 'node:assert/strict';
import {DatabaseSync} from 'node:sqlite';
import {readFileSync} from 'node:fs';
import {withdrawalOperatorTestFetch} from '../withdrawal-operator-test.mjs';

const origin='https://operator.example.test',token='ABCDEFGHIJKLMNOPQRSTUVWXYZabcdef0123456789_';
const one='0f887229-8288-498a-9044-452ad1b535c4',other='9a976162-abdb-4223-a4dd-952b78c85c30';
const eventId='f9e80c8c-8a44-4800-b06f-50d1edbd52c3';
function database(){
 const raw=new DatabaseSync(':memory:');
 for(const f of ['0010_withdrawal.sql','0013_test_mail_attempts.sql','0014_test_mail_events.sql','0018_test_withdrawal_languages.sql','0020_test_withdrawal_operator.sql'])raw.exec(readFileSync(new URL('../migrations/'+f,import.meta.url),'utf8'));
 const insert=raw.prepare('INSERT INTO withdrawal_requests VALUES(?,?,?,?,?)');insert.run(one,'Synthetic One','Synthetic order','owner@example.test',1700000000000);insert.run(other,'Private Other','Other order','other@example.test',1700000000001);
 raw.prepare('INSERT INTO withdrawal_test_languages VALUES(?,?)').run(one,'en');raw.prepare('INSERT INTO withdrawal_test_languages VALUES(?,?)').run(other,'de');
 raw.prepare("INSERT INTO withdrawal_receipts VALUES(?,'pending')").run(one);raw.prepare("INSERT INTO withdrawal_receipts VALUES(?,'pending')").run(other);
 return {raw,prepare(sql){return {bind(...v){const q=raw.prepare(sql);return {first:async()=>q.get(...v)||null,all:async()=>({results:q.all(...v)}),run:async()=>q.run(...v)};}};}};
}
const env=db=>({DB:db,WITHDRAWAL_OPERATOR_TEST_ONLY:'true',STRIPE_LIVE:'false',OPERATOR_TEST_ORIGIN:origin,WITHDRAWAL_OPERATOR_TEST_TOKEN:token,TEST_MAIL_RECIPIENT:'owner@example.test'});
const request=(path,{auth=token,method='GET',body,extra={}}={})=>new Request(origin+path,{method,headers:{...(auth?{Authorization:'Bearer '+auth}:{}),...(method==='POST'?{Origin:origin,'Content-Type':'application/json'}:{}),...extra},...(body?{body:JSON.stringify(body)}:{})});

test('operator endpoint fails closed for missing or incorrect authentication, live mode and wrong origin',async()=>{
 const db=database(),e=env(db);
 for(const r of [request('/operator/withdrawals',{auth:''}),request('/operator/withdrawals',{auth:'a'.repeat(43)})])assert.equal((await withdrawalOperatorTestFetch(r,e)).status,401);
 assert.equal((await withdrawalOperatorTestFetch(request('/operator/withdrawals'),{...e,STRIPE_LIVE:'true'})).status,503);
 assert.equal((await withdrawalOperatorTestFetch(request('/operator/withdrawals'),{...e,WITHDRAWAL_OPERATOR_TEST_TOKEN:'weak'})).status,503);
 assert.equal((await withdrawalOperatorTestFetch(request('/operator/withdrawals'),{...e,OPERATOR_TEST_ORIGIN:'https://elsewhere.test'})).status,503);
});

test('case list is bounded, no-store and contains only allowlisted test-recipient pseudonyms',async()=>{
 const db=database(),r=await withdrawalOperatorTestFetch(request('/operator/withdrawals'),env(db));assert.equal(r.status,200);assert.equal(r.headers.get('Cache-Control'),'no-store');
 const json=await r.json();assert.equal(json.cases.length,1);assert.equal(json.cases[0].id,one);
 assert.doesNotMatch(JSON.stringify(json),/Synthetic One|Synthetic order|owner@example\.test|Private Other|other@example\.test/);
});

test('authorized operator can inspect only its own test case, never another recipient',async()=>{
 const db=database(),e=env(db),r=await withdrawalOperatorTestFetch(request('/operator/withdrawals/'+one),e);
 assert.equal(r.status,200);const x=await r.json();assert.equal(x.name,'Synthetic One');assert.equal(x.language,'en');assert.equal(x.deliveryStatus,'not_found');
 assert.equal((await withdrawalOperatorTestFetch(request('/operator/withdrawals/'+other),e)).status,404);
});

test('operator review is bounded, idempotent, append-only and does not change receipt or trigger sending',async()=>{
 const db=database(),e=env(db),path='/operator/withdrawals/'+one+'/review',body={eventId,action:'investigating'};
 const first=await withdrawalOperatorTestFetch(request(path,{method:'POST',body}),e);assert.equal(first.status,201);
 const replay=await withdrawalOperatorTestFetch(request(path,{method:'POST',body}),e);assert.equal(replay.status,200);
 const conflict=await withdrawalOperatorTestFetch(request(path,{method:'POST',body:{...body,action:'escalated'}}),e);assert.equal(conflict.status,409);
 assert.equal(db.raw.prepare('SELECT count(*) n FROM withdrawal_test_operator_events').get().n,1);
 assert.throws(()=>db.raw.exec("UPDATE withdrawal_test_operator_events SET action='escalated'"));
 assert.throws(()=>db.raw.exec('DELETE FROM withdrawal_test_operator_events'));
 assert.throws(()=>db.raw.prepare("INSERT OR REPLACE INTO withdrawal_test_operator_events VALUES(?,?,?,?)").run(eventId,one,'escalated',1700000000000));
 assert.equal(db.raw.prepare('SELECT status FROM withdrawal_receipts WHERE request_id=?').get(one).status,'pending');
 assert.equal(db.raw.prepare('SELECT count(*) n FROM mail_dispatch_attempts').get().n,0);
});

test('an authorized operator can inspect a bounded review history with no free-text content',async()=>{
 const db=database(),e=env(db),path='/operator/withdrawals/'+one+'/review';
 assert.equal((await withdrawalOperatorTestFetch(request(path,{method:'POST',body:{eventId,action:'investigating'}}),e)).status,201);
 const response=await withdrawalOperatorTestFetch(request(path),e);assert.equal(response.status,200);
 const body=await response.json();assert.deepEqual(body.events.map(x=>x.action),['investigating']);assert.equal(body.truncated,false);
 assert.doesNotMatch(JSON.stringify(body),/Synthetic|owner@example\.test/);
});

test('a large test queue is truncated explicitly and never emits unbounded customer details',async()=>{
 const db=database(),e=env(db),insert=db.raw.prepare('INSERT INTO withdrawal_requests VALUES(?,?,?,?,?)'),receipt=db.raw.prepare("INSERT INTO withdrawal_receipts VALUES(?,'pending')");
 for(let i=0;i<55;i++){const id=`00000000-0000-4000-8000-${String(i).padStart(12,'0')}`;insert.run(id,'secret name','secret contract','owner@example.test',1700000000100+i);receipt.run(id);}
 const result=await (await withdrawalOperatorTestFetch(request('/operator/withdrawals'),e)).json();assert.equal(result.cases.length,50);assert.equal(result.truncated,true);assert.doesNotMatch(JSON.stringify(result),/secret name|secret contract|owner@example/);
});

test('operator mutations reject cross-origin, arbitrary notes and unknown actions without storing sensitive data',async()=>{
 const db=database(),e=env(db),path='/operator/withdrawals/'+one+'/review';
 assert.equal((await withdrawalOperatorTestFetch(request(path,{method:'POST',body:{eventId,action:'investigating'},extra:{Origin:'https://evil.test'}}),e)).status,403);
 for(const body of [{eventId,action:'delete'},{eventId,action:'investigating',note:'secret customer text'},{eventId:'bad',action:'escalated'}])assert.equal((await withdrawalOperatorTestFetch(request(path,{method:'POST',body}),e)).status,400);
 assert.equal(db.raw.prepare('SELECT count(*) n FROM withdrawal_test_operator_events').get().n,0);
});

test('internal storage failures are unavailable, not blamed on the request',async()=>{
 const db=database(),e={...env(db),DB:{prepare(){throw new TypeError('sensitive database failure')}}};
 const response=await withdrawalOperatorTestFetch(request('/operator/withdrawals'),e);
 assert.equal(response.status,503);assert.doesNotMatch(await response.text(),/sensitive database failure/);
});

test('simultaneous identical operator actions create only one audit event',async()=>{
 const db=database(),e=env(db),path='/operator/withdrawals/'+one+'/review',body={eventId,action:'investigating'};
 const a=await Promise.all(Array.from({length:10},()=>withdrawalOperatorTestFetch(request(path,{method:'POST',body}),e)));
 assert.equal(a.filter(x=>x.status===201).length,1);assert.equal(a.filter(x=>x.status===200).length,9);
 assert.equal(db.raw.prepare('SELECT count(*) n FROM withdrawal_test_operator_events').get().n,1);
});

test('oversized and malformed operator input is rejected without writing audit records',async()=>{
 const db=database(),e=env(db),path=origin+'/operator/withdrawals/'+one+'/review',headers={Authorization:'Bearer '+token,Origin:origin,'Content-Type':'application/json'};
 for(const [body,status] of [['{',400],['x'.repeat(2049),413]]){
  const result=await withdrawalOperatorTestFetch(new Request(path,{method:'POST',headers,body}),e);assert.equal(result.status,status);
 }
 assert.equal(db.raw.prepare('SELECT count(*) n FROM withdrawal_test_operator_events').get().n,0);
});

test('a known test case with missing language metadata is flagged, not mistaken for a missing case',async()=>{
 const db=database(),e=env(db);db.raw.prepare('DELETE FROM withdrawal_test_languages WHERE request_id=?').run(one);
 const list=await(await withdrawalOperatorTestFetch(request('/operator/withdrawals'),e)).json();assert.equal(list.cases[0].metadata_state,'missing_language');
 const detail=await withdrawalOperatorTestFetch(request('/operator/withdrawals/'+one),e);assert.equal(detail.status,503);assert.doesNotMatch(await detail.text(),/Synthetic|owner@example/);
 const action=await withdrawalOperatorTestFetch(request('/operator/withdrawals/'+one+'/review',{method:'POST',body:{eventId,action:'escalated'}}),e);assert.equal(action.status,201);
 assert.equal((await withdrawalOperatorTestFetch(request('/operator/withdrawals/'+other),e)).status,404);
});
