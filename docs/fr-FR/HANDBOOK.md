<!-- generated-doc locale=fr-FR source=docs/HANDBOOK.md sha256=8c6422e18d8939164f91da28603ed597f478f1a9b8bb41e14cbe4f6630bab3fc -->

> Traduction assistée par machine. La structure, les exemples de code et les liens sont vérifiés ; cela ne constitue ni une révision par un locuteur natif ni une validation juridique. Les rapports historiques conservent leur date et leur portée d'origine. [Source originale](../HANDBOOK.md)

# MAKRIVO — Manuel d’utilisation

Fabricant : **Aivana**. Instantané de la documentation : **2026-10-12**. Ce manuel décrit l'implémentation actuelle de l'arbre de travail. Les intégrations expérimentales et les fonctions dépendantes du matériel sont identifiées explicitement. Les rapports historiques décrivent leur instantané daté, et ne constituent pas une garantie pour une version plus récente.

## Documentation et langues

Utilisez le sélecteur de langue de la documentation pour ouvrir le manuel complet, l'inventaire des fonctionnalités, le dépannage, les notes de développement, les rapports de recherche et de tests historiques dans l'une des langues d'application 27. Les traductions générées sont assistées par machine et comportent des contrôles structurels ; ils n’ont pas tous reçu d’évaluation par des locuteurs natifs. Les identifiants techniques et les exemples d'exécutables restent inchangés. La source originale reste disponible pour comparaison. L'analyse des licences traduites est informative et ne remplace pas les conditions juridiques originales de tiers ou un CLUF final.

## Installation et premier lancement

La cible initiale est Windows 11 x64. Conservez tous les fichiers et sous-répertoires de la distribution ensemble. Lancez `MAKRIVO.exe`. Une extraction de source n'est pas un programme d'installation. Les packages autonomes incluent leur runtime .NET ; Les packages dépendants du framework nécessitent les environnements d'exécution .NET 10 Desktop et ASP.NET appropriés. ONNX et les hôtes compagnons doivent être inclus pour leurs fonctionnalités respectives.

1. Démarrez MAKRIVO et choisissez la langue de l'interface dans Paramètres. Automatique suit Windows. La disposition du clavier est un paramètre distinct.
2. Choisissez un profil, puis ajoutez une vignette via le catalogue d'actions consultable. Donnez-lui un nom reconnaissable et choisissez ses actions.
3. Passez en revue les étapes et attribuez un raccourci si nécessaire. Évitez les combinaisons déjà utilisées par Windows ou votre application cible.
4. Enregistrez le brouillon. Les modifications non enregistrées ne constituent pas la configuration enregistrée.
5. Testez avec une action inoffensive, telle que l’ouverture d’un document, et inspectez le feedback d’exécution.

La fermeture de la fenêtre principale laisse normalement l'application dans la zone de notification. Double-cliquez sur son icône dans la barre d'état pour le rouvrir ou utilisez le menu de la barre d'état pour quitter. Commencer par Windows est facultatif et initialement désactivé. Le contrôle de pause suspend les liaisons ; l'icône montre un badge de pause. Annulez l'exécution des macros via l'interface utilisateur, la barre d'état ou le raccourci d'annulation configuré ; la valeur par défaut est `Ctrl+Alt+Escape`.

## Tuiles, actions et palette de commandes

Les vignettes appartiennent à des profils et peuvent contenir plusieurs étapes ordonnées. L'éditeur prend en charge la sauvegarde/rejet, l'aperçu, l'annulation/la restauration et la réorganisation des étapes. Le catalogue fournit des préréglages pour les programmes, documents, URL, texte, presse-papiers, raccourcis, médias, audio, fenêtres, actions système et intégrations prises en charge. Un préréglage nécessitant des paramètres s’ouvre sous forme de brouillon.

Ouvrez la palette avec `Ctrl+K` tandis que MAKRIVO a le focus. Recherchez des actions enregistrées, des préréglages, des changements de profil et des outils. Les touches fléchées sélectionnent ; Entrée active ; `Ctrl+Enter` modifie un élément pris en charge ; L'évasion se ferme. Certaines commandes prêtes à l'emploi s'exécutent immédiatement, tandis que les actions configurées nécessitent une révision. Un raccourci de palette globale distinct peut être configuré ; les nouvelles configurations utilisent `Ctrl+Shift+Space`.

