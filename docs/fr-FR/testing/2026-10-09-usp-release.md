# Assistance clavier, superposition et retour d’exécution

[English (en-US)](../../en-US/testing/2026-10-09-usp-release.md) | [Deutsch (de-DE)](../../de-DE/testing/2026-10-09-usp-release.md) | [Français (fr-FR)](2026-10-09-usp-release.md) | [Italiano (it-IT)](../../it-IT/testing/2026-10-09-usp-release.md)

Les trois améliorations prioritaires ont été mises en œuvre dans l’application Windows existante :

- Assistant des touches spéciales : inventaire réel des périphériques et touches pris en charge par iCUE, capture temporaire des touches G, vérification de la réussite de la configuration, validation des conflits et brouillon d’action non enregistré. L’annulation de la capture, l’invalidation des rappels en attente et la restauration des affectations normales couvrent les erreurs et la fermeture de la boîte de dialogue.
- Superposition du clavier : géométrie de référence physique, affectations du profil actif, actualisation en direct du profil et des modifications, et mise en évidence des touches Windows ordinaires. Maintenez Ctrl+Alt+F1 ou activez/désactivez la vue via la palette ou la page clavier. Elle ne prend pas le focus, laisse passer les clics et n’apparaît pas dans la barre des tâches.
- Retour d’exécution : phases typées Waiting/Running/Step/Completed/Cancelled/Failed/Rejected, progression des étapes et répétitions, et notification sans prise de focus. Le retour exclut le contenu des actions et les messages d’exception. L’annulation à la dernière étape ne peut pas signaler une réussite. La fin indique la fin de l’exécution, sans interroger séparément l’état OBS.

Vérification :

- 24 contrôles Core réussis, notamment l’ordre des phases, les résultats d’échec/d’annulation, l’annulation à la dernière étape, l’isolation des exceptions des écouteurs et l’exclusion des valeurs d’action privées.
- Compilation Release de la solution réussie sans avertissements ni erreurs.
- Contrôles Windows réussis ; le K100 connecté a signalé G1–G6 et la configuration/libération native a réussi. Les points de terminaison audio ont été inspectés sans modifier leur état.
- Test rapide WPF réussi avec toutes les dispositions de superposition de référence, assistant/catalogue/conflits/persistance des brouillons, configuration de capture native en direct, transfert des rappels, rejet des rappels périmés et restauration des affectations après la pause. L’entrée des rappels natifs a été simulée ; l’appui humain sur une touche G reste à vérifier manuellement.
- L’absence d’effet sur la fenêtre au premier plan a été vérifiée pour la notification d’exécution. Les propriétés d’interaction de la superposition et son affichage/masquage ont été vérifiés ; des captures ont été générées pour l’assistant, la superposition et la notification.
- Les 27 langues contiennent les 819 chaînes canoniques ; la validation hors ligne des espaces réservés et des clés a réussi.

Les captures et le résultat GUI complet se trouvent dans artifacts/usp-win-x64/smoke. Les tests utilisent des profils temporaires ; les paramètres de production sont sauvegardés et leur empreinte est vérifiée lors du déploiement.
