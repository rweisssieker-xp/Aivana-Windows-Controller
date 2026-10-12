<!-- generated-doc locale=nl-NL source=docs/superpowers/plans/2026-10-08-controller-polish.md sha256=1a076a67eea31048f6305f30a121c474d592451d1ddcd9b46bc339fd8ec068f2 -->

> Machinaal ondersteunde vertaling. Structuur, codevoorbeelden en links worden gecontroleerd; dit is geen taalkundige controle door een moedertaalspreker of juridische goedkeuring. Historische rapporten behouden hun oorspronkelijke datum en reikwijdte. [Originele bron](../../../superpowers/plans/2026-10-08-controller-polish.md)

# Verbeterplan voor de bruikbaarheid van de controller en het capaciteitsverbeteringsplan

[English (US)](2026-10-08-controller-polish.md) | [Deutsch](2026-10-08-controller-polish.md) | [Français](2026-10-08-controller-polish.md) | [Italiano](2026-10-08-controller-polish.md)

Gebruikersverzoek: uiterlijk, bruikbaarheid en functionaliteit verbeteren; voorafgaande toestemming vraagt ​​om maximaal bruikbare subagenten op noodzakelijke kleinere modellen en autonome voltooiing.

## Reikwijdte
Behoud bestaande profielen en werkende Corsair SDK-integratie. Verbeter de bestaande WPF-applicatie in plaats van een afzonderlijk prototype te maken.

- [x] Shell-/dashboardwerker: responsieve lay-out, actieve navigatie, leesbare bedieningselementen, compacte activiteit/status, herschikking van tegels met slepen en neerzetten, handige lege statussen en sjablonen. Bezit MainWindow.xaml, MainWindow.xaml.cs en MainWindow.Dashboard.cs.
- [x] Editor-werknemer: MainWindow.Editor.cs en optionele gedeeltelijke bestanden voor de editor. Expliciete gefaseerde bewerkingen, opslaan/weggooien, inline validatie, speciale macrolijst met slepen en neerzetten, dubbele stappen, betrouwbare sneltoetsregistratie en bestands-/URL-velden.
- [x] Windows-werker: native validatie, robuuste apparaatnamen en SDK-mogelijkheden, begrensde modifier-wachttijden en veilige injectie; eigen Windows- en Windows-tests.
- [x] Parent: voortgang van de kernuitvoering, controllerinterfaces, behoud van configuratie, testen van integratie, feitelijk weergegeven schermafbeeldingen, publiceren/verpakken en vervangen van actieve applicatie.

## Interface-contract
Controller biedt bestaande API's plus string LastStatus, bool HasRunningActions, bool IsRunning(Guid), string GetTileStatus(Guid), string ForegroundProcess, bool CorsairAvailable, IReadOnlyList<CorsairKeyboardInfo> CorsairKeyboards, Task TestCorsairColorAsync(string), void OpenIcue(). Editor toont bool HasUnsavedChanges, void DiscardDraft(), bool CommitDraft(); bestaande veld-/methodenamen blijven beschikbaar voor deelklassen.

## Acceptatie
Geen reset van echte gebruikersconfiguratie; geldige gefaseerde wijzigingen worden één keer opgeslagen, ongeldige concepten blijven bewerkbaar, weggooien herstelt opgeslagen gegevens. Native sneltoetsaliassen gevalideerd zonder testinvoer in niet-gerelateerde vensters te injecteren. Tegel- en macroherschikking getest; alle weergaven worden weergegeven op bureaubladgrootte; status actief/mislukt/voltooid zichtbaar; het werkelijke SDK-apparaatmodel wordt weergegeven. Bouw/test en gepubliceerde rookpas, en bijgewerkte applicatie is gelanceerd.
