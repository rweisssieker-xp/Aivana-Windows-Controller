<!-- generated-doc locale=nb-NO source=docs/FEATURES.md sha256=ea712f39457ce69f772f09a1294201705a5f36a92a8bf15f356bd80132c3e723 -->

> Maskinassistert oversettelse. Struktur, kodeeksempler og lenker kontrolleres; dette innebærer ikke gjennomgang av en morsmålsbruker eller juridisk godkjenning. Historiske rapporter beholder sine opprinnelige datoer og omfang. [Originalt kilde](../FEATURES.md)

# MAKRIVO – Oversikt over funksjoner

produsent: **Aivana**. Stand: **12. Oktober 2026**. Dette er utviklingsstadiet, ingen salg eller hardware frigjøring.

## Windows og styring

- Kachler, globale hotkeys, kommandospalett, favoritter og applikasjonavhengige profiler.
- Makroer med flere trinn, gjentakelser, pauser, avbrudd og forhåndsvisning.
- Åpne programmer, filer og nettsteder; Bruk tekst, mellomledd og knappkort.
- Ordne vinduer, låse Windows, kontrollere lydutganger og mikrofoner, og endre skjermlyset gjennom støttet grensesnitt.
- OBS-WebSocket-v5-Anbindung for scener, opptak, streaming og lyd.
- Formler, arbeidsplassassistent, enhet gjenkjenning og tastatur overlay.
- Import og eksport av profilkonfigurasjoner; utformingskontroll og konfliktdeteksjon.
- Lokalt lagret innstillinger; ingen konto for daglig bruk.

## Utvidede funksjoner i den nåværende kildekoden

| Området | Funksjonsomfang | Grenser |
| --- | --- | --- |
| Resultatkontroll | Forhåndsundersøkelse, etterkontroll og gjennomgang av undersøkelsen | Bare verifiserbare effekter bekreftes; et vellykket trykk beviser ikke målvirkning. |
| Arbeidsplassassistent | Forberede profiler og prosesser fra utvalgte programmer, filer, URL-er og andre handlinger | Forhåndsvisning og lagring er nødvendig; Institusjon utfører ingenting. |
| Makroopptak og Workflows | Opptagelse, betingelser, gest og lokal profilsynkronisering | Oppførselen og grensene er dokumentert i gjennomførings- og testrapporter. |
| UI-automasjon | Velg mål, sjekk selektorer, ta opp handlinger og begrensede klikkflyter | Separat worker; tvetydige, deaktiverte og passordmål avvises; ingen opptak av generell tekstinntasting. |
| Utvidelser | Kontrollere, importere, aktivere, eksportere og returnerer pakker etter uttrykkelig tillit | Prosessisolasjon er ikke en sandbox; ingen offentlig pluginmarked. |
| Mobilpanel | Lokalt HTTPS-panel, kobling, utvalgte profiler og status/testing | Reelle telefon/tablett-LAN-avtak er fortsatt åpen; ingen universell sensor-dashboard. |
| Produsentadapter | Corsair, Logitech-G-SDK, Razer/SteelSeries-RGB og Wooting-Analogadapter | Modell/SDK-avhengig; nye adaptere ikke helt validert med ekte maskinvare. Ingen nye proprietære SDK-DLLs bundet. |
| Fysisk tastatur | Standardknapper ved hjelp av enhet og skanningskode | Raw Input undertrykker ikke normal Windows innføring. |
| Lokalt AI-tjenesteassistent | Optional prototype for Ollama, LM Studio, llama.cpp og ONNX Runtime GenAI CPU produserer begrensede design fra uttrykkelig utvalgte ressurser | Deaktivert som standard; native CPU-runtime testet med en tilfeldig modell, men ingen dokumentert planleggingskvalitet eller tidsbesparelse; ingen modellvekter følger med. |

## Ikke tilbudt som ferdig salgsfeature

- **Exklusiv sekundær/makropad:** En signert, lisensiert og hardwarekontrollert filterdriver mangler. Normale tastinginntekter kan fortsette å nå Windows.
- Fullt hardwarebrytende av alle produsentadaptere samt ekte mobil-LAN-test.
- Riktig blandet monitor/DPI-utveksling og profesjonell avtak av alle nye oversettelser. PerMonitorV2 og simulert 150/200-Prozent-Layouts ble sjekket; ekte skjermstester forblir separate. Overflaten inneholder 27 språk; detaljerte nye tekster er ennå ikke fullstendig språklige.
- Signert salgsinstaller, endelige EULA, verifiserte SDK-salgsrettigheter og produktiv kjøpslicens/Checkout-integrasjon.

## detaljer og bevis

- [Bruksdokumentasjon](README.md)
- [Utvidelser: funksjoner, tester og åpne grenser](testing/2026-10-12-six-extensions.md)
- [Lokalt AI-assistent: Prototype og teststand](testing/2026-10-12-local-meeting-assistant.md)
- [Lokale AI-tid og modellforutsetninger](testing/2026-10-12-local-ai-runtimes.md)
- [DPI- og skalaundersøkelser](testing/2026-10-12-dpi-scaling.md)
- [Lisensundersøkelse](licensing/2026-10-12-commercial-license-audit.md) – historisk arkiv; det nye Publish-skriptet supplerer Runtime-instruksjoner. Den endelige pakken skal undersøkes separat.
- [Salgsplattformer](licensing/makrivo-vertriebsplattformen-2026-10-12.html) – Sammenlignende stand; Stripe er dessuten en mulig betalingsplattform, se aktuelle leverandørvilkår.

## Gjør et komplett utviklingspakke

```powershell
./tools/publish-complete.ps1 -SelfContained
```

Pakken inneholder skrivebordsprogram, UI-automatiseringsarbeider, mobilhost, nettleserfiler, eksempelplugin og runtime-lisensiering. Byggende fakta blir ikke sjekket inn i Git. En GitHub kildekode er ennå ikke publisert Windows installatør.
