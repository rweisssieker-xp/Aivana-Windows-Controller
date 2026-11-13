# MAKRIVO offline license format

`LicenseVerifier.Verify(envelopeJson, publicKeys, expectedDeviceId, currentMajorVersion)` returns an immutable result. Invalid input returns `IsValid=false`, an operator-safe error, and no payload. No network request or expiry check is performed. A license issued ten years ago remains valid for its purchased major version.

`LicenseSigner.Sign(payload, keyId, privateKeyPem)` is an issuer utility, never a desktop activation bypass. Call it only after the commerce service has authenticated the purchase and atomically allocated one of four devices. Its caller owns entitlement validation; the verifier rejects invalid signed entitlements. Keep production private keys exclusively in the issuer secret store. The test suite generates ephemeral keys in memory.

The envelope contains exactly `payload`, `signature`, `keyId`. Payload bytes are UTF-8 JSON encoded as canonical unpadded base64url. Sign the exact bytes using RSA SHA-256 / PKCS#1 v1.5. JSON property order is irrelevant; duplicate, missing, unknown, or wrongly typed fields fail closed. Payload fields are `schemaVersion`, `product`, `licenseId`, `majorVersion`, `maxDevices`, `deviceId`, `issuedAt`. There is no expiry field.

Trusted key rings are JSON objects mapping an opaque key ID to a PEM `PUBLIC KEY` (SubjectPublicKeyInfo) or `RSA PUBLIC KEY` (PKCS#1). Generate RSA keys of at least 3072 bits in the operator's protected environment; export only the public half into the retail configuration. Never paste private material into the repository, app resources, logs or support output. Rotate by adding the new public ID while retaining public IDs for previously issued perpetual licenses. No real key or placeholder trusted key is supplied here.

The verifier limits envelope UTF-8 size to 16 KiB, signed payload to 4 KiB and signature to 1 KiB. Key IDs are at most 128 characters and PEM entries at most 16 KiB. UTC ISO-8601 issuance is required, with a five-minute future clock-skew allowance. Major versions must match exactly; schema is 1, product is MAKRIVO, maxDevices is 4, license ID is a nonempty hyphenated GUID, and device ID is 64 lowercase hex characters. The four-device global limit is enforced by the issuer, not by this offline verifier. Offline copies cannot be remotely revoked reliably.

Run the dependency-free checks with:

```powershell
dotnet run --project tests/Aivana.Licensing.Tests
```
