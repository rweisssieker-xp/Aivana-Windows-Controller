# MAKRIVO disabled Cloudflare deployment — 2026-10-26

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
