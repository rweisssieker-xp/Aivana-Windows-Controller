<!-- generated-doc locale=nl-NL source=docs/VERIFICATION.md sha256=f19afb9e7b5ffac90c83167416ab3af0964a4481dd16b7357f8585480c6aae65 -->

> Machinaal ondersteunde vertaling. Structuur, codevoorbeelden en links worden gecontroleerd; dit is geen taalkundige controle door een moedertaalspreker of juridische goedkeuring. Historische rapporten behouden hun oorspronkelijke datum en reikwijdte. [Originele bron](../VERIFICATION.md)

# Examen – 8. Oktober 2026

[English (US)](../en-US/VERIFICATION.md) | [Deutsch](../de-DE/VERIFICATION.md) | [Français](../fr-FR/VERIFICATION.md) | [Italiano](../it-IT/VERIFICATION.md)

De applicatie is gebouwd op Windows x64 met de stabiele .NET SDK 10.0.202 en uitgebracht als een standalone Windows-x64-Ausgabe.

## Automatisch gecontroleerd

- Algemene opbouw: geen waarschuwingen en geen fouten.
- 19 Kerngedragstests: standaardconfiguratie, validatie, nul/dubbele gegevens, profielprioriteit, import/export en back-upherstel, macrovolgorde en nieuwe poging, afbreken, dubbele trigger, serialisatie, onveranderlijke momentopname van uitvoering en stapfout.
- Windows-controle: sneltoetsnormalisatie en aliassen, oorspronkelijke structuurgroottes, lijst met echte toetsenbordapparaten, beveiligde registratie en opnieuw delen, synthetische WM_HOTKEY voor tegels en annuleren, tegelregistratie uitgeschakeld tijdens invoersimulatie.
- De standalone EXE werd gestart met `--smoke-test`: alle vijf de WPF-weergaven worden weergegeven, tegels kunnen worden gemaakt/gekopieerd/verwijderd, concepten blijven behouden tijdens het vernieuwen, de configuratie wordt opgeslagen en opnieuw geladen. Deze test maakt gebruik van een geïsoleerde tijdelijke configuratiemap en voert geen tekst- of media-acties uit.
- Dashboard, editor en compacte weergave (900 × 720) werden visueel geïnspecteerd als echte WPF-weergaven. Concepten opslaan, weggooien, tegel-/macrovolgorde, Duitse sneltoetsaliassen, individuele macro-annuleringen en stapvoortgang worden gecontroleerd.

## Hardware-afhankelijke limieten

Bij de laatste lokale run zijn vier Windows-toetsenbordinterfaces gedetecteerd. Dit nummer beschrijft HID/Windows-interfaces, niet noodzakelijkerwijs vier fysieke toetsenborden.

De officiële iCUE SDK is geïnstalleerd en bevindt zich naast de EXE. De live test maakt verbinding met iCUE en detecteert een Corsair-toetsenbord met model- en LED-informatie. Verlichtingsveranderingen werden niet automatisch geactiveerd; Modelafhankelijke speciale sleutels en andere fabrikanten zijn niet op hardware getest. Algemene sneltoetsen zijn van toepassing op alle toetsenborden.

Tekst-/media-invoer werd niet in andere open user agents geïnjecteerd. De oorspronkelijke datastructuren en fout-/vrijgavelogica werden gecontroleerd; Het gedrag in een specifiek doelprogramma is onder meer afhankelijk van het autorisatieniveau ervan.

## Herhaal

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64
& ./artifacts/win-x64/MAKRIVO.exe --smoke-test
```

De GUI-test schrijft resultaat en screenshot naar `smoke` naast de geteste EXE. Het is bedoeld als diagnostische modus en start na voltooiing geen persistente achtergrondinstantie.


## Geavanceerde bedieningselementen

Daarnaast gecontroleerd: profielvoorwaarden met proces- en venstertitel, vervanging van variabelen zonder recursieve uitbreiding, macro-preview zonder uitvoering, validatie van nieuwe acties en achterwaartse compatibiliteit van oude JSON-bestanden. Native controles omvatten het commandopalet met registratie, conflicten, verzending en vrijgave van WM_HOTKEY, evenals pure monitorsplitsing en on-demand leestoegang tot het klembord. De WPF-test opent en rendert het daadwerkelijke palet, controleert ongedaan maken/opnieuw uitvoeren en favorieten, controleert schone concepten na het renderen en voltooit het proces met succes.

Windows-vergrendeling, klembordwijzigingen en vensterbewegingen werden tijdens het testen niet toegepast op user-agents. Postvensteracties Windows verzoeken asynchroon; Het specifieke doelprogramma kan zijn eigen minimumgroottes of autorisatielimieten hebben. Profielvergrendeling en uitvoeringsstatistieken zijn van toepassing op de huidige sessie.


Verlengingsexamen van 12.10.2026: 30 kernexamens geslaagd; Windows-adapter inclusief vrijgave tijdens invoerinjectie, alleen-lezen monitordetectie en OBS-statusmock geslaagd. WPF controleert stroom vastleggen/annuleren, recorder starten/stoppen, voorbeeld van profieluitwisseling, gebaren en symboolbronnen. Lokalisatie: 27-catalogi elk met 967-sleutels; Documentatie: 84-pagina's in vier talen. Eigen knoppen anders dan Corsair zijn niet getest op hardware; Algemene ondersteuning omvat apparaatspecifieke standaardgebeurtenissen F13–F24 en mediasleutels. Cloudtransport vindt plaats via een geselecteerde gesynchroniseerde map; Importeren wordt gecontroleerd en blijft uitgeschakeld.
