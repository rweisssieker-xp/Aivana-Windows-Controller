<!-- generated-doc locale=fr-FR source=docs/superpowers/plans/2026-10-08-windows-controller.md sha256=10a5ba49bbefc617bce40563c668e3c4628647822117087deba766713fb9b3af -->

> Traduction assistée par machine. La structure, les exemples de code et les liens sont vérifiés ; cela ne constitue ni une révision par un locuteur natif ni une validation juridique. Les rapports historiques conservent leur date et leur portée d'origine. [Source originale](../../../superpowers/plans/2026-10-08-windows-controller.md)

# Plan de mise en œuvre du contrôleur Windows

[English (US)](2026-10-08-windows-controller.md) | [Deutsch](2026-10-08-windows-controller.md) | [Français](2026-10-08-windows-controller.md) | [Italiano](2026-10-08-windows-controller.md)

> Pour les travailleurs agents : utilisez une propriété parallèle isolée ; l'utilisateur a explicitement demandé une implémentation complète avec un maximum de sous-agents utiles.

**Objectif : ** Fournir le panneau de raccourcis clavier natif Windows dans la conception approuvée.
**Architecture :** Configuration de base et exécution annulable ; Adaptateurs Windows ; Composition de bureau WPF.
**Tech Stack : ** .NET 10, WPF, Win32, JSON. Pas de dépendances payantes.
**Spec:** ../specs/2026-10-08-windows-controller-design.md

## Contraintes mondiales
Interface utilisateur allemande ; stockage local ; Windowsx64 ; état véridique des capacités matérielles ; pas de journalisation du clavier ; pas de démarrage automatique par défaut.

## Objectif de l'examen
Les importations mal formées ne doivent pas remplacer les paramètres enregistrés ; les modificateurs retenus ne doivent pas contaminer la sortie ; le matériel débranché ne doit pas planter ; la fermeture doit préserver la disponibilité des plateaux ; les macros annulées doivent libérer des ressources.

## Tâches et propriété
- [x] Agent principal : src/Aivana.Core et tests/Aivana.Tests. Modèles, persistance JSON, validation, sélection de profil et MacroRunner. Exécutez les tests comportementaux ayant échoué, puis implémentez et exécutez tous les tests de base.
- [x] Agent Windows : src/Aivana.Windows. RegisterHotKey, inventaire des appareils, SendInput, processus de premier plan et adaptateur d'éclairage Corsair en option. Vérifiez les tailles de structure natives et le nettoyage des enregistrements avec des tests de fumée sécurisés.
- [x] Agent GUI : src/Aivana.Desktop/MainWindow.xaml et .cs plus assistants d'interface utilisateur uniquement. Tableau de bord sombre, éditeur complet, profils, appareils, paramètres, aperçu du clavier et éditeur de macros. Construire une fois que les contrats existent.
- [x] Parent : contrats partagés, fichiers de projet, composition de DesktopController, cycle de vie des applications/plateaux d'état, boîtes de dialogue d'importation/exportation, documentation, contrôles d'intégration, test de fumée de publication et d'exécution de l'application.

## Interfaces partagées
Les contrats résident dans Core/Models.cs. L’implémentation du contrôleur réside dans Desktop/DesktopController.cs. L'interface utilisateur appelle les méthodes du contrôleur via cette classe concrète. Les agents reçoivent des signatures exactes dans leurs tâches. Chacun possède des fichiers séparés. Le parent résout les échecs entre projets une fois que tous les agents ont terminé.

## Vérification et livraison
- [x] dotnet build Aivana.slnx -c Release : zéro erreur.
- [x] dotnet run --project tests/Aivana.Tests -c Release : les vérifications comportementales réussissent.
- [x] dotnet publier src/Aivana.Desktop -c Release -r win-x64 --self-contained true -oartefacts/win-x64.
- [x] Lancez l'exécutable publié, inspectez l'interface utilisateur de Windows et la stabilité des processus, exercez un flux de profil/édition/persistance sécurisé.
- [x] Écrivez un fichier README avec les instructions de construction/démarrage et les limitations matérielles ; les vérifications d'enregistrement et les tests restants uniquement sur le matériel.
