<!-- generated-doc locale=en-US source=docs/testing/2026-10-12-github-publication.md sha256=ab4d40cd12047e2cdacad359a12febe16d5689a82f4f792b484b3d4e2430e02a -->

> Machine-assisted translation. Structure, code and links are checked; native-speaker and legal review are not implied. Historical reports retain their original dates and scope. [Original source](../../testing/2026-10-12-github-publication.md)

# GitHub release of the MAKRIVO development status

Status: 12. October 2026. Source code release, not a signed retail release.

- Solution release build: successful, zero warnings and errors.
- Core exams: successful.
- WPF smoke test: successful; fresh results report in local build folder.
- First full Windows suite: foreground window, audio, screen and iCUE checks pass; then the UI Automation worker response times out.
- Targeted retry `--extensions-only`: successful including UI automation, ambiguity, disabled targets, password exclusion, plugin process and real HTTPS mobile host. The original timeout was not obscured by a timeout change; the cause has not yet been conclusively proven.
- Subsequent full Windows rerun: successful including foreground window and all extension checks. A successful rerun does not exclude sporadic timeouts.
- Prior to publication, no typical private keys or productive Stripe/GitHub/AWS keys were found in the audited source inventory. This limited sample check is not a complete secret check.
- Build artifacts and local orchestration data are not published.

Functional scope and remaining deductions: [Feature List](../FEATURES.md)The Previous test reports retain their timely validity; they are not a guarantee for any hardware or desktop context.
