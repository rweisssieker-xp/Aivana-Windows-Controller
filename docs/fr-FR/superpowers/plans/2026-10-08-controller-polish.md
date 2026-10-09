# Plan d’amélioration de l’ergonomie et des capacités du contrôleur

[English (en-US)](../../../en-US/superpowers/plans/2026-10-08-controller-polish.md) | [Deutsch (de-DE)](../../../de-DE/superpowers/plans/2026-10-08-controller-polish.md) | [Français (fr-FR)](2026-10-08-controller-polish.md) | [Italiano (it-IT)](../../../it-IT/superpowers/plans/2026-10-08-controller-polish.md)

Demande de l’utilisateur : améliorer l’apparence, l’ergonomie et les fonctionnalités ; les demandes d’autorisation précédentes prévoyaient le maximum de sous-agents utiles et l’exécution autonome.

## Périmètre

Préserver les profils existants et l’intégration SDK Corsair opérationnelle. Améliorer l’application WPF existante plutôt que de créer un prototype séparé.

- [x] Responsable shell/tableau de bord : disposition réactive, navigation active, commandes lisibles, activité/état compacts, réorganisation des tuiles par glisser-déposer, états vides utiles et modèles. Responsable de MainWindow.xaml, MainWindow.xaml.cs et MainWindow.Dashboard.cs.
- [x] Responsable éditeur : MainWindow.Editor.cs et fichiers partiels facultatifs réservés à l’éditeur. Modifications explicites en brouillon, Enregistrer/Abandonner, validation intégrée, liste de macros dédiée avec glisser-déposer, duplication des étapes, capture fiable des raccourcis et champs de fichiers/URL.
- [x] Responsable Windows : validation native, noms de périphériques et capacités SDK robustes, attente bornée des modificateurs et injection sûre ; responsable de Windows et de ses tests.
- [x] Parent : progression d’exécution centrale, interfaces du contrôleur, préservation de la configuration, tests d’intégration, captures réellement rendues, publication/paquet et remplacement de l’application en cours.

## Contrat d’interface

Le contrôleur fournit les API existantes ainsi que string LastStatus, bool HasRunningActions, bool IsRunning(Guid), string GetTileStatus(Guid), string ForegroundProcess, bool CorsairAvailable, IReadOnlyList<CorsairKeyboardInfo> CorsairKeyboards, Task TestCorsairColorAsync(string) et void OpenIcue(). L’éditeur expose bool HasUnsavedChanges, void DiscardDraft(), bool CommitDraft() ; les noms de champs et méthodes existants restent disponibles dans les classes partielles.

## Critères d’acceptation

Ne pas réinitialiser la configuration réelle de l’utilisateur ; les modifications en brouillon valides sont enregistrées en une seule fois, les brouillons invalides restent modifiables et l’abandon restaure les données enregistrées. Valider les alias de raccourcis natifs sans envoyer d’entrées de test dans des fenêtres sans rapport. Vérifier la réorganisation des tuiles et des macros ; le rendu de toutes les vues aux dimensions de bureau ; l’affichage des états en cours/échec/terminé ; l’affichage du modèle réel du périphérique SDK. Compilation/tests et test rapide après publication réussis, puis lancement de l’application mise à jour.
