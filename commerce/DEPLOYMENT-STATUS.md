# MAKRIVO disabled Cloudflare deployment — 2026-10-26

## 15 November 2026: isolated delivery-event monitor

Separate EU queues `makrivo-mail-events-test` and `makrivo-mail-events-test-dlq`, and a distinct WEUR test database `makrivo-mail-monitor-test`, were created without applying test migrations to the commerce database. The no-route Worker `makrivo-mail-monitor-test` version `37e710e9-83b3-417b-96be-50a9b872ec52` consumes only the configured sender and the owner's one test mailbox. The MAKRIVO-domain event subscription was temporarily enabled for a synthetic mail test and then verified disabled again. It is not continuous monitoring.

After subsequent test-only monitoring changes, the same isolated queue consumer was redeployed as version `e5f503d3-7a6f-4aa8-a4e8-16775a0af2e8`; the event subscription remains disabled. The new unclaimed-job and Cron logic is in the commerce source only, not in that queue-only Worker and not deployed to `makrivo-commerce`. A commerce Worker dry run bundled successfully with the existing disabled configuration; it did not publish it.

A test email arrived in the owner's Gmail inbox; the corresponding real Cloudflare `delivered` event reached the isolated D1 with the same provider message ID. An intentionally invalid, non-personal event was routed to the dedicated dead-letter queue after retries and acknowledged only after its synthetic content was identified. Bounce events were tested synthetically, not by provoking a real bounce. No independent alert channel, continuously monitored dead-letter queue, support workflow or production event processing is installed. See `DELIVERY-MONITOR-PLAN.review.md` and `MAIL-SUPPORT.review.md`.

Source commit `957c5b6` includes the local monitoring preparation; it has not replaced the separately deployed disabled `makrivo-commerce` Worker described below. A remote read confirmed `SALES_ENABLED`, `LEGAL_APPROVED`, `STRIPE_LIVE` and `MAIL_DELIVERY_TEST_ENABLED` remain `false`. No public sale or real customer-data processing was enabled.

## 15 November 2026: restricted queue-dispatch code

Source commit `4a69104` was deployed as Worker version `29c0e53a-fddd-4fab-9003-3e4df44d3837`. Authenticated settings inspection confirmed the email binding and `MAIL_DELIVERY_TEST_ENABLED=false`, `SALES_ENABLED=false`, `LEGAL_APPROVED=false`, `STRIPE_LIVE=false`. There is no public route. The remote database has not had test-only migrations 0010 or 0013 applied; the disabled worker does not access those tables. A local workerd run using that non-test configuration returned page 200, disabled checkout 503 and scheduled handler 200 without the test-only tables.

With an isolated **local** D1, separate synthetic order and withdrawal records, and a remote Cloudflare email binding restricted to the account owner's test mailbox, the purchase and withdrawal dispatch functions each produced one real test email that appeared in the mailbox. Payment was simulated: no Stripe account, payment, real contract or customer withdrawal was used. A repeated dispatch returned `already_claimed` for both. A separate test with an unonboarded sender returned `outcome_unknown`, remained blocked on retry, and made the local scheduled handler report operator review. The Node suite passed 128 tests. See `DELIVERY-IMPLEMENTATION.review.md` for limits. At that time no delivery-event subscription had been configured; the later disabled test subscription is described above. No real customer dispatch or license release is enabled.

## 15 November 2026: sending domain and disabled binding

The user signed in to the Cloudflare account owning the active `aivana-gmbh.ai` zone. The dedicated sending domain `makrivo.aivana-gmbh.ai` was created, with message-content preview disabled and suppression dropping disabled. Cloudflare created separate bounce MX/SPF, DKIM and DMARC DNS records. Public DNS checks observed those records and the existing apex MX still pointed to Microsoft. No account-wide mail configuration or tariff was changed.

After a remote check showed zero orders and authorizations, a private export was restored in memory and passed `PRAGMA integrity_check`. Migration 0012 was applied; a remote query confirmed the expiry index and zero orders and authorizations. Source commit `b83971d` was deployed as Worker version `ad3401ed-5eef-4c79-94af-bbc6adbb73fa`, with an email binding restricted to `noreply@makrivo.aivana-gmbh.ai`. Deploy output confirms `SALES_ENABLED=false`, `LEGAL_APPROVED=false`, `STRIPE_LIVE=false`, empty Stripe and signer identifiers, and the five-minute schedule. No public route was added. The Worker does not yet invoke the email adapter, so this binding does not send customer messages or fulfill confirmation duties.

