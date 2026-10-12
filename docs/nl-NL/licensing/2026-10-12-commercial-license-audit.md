<!-- generated-doc locale=nl-NL source=docs/licensing/2026-10-12-commercial-license-audit.md sha256=b58c6861e0b38c4495265651b5ab10349c701d8fb4582dd7d7cae5d2d865bc5f -->

> Machinaal ondersteunde vertaling. Structuur, codevoorbeelden en links worden gecontroleerd; dit is geen taalkundige controle door een moedertaalspreker of juridische goedkeuring. Historische rapporten behouden hun oorspronkelijke datum en reikwijdte. [Originele bron](../../licensing/2026-10-12-commercial-license-audit.md)

# MAKRIVO: Controleer op commerciële eenmalige verkoop

Status: 12. Oktober 2026. Fabrikant: Aivana.

## Resultaat

In de gecontroleerde inventarisatie is geen algemene verplichting tot openbaarmaking van de eigen MAKRIVO-broncode aangetroffen. Een eenmalige eigen verkoop lijkt mogelijk, maar is nog niet volledig voorbereid. In het bijzonder is de herdistributietoestemming voor de bestaande Corsair-SDK-DLL niet gedocumenteerd. Deze beoordeling vervangt geen wettelijke goedkeuring.

## Technische en licentiebevindingen

| bestanddeel | Bevindingen | Vereiste stap |
| --- | --- | --- |
| Eigen broncode | Geen eigen LICENTIE, KOPIËREN of KENNISGEVING gevonden in de onderzochte Git-inventaris en de lokaal beschikbare licentiebestandsgeschiedenis. | Bevestig het eigendom van rechten, inclusief bijdragen, aangenomen codefragmenten en mogelijke werkgeversrechten; maak uw eigen gebruiksvoorwaarden. |
| .NET / Windows-bureaublad | Het onderzochte zelfstandige pakket onder `artifacts/makrivo-win-x64` bevat versie 10.0.6. De lokale runtimepakketten bevatten MIT-licenties die commerciële distributie mogelijk maken en vereisen dat kennisgevingen worden opgenomen. Er zijn geen overeenkomstige licentie-/kennisgevingsbestanden gevonden in de onderzochte uitvoermappen. | Voeg exacte licentieteksten en toepasselijke informatie van derden toe voor de daadwerkelijk geleverde runtime in het verkooppakket. |
| NuGet afhankelijkheden | Er zijn geen directe PackageReference-vermeldingen gevonden in de onderzochte projectbestanden. Het desktopactivabestand dat wordt onderzocht, bevat zijn eigen projectafhankelijkheden. Runtimepakketten blijven nog steeds licentierelevant. | Inventariseer alle projecten, transitieve pakketten en gebundelde bestanden opnieuw bij de definitieve release. |
| Corsair iCUE SDK | `artifacts/win-x64/iCUESDK.x64_2019.dll`, bestandsversie 4.0.84.0. De lokale SDK-referentie-PDF bevat op afgedrukte pagina's 35–38 de EULA: royaltyvrij gebruik, maar beperkingen op overdracht/toewijzing. Er was geen duidelijke toestemming om de DLL door te geven aan klanten. | Zorg voor robuuste herdistributietoestemming voordat u gaat bundelen. U kunt ook een pakket zonder SDK-DLL controleren dat gebruikmaakt van een door de klant geïnstalleerde SDK; Ook hierop zijn de SDK voorwaarden van toepassing. |
| Verschillende pakketten | `artifacts/makrivo-win-x64` bevat niet de Corsair DLL; `artifacts/win-x64` bevat ze. De applicatie zoekt dynamisch naar de SDK op meerdere locaties. | Stel het daadwerkelijke installatieprogramma voor de detailhandel in en controleer de inhoud ervan. Het loutere bestaan ​​van een pakket zonder DLL bevestigt niet alle integratierechten. |
| OBS | Eigen WebSocket client in de onderzochte C# code; geen OBS-clientpakket, OBS binair of aangenomen GPL-code geïdentificeerd. | De waargenomen protocolintegratie rechtvaardigt op zichzelf geen gevestigde openbaarmakingsverplichting van de GPL. De oorsprong van eventuele geadopteerd fragmenten moet nog worden bevestigd. |
| App-pictogrammen | Een lokaal Python-script tekent geometrische symbolen met behulp van Pillow. De tool is een build-afhankelijkheid. | Documenteer de herkomst en rechten van alle daadwerkelijk geleverde afbeeldingen, geluiden, lettertypen en andere middelen. Het script alleen bewijst niet de rechten op elk item. |
| Licentiebeheer | Er is geen volledige aankooplicentie/activatiecontrole geïdentificeerd in de onderzochte applicatiecode. | Implementeer indien gewenst ondertekende offline licentiebestanden. Technische activering en wettelijke gebruiksvoorwaarden zijn afzonderlijke taken. |

