# First Store release: AI assistant excluded

The approved first Store variant omits the optional local Ollama meeting assistant. Build the retail input using `tools/publish-retail.ps1 -StoreRelease` with the usual public-key, commerce URL and output arguments, then package that checked input using `tools/publish-store.ps1`. Ordinary builds keep the optional assistant. Store builds and retail publication fail closed if an experimental ONNX host source project is present; use the clean release checkout. Store packaging requires explicit ONNX exclusion metadata and rejects known ONNX host/runtime/model paths in both the retail source and the final payload, even when inventories and archives are regenerated.

`StoreRelease=true` removes the model transport and assistant window source from compilation. The palette command is omitted. Programmatic model calls and assistant window callbacks are absent from the resulting assemblies. Existing saved assistant settings cannot restore code that was not compiled. The ordinary workflow editor, templates, and deterministic meeting planner remain available.

Both application assemblies embed the no-AI release marker. Store publishing and checking inspect PE metadata without executing input assemblies, require boolean release status flags, and reject missing markers, the model transport type, or assistant window/draft methods. The checked MSIX must match its checked payload. An older AI-enabled retail candidate cannot be used for this release. These checks establish package integrity and build variant; they do not replace packaged runtime acceptance or Store certification.

## Listing drafts

**English:** Create Windows hotkeys and reusable workflows for your apps and daily tasks. Combine actions, review each step, and start workflows when you choose. This first Store release does not include an AI assistant.

**Deutsch:** Erstelle Windows-Tastenkürzel und wiederverwendbare Abläufe für deine Programme und täglichen Aufgaben. Kombiniere Aktionen, prüfe die einzelnen Schritte und starte Abläufe nach Bedarf. Diese erste Store-Version enthält keinen KI-Assistenten.

Existing commercial terms and legal review requirements remain governed by the release documents. No upload, installation, signing or publication is part of this change.
