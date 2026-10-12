<!-- generated-doc locale=nb-NO source=docs/superpowers/plans/2026-10-08-controller-polish.md sha256=1a076a67eea31048f6305f30a121c474d592451d1ddcd9b46bc339fd8ec068f2 -->

> Maskinassistert oversettelse. Struktur, kodeeksempler og lenker kontrolleres; dette innebærer ikke gjennomgang av en morsmålsbruker eller juridisk godkjenning. Historiske rapporter beholder sine opprinnelige datoer og omfang. [Originalt kilde](../../../superpowers/plans/2026-10-08-controller-polish.md)

# Kontrolleringsplan for brukbarhet og kapasitet

[English (US)](2026-10-08-controller-polish.md) | [Deutsch](2026-10-08-controller-polish.md) | [Français](2026-10-08-controller-polish.md) | [Italiano](2026-10-08-controller-polish.md)

Brukerforespørsel: forbedrer utseendet, brukbarheten og funksjonaliteten; forhåndsbestemmelse krever maksimalt nyttige underagenter på nødvendige mindre modeller og autonom fullføring.

## Omfanget
Oppbevar eksisterende profiler og arbeide Corsair SDK integrasjon. Forbedre den eksisterende WPF applikasjonen i stedet for å lage en separat prototype.

- [x] Shell/dashboard arbeider: responsiv layout, aktiv navigasjon, lesbare kontroller, kompakt aktivitet/status, drag-and-drop tile reorder, nyttige tomme stater og maler. MainWindow.xaml, MainWindov.xam.cs og MainWind.Dashboard.cs
- [x] Redigerarbeider: MainWindow.Editor.cs og valgfritt redigerer bare partielle filer. Eksplicit trinnredigering, Save/Discard, validering inline, dedikert makro liste med drag-and-drop, duplikat trinn, pålitelig hotkey capture og fil/URL-felt.
- [x] Windows arbeidere: innfødte validering, robuste enhetene og SDK-funksjoner, begrenset modifiserende vent og sikker injeksjon; egen Windows og Windows tester.
- [x] Foreldre: kjerneutføringsprosess, kontrollgrensesnitt, konfigurasjonsbevaring, integrasjonstesting, virkelige renderte skjermbilder, publisering/pakking og utskiftning av kjøreapplikasjon.

## Interaksjonskontrakt
Kontrolleren gir eksisterende APIs pluss rad LastStatus, bool HasRunningActions, bool IsRunning(Guid), rad GetTileStatus(Guid) og rad ForegroundProcess, bool CorsairAvailable, IReadOnlyList<CorsairKeyboardInfo> CorsairKejboard, Task TestCorsairColorAsync(string), void OpenIcue(). Redaktøren viser Bool, DiscardDraft(), Bool CommitDraft(); eksisterende felt/metode navn forblir tilgjengelige i delklasser.

## Aksept
Ingen resett av den virkelige brukerkonfigurasjonen; gyldige endringer lagres en gang, ulovlige utkast forblir redigerbare, nedsatt gjenoppretter lagrede data. Native hotkey aliaer valideres uten å injisere testinnføringer i urelaterte vinduer. Tile og macro reorder testet; alle visninger renderer på skrivebordstørrelser; kjøre/feil/full status synlig; den virkelige SDK-enhetsmodellen vises. Bygg/tester og publisere røykpass, og oppdaterte applikasjoner er lansert.
