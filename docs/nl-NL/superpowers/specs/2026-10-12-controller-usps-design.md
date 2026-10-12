<!-- generated-doc locale=nl-NL source=docs/superpowers/specs/2026-10-12-controller-usps-design.md sha256=4c3b2dbedb83af6bd48917515ae6559012afb6318ce69d1e32fb2765e0793327 -->

> Machinaal ondersteunde vertaling. Structuur, codevoorbeelden en links worden gecontroleerd; dit is geen taalkundige controle door een moedertaalspreker of juridische goedkeuring. Historische rapporten behouden hun oorspronkelijke datum en reikwijdte. [Originele bron](../../../superpowers/specs/2026-10-12-controller-usps-design.md)

# MAKRIVO bedieningsuitbreidingen

De gebruiker vroeg om alle negen voorgestelde verbeteringen en een herkenbaar systeemvakpictogram. Bestaande configuraties, met name de helderheid G1/G2, moeten compatibel blijven. Bestaande niet-vastgelegde wijzigingen in branding en documentatie behoren tot de gedeelde werkruimte en moeten behouden blijven.

## Resultaten en grenzen

1. Helderheidsacties selecteren alle ondersteunde monitoren of een stabiele specifieke monitor-ID. Het ontbreken van expliciete doelstellingen mislukt zichtbaar. Dashboard toont de huidige helderheid, uitvoervolume, microfoonstatus en OBS-status via begrensde asynchrone metingen.
2. Speciale toetsen ondersteunen direct indrukken, tikken, vasthouden en dubbeltikken, met herhalingsonderdrukking. Een vastgehouden laagselector verandert tijdelijk de actieve mapping en als u deze loslaat, wordt deze hersteld. Bestaande onmiddellijke bindingen behouden hun gedrag. Pauze, profielwijzigingen, verbroken verbinding en annulering wissen in behandeling zijnde gebaren.
3. De workflowontwerper bewerkt de voorwaarden, stuurt vertakkingen door en stopt/doorgaat met mislukkingsbeleid op transactionele wijze in een tegelconcept. Voorwaarden hebben betrekking op bestanden, lopende processen en venstertitels. Geen onbegrensde sprongcycli.
4. Expliciete opname legt korte akkoorden vast en wacht, negeert geïnjecteerde invoer en stopt bij Escape. Geregistreerde acties worden vóór vaststelling beoordeeld; gewone tekst wordt niet opgenomen.
5. Standaard speciale sleutels van andere fabrikanten gebruiken per apparaat Windows Raw Input. Voor bedrijfseigen sleutels is een ondersteunde leveranciers-API of toewijzing van een leverancier aan een standaardsleutel vereist; niet-ondersteunde functionaliteit wordt eerlijk weergegeven. Diagnose verklaart profiel, pauze, bindingsconflicten en feitelijke adapterstatus.
6. Profieluitwisseling maakt gebruik van een door de gebruiker geselecteerde gedeelde map (inclusief mappen die zijn gesynchroniseerd door een bestaande cloudclient). Revisiecontroles voorkomen stille overschrijvingen. Pull geeft voorbeelden van wijzigingen en importeert uitgeschakelde acties waarvan de hardwaretoewijzingen zijn verwijderd. De opname van actie-inhoud is expliciet. Er worden geen cloudgegevens opgeslagen door MAKRIVO.
7. UI-tekenreeksen behoren tot de bestaande catalogi in de 27-taal; DE/EN/FR/IT blijven de minimaal beoordeelde documentatietalen. Hiaten in de vertaling moeten worden onthuld in plaats van dat ze volledig zijn.
8. Een herkenbaar groenblauw M-toetsenbordteken vervangt het generieke Windows-pictogram in het systeemvak. De gepauzeerde modus heeft een oranje pauzebadge. Voor uitvoerbare/vensterbranding wordt hetzelfde merkteken gebruikt. Tooltip bevat staat en profiel.

## Verificatie

Kerncontroles hebben betrekking op achterwaartse compatibiliteit, snapshots, annulering, gebarentiming, workflowvertakkingen/-fouten, recorderfiltering en synchronisatieconflicten. Windows-controles hebben betrekking op alleen-lezen weergave-opsomming, SDK-releasegebeurtenissen en Raw Input-parsering. WPF rookcontroles dekken nieuwe toegangspunten en bronnen zonder de werkelijke monitorhelderheid te veranderen, in een andere applicatie te typen of gebruikersprofielen te overschrijven. Bij de implementatie blijft een back-up en G1/G2-configuratie behouden.