## Verstandig aankoopmodel – nog niet geïmplementeerd

- eenmalige betaling; Permanent gebruiksrecht op de aangeschafte hoofdversie.
- Eén persoon op maximaal twee eigen pc's; Professioneel gebruik uitdrukkelijk toestaan.
- Inclusief bugfixes en kleine updates; Eventueel latere hoofdversies tegen betaling aanbieden.
- Digitaal ondertekend licentiebestand dat offline kan worden gecontroleerd; Lever nooit privé-ondertekeningssleutels af met de app.
- Wettelijke rechten en vereiste beveiligingsupdates mogen niet over de hele linie worden uitgesloten door de hoofdversieregel.

## Open vóór eerste verkoop

1. Corsair Distributierechten verduidelijken of verkoop voorbereiden zonder gebundelde SDK-DLL.
2. Voeg licentie- en teksten van derden toe voor de exacte release-afhankelijkheden en ontvang deze in het installatieprogramma.
3. Bevestig het eigendom van de rechten op uw eigen code en assets; Creëer een EULA met duidelijke apparaat-, gebruikers- en upgraderegels.
4. Voor consumentenverkopen regelt u het afrekenen inclusief informatievereisten en annulering. Een algemene verklaring van “geen restitutie voor downloads” is niet voldoende. Voor het vervallen van het herroepingsrecht als de goederen onmiddellijk ter beschikking worden gesteld, zijn de wettelijke vereisten vereist.
5. Controleer het voltooide installatieprogramma opnieuw, inclusief plug-ins en alle transitieve afhankelijkheden. De werkkopie wordt parallel uitgevouwen; Deze inventaris is geen vrijgave van toekomstige bestanden.

## GitHub

De onderzochte repository is openbaar. Zonder open source-licentie wordt de code niet automatisch gratis voor verder gebruik. De platformrechten GitHub voor openbare repositories moeten nog steeds worden nageleefd. Bij een latere overstap naar privé worden bestaande kopieën of forks niet verwijderd. Zichtbaarheid en licentie zijn tijdens deze beoordeling niet gewijzigd.

## Bewijs en bronnen

- Lokale .NET-licenties: `C:/Users/weiss/.nuget/packages/microsoft.netcore.app.runtime.win-x64/10.0.6/LICENSE.TXT` en `C:/Users/weiss/.nuget/packages/microsoft.windowsdesktop.app.runtime.win-x64/10.0.6/LICENSE`.
- Lokale .NET-kennisgevingen van derden: `C:/Users/weiss/.nuget/packages/microsoft.netcore.app.runtime.win-x64/10.0.6/THIRD-PARTY-NOTICES.TXT`.
- Lokale Corsair SDK: `artifacts/dependencies/iCUESDK_4.0.84.zip`; geëxtraheerde referentie-PDF op `sdk/iCUESDK/doc/iCUE SDK - Overview and Reference.pdf`.
- [.NET 10.0.6 MIT-licentie](https://raw.githubusercontent.com/dotnet/runtime/v10.0.6/LICENSE.TXT)
- [Corsair SDK-documentatie met EULA](https://raw.githubusercontent.com/CorsairOfficial/cue-sdk/master/docs/index.html)
- [GitHub: Repositories licentie](https://docs.github.com/en/repositories/managing-your-repositorys-settings-and-features/customizing-your-repository/licensing-a-repository)
- [BGB § 327f – bijwerken](https://www.gesetze-im-internet.de/bgb/__327f.html)
- [BGB § 356 – Recht op intrekking](https://www.gesetze-im-internet.de/bgb/__356.html)

Niet gecontroleerd: volledige externe bijdragegeschiedenis en rechtenketens, vrijgave van handelsmerken, bestaande verkoopafhandeling, toekomstige plug-inpakketten, volledige juridische effectiviteit van een nog niet beschikbare EULA.
