<!-- generated-doc locale=fr-FR source=docs/testing/2026-10-09-usp-release.md sha256=96a5b23b67032ef07f2f5dd93e9585e7bab0a522e5096ffc9f4d020823c4264e -->

> Traduction assistée par machine. La structure, les exemples de code et les liens sont vérifiés ; cela ne constitue ni une révision par un locuteur natif ni une validation juridique. Les rapports historiques conservent leur date et leur portée d'origine. [Source originale](../../testing/2026-10-09-usp-release.md)

# Assistance clavier, superposition et retour d'exécution

[English (US)](2026-10-09-usp-release.md) | [Deutsch](2026-10-09-usp-release.md) | [Français](2026-10-09-usp-release.md) | [Italiano](2026-10-09-usp-release.md)

Implémentation des trois améliorations prioritaires dans l'application Windows existante :

- Assistant de clé spéciale : inventaire réel des périphériques/clés pris en charge par iCUE, capture temporaire de la clé G, vérification de la réussite de la configuration, validation des conflits et brouillon d'action non enregistré. L’annulation de capture, l’invalidation du rappel en file d’attente et la restauration de liaison normale couvrent les erreurs et la fermeture de la boîte de dialogue.
- Superposition de clavier : géométrie de référence physique, liaisons de profil actif, actualisation de profil/modification en direct et mise en surbrillance des touches Windows ordinaires. Maintenez Ctrl+Alt+F1 ou parcourez la page palette/clavier. Non activable, cliquable et absent de la barre des tâches.
- Retour d'information sur l'exécution : phases tapées En attente/En cours d'exécution/Étape/Terminé/Annulé/Échec/Rejeté, progression de l'étape/répétition et un toast non activant. Les commentaires excluent le contenu des actions et les messages d’exception. L'annulation de la dernière étape ne peut pas signaler l'achèvement. L'achèvement décrit l'exécution, et non une requête d'état OBS indépendante.

Vérification :

- 24 Vérifications principales réussies, y compris l'ordre des phases, les résultats d'échec/annulation, l'annulation de l'étape finale, l'isolement des exceptions d'écoute et l'exclusion des valeurs d'action privée.
- Version de la solution publiée réussie sans avertissements/erreurs.
- Windows contrôles réussis ; le K100 connecté a signalé G1–G6 et la configuration/libération native a réussi. Les points de terminaison audio ont été inspectés sans changer leur état.
- WPF smoke a été transmis avec toutes les dispositions de superposition de référence, la persistance de l'assistant/catalogue/conflits/brouillon, la configuration de capture native en direct, le transfert de rappel, le rejet de rappel obsolète et la restauration de liaison en pause. L'entrée de rappel native a été simulée ; une pression humaine physique sur la touche G reste une vérification manuelle.
- L'invariance réelle de la fenêtre de premier plan a été vérifiée pour le toast d'exécution. Les propriétés d'interaction de superposition et afficher/masquer ont été vérifiées ; des captures d'écran ont été rendues pour l'assistant, la superposition et le toast.
- Les langages 27 contiennent toutes les chaînes canoniques 819 ; Validation de l'espace réservé/de la clé hors ligne réussie.

Les instantanés et le résultat complet de l'interface graphique se trouvent dans Artefacts/usp-win-x64/smoke. Les tests utilisent des profils temporaires ; les paramètres de production sont sauvegardés et vérifiés lors du déploiement.
