<!-- generated-doc locale=da-DK source=docs/testing/2026-10-09-usp-release.md sha256=96a5b23b67032ef07f2f5dd93e9585e7bab0a522e5096ffc9f4d020823c4264e -->

> Maskinassisteret oversættelse. Struktur, kodeeksempler og links kontrolleres; dette er ikke en gennemgang af en modersmålstalende eller en juridisk godkendelse. Historiske rapporter bevarer deres oprindelige datoer og omfang. [Oprindelig kilde](../../testing/2026-10-09-usp-release.md)

# Keyboard support, overlay og feedback

[English (US)](2026-10-09-usp-release.md) | [Deutsch](2026-10-09-usp-release.md) | [Français](2026-10-09-usp-release.md) | [Italiano](2026-10-09-usp-release.md)

Implementerede de tre prioriterede forbedringer i den eksisterende Windows app:

- Special-key wizard: ægte iCUE-støttede enhed/nøgleindlæg, midlertidig G-key fangst, konfiguration-succes verifikation, konflikt validering og en uafhængig handlingsplan. Capture rollback, queued callback invalidation og normal-binding restaurering dækker fejl og dialog lukning.
- Keyboard overlay: fysisk referencegeometri, aktive profilbindinger, live profil/redigere refresh og almindelig Windows nøgleoplysning. Hold Ctrl+Alt+F1 eller tag gennem palet/keyboard-siden. Ikke aktiveret, klikket og fraværet fra opgaver.
- Udførelsesoplysninger: typisk ventet / løbende / trin / afsluttet / mislykkede / afviste faser, trin / gentagelse fremskridt og en ikke-aktiverende toast. Feedback udelukker handlingsindhold og undtagelser. Den sidste fase af annullering kan ikke blive afsluttet. Kompletion beskriver udførelse, ikke en uafhængig OBS statsforespørgsel.

Verifikation af:

- 24 Core kontroller gennemgået, herunder fase bestilling, fejl/annullering resultater, endelig skridt annullering, lytter undtagelse isolering og udelukkelse af private handlingsværdier.
- Udgivelse af opbygning uden advarsel / fejl.
- Windows kontrollerne passerede; de forbundne K100 rapporterer G1-G6 og indfødte konfiguration / frigivelse lykkedes. Audio endpoints blev inspiceret uden at ændre deres tilstand.
- WPF røg passeret med alle reference overlay layouter, magi/katalog/konflikter/drap persistens, live indfødt optagelse konfiguration, callback forwarding, stale callback afvisning og pause-binding restauration. Native callback input blev simuleret; en fysisk menneskelig G-key press forbliver en manuel check.
- Faktisk foreground-vindue invariance blev kontrolleret for udførelsen toast. Overlay-interaktionsegenskaber og show/hide blev kontrolleret; skærmbilleder blev renderet til trolleren, overlay og toast.
- 27 sprog indeholder alle 819 kanoniske rækker; offline placering/nøgle validation passeret.

Snapshots og det fulde GUI-resultat er i artifi/usp-win-x64/smoke. Tester bruger midlertidige profiler; produktionsindstillinger er backed up og hash-check under opførelsen.
