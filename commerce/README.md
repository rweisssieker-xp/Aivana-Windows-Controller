# MAKRIVO commerce — deployment disabled

Standalone Cloudflare Worker/D1. Seller Aivana GmbH, https://www.aivana-gmbh.ai. €29 gross one-time, one person/four own PCs. Perpetual major version 1. No external website was changed. A disabled deployment without public routes was provisioned on 2026-10-26; see DEPLOYMENT-STATUS.md. No Stripe/signing secret or production sales approval was provisioned.

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

Replace database ID and exact HTTPS PUBLIC_ORIGIN (no trailing slash). Supply fixed Stripe Price/product IDs, key ID and reviewed HTTPS terms/privacy/imprint URLs. An isolated test also needs the versioned German and English purchase terms and separate digital-content declarations described in `DIGITAL-CONSENT.review.md`. Use test-mode credentials and authorized test data. workers.dev stays disabled; configure an operator domain separately when appropriate. Isolate test and live databases/keys.

**Production remains blocked in code:** `ready()` rejects `STRIPE_LIVE=true` even when `SALES_ENABLED` and `LEGAL_APPROVED` are true. In addition, orders with a confirmation obligation cannot receive an offline license: there is no implemented way to send a complete contract confirmation or mark delivery. Do not remove either guard or configure a public sales route merely by supplying flags or credentials. Implement and independently verify delivery, the required legal and operating processes and end-to-end acceptance first; see `CONFIRMATION-QUEUE.review.md` and `DEPLOYMENT-STATUS.md`.

Stripe Price must be active, one_time, EUR 2900 and tax_behavior=inclusive for the matching product. Configure applicable Stripe Tax registrations; automatic tax must retain the exact 2900 total. No discounts, subscriptions or adjustable quantities. RSA key must be at least 3072 bits and PKCS8 private PEM, stored only in Worker secrets. Key ID must be 1–64 ASCII letters, digits, dot, underscore or hyphen. Invalid/private-key-type/undersized signer configuration fails readiness before checkout, even if payment configuration exists. Embed corresponding public RSA PEM under LICENSE_KEY_ID in the Windows public key ring. Back up keys securely and preserve old verifier keys.

Subscribe webhook to checkout.session.completed, checkout.session.async_payment_succeeded, checkout.session.async_payment_failed, charge.refunded and charge.dispute.created. Failed/unpaid async checkout cannot fulfill. Refunds including partial refunds and disputes revoke subsequent issuance; existing offline licenses cannot be killed. Payment tombstones prevent delayed delivery from restoring revoked issuance. Retry webhook failures.

## API and claim security

`POST /api/checkout/prepare` requires the selected language, explicit `acceptTerms`, `acceptEarlyStart` and `acknowledgeWithdrawal` values, and the digests of all three displayed document versions. It atomically saves the order and evidence and returns a random recovery code and checkout authorization token without creating a Stripe session. The buyer must save the recovery code privately before continuing; only its SHA-256 hash is stored. `POST /api/checkout` requires the recovery code, terms acceptance and matching unexpired checkout token, and creates or resumes the same Stripe session using an order-specific idempotency key. `POST /api/checkout/preview` uses the recovery code to return the original stored versions and a rotated token for buyer review before resuming. Token hashes can remain after expiry until overwritten; retention is unresolved. The service retrieves the current session from Stripe before exposing an open unpaid hosted URL; a public Stripe session ID never authorizes access. The response does not set or replace an existing claim cookie. After payment, the buyer uses the saved code in Restore purchase to set the Secure HttpOnly SameSite=Lax cookie. POST /api/claim/restore requires exact Origin, as do all buyer POST requests. GET /api/order reports status and devices. POST /api/licenses/activate and /deactivate accept `{ "deviceId": "64 lowercase hex" }`. Claims and checkout tokens must never enter URLs, analytics, logs or support tickets.

