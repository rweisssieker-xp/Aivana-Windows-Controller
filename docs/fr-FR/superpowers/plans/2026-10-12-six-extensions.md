<!-- generated-doc locale=fr-FR source=docs/superpowers/plans/2026-10-12-six-extensions.md sha256=0ac83f2f795ba08551082caee7c775af015828dd39e200dd9857aab504cdba01 -->

> Traduction assistée par machine. La structure, les exemples de code et les liens sont vérifiés ; cela ne constitue ni une révision par un locuteur natif ni une validation juridique. Les rapports historiques conservent leur date et leur portée d'origine. [Source originale](../../../superpowers/plans/2026-10-12-six-extensions.md)

# MAKRIVO Plan de mise en œuvre des extensions

> **Pour les travailleurs agentiques :** SOUS-COMPÉTENCE REQUISE : Utilisez superpowers:executing-plans pour l'exécution native ou superpowers:subagent-driven-development une fois que l'utilisateur a sélectionné la délégation. Suivez les étapes avec des cases à cocher.

**Objectif : ** Fournir six extensions intégrées et une prise en charge de clavier multifabricant basée sur les capacités.

**Architecture :** Préserver la séparation Noyau / Windows / WPF. Introduire des processus de plug-in et de services mobiles isolés avec un IPC authentifié étroit ; toute exécution passe par le contrôleur existant et la file d'attente MacroRunner. Les adaptateurs matériels annoncent les capacités détectées plutôt que la prise en charge déduite à l'échelle de la marque.

**Tech Stack : ** C#, .NET 10, WPF, Windows UI Automation, Win32 Raw Input, SDK du fournisseur documentés, ASP.NET Core HTTPS/WebSocket, System.Text.Json.

