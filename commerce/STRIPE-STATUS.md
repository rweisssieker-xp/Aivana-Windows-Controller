# MAKRIVO Stripe setup — 28 October 2026

The signed-in Dashboard initially displayed another business sandbox. The existing **Aivana GmbH** account was selected before creating anything. Its sandbox contains the new MAKRIVO product; other products and businesses were not modified.

Verified from the saved Stripe Price response:

- Sandbox account: `acct_1UOHM0K2smzNGbDx`.
- Product: `makrivo_major1_4pc`.
- Price: `price_1UVPHTK2smzNGbDxsZH2RKJP`.
- Amount/currency: `2900` / `eur`.
- `type=one_time`, `recurring=null`, `tax_behavior=inclusive`, `livemode=false`.
- Product description: one person, four own Windows PCs, commercial use, perpetual major version 1, no subscription or automatic renewal.

These are sandbox identifiers, not live credentials or evidence of a paid order. No Payment Link, live charge, subscription, API key, webhook secret or license signing secret was created. No Worker secret/configuration was changed with these identifiers. The product tax category and account tax registrations remain to be verified; an inclusive Price alone does not establish correct tax setup.

Selecting the existing live Aivana account led to **Payments activation / company verification and bank setup**. That onboarding must be completed by the account owner. No company or payout information from other businesses was copied. The internal browser remains at that handoff.

Next: complete Aivana onboarding; configure dedicated staging credentials/webhook and isolated data; exercise hosted checkout, refunds, duplicate deliveries, recovery and four-seat enforcement; then separately configure and verify live product/Price and legal/tax information. The sales flags stay disabled until acceptance. Local screenshots are under `artifacts/store-readiness/stripe-makrivo-sandbox-20261028.png`.
