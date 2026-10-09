# Plan de mise en œuvre du contrôleur Windows

[English (en-US)](../../../en-US/superpowers/plans/2026-10-08-windows-controller.md) | [Deutsch (de-DE)](../../../de-DE/superpowers/plans/2026-10-08-windows-controller.md) | [Français (fr-FR)](2026-10-08-windows-controller.md) | [Italiano (it-IT)](../../../it-IT/superpowers/plans/2026-10-08-windows-controller.md)

> Pour les agents chargés du travail : utiliser des responsabilités parallèles et isolées ; l’utilisateur a explicitement demandé une mise en œuvre complète avec le nombre maximal de sous-agents utiles.

**Objectif :** livrer le panneau natif de raccourcis Windows selon la conception approuvée.
**Architecture :** configuration centrale et exécution annulable ; adaptateurs Windows ; application de bureau WPF.
**Technologies :** .NET 10, WPF, Win32, JSON. Aucune dépendance payante.
**Spécification :** ../specs/2026-10-08-windows-controller-design.md

## Contraintes générales
Interface en allemand ; stockage local ; Windows x64 ; état des capacités matérielles fidèle à la réalité ; aucune journalisation du clavier ; aucun démarrage automatique par défaut.

## Points de vérification
Les imports mal formés ne doivent pas remplacer les paramètres enregistrés ; les modificateurs maintenus ne doivent pas contaminer la sortie ; le débranchement d’un périphérique ne doit pas provoquer de plantage ; la fermeture doit préserver la disponibilité dans la zone de notification ; les macros annulées doivent libérer les ressources.

## Tâches et responsabilités
- [x] Agent Core : src/Aivana.Core et tests/Aivana.Tests. Modèles, persistance JSON, validation, sélection de profil et MacroRunner. Lancer les tests comportementaux en échec, implémenter, puis exécuter tous les tests du cœur.
- [x] Agent Windows : src/Aivana.Windows. RegisterHotKey, inventaire des périphériques, SendInput, processus au premier plan et adaptateur facultatif d’éclairage Corsair. Vérifier les tailles des structures natives et le nettoyage de l’enregistrement avec des tests rapides sûrs.
- [x] Agent GUI : src/Aivana.Desktop/MainWindow.xaml et .cs, ainsi que les seuls assistants d’interface. Tableau de bord sombre, éditeur complet, profils, périphériques, paramètres, vue clavier et éditeur de macros. Compiler lorsque les contrats existent.
- [x] Parent : contrats partagés, fichiers de projet, composition DesktopController, cycle de vie de l’application et de la zone de notification, dialogues d’import/export, documentation, vérifications d’intégration, publication et test rapide de l’application lancée.

## Interfaces partagées
Les contrats se trouvent dans Core/Models.cs. L’implémentation du contrôleur se trouve dans Desktop/DesktopController.cs. L’interface appelle les méthodes du contrôleur par cette classe concrète. Les agents reçoivent les signatures exactes dans leurs tâches. Chacun possède des fichiers distincts. Le parent résout les problèmes entre projets après la fin des agents.

## Vérification et livraison
- [x] dotnet build Aivana.slnx -c Release : aucune erreur.
- [x] dotnet run --project tests/Aivana.Tests -c Release : vérifications comportementales réussies.
- [x] dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64.
- [x] Lancer l’exécutable publié, inspecter l’interface Windows et la stabilité du processus, puis vérifier un parcours sûr de profil, édition et persistance.
- [x] Rédiger le README avec les instructions de compilation et de démarrage, ainsi que les limites matérielles ; consigner les vérifications et les tests matériels restant à effectuer.
