<!-- generated-doc locale=en-US source=docs/licensing/2026-10-12-commercial-license-audit.md sha256=b58c6861e0b38c4495265651b5ab10349c701d8fb4582dd7d7cae5d2d865bc5f -->

> Machine-assisted translation. Structure, code and links are checked; native-speaker and legal review are not implied. Historical reports retain their original dates and scope. [Original source](../../licensing/2026-10-12-commercial-license-audit.md)

# MAKRIVO: Check for commercial one-time sale

Status: 12. October 2026. Manufacturer: Aivana.

## Result

In the audited inventory, no general obligation to disclose one's own MAKRIVO source code was identified. A proprietary one-time sale appears possible, but is not yet fully prepared. In particular, redistribution permission for the existing Corsair-SDK-DLL is not documented. This review does not replace legal approval.

## Technical and licensing findings

| component | Findings | Required step |
| --- | --- | --- |
| Own source code | No own LICENSE, COPYING or NOTICE found in the examined Git inventory and its locally available license file history. | Confirm ownership of rights including contributions, adopted code fragments and possible employer rights; create your own terms of use. |
| .NET / Windows Desktop | The examined standalone package under `artifacts/makrivo-win-x64` contains version 10.0.6. The local runtime packages contain MIT licenses that allow commercial distribution and require that notices be included. No corresponding license/notice files were found in the output folders examined. | Include exact license texts and applicable third-party information for the runtime actually delivered in the sales package. |
| NuGet dependencies | No direct PackageReference entries found in the project files examined. The desktop assets file being examined contains its own project dependencies. Runtime packages still remain license-relevant. | Re-inventory all projects, transitive packages, and bundled files upon final release. |
| Corsair iCUE SDK | `artifacts/win-x64/iCUESDK.x64_2019.dll`, file version 4.0.84.0. The local SDK reference PDF contains on printed pages 35–38 the EULA: royalty-free use, but restrictions on transfer/assignment. There was no clear permission to pass on the DLL to customers. | Obtain robust redistribution permission before bundling. Alternatively, check a package without an SDK DLL that uses a customer-installed SDK; The SDK conditions also apply to this. |
| Different packages | `artifacts/makrivo-win-x64` does not contain the Corsair DLL; `artifacts/win-x64` contains them. The application dynamically looks for the SDK in multiple locations. | Set the actual retail installer and check its contents. The mere existence of a package without a DLL does not confirm all integration rights. |
| OBS | Own WebSocket client in the examined C# code; no OBS client package, OBS binary, or adopted GPL code identified. | The observed protocol integration does not in itself justify any established GPL disclosure obligation. The origin of any fragments that may have been adopted remains to be confirmed. |
| App icons | A local Python script draws geometric symbols using Pillow. The tool is a build dependency. | Document the origin and rights of all images, sounds, fonts and other assets actually delivered. The script alone does not prove the rights to each asset. |
| License management | No full purchase license/activation check identified in the examined application code. | If desired, implement signed offline license files. Technical activation and legal terms of use are separate tasks. |

## Sensible purchase model – not yet implemented

- one-time payment; Permanent right of use to the purchased main version.
- One person on up to two of their own PCs; Expressly allow professional use.
- Include bug fixes and minor updates; Optionally offer later major versions for a fee.
- Digitally signed license file that can be checked offline; Never deliver private signing keys with the app.
- Legal rights and required security updates may not be excluded across the board by the major version rule.

## Open before first sale

1. Corsair Clarify distribution rights or prepare to sell without bundled SDK DLL.
2. Include license and third-party texts for the exact release dependencies and receive them in the installer.
3. Confirm ownership of rights to your own code and assets; Create EULA with clear device, user and upgrade rules.
4. For consumer sales, handle the checkout including information requirements and cancellation. A blanket statement of “no refunds for downloads” is not enough. The expiry of the right of withdrawal if the goods are made available immediately requires the legal requirements.
5. Recheck the finished installer including plugins and all transitive dependencies. The working copy is expanded in parallel; This inventory is not a release of future files.

## GitHub

The examined repository is public. Without an open source license, the code does not automatically become free for any further use. The GitHub platform rights for public repositories must still be observed. A later switch to private does not remove existing copies or forks. Visibility and license were not changed during this review.

## Evidence and sources

- Local .NET licenses: `C:/Users/weiss/.nuget/packages/microsoft.netcore.app.runtime.win-x64/10.0.6/LICENSE.TXT` and `C:/Users/weiss/.nuget/packages/microsoft.windowsdesktop.app.runtime.win-x64/10.0.6/LICENSE`.
- Local .NET Third Party Notices: `C:/Users/weiss/.nuget/packages/microsoft.netcore.app.runtime.win-x64/10.0.6/THIRD-PARTY-NOTICES.TXT`.
- Local Corsair SDK: `artifacts/dependencies/iCUESDK_4.0.84.zip`; extracted reference PDF at `sdk/iCUESDK/doc/iCUE SDK - Overview and Reference.pdf`.
- [The .NET 10.0.6 license](https://raw.githubusercontent.com/dotnet/runtime/v10.0.6/LICENSE.TXT)
- [Corsair SDK documentation with EULA](https://raw.githubusercontent.com/CorsairOfficial/cue-sdk/master/docs/index.html)
- [GitHub: Licensing Repositories](https://docs.github.com/en/repositories/managing-your-repositorys-settings-and-features/customizing-your-repository/licensing-a-repository)
- [BGB § 327f – Updates](https://www.gesetze-im-internet.de/bgb/__327f.html)
- [BGB § 356 – Right of withdrawal](https://www.gesetze-im-internet.de/bgb/__356.html)

Not checked: complete external contribution history and rights chains, trademark release, existing sales checkout, future plugin packages, full legal effectiveness of a not yet available EULA.
