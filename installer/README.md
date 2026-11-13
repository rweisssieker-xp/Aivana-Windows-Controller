# Retail installer versions

`tools/publish-retail.ps1 -PackageVersion 1.0.1 ...` passes the chosen version to WiX. The default is `1.0.0`. Increase the three-part version for every distributed upgrade; Windows Installer ignores a fourth part, so it is rejected. Major and minor must be 0–255, build 0–65535. Keep the stable UpgradeCode in Product.wxs. A changed installer version does not change the purchased major-version license policy.

All outputs remain candidates with releaseApproved=false. Unsigned builds require `-AllowUnsignedCandidate` and explicitly report TEST_ONLY. An operator must supply the HTTPS commerce URL and public-only RSA key ring. Production requires real signing and independent legal/physical acceptance bound to packageBuildSha256.

The experimental ONNX companion is included only when its project exists. `experimentalOnnxIncluded` records the result; inclusion makes no claim about model availability, model quality or accepted AI behavior. The base retail package remains supported without this optional project.
