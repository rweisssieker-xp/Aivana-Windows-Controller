<!-- generated-doc locale=it-IT source=docs/superpowers/plans/2026-10-12-local-ai-runtimes.md sha256=f24c6b3bce4d0b4dd9d42732c363941a36644f90cf8d664d61ea0c60eb950079 -->

> Traduzione assistita automaticamente. Struttura, esempi di codice e collegamenti vengono verificati; ciò non costituisce una revisione madrelingua o una convalida legale. I rapporti storici conservano le date e l'ambito originali. [Fonte originale](../../../superpowers/plans/2026-10-12-local-ai-runtimes.md)

# Tempi di esecuzione dell'IA locale e controlli rimanenti

Obiettivo: collegare effettivamente ulteriori runtime IA locali, ricevere revisioni di progettazione e rendere visibili le versioni aperte. L'utente autorizza l'implementazione senza domande. Le modifiche esistenti vengono mantenute.

1. Test di trasporto loopback e di ispezione dei pacchetti pre-implementazione ONNX; Controlla indirizzi di rete, limiti di output, interruzione e risorse sconosciute.
2. Messaggi del pianificatore condiviso; Ollama ricevuto, LM Studio (127.0.0.1:1234) e llama.cpp (127.0.0.1:8080) aggiunti. Nessun URL cloud o cambio automatico del provider. I server locali possono inoltrarsi; Il loro funzionamento resta di responsabilità dell'operatore.
3. ONNX Runtime GenAI CPU nel proprio processo con timeout, protocollo JSON limitato e terminazione del processo. Seleziona esplicitamente la cartella del modello; nessun modello viene scaricato automaticamente. Solo modelli di testo compatibili con GenAI, nessuna rete .onnx arbitraria.
4. WPF selezione per runtime, modello, cartella ONNX; tutti offrono lo stesso design rigorosamente testato.
5. Banco di prova con limiti chiari per hardware, DPI, pannello mobile ed esclusivo macro pad. In questo caso i controlli fisici e un driver firmato/con licenza non possono essere sostituiti dal software.
6. Build di rilascio, test core/Windows, fumo WPF, test sul lavoratore/pacchetto e revisione indipendente. Creare un nuovo pacchetto completo; Riporta la qualità del modello come testata solo se il modello esiste effettivamente.

Risultato: quattro tempi di esecuzione implementati, ONNX testato in modo nativo separatamente, scelta del fornitore e banco di prova nel fumo WPF imballato superato. Core/Windows suite verde. Esamina la ricerca del limite Unicode con costante comune e regressione corretta. Le approvazioni reali delle modifiche hardware/mobile/DPI, il rilascio dei driver, le traduzioni tecniche complete e la qualità delle istruzioni dal vivo rimangono espliciti lavori aperti; non rilasciato come completo.

Continuazione: Implementazione separata di modelli Jinja e denominati/default; verifica di tipo/limite/fallo-back. 27 Il vero modello di istruzione scaricato separatamente e SHA256 verificato: la CPU nativa funziona, ma 0/4 I casi di pianificazione rispettano il contratto JSON; nessuna azione viene eseguita. recensione indipendente: zero-fallback corretto e testato, dopo di che nessuna ulteriore scoperta concreta. Windows-I test sono passati, rilascio-build 0 Avvertenze e errori.
