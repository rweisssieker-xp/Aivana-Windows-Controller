# MAKRIVO disabled Cloudflare deployment — 2026-10-26

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
