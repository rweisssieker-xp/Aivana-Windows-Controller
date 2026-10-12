<!-- generated-doc locale=nb-NO source=docs/VERIFICATION.md sha256=f19afb9e7b5ffac90c83167416ab3af0964a4481dd16b7357f8585480c6aae65 -->

> Maskinassistert oversettelse. Struktur, kodeeksempler og lenker kontrolleres; dette innebærer ikke gjennomgang av en morsmålsbruker eller juridisk godkjenning. Historiske rapporter beholder sine opprinnelige datoer og omfang. [Originalt kilde](../VERIFICATION.md)

# Prøve – 8. Oktober 2026

[English (US)](../en-US/VERIFICATION.md) | [Deutsch](../de-DE/VERIFICATION.md) | [Français](../fr-FR/VERIFICATION.md) | [Italiano](../it-IT/VERIFICATION.md)

Applikasjonen ble bygget under Windows x64 med den stabile .NET-SDK 10.0.202 og publisert som en selvstendig Windows-x64-Ausgabe.

## Automatisk sjekket

- Totalbygning: Ingen advarsler og ingen feil.
- 19 Core-behandlingsstester: Standardkonfigurasjon, validering, null/duplikatdata, profilprioritet, import/eksport og sikkerhetsoppretting, makrorefølge og gjenta, avbrudd, dobbeltutløsning, serialisering, uendret utførelsesnappshot og trinnfeil.
- Windows-testen: Hotkey-normalisering og alia, innfødte strukturstørrelser, ekte tastatur-enhetsliste, sikker registrering og tilbakekalling, syntetisk WM_HOTKEY for kjel og avbrudd, deaktivert kjel registrering under innsimulering.
- Den uavhengige EXE ble lansert med `--smoke-test`: alle fem WPF-visjoner kan bli installert/kopieret/slettet, designene blir oppbevart når de er refreshert, konfigurasjonen lagres og lastes opp igjen. Denne testen bruker en isolert midlertidig konfigurasjonsdiagram og utfører ingen tekst- eller medieaksjoner.
- Dashboard, redaktør og kompakt bilde (900 × 720) ble visuelt testet som ekte WPF-renderings. Utformingsbevaring, avvisning, makro-ordre, tysk hotkey allianse, individuelle makroavbrudd og skritt fremover er testet.

## Hardwareavhengige grenser

Den siste lokale gjennomgangen oppdaget fire Windows tastaturgrensesnitt. Dette tallet beskriver HID-/Windows-grensesnitt, ikke nødvendigvis fire fysiske tastaturer.

Den offisielle iCUE SDK er installert og ligger ved siden av EXE. Live-testet kobler seg til iCUE og gjenkjenner en Corsair-tastatur med modell- og LED-informasjon. Belysningsendringer ble ikke utført automatisk; modellavhengige sonde-tasten og andre produsenter er ikke testet med maskinvare. Generiske hotkeys gjelder tastaturoverskridende.

Tekst/medieinntekter er ikke injisert i andre åpne brukere. Native datastrukturer og feil/fri logikk har blitt undersøkt, og atferd i et bestemt målprogram avhenger blant annet av dets godkjennelsesnivå.

## Gjenta

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64
& ./artifacts/win-x64/MAKRIVO.exe --smoke-test
```

GUI-testen skriver resultat og skjermbilder etter `smoke` ved siden av den verifiserte EXE. Det er ment som diagnostisk modus og starter ikke en permanent bakgrunnsinstans etter fullføring.


## Utvidet kontroll

Videre sjekket: Profilvilkår med prosess og vinduinnlegg, variabler uten rekursiv ekspansjon, makroutvikling uten utførelse, ny handling validering og nedverdig kompatibilitet av gamle JSON-filer. Native tester dekker kommandoen med registrering, konflikter, WM_HOTKEY-utgivelse og utgivelse, samt ren skjermutdeling og behovsbasert clipboard-lesing. Testet WPF åpner og returnerer den faktiske paletten, tester tilbakevendende/återvinning og favoritter, tester rene design etter returnering og avsluttes prosessen med suksess.

Windows-blokk, mellomliggende endringer og vindubevegelser ble ikke brukt på brukere under testen. Fensteraksjoner publiserer Windows-forespørsler asynkron; det konkrete målprogrammet kan ha sine egne minimumsstørrelser eller tillatelsesgrenser. Profilsamling og statistikk gjelder for den pågående møtet.


Utvidelsesundersøkelser fra 12.10.2026: 30 Core testene er gjennomgått; Windows-Adapter, inkludert frigjøring under injeksjon, skriveskyttet monitor gjenkjenning og OBS-Status-Mock overlevde. WPF sjekker utløpsinntak/avbrudd, rekordstart/stopp, profilutveksling forhåndsvisning, gest og symbolressurser.: 27 Kataloger med 967 Nøkkelen; dokumentasjon: 84 Sider i fire språk. eiere knopper av andre produsenter som Corsair har ikke blitt testet på maskinvare; generisk støtte inkluderer enhetspesifikke standard hendelser F13–F24 Cloud-transport skjer gjennom en valgt synkronisert mappe; Importen blir sjekket og deaktivert.
