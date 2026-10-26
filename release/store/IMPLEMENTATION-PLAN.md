# Microsoft Store implementation plan

> **For agentic workers:** Use superpowers:subagent-driven-development or superpowers:executing-plans. Track tasks below.

**Goal:** Produce a verifiable MSIX candidate and honest Store submission materials while retaining the external four-PC one-time license.

**Architecture:** Reuse the retail payload and licensing. Add a separate MSIX packager/checker and Store-specific listing/review material; preserve concurrent application changes.

**Tech stack:** PowerShell 7, Windows SDK MakeAppx, XML, existing .NET 10 WPF app.

**Spec:** release/store/DESIGN.md

## Constraints and review focus

- No silent change to EUR 29 gross, four PCs, perpetual major 1, no subscription.
- Missing/invalid identity, wrong assets, unsafe payloads or damaged inventory must fail before a misleading package is emitted.
- Store installation does not grant a paid license; disclose external payment prominently.
- Test identity, test checkout or test keys must never be called submission-ready.
- MSIX data virtualization, helper execution, device access and packaged installation require separate actual acceptance.

## Tasks

- [x] Add failing packaging tests for identity/version/layout and tampered payload; implement MSIX packaging/checking and run tests.
- [x] Build a real unsigned MakeAppx candidate using explicit test identity and existing test retail payload; inspect archive/manifest and digest.
- [x] Prepare German/English Store listing, accurate reviewer notes and Partner Center/account handoff with official sources.
- [x] Independently review, run affected tests, commit only owned Store files and publish a reviewable GitHub change.
