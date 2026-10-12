# MAKRIVO — Documentation

[English (US)](../en-US/README.md) | [Deutsch](../de-DE/README.md) | [Français](../fr-FR/README.md) | [Italiano](../it-IT/README.md)

[Guide utilisateur](../../README.fr-FR.md)

La recherche décrit les capacités documentées des fabricants, sans garantir leur prise en charge dans le logiciel. Les plans et rapports datés conservent leur portée et leurs dates originales.

## Vérification et tests

- [Vérification — 8 octobre 2026](VERIFICATION.md)

- [Audit de régression de l’application — 2026-10-09](testing/2026-10-09-app-audit.md)

- [Assistance clavier, superposition et retour d’exécution](testing/2026-10-09-usp-release.md)

## Recherche

- [Marques de claviers et fonctions spéciales — recherche pour le contrôleur Windows](research/keyboard-custom.md)

- [Claviers de jeu avec fonctions spéciales et interfaces d’intégration](research/keyboard-gaming.md)

- [Dispositions physiques et variantes : commandes clavier de type Stream Deck](research/keyboard-layout-evidence.md)

- [Fonctions spéciales des claviers et pavés de touches auxiliaires](research/keyboard-productivity.md)

- [Intégration à OBS Studio](research/obs-integration.md)

- [Fabricants de claviers et fonctions spéciales — Inventaire pour un contrôleur Windows](research/tastaturhersteller-sonderfunktionen.md)

- [CSV](research/tastaturhersteller-sonderfunktionen.csv)

## Conceptions et plans historiques

- [Plan d’amélioration de l’ergonomie et des capacités du contrôleur](superpowers/plans/2026-10-08-controller-polish.md)

- [Plan de mise en œuvre du contrôleur Windows](superpowers/plans/2026-10-08-windows-controller.md)

- [Vue de clavier physique pour l’affectation](superpowers/specs/2026-10-08-keyboard-mapping.md)

- [MAKRIVO — conception](superpowers/specs/2026-10-08-windows-controller-design.md)

Les catalogues de traduction lisibles par machine restent dans docs/localization ; leurs clés sont des données techniques et ne sont pas traduites comme documentation.

## Validation de la documentation

```powershell
python tools/check_documentation.py
```
