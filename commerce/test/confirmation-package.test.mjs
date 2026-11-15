import {test} from 'node:test';
import assert from 'node:assert/strict';
import {createHash} from 'node:crypto';
import {DatabaseSync} from 'node:sqlite';
import {readFileSync} from 'node:fs';
import {buildTestConfirmationPackage} from '../confirmation-package-test.mjs';

const digest=x=>createHash('sha256').update(JSON.stringify(x)).digest('base64url');
function fixture(language='de'){
 const orderId='synthetic-order', at=1700000000000;
 const terms={language,version:'terms-v1',text:'Synthetic original terms'};
 const declaration=(kind,version,text)=>{const doc={kind,language,version,text};return {order_id:orderId,...doc,digest:digest(doc),accepted_at:at};};
 const supplement=(kind,version,text)=>{const doc={kind,language,version,text};return {order_id:orderId,...doc,digest:digest(doc),captured_at:at};};
 return {orderId,language,acceptance:{order_id:orderId,...terms,digest:digest(terms),accepted_at:at},declarations:[declaration('early_start','early-v1','Synthetic early'),declaration('withdrawal_ack','ack-v1','Synthetic acknowledgment')],supplements:[supplement('withdrawal_notice','notice-v1','Synthetic notice'),supplement('withdrawal_form','form-v1','Synthetic form')],recoveryCode:'do-not-output'};
}

test('package preserves original order evidence plus exactly its own notice and form in German and English',async()=>{
 for(const language of ['de','en']){
  const result=await buildTestConfirmationPackage(fixture(language));
  assert.equal(result.language,language);assert.equal(result.terms.text,'Synthetic original terms');
  assert.deepEqual(result.declarations.map(x=>x.kind),['early_start','withdrawal_ack']);
  assert.deepEqual(result.supplements.map(x=>x.kind),['withdrawal_notice','withdrawal_form']);
  assert.deepEqual(result.supplements.map(x=>x.text),['Synthetic notice','Synthetic form']);
  assert.ok(!JSON.stringify(result).includes('do-not-output'));
 }
});

test('missing, duplicate, foreign, later, different-language, changed or unknown supplemental documents fail closed',async()=>{
 for(const change of [
  x=>x.supplements.pop(),x=>x.supplements.push({...x.supplements[0]}),x=>{x.supplements[0].order_id='other-order';},
  x=>{x.supplements[0].language=x.language==='de'?'en':'de';},x=>{x.supplements[0].captured_at++;},
  x=>{x.supplements[0].text='tampered';},x=>{x.supplements[0].kind='unknown';},x=>{x.supplements[0].version='bad\nversion';},
  x=>{x.supplements[0].text='x'.repeat(20001);},x=>{x.acceptance.text='changed';},
 ]){const input=fixture();change(input);await assert.rejects(buildTestConfirmationPackage(input));}
});

test('isolated migration preserves existing orders and protects each exact supplement snapshot',()=>{
 const db=new DatabaseSync(':memory:');
 db.exec(readFileSync(new URL('../schema.sql',import.meta.url),'utf8'));
 const f=fixture();
 db.prepare('INSERT INTO orders VALUES(?,?,?,?)').run(f.orderId,'synthetic-hash',1,null);
 const a=f.acceptance;
 db.prepare('INSERT INTO contract_acceptances VALUES(?,?,?,?,?,?)').run(a.order_id,a.language,a.version,a.text,a.digest,a.accepted_at);
 db.exec(readFileSync(new URL('../migrations/0016_test_confirmation_supplements.sql',import.meta.url),'utf8'));
 assert.equal(db.prepare('SELECT COUNT(*) n FROM test_confirmation_supplements').get().n,0);
 const insert=db.prepare('INSERT INTO test_confirmation_supplements(order_id,kind,language,version,text,digest,captured_at) VALUES(?,?,?,?,?,?,?)');
 for(const s of f.supplements) insert.run(s.order_id,s.kind,s.language,s.version,s.text,s.digest,s.captured_at);
 assert.throws(()=>insert.run(f.orderId,'withdrawal_notice','de','v2','changed','digest',a.accepted_at));
 assert.throws(()=>db.prepare("INSERT OR REPLACE INTO test_confirmation_supplements VALUES(?,?,?,?,?,?,?)").run(f.orderId,'withdrawal_notice','de','v2','changed','digest',a.accepted_at));
 assert.throws(()=>db.prepare("UPDATE test_confirmation_supplements SET text='changed'").run());
 assert.throws(()=>db.prepare('DELETE FROM test_confirmation_supplements').run());
 assert.equal(db.prepare('SELECT COUNT(*) n FROM test_confirmation_supplements').get().n,2);
 assert.equal(db.prepare('SELECT text FROM contract_acceptances WHERE order_id=?').get(f.orderId).text,'Synthetic original terms');
});

test('supplement migration rejects wrong order, language or capture time',()=>{
 const db=new DatabaseSync(':memory:');db.exec(readFileSync(new URL('../schema.sql',import.meta.url),'utf8'));
 const f=fixture(),a=f.acceptance;
 db.prepare('INSERT INTO orders VALUES(?,?,?,?)').run(f.orderId,'synthetic-hash',1,null);
 db.prepare('INSERT INTO contract_acceptances VALUES(?,?,?,?,?,?)').run(a.order_id,a.language,a.version,a.text,a.digest,a.accepted_at);
 db.exec(readFileSync(new URL('../migrations/0016_test_confirmation_supplements.sql',import.meta.url),'utf8'));
 const insert=db.prepare('INSERT INTO test_confirmation_supplements VALUES(?,?,?,?,?,?,?)');const s=f.supplements[0];
 assert.throws(()=>insert.run('foreign',s.kind,s.language,s.version,s.text,s.digest,s.captured_at));
 assert.throws(()=>insert.run(s.order_id,s.kind,'en',s.version,s.text,s.digest,s.captured_at));
 assert.throws(()=>insert.run(s.order_id,s.kind,s.language,s.version,s.text,s.digest,s.captured_at+1));
 assert.equal(db.prepare('SELECT COUNT(*) n FROM test_confirmation_supplements').get().n,0);
});
