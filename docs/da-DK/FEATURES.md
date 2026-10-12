<!-- generated-doc locale=da-DK source=docs/FEATURES.md sha256=ea712f39457ce69f772f09a1294201705a5f36a92a8bf15f356bd80132c3e723 -->

> Maskinassisteret oversættelse. Struktur, kodeeksempler og links kontrolleres; dette er ikke en gennemgang af en modersmålstalende eller en juridisk godkendelse. Historiske rapporter bevarer deres oprindelige datoer og omfang. [Oprindelig kilde](../FEATURES.md)

# MAKRIVO – en oversigt over

Producenterne: **Aivana**. Stand: **12. Oktober 2026**. Dette er udviklingsstatus, ingen salg eller hardware frigivelse.

## Windows - Styring af

- Kachler, globale hotkeys, kommandoer, favoritter og applikationsafhængige profiler.
- Makroer med flere skridt, gentagelser, pause, afbrydelse og forudsigelse.
- Åbn programmer, filer og websteder; Brug tekst, mellemrum og knap.
- Anordne vinduer, blokere Windows, styre lydudgang og mikrofoner samt ændre skærmlys via støttede grænser.
- OBS-WebSocket-v5-Anbindung til scener, optagelse, streaming og lyd.
- Formler, arbejdsplads assistent, enhed genkendelse og tastatur overlay.
- Import og eksport af profilkonfigurationer; konstruktionsundersøgelse og konfliktdetektion.
- Lokalt gemt indstillinger; ingen konto er nødvendig for daglig brug.

## Udvidede funktioner i den nuværende kildekode

| Området | Funktionelle omfang | Grænserne |
| --- | --- | --- |
| Resultatundersøgelse | Forundersøgelse, efterkontrol og testprocedure | Kun verificable effekter bekræfter; et vellykket tastatur beviser ikke målvirkningen. |
| Arbejdsstilassistent | Forbered profiler og procedurer fra udvalgte programmer, filer, URL'er og andre handlinger | Forudsigelse og opbevaring er nødvendigt; Anlægget udfører intet. |
| Makroindtagelse og Workflows | Optagelse, betingelser, gestus og lokal profilsynkronisering | Adfærd og grænser er dokumenteret i gennemførelses- og testrapporter. |
| UI-automation | Vælg målsætninger, tjek selektorer, indtagelse af handlinger og begrænsede klik | Separaterede arbejdere; flere, deaktiverede og adgangskoder afvises; ingen generel tekstindtagelse. |
| Udvidelser | Prøve, importere, aktivere, eksportere og returnerer pakker efter udtrykkelig tillid | Processisolering er ikke en sandboks; ingen offentlig pluginsmarked. |
| Mobilpanel | Lokalt HTTPS-panel, kopiering, udvalgte profiler og status-/testhistorie | Virkelig telefon/tablet-LAN indtagelse er stadig åben; ingen universel sensor dashboard. |
| Producentadapter | Corsair, Logitech-G-SDK, Razer/SteelSeries-RGB og Wooting-Analogadapter | Modeller/SDK-afhængig; nye adaptere ikke fuldt valideret med real hardware. Ingen nye proprietære SDK-DLL'er er bundet. |
| Fysisk tastatur | Standardknapper ved hjælp af enheden og skanningskode | Raw Input undertrykker ikke den normale Windows indtagelse. |
| Lokalt AI-rådgivere | Optionelle prototyper for Ollama, LM Studio, llama.cpp og ONNX Runtime GenAI CPU genererer begrænsede design fra udtrykkeligt udvalgte ressourcer | Standard ud; indfødte CPU-drift testet med tilfældig model, men ingen dokumenteret planlægning kvalitet eller tid besparelse; ingen model vægt. |

## Ikke tilbudt som færdig salg

- **Exklusiv anden computer/makropad: ** En undertegnet, licenseret og hardware-kontrolleret filterdrivende mangler. Normale tastaturindtægter kan fortsætte med at nå Windows.
- Fuld hardware aftagelse af alle producenter adaptere samt virkelige mobile enheder LAN-test.
- Virkelige blandede monitor-/DPI-ændringer og faglige aftagelser af alle nye oversættelser. PerMonitorV2 og simuleret 150/200-Prozent-Layouts er blevet testet; virkelige displaytest forbliver separate. Oversigtet indeholder 27 sprog; detaljerede nye tekster er endnu ikke fuldt ud afskaffet.
- Signeret salgsinstaller, den endelige EULA, kontrollerede SDK-salgsrettigheder og produktive købs- / check-out integration.

## Details og beviser

- [Brugerdokumentation](README.md)
- [Udvidelser: funktioner, tests og åbne grænser](testing/2026-10-12-six-extensions.md)
- [Lokalt AI-assistent: Prototype og teststand](testing/2026-10-12-local-meeting-assistant.md)
- [Lokale KI-tid og modelforudsætninger](testing/2026-10-12-local-ai-runtimes.md)
- [DPI- og skaleringstest](testing/2026-10-12-dpi-scaling.md)
- [Licensundersøgelse](licensing/2026-10-12-commercial-license-audit.md) Historisk arkivering; det nye Publish-skript supplerer Runtime-instruktioner. Den endelige pakke skal undersøges separat.
- [Salgplatforme](licensing/makrivo-vertriebsplattformen-2026-10-12.html) – Sammenlignende status; Stripe er yderligere en mulig betalingsplatform, se de aktuelle leverandørvilkår.

## Opret et komplet udviklingspakke

```powershell
./tools/publish-complete.ps1 -SelfContained
```

Pakken indeholder Desktop-app, UI-automation arbejdere, mobilhost, browserfiler, eksempelplugin og runtime licensanvisninger. Konstruktionsfakter bliver ikke gennemgået i Git. En GitHub kildekode er endnu ikke offentliggjort Windows installator.
