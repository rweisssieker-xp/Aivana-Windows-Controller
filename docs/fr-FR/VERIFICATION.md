<!-- generated-doc locale=fr-FR source=docs/VERIFICATION.md sha256=f19afb9e7b5ffac90c83167416ab3af0964a4481dd16b7357f8585480c6aae65 -->

> Traduction assistée par machine. La structure, les exemples de code et les liens sont vérifiés ; cela ne constitue ni une révision par un locuteur natif ni une validation juridique. Les rapports historiques conservent leur date et leur portée d'origine. [Source originale](../VERIFICATION.md)

# Examen – 8. Octobre 2026

[English (US)](../en-US/VERIFICATION.md) | [Deutsch](../de-DE/VERIFICATION.md) | [Français](VERIFICATION.md) | [Italiano](../it-IT/VERIFICATION.md)

L'application a été construite sur Windows x64 avec le SDK .NET stable 10.0.202 et publiée en tant que Windows-x64-Ausgabe autonome.

## Vérifié automatiquement

- Construction globale : aucun avertissement et aucune erreur.
- 19 Tests de comportement de base : configuration par défaut, validation, données nulles/dupliquées, priorité du profil, importation/exportation et restauration de sauvegarde, commande et nouvelle tentative de macro, abandon, double déclenchement, sérialisation, instantané d'exécution immuable et échec d'étape.
- Vérification Windows : normalisation des raccourcis clavier et alias, tailles de structure natives, liste des périphériques de clavier réels, enregistrement et partage sécurisés, WM_HOTKEY synthétique pour les vignettes et l'annulation, enregistrement des vignettes désactivé pendant la simulation d'entrée.
- L'EXE autonome a été démarré avec `--smoke-test` : les cinq vues WPF sont rendues, les vignettes peuvent être créées/copiées/supprimées, les brouillons sont conservés lors de l'actualisation, la configuration est enregistrée et rechargée. Ce test utilise un répertoire de configuration temporaire isolé et n'effectue aucune action textuelle ou multimédia.
- Le tableau de bord, l'éditeur et la représentation compacte (900 × 720) ont été inspectés visuellement comme de véritables rendus WPF. L'enregistrement des brouillons, la suppression, l'ordre des vignettes/macros, les alias de raccourcis clavier allemands, les annulations de macros individuelles et la progression des étapes sont vérifiés.

## Limites dépendantes du matériel

La dernière exécution locale a détecté quatre interfaces clavier Windows. Ce numéro décrit les interfaces HID/Windows, pas nécessairement quatre claviers physiques.

Le SDK officiel iCUE est installé et se trouve à côté de l'EXE. Le test en direct se connecte à iCUE et détecte un clavier Corsair avec des informations sur le modèle et les LED. Les changements d'éclairage n'étaient pas déclenchés automatiquement ; Les clés spéciales dépendant du modèle et d'autres fabricants ne sont pas testées sur le matériel. Les raccourcis clavier génériques s'appliquent à tous les claviers.

La saisie de texte/média n’a pas été injectée dans d’autres agents utilisateurs ouverts. Les structures de données natives et la logique erreur/libération ont été vérifiées ; Le comportement dans un programme cible spécifique dépend, entre autres, de son niveau d'autorisation.

## Répéter

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64
& ./artifacts/win-x64/MAKRIVO.exe --smoke-test
```

Le test GUI écrit le résultat et la capture d'écran dans `smoke` à côté de l'EXE testé. Il est conçu comme un mode de diagnostic et ne lance pas d'instance persistante en arrière-plan une fois l'opération terminée.


## Contrôles avancés

Vérifié en plus : conditions de profil avec titre de processus et de fenêtre, remplacement de variable sans expansion récursive, aperçu de macro sans exécution, validation de nouvelles actions et compatibilité ascendante des anciens fichiers JSON. Les vérifications natives couvrent la palette de commandes avec l'enregistrement, les conflits, l'envoi et la libération de WM_HOTKEY, ainsi que le fractionnement pur du moniteur et l'accès en lecture au presse-papiers à la demande. Le test WPF ouvre et restitue la palette réelle, vérifie les annulations/rétablissements et les favoris, vérifie les brouillons propres après le rendu et termine le processus avec succès.

Le verrouillage Windows, les modifications du presse-papiers et le mouvement des fenêtres n'ont pas été appliqués aux agents utilisateurs pendant les tests. Publier les actions de la fenêtre Windows demande de manière asynchrone ; Le programme cible spécifique peut avoir ses propres tailles minimales ou limites d'autorisation. Le verrouillage du profil et les statistiques d'exécution s'appliquent à la session en cours.


Examen d'extension de 12.10.2026 : examens de base 30 réussis ; Adaptateur Windows incluant la libération pendant l'injection d'entrée, la détection du moniteur en lecture seule et la simulation d'état OBS réussie. WPF vérifie la validation/annulation du flux, le démarrage/arrêt de l'enregistreur, l'aperçu de l'échange de profil, les gestes et les ressources de symboles. Localisation : catalogues 27 chacun avec des clés 967 ; Documentation : pages 84 en quatre langues. Les boutons propriétaires autres que Corsair n'ont pas été testés sur le matériel ; La prise en charge générique inclut les événements standard spécifiques à l'appareil F13–F24 et les clés multimédias. Le transport dans le cloud s'effectue via un dossier synchronisé sélectionné ; Les importations sont vérifiées et restent désactivées.
