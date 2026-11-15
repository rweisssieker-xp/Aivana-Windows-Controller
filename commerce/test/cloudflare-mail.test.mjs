import { test } from 'node:test';
import assert from 'node:assert/strict';
import { sendPreparedEmail } from '../cloudflare-mail.mjs';

const input = () => ({ sender:'sales@example.test', recipient:'buyer@example.test', subject:'Test confirmation', text:'Synthetic test content' });

test('passes one plain-text message to the Cloudflare binding and reports acceptance only', async () => {
  const calls=[];
  const binding={send:async message=>{calls.push(message);return {messageId:'cf-test-id'};}};
  const result=await sendPreparedEmail(binding,input());
  assert.deepEqual(calls,[{from:'sales@example.test',to:'buyer@example.test',subject:'Test confirmation',text:'Synthetic test content'}]);
  assert.deepEqual(result,{status:'accepted_by_provider',messageId:'cf-test-id'});
});

test('rejects malformed addresses, control characters and oversized messages before invoking provider', async () => {
  for (const patch of [
    {sender:'sales@example.test\r\nBcc: other@example.test'},
    {recipient:'buyer@example.test,other@example.test'},
    {recipient:'buyer@example.test\n'},
    {subject:'ok\r\nBcc: other@example.test'},
    {subject:''},
    {text:''},
    {text:'x'.repeat(200001)},
  ]) {
    let sent=false;
    await assert.rejects(sendPreparedEmail({send:async()=>{sent=true;}},{...input(),...patch}));
    assert.equal(sent,false);
  }
});

test('provider errors and invalid responses yield an uncertain outcome without leaking details or retrying', async () => {
  for (const reply of [()=>{throw new Error('private buyer@example.test');},()=>({}),()=>({messageId:'bad\nid'})]) {
    let count=0;
    const result=await sendPreparedEmail({send:async()=>{count++;return reply();}},input());
    assert.deepEqual(result,{status:'outcome_unknown'});
    assert.equal(count,1);
  }
});
