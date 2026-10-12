<!-- generated-doc locale=fr-FR source=docs/testing/2026-10-12-six-extensions.md sha256=ff2eb755b2f18dce8c7bd7c832018ae71ee706bcca65e37c191dbb55ff999c1e -->

> Traduction assistée par machine. La structure, les exemples de code et les liens sont vérifiés ; cela ne constitue ni une révision par un locuteur natif ni une validation juridique. Les rapports historiques conservent leur date et leur portée d'origine. [Source originale](../../testing/2026-10-12-six-extensions.md)

# Six extensions : implémentation et banc de tests

Statut : 12. Octobre 2026, succursale `codex/six-extensions`. Statut de fonctionnement, pas de version de production.

## Service

La palette de commandes ouvre les entrées **Configurer le poste de travail**, **Fabricant de clavier et SDK**, **Panneau mobile**, **Extensions**, **UI automation**, **UI record flow** et **Execution test**.

- Workspace Assistant crée des profils Workplace, Meeting, Streaming, Research et Project. Les programmes du registre/menu Démarrer ou du sélecteur de fichiers, les URL, la sortie/écran et la scène OBS sont affichés sous forme d'étapes avant d'être enregistrés. L'installation n'effectue aucune action.
- Dans l'éditeur d'étapes, **Check result** active le contrôle préliminaire et le post-contrôle. L'historique et le panneau mobile distinguent la confirmation réelle de l'effet invérifiable. Les modifications relatives, les raccourcis clavier et la saisie de texte ne fournissent pas une confirmation générale de l'efficacité. La preuve du processus ne prouve pas la préparation technique de la demande.
- UI Automation utilise un travailleur distinct et limité dans le temps. Sélection via la position de la souris, vérification du sélecteur, Invoke/Select/Toggle/Focus/SetValue/WaitState ; les accès multiples, les cibles désactivées et les champs de mot de passe sont rejetés. L'enregistrement capture les actions de clic et les pauses prises en charge ; Les entrées de texte ne sont pas enregistrées. L'admission sera vérifiée avant l'acceptation.
- Extensions : inspection ZIP, hachages, schéma, importation désactivée, confiance explicite avant activation, processus isolé avec IPC limité, exportation, désactivation, désinstallation, restauration/élimination de la version précédente. L'isolation des processus n'est pas un bac à sable : les plugins partagés s'exécutent avec des droits d'utilisateur. L’exemple de package `makrivo.example.zip` contient un compte à rebours et une action d’état. Les schémas pris en charge représentent un sous-ensemble de schéma JSON simple documenté dans le code source ; pas de marché public.
- Panneau mobile : sélectionnez l'IP locale et les profils partagés, ouvrez l'adresse HTTPS, vérifiez le certificat à l'aide du SHA-256-Fingerprints affiché et faites-lui confiance consciemment, puis entrez un code unique à six chiffres. Les clés privées sont protégées par Windows-DPAPI ; seuls les hachages d'accès client sont enregistrés. Les certificats changent après le renouvellement ou le changement d'IP. Le pare-feu Windows est libéré par l'utilisateur si nécessaire. Aucune installation automatique de certificat, redirection de port ou changement de pare-feu. L'accès client peut être révoqué. Le panneau affiche l'état de l'action et l'historique des tests ; ce n'est pas encore un tableau de bord universel avec capteurs en direct.

## Claviers

| Fabricant | Mise en œuvre | Limitation |
|---|---|---|
| Corsair | Touches iCUE-G et RVB existantes | Connexion locale iCUE et modèle approprié requis |
| LogitechG | SDK G-Key et SDK LED séparé | libérer explicitement le x64-DLL local d'origine ; Le SDK G-Key ne fournit pas l'identité individuelle de plusieurs claviers |
| Razer | Chroma RVB et reconnaissance des touches standard | Attribuez des clés propriétaires précédemment dans Synapse |
| SteelSeries | GameSense-RGB et reconnaissance de clé standard | attribuer des clés propriétaires précédemment dans GG |
| Wooting | Valeurs analogiques liées à l'appareil, seuils de pression/relâchement | local d'origine x64-SDK-DLL ; l'entrée HID normale reste active |
| Keychron/QMK/VIA/Ducky/ASUS/HyperX/Roccat/autres | Boutons physiques standards par appareil et scan code | Les commandes Fn et propriétaires dépendent du modèle |

