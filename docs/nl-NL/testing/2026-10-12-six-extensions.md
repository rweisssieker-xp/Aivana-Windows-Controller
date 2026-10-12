<!-- generated-doc locale=nl-NL source=docs/testing/2026-10-12-six-extensions.md sha256=ff2eb755b2f18dce8c7bd7c832018ae71ee706bcca65e37c191dbb55ff999c1e -->

> Machinaal ondersteunde vertaling. Structuur, codevoorbeelden en links worden gecontroleerd; dit is geen taalkundige controle door een moedertaalspreker of juridische goedkeuring. Historische rapporten behouden hun oorspronkelijke datum en reikwijdte. [Originele bron](../../testing/2026-10-12-six-extensions.md)

# Zes uitbreidingen: implementatie en testbank

Status: 12. Oktober 2026, vestiging `codex/six-extensions`. Werkende status, geen productievrijgave.

## Dienst

Het opdrachtenpalet opent de items **Werkstation instellen**, **Toetsenbordfabrikant en SDK's**, **Mobiel paneel**, **EExtensies**, **UI automatisering**, **UI recordstroom** en **Uitvoeringstest**.

- Workspace Assistant maakt werkplek-, vergader-, streaming-, onderzoeks- en projectprofielen. Programma's uit het Register/Startmenu of Bestandskiezer, URL's, Uitvoer/Scherm en OBS Scène worden als stappen weergegeven voordat ze worden opgeslagen. De inrichting voert geen handelingen uit.
- In de stappeneditor schakelt **Check result** de voorafgaande controle en de controle achteraf in. Geschiedenis en mobiel panel onderscheiden daadwerkelijke bevestiging van niet-verifieerbaar effect. Relatieve wijzigingen, sneltoetsen en tekstinvoer bieden geen algemene bevestiging van de effectiviteit. Bewijs van het proces bewijst niet dat de toepassing technisch gereed is.
- UI Automation maakt gebruik van een afzonderlijke, in de tijd beperkte medewerker. Selectie via muispositie, selectiecontrole, Invoke/Select/Toggle/Focus/SetValue/WaitState; meerdere treffers, uitgeschakelde doelen en wachtwoordvelden worden afgewezen. Opname legt ondersteunde klikacties en pauzes vast; Tekstinvoer wordt niet opgenomen. De toelating wordt vóór aanvaarding gecontroleerd.
- Extensies: ZIP-inspectie, hashes, schema, uitgeschakelde import, expliciet vertrouwen vóór activering, geïsoleerd proces met beperkte IPC, exporteren, deactiveren, verwijderen, vorige versie herstellen/verwijderen. Procesisolatie is geen sandbox: gedeelde plug-ins worden uitgevoerd met gebruikersrechten. Voorbeeldpakket `makrivo.example.zip` bevat aftel- en statusactie. Ondersteunde schema's vertegenwoordigen een gedocumenteerde eenvoudige JSON-schema-subset in de broncode; geen openbare marktplaats.
- Mobiel paneel: selecteer lokale IP- en gedeelde profielen, open het HTTPS-adres, controleer het certificaat met behulp van de weergegeven SHA-256-Fingerprints en vertrouw het bewust, voer vervolgens een eenmalige zescijferige code in. Privésleutels worden beschermd met Windows-DPAPI; alleen hashes van clienttoegang worden opgeslagen. Certificaten veranderen na verlenging of IP-wijziging. De firewall Windows wordt indien nodig door de gebruiker vrijgegeven. Geen automatische certificaatinstallatie, port forwarding of firewallwijziging. Klanttoegang kan worden ingetrokken. Het paneel toont de actiestatus en testgeschiedenis; het is nog geen universeel live-sensordashboard.

## Toetsenborden

| Fabrikant | Implementatie | Beperking |
|---|---|---|
| Corsair | bestaande iCUE-G-toetsen en RGB | Lokale iCUE-aansluiting en geschikt model vereist |
| Logitech G | G-Key SDK en aparte LED SDK | originele lokale x64-DLL expliciet vrijgeven; G-Key SDK biedt geen individuele identiteit van meerdere toetsenborden |
| Razer | Chroma RGB en standaard sleutelherkenning | Wijs eerder eigen sleutels toe in Synapse |
| SteelSeries | GameSense-RGB en standaard sleutelherkenning | wijs eerder eigen sleutels toe in GG |
| Wooting | Apparaatgerelateerde analoge waarden, druk-/vrijgavedrempels | origineel lokaal x64-SDK-DLL; normale HID-ingang blijft actief |
| Keychron/QMK/VIA/Ducky/ASUS/HyperX/Roccat/andere | Standaard fysieke knoppen per apparaat en scancode | Fn en eigen bedieningselementen zijn afhankelijk van het model |