Les étapes de texte et de presse-papiers prennent en charge `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` et `{{user}}`. Le format de la date est `yyyy-MM-dd` ; le formatage de l’heure est `HH:mm`. Les variables inconnues restent inchangées. La lecture du presse-papiers n’a lieu que sur demande. Une action du Presse-papiers écrit du texte sans le coller automatiquement.

Les actions lancées depuis l'UI peuvent cibler la dernière application externe. Si la cible disparaît ou si Windows bloque l'activation au premier plan, activez manuellement la cible et réessayez. La livraison réussie des clés ne prouve pas que la cible a accompli sa tâche.

## Profils et configuration du lieu de travail

Les profils peuvent correspondre à un nom de processus et à une partie du titre d'une fenêtre. Lorsque les deux sont définis, les deux doivent correspondre. L'ordre des profils résout les correspondances concurrentes ; le profil par défaut est la solution de secours. Activez explicitement la sélection automatique. Verrouiller le profil empêche les modifications automatiques pour la session en cours.

Ouvrez Workplace Setup à partir des Outils ou de la palette. Sélectionnez des programmes, des documents, des sites Web, une cible audio/affichage ou une scène OBS. L'assistant prépare des profils de lieu de travail, de réunion, de streaming, de recherche ou de projet. Inspectez les étapes générées avant de les enregistrer. Le programme d'installation n'exécute pas les actions préparées.

## Macros, conditions et enregistrement

Les macros exécutent leurs étapes dans l'ordre et prennent en charge les attentes, les répétitions et les annulations. La limite générale de répétition est 100. Utilisez Aperçu avant d'exécuter des actions qui saisissent dans une autre application ou modifient l'état du système.

Attendre que la fenêtre cible accepte un processus, une sous-chaîne de titre et un délai d'attente de 1 à 600,000 ms. Au moins un filtre est requis. Une fenêtre visible correspondante doit répondre et passer au premier plan avant que les étapes suivantes ne se poursuivent. Une fenêtre prête ne prouve pas qu'un document ou un service a fini de se charger.

L'éditeur de flux de travail ajoute des conditions d'existence de fichier, d'exécution de processus et de titre de fenêtre. Une condition fausse saute l’étape et peut sauter des étapes supplémentaires. Niez la condition pour exprimer une branche alternative. Sélectionnez délibérément l'arrêt en cas d'erreur ou la poursuite en cas d'erreur ; continuer peut laisser les étapes ultérieures sans leurs conditions préalables attendues.

L'enregistreur d'actions capture les accords de raccourci et les attentes, pas la saisie ordinaire. Il prend en charge les raccourcis modificateurs et les touches de fonction. Escape arrête l'enregistrement. Passez en revue la liste et adoptez-la comme brouillon de vignette non enregistré, puis enregistrez-la explicitement. L'enregistrement des clics sur l'interface utilisateur est un outil distinct décrit ci-dessous.

## Claviers, gestes et calques

La page du clavier répertorie les interfaces détectées et les dispositions de référence. Un seul clavier physique peut exposer plusieurs interfaces. Sélectionnez la langue et la géométrie correctes ; un nom de produit à lui seul n’identifie pas la mise en page ISO ou ANSI.

Les raccourcis clavier globaux standard s’appliquent à tous les claviers. Les liaisons Raw Input spécifiques à l'appareil observent un appareil et un code d'analyse particuliers, y compris les touches de fonction et multimédia prises en charge. **Raw Input ne supprime pas l’entrée Windows ordinaire. Un deuxième clavier exclusif n'est pas prêt pour la production.** Un backend de pilote de filtre signé, sous licence et testé est toujours requis ; aucun n'est installé par MAKRIVO.

Les raccourcis clavier spéciaux pris en charge peuvent utiliser une pression immédiate, une pression, un maintien ou une double pression. Les clés de couche conservées sélectionnent temporairement un autre mappage et restaurent la couche de base lors de la libération ; les touches de sélection de calque n'exécutent pas leur propre macro. Les seuils de maintien vont de 150 à 5,000 ms ; appuyez deux fois sur les fenêtres de 150 à 1,000 ms. Ces mappages sont distincts des raccourcis clavier globaux ordinaires.

