<!-- generated-doc locale=fr-FR source=docs/superpowers/plans/2026-10-08-controller-polish.md sha256=1a076a67eea31048f6305f30a121c474d592451d1ddcd9b46bc339fd8ec068f2 -->

> Traduction assistée par machine. La structure, les exemples de code et les liens sont vérifiés ; cela ne constitue ni une révision par un locuteur natif ni une validation juridique. Les rapports historiques conservent leur date et leur portée d'origine. [Source originale](../../../superpowers/plans/2026-10-08-controller-polish.md)

# Plan d’utilisation et d’amélioration des capacités du contrôleur

[English (US)](2026-10-08-controller-polish.md) | [Deutsch](2026-10-08-controller-polish.md) | [Français](2026-10-08-controller-polish.md) | [Italiano](2026-10-08-controller-polish.md)

Demande de l'utilisateur : améliorer l'apparence, la convivialité et la fonctionnalité ; l'autorisation préalable demande le maximum de sous-agents utiles sur les modèles plus petits nécessaires et l'achèvement autonome.

## Portée
Préservez les profils existants et l’intégration fonctionnelle du SDK Corsair. Améliorez l'application WPF existante plutôt que de créer un prototype distinct.

- [x] Travailleur Shell/tableau de bord : mise en page réactive, navigation active, commandes lisibles, activité/statut compact, réorganisation des vignettes par glisser-déposer, états et modèles vides utiles. Posséder MainWindow.xaml, MainWindow.xaml.cs et MainWindow.Dashboard.cs.
- [x] Travailleur de l'éditeur : MainWindow.Editor.cs et fichiers partiels facultatifs réservés à l'éditeur. Modifications par étapes explicites, enregistrement/rejet, validation en ligne, liste de macros dédiée avec glisser-déposer, étapes en double, capture de raccourcis clavier fiable et champs de fichier/URL.
- [x] Travailleur Windows : validation native, noms de périphériques et capacités SDK robustes, attentes de modificateur limitées et injection sécurisée ; propres tests Windows et Windows.
- [x] Parent : progression de l'exécution du noyau, interfaces du contrôleur, préservation de la configuration, tests d'intégration, captures d'écran réelles rendues, publication/package et remplacement de l'application en cours d'exécution.

## Contrat d'interface
Le contrôleur fournit les API existantes ainsi que la chaîne LastStatus, bool HasRunningActions, bool IsRunning(Guid), string GetTileStatus(Guid), string ForegroundProcess, bool CorsairAvailable, IReadOnlyList<CorsairKeyboardInfo> CorsairKeyboards, Task TestCorsairColorAsync(string), void OpenIcue(). L'éditeur expose bool HasUnsavedChanges, void DiscardDraft(), bool CommitDraft(); Les noms de champs/méthodes existants restent disponibles dans les classes partielles.

## Acceptation
Pas de réinitialisation de la configuration utilisateur réelle ; les modifications par étapes valides sont enregistrées une fois, les brouillons non valides restent modifiables, l'annulation restaure les données enregistrées. Alias ​​de raccourci clavier natifs validés sans injecter d’entrées de test dans des fenêtres non liées. Réorganisation des tuiles et des macros testée ; toutes les vues s'affichent à la taille d'un ordinateur de bureau ; état en cours d'exécution/échec/complet visible ; Le modèle réel du périphérique SDK est affiché. Construction/tests et passe de fumée publié, et l'application mise à jour est lancée.