`POST /api/order/export` requires the exact configured Origin and the same authenticated purchase claim. A user-triggered button fetches the JSON and initiates the download without exposing the HttpOnly cookie to JavaScript. A cross-origin request or top-level GET cannot trigger an export. Selected records for that order only include creation time, entitlement status, accepted terms and active/inactive device codes. The export excludes the claim and its hash, Stripe session and payment identifiers, and all other orders. Responses are JSON attachments with `no-store` and `nosniff`. More than 1,000 device records returns an error instead of a silently truncated file. This is not a complete data-subject access workflow: Stripe, hosting/logs, support, accounting and backups are not included, identity verification without the secret is not implemented, and the operator must still handle access and deletion requests. The endpoint and its output require staging/privacy review before use in a live service.

If the page reloads or the response is lost, the buyer can resume payment using the saved code. If a session is recorded, its current status is retrieved; an open unpaid session can be resumed, and a completed or closed session is not recreated. Stripe may discard an idempotency key after 24 hours: a session whose creation outcome remains unknown is never automatically recreated after the conservative 20-hour cutoff and requires operator reconciliation. Do not tell the buyer to create a new order until the previous Stripe outcome has been checked. A Stripe error may be cached and also require support. Operator recovery and reconciliation are not implemented or verified. A code lost before payment leaves an unused order; a code lost after payment requires separately authenticated operator recovery (not implemented). The code is a bearer credential; never share it.

The buyer page supports German and English. Explicit `?lang=de` or `?lang=en` overrides browser preference; English is fallback. The selected language is retained for the order's Stripe return URLs, even on retry. Language links are blocked during requests and while the initial recovery code is displayed. Product wording does not constitute legal approval of terms, withdrawal information or tax setup. Stripe Checkout is hosted separately.

Rate limit: 30 buyer API requests per client IP/minute. The request IP is processed in Worker memory to compute SHA256 of product scope + UTC minute + IP. D1 stores only the base64url digest, hit count and expires_at (epoch milliseconds), not the raw IP. These hashes are pseudonymous, not anonymous: someone with a candidate IP and minute can test the digest. Missing Cloudflare IP headers share an unknown-client bucket. Never log cookies, Authorization, IPs, signing secrets or request bodies. Configure Cloudflare WAF/rate limits for checkout/webhook separately before launch; Cloudflare network/access logging and its retention need their own operator review.

## Acceptance still required

Actual Stripe hosted checkout/async success/failure; duplicate/reordered/refunded/disputed webhooks; copied session-ID attacker; simultaneous D1 activation; browser return/cookie handling; tax geography; Windows verification using matching public key; legally reviewed digital-content consent/withdrawal; monitoring, operator-assisted recovery and retention. Current checkbox is not legal approval. Local RSA tests verify exact bytes in WebCrypto; Cloudflare-to-Windows production interoperability remains a staging check.

Primary references: https://docs.stripe.com/checkout/fulfillment and https://developers.cloudflare.com/d1/worker-api/d1-database/ . D1 batches are atomic transactions.

## Pausing new sales

SALES_ENABLED and legal/signer readiness gate only new Checkout. Pausing sales does not stop payment webhooks, existing order access, saved-code recovery or deactivation. Webhooks require the database, Stripe provider/webhook credentials, product/price IDs and explicit STRIPE_LIVE=true/false; they never depend on the private signer. Existing entitled activation requires a valid signer and is independently blocked for any order with a stored confirmation obligation. Orders without such an obligation retain compatibility behavior, which does not prove that a confirmation was delivered. Keep provider/webhook credentials and database online to process pending payments, refunds and disputes.

The buyer page lists active device codes and available seats after Refresh payment and PCs or activation. Each own-PC entry offers Deactivate this PC with a confirmation explaining local offline-file removal. Successful deactivation refreshes the list and seat count. Deactivation is idempotent and requires the same authenticated purchase claim.

## Rate-limit retention and existing-install migration

