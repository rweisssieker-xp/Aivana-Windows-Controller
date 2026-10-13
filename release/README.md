# MAKRIVO release operations

Status 2026-10-13: reviewable implementation materials, **production release blocked**. `product.json` is the commercial source of truth: MAKRIVO, 2900 EUR minor units gross, inclusive tax, one-time payment, four own Windows PCs for one person, commercial use allowed, perpetual major version 1. There is no subscription, renewal or runtime expiry. Minor fixes are included; future major upgrades are separate optional purchases. Required legal updates remain unaffected.

`EULA-review-draft.md` and `privacy-data-flows.md` are substantive but unreviewed legal drafts. Do not represent their presence as approval. `acceptance.json` records pending real-world acceptance; null evidence is intentional. Existing audits are historical evidence and have not been rewritten.

## Local verification and candidate

Run from the repository root, using installed toolchains. Consult `commerce/README.md` for its final, authoritative deployment variables and commands. These commands do not deploy or authorize sale:

```powershell
dotnet run --project tests/Aivana.Licensing.Tests
node --test commerce/test/*.test.mjs
python -m unittest discover -s tests -p test_retail_package.py
pwsh -File tools/publish-retail.ps1 -PublicKeyFile C:/operator/public-key-ring.json -CommerceUrl https://checkout.example.invalid -OutputDirectory C:/operator/candidates/makrivo-unique -AllowUnsignedCandidate
```

The `.invalid` URL is a placeholder and cannot serve customers. Use a new output directory each time. A candidate may stop on build, dependency notice or tool prerequisites; record the actual error. An unsigned candidate is not approved for public distribution. The public key ring contains only PEM public keys keyed by key ID. Keep signing private keys outside the repository and output tree. A real signer and timestamp validation are necessary for a final package; supplying a certificate argument alone is not signature evidence.

For a signed attempt, replace the candidate command's unsigned switch with `-CertificateThumbprint <operator-certificate-thumbprint>` and a real operator-controlled HTTPS commerce URL. Do not use a test certificate as production evidence. Follow the package checker's actual parameter help before checking a package:

```powershell
Get-Help tools/check-retail-package.ps1 -Full
Get-FileHash C:/operator/candidates/makrivo-unique/MAKRIVO.msi -Algorithm SHA256
```

## Commerce and secrets

Use isolated Stripe test and live environments, products, webhook endpoints and storage. Configure the exact one-time product server-side; a browser price or success redirect is not proof of payment. Hosted Checkout must use payment mode, quantity one and inclusive 29 EUR gross. Prevent accidental additional tax collection. Validate applicable sales territories and tax treatment before accepting their orders.

Illustrative secret categories are `STRIPE_SECRET_KEY`, `STRIPE_WEBHOOK_SECRET` and `LICENSE_PRIVATE_KEY_PEM`; these names are placeholders until reconciled with `commerce/README.md`. Public configuration includes the corresponding signing key ID/public ring, exact Stripe price ID, origin, seller/legal document URLs and explicit operator/legal live-enable gates. Never paste actual keys into commands captured in history, logs, chat, JSON release documents or packages. Use the deployment platform's secret prompt/store and least-privilege operator access. Back up the activation database and securely escrow signing keys; document rotation while preserving public keys needed for existing licenses.

The webhook must verify raw-body signatures and authoritative paid session details. Unpaid, wrong-price/product/currency and wrong-environment sessions receive no entitlement. Exercise retries and parallel activation in staging: four unique devices succeed, a fifth fails, and a repeat activation of the same device consumes no extra seat. Customer access requires the secret claim; public session IDs are insufficient. Redact secrets from incident evidence.

## Customer activation, moves and refunds

Online activation imports a signed device license into local storage separate from user settings. For an offline PC, obtain its product-scoped device request, transfer it through a connected PC, authenticate the paid claim, submit the device ID, then transfer and import the response envelope on the offline PC. Use the actual app/customer UI or documented API; do not invent a downloadable request file format. The response must verify against embedded public keys and the target device before execution. Do not email customers or send license material without explicit authorization.

After activation, offline use requires no periodic server check. A local device hash is personal data when linked to an order; it offers best-effort device binding, not clone-proof DRM. Deactivate unused seats to permit replacements. Refund fulfillment must reconcile the Stripe event and prevent further activations for the refunded entitlement. Existing offline licenses have no expiry and cannot be guaranteed to stop remotely. Operator communications and refund policy must reflect that limitation; do not add a silent online requirement or expiry to the perpetual contract.

## Real acceptance protocol

For every case record operator, date/time, source revision, installer SHA-256, installed binary hashes, Windows build, physical device model/firmware, steps, expected result, actual result, logs and defects. Link immutable evidence in `acceptance.json`; only then change the corresponding status. Mocks and unit checks cannot approve this matrix. Retest affected cases after a binary change.

| Case | Required physical procedure and pass condition |
| --- | --- |
| hardware | On each advertised native hardware model: connect, bind inputs, run/cancel representative action, disconnect/reconnect and resume. No stuck input, unintended action or crash; identify model and firmware. |
| lan | Real phone/tablet and Windows host on LAN: pair/authenticate, execute/cancel, drop/reconnect network and reject unauthorized client. Record router/network conditions without secrets. |
| dpi | Two physical monitors with different scaling: move app, change primary monitor/resolution/scaling and repeat selection/execution. Controls remain reachable and coordinates/actions correct. |
| endurance | At least eight hours representative repeated activity on real hardware, with cancel/reconnect cycles; record memory/handles/CPU at start, hourly and end. No stuck input/crash, no unexplained sustained resource growth. Record cycle count and workload. |
| install | Clean supported Windows standard-user VM/PC: signature/SmartScreen evidence, per-user install, launch, missing-license recovery and valid-license import; no unsolicited autostart. |
| upgrade | Install previous supported package, create/export settings and license, upgrade, verify preserved data and entitlement; test rollback/recovery on failed upgrade. |
| uninstall | Uninstall from installed state, confirm app removal and intentional preservation of user configuration/license; reinstall can recover. Record residual paths, no destructive cleanup of user data. |

AI remains experimental/off (historical evaluation: zero valid plans in four trials with one model); exclusive keyboard suppression remains unsupported without a real suitable signed filter driver. Advertise compatibility only for models whose evidence passed. Install/upgrade/uninstall are also pending, even if automated package checks pass.

## Final release gates

Require documented legal approval of consumer checkout, withdrawal/form/confirmation, updates and actual privacy notice; accepted real physical matrix; complete dependency notices and redistribution rights; production signer and verified timestamps; real website/deployment ownership and access; configured Stripe/D1/domain with tested live/test separation and a controlled end-to-end paid/refunded transaction. Confirm legal text and product details shown before payment match packaged documents. Keep release approved false until each gate has real evidence. No current artifact asserts these gates have passed.
