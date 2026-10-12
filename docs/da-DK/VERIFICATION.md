<!-- generated-doc locale=da-DK source=docs/VERIFICATION.md sha256=f19afb9e7b5ffac90c83167416ab3af0964a4481dd16b7357f8585480c6aae65 -->

> Maskinassisteret oversættelse. Struktur, kodeeksempler og links kontrolleres; dette er ikke en gennemgang af en modersmålstalende eller en juridisk godkendelse. Historiske rapporter bevarer deres oprindelige datoer og omfang. [Oprindelig kilde](../VERIFICATION.md)

# Undersøgelse – 8. Oktober 2026

[English (US)](../en-US/VERIFICATION.md) | [Deutsch](../de-DE/VERIFICATION.md) | [Français](../fr-FR/VERIFICATION.md) | [Italiano](../it-IT/VERIFICATION.md)

Applikationen blev bygget under Windows x64 med den stabile .NET-SDK 10.0.202 og udgivet som en selvstændig Windows-x64-Ausgabe.

## Automatisk kontrolleret

- Overordnet konstruktion: ingen advarsler og ingen fejl.
- 19 Core adfærdstest: Standardkonfiguration, validering, nul-/duplikatdata, profilprioritet, import/eksport og sikkerhed genopretning, makroreord og gentagelse, afbrydelse, dobbeltudløsning, serialisering, uændret udførelsesnapshot og trinfejl.
- Windows test: Hotkey standardisering og alia, indfødte strukturstørrelser, virkelige tastatur enhedsliste, sikker registrering og genopretning, syntetisk WM_HOTKEY for kager og nedbrydning, deaktiveret kager registrering under input simulering.
- Den uafhængige EXE blev lanceret med `--smoke-test`: alle de fem WPF synspunkter kan tilbagekaldes, lukker kan indstilles/kopieres/slås, designene bliver opbevaret ved refresh, konfigurationen lagres og lades igen. Denne test bruger en isoleret midlertidig konfigurationsliste og udfører ingen tekst- eller medieaktiviteter.
- Dashboard, editor og kompakte udstillinger (900 × 720) blev visuelt testet som virkelige WPF-renderings. Designet lagring, afvisning, kachel-/makro-ordningen, tyske hotkey-allias, individuelle makroavbrud og skridt fremskridt er blevet testet.

## Hardware-afhængige grænser

Den sidste lokale gennemgang opdagede fire Windows-tastaturgrænseflader. Dette tal beskriver HID-/Windows-grænseflade, ikke nødvendigvis fire fysiske tastaturer.

Den officielle iCUE SDK er installeret og ligger ved siden af EXE. Live-testen forbinder sig til iCUE og genkender en Corsair-tastatur med model- og LED-oplysninger. Belysningsændringer er ikke automatisk foretaget; modellerne og andre producenter er ikke testet med hardware. Generiske hotkeys gælder tværfagligt.

Tekst/medieindtægter er ikke injiceret i andre åbne brugerprogrammer. Native datastrukturer og fejl/frigabelogik er blevet undersøgt; adfærd i et bestemt målprogram afhænger blandt andet af dens berettigelsesniveau.

## Gentag

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64
& ./artifacts/win-x64/MAKRIVO.exe --smoke-test
```

GUI-testen skriver resultatet og skærmbilledet efter `smoke` ved siden af den kontrollerede EXE. Det er beregnet som en diagnose og starter ikke en permanent baggrundsindsats efter afslutningen.


## Udvidet kontrol

Yderligere kontrolleret: profilbetingelser med proces og vindue titler, variabler uden rekursiv udvidelse, makrovisning uden udførelse, ny aktionsvalidering og nedadgående kompatibilitet af gamle JSON-filer. Native tests dækker kommandepaletten med registrering, konflikter, WM_HOTKEY-udlevering og frigivelse samt ren monitoruddeling og behovstyret læsning. Testet WPF åbner og returnerer det faktiske sortiment, kontrollerer tilbagevendende / genoptagelse og favoritter, kontrollerer rene design efter returnering og afsluttes processen med succes.

Windows blokering, mellemliggende ændringer og vindu bevægelser blev ikke anvendt på brugere under testen. Windows-aktioner sender Windows anmodninger asynkron; det konkrete målprogram kan have deres egne minimumsstørrelser eller tilladelsesgrænser. Profilsæt og statistikker gælder for den pågældende session.


Udvidelsesundersøgelse af 12.10.2026: 30 Core-prøverne er gået igennem; Windows-Adapter, herunder frigivelse under indsprøjtning, skrivet monitoreregning og OBS-Status-Mock ophører. WPF overvågning af udløb/optagelse, rekordstart/stop, profiludveksling, gestus og symbolressourcer. lokalisering: 27 Kataloger med 967 Nøgler – dokumentation: 84 sider i fire sprog. ejere knopper af andre producenter som Corsair har ikke været testet på hardware; den generiske support omfatter enhedspecifikke standardhændelser F13–F24 Cloud-transport foregår via en valgt synkroniseret mappe; Importen kontrolleres og bliver deaktiveret.