L'assistant de clé spéciale détecte les clés compatibles sans exécuter les actions qui leur sont assignées lors de la capture. Les diagnostics expliquent les appareils indisponibles, les liaisons suspendues, les incompatibilités de profils et les conflits. La superposition affiche la mise en page et les liaisons sélectionnées ; son raccourci de mise en attente par défaut est `Ctrl+Alt+F1`.

| Intégration | Exigences et limites |
| --- | --- |
| Corsair | Exécution de iCUE, accès au SDK et modèle compatible. G1–G6 et fonctions RVB prises en charge ; Les commandes propriétaires Fn, molette, verrouillage et profil ne sont généralement pas exposées par l'API G-key. |
| LogitechG | Les bibliothèques locales originales du SDK x64 G-key/LED nécessitent une approbation explicite. Les événements G-key n'identifient pas de manière unique plusieurs claviers physiques identiques. |
| Razer | Chroma RVB et entrée standard ; mappez les contrôles propriétaires dans Synapse si nécessaire. |
| SteelSeries | GameSense RVB et entrée standard ; mapper les contrôles propriétaires dans GG si nécessaire. |
| Wooting | SDK x64 local d'origine compatible pour les valeurs et seuils analogiques ; l'entrée HID normale reste active. |
| Autres claviers | Touches de raccourci standard ou liaisons de scan-code physique prises en charge. Les contrôles Fn et propriétaires dépendent du modèle et de la configuration du fournisseur. |

La recherche du fabricant décrit des capacités documentées, non implémentées ou testées, avec chaque appareil répertorié. Les nouveaux adaptateurs nécessitent encore une réelle acceptation matérielle. Ne téléchargez pas les binaires du SDK à partir de sources non fiables ; Les droits de distribution du SDK sont distincts de la compatibilité technique.

## Audio, affichages et OBS

L’audio est en volume 0 à 100 pourcentage et sortie mute/unmute/toggle ou finition microphone. Choisissez un point de fin spécifique ou utilisez le Windows un cible manquant produit une erreur au lieu de contrôler silencieusement un autre appareil. Réfrigérer les appareils après la réconnexion. Contrôle de bouton n'est pas une revendication de support de mixteur indépendant par application.

Les actions d'affichage augmentent/diminuent la luminosité de 10 points de pourcentage ou définissent une valeur de 0 à 100. Les écrans internes utilisent les interfaces WMI prises en charge ; les moniteurs externes nécessitent la prise en charge de DDC/CI et généralement son paramètre de menu du moniteur. Les stations d'accueil, les adaptateurs et les câbles peuvent bloquer le contrôle. Choisissez tous les écrans pris en charge ou un moniteur. La reconnexion peut modifier les identités disponibles ; revoir la cible enregistrée. Les lectures manquantes sont affichées comme indisponibles plutôt que comme valeurs mesurées.

OBS nécessite WebSocket v5 activé dans les outils OBS. Configurez le point de terminaison, testez la connexion et chargez les noms de scène/entrée avant de sauvegarder. Les actions prises en charge incluent la commutation de scène, le démarrage/arrêt/basculement du flux/enregistrement, la pause/reprise de l'enregistrement et la sourdine d'entrée. Les mots de passe peuvent être stockés dans Windows Credential Manager et sont exclus des exportations de configuration. Une commande OBS réussie n’établit pas tous les effets de streaming en aval.

## Vérification des résultats et historique d’exécution

Activez Vérifier le résultat pour les étapes appropriées. L'application distingue les contrôles en amont, les effets confirmés, les échecs, les annulations et les effets qui ne peuvent pas être vérifiés. L'existence d'un processus n'est pas la préparation d'une application. Les modifications relatives, les raccourcis et la saisie de texte ne peuvent pas faire l'objet d'une réclamation globale de réussite. Inspectez le journal d’activité et l’historique de vérification lorsqu’une action échoue. Les erreurs de configuration doivent être corrigées avant de réexécuter la macro complète.

## Automatisation de l'interface utilisateur et enregistrement des clics

Ajoutez une étape d'automatisation d'UI, sélectionnez l'opération et capturez le cible sous l'indice après le retard affiché. Capture seule ne fonctionne rien. Vérifiez le sélectionneur et nécessite un match unique. `Invoke`, `Select`, `Toggle`, `Focus`, `SetValue` et `WaitState`. Les champs de mot de passe, les contrôles désactivés et les sélectionneurs ambigues sont rejetés.

