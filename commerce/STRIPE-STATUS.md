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

## Recheck — 9 November 2026

After the owner signed in again, the live account's read-only Stripe response still reported `business_type=individual`, no company name, and enabled charges/payouts. In Settings → Business → Tax details the company-type control is disabled and displays “Einzelunternehmen / Natürliche Person”. Stripe says these details are shared with other Stripe accounts and directs the account owner to contact support to edit them. “Aivana GmbH” in the Dashboard is a trade/display name and does not establish that the GmbH is the legal account holder. Do not change a shared legal entity or payout account without the owner's direct involvement, and do not enable MAKRIVO sales against this account until the seller and payout ownership are established.

On the user's explicit instruction, an inquiry was sent to Stripe's support assistant. It recommended a separate account for the GmbH. The owner created a separate account in the browser, `acct_1UZq8V2fg26yhZUg` (display name “Aivana GmbH – MAKRIVO”); the initial sandbox was opened. The activation form was started with a new legal entity rather than copying an existing account, and `Kapitalgesellschaft / GmbH` selected. Public business name, registration and contact details were entered from Aivana's imprint. The next form requests the authorized representative's personal information; the owner must supply and verify it. The legal entity is **not yet verified** and no final activation or payout ownership is asserted. No live MAKRIVO product, payment or sales switch was enabled.

## Separate account follow-up — 9 November 2026

The supplied representative details were entered. The user confirmed Udo Bergmann as the only beneficial owner with more than 25% ownership; Stripe still marks his identity details incomplete. The onboarding summary also reports a missing payout bank account. The owner-information form was left open for the user, who must review and provide the missing details and complete the bank mandate. The final Stripe account submission has not occurred. The MAKRIVO software category and description, published Aivana support telephone, `AIVANA GMBH` statement descriptor and included Radar Lite were configured. Stripe Tax was deferred and remains a release gate.

A separate Stripe **sandbox** belonging to this new account is `acct_1UZq8kGWmRw4qZXT`. In that sandbox only, product `prod_Vb4fieHYtK2Qel` and price `price_1UZsW9GWmRw4qZXTWYXmojEh` were created. The price details show EUR 29.00, one-time and tax-inclusive. The default product tax category (`txcd_10000000`, electronically supplied services) is provisional and requires accounting review; inclusive pricing does not establish correct taxation or registration. These identifiers are test-only, do not prove that checkout or fulfillment works, and have not been installed in the Worker. No live product, secret or payment was created.
