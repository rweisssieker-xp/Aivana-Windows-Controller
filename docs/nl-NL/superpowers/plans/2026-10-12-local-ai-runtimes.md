<!-- generated-doc locale=nl-NL source=docs/superpowers/plans/2026-10-12-local-ai-runtimes.md sha256=f24c6b3bce4d0b4dd9d42732c363941a36644f90cf8d664d61ea0c60eb950079 -->

> Machinaal ondersteunde vertaling. Structuur, codevoorbeelden en links worden gecontroleerd; dit is geen taalkundige controle door een moedertaalspreker of juridische goedkeuring. Historische rapporten behouden hun oorspronkelijke datum en reikwijdte. [Originele bron](../../../superpowers/plans/2026-10-12-local-ai-runtimes.md)

# Lokale AI-runtimes en resterende controles

Doel: daadwerkelijk extra lokale AI-runtimes koppelen, ontwerprecensies ontvangen en open releases zichtbaar maken. Gebruiker autoriseert implementatie zonder vragen. Bestaande wijzigingen blijven behouden.

1. Loopback-transport en ONNX pre-implementatie pakketinspectietests; Controleer netwerkadressen, uitvoerlimieten, afbreken en onbekende bronnen.
2. Gedeelde Planner-berichten; Ollama ontvangen, LM Studio (127.0.0.1:1234) en llama.cpp (127.0.0.1:8080) toegevoegd. Geen cloud-URL's of automatisch wisselen van provider. Lokale servers kunnen zichzelf doorsturen; De werking ervan blijft de verantwoordelijkheid van de exploitant.
3. ONNX Runtime GenAI CPU in eigen proces met time-out, beperkt JSON-protocol en procesbeëindiging. Selecteer expliciet de modelmap; geen enkel model wordt automatisch gedownload. Alleen GenAI-compatibele tekstmodellen, geen willekeurige .onnx-netwerken.
4. WPF selectie voor runtime, model, map ONNX; ze leveren allemaal hetzelfde uitvoerig geteste ontwerp.
5. Testbank met duidelijke limieten voor hardware, DPI, mobiel paneel en exclusief macropad. Fysieke controles en een ondertekende/gelicentieerde chauffeur kunnen hier niet door software worden vervangen.
6. Releasebuild, core/Windows-testen, WPF-rook, worker-/pakkettesten en onafhankelijke beoordeling. Nieuw compleet pakket aanmaken; Rapporteer de geteste modelkwaliteit alleen als het model daadwerkelijk bestaat.

Resultaat: vier runtimes geïmplementeerd, ONNX native afzonderlijk getest, keuze van provider en testbank in de verpakte WPF rook doorgegeven. Core/Windows suites groen. Bekijk het vinden van de Unicode-limiet met gemeenschappelijke constante en vaste regressie. Echte goedkeuringen voor wijzigingen in hardware/mobiel/DPI, release van stuurprogramma's, volledige technische vertalingen en live instructiekwaliteit blijven expliciet open werk; niet als compleet uitgegeven.

Voorts: afzonderlijke Jinja en aangeduide/default-samenwerken geïmplementeerd; Type/Grenz/Fallback-regressie-testingen ondergaan. 27 Catalogen worden aangevuld. echte instructiemodel gescheiden gedownload en SHA256 gecontroleerd: native CPU-inferentie werkt, maar 0/4 Planning gevallen voldoen aan de JSON-overeenkomst; geen acties worden uitgevoerd. onafhankelijke beoordeling: nul-fallback gecorrigeerd en getest, daarna geen concrete verdere bevindingen. Windows-KI-test doorgevoerd, release-build 0 Waarschuwingen en fouten.
