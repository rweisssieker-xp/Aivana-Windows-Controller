import test from 'node:test';
import assert from 'node:assert/strict';
import { checkLaunch, liveAccountId } from '../stripe-launch-check.mjs';

const account = {
  id: liveAccountId, country: 'DE', business_type: 'company', company: { name: 'Aivana GmbH' },
  details_submitted: true, charges_enabled: true, payouts_enabled: true,
  requirements: { currently_due: [], past_due: [], pending_verification: [], disabled_reason: null },
};
const price = { id: 'price_testfixture', product: 'prod_testfixture', livemode: true, active: true,
  type: 'one_time', recurring: null, currency: 'eur', unit_amount: 2900, tax_behavior: 'inclusive' };

test('accepts only the separate German GmbH and the specified live one-time EUR price', () => {
  assert.deepEqual(checkLaunch(account, price, price.id, price.product), []);
  assert.deepEqual(checkLaunch({ ...account, id: 'acct_other', business_type: 'individual' }, price, price.id, price.product), ['wrong_company_account']);
});

test('incomplete verification and test or recurring prices are flagged separately', () => {
  const incomplete = { ...account, payouts_enabled: false, requirements: { ...account.requirements, pending_verification: ['private-field'] } };
  assert.deepEqual(checkLaunch(incomplete, { ...price, livemode: false, recurring: { interval: 'month' } }, price.id, price.product), ['account_activation_pending', 'price_mismatch']);
  assert.deepEqual(checkLaunch(account, { ...price, product: 'prod_other' }, price.id, price.product), ['price_mismatch']);
});