A single synthetic test message was sent using the Cloudflare REST API to the authenticated account owner's own Gmail mailbox. Cloudflare initially queued it, and Gmail subsequently showed the message in Inbox with SPF, DKIM and DMARC all passing. This verifies that particular sender and destination through the REST API, not the Worker binding, actual purchase, queue processing, bounces, or general delivery. No customer data or payment was involved. The account's paid-plan status remains unverified because the token could not read subscriptions. No live sale has been enabled.

## 15 November 2026: expiry cleanup prepared locally

The source now includes scheduled deletion of expired `checkout_authorizations`, and migration `0012_checkout_authorization_expiry.sql` adds an expiry index. The full Node suite passed 114 tests, including expiry boundary, valid/renewed authorization preservation, recovery after cleanup and migration replay. A separate local workerd/D1 instance returned page 200, disabled checkout 503 and scheduled handler 200; a synthetic expired authorization was removed while an active authorization and both synthetic orders remained. These are local results, not a remote concurrency or production test.

At that earlier check the Cloudflare authentication session was unavailable, so migration 0012 was not yet applied or deployed. The later authenticated deployment and backup are recorded above. Sales remain disabled.

## 12 November 2026: digital-content declarations

Before migration `0011_digital_consents.sql`, a remote export was restored into in-memory SQLite and passed `PRAGMA integrity_check`; it contained zero orders and the confirmation-jobs table. The migration was applied to the same isolated D1 database. A remote query confirmed zero orders, declaration rows and checkout authorizations, and all three declaration-protection triggers. The checked backup is `artifacts/store-readiness/makrivo-d1-before-0011-20261112.sql` and must remain private.

Source commit `f024fa4` was deployed as Worker version `92a03ff1-8beb-4c3b-bc4e-26891928c0fd`. Upload output confirms sales, legal approval and live Stripe remain false, payment and signing identifiers are empty, and the five-minute schedule remains configured. No public route or legal texts were added. The integrated suite passed 105 tests; local workerd with migration 0011 returned page 200, disabled checkout 503 and scheduled handler 200. This is not a live transaction, provider delivery or legal approval. The isolated withdrawal prototype remains unmounted.

## 12 November 2026: confirmation queue and private downloads

The selected isolated D1 database had zero orders, acceptance records and entitlements; neither `confirmation_obligations` nor `confirmation_jobs` existed. A remote export was restored into in-memory SQLite and passed `PRAGMA integrity_check`, with the existing acceptance schema and zero orders confirmed. Migration `0005_confirmation_jobs.sql` was applied; a remote query confirmed both tables, three obligation immutability triggers and zero orders, obligations and jobs. No withdrawal-test migration was applied.

Source commit `80c7281` (including the isolated, unmounted withdrawal-test files) was deployed as Worker version `b8aff80b-c40c-4d33-be6d-ce93809bf893`. Upload output confirmed `SALES_ENABLED=false`, `LEGAL_APPROVED=false`, `STRIPE_LIVE=false`, empty Stripe/product/signer settings and the five-minute schedule. The configured Worker has no public route. Local workerd accepted the merged Worker with the 0005 migration: page 200, checkout 503 and scheduled handler 200; the combined local suite passed 88 tests. This does not demonstrate remote HTTP, Stripe payments, mail delivery, legal compliance or a production purchase. The unmounted withdrawal module is not a live withdrawal function.

## 9 November 2026: checkout recovery update

**Contract-evidence preparation:** The isolated D1 database was checked to contain zero orders and no `contract_documents`/`contract_acceptances` tables. A local export was restored into SQLite and passed `PRAGMA integrity_check`; migration `0004_contract_acceptances.sql` was then applied. A remote schema query confirmed both tables and all six immutability triggers. Source commit `2b61634` was tested with 54 Node tests and local workerd (page 200, disabled checkout 503 and scheduled handler 200), then deployed as disabled Worker version `6dc57e01-f8a0-44b0-aab3-0ee3eb7d676b`. Both sales and legal approval remain false; no legal documents, payment credentials or license secrets were added. New live checkout is also blocked in code until durable confirmation delivery is implemented. This is not a tested sale or proof of legally sufficient consent, delivery, tax configuration, or remote HTTP operation.

**Later runtime correction:** A local Wrangler/workerd start of the earlier source failed because the Worker module exported the SQL string `activationSql` as a named entry point. The earlier successful deployment and Node-only tests did not establish runtime startup. Commit `7d2c621` moves the unchanged SQL to an imported module and adds an export regression test. With that fix, Wrangler 4.153.0 started workerd locally; the German page returned HTTP 200, disabled checkout returned 503, and the scheduled-handler test endpoint returned 200. The full Node suite passed 41 tests. The same fix was uploaded to the still-disabled remote Worker as version `d3301575-79ca-4029-8b9b-8ba6fefd9aad`; deployment output again showed sales and legal approval disabled and the five-minute schedule. The Worker has no public route, so this does not assert a remote HTTP or real Stripe end-to-end test.

