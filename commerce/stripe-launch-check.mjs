import { pathToFileURL } from 'node:url';

export const liveAccountId = 'acct_1UZq8V2fg26yhZUg';

export function checkLaunch(account, price, priceId, productId) {
  const issues = [];
  if (account?.id !== liveAccountId || account.country !== 'DE' || account.business_type !== 'company' ||
      account.company?.name?.trim().toLowerCase() !== 'aivana gmbh') {
    issues.push('wrong_company_account');
  }
  const requirements = account?.requirements;
  if (account?.details_submitted !== true || account.charges_enabled !== true || account.payouts_enabled !== true ||
      requirements?.disabled_reason !== null || !['currently_due', 'past_due', 'pending_verification'].every(field =>
        Array.isArray(requirements[field]) && requirements[field].length === 0)) {
    issues.push('account_activation_pending');
  }
  if (price?.id !== priceId || price?.product !== productId || price?.livemode !== true || price.active !== true ||
      price.type !== 'one_time' || price.recurring !== null || price.currency !== 'eur' ||
      price.unit_amount !== 2900 || price.tax_behavior !== 'inclusive') {
    issues.push('price_mismatch');
  }
  return issues;
}

async function main() {
  const [priceId, productId, ...extra] = process.argv.slice(2);
  const key = process.env.MAKRIVO_STRIPE_CHECK_KEY;
  if (!/^price_[A-Za-z0-9]+$/.test(priceId ?? '') || !/^prod_[A-Za-z0-9]+$/.test(productId ?? '') ||
      extra.length || !/^rk_live_[A-Za-z0-9]+$/.test(key ?? '')) {
    console.error('Provide the live price and product IDs and inject a restricted live read key from the secret store.');
    return 2;
  }
  const get = async path => {
    const response = await fetch(`https://api.stripe.com/v1/${path}`, {
      headers: { Authorization: `Bearer ${key}`, 'Stripe-Version': '2026-10-28.endive' },
      redirect: 'error',
      signal: AbortSignal.timeout(15000),
    });
    if (!response.ok) throw new Error('Stripe read failed');
    return response.json();
  };
  try {
    // The unqualified account endpoint identifies the account of the key itself.
    const [account, price] = await Promise.all([get('account'), get(`prices/${priceId}`)]);
    const issues = checkLaunch(account, price, priceId, productId);
    console.log(JSON.stringify({ accountAndPriceVerified: issues.length === 0, issues }));
    return issues.length ? 1 : 0;
  } catch {
    console.error('Could not verify Stripe. Check live read permissions and network access.');
    return 2;
  }
}

if (process.argv[1] && import.meta.url === pathToFileURL(process.argv[1]).href) process.exitCode = await main();