L'automatisation de l'interface utilisateur s'exécute dans un processus compagnon limité. Cela dépend de la prise en charge et des autorisations d’accessibilité Windows de l’application cible. L'enregistreur de clics capture les actions et les pauses prises en charge ; il n'enregistre pas la saisie de texte générale. Examiner et valider son projet avant de l’adopter. Les modifications de l’interface utilisateur dans une application externe peuvent invalider les sélecteurs. L'isolation des processus limite les plantages et les annulations, mais ne constitue pas un bac à sable de sécurité.

## Plugins

Ouvrez les extensions pour inspecter un package avant de l'importer. La structure du package, le manifeste et les hachages sont vérifiés. Une importation démarre désactivée. Lisez les actions déclarées et faites explicitement confiance à l'éditeur avant de les activer. Les plugins s'exécutent avec vos autorisations utilisateur dans un processus distinct ; ce processus est **pas un bac à sable**. Installez uniquement les packages en qui vous avez confiance.

Le gestionnaire prend en charge l'exportation, la désactivation, la désinstallation et la restauration/suppression d'une version précédente. Les dépendances manquantes bloquent l'invocation. L'exemple de package fournit un compte à rebours et des actions d'état local. Le schéma pris en charge est un sous-ensemble JSON-Schema documenté, et non toutes les fonctionnalités JSON-Schema possibles. Il n’existe pas de marché public de plugins.

## Panneau mobile

1. Ouvrez Mobile Panel, sélectionnez une adresse IP locale et choisissez explicitement les profils à exposer. L'adresse de bouclage par défaut n'est accessible que sur le PC lui-même ; sélectionnez l'adresse LAN appropriée pour un téléphone ou une tablette.
2. Démarrez le panneau et lisez son adresse HTTPS, l'empreinte digitale du certificat et le code d'appairage à six chiffres à usage unique.
3. Sur un appareil du même réseau approuvé, ouvrez l'adresse. Comparez l'empreinte digitale SHA-256 via un canal approuvé avant de faire confiance au certificat local. L'application n'installe pas automatiquement un certificat de confiance.
4. Associez-le en utilisant le code affiché et testez une tuile inoffensive. Une autorisation manuelle du pare-feu Windows peut être nécessaire pour l'accès au réseau local. N'exposez pas le port à Internet.
5. Arrêtez le panneau lorsque vous avez terminé ou révoquez les clients couplés via l'application.

Les clés de serveur privées sont protégées par Windows DPAPI ; les informations d'identification du client sont stockées sous forme de hachages. Changer l'adresse IP ou renouveler le certificat peut nécessiter une confiance renouvelée. Le panneau affiche les états d'action et de vérification, mais pas tous les capteurs en direct possibles. La configuration du navigateur et les contrôles HTTPS locaux ne remplacent pas l'acceptation réelle du réseau local par téléphone/tablette.

## Assistant de réunion local IA

Cet assistant expérimental facultatif est désactivé par défaut. Ouvrez-le à partir des Outils, activez-le explicitement, sélectionnez un environnement d'exécution/un modèle et ajoutez uniquement les ressources que vous souhaitez qu'il prenne en compte. Énoncez l'objectif, créez un brouillon, inspectez les demandes non résolues et transférez le résultat à l'éditeur normal. Rien n'est exécuté automatiquement ; la sauvegarde et l'exécution restent des actions explicites. Il n’y a pas de repli automatique du temps d’exécution.

| Durée d'exécution | Configuration |
| --- | --- |
| Ollama | Service local séparé et modèle installé localement à `127.0.0.1:11434`. Les modèles distants/cloud déclarés sont rejetés. |
| LM Studio | Serveur local avec un modèle chargé localement et prise en charge du schéma JSON sur `127.0.0.1:1234`. N'activez pas le routage à distance pour le traitement local. |
| llama.cpp | `llama-server` local et modèle compatible avec prise en charge du schéma JSON sur `127.0.0.1:8080`. |
| ONNX Runtime GenAI CPU | Dossier de modèle de texte GenAI local compatible et fiable ; sélectionnez `genai_config.json`. L'inférence utilise un processus compagnon intégré sans HTTP. |

