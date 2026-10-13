<!-- generated-doc locale=nl-NL source=docs/MAINTENANCE.md sha256=9bf5875b50386e698c0574eee390f790d4b1c29cb11288c856c98e03a05877f7 -->

> Machinaal ondersteunde vertaling. Structuur, codevoorbeelden en links worden gecontroleerd; dit is geen taalkundige controle door een moedertaalspreker of juridische goedkeuring. Historische rapporten behouden hun oorspronkelijke datum en reikwijdte. [Originele bron](../MAINTENANCE.md)

# Alle documentatie talen houden

De documentatie volgt alle 27 UI-taal: de geïntroduceerde mappen gebruiken regionale codes; Noorse kaarten `nb-NO` De Latijnse Servische `sr-Latn`Elke map bevat een index, handboek, gedetailleerde actie verwijzing, functie inventaris, verificatie notities, onderzoek met inbegrip van de CSV, historische plannen en licentiegegegevens.

## De bron van de waarheid

Edit de wortel `README.md`, `docs/HANDBOOK.md`, `docs/FEATURES.md`, `docs/VERIFICATION.md`, deze pagina, of de originele bestanden onder `docs/research`, `docs/testing`, `docs/superpowers` en `docs/licensing`Niet een historisch verslag te herschrijven om te betekenen dat het een latere versie valideren. Houd de implementatie, simulatie, echte apparatuur acceptatie en commerciële goedkeuring onderscheidend.

## Geef en valideren

```powershell
python tools/localize_documentation.py --refresh-sources
python tools/check_documentation.py
```

De optionele online generator stuurt documentatieproos naar het publiekvertaling eindpunt van Google, volgens de bestaande localisatie-tool. Het leest niet de gebruikersconfiguratie, geheimen of modellenbestanden. Alleen draait het op de documentatie die geschikt is voor die service. Code-blocks, inline-code, URL's, tabbladstructuur en technische identificatoren zijn beschermd. `artifacts/documentation-translation`.

Wanneer de online provider beperkt is, `tools/translate_documentation_local.py` hetzelfde cache vult met behulp van een uitdrukkelijk geleverde plaats M2M100 CTranslate2 Het model. de 2026-10-12 Snapshot gebruikt de MIT-licentie facebook/m2m100_418M Modellen, omgezet als JustFrederik/m2m_100_418m_ct2_int8 Bij herziening `1aeed44db4dd61a486bba44acf54c76507082a2c`. CTranslate2 4.8.2 en `SentencePiece` 0.2.2 zijn alleen tools. geen modelweefsels of vertalingsafhankelijkheden worden verzonden met MAKRIVO. De generator draaien met `--cache-only` na het invullen van de cache om zonder netwerkverzoeken te renderen. de lokale assistent downloadt of uitvoert niet afstandsmodelcode.

```powershell
python -m pip install ctranslate2==4.8.2 sentencepiece==0.2.2
python tools/translate_documentation_local.py --model C:\models\m2m100-ct2 --refresh-sources
python tools/localize_documentation.py --cache-only
python tools/check_documentation.py
```

Zonder `--refresh-sources` hervat een run zijn bevroren momentopname. Gebruik de vernieuwingsoptie na het bewerken van originelen. Gecachte segmenten worden hergebruikt. Een gedeeltelijke run kan taalregiocodes kiezen met `--locales`; voltooi alle talen vóór publicatie. Onderbroken runs vervangen een mislukte vertaling niet stilzwijgend door onvertaalde tekst.

Het manifeste registreert elke bron en geproduceerde bestand met SHA-256 vingerafdrukken van geïntroduceerd UTF-8 tekst: LF-lijnendingen en geen byte-bestelling. De offline checker verifieert volledige per-linguid coverage, vingerafdrukken, lokale links, code voorbeelden, tabel/CSV structuur, bronnen URL's en onherstelbare locaties. Het ontkent staalbronnen. Een doorlopende structurele check is geen native speaker review of wettelijke goedkeuring. De geproduceerde pagina's dragen deze kwalificatie zichtbaar.

## Publicatie en pakketten

Publiceren van de documentatie, generator en checker samen. behoud de oorspronkelijke licentiebepalingen van derden; vertaalde licentiebestelling is informatief. Build artefacts, translation cache en lokale orchestratiefiles behouden niet tot Git. Een bron-documentatie release publiceert niet zelf een Windows De installatie.

Gebruikersvriendelijke app-vertalingen zijn afzonderlijke JSON-catalogen onder `src/Aivana.Core/Localization`Een app bouw en zijn documentatie moeten een versie van een snapshot delen; nieuw beschreven experimentele runtime ondersteuning kan een geïnstalleerd pakket voorkomen.
