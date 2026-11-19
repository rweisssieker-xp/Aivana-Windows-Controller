import {test} from 'node:test';
import assert from 'node:assert/strict';
import {DatabaseSync} from 'node:sqlite';
import {readFileSync} from 'node:fs';
import {withdrawalTestFetch} from '../withdrawal.mjs';

function database(){
 const raw=new DatabaseSync(':memory:');
 raw.exec(readFileSync(new URL('../migrations/0010_withdrawal.sql',import.meta.url),'utf8'));
 raw.exec(readFileSync(new URL('../migrations/0015_test_withdrawal_rate_limits.sql',import.meta.url),'utf8'));
 return {raw,async batch(statements){raw.exec('BEGIN IMMEDIATE');try{const v=statements.map(s=>s._run());raw.exec('COMMIT');return v;}catch(e){raw.exec('ROLLBACK');throw e;}},prepare(sql){return {bind(...v){const q=raw.prepare(sql);return {_run:()=>q.run(...v),run:async()=>q.run(...v),first:async()=>q.get(...v)||null,all:async()=>({results:q.all(...v)})};}};}};
}
const origin='https://test.invalid';
const env=db=>({DB:db,PUBLIC_ORIGIN:origin,WITHDRAWAL_TEST_ONLY:'true',STRIPE_LIVE:'false',WITHDRAWAL_TEST_RATE_LIMIT_ENABLED:'true',WITHDRAWAL_TEST_TRUST_PROXY:'true',WITHDRAWAL_TEST_RATE_LIMIT_SECRET:'12345678901234567890123456789012'});
function post(i,email='victim@example.test',ip='203.0.113.2'){
 const id=`00000000-0000-4000-8000-${String(i).padStart(12,'0')}`;
 return new Request(origin+'/withdrawal',{method:'POST',headers:{Origin:origin,'Content-Type':'application/json','CF-Connecting-IP':ip},body:JSON.stringify({requestId:id,name:'Test',contract:'Test contract',email,confirmed:true})});
}

test('test protection limits repeated recipient requests even with changed source and stores no address or source',async()=>{
 const db=database(),e=env(db);
 for(let i=1;i<=3;i++)assert.equal((await withdrawalTestFetch(post(i,'victim@example.test',`203.0.113.${i}`),e)).status,202);
 const blocked=await withdrawalTestFetch(post(4,'victim@example.test','203.0.113.4'),e);
 assert.equal(blocked.status,429);assert.ok(Number(blocked.headers.get('Retry-After'))>0);
 assert.equal(db.raw.prepare('SELECT count(*) n FROM withdrawal_requests').get().n,3);
 const buckets=JSON.stringify(db.raw.prepare('SELECT * FROM withdrawal_test_rate_limits').all());
 assert.doesNotMatch(buckets,/victim@example\.test|203\.0\.113/);
});

test('test protection limits one source across distinct recipient addresses',async()=>{
 const db=database(),e=env(db);
 for(let i=1;i<=10;i++)assert.equal((await withdrawalTestFetch(post(i,`test${i}@example.test`),e)).status,202);
 assert.equal((await withdrawalTestFetch(post(11,'test11@example.test'),e)).status,429);
 assert.equal(db.raw.prepare('SELECT count(*) n FROM withdrawal_requests').get().n,10);
});

test('enabled protection fails closed without secret, trusted proxy identity or storage',async()=>{
 const db=database(),e=env(db);
 for(const changed of [{WITHDRAWAL_TEST_RATE_LIMIT_SECRET:''},{WITHDRAWAL_TEST_TRUST_PROXY:'false'}])assert.equal((await withdrawalTestFetch(post(1),{...e,...changed})).status,503);
 const request=post(1);request.headers.delete('CF-Connecting-IP');assert.equal((await withdrawalTestFetch(request,e)).status,503);
 db.raw.exec('DROP TABLE withdrawal_test_rate_limits');assert.equal((await withdrawalTestFetch(post(1),e)).status,503);
 assert.equal(db.raw.prepare('SELECT count(*) n FROM withdrawal_requests').get().n,0);
});

test('previously stored identical declaration can be retried after the request limit without another receipt',async()=>{
 const db=database(),e=env(db),first=await withdrawalTestFetch(post(1),e),saved=await first.json();assert.equal(first.status,202);
 for(let i=2;i<=3;i++)assert.equal((await withdrawalTestFetch(post(i),e)).status,202);
 assert.equal((await withdrawalTestFetch(post(4),e)).status,429);
 const replay=await withdrawalTestFetch(post(1),e);assert.equal(replay.status,202);assert.deepEqual(await replay.json(),saved);
 assert.equal(db.raw.prepare('SELECT count(*) n FROM withdrawal_requests').get().n,3);
 assert.equal(db.raw.prepare('SELECT count(*) n FROM withdrawal_receipts').get().n,3);
});

test('blocked test requests provide an alternate contact and do not claim that the declaration was received',async()=>{
 const db=database(),e=env(db);
 for(let i=1;i<=3;i++)await withdrawalTestFetch(post(i),e);
 const blocked=await withdrawalTestFetch(post(4),e);const body=await blocked.text();
 assert.match(body,/info@aivana-gmbh\.ai/);assert.doesNotMatch(body,/receipt_pending|gespeichert/);
 const page=await (await withdrawalTestFetch(new Request(origin+'/withdrawal'),e)).text();assert.match(page,/info@aivana-gmbh\.ai/);
});

test('parallel requests for the same recipient cannot bypass the test limit',async()=>{
 const db=database(),e=env(db);
 const results=await Promise.all(Array.from({length:20},(_,i)=>withdrawalTestFetch(post(i+1,'victim@example.test',`203.0.113.${i+1}`),e)));
 assert.equal(results.filter(x=>x.status===202).length,3);assert.equal(results.filter(x=>x.status===429).length,17);
 assert.equal(db.raw.prepare('SELECT count(*) n FROM withdrawal_requests').get().n,3);
});

test('a different declaration with a stored identifier does not bypass recipient throttling',async()=>{
 const db=database(),e=env(db);
 for(let i=1;i<=3;i++)await withdrawalTestFetch(post(i),e);
 const changed=post(1,'other@example.test');
 assert.equal((await withdrawalTestFetch(changed,e)).status,409);
 assert.equal(db.raw.prepare('SELECT count(*) n FROM withdrawal_requests').get().n,3);
});

test('storage failure provides an alternate contact without claiming a successful receipt',async()=>{
 const db=database(),e=env(db);db.raw.exec('DROP TABLE withdrawal_test_rate_limits');
 const failed=await withdrawalTestFetch(post(1),e),body=await failed.text();
 assert.equal(failed.status,503);assert.match(body,/info@aivana-gmbh\.ai/);assert.doesNotMatch(body,/receipt_pending|gespeichert/);
 assert.equal(db.raw.prepare('SELECT count(*) n FROM withdrawal_requests').get().n,0);
});
