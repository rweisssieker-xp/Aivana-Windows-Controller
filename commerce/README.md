# MAKRIVO commerce — deployment disabled

Standalone Cloudflare Worker/D1. Seller Aivana GmbH, https://www.aivana-gmbh.ai. €29 gross one-time, one person/four own PCs. Perpetual major version 1. No external website was changed. No live deployment, secret or production signing was provisioned.

Run `npm test` with Node 25. Tests require no packages and use actual SQLite and WebCrypto. Local tests do not replace Stripe/Cloudflare staging acceptance.

## Operator setup

Install/configure Wrangler separately. Create a staging DB first:

```
npx wrangler d1 create makrivo-commerce
npx wrangler d1 execute makrivo-commerce --remote --file schema.sql
npx wrangler secret put STRIPE_SECRET_KEY
npx wrangler secret put STRIPE_WEBHOOK_SECRET
npx wrangler secret put LICENSE_PRIVATE_KEY_PEM
npx wrangler deploy
```

Replace database ID and exact HTTPS PUBLIC_ORIGIN (no trailing slash). Supply fixed Stripe Price/product IDs, key ID and reviewed HTTPS terms/privacy/imprint URLs. Only after staging and legal acceptance set SALES_ENABLED=true and LEGAL_APPROVED=true. workers.dev stays disabled; configure operator domain separately. Production requires live keys and STRIPE_LIVE=true. Isolate test and live databases/keys.

Stripe Price must be active, one_time, EUR 2900 and tax_behavior=inclusive for the matching product. Configure applicable Stripe Tax registrations; automatic tax must retain the exact 2900 total. No discounts, subscriptions or adjustable quantities. RSA key must be at least 3072 bits and PKCS8 private PEM, stored only in Worker secrets. Key ID must be 1–64 ASCII letters, digits, dot, underscore or hyphen. Invalid/private-key-type/undersized signer configuration fails readiness before checkout, even if payment configuration exists. Embed corresponding public RSA PEM under LICENSE_KEY_ID in the Windows public key ring. Back up keys securely and preserve old verifier keys.

Subscribe webhook to checkout.session.completed, checkout.session.async_payment_succeeded, checkout.session.async_payment_failed, charge.refunded and charge.dispute.created. Failed/unpaid async checkout cannot fulfill. Refunds including partial refunds and disputes revoke subsequent issuance; existing offline licenses cannot be killed. Payment tombstones prevent delayed delivery from restoring revoked issuance. Retry webhook failures.

## API and claim security

POST /api/checkout `{ "acceptTerms": true }` creates 256-bit random claim, stores SHA256 only and sets Secure HttpOnly SameSite=Lax cookie. Response contains the hosted Stripe URL and a one-time displayed recovery code for this newly created buyer order. The page requires saving that secret privately before redirect. Price livemode and the created session mode/livemode/HTTPS checkout.stripe.com host are checked before exposing the URL. GET /api/order reports status and devices. POST /api/licenses/activate and /deactivate accept `{ "deviceId": "64 lowercase hex" }`. Cookie or Authorization Bearer claim required. Every buyer POST requires exact configured Origin. Public Stripe session IDs provide no authorization. Claims must never enter URLs, analytics, logs or support tickets.

Buyer page supports returning in same browser, manual device-code activation and license JSON download/import. Save the displayed recovery code privately before payment. POST /api/claim/restore with JSON recoveryCode and exact Origin validates the code against the stored hash, then sets the HttpOnly cookie. The Restore purchase form supports another browser or an overwritten cookie. Recovery code is a bearer credential: anyone holding it can manage the purchase. Without cookie or saved code, separately authenticated operator recovery is still required (not implemented). No email recovery is claimed.

One SQL statement atomically enforces four active device seats; retries are idempotent. Deactivation releases issuance seat but cannot invalidate an already issued offline license. No subscription or runtime expiry.

Rate limit: 30 buyer API requests per client IP/minute. The request IP is processed in Worker memory to compute SHA256 of product scope + UTC minute + IP. D1 stores only the base64url digest, hit count and expires_at (epoch milliseconds), not the raw IP. These hashes are pseudonymous, not anonymous: someone with a candidate IP and minute can test the digest. Missing Cloudflare IP headers share an unknown-client bucket. Never log cookies, Authorization, IPs, signing secrets or request bodies. Configure Cloudflare WAF/rate limits for checkout/webhook separately before launch; Cloudflare network/access logging and its retention need their own operator review.