Les clients HTTP utilisent uniquement le bouclage, n'utilisent pas de proxy et ne suivent pas les redirections. MAKRIVO ne peut pas établir de manière fiable si un serveur local compatible appelle en interne un service distant. Les serveurs locaux authentifiés ne sont pas configurables dans cette version. Les poids de modèle, Python et les serveurs d'inférence locaux ne sont pas automatiquement téléchargés ou installés.

ONNX Les paquets ont besoin `tokenizer.json` et un modèle de chat valide. les formulaires de modèle soutenus dans `tokenizer_config.json` C’est un string `chat_template`Un dictionnaire avec un filet `default`, ou une liste de modèles nommés / par défaut indépendante. si le modèle manque ou null, `chat_template.jinja` Les modèles vides, ambigues, oversized ou incorrectly typed sont rejetés. ONNX Les fichiers, les modèles d'insertion/vision et les paquets GPU uniquement ne sont pas échangables avec les compatibles. GenAI Modèles de textes : le lien GenAI La version runtime est 0.17.1Utilisez uniquement des modèles de confiance; la séparation du processus n'est pas une boîte de sable.

L'assistant prend en charge des programmes sélectionnés sans arguments, des documents et des sites Web sélectionnés et coupe le microphone sur demande expresse. Il ne crée pas de messages, de rendez-vous, de scripts arbitraires, de clics gratuits, de téléchargements ou de changement de casque. Les noms d'objectif et de ressource sont envoyés au runtime local sélectionné ; les chemins de ressources et le contenu des fichiers ne sont pas fournis séparément. Un objectif saisi par l'utilisateur peut lui-même contenir des données personnelles. Le comportement réseau d'un runtime local reste sous la responsabilité de l'utilisateur.

Le budget de requête ONNX est de 90 secondes, chargement du modèle compris, jusqu'à 3,072 jetons d'invite et jetons générés 1,024, également limité par le contexte du modèle. Les JSON mal formés/tronqués et les ID de ressources inconnus sont rejetés. Annuler ou fermer pour mettre fin à une demande. Un test d'exécution sur modèle aléatoire établit l'exécution, et non la qualité de la planification utile ou les gains de temps. Les journaux d'expérimentation locaux contiennent des identifiants et des horaires d'événements, et non le texte des objectifs ou le contenu du document ; ils ne sont pas automatiquement téléchargés.

L'évaluation ultérieure de l'instructeur a produit des plans valables zéro dans quatre essais allemands/anglais. l'inventaire natif a fonctionné, mais le modèle a échoué au contrat de planification strict. C'est un résultat de qualité négatif, pas un modèle recommandé ou un assistant utile. Voir le rapport de test de temps de conduite local daté pour la révision exacte du modèle et les limites.

## Importation, exportation, synchronisation et sauvegarde

Examinez les aperçus d’importation avant de remplacer une configuration. Les actions importées sont désactivées au démarrage et la synchronisation portable supprime les liaisons spécifiques au matériel. Réaffectez les appareils et activez délibérément les actions après inspection. Examinez le contenu du texte/de l'action avant de partager une exportation ; l'exclusion des mots de passe ne rend pas non sensibles tous les contenus d'action créés par l'utilisateur.

La synchronisation du profil échange les fichiers dans un dossier sélectionné, optionnellement géré par votre client cloud existant. MAKRIVO ne fournit pas d'account en nuage et n'enregistre pas d'accréditations en nuage. Les révisions empêchent les surinscriptions silencieuses. Inclure les contenus d'action est un choix explicite. Backup votre configuration avant une importation de remplacement. `%LOCALAPPDATA%\AivanaWindowsController\settings.json`• le nom historique de l’enregistrement est conservé pour la compatibilité. fermez l’application avant de la copier / le restaurer manuellement. les approbations SDK stockées et les créances protégées par l’OS peuvent être récupérées sur un autre PC.

## Dépannage

