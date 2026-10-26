# Local Store packaging evidence — 2026-10-26

Source: clean Git export of `bf99712d0cbfcf3f9fa3842c1257ceb7c8adf655` (the published retail implementation). The current workspace's unrelated, uncommitted debugger/workspace/ONNX changes were not included in this candidate. The export was built with the existing retail publisher and the public test fixture ring, using `https://commerce.invalid`. No private signing key was used or included.

## Produced artifacts

- Retail source package: `artifacts/store-retail-source-282d570f7c6b40c490d01e6dd5e06302`.
- Retail source package digest: `240278f9efc209bf7846503ce270560109111671a2f95b89bccdd978665e2ac0`.
- MSIX: `artifacts/store-candidate-20261026-clean-bf99712/MAKRIVO.msix`.
- MSIX size: 163,557,966 bytes.
- MSIX SHA-256: `fe57906a436bd44db14eff0546cc7e539093522bb366589e9edbfd6eecab003d`.
- Test identity: `MAKRIVO.TestOnly`, publisher `CN=MAKRIVO Test Only`, version `1.0.0.0`, x64.

The existing retail publisher completed successfully, including self-contained helper processes, dependency notices and retail inventory validation. The new publisher called Windows SDK MakeAppx with schema validation enabled and produced a real package. The Store checker passed against the resulting archive, full payload and inventory. An independent read-only review found no scoped implementation blocker.

## Meaning of this evidence

This proves that the selected source can be published into a structurally validated MSIX with the retained retail licensing resource and matching payload. It does **not** prove runtime behavior after packaged installation, upgrade, uninstall or data migration, hardware/LAN compatibility, WACK acceptance, production activation, legal approval or Microsoft certification.

The candidate is unsigned and uses a test identity, test public key and unreachable test purchase URL. `testIdentity`, `testRetailSource` and `unsigned` are true; `releaseApproved` and `submissionReady` are false. It must not be uploaded or advertised as the real Aivana Store release. Store production identity and working purchase/reviewer access remain necessary.

Actual source revision, artifact SHA-256 and package inventory should accompany every later acceptance record. Rebuilding with different identity, keys or URLs produces a different package and requires checking that exact build.

## Partner Center identity assigned later on 2026-10-26

MAKRIVO was reserved in the existing Aivana account as Store product 9NKW4WK59QX8. Submission 1 (1152921505702236458) exists in draft. Exact public identity values are in partner-center-identity.json.

A second MSIX was built with that actual identity from the same clean retail source: artifacts/store-candidate-20261026-aivana-identity/MAKRIVO.msix, SHA-256 19ab02492995884836807ae111e2052505fc8d7cbc21d21d2ec5012c79d37ee3. Publisher and checker completed successfully. Its testIdentity flag is false, but testRetailSource and unsigned remain true and submissionReady/releaseApproved remain false. This resolves the identity placeholder only: commerce.invalid and test keys still prevent real paid use. No package was uploaded.