## Acceptance still required

Actual Stripe hosted checkout/async success/failure; duplicate/reordered/refunded/disputed webhooks; copied session-ID attacker; simultaneous D1 activation; browser return/cookie handling; tax geography; Windows verification using matching public key; legally reviewed digital-content consent/withdrawal; monitoring, operator-assisted recovery and retention. Current checkbox is not legal approval. Local RSA tests verify exact bytes in WebCrypto; Cloudflare-to-Windows production interoperability remains a staging check.

Primary references: https://docs.stripe.com/checkout/fulfillment and https://developers.cloudflare.com/d1/worker-api/d1-database/ . D1 batches are atomic transactions.

## Pausing new sales

SALES_ENABLED and legal/signer readiness gate only new Checkout. Pausing sales does not stop payment webhooks, existing order access, saved-code recovery or deactivation. Webhooks require the database, Stripe provider/webhook credentials, product/price IDs and explicit STRIPE_LIVE=true/false; they never depend on the private signer. Existing entitled activation requires a valid signer but remains available while new sales are paused. Keep provider/webhook credentials and database online to process pending payments, refunds and disputes.

The buyer page lists active device codes and available seats after Refresh payment and PCs or activation. Each own-PC entry offers Deactivate this PC with a confirmation explaining local offline-file removal. Successful deactivation refreshes the list and seat count. Deactivation is idempotent and requires the same authenticated purchase claim.

## Rate-limit retention and existing-install migration

Fresh databases use schema.sql, where rate_limits.expires_at is NOT NULL and indexed. Existing installations with the old two-column table must apply the following exactly once BEFORE deploying this Worker (inspect PRAGMA table_info(rate_limits) to distinguish them):

```
npx wrangler d1 execute makrivo-commerce --remote --file migrations/0002_rate_limit_retention.sql
```

Do not apply that migration to a fresh schema.sql database or repeat it: ALTER TABLE adds the column once. The migration adds a nullable expiry column without modifying commerce records; NULL marks legacy buckets, including any old raw-IP values, for deletion by the first buyer cleanup or scheduled invocation. Arrange the Worker update and first successful cleanup in the same maintenance window; verify no NULL expiry rows remain. Back up and verify the selected environment/database before the operator runs remote migrations. No migration/deployment was executed during implementation.

Each new bucket expires at the end of its UTC minute plus a one-minute buffer: logical lifetime is 60–120 seconds from insertion. Eligible buyer API requests delete only NULL-expiry or expired counters before incrementing the current-minute bucket. Atomic SQL increments preserve the 30/minute limit; hashing includes the minute so a new minute gets a separate counter. Cleanup never deletes based on hit count or a partial bucket-name match.

The Worker also has an awaited scheduled handler; wrangler.jsonc configures `*/5 * * * *`. Cleanup uses actual execution Date.now(), never a future scheduled-event timestamp. It runs independently of sales/legal/signing/provider readiness, needs only the D1 binding, and touches only rate_limits. Under normal successful five-minute invocation, physical retention is at most approximately seven minutes from bucket creation (120 seconds to expiry plus up to five minutes to cleanup); buyer traffic can shorten this. This is an intended operational bound, not a guarantee during outage, paused deployment or cron failure. Monitor failed invocations and database cleanup; rows persist until a successful cleanup. No orders, entitlements, devices, license payloads, events or refund tombstones are deleted by this job; their retention remains a separate legal/operator policy.

Cron deployment and remote D1 verification remain pending. After the operator deploys, verify the five-minute trigger and successful scheduled execution, expired/NULL row removal and active-row preservation against staging D1; test cron locally via Wrangler's scheduled-handler endpoint. Local automated tests invoke the actual exported scheduled handler against SQLite; they do not prove Cloudflare delivery or timing. Primary references checked 2026-10-26: [scheduled handler](https://developers.cloudflare.com/workers/runtime-apis/handlers/scheduled/) and [Cron Triggers configuration](https://developers.cloudflare.com/workers/configuration/cron-triggers/).
