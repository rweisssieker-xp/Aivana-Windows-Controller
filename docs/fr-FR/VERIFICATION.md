# Vérification — 8 octobre 2026

[English (en-US)](../en-US/VERIFICATION.md) | [Deutsch (de-DE)](../de-DE/VERIFICATION.md) | [Français (fr-FR)](VERIFICATION.md) | [Italiano (it-IT)](../it-IT/VERIFICATION.md)

L’application a été compilée sous Windows x64 avec le SDK .NET stable 10.0.202, puis publiée en version autonome pour Windows x64.

## Vérifications automatisées

- Compilation complète : aucun avertissement ni erreur.
- 19 tests de comportement Core : configuration par défaut, validation, données nulles/en double, priorité des profils, import/export et restauration de sauvegarde, ordre et répétition des macros, annulation, double déclenchement, sérialisation, instantané d’exécution immuable et erreur d’étape.
- Vérification Windows : normalisation des raccourcis et alias, tailles des structures natives, liste réelle des périphériques clavier, enregistrement et libération sûrs, WM_HOTKEY synthétique pour la tuile et l’annulation, désactivation de l’enregistrement des tuiles pendant la simulation d’entrée.
- L’EXE autonome a été lancé avec `--smoke-test` : les cinq vues WPF s’affichent, les tuiles peuvent être créées/copiées/supprimées, les brouillons sont préservés pendant l’actualisation, la configuration est enregistrée puis rechargée. Ce test utilise un répertoire de configuration temporaire isolé et n’effectue aucune action texte ou média.
- Le tableau de bord, l’éditeur et la présentation compacte (900 × 720) ont été inspectés visuellement avec un rendu WPF réel. L’enregistrement et l’abandon des brouillons, l’ordre des tuiles/macros, les alias allemands de raccourcis, l’annulation de macros individuelles et la progression des étapes ont été vérifiés.

## Limites dépendant du matériel

Le dernier test local a détecté quatre interfaces clavier Windows. Ce nombre décrit les interfaces HID/Windows, pas nécessairement quatre claviers physiques.

Le SDK iCUE officiel est installé à côté de l’EXE. Le test en direct se connecte à iCUE et détecte un clavier Corsair avec les informations de modèle et de LED. Aucun changement d’éclairage n’a été déclenché automatiquement ; les touches spéciales dépendant du modèle et les autres fabricants n’ont pas été testés sur matériel. Les raccourcis génériques fonctionnent sur tous les claviers.

Les entrées texte/média n’ont pas été injectées dans d’autres applications ouvertes de l’utilisateur. Les structures natives et la logique d’erreur/libération ont été vérifiées ; le comportement dans une application cible particulière dépend notamment de son niveau d’autorisation.

## Relancer les vérifications

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64
& ./artifacts/win-x64/Aivana.Controller.exe --smoke-test
```

Le test GUI écrit le résultat et la capture d’écran dans `smoke`, à côté de l’EXE vérifié. Il s’agit d’un mode de diagnostic qui ne lance aucune instance d’arrière-plan permanente à la fin.

## Contrôle étendu

Vérifications supplémentaires : conditions de profil basées sur le processus et le titre de fenêtre, substitution de variables sans expansion récursive, aperçu de macro sans exécution, nouvelle validation d’action et compatibilité descendante des anciens fichiers JSON. Les contrôles natifs couvrent la palette de commandes avec enregistrement, conflits, distribution WM_HOTKEY et libération, ainsi que le découpage en moniteurs en lecture seule et la lecture du presse-papiers à la demande. Le test WPF ouvre et affiche la palette réelle, vérifie Annuler/Rétablir et les favoris, contrôle que les brouillons restent propres après le rendu et se termine correctement.

Le verrouillage de Windows, les changements du presse-papiers et le déplacement de fenêtres n’ont pas été appliqués aux programmes de l’utilisateur pendant les tests. Les actions de fenêtre envoient des demandes Windows asynchrones ; le programme cible peut appliquer ses propres tailles minimales ou limites d’autorisation. Le verrouillage du profil et les statistiques d’exécution ne valent que pour la session en cours.