Aucune nouvelle DLL SDK propriétaire n’est incluse. Les nouveaux adaptateurs du fabricant ne sont pas encore validés matériellement sans dispositifs physiques appropriés.

**Le deuxième macropad exclusif n'est pas prêt :** Raw Input observe les appareils séparément, mais ne supprime pas leur entrée Windows normale. Une version de production nécessite un pilote de filtre sous licence et signé, vérifié sous Windows 11/Secure Tests de démarrage et de version d'urgence, de blocage, de suspension et de reconnexion. Aucun pilote n'a été installé. L'application marque explicitement cette limite.

## Examens

- Version de version : réussie, zéro avertissement/erreur.
- Core Console : tous les groupes de tests réussissent, y compris les conflits de clés de fournisseur, la restauration du plugin et les erreurs d'exécution réelles dans l'historique des tests.
- Intégration d'extension : véritable travailleur UIA avec l'application de test WPF ; appel unique, ambiguïté, cibles désactivées, exclusion de mot de passe. Processus de plug-in de canal nommé réel, limites de protocole, hystérésis analogique et décodage ABI Logitech réussi.
- Véritable hôte HTTPS : certificat épinglé, accès non apparié, code faux/unique, protection de l'origine, fichiers du navigateur, stockage de hachage et clé protégée réussie. Un bug Schannel avec des clés éphémères découvert lors des tests a été corrigé.
- Suite Windows avec test de réussite au premier plan explicitement omis : réussi ; Audio/OBS/iCUE/Raw Input et état de l'écran vérifiés. Lors de l’exécution précédente, la suite complète a échoué car le basculement vers la fenêtre de test isolée a été refusé. L'exécution ultérieure du prototype d'IA comprenait également la suite complète, y compris la commutation de mise au point et un clavier iCUE connecté. Une autre exécution répétée a échoué à nouveau en raison du changement de focus ; Le test dépendant du contexte n’est donc pas vert stable. Ce test n'a pas été atténué. Les accès en lecture DDC qui échouent pendant une courte période sont répétés pendant une durée limitée ; aucune luminosité n'a été modifiée.
- WPF-Smoke : rendu de toutes les boîtes de dialogue précédentes et nouvelles, invariance de configuration, éditeur et CRUD réussis ; Captures d'écran dans le dossier de construction `smoke`.
- Navigateur : Playwright avec Edge, 390×844 et 1024×768, couplage flux/UUID/envoi unique/pas de débordement horizontal réussi. API simulée ; vrai test HTTPS séparément. Photos sur `artifacts/mobile-browser`. Le plug-in du navigateur n'était pas disponible, d'où le recours régulier à Playwright.
- Examen indépendant du code effectué ; Correction des commandes mobiles retardées, des résultats de vérification du terminal manquants, de la vérification du chemin de processus, de la restauration du plugin et du redémarrage du rythme cardiaque RVB.

Toujours ouvert : intégration exclusive du pilote, test LAN physique du fabricant/téléphone/tablette, mise à l'échelle Windows 150/200 pour cent et vérification complète de la traduction de tout nouveau texte de boîte de dialogue. Les nouveaux textes de dialogue sont majoritairement allemands ; Les catalogues de langues existants sont conservés. Par conséquent, l’exigence globale ** n’a pas été entièrement acceptée**.

## Générer un package

`./tools/publish-complete.ps1 -SelfContained` crée un nouveau dossier sous `artifacts/release-*`, comprenant le bureau, le travailleur UIA, l'hôte HTTPS, les fichiers du navigateur, un exemple de plug-in importable et les notes de licence d'exécution. L'application installée n'est pas écrasée. Sans `-SelfContained`, le package doit correspondre à .NET-10-Desktop-/ASP.NET-Runtimes.

Le package est un banc d’essai local et non un programme d’installation commercial signé. Les propres conditions d'utilisation et les droits de distribution du SDK sont des versions distinctes ; voir la documentation existante sous `docs/licensing`.
