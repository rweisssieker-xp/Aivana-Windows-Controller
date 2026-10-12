<!-- generated-doc locale=nb-NO source=docs/testing/2026-10-09-usp-release.md sha256=96a5b23b67032ef07f2f5dd93e9585e7bab0a522e5096ffc9f4d020823c4264e -->

> Maskinassistert oversettelse. Struktur, kodeeksempler og lenker kontrolleres; dette innebærer ikke gjennomgang av en morsmålsbruker eller juridisk godkjenning. Historiske rapporter beholder sine opprinnelige datoer og omfang. [Originalt kilde](../../testing/2026-10-09-usp-release.md)

# Keyboard støtte, overlay og utførelse tilbakemelding

[English (US)](2026-10-09-usp-release.md) | [Deutsch](2026-10-09-usp-release.md) | [Français](2026-10-09-usp-release.md) | [Italiano](2026-10-09-usp-release.md)

Implementerte de tre prioriterte forbedringene i den eksisterende Windows appen:

- Special-key wizard: ekte iCUE-støttet enhet/nøkkelinnlegg, midlertidig G-nøkkelfaktura, konfigurasjon-suksess verifisering, konfliktvalidering og en uavhengig handlingsplan. Capture rollback, queued callback invalidasjon og normal-binding restaurering dekker feil og dialog lukking.
- Keyboard overlay: fysisk referansegeometri, aktive profilbindinger, live profil/edit refresh og vanlig Windows nøkkeloppmerking. Hold Ctrl+Alt+F1 eller togg gjennom palett/keyboard-siden. Ikke aktivering, klikk gjennom og fravær fra oppgavebordet.
- Eksekusjon tilbakemelding: typisk venting/running/step/fullført/annullt/avvist faser, steg/repetisjon fremgang og en ikke-aktiverende toast. Feedback utelukker handling innhold og unntak meldinger. Endelig kan ikke avbestillingen rapporteres. Komplettelsen beskriver eksekusjon, ikke en uavhengig OBS statlig etterspørsel.

Verifikasjon av:

- 24 Kjernekontrollene er gjennomgått, inkludert fasbestilling, feil/annullingsresultater, sluttspillannelse, lytterundring og ekskludering av private handlingsverdier.
- Release løsning bygges gjennom uten advarsler/feil.
- Windows sjekker passerte; de tilknyttet K100 rapporterte G1–G6 og innfødte konfigurasjon/utgivelse lykkes. Audio endpoints ble inspisert uten å endre status.
- WPF røyk passerer med alle referanse overlay layouts, magi/katalog/konflikter/draft utholdenhet, live innfødt fanger konfigurasjon, callback overføring, stale callback avvisning og pause-binding restaurering. Native callback input ble simulert; en fysisk menneskelig G-key press forblir en manuell sjekk.
- Den virkelige foreground-vindue invariansen ble sjekket for utførelsen av toast. Overlay-interaksjonsegenskaper og show/hide ble sjekket; skjermbilder ble gjentatt for trollmann, overlay og toast.
- 27 språk inneholder alle 819 kanoniske strenger; off-siteholder / nøkkelvalidering passerte.

Snapshots og det fulle GUI-resultatet er i artifakter/usp-win-x64/smoke. Tester bruker midlertidige profiler; produksjonsinnstillingene er backed up og hash-check under utførelsen.
