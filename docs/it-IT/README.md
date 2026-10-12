# MAKRIVO — Documentazione

[English (US)](../en-US/README.md) | [Deutsch](../de-DE/README.md) | [Français](../fr-FR/README.md) | [Italiano](../it-IT/README.md)

[Guida utente](../../README.it-IT.md)

La ricerca descrive le funzionalità documentate dai produttori, senza garantire il supporto implementato. I piani e i rapporti datati mantengono l’ambito e le date originali.

## Verifica e test

- [Verifica — 8 ottobre 2026](VERIFICATION.md)

- [Revisione delle regressioni dell'app — 2026-10-09](testing/2026-10-09-app-audit.md)

- [Assistenza tastiera, overlay e feedback dell'esecuzione](testing/2026-10-09-usp-release.md)

## Ricerca

- [Marchi di tastiere con funzioni speciali — ricerca per Windows Controller](research/keyboard-custom.md)

- [Tastiere da gaming con funzioni speciali e interfacce di integrazione](research/keyboard-gaming.md)

- [Layout fisici e varianti: controlli tastiera in stile Stream Deck](research/keyboard-layout-evidence.md)

- [Funzioni speciali di tastiere e tastierini aggiuntivi](research/keyboard-productivity.md)

- [Integrazione con OBS Studio](research/obs-integration.md)

- [Produttori di tastiere e funzioni speciali — inventario per un controller Windows](research/tastaturhersteller-sonderfunktionen.md)

- [CSV](research/tastaturhersteller-sonderfunktionen.csv)

## Progetti e piani storici

- [Piano di miglioramento dell'usabilità e delle funzionalità del controller](superpowers/plans/2026-10-08-controller-polish.md)

- [Piano di implementazione del controller Windows](superpowers/plans/2026-10-08-windows-controller.md)

- [Vista fisica della tastiera per l'associazione delle azioni](superpowers/specs/2026-10-08-keyboard-mapping.md)

- [MAKRIVO — Progetto](superpowers/specs/2026-10-08-windows-controller-design.md)

I cataloghi di traduzione leggibili dal software restano in docs/localization; le chiavi sono dati di implementazione e non vengono tradotte come documentazione.

## Verifica della documentazione

```powershell
python tools/check_documentation.py
```
