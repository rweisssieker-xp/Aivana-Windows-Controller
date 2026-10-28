> REVIEW DRAFT — 26 October 2026. Not approved for publication. Release gates are maintained separately in PRIVACY-REVIEW.md.

# MAKRIVO privacy notice

## Who is responsible

Aivana GmbH, Paulusstr. 45a – Hinterhaus – LOFT45, 33602 Bielefeld, Germany, represented by Udo Bergmann, is responsible for the processing described here. Contact us, including for privacy requests, at info@aivana-gmbh.ai.

This notice covers MAKRIVO for Windows and our external purchase and license service. Microsoft processes information about Microsoft Store access and acquisition under its own [privacy statement](https://www.microsoft.com/en-us/privacy/privacystatement).

## Local app operation

MAKRIVO stores profiles, action definitions, settings and backups locally. These can contain application names, file paths, website addresses and text you enter. Actions can start applications, open websites or exchange data with services you configure. Those services receive the information needed for the action and apply their own privacy terms.

The local meeting experiment log records timestamps, experiment identifiers, durations, events and corrections. It rotates at approximately 2 MiB. Local data and backups remain until deleted or replaced; deleting a profile does not necessarily remove copies in backups or a folder you synchronize. The app does not send these configurations or experiment logs to Aivana as part of the purchase and license process.

If you enable mobile control, paired browsers on your network can receive selected profiles, action names, status and the most recent 50 verification entries. The connection uses HTTPS. A pairing cookie can persist for 30 days; token hashes are stored locally. Protect access to paired devices and the network.

OBS integration exchanges commands and status with the OBS server you configure. Its default connection is local; a configured remote server receives the same functional data. The OBS password is stored using Windows Credential Manager. Profile synchronization writes to a folder you select. If that folder belongs to a cloud service, that service processes the synchronized contents. Plugins run as trusted local processes with the permissions available to them; they are not isolated by a network sandbox. Device lighting integrations communicate with locally installed vendor services.

## Purchase, payment and activation

The external purchase service uses Stripe-hosted checkout. Stripe receives the contact, billing and payment information you provide. We process the order and payment references, payment status and information necessary for invoicing, license delivery and support. Our license service does not store card details. Stripe acts as processor or controller depending on the processing purpose; see its [data processing agreement](https://stripe.com/legal/dpa) and [privacy policy](https://stripe.com/privacy).

The license service uses Cloudflare Workers and D1 to handle purchase claims and activations. It stores order and license identifiers, a hash of the purchase recovery secret, payment references and device codes with activation status. A device code is a SHA-256-derived identifier used to allocate the four PCs allowed by the license. It is pseudonymous, not anonymous. The underlying Windows identifier is not submitted to this service. A signed license file is saved locally and checked offline; there is no periodic online license check.

Requests expose network information, including the IP address, to the hosting infrastructure. The service uses short-lived rate-limit identifiers to protect purchase and activation endpoints. The purchase claim cookie is necessary to access your order in that browser and expires after one year unless removed earlier. It is transmitted only over HTTPS and is unavailable to page scripts. Keep your recovery code private. Cloudflare describes its own processing in its [privacy policy](https://www.cloudflare.com/privacypolicy/).

We process purchase and activation information to perform the contract (Article 6(1)(b) GDPR), required accounting records to meet legal obligations (Article 6(1)(c)), and necessary abuse-prevention information in our legitimate interest in protecting the service and license system (Article 6(1)(f)). Required purchase, billing and activation information is needed to complete the transaction or allocate a device; without it, those services cannot be provided.

## Support, recipients and retention

If you contact us, we process your contact details, message and attachments to answer the request and provide contractual support (Article 6(1)(b)); for other enquiries, our legitimate interest in responding is the basis (Article 6(1)(f)). Send only diagnostics needed for the issue.

Information is accessible to authorized personnel and service providers needed for payment, hosting, communications and accounting, and to public authorities when legally required. We retain contract and license records to administer your continuing entitlement and address claims, accounting records for applicable statutory periods, and support information while needed to resolve and document the issue. We remove data when those purposes and applicable retention duties no longer require it. Local copies, your chosen synchronization provider and independently responsible providers have their own retention arrangements.

## Your rights

Subject to the legal conditions, you may request access, correction, erasure, restriction and portability. You may object to processing based on legitimate interests. Any consent can be withdrawn for the future. Contact info@aivana-gmbh.ai. You may complain to a data protection supervisory authority, particularly where you live or work or where an alleged infringement occurred. Contact us if an automated activation refusal needs review.
