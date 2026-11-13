# MAKRIVO implementation status — 2026-10-13

Commercial terms: EUR 29 gross once, one person on up to four own Windows PCs, commercial use permitted, perpetual purchased major version 1. No subscription or automatic renewal.

Implemented: authenticated Stripe payment-mode Checkout service; signed offline device licenses; four-seat activation and replacement; purchase recovery code; desktop verification/import and execution guards; retail labels in all 27 application languages; per-user Windows MSI publisher; dependency notices and fail-closed release checks. Development builds are explicitly identified.

Verified locally: 29 licensing checks; 20 commerce tests including authenticated device deactivation; production license-store and queued-dispatch tests; 27-language lookup checks; six invalid/valid build-configuration checks; bidirectional Worker/.NET cryptographic interoperability. Desktop builds from both the current workspace and a clean staged-source snapshot passed with zero warnings/errors, including retail mode. Six package test methods covering 25 scenarios and the complete unsigned candidate passed validation. WiX compiled both default 1.0.0 and explicit 1.0.1 installers; the MSI databases confirmed their versions. These checks are not production or physical acceptance.

Local candidate: `artifacts/retail-candidate-20261013-cebe32f6/MAKRIVO.msi`. MSI SHA-256: `be83990bada54eab8dad403741525cdf4c1f6b0ebf4c9e0de3094fffe06bf449`. This candidate includes concurrent workspace/experimental ONNX changes not owned by this retail implementation; its exact working-source state is therefore not represented by the retail commit alone. It is **TEST_ONLY**, unsigned, embeds an ephemeral public test key and points to `https://commerce.invalid`. It cannot serve paying customers. No private signing key is distributed.

Still required before public sale:

- Operator-controlled deployment/domain, actual Stripe product/price and test/live secrets, D1 database, real license signing key and matching embedded public key.
- Controlled end-to-end checkout, activation, refund, recovery and concurrent D1 acceptance in staging/production as appropriate.
- Trusted Windows signing certificate, timestamped binaries and installer.
- Legal approval of consumer terms, withdrawal/confirmation, tax territories and the actual privacy flow. Draft documents are not legal approval.
- Recorded physical hardware, LAN, DPI, endurance, installation, upgrade and uninstall acceptance for the exact release package.

No live website was changed or payment service deployed. `releaseApproved` and all unperformed acceptance gates remain false/pending. AI remains experimental/off: the historical evaluation produced zero valid plans in four trials with one model. Exclusive keyboard suppression is not advertised without a suitable signed filter driver.