Er zijn geen nieuwe eigen SDK-DLL's opgenomen. De nieuwe fabrikantadapters zijn nog niet hardwarematig gevalideerd zonder geschikte fysieke apparaten.

**Exclusief tweede macropad is niet gereed:** Raw Input observeert apparaten afzonderlijk, maar onderdrukt hun normale Windows-invoer niet. Voor een productierelease is een gelicentieerde, ondertekende filterdriver vereist, geverifieerd onder Windows 11/Secure Opstart- en noodontgrendelings-, crash-, opschortings- en opnieuw verbindingstests. Er is geen stuurprogramma geïnstalleerd. De applicatie markeert deze limiet expliciet.

## Examens

- Releasebuild: succesvol, geen waarschuwingen/fouten.
- Kernconsole: Alle testgroepen slagen, inclusief conflicten met leverancierssleutels, het terugdraaien van plug-ins en daadwerkelijke uitvoeringsfouten in de testgeschiedenis.
- Extensie-integratie: echte UIA-werknemer met WPF-testapplicatie; unieke aanroep, dubbelzinnigheid, uitgeschakelde doelen, wachtwoorduitsluiting. Real-Named Pipe-plug-inproces, protocolgrenzen, analoge hysteresis en Logitech ABI-decodering succesvol.
- Echte HTTPS-host: vastgezet certificaat, ongepaarde toegang, valse/unieke code, oorsprongsbescherming, browserbestanden, hash-opslag en beveiligde sleutel succesvol. Een Schannel-bug met kortstondige sleutels die tijdens het testen werd ontdekt, is opgelost.
- Windows suite met expliciet weggelaten succestest op de voorgrond: succesvol; Audio/OBS/iCUE/Raw Input en schermstatus gecontroleerd. In de vorige run mislukte de volledige suite omdat de focusschakelaar naar het geïsoleerde testvenster werd geweigerd. De daaropvolgende run naar het AI-prototype omvatte ook de volledige suite inclusief focusschakeling en een aangesloten iCUE-toetsenbord. Een andere herhalingsrun mislukte opnieuw vanwege de verandering in focus; De contextafhankelijke toets is dus niet stabiel groen. Deze test werd niet verzwakt. DDC-leestoegangen die korte tijd mislukken, worden gedurende een beperkte tijd herhaald; er is geen helderheid gewijzigd.
- WPF-Smoke: alle vorige en nieuwe dialoogvensters weergegeven, configuratie-invariantie, editor en CRUD succesvol; Schermafbeeldingen in buildmap `smoke`.
- Browser: Toneelschrijver met Edge, 390×844 en 1024×768, koppeling van flow/UUID/eenmalig verzenden/geen horizontale overflow succesvol. API-mock; echte HTTPS-test afzonderlijk. Foto's op `artifacts/mobile-browser`. Browserplug-in was niet beschikbaar, vandaar de reguliere terugval van Playwright.
- Onafhankelijke codebeoordeling uitgevoerd; Vertraagde mobiele opdrachten, ontbrekende terminalcontroleresultaten, procespadcontrole, terugdraaien van plug-ins en opnieuw opstarten van RGB-hartslag opgelost.

Nog open: exclusieve stuurprogramma-integratie, fysieke LAN-testen van fabrikant/telefoon/tablet, Windows schaling 150/200 procent en volledige vertalingscontrole van alle nieuwe dialoogteksten. Nieuwe dialoogteksten zijn overwegend Duits; Bestaande taalcatalogi blijven behouden. Daarom is de algemene eis ** niet volledig aanvaard**.

## Pakket genereren

`./tools/publish-complete.ps1 -SelfContained` maakt een nieuwe map aan onder `artifacts/release-*`, inclusief desktop, UIA-werker, HTTPS-host, browserbestanden, importeerbare voorbeeldplug-in en runtime-licentie-opmerkingen. De geïnstalleerde applicatie wordt niet overschreven. Zonder `-SelfContained` moet het pakket overeenkomen met .NET-10-Desktop-/ASP.NET-Runtimes.

Het pakket is een lokaal testbed en geen ondertekend retail-installatieprogramma. Eigen gebruiksvoorwaarden en SDK-distributierechten zijn afzonderlijke releases; zie bestaande documentatie onder `docs/licensing`.
