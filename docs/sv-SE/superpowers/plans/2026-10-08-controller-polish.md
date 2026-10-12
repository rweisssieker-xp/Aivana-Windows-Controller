<!-- generated-doc locale=sv-SE source=docs/superpowers/plans/2026-10-08-controller-polish.md sha256=1a076a67eea31048f6305f30a121c474d592451d1ddcd9b46bc339fd8ec068f2 -->

> Maskinassisterad översättning. Struktur, kodexempel och länkar kontrolleras; detta innebär inte granskning av en modersmålstalare eller juridiskt godkännande. Historiska rapporter behåller sina ursprungliga datum och omfattning. [Originalkälla](../../../superpowers/plans/2026-10-08-controller-polish.md)

# Kontrollera användbarhet och kapacitetsförbättring

[English (US)](2026-10-08-controller-polish.md) | [Deutsch](2026-10-08-controller-polish.md) | [Français](2026-10-08-controller-polish.md) | [Italiano](2026-10-08-controller-polish.md)

Användarförfrågan: Förbättrar utseendet, användbarheten och funktionaliteten; Förhandstillstånd kräver maximalt användbara underagenter på nödvändiga mindre modeller och autonomt slutförande.

## Omfattning
Bevara befintliga profiler och arbeta Corsair SDK integration. Förbättra den befintliga WPF appen istället för att skapa en separat prototyp.

- [x] Shell/dashboard arbetare: responsiv layout, aktiv navigation, läsbara kontroller, kompakt aktivitet/status, drag-and-drop tile reorder, användbara tomma stater och mallar. MainWindow.xaml, MainWindov.xam.cs och MainWind.Dashboard.cs.
- [x] Redigerare: MainWindow.Editor.cs och valfri redigerare-endast partiella filer. Explicit staged edits, Save/Discard, validering inline, dedikerad makrolista med drag-and-drop, dubbla steg, pålitlig hotkey capture och fil/URL-fält.
- [x] Windows arbetare: inhemska validering, robusta enhetsnamn och SDK-förmåga, begränsade modifierare och säker injektion; egna Windows och Windows tester.
- [x] Föräldrarna: kärnprestanda, kontrollgränssnitt, konfigurationsbevarande, integrationstester, verkliga renderade skärmdumpar, publicering/förpackning och ersättning av körande program.

## Interface avtal
Kontrollören tillhandahåller befintliga APIs plus sträng LastStatus, bool HasRunningActions, bool IsRunning(Guid), sträng GetTileStatus(Guid) och sträng ForegroundProcess, bool CorsairAvailable, IReadOnlyList<CorsairKeyboardInfo> CorsairKayboards, Task TestCorsairColorAsync(string), void OpenIcue(). Redaktören exponerar bool HasUnsavedChanges, void DiscardDraft(), bool CommitDraft(); befintliga fält/metodnamn förblir tillgängliga i partiella klasser.

## Acceptans
Ingen omställning av den verkliga användarkonfigurationen; giltiga steg ändringar sparas en gång, ogiltiga utkast förblir redigerbara, diskard återställer sparade data. Native hotkey alias valideras utan att injicera testinmatningar i orelaterade fönster. Tile och macro reorder testas; alla visningar renderas på skrivbordets storlek; kör / misslyckas / fullständiga status synlig; den faktiska SDK-enhetsmodellen visas. Bygg/tester och publicera rökpass, och uppdaterad applikation lanseras.
