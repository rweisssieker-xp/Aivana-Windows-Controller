<!-- generated-doc locale=nl-NL source=docs/testing/2026-10-12-local-ai-runtimes.md sha256=3dfbb80761aad758ea6a9307999ff530dfdd4a0cf685804e15052508cf107fd7 -->

> Machinaal ondersteunde vertaling. Structuur, codevoorbeelden en links worden gecontroleerd; dit is geen taalkundige controle door een moedertaalspreker of juridische goedkeuring. Historische rapporten behouden hun oorspronkelijke datum en reikwijdte. [Originele bron](../../testing/2026-10-12-local-ai-runtimes.md)

# Lokale AI-runtimes

## Dienst

Hulpmiddelen → Open AI-vergaderassistent, activeer deze expliciet en selecteer AI-runtime. Maak middelen vrij, formuleer een doel, maak een concept en controleer deze in de editor. Geen enkele aanbieder voert het ontwerp automatisch uit. Er vindt geen automatische verandering van aanbieder plaats.

| Duur | Vereiste | verbinding |
|---|---|---|
| Ollama | lokaal geïnstalleerd model | 127.0.0.1:11434 |
| LM Studio | draaiende lokale server met lokaal geladen model en ondersteuning voor JSON-schema's | 127.0.0.1:1234 |
| llama.cpp | draaiende lama-server met ondersteuning voor lokaal model en JSON-schema | 127.0.0.1:8080 |
| ONNX Runtime GenAI CPU | compatibele, vertrouwde lokale GenAI-tekstmodelmap | afzonderlijk gebundeld proces, geen HTTP |

De HTTP-clients gebruiken geen proxy's en volgen geen omleidingen. Lokale servers kunnen zelf aanvullende diensten adresseren; In het bijzonder mag LM Link/Remote Routing niet worden gebruikt voor lokale verwerking. MAKRIVO kan dit niet betrouwbaar detecteren via de compatibele interface. Ollama externe metadata en cloudnamen worden bovendien afgewezen. Geverifieerde lokale servers kunnen in deze versie niet worden geconfigureerd.

## ONNX

In het dialoog genai_config.json selecteren. de map moet tokenizer.json en een chatvormen bevatten. chat_template wordt ondersteund als een tekenketen, een genoemde standaardvormen (Array of Dictionary) en een aparte chat_template.jinja, als er geen inlinevormen aanwezig zijn of dat nul is. meervoudige vormen worden afgewezen; maximaal 65536 De populaire .onnx-bestanden, visioen/embeddingmodellen of pure GPU/NPU-exporten zijn geen vergadermodellen.

Gebundeld: Microsoft.ML.OnnxRuntimeGenAI 0.17.1, CPU. Providerconfiguratie wordt opnieuw ingesteld op CPU. Geen download en geen modelgewichten in het pakket. Geen Python-installatie vereist. Gebruik alleen vertrouwde modelpakketten; het afzonderlijke proces is een crash/abort-grens, geen beveiligingssandbox.

Limieten: 90 seconden inclusief laden van model, maximum 3072 prompttokens en 1024 gegenereerde tokens, extra limiet voor modelcontext; JSON en bekende resource-ID's zorgvuldig gecontroleerd. Een onjuist/afgekort antwoord wordt verwijderd. Sluiten/Annuleren beëindigt het gestarte werkproces. Geen vrije modelopdrachten en geen programma's/bestandspaden in de afzonderlijke bronnenlijst van de prompt; de door de gebruiker geschreven doeltekst kan zelf dergelijke gegevens bevatten.

## Verificatie

