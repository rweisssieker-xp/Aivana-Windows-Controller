<!-- generated-doc locale=nl-NL source=docs/testing/2026-10-12-local-meeting-assistant.md sha256=56631ca85dca5f785448d12586b0741628728925a649f6124f7aebffb19bb615 -->

> Machinaal ondersteunde vertaling. Structuur, codevoorbeelden en links worden gecontroleerd; dit is geen taalkundige controle door een moedertaalspreker of juridische goedkeuring. Historische rapporten behouden hun oorspronkelijke datum en reikwijdte. [Originele bron](../../testing/2026-10-12-local-meeting-assistant.md)

# Lokale AI-vergaderassistent – prototype

Status: 12. Oktober 2026. ForgeMind Outcome Agent-hypothese, begrensde vergaderstroom. Geen bewezen concurrentievoordeel en geen gemeten tijdsbesparing zonder echte vergelijkingstests.

## Gebruik

1. Commandopalet → **KI Vergaderassistent**. Het experiment is standaard uitgeschakeld en wordt gereset tijdens het importeren van configuraties of draagbare exporten.
2. Schakel lokale assistent in. Ollama moet afzonderlijk worden uitgevoerd op `127.0.0.1:11434`; er moet al een geschikt lokaal model zijn geïnstalleerd. **Laad lokale modellen** en selecteer een model. MAKRIVO installeert geen modellen/services, neemt geen contact op met OpenAI/Cloud API en gebruikt geen API-sleutels. De offline configuratie en betrouwbaarheid van de lokale Ollama-service ligt bij de gebruiker; gedeclareerde externe/cloudmodellen worden afgewezen.
3. Voer de bestemming in en voeg expliciet programma's/documenten/websites toe. Doel, resource-ID, naam en type worden verzonden; Doelpaden en inhoud van geselecteerde bestanden worden niet extra verzonden; de bestemmingsinvoer zelf wordt woordelijk verzonden. Websitelabels bevatten alleen schema/host, de volledige link blijft lokaal.
4. **KI concept maken**. Maximaal 90 seconden, 1024 uitvoertokens, beperkte responsgrootte. Wijzigingen in doel/model/bron negeren voorbeeld; Annuleren/Sluiten beëindigt het verzoek. Het model kan alleen geselecteerde ID's voorstellen. Onbekende ID's, gratis opdrachten, extra JSON-velden en uitvoerbare documenttypen worden afgewezen.
5. Bekijk voorbeeld en open punten → **Check in editor**. Het resultaat is in eerste instantie een niet-opgeslagen concept. Eerst opslaan in de bestaande editor en indien nodig expliciet uitvoeren. Normale macrovalidatie, pre/post-controle, pauzeren en afbreken blijven verantwoordelijk.

Ondersteund: Programma's starten zonder argumenten, expliciet geselecteerde lokale PDF/DOCX/XLSX/PPTX/TXT/MD openen, HTTP(S)-websites openen, microfoon dempen op expliciet verzoek. Geen standaardwijziging van de headset, geen berichten, afspraken, downloads, scripts, gratis klikken of tekstinvoer. Wensen die niet kunnen worden weergegeven, verschijnen als open punten als het model ze correct herkent; De gebruiker moet controleren of aan de technische doelstellingen is voldaan. Het openen van documenten wordt niet automatisch technisch geverifieerd.

## Vergelijkingsexperiment

**Handmatige instelling van metingen · Start / Einde** meet de volledige handmatige vergelijkingsstroom. Aannames van de wizard meten de tijd vanaf het openen van de wizard tot het eerste opslaan in de editor; `generated` bevat daarentegen alleen de planningsduur. Vergelijk niet twee ongelijke looptijden om tijd te besparen.

Lokale gebeurtenissen bevinden zich in de configuratiemap onder `Experiments/meeting-experiment.jsonl`: tijdstempel, willekeurige poging/tegel-ID, gebeurtenis, duur, correctie ja/nee. Geen doelteksten, bronnamen, paden, modelantwoorden of documentinhoud. Bestand roteert van 2 MiB, een eerder bestand blijft behouden. Meetfouten mogen macro's en het opslaan van configuraties niet afbreken.

