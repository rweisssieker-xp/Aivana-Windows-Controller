import { test } from 'node:test';
import assert from 'node:assert/strict';
import { createHash } from 'node:crypto';
import { buildConfirmationEvidence } from '../confirmation-evidence.mjs';

// Independent encoding of the actual checkout snapshot contract.
const digest = value => createHash('sha256').update(JSON.stringify(value)).digest('base64url');
function fixture(language = 'de') {
  const orderId = 'synthetic-order';
  const terms = { language, version: 'terms-v1', text: 'Original terms\nSecond line' };
  const early = { kind: 'early_start', language, version: 'early-v1', text: 'Early start example' };
  const ack = { kind: 'withdrawal_ack', language, version: 'ack-v1', text: 'Acknowledgement example' };
  return {
    orderId, language,
    acceptance: { order_id: orderId, ...terms, digest: digest(terms), accepted_at: 1700000000000, claim_hash: 'must-not-escape' },
    declarations: [early, ack].map(d => ({ order_id: orderId, ...d, digest: digest(d), accepted_at: 1700000000000 })),
    recoveryCode: 'must-not-escape', payment_intent: 'must-not-escape',
  };
}

test('produces the original three validated texts for one German and English purchase without secrets', async () => {
  for (const language of ['de', 'en']) {
    const out = await buildConfirmationEvidence(fixture(language));
    assert.equal(out.orderId, 'synthetic-order');
    assert.equal(out.language, language);
    assert.equal(out.terms.text, 'Original terms\nSecond line');
    assert.equal(out.declarations.find(x => x.kind === 'early_start').text, 'Early start example');
    assert.equal(out.declarations.find(x => x.kind === 'withdrawal_ack').text, 'Acknowledgement example');
    assert.equal(out.terms.acceptedAt, 1700000000000);
    assert.ok(!JSON.stringify(out).includes('must-not-escape'));
  }
});

test('rejects missing, duplicated, foreign or differently localized declarations', async () => {
  for (const change of [
    x => x.declarations.pop(),
    x => x.declarations.push({...x.declarations[0]}),
    x => { x.declarations[1].kind = 'early_start'; },
    x => { x.declarations[0].order_id = 'another-order'; },
    x => { x.acceptance.order_id = 'another-order'; },
    x => { x.declarations[0].language = 'en'; },
  ]) {
    const input = fixture(); change(input);
    await assert.rejects(buildConfirmationEvidence(input));
  }
});

test('rejects tampered text, invalid metadata and missing acceptance time', async () => {
  for (const change of [
    x => { x.acceptance.text += ' changed'; },
    x => { x.declarations[0].digest = 'bogus'; },
    x => { x.acceptance.version = 'bad\nheader'; },
    x => { x.declarations[0].text = ''; },
    x => { x.acceptance.accepted_at = 0; },
    x => { x.declarations[1].accepted_at = Number.NaN; },
    x => { x.language = 'fr'; },
  ]) {
    const input = fixture(); change(input);
    await assert.rejects(buildConfirmationEvidence(input));
  }
});

test('rejects unsafe metadata even when its digest is internally consistent', async () => {
  const input = fixture();
  input.acceptance.version = 'bad\nheader';
  input.acceptance.digest = digest({ language: 'de', version: input.acceptance.version, text: input.acceptance.text });
  await assert.rejects(buildConfirmationEvidence(input));
});

test('rejects declarations submitted at a different time or with an unknown kind', async () => {
  const input = fixture();
  input.declarations[0].accepted_at++;
  await assert.rejects(buildConfirmationEvidence(input));
  const other = fixture();
  other.declarations[0].kind = 'unknown';
  await assert.rejects(buildConfirmationEvidence(other));
});
