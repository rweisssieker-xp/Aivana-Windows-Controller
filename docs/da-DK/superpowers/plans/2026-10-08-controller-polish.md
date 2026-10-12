<!-- generated-doc locale=da-DK source=docs/superpowers/plans/2026-10-08-controller-polish.md sha256=1a076a67eea31048f6305f30a121c474d592451d1ddcd9b46bc339fd8ec068f2 -->

> Maskinassisteret oversættelse. Struktur, kodeeksempler og links kontrolleres; dette er ikke en gennemgang af en modersmålstalende eller en juridisk godkendelse. Historiske rapporter bevarer deres oprindelige datoer og omfang. [Oprindelig kilde](../../../superpowers/plans/2026-10-08-controller-polish.md)

# Kontroller kapacitet og forbedring af kapacitet

[English (US)](2026-10-08-controller-polish.md) | [Deutsch](2026-10-08-controller-polish.md) | [Français](2026-10-08-controller-polish.md) | [Italiano](2026-10-08-controller-polish.md)

Brugers anmodning: forbedrer udseendet, anvendeligheden og funktionaliteten; forudgående tilladelse kræver maksimalt nyttige underagenter på nødvendige mindre modeller og autonome udførelser.

## Omfang
Bevar eksisterende profiler og arbejder Corsair SDK integration. Forbedre den eksisterende WPF applikation i stedet for at skabe en separat prototype.

- [x] Shell/dashboard arbejdere: responsiv layout, aktiv navigation, læsbar kontrol, kompakt aktivitet/status, drag-and-drop tile reorder, nyttige tomme stater og malerier. Own MainWindow.xaml, MainWindov.xam.cs og MainWind.Dashboard.cs
- [x] Editor arbejder: MainWindow.Editor.cs og valgfri editor-kun partielle filer. Eksplicit trin redigeringer, Save/Discard, validering inline, dedikeret makro liste med drag-and-drop, duplicate skridt, pålidelig hotkey optagelse og fil/URL felter.
- [x] Windows arbejdere: indfødte validering, robuste enhedsnavn og SDK kapaciteter, begrænsede modifikator vent og sikker injektion; egen Windows og Windows tests.
- [x] Forældre: Core execution progress, controllergrænser, konfiguration bevarelse, integration test, virkelige renderede screenshots, udgivelse/pakning og udskiftning af køreapplikation.

## Interface kontrakt
Kontrolleren giver eksisterende APIs plus string LastStatus, bool HasRunningActions, bool IsRunning(Guid), string GetTileStatus(Guid) og string ForegroundProcess, bool CorsairAvailable, IReadOnlyList<CorsairKeyboardInfo> CorsairKayboards, Task TestCorsairColorAsync(string), void OpenIcue(). Editoren udsender bool HasUnsavedChanges, void DiscardDraft(), bool CommitDraft(); eksisterende felt/metodene forbliver tilgængelige i alle dele af klasser.

## Acceptance
Ingen genoprettelse af den virkelige brugerkonfiguration; gyldige trin ændringer gemmes en gang, ulovlige udkast forbliver redigerbare, diskard genoprettelser gemte data. Native hotkey alias valideret uden at injicere testindtægter i uforholdsmæssige vinduer. Tile og macro reorder testet; alle udsigter renderer på skrivebordstørrelser; køre/fejl/komplet status synlig; den virkelige SDK-enhedsmodel vises. Bygg/tester og offentliggør røgpass, og opdateret applikation er lanceret.
