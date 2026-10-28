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

## First Store release without AI — 28 October 2026

The user selected exclusion of the optional AI assistant. Clean worktree `C:/tmp/Aivana-store-no-ai` provided runtime source `e9860a0`; packaging hardening `c67df63` was applied before MSIX production/checking. These reviewed changes were integrated as `7c9881b`, preserving unrelated local development changes outside the commit.

- Retail candidate: `artifacts/store-retail-no-ai-20261028`, digest `d65395afc55197e73fc0f961e6ac2b792c7b977b42b3d840f5dab3c744a886d5`.
- Actual-identity MSIX: `artifacts/store-no-ai-20261028/MAKRIVO.msix`, SHA-256 `b8ce769cafdd4789a1df72e793e1196f16a85b78812820f22e9504d181ed6d8a`.
- `storeRelease=true`, `aiAssistantIncluded=false`, `experimentalOnnxIncluded=false`; both compiled Desktop and Windows assemblies verified by PE metadata without executing input assemblies.
- Store compile succeeded with zero warnings/errors. Packaging Python suite: 19 passed; final valid Store fixture plus 16 mutations passed, seven injected ONNX/model variants rejected. An independent replay cleared the checker fix. Ordinary model-client regression passed. The unchanged core suite had 36 successful checks and a reproduced file-lock failure in `bounded meeting assistant`; no blanket all-tests-passing claim is made.
- Root reran the final package checker successfully against the exact MSIX and verified its SHA-256. MakeAppx schema validation completed. WiX emitted path-normalization warnings while building the unsigned retail fixture.

This remains an unsigned test-source candidate with `https://commerce.invalid` and a freshly generated test-only public key; no private signing key was retained. `releaseApproved=false`, `submissionReady=false`. It was not installed, uploaded, certified or published. Packaged runtime/hardware acceptance and a working production license service remain open.