**Spec:** `docs/superpowers/specs/2026-10-12-six-extensions-design.md` (approuvé par l'utilisateur).

## Contraintes mondiales

- La configuration existante doit se charger sans modification ; ajoutez des types d'action plutôt que de réorganiser leurs valeurs d'énumération persistantes.
- Préservez les profils existants, les affectations G1/G2, le fonctionnement local et l'utilisation sans compte.
- Raw Input fournit uniquement une observation. La suppression exclusive par appareil nécessite un véritable backend de filtre de clavier.
- Les plugins exécutables importés ne doivent pas s'exécuter automatiquement.
- Par défaut, désactivé et restreindre les interfaces configurées pour le service mobile ; pas de solution de secours pour l'authentification en texte clair.
- La livraison d'un raccourci/texte ne constitue pas une preuve que l'application cible a accepté la commande.
- Ne remplacez pas l'application installée, n'installez pas de pilotes, ne modifiez pas les véritables mappages de clavier et n'ouvrez pas l'accès au réseau à titre de test accidentel.
- Conserver les modifications non validées sans rapport. Ne validez pas les modifications partagées existantes dans le cadre de cette fonctionnalité. Ne mettez en scène que des éléments de fonctionnalités pouvant être examinés de manière indépendante si des validations sont demandées.

## Objectif de l'examen

1. Modèles de clavier identiques en double : une reconnexion ambiguë doit nécessiter une nouvelle sélection (tâches 10–12).
2. Annulation pendant les attentes UIA/fournisseur/plugin/réseau : aucune action différée après annulation (Tâches 1, 4, 6, 8).
3. L'utilisateur modifie pendant que l'assistant ou l'aperçu du package est ouvert : la non-concordance des révisions doit empêcher l'écrasement silencieux (tâches 3, 7).
4. La langue et la présentation physique diffèrent : les sélecteurs et les liaisons de scan-code ne doivent pas dépendre des noms d'affichage localisés (tâches 4, 10).
5. Nouvelle tentative réseau et redémarrage du plugin : les commandes ne doivent pas s'exécuter silencieusement deux fois (tâches 6, 8).

## Contrats partagés et ordre de dépendance

Les tâches 1–3 établissent l'exécution et la configuration. Tâches 4–5 build UIA. Les tâches 6–7 fournissent des plugins. Les tâches 8–9 fournissent un accès mobile. Les tâches 10–12 fournissent des adaptateurs de clavier et une intégration de filtres. La tâche 13 valide le produit complet.

Chaque tâche suit un cycle de test d'échec → implémentation minimale → test de réussite. Une tâche n'est terminée que lorsqu'elle est accessible via l'application et que ses vérifications réussissent. Les échafaudages, les faux adaptateurs et la documentation ne suffisent pas à eux seuls à satisfaire un livrable.

### Tâche 1 : Vérification typée et persistance compatible

**Fichiers :** Créer `src/Aivana.Core/ActionVerification.cs`, `tests/Aivana.Tests/VerificationChecks.cs` ; modifier `src/Aivana.Core/Models.cs`, `src/Aivana.Core/MacroRunner.cs`, `src/Aivana.Core/ConfigurationValidator.cs`, `tests/Aivana.Tests/Program.cs`.

**Interfaces :** Produit `VerificationState { Requested, Completed, Verified, Failed, TimedOut, Cancelled, Unverifiable }`, `ActionVerificationResult(VerificationState State, string Code, string Message)`, `IActionVerificationProvider.PreflightAsync(ActionStep, CancellationToken)` et `VerifyAsync(ActionStep, CancellationToken)`, tous deux renvoyant `Task<ActionVerificationResult>`. Ajoutez un `VerificationOptions` sérialisable en option avec un indicateur et un délai d'attente activés (100–60000 ms), désactivé par défaut. Publiez les résultats saisis par étape, y compris l'ID d'exécution, l'ID de vignette et l'index d'étape sans charges utiles d'action.

- [ ] Vérifications d'écriture : l'ancien JSON conserve les valeurs numériques des actions existantes ; les options absentes désactivent la vérification ; le délai d'attente non valide échoue à la validation ; un échec du contrôle en amont empêche l’invocation de l’exécuteur ; l'échec d'une postcondition ne produit jamais de succès vérifié ; l'exécuteur non pris en charge est invérifiable ; l'annulation arrête la vérification et les étapes ultérieures.
- [ ] Exécutez `dotnet run --project tests/Aivana.Tests -c Release` ; confirmer l’échec des nouveaux contrôles avant la mise en œuvre.
- [ ] Implémenter les contrats, la validation, le clonage/préservation d'instantanés et l'intégration de MacroRunner. Respecter les politiques d'arrêt/continuation OnError ; préserver les abonnés existants aux commentaires sur l’exécution.
- [ ] Exécuter des tests de base ; exiger que tous les contrôles soient réussis.

### Tâche 2 : fournisseurs de vérification Windows et interface utilisateur des résultats

**Fichiers : ** Créer `src/Aivana.Windows/WindowsActionVerification.cs`, `src/Aivana.Desktop/MainWindow.Verification.cs`, `tests/Aivana.Windows.Tests/VerificationChecks.cs` ; modifier `src/Aivana.Windows/WinActionExecutor.cs`, `src/Aivana.Desktop/DesktopController.Feedback.cs`, `src/Aivana.Desktop/MainWindow.Editor.cs`, tester les points d'entrée.

**Interfaces :** WindowsActionVerification implémente le fournisseur de tâche 1 à l'aide des clients audio, affichage, processus/fenêtre et OBS existants. Le contrôle en amont teste la disponibilité et les identifiants ; la vérification compare l'état souhaité après une interrogation limitée. Produisez des instantanés des résultats du contrôleur consommés par la vue historique WPF.

- [ ] Ajoutez des tests d'échec pour les ID audio/affichage explicites indisponibles, le délai d'attente OBS, l'état observé qui ne correspond pas, les effets de raccourci inconnus, l'omission de charge utile sensible et l'interrogation annulée, à l'aide de fournisseurs de lecture injectables.
- [ ] Implémentez une véritable vérification par relecture pour le volume absolu, la sourdine, la luminosité absolue, la préparation du processus/fenêtre et les commandes OBS prises en charge. Les actions relatives/à bascule nécessitent un pré-état capturé ; sinon, signalez Invérifiable.
- [ ] Ajoutez des contrôles de vérification par étape et un historique d'exécution sécurisé affichant l'état, l'exigence ayant échoué et le texte de récupération.
- [ ] Exécutez les contrôles core/Windows et la fumée WPF avec les fournisseurs d'État injectés ; pas de réelle luminosité ni d'écriture audio.

### Tâche 3 : Création guidée d'un espace de travail

**Fichiers : ** Créer `src/Aivana.Core/WorkspaceSetup.cs`, `src/Aivana.Windows/WorkspaceDiscovery.cs`, `src/Aivana.Desktop/MainWindow.WorkspaceWizard.cs`, `tests/Aivana.Tests/WorkspaceSetupChecks.cs` ; modifier les points d'entrée des palettes et des tableaux de bord.

**Interfaces :** `WorkspaceSetup.CreateProfile(WorkspaceSelection selection) -> Profile` ; La sélection contient l'ID du modèle, les chemins d'exécution choisis, les URL, les ID audio/affichage, la scène OBS et les liaisons sélectionnées. Discovery renvoie les candidats nommés avec des identifiants et une disponibilité stables. Projet de révision de la configuration des enregistrements pour la détection des conflits.

- [ ] Ajoutez les tests ayant échoué pour les cinq familles d'assistants demandées, les étapes générées indépendamment, les cibles requises manquantes, annulez sans mutation et enregistrez la révision modifiée.
- [ ] Découvrez les applications via les chemins d'application enregistrés et les raccourcis du menu Démarrer ; appareils via des fournisseurs existants ; scènes via une requête OBS en lecture seule. Gardez la sélection manuelle disponible pour les applications non découvertes.
- [ ] Implémenter Choisir le workflow → Sélectionner les cibles → Choisir les contrôles → Prévisualiser/valider → Enregistrer. Générez des profils modifiables et ne les exécutez jamais pendant l'installation.
- [ ] Ajoutez les vérifications de fumée réelles de l'assistant WPF pour l'achèvement/l'annulation/le conflit ; exécuter tous les tests.

### Tâche 4 : sélecteurs UIA et actions limitées

**Fichiers : ** Créer `src/Aivana.Core/UiAutomationAction.cs`, `src/Aivana.Windows/UiAutomationService.cs`, `tests/Aivana.Windows.Tests/UiAutomationChecks.cs` ; modifier les modèles, la validation de la configuration et WinActionExecutor.

**Interfaces :** Ajouter `ActionKind.UiAutomation` ; `UiElementSelector` stocke le processus, l'ID d'automatisation, le type de contrôle et les critères d'ancêtre ordonnés. `UiAutomationService.ExecuteAsync(UiAutomationAction action, CancellationToken)` renvoie `Task` ; opérations Invoke, SetValue, Select, Toggle, Focus et WaitState. La résolution du sélecteur doit renvoyer exactement un élément éligible.

- [ ] Ajoutez des tests ayant échoué dans une fenêtre de test dédiée pour la relocalisation/le redimensionnement, les correspondances en double, les éléments manquants, les modèles non pris en charge, les contrôles désactivés, les valeurs de mot de passe, les étiquettes Unicode et le délai d'attente/annulation.
- [ ] Implémentez UIA sur un travailleur dédié avec des requêtes limitées et un ciblage de processus. Les fournisseurs potentiellement suspendus s'exécutent derrière une limite de travail redémarrable ; l'abandon d'une demande ne doit pas laisser d'effets secondaires en file d'attente.
- [ ] Exclure les valeurs de mot de passe ; signaler les restrictions d’accès/d’intégrité au lieu de les contourner.
- [ ] Exécutez les contrôles Windows en utilisant des appareils isolés ; ajoutez uniquement les valeurs d'énumération et réexécutez les vérifications de persistance héritées.

### Tâche 5 : sélecteur UIA et enregistrement révisé

**Fichiers :** Créer `src/Aivana.Desktop/MainWindow.UiAutomation.cs`, `src/Aivana.Windows/UiAutomationRecorder.cs` ; modifier le catalogue d'actions, l'intégration de l'éditeur et de l'enregistreur ; créer des contrôles de fumée UIA WPF.

**Interfaces :** Picker renvoie un `UiElementSelector` ; L'enregistreur renvoie les étapes UIA proposées et n'écrit jamais directement dans un profil enregistré. La commande Test-selector renvoie le nombre de correspondances sans appel.

- [ ] Ajouter les vérifications ayant échoué : le sélecteur exclut MAKRIVO et la capture de la valeur du mot de passe ; l'enregistrement/l'annulation laisse le profil existant inchangé ; les rapports d'aperçu ambigus correspondent au nombre ; l'acceptation des étapes enregistrées crée un brouillon modifiable.
- [ ] Implémentez la mise en surbrillance/le sélecteur d'éléments, les paramètres spécifiques à l'opération, l'aperçu du sélecteur et la fenêtre de révision de l'enregistreur. Conserver le comportement existant de l’enregistreur de raccourcis.
- [ ] Ajoutez des entrées de catalogue et un accès à la palette ; validez les erreurs de l’éditeur avant de sauvegarder.
- [ ] Exécutez les contrôles de fumée Windows et WPF sur le luminaire ; inspectez les captures d’écran du sélecteur/éditeur.

### Tâche 6 : hôte et protocole de plug-in isolés

**Fichiers : ** Créer `src/Aivana.Core/Plugins/PluginContracts.cs`, `src/Aivana.Windows/Plugins/PluginProcessClient.cs`, `src/Aivana.PluginHost/Aivana.PluginHost.csproj`, `src/Aivana.PluginHost/Program.cs`, `tests/Aivana.Tests/PluginProtocolChecks.cs` ; modifier la répartition des solutions et des actions.

**Interfaces :** Version-1 Le manifeste spécifie l'ID/la version du package, le point d'entrée, les schémas de paramètres, les actions, les fournisseurs d'état et les déclarations de capacités. Les messages IPC contiennent la version du protocole, l'ID de demande unique, la méthode, les paramètres JSON et le résultat saisi. `PluginProcessClient.InvokeAsync(string actionId, JsonElement parameters, CancellationToken)` renvoie un résultat typé. Ajouter `ActionKind.Plugin`.

- [ ] Ajoutez des tests ayant échoué pour un protocole incompatible, un message mal formé/surdimensionné (> 1 MiB), un ID de demande en double, une annulation, un crash, un processus qui ne répond pas et une action manquante.
- [ ] Implémentez l'IPC de canal nommé du même utilisateur avec une négociation de 10 secondes et un délai d'expiration configurable plafonné à 60 secondes. Licencier le travailleur qui ne répond pas ; ne rejoue pas automatiquement une action en vol après le redémarrage.
- [ ] Appliquer les schémas et les services de contrôleur autorisés. Expliquer la confiance dans le plugin exécutable ; l'isolation des processus n'est pas un bac à sable du système d'exploitation.
- [ ] Exécutez des tests de base et des tests d'intégration sur de vrais processus de plugin de test.

### Tâche 7 : packages de plugins et exemples d'intégration

**Fichiers :** Créer `src/Aivana.Core/Plugins/PluginPackage.cs`, `src/Aivana.Desktop/MainWindow.Plugins.cs`, `samples/Aivana.ExamplePlugin/`, tests de package ; modifier les entrées de la palette/des paramètres.

**Interfaces :** Le service de package expose `InspectAsync(path)`, `InstallAsync(reviewedPackage, revision)`, `ExportAsync(packageId, destination)`, `DisableAsync(packageId)` et `UninstallAsync(packageId)`. Le paquet examiné enregistre les hachages, les dépendances, la version et les capacités déclarées.

- [ ] Ajoutez des tests d'échec pour la traversée zip, les chemins absolus, l'échappement de lien symbolique/analyse, les entrées en double, les données étendues >100 MiB, l'incompatibilité de hachage, la rétrogradation, la révision obsolète et les importations désactivées.
- [ ] Implémenter l'aperçu et l'activation explicite, les erreurs de dépendance, l'installation par étapes et la restauration de la mise à niveau ; décharger le travailleur avant son retrait.
- [ ] Livrer un exemple réel de plugin renvoyant une action de compte à rebours configurable et un état en direct via IPC, y compris l'annulation ; package/export/installation/désinstallation via l’interface utilisateur.
- [ ] Exécutez les tests de package/hôte et les vérifications de bout en bout WPF. Vérifiez qu’aucune exécution d’exécutable importé avant l’activation.

### Tâche 8 : service mobile HTTPS et appairage

**Files : ** Créer un projet `src/Aivana.MobileHost/` ASP.NET Core, `src/Aivana.Core/MobilePanelContracts.cs`, `src/Aivana.Windows/MobilePanelClient.cs`, tests d'intégration ; modifier la solution et le contrôleur.

**Interfaces :** Le service mobile communique avec le contrôleur via l'IPC du même utilisateur. Itinéraires : appairage, profils/actions autorisés, invocation d'action authentifiée, annulation et statut WebSocket. L'appel inclut un ID de commande unique ; le contrôleur possède la déduplication limitée. Les informations d'identification sont révocables et stockées via la protection Windows.

- [ ] Ajoutez des tests d'échec pour l'énumération/l'exécution non appariée, le code expiré, le code réutilisé, le jeton révoqué, l'origine invalide, la commande en double, la trame surdimensionnée et les limites d'inondation.
- [ ] Implémentez un écouteur HTTPS uniquement, des interfaces sélectionnées par l'utilisateur, des instructions d'approvisionnement/de confiance de certificat local, des codes d'appariement uniques à chiffres 6 expirant après 120 secondes, un maximum de cinq tentatives par code et une révocation de jeton. Stockez des jetons aléatoires hachés ; n’enregistrez jamais les informations d’identification.
- [ ] Implémenter les ID d'action stables sur liste blanche et le routage de file d'attente existant ; refuser le shell, les chemins et les commandes d'installation de package arbitraires. Configurez des limites de débit explicites par client (commandes 10/s, rafale 30) et la limite de messages 64 KiB.
- [ ] Vérifiez les tests d'intégration de bouclage sans ajouter de règles de pare-feu ni activer l'accès au réseau local ; test d'arrêt et de persistance du jeton.

### Tâche 9 : Contrôles des services de panneau mobile et de bureau

**Files : ** Créer un panneau réactif `src/Aivana.MobileHost/wwwroot/` et `src/Aivana.Desktop/MainWindow.MobilePanel.cs` ; ajoutez des scripts de vérification de navigateur/mobile et une couverture de fumée WPF.

**Interfaces :** Consommer la tâche 8 itinéraires uniquement. Exposez la grille d'action tactile, le sélecteur de profil, les valeurs/progression en direct, l'annulation, l'état de la connexion et l'appairage. Desktop contrôle le démarrage/l’arrêt du service, les instructions d’adresse/certificat, les profils autorisés et la révocation des clients couplés.

- [ ] Ajoutez des contrôles d'intégration défaillants pour le flux d'authentification, les ID d'action obsolètes/manquants, la reconnexion sans réexécution d'action, le client révoqué et l'arrêt du service.
- [ ] Implémentez une interface utilisateur réactive et accessible à l'aide d'actifs locaux, sans scripts tiers ni dépendances Internet. Les jetons n'apparaissent jamais dans les URL.
- [ ] Testez la disposition du navigateur sur 360/768/1280 CSS px ; inspectez les captures d’écran réelles. Vérifiez avec un vrai téléphone/tablette uniquement après que l'utilisateur a explicitement activé le service LAN.
- [ ] Documentez et signalez la vérification du matériel mobile séparément de l'émulation du navigateur.

### Tâche 10 : capacités de l'appareil et observation à clé ordinaire

**Fichiers : ** Créer `src/Aivana.Core/KeyboardCapabilities.cs`, `src/Aivana.Windows/KeyboardAdapterRegistry.cs`, `src/Aivana.Desktop/MainWindow.KeyboardAdapters.cs` ; modifier RawKeyboardBindingService, KeyboardBinding, le validateur et le diagnostic de l'appareil ; ajouter des tests.

**Interfaces :** `IKeyboardAdapter` expose des instantanés de fonctionnalités, l'identité stable de l'appareil et les bords des touches physiques (code d'analyse, indicateurs étendus, pression/relâchement). Les modes distinguent l'observation, l'entrée standard mappée, le SDK natif et le pilote exclusif. Les liaisons spéciales de clé virtuelle existantes conservent leur représentation et leur comportement de migration.

- [ ] Ajoutez des tests d'échec pour les anciennes liaisons, les distinctions de saisie, l'indépendance de la langue/de la mise en page, les arrêts répétés, le débranchement pendant le maintien, l'ambiguïté du modèle en double et le rejet des entrées injectées.
- [ ] Étendre l'observation aux clés physiques ordinaires sans les supprimer. Ajoutez une capture explicite et une gestion des identités de reconnexion.
- [ ] Afficher les capacités réelles de l'adaptateur et l'état du matériel non testé dans l'interface utilisateur de l'appareil ; ne déduisez jamais la prise en charge Fn/dial des noms de marque.
- [ ] Exécutez les vérifications core/Windows/WPF avec des rapports simulés et une énumération des périphériques en lecture seule.

### Tâche 11 : Adaptateurs du fabricant

**Files :** Créez des fichiers séparés sous `src/Aivana.Windows/KeyboardAdapters/` pour LogitechG, RazerChroma, SteelSeriesGameSense, WootingAnalog et les conseils génériques de QmkVia ; modifiez le registre/l'interface utilisateur et ajoutez des vérifications de protocole d'adaptateur.

**Interfaces :** Consommer la tâche 10 contrats d'adaptateur. Les bibliothèques natives se chargent uniquement à partir d'emplacements d'applications/fournisseurs validés ; L’absence du SDK est une fonctionnalité indisponible. Les capacités d’éclairage et d’entrée sont distinctes.

- [ ] Vérifiez les en-têtes officiels actuels du SDK, les conditions de redistribution, les dépendances logicielles et les modèles pris en charge avant d'implémenter chaque adaptateur ; enregistrer les sources et les versions exactes.
- [ ] Ajoutez des tests d'échec pour l'architecture SDK absente/mauvaise, la pression/libération/répétition d'un événement, la déconnexion, la réponse mal formée et le périphérique non pris en charge. Utilisez des abstractions d'appels natifs injectables pour les contrôles déterministes.
- [ ] Implémentez l'entrée Logitech G-key et l'éclairage pris en charge séparément ; Éclairage Chroma Razer et assistant de mappage de touches standard Synapse ; SteelSeries GameSense état/éclairage ; Wooting sondage analogique avec seuils/hystérésis configurables ; Guide de configuration des touches standard QMK/VIA. Ne revendiquez pas les entrées exclusives des API d’éclairage.
- [ ] Validez les appareils réels compatibles lorsqu'ils sont disponibles. Marquer les autres implémentés/matériels non testés, avec des preuves de modèle/micrologiciel ; Le SDK utilisable manquant est une porte externe divulguée.

### Tâche 12 : backend de filtre de clavier exclusif

**Files :** Créez `src/Aivana.Windows/KeyboardFilter/`, source d'installation sous `tools/keyboard-filter/`, tests d'intégration backend et documentation de déploiement. Un projet de pilote signé dédié est requis si aucun backend sous licence approprié ne réussit l'évaluation.

**Interfaces :** Le backend rapporte l'état installé/compatible/signé ; expose le démarrage/arrêt de la capture spécifique à l'appareil, les bords clés et le déverrouillage d'urgence. Un chien de garde distinct rétablit le transfert en cas de perte du contrôleur. Le clavier principal ne devient jamais exclusif par défaut.

- [ ] Évaluez le permis de conduire candidat, la provenance binaire signée et la compatibilité actuelle de démarrage Windows 11/Secure. Enregistrez une décision d'aller/retour. Aucune installation de pilote lors des tests ordinaires.
- [ ] Rédiger des tests contractuels pour la sélection de la cible du filtre, l'ouverture en cas d'échec, l'expiration du battement de cœur, la perte de l'IPC, l'ambiguïté de reconnexion, la pause/annulation, la libération et l'élimination de la clé conservée.
- [ ] Implémenter le backend, l'interface utilisateur de sélection/activation explicite, la version d'urgence et l'installateur/désinstallateur. Ne remplacez pas l'heuristique de synchronisation de crochet de bas niveau/Raw Input.
- [ ] Valider sur une machine de test récupérable avec deux claviers : touche sélectionnée supprimée uniquement sur le périphérique sélectionné ; la saisie principale n'est pas affectée ; crash/suspension/désinstallation restaure l'entrée.
- [ ] Achèvement de la production du portail sur le backend réel signé/sous licence et les tests physiques. Si la signature/licence externe n'est pas disponible, gardez la tâche incomplète avec des preuves de dépendance précises.

### Tâche 13 : Vérification intégrée, traductions et package de publication

**Fichiers :** Ajouter `docs/testing/2026-10-12-six-extensions.md` ; mettre à jour la documentation DE/EN/FR/IT pertinente, les catalogues de localisation et les points d'entrée des contrôles de fumée. Conservez les autres modifications de la documentation.

- [ ] Intégrer tous les points d'entrée et instantanés ; compléter les traductions à l'aide du flux de travail du catalogue de langues 27 existant et divulguer les traductions non révisées.
- [ ] Exécutez `dotnet build Aivana.slnx -c Release`, `dotnet run --project tests/Aivana.Tests -c Release`, `dotnet run --project tests/Aivana.Windows.Tests -c Release` et `dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test`.
- [ ] Inspectez les captures d'écran de bureau/mobile et vérifiez la mise à l'échelle du pourcentage 100/150/200 Windows, les allers-retours de configuration, le cycle de vie du plugin, la révocation du réseau et la reconnexion réelle du clavier.
- [ ] Publier dans un nouveau répertoire d'artefacts avec `dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/six-extensions/win-x64` ; inclure les binaires du service compagnon/hôte et les avis de licence. N'écrasez pas l'application installée.
- [ ] Signaler les tests terminés, les adaptateurs matériels non testés et les portes de version externes. Les six livrables doivent répondre à leurs critères d’acceptation avant de qualifier l’intégralité de la demande de complète.

## Auto-évaluation du plan

Les six sections de spécifications correspondent aux tâches 1–12 ; La tâche 13 couvre la localisation, la vérification intégrée et la publication. Les rapports de préparation, la sérialisation de l'exécution, la configuration héritée, les secrets, la confiance des packages et l'ambiguïté des périphériques physiques ont des propriétaires et des tests explicites. Les portes externes des pilotes/SDK/matériel restent incomplètes jusqu'à ce qu'elles soient prouvées, plutôt que comptées comme un succès logiciel.

## Choix d'exécution pour examen par l'utilisateur

Recommandé : exécution native dans ce chat, tâche par tâche, car la plupart des modules partagent des contrats et l'espace de travail actuel contient des modifications existantes substantielles. L'exécution déléguée est disponible si l'utilisateur le choisit. Examinez ce plan et sélectionnez la méthode d'exécution avant la mise en œuvre, comme l'exige le flux de travail de rédaction des plans.
