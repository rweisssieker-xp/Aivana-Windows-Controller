<!-- generated-doc locale=nl-NL source=docs/FEATURES.md sha256=ea712f39457ce69f772f09a1294201705a5f36a92a8bf15f356bd80132c3e723 -->

> Machinaal ondersteunde vertaling. Structuur, codevoorbeelden en links worden gecontroleerd; dit is geen taalkundige controle door een moedertaalspreker of juridische goedkeuring. Historische rapporten behouden hun oorspronkelijke datum en reikwijdte. [Originele bron](../FEATURES.md)

# MAKRIVO – Functieoverzicht

Fabrikant: **Aivana**. Status: **12. Oktober 2026**. Dit is de ontwikkelingsstatus, geen verkoop- of hardwarerelease.

## Windows-controle

- Tegels, globale sneltoetsen, opdrachtpalet, favorieten en applicatie-afhankelijke profielen.
- Macro's in meerdere stappen, herhalen, pauzeren, annuleren en bekijken.
- Programma's, bestanden en websites openen; Gebruik tekst-, klembord- en toetsenbordsneltoetsen.
- Rangschik vensters, vergrendel Windows, bedien audio-uitgangen en microfoons en wijzig de schermhelderheid via ondersteunde interfaces.
- OBS-WebSocket-v5-Anbindung voor scènes, opname, streaming en audio.
- Sjablonen, Werkplekassistent, Apparaatdetectie en Toetsenbordoverlay.
- Import en export van profielconfiguraties; Ontwerpbeoordeling en conflictdetectie.
- Lokaal opgeslagen instellingen; geen account vereist voor dagelijks gebruik.

## Geavanceerde functies in de huidige broncode

| gebied | Bereik van functies | Grenzen |
| --- | --- | --- |
| Resultatencontrole | Pre-inspectie, post-inspectie en testgeschiedenis | Bevestig alleen verifieerbare effecten; Een succesvolle toetsaanslag bewijst niet het doeleffect. |
| Werkplaats assistent | Bereid profielen en processen voor van geselecteerde programma's, bestanden, URL's en andere acties | Voorbeeld en opslaan vereist; Instellen doet niets. |
| Macro-opname en workflows | Opname, voorwaarden, gebaren en lokale profielsynchronisatie | Gedrag en grenzen worden gedocumenteerd in de implementatie- en testrapporten. |
| UI-automatisering | Selecteer doelen, controleer selectors, registreer acties en beperkte klikreeksen | Aparte werknemer; dubbelzinnige, uitgeschakelde en wachtwoorddoelen worden afgewezen; geen algemene tekstinvoeropname. |
| Extensies | Pakketten inspecteren, importeren, activeren op basis van expliciet vertrouwen, exporteren en terugdraaien | Procesisolatie is geen sandbox; geen openbare plug-inmarktplaats. |
| Mobiel paneel | Lokaal HTTPS-paneel, koppeling, geselecteerde profielen en status-/auditgeschiedenis | Echte LAN-acceptatie voor telefoons/tablets is nog steeds open; geen universeel sensordashboard. |
| Fabrikant adapter | Corsair, Logitech-G-SDK, Razer/SteelSeries-RGB en Wooting analoge adapter | Afhankelijk van model/SDK; nieuwe adapters niet volledig gevalideerd met echte hardware. Geen nieuwe eigen SDK-DLL's gebundeld. |
| Fysieke toetsenbordtoewijzing | Herken standaardsleutels op basis van apparaat- en scancode | Raw Input onderdrukt de normale Windows-invoer niet. |
| Lokale AI-vergaderassistent | Optioneel prototype voor Ollama, LM Studio, llama.cpp en ONNX Runtime GenAI CPU produceert beperkte ontwerpen van expliciet geselecteerde bronnen | Standaard uitgeschakeld; native CPU-runtime getest met willekeurig model, maar geen bewezen planningskwaliteit of tijdbesparing; geen modelgewichten inbegrepen. |

## Nog niet beschikbaar als voltooide verkoopfunctie

- **Exclusief secundair toetsenbord/macropad:** Er ontbreekt een ondertekend, gelicentieerd en op hardware getest filterstuurprogramma. Normale toetsaanslagen kunnen nog steeds Windows bereiken.
- Volledige hardwareacceptatie van alle adapters van de fabrikant, evenals echte LAN-tests voor mobiele apparaten.
- Echte gemengde monitor-/DPI-wijzigingen en technische goedkeuring van alle nieuwe vertalingen. PerMonitorV2 en gesimuleerde 150/200-Prozent-Layouts werden gecontroleerd; Echte displaytests blijven gescheiden. De interface biedt 27-talen; Gedetailleerde nieuwe teksten zijn taalkundig nog niet volledig goedgekeurd.
- Ondertekend installatieprogramma voor de detailhandel, definitieve EULA, geverifieerde SDK-distributierechten en productieve aankooplicentie/afrekenintegratie.

## Details en bewijs

- [Gebruiksdocumentatie](README.md)
- [Uitbreidingen: functies, tests en open grenzen](testing/2026-10-12-six-extensions.md)
- [Lokale AI-assistent: prototype en teststand](testing/2026-10-12-local-meeting-assistant.md)
- [Lokale AI-duur en modelvoorwaarden](testing/2026-10-12-local-ai-runtimes.md)
- [DPI- en schaalbeoordeling](testing/2026-10-12-dpi-scaling.md)
- [Licentie controle](licensing/2026-10-12-commercial-license-audit.md) – historische opname; het nieuwe Publish-script aanvult de runtime-invisie. Het definitieve pakket blijft gescheiden te controleren.
- [Verkoopplatforms](licensing/makrivo-vertriebsplattformen-2026-10-12.html) – vergelijkingsstatus; Stripe is bovendien een mogelijke betalingsplatform, zie de huidige aanbiedervoorwaarden.

## Genereer een compleet ontwikkelingspakket

```powershell
./tools/publish-complete.ps1 -SelfContained
```

Het pakket bevat een desktop-app, UI-automatiseringswerker, mobiele host, browserbestanden, voorbeeldplug-in en runtime-licentie-opmerkingen. Build-artefacten worden niet ingecheckt in Git. Een GitHub-broncodeversie is nog geen gepubliceerd Windows-installatieprogramma.