Fresh databases use schema.sql, where rate_limits.expires_at is NOT NULL and indexed. Existing installations with the old two-column table must apply the following exactly once BEFORE deploying this Worker (inspect PRAGMA table_info(rate_limits) to distinguish them):

```
npx wrangler d1 execute makrivo-commerce --remote --file migrations/0002_rate_limit_retention.sql
```

Do not apply that migration to a fresh schema.sql database or repeat it: ALTER TABLE adds the column once. The migration adds a nullable expiry column without modifying commerce records; NULL marks legacy buckets, including any old raw-IP values, for deletion by the first buyer cleanup or scheduled invocation. Arrange the Worker update and first successful cleanup in the same maintenance window; verify no NULL expiry rows remain. Back up and verify the selected environment/database before the operator runs remote migrations. The initial patch was verified locally; the later disabled Aivana deployment uses a fresh schema, not this legacy migration (see DEPLOYMENT-STATUS.md).

Each new bucket expires at the end of its UTC minute plus a one-minute buffer: logical lifetime is 60–120 seconds from insertion. Eligible buyer API requests delete only NULL-expiry or expired counters before incrementing the current-minute bucket. Atomic SQL increments preserve the 30/minute limit; hashing includes the minute so a new minute gets a separate counter. Cleanup never deletes based on hit count or a partial bucket-name match.

The Worker also has an awaited scheduled handler; wrangler.jsonc configures `*/5 * * * *`. Cleanup uses actual execution Date.now(), never a future scheduled-event timestamp. It runs independently of sales/legal/signing/provider readiness, needs only the D1 binding, and touches only rate_limits. Under normal successful five-minute invocation, physical retention is at most approximately seven minutes from bucket creation (120 seconds to expiry plus up to five minutes to cleanup); buyer traffic can shorten this. This is an intended operational bound, not a guarantee during outage, paused deployment or cron failure. Monitor failed invocations and database cleanup; rows persist until a successful cleanup. No orders, entitlements, devices, license payloads, events or refund tombstones are deleted by this job; their retention remains a separate legal/operator policy.

The disabled Aivana deployment has a remotely imported fresh schema and verified five-minute schedule; actual delivery evidence is tracked in DEPLOYMENT-STATUS.md. Other environments still require their own cron deployment and remote D1 verification. After the operator deploys, verify the five-minute trigger and successful scheduled execution, expired/NULL row removal and active-row preservation against staging D1; test cron locally via Wrangler's scheduled-handler endpoint. Local automated tests invoke the actual exported scheduled handler against SQLite; they do not prove Cloudflare delivery or timing. Primary references checked 2026-10-26: [scheduled handler](https://developers.cloudflare.com/workers/runtime-apis/handlers/scheduled/) and [Cron Triggers configuration](https://developers.cloudflare.com/workers/configuration/cron-triggers/).


## Checkout recovery migration

Fresh databases use `schema.sql`. Before deploying this Worker over an existing database, back up and verify the correct account and database, then apply `migrations/0003_checkout_attempts.sql` once in a coordinated maintenance window. The migration adds only a table; existing orders, entitlements and claim recovery remain unchanged. Do not apply it to a fresh database created from the updated schema. Deployment and migration are not performed by local tests. Test interrupted and concurrent payment creation, browser return and operator reconciliation in isolated Stripe test mode before enabling sales.

## Cloudflare runtime smoke check

The Node suite checks the Worker module and SQL, but it does not start Cloudflare's runtime. In November 2026 a named non-callable module export passed Node tests while preventing workerd startup. Before deploying, additionally run `wrangler dev --local --test-scheduled` against an isolated local D1 database initialized from `schema.sql`, with sales and legal approval disabled. Check the German/English page, a rejected checkout request and the scheduled handler; confirm the server actually starts. `test/worker-exports.test.mjs` catches the specific incompatible-export class but is not a replacement for this runtime check. Do not use a remote D1 database or real secrets for the local smoke test. This does not replace an authenticated Stripe/Cloudflare staging test.
