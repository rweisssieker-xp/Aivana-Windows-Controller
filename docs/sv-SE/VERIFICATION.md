<!-- generated-doc locale=sv-SE source=docs/VERIFICATION.md sha256=f19afb9e7b5ffac90c83167416ab3af0964a4481dd16b7357f8585480c6aae65 -->

> Maskinassisterad översättning. Struktur, kodexempel och länkar kontrolleras; detta innebär inte granskning av en modersmålstalare eller juridiskt godkännande. Historiska rapporter behåller sina ursprungliga datum och omfattning. [Originalkälla](../VERIFICATION.md)

# Prövning – 8. oktober 2026

[English (US)](../en-US/VERIFICATION.md) | [Deutsch](../de-DE/VERIFICATION.md) | [Français](../fr-FR/VERIFICATION.md) | [Italiano](../it-IT/VERIFICATION.md)

Applikationen byggdes under Windows x64 med den stabila .NET-SDK 10.0.202 och publicerades som en självständig Windows-x64-Ausgabe.

## Automatiskt kontrollerat

- Allmän konstruktion: Inga varningar och inga misstag.
- 19 Core beteendest: standardkonfiguration, validering, noll/duplikatdata, profilprioritet, import/export och säkerhet återställning, makroordning och upprepning, avbrott, dubbelutveckling, serialisering, oförändrad genomförande snapshot och stegfel.
- Windows-tester: Hotkey-normalisering och Aliasnamn, inhemska strukturstorlekar, verkliga tangentbordslistan, säker registrering och återgivning, syntetisk WM_HOTKEY för klackar och avbrott, inaktiverad klackar registrering under inmatningssimulering.
- Den oberoende EXE startades med `--smoke-test`: alla fem WPF-visningar kan återställas, lås kan installeras/kopieras/avlägsnas, mönster förblir kvar när de återställs, konfiguration sparas och laddas upp igen. Denna test använder en isolerad tillfällig konfigurationsdiagram och utför inga text- eller medieåtgärder.
- Dashboard, redigerare och kompakta utställningar (900 × 720) har visuellt testats som verkliga WPF-renderings. Design lagring, avslag, kål-/makro-ordningen, tyska hotkey-alliansen, enskilda makroavbrott och steg framåt har kontrollerats.

## Hardwareberoende gränser

Den senaste lokala spåren upptäckte fyra Windows tangentgränssnitt. Detta nummer beskriver HID/Windows gränssnitt, inte nödvändigtvis fyra fysiska tangentbord.

Den officiella iCUE SDK är installerad och ligger bredvid EXE. Live-testet ansluter sig till iCUE och identifierar en Corsair-tastatur med modell- och LED-information. Belysningsändringar har inte utförts automatiskt; modellberoende svetsnycklar och andra tillverkare har inte testats. Generiska hotkeys gäller mellan tangentbord.

Text/medieinspelningar injiceras inte i andra öppna användarprogram. De inhemska datastrukturerna och fel/fri logik har granskats; beteendet i ett specifikt målprogram beror bland annat på dess behörighet.

## Upprepa

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64
& ./artifacts/win-x64/MAKRIVO.exe --smoke-test
```

GUI-testet skriver resultat och skärmdumpar efter `smoke` bredvid den kontrollerade EXE. Det är tänkt som en diagnosmodus och börjar inte en permanent bakgrundsinstans efter avslutad.


## Utökad kontroll

Dessutom kontrolleras: Profilförhållanden med process och fönstretitel, variabler utan rekursiv expansion, makroövervakning utan genomförande, ny åtgärdsvalidering och nedåtkompatibilitet av gamla JSON-filer. Native test täcker kommandospaletten med registrering, konflikter, WM_HOTKEY-utskjutning och släppande samt ren skärmdelning och behovsbaserad läsning. Testet WPF öppnar och returnerar den faktiska paletten, undersöker återhämtning/återhämtning och favoriter, kontrollerar rena design efter retur och avslutar processen framgångsrikt.

Windows blockering, mellanliggande ändringar och fönsterrörelser tillämpas inte på användarprogram under testerna. Fönstret åtgärder postar Windows-förfrågningar asynkron; det konkreta målprogrammet kan ha sina egna minimikrav eller behörighet gränser. Profiler och statistik gäller för den pågående sessionen.


Utvidgningsundersökning från 12.10.2026: 30 Kernprov har genomgått; Windows-Adapter, inklusive avlägsnande under injektion, skrivet skyddad monitorns upptäckt och OBS-Status-Mock överlevde. WPF Kontrollera uttag/upptag, rekordstart/stopp, profilutbyte förhandsvisning, gest och symbolresurser.: 27 Kataloger med 967 Nycklar - Dokumentation: 84 sidor på fyra språk. ägare knappar av andra tillverkare än Corsair har inte testats på hårdvara; den generiska stödet omfattar enhetsspecifika standard händelser F13–F24 molntransport sker via en markerad synkroniserad mapp; import kontrolleras och förblir inaktiverad.
