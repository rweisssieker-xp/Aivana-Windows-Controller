<!-- generated-doc locale=it-IT source=docs/testing/2026-10-12-local-ai-runtimes.md sha256=3dfbb80761aad758ea6a9307999ff530dfdd4a0cf685804e15052508cf107fd7 -->

> Traduzione assistita automaticamente. Struttura, esempi di codice e collegamenti vengono verificati; ciò non costituisce una revisione madrelingua o una convalida legale. I rapporti storici conservano le date e l'ambito originali. [Fonte originale](../../testing/2026-10-12-local-ai-runtimes.md)

# Runtime dell'intelligenza artificiale locale

## Servizio

Strumenti → Apri l'assistente alla riunione AI, attivalo esplicitamente e seleziona AI runtime. Rilascia risorse, formula un obiettivo, crea una bozza e controllala nell'editor. Nessun fornitore esegue la progettazione automaticamente. Non è previsto il cambio automatico del fornitore.

| Durata | Requisito | connessione |
|---|---|---|
| Ollama | modello installato localmente | 127.0.0.1:11434 |
| LM Studio | server locale in esecuzione con modello caricato localmente e supporto dello schema JSON | 127.0.0.1:1234 |
| llama.cpp | esecuzione del server lama con modello locale e supporto dello schema JSON | 127.0.0.1:8080 |
| ONNX Runtime GenAI CPU | cartella del modello di testo locale GenAI compatibile e affidabile | processo in bundle separato, nessun HTTP |

I client HTTP non utilizzano proxy e non seguono reindirizzamenti. I server locali stessi possono indirizzare servizi aggiuntivi; In particolare, LM Link/Remote Routing non può essere utilizzato per l'elaborazione locale. MAKRIVO non è in grado di rilevarlo in modo affidabile tramite l'interfaccia compatibile. Ollama metadati remoti e nomi cloud vengono inoltre rifiutati. I server locali autenticati non possono essere configurati in questa versione.

## ONNX

Nel dialogo selezionare genai_config.json. la cartella deve contenere tokenizer.json e un modello di chat. chat_template è supportato come catena di carattere, un modello di default denominato (Array o Dictionary) e un separato chat_template.jinja, se non esiste un modello in linea o questo è zero. 65536 I file .onnx popolari, i modelli di visione/impostazione o le esportazioni GPU/NPU pulite non sono modelli di riunione.

In bundle: Microsoft.ML.OnnxRuntimeGenAI 0.17.1, CPU. La configurazione del provider viene reimpostata sulla CPU. Nessun download e nessun peso del modello nel pacchetto. Nessuna installazione Python richiesta. Utilizzare solo pacchetti di modelli attendibili; il processo separato è un limite di arresto anomalo/interruzione, non un sandbox di sicurezza.

Limiti: secondi 90 incluso caricamento del modello, token di richiesta 3072 massimi e token generati 1024, limite aggiuntivo del contesto del modello; JSON rigorosamente controllato e ID di risorse noti. La risposta errata/troncata verrà scartata. Chiudi/Annulla termina il processo di lavoro avviato. Nessun comando del modello libero e nessun programma/percorso di file nell'elenco delle risorse separato del prompt; il testo di destinazione scritto dall'utente può esso stesso contenere tali dati.

## Verifica

- Suite principale, test di trasporto loopback con risposte LM-Studio/llama.cpp/Ollama simulate, controllo di schema/interruzione/risorse.
- Regressione del protocollo Unicode con destinazioni lunghe massime e nomi di risorse 24.
- Inferenza CPU nativa reale con tiny-random-gpt2-fp32-Testgraph ufficiale da microsoft/onnxruntime-genai, commit 5c19002edce50beaa3a5c7eeefd8fe5269ee8d19. Aggiunto modello di test e tokenizzatore di byte adattato al piccolo grafico casuale. Questo modello casuale non controlla la qualità e non è in fase di rilascio.
- Test di runtime nativo riproducibile: tools/check-onnx-runtime.ps1 -ModelDirectory <GenAI cartella del modello>.
- WPF-Smoke controlla la selezione di tutti e quattro i fornitori ed il banco prova; La sola selezione non genera una richiesta di modello.

## Azioni reali rimanenti

È ancora necessario il test di sostituzione hardware/smartphone/tablet/monitor. La soppressione esclusiva della tastiera rimane bloccata senza un driver di filtro firmato, concesso in licenza e verificato. Non sostituito da driver fittizi o Raw Input. Il cambio di messa a fuoco può ancora essere bloccato da Windows; l'errore ora menziona l'attivazione manuale della finestra di destinazione. Sono state aggiunte le traduzioni ai controlli AI centrali in tutti i cataloghi 27; Nuovi testi dettagliati e l'approvazione della traduzione tecnica sono ancora aperti. La qualità dal vivo con un modello di istruzioni adeguato, il funzionamento continuo e il risparmio di tempo misurato non sono dimostrati da modelli casuali/falsi test HTTP.

## Fonti

- https://onnxruntime.ai/docs/genai/api/csharp.html
- https://www.nuget.org/packages/Microsoft.ML.OnnxRuntimeGenAI/0.17.1
- https://lmstudio.ai/docs/developer/openai-compat/chat-completions
- https://github.com/ggml-org/llama.cpp/blob/master/tools/server/README.md

Test finale: rilascio build senza avvisi/errori; Superato il core completo e la suite Windows, inclusi il dispositivo di messa a fuoco nativo, HTTPS, UIA, plug-in IPC e test di trasporto AI compatibili. Nuova versione del pacchetto: 20261012-190618: fumo WPF superato (orologio 19:07), inferenza CPU ONNX in bundle con grafico di test superato. Revisione indipendente: limite del protocollo Unicode corretto, nessun altro risultato concreto. Da ciò non deriva alcuna accettazione fisica da parte del produttore/telefono/monitor.

## Continua: Reale qualità e modelli

Un ulteriore CPU con allama-ai/SmolLM-360M-Instruct-onnx-int4, Revision 82a3d1a8a86c8053f2a859a90ed4d344fc0725b0: inferenza nativa sopravvissuto. Modellgewichte-SHA256: 48e9c4ef12f7f995ca11fc7020b39990aa27bc67cc0fe1160676a000a04429.

Quattro test di pianificazione reali (tedesco/inglese, documento, programma, chiara schermata di microfono): **0/4 Piani validi**; JsonException in caso di rigoroso esame di risposta. 63501, 6376, 72118 e 6774 Questo piccolo modello non soddisfa il contratto di pianificazione e non è raccomandato come adatto. Rapporto: artifacts/local-ai-evaluation/smollm360-results.json. Windows-Test --evaluate-onnx <modello di ordine> <spedizione di rapporto>. la funzione di tempo di funzionamento non dimostra la qualità del modello.

Strumento di navigazione e report di prova sono stati aggiunti in tutti i 27 I cataloghi linguistici sono tradotti. i testi a lungo termine e le recensioni linguistiche rimangono aperte. la regressione dei modelli verifica anche i tipi JSON sbagliati, i modelli vuoti/multiplici/sbagliati e zero con il Jinja Fallback.

Pacchetto aggiornato: Artifacts/release-20261012-201134Il pacchetto WPFIl suono di am 12.10.2026 di O 20:12 sopravvissuto; nativo CPU con il modello di test di istruzione separato nel lavoratore legato sopravvissuto. Windows-I test di AI sono stati superati; costruire senza avvertimenti/errore. qualità di pianificazione del modello continua 0/4.
