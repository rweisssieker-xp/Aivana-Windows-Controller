import {test} from 'node:test';
import assert from 'node:assert/strict';
import {DatabaseSync} from 'node:sqlite';
import {readFileSync} from 'node:fs';
import worker from '../worker.mjs';

function database(){
  const raw=new DatabaseSync(':memory:');
  for(const name of ['../schema.sql','../migrations/0010_withdrawal.sql','../migrations/0013_test_mail_attempts.sql'])raw.exec(readFileSync(new URL(name,import.meta.url),'utf8'));
  return {raw,prepare(sql){return {bind(...args){const s=raw.prepare(sql);return {async first(){return s.get(...args)||null},async all(){return {results:s.all(...args)}},async run(){return s.run(...args)}}}}}};
}

test('scheduler reports an old test receipt that cannot start because the sender is not configured',async()=>{
  const db=database();
  db.raw.prepare('INSERT INTO withdrawal_requests VALUES(?,?,?,?,?)').run('job-1','Synthetic','Synthetic','owner@example.test',Date.now()-16*60*1000);
  db.raw.prepare("INSERT INTO withdrawal_receipts VALUES('job-1','pending')").run();
  await assert.rejects(worker.scheduled({}, {DB:db,MAIL_DELIVERY_TEST_ENABLED:'true',STRIPE_LIVE:'false'}, {}),/Test mail requires operator review/);
  assert.equal(db.raw.prepare('SELECT COUNT(*) AS n FROM mail_dispatch_attempts').get().n,0);
});

test('unclaimed test monitoring is gated off in disabled and live modes',async()=>{
  const db=database();
  db.raw.prepare('INSERT INTO withdrawal_requests VALUES(?,?,?,?,?)').run('job-1','Synthetic','Synthetic','owner@example.test',Date.now()-16*60*1000);
  db.raw.prepare("INSERT INTO withdrawal_receipts VALUES('job-1','pending')").run();
  await worker.scheduled({}, {DB:db,MAIL_DELIVERY_TEST_ENABLED:'false',STRIPE_LIVE:'false'}, {});
  await worker.scheduled({}, {DB:db,MAIL_DELIVERY_TEST_ENABLED:'true',STRIPE_LIVE:'true'}, {});
});