The separate `makrivo-commerce` D1 database was checked before migration: `orders`, `devices`, `entitlements`, `events` and `revoked_payments` contained zero rows, and `checkout_attempts` was absent. A full D1 export was downloaded to the local, untracked `artifacts/store-readiness/makrivo-d1-before-0003-20261109.sql`; its SQL was restored into an in-memory SQLite database and `PRAGMA integrity_check` returned `ok`. The migration `migrations/0003_checkout_attempts.sql` was then applied to the selected remote database. A subsequent query confirmed the expected `checkout_attempts` schema and zero orders.

After the migration, source commit `65c9d69` was deployed to the existing isolated Worker as version `c65406ba-28b9-4ccf-b0f5-f13a2622f75c`. The deploy output reports `SALES_ENABLED=false`, `LEGAL_APPROVED=false`, `STRIPE_LIVE=false`, the expected D1 binding, and the existing five-minute schedule. The deployment used `wrangler.aivana.jsonc`, which declares no routes, disables `workers.dev` and previews, and contains no payment, signer or customer-facing legal configuration. Secret-list output was empty before deployment. This is a disabled update, not a live sale or an end-to-end staging acceptance; real Stripe test-mode checkout, recovery, refunds, Cloudflare D1 concurrency, and production readiness remain unverified.

The existing Cloudflare account managing `aivana-gmbh.ai` was verified through its authenticated zone metadata. A separate `makrivo-commerce` Worker and fresh database were created without modifying the existing website or its services.

- Source commit: `3b3a316`.
- Worker version: `e0eb373a-24a8-4b69-aa8b-b0f832630e9d`.
- D1 name: `makrivo-commerce`; database ID: `c1313e7b-e8d4-44fa-bc20-9bbfbbf0a6bf`.
- D1 API reports jurisdiction `eu`. This is not a claim that every Cloudflare network operation or provider log stays in the EU.
- Fresh `schema.sql` import completed remotely. The legacy migration was not applied to this fresh database; unrelated databases were not modified.
- Authenticated Worker metadata confirms the D1 binding and `SALES_ENABLED=false`, `LEGAL_APPROVED=false`, `STRIPE_LIVE=false`.
- `workers.dev` and preview URLs are disabled; no public route or custom domain was assigned. `https://kaufen.aivana-gmbh.ai` is a planned origin value only, not a live purchase URL.
- The five-minute cleanup trigger `*/5 * * * *` is present in Cloudflare's schedules API. A synthetic expired/active-row probe was seeded in the new rate-limit table to check actual scheduled delivery; final results are recorded below when observed. Configuration alone is not execution evidence.

Wrangler 4.153.0 dry-run, upload and trigger deployment completed successfully. The rate-limit implementation passed 23 local SQLite/handler tests and an independent read-only review. Raw IPs are not written into the new rate-limit buckets. The short cache cleanup touches no order, entitlement, device, event or refund tables.

Use `--config commerce/wrangler.aivana.jsonc` for this selected account/database. The generic `wrangler.jsonc` remains a disabled operator template. Do not run the legacy ALTER migration against the fresh database or enable checkout merely because a Worker version exists.

Still required: Stripe test/live configuration, actual license signing secret and matching app public key, controlled checkout/refund/activation/recovery acceptance, reviewed legal/privacy documents and processing arrangements, public route/domain setup and final release approval. No charge, buyer activation or public sales launch occurred.

## Scheduled delivery probe

At approximately 16:39 UTC, a remote SELECT still returned both synthetic `MAKRIVO_TTL_CHECK_EXPIRED_63d27e4a` and `MAKRIVO_TTL_CHECK_ACTIVE_63d27e4a` rows. Actual scheduled execution is therefore **not yet verified**; the schedule metadata alone is insufficient. Recheck after Cloudflare trigger propagation, record whether only the future-expiry row survives, then remove only these synthetic probe rows. Neither row represents a buyer or contains an IP address. Do not enable public sales until the deployed cleanup behavior is accepted.

28 October follow-up: the authenticated remote SELECT succeeded and returned no synthetic probe rows. Both expiry timestamps were by then in the past; no explicit cleanup DELETE was issued. This confirms eventual removal after the deployed schedule had time to propagate, not exact timing or preservation of the formerly active probe during its initial hour. Preservation of unexpired buckets remains covered by local tests; no seven-minute physical-retention guarantee is asserted.