Opnames: handmatige basislijn, modelgeneratie/fouten/afbreken, het negeren van voorbeeldwijzigingen/sluitingen, initiële opslag en correcties in de editor, en daadwerkelijke eindstatussen van de tegels die daaruit zijn opgeslagen. `run_completed` betekent alleen dat de loper klaar is; Bevestigde doeleffecten worden afzonderlijk vermeld in het testproces. Herhalingen van dezelfde macro zijn geen nieuwe, onafhankelijke instelpogingen. Het eerste experiment kan na opnieuw opstarten worden voortgezet, omdat de experiment-ID op de opgeslagen tegel blijft staan. Gedupliceerde tegels delen deze oorsprong; groeperen op proef-ID tijdens de evaluatie.

Eerste experiment: 20 vergelijkbare vergaderfaciliteiten of twee weken met vrijwillige testgebruikers; aanvankelijk intern gebruik. Vergelijk de handmatige insteltijd versus de volledig ondersteunde insteltijd; Evalueer bovendien het correctie-/afkeuringspercentage, runner-fouten en echte testresultaten. Als er geen verbetering optreedt of als de beveiligingslimieten worden overschreden, deactiveer dan de schakelaar. In de repository is geen gebruik of betalingsbereidheid aangetoond. Geen automatische telemetrietransmissie of prijsaanpassing.

## Proefbank

- Kern: Limieten, typen, onbekende/dubbele ID's, scriptuitsluiting, lange links, optionele verouderde configuratie, opt-in voor import/export, gegevensbesparende meting en eenmalige verwijdering en correcte toewijzing.
- Transport met nepmodel: Ollama JSON-schema, loopback-bestemming, geen bestandspaden in prompt, cloudnaam en externe alias afgewezen, corrupte/te lange reactie afgewezen, afbreken.
- WPF-Smoke: dialoogvenster weergegeven; Het indienen van een concept verandert de opgeslagen tegels niet, het bekijken/opslaan behoudt de herkomst en registreert correctie, weggooien bespaart niets.
- Onafhankelijke beoordeling: voorbeeld verwijderd en lange links gevonden en gecorrigeerd; Regressietesten ROOD → GROEN.
- Release-build zonder waarschuwingen/fouten, volledige kernsuite en WPF-rook succesvol. Aanvankelijk bestond de volledige Windows-suite inclusief focuswijzigingen. Een andere run mislukte opnieuw omdat de voorgrondwijziging werd geweigerd; de test is contextafhankelijk en blijft ongewijzigd. De overige Windows-tests inclusief modeltransport zijn geslaagd, waarbij de succestest op de voorgrond expliciet is weggelaten.
- Pakketinspectie bracht een bestaande bug aan het licht in de atomaire uitwisseling van kort geopende configuratiebestanden. Een leesvergrendeling reproduceerde dezelfde fout bij geweigerde toegang; beperkte herhaling van alleen de atomaire uitwisseling is ROOD → GROEN aangevinkt. Permanente toegangsfouten blijven zichtbaar.
- Huidig zelfstandig pakket `artifacts/release-20261012-184105`: WPF smoke succesvol na geheugencorrectie, inclusief AI-dialoog en acceptatie/afwijzing van concepten.
- Geen echte modeltest: de lokale Ollama-service reageerde niet tijdens de test. Geen uitspraken over modelkwaliteit, latentie, semantische volledigheid of tijdbesparing. Hardware-/driver-/schalingsbeperkingen van de vorige release blijven bestaan.

Protocolbronnen : [Ollama Chat](https://docs.ollama.com/api/chat), [Structured outputs](https://docs.ollama.com/capabilities/structured-outputs), [Officiële type-definities, met inbegrip van afstandsmodelgegevens](https://github.com/ollama/ollama/blob/main/api/types.go).