| Symptôme | Que vérifier |
| --- | --- |
| Le raccourci ne fait rien | Application en cours d'exécution, liaisons non suspendues, profil actif correct, vignette enregistrée/activée, conflits de raccourcis et cible disponible. |
| La fenêtre cible ne peut pas s'activer | Activez-le manuellement. Windows Les restrictions de premier plan et les différences de privilèges peuvent bloquer l'automatisation. |
| La clé spéciale n'est pas détectée | Service fournisseur/SDK/prise en charge du modèle ; configurez les contrôles propriétaires pour émettre une clé standard prise en charge si possible. |
| Le deuxième clavier tape également normalement | Attendu avec Raw Input. La suppression exclusive n’est pas implémentée pour la production. |
| Audio ou luminosité indisponible | Actualiser les appareils ; inspectez l’identité sélectionnée, les sessions de point de terminaison, la prise en charge WMI/DDC/CI et les restrictions de station d’accueil/câble. |
| OBS hors ligne | WebSocket v5 activé, point de terminaison/mot de passe correct, service accessible et noms de scène/entrée valides. |
| L'automatisation de l'interface utilisateur échoue | Hôte compagnon inclus, sélecteur valide unique, cible activée accessible, contrôle et autorisations pris en charge. Réessayez un délai d'attente transitoire uniquement après avoir examiné la cible. |
| Panneau mobile inaccessible | Adresse LAN plutôt que de bouclage, même réseau, serveur en cours d'exécution, profils approuvés, empreinte digitale/confiance du certificat et autorisations manuelles du pare-feu. |
| Le plugin échoue | Intégrité du package, état de confiance/activé explicite, dépendances, compatibilité des protocoles et journal d’exécution. |
| La requête IA échoue | Corrigez le runtime local sélectionné, le service/modèle prêt, le schéma JSON pris en charge, le dossier de modèle compatible de confiance et les limites de requêtes. |
| La mise en page ou le texte semble incorrect | Séparez le langage de l'interface utilisateur de la géométrie du clavier ; redimensionnez la fenêtre, vérifiez l'échelle d'affichage et signalez la page/langue/version exacte. |

Pour un rapport de bogue utile, incluez la version Windows, la construction/la validation de l'application, la langue, la mise à l'échelle de l'affichage, le modèle matériel, une séquence minimale et le journal rédigé correspondant. Supprimez les chemins personnels, le contenu des actions et les informations d’identification avant la publication.

## Construction, tests et emballage

Utilisez le SDK épinglé dans `global.json`. Depuis la racine du référentiel :

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test
./tools/publish-complete.ps1 -SelfContained
python tools/check_documentation.py
```

Les tests d'intégration Windows peuvent ouvrir des fenêtres isolées et configurer/libérer temporairement les liaisons de fournisseur prises en charge. Exécutez-les sans qu’un autre contrôleur ne possède ces liaisons. Les sondes de luminosité/audio par défaut sont en lecture seule ; les tests aller-retour matériels explicites sont distincts. Les protocoles simulés, les captures d'écran rendues et les graphiques d'inférence aléatoires ne doivent pas être représentés comme une véritable acceptation de la qualité du matériel/du modèle.

L'éditeur complet rassemble des hôtes compagnons, des ressources mobiles, l'exemple de plugin, le runtime ONNX et les avis de dépendance disponibles. Conservez ses sous-répertoires. Une version réussie ne constitue pas un programme d'installation signé ni une approbation de version légale. La source et les builds peuvent évoluer séparément ; comparez l'instantané de la documentation et validez avec la version installée.

## Licence, confidentialité et statut de publication

Le modèle commercial prévu est un achat unique avec utilisation perpétuelle de la version majeure achetée ; le prix final, la quantité d'appareil et les conditions de mise à niveau doivent être définis dans les conditions de vente réelles. Un système d’achat/activation de production n’est pas établi par ce manuel. Aucun compte cloud n'est requis pour le contrôle quotidien.

Ne distribuez pas de binaires SDK propriétaires sans autorisation prouvée. Conservez la licence et les avis de tiers pour chaque dépendance livrée, y compris les packages .NET et ONNX. Les poids des modèles ont leurs propres termes. L’audit de licence daté est antérieur à certaines nouvelles dépendances et ne constitue pas une autorisation pour ces ajouts. Le fait que le référentiel soit public n'accorde pas lui-même une licence générale open source.

Le matériel d'un véritable fournisseur, le fonctionnement LAN du téléphone/tablette, les transitions mixtes moniteur/DPI, l'utilisation de longue durée, la révision linguistique complète, la prise en charge exclusive des pilotes et la qualité utile de la planification de l'IA restent un travail d'acceptation distinct. Ne déduisez jamais l’approbation de la seule présence de fonctionnalités.