- Kernsuite, loopback-transporttests met gesimuleerde LM-Studio/llama.cpp/Ollama-reacties, schema/abort/resource check.
- Unicode-protocolregressie met maximale lange doelen en 24-bronnamen.
- Echte native CPU-gevolgtrekking met officiële tiny-random-gpt2-fp32-Testgraph van microsoft/onnxruntime-genai, commit 5c19002edce50beaa3a5c7eeefd8fe5269ee8d19. Testsjabloon toegevoegd en byte-tokenizer aangepast aan de kleine willekeurige grafiek. Dit willekeurige model controleert de kwaliteit niet en is niet vrijgegeven.
- Reproduceerbare systeemeigen runtimetest: tools/check-onnx-runtime.ps1 -ModelDirectory <GenAI modelmap>.
- WPF-Smoke controleert de selectie van alle vier de aanbieders en de testbank; Selectie alleen genereert geen modelaanvraag.

## Resterende echte aandelen

Vervangingstests voor hardware/smartphone/tablet/monitor zijn nog steeds vereist. Exclusieve toetsenbordonderdrukking blijft geblokkeerd zonder een ondertekend, gelicentieerd en geverifieerd filterstuurprogramma. Niet vervangen door dummy-drivers of Raw Input. De focusverandering kan nog steeds worden geblokkeerd door Windows; de fout vermeldt nu handmatige activering van het doelvenster. Er zijn vertalingen toegevoegd aan de centrale AI-controles in alle 27-catalogi; Gedetailleerde nieuwe teksten en goedkeuring voor technische vertalingen staan ​​nog open. Livekwaliteit met een geschikt instructiemodel, continue werking en gemeten tijdsbesparing worden niet bewezen door willekeurig model/nep-HTTP-tests.

## Bronnen

- https://onnxruntime.ai/docs/genai/api/csharp.html
- https://www.nuget.org/packages/Microsoft.ML.OnnxRuntimeGenAI/0.17.1
- https://lmstudio.ai/docs/developer/openai-compat/chat-completions
- https://github.com/ggml-org/llama.cpp/blob/master/tools/server/README.md

Laatste test: release-build zonder waarschuwingen/fouten; Geslaagd voor de volledige kern en Windows-suite, inclusief native focus-fixture, HTTPS, UIA, plug-in IPC en compatibele AI-transporttests. Nieuwe pakketrelease-20261012-190618: WPF rook geslaagd (19:07 klok), gebundelde ONNX CPU-inferentie met testgrafiek geslaagd. Onafhankelijke beoordeling: Unicode-protocollimiet gecorrigeerd, geen andere concrete bevindingen. Hieruit is geen fysieke acceptatie van de fabrikant/telefoon/monitor afgeleid.

## Volgende: echte modelkwaliteit en modellen

Extra CPU-loop met allama-ai/SmolLM-360M-Instruct-onnx-int4, Revision 82a3d1a8a86c8053f2a859a90ed4d344fc0725b0: inheemse inferentie overleefd. Modellgewichte-SHA25648e9c4ef12f7f995ca11fc7020b39990aa27bc67cc0fe1160676a000a04429.

Vier echte planningstests (Duits/Engels, document, programma, uitdrukkelijke microfoon-stroomschijf): **0/4 Geldig Plannen**; JsonException bij strenge reactieprocedure. 63501, 6376, 72118 en 6774 ms. Geen acties uitgevoerd. dit kleine model voldoet niet aan de planningovereenkomst en wordt niet aanbevolen als geschikt. verslag: artifacts/local-ai-evaluation/smollm360-results.json. Windows-Tests --evaluate-onnx <modelordre> <rapportpad>. de functie van de looptijd bewijst geen modelkwaliteit.

Toolsnavigatie en testrapporten zijn in alle 27 Langtjes en taalopname blijven open. Modellenregresie onderzoekt ook valse JSON-types, leeg/meerdere/overlange modellen en nul met Jinja-fallback.

Updated pakket: artifacts/release-20261012-201134Gepacter WPFSmoke op 12.10.2026 Om te 20:12 Native CPU-inferentie met het afzonderlijke Instruct-testmodel in de gebonden Worker. Windows-KI-test doorstaan; Build zonder waarschuwingen/fouten. modelplanning kwaliteit blijven 0/4.
