import {test} from 'node:test';
import assert from 'node:assert/strict';
import {DatabaseSync} from 'node:sqlite';
import {readFileSync} from 'node:fs';
import {createHash} from 'node:crypto';
import {readTestSupplementDocuments,prepareTestSupplementStatements,loadTestFullOrderSnapshot,calculateTestFullSnapshotDigest} from '../confirmation-package-test.mjs';

const digest=x=>createHash('sha256').update(JSON.stringify(x)).digest('base64url');
const order='synthetic-order',at=1700000000000,language='de';
const settings={FULL_CONFIRMATION_TEST_ENABLED:'true',STRIPE_LIVE:'false',WITHDRAWAL_NOTICE_VERSION_DE:'notice-v1',WITHDRAWAL_FORM_VERSION_DE:'form-v1'};
function database(){
 const raw=new DatabaseSync(':memory:');
 for(const file of ['../schema.sql','../migrations/0016_test_confirmation_supplements.sql'])raw.exec(readFileSync(new URL(file,import.meta.url),'utf8'));
 return {raw,prepare(sql){return {bind(...args){return {async first(){return raw.prepare(sql).get(...args)||null;},async all(){return {results:raw.prepare(sql).all(...args)};},runSync(){return raw.prepare(sql).run(...args);}};}};},async batch(stmts){raw.exec('BEGIN');try{for(const s of stmts)s.runSync();raw.exec('COMMIT');}catch(e){raw.exec('ROLLBACK');throw e;}}};
}
function seedDocuments(db){
 for(const [version,text] of [['notice-v1','Synthetic notice original'],['form-v1','Synthetic form original'],['notice-v2','Synthetic notice changed'],['form-v2','Synthetic form changed']])
  db.raw.prepare('INSERT INTO contract_documents(language,version,text) VALUES(?,?,?)').run(language,version,text);
}
function baseStatements(db){
 const t={language,version:'terms-v1',text:'Synthetic terms'};
 const declarations=[{kind:'early_start',language,version:'early-v1',text:'Synthetic early'},{kind:'withdrawal_ack',language,version:'ack-v1',text:'Synthetic ack'}];
 return [db.prepare('INSERT INTO orders VALUES(?,?,?,?)').bind(order,'synthetic-claim',at,null),db.prepare('INSERT INTO contract_acceptances VALUES(?,?,?,?,?,?)').bind(order,language,t.version,t.text,digest(t),at),...declarations.map(d=>db.prepare('INSERT INTO contract_declarations VALUES(?,?,?,?,?,?,?)').bind(order,d.kind,d.language,d.version,d.text,digest(d),at))];
}
test('test checkout atomically stores exactly the two texts displayed, bound to one order',async()=>{
 const db=database();seedDocuments(db);
 const shown=await readTestSupplementDocuments(db,settings,language);
 assert.deepEqual(shown.map(x=>x.text),['Synthetic notice original','Synthetic form original']);
 const statements=await prepareTestSupplementStatements({db,env:settings,orderId:order,language,acceptedAt:at,displayed:shown.map(x=>x.digest)});
 await db.batch([...baseStatements(db),...statements]);
 const snapshot=await loadTestFullOrderSnapshot(db,settings,order,language);
 assert.deepEqual(snapshot.supplements.map(x=>x.text),['Synthetic notice original','Synthetic form original']);
 assert.match(snapshot.digest,/^[A-Za-z0-9_-]{43}$/);
 const originalDigests=[digest({language,version:'terms-v1',text:'Synthetic terms'}),digest({kind:'early_start',language,version:'early-v1',text:'Synthetic early'}),digest({kind:'withdrawal_ack',language,version:'ack-v1',text:'Synthetic ack'}),...shown.map(x=>x.digest)];
 assert.equal(await calculateTestFullSnapshotDigest(language,originalDigests),snapshot.digest);
 const altered=[...originalDigests];altered[4]=altered[3];assert.notEqual(await calculateTestFullSnapshotDigest(language,altered),snapshot.digest);
 await assert.rejects(calculateTestFullSnapshotDigest(language,originalDigests.slice(0,4)));
 await assert.rejects(calculateTestFullSnapshotDigest('en',[...originalDigests.slice(0,4),'bad']));
});
test('changed, missing, foreign and disabled test documents fail before any write',async()=>{
 const db=database();seedDocuments(db);const shown=await readTestSupplementDocuments(db,settings,language), displayed=shown.map(x=>x.digest);
 for(const x of [
  {...settings,WITHDRAWAL_NOTICE_VERSION_DE:'notice-v2'},
  {...settings,WITHDRAWAL_FORM_VERSION_DE:'missing'},
  {...settings,FULL_CONFIRMATION_TEST_ENABLED:'false'},
  {...settings,STRIPE_LIVE:'true'},
 ])await assert.rejects(prepareTestSupplementStatements({db,env:x,orderId:order,language,acceptedAt:at,displayed}));
 await assert.rejects(prepareTestSupplementStatements({db,env:settings,orderId:order,language:'en',acceptedAt:at,displayed}));
 await assert.rejects(prepareTestSupplementStatements({db,env:settings,orderId:order,language,acceptedAt:at,displayed:[displayed[1],displayed[0]]}));
 assert.equal(db.raw.prepare('SELECT count(*) n FROM orders').get().n,0);
});
test('failure inserting a second supplement rolls back order, declarations, and both supplements',async()=>{
 const db=database();seedDocuments(db);const shown=await readTestSupplementDocuments(db,settings,language);
 const statements=await prepareTestSupplementStatements({db,env:settings,orderId:order,language,acceptedAt:at,displayed:shown.map(x=>x.digest)});
 db.raw.exec("CREATE TRIGGER synthetic_fail_second BEFORE INSERT ON test_confirmation_supplements WHEN NEW.kind='withdrawal_form' BEGIN SELECT RAISE(ABORT,'synthetic failure'); END");
 await assert.rejects(db.batch([...baseStatements(db),...statements]));
 for(const table of ['orders','contract_acceptances','contract_declarations','test_confirmation_supplements'])assert.equal(db.raw.prepare('SELECT count(*) n FROM '+table).get().n,0);
});
test('resume loads historical texts and digest; missing or altered historical snapshots fail closed',async()=>{
 const db=database();seedDocuments(db);const shown=await readTestSupplementDocuments(db,settings,language);
 await db.batch([...baseStatements(db),...await prepareTestSupplementStatements({db,env:settings,orderId:order,language,acceptedAt:at,displayed:shown.map(x=>x.digest)})]);
 const before=await loadTestFullOrderSnapshot(db,settings,order,language);
 const rotated={...settings,WITHDRAWAL_NOTICE_VERSION_DE:'notice-v2',WITHDRAWAL_FORM_VERSION_DE:'form-v2'};
 assert.notDeepEqual((await readTestSupplementDocuments(db,rotated,language)).map(x=>x.text),before.supplements.map(x=>x.text));
 const after=await loadTestFullOrderSnapshot(db,rotated,order,language);
 assert.deepEqual(after,before);
 db.raw.exec('DROP TRIGGER test_confirmation_supplements_no_update');
 db.raw.prepare("UPDATE test_confirmation_supplements SET text='tampered' WHERE kind='withdrawal_form'").run();
 await assert.rejects(loadTestFullOrderSnapshot(db,rotated,order,language));
 await assert.rejects(loadTestFullOrderSnapshot(db,rotated,'unknown-order',language));
 await assert.rejects(loadTestFullOrderSnapshot(db,{...rotated,STRIPE_LIVE:'true'},order,language));
});
