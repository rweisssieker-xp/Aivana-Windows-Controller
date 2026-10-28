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

## Later onboarding follow-up

At the user's request, the public software description and support telephone from Aivana's imprint were entered. The inherited statement descriptor was corrected to `AIVANA GMBH` / `AIVANA`. Included Radar Lite was selected; optional tax onboarding and Climate contributions were skipped. Tax configuration remains a release gate.

The owner subsequently advanced the live account to Dashboard. Read-only `accounts retrieve` confirms `charges_enabled=true`, `payouts_enabled=true`, `details_submitted=true`, no currently-due requirements — but **`business_type=individual`, company name absent**. The account is therefore technically active but does not establish the intended Aivana GmbH seller setup. The shared-legal-entity notice means changing its legal identity can affect linked accounts; the owner was asked to separate/correct that configuration and verify payout ownership. No agent acceptance of the Stripe/SEPA agreements occurred. No live MAKRIVO product or charge was created. Sales remain disabled.
