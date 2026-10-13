<!-- generated-doc locale=fr-FR source=docs/HANDBOOK.md sha256=8c6422e18d8939164f91da28603ed597f478f1a9b8bb41e14cbe4f6630bab3fc -->

> Traduction assistée par machine. La structure, les exemples de code et les liens sont vérifiés ; cela ne constitue ni une révision par un locuteur natif ni une validation juridique. Les rapports historiques conservent leur date et leur portée d'origine. [Source originale](../HANDBOOK.md)

# MAKRIVO — Manuel d’utilisation

Fabricant : **Aivana**. Instantané de la documentation : **2026-10-12**. Ce manuel décrit l'implémentation actuelle de l'arbre de travail. Les intégrations expérimentales et les fonctions dépendantes du matériel sont identifiées explicitement. Les rapports historiques décrivent leur instantané daté, et ne constituent pas une garantie pour une version plus récente.

## Documentation et langues

Utilisez le sélecteur de langue de la documentation pour ouvrir le manuel complet, l'inventaire des fonctionnalités, le dépannage, les notes de développement, la recherche et les rapports de tests historiques dans l'une des 27 langues de l'application. Les traductions générées sont assistées par machine et soumises à des contrôles structurels ; elles n'ont pas toutes été révisées par des locuteurs natifs. Les identifiants techniques et les exemples exécutables restent inchangés. La source originale reste disponible pour comparaison. L'analyse des licences traduite est informative et ne remplace ni les conditions juridiques originales des tiers ni un CLUF final.

## Installation et premier lancement

La cible initiale est Windows 11 x64. Conservez ensemble tous les fichiers et sous-dossiers de la distribution. Lancez `MAKRIVO.exe`. Une copie du dépôt source n'est pas un programme d'installation. Les packages autonomes incluent leur environnement .NET ; les packages dépendants du framework nécessitent les environnements .NET 10 Desktop et ASP.NET appropriés. ONNX et les hôtes compagnons doivent être inclus pour leurs fonctionnalités respectives.

1. Démarrez MAKRIVO et choisissez la langue de l'interface dans Paramètres. Automatique suit Windows. La disposition du clavier est un paramètre distinct.
2. Choisissez un profil, puis ajoutez une tuile via le catalogue d'actions consultable. Donnez-lui un nom reconnaissable et choisissez ses actions.
3. Passez en revue les étapes et attribuez un raccourci si nécessaire. Évitez les combinaisons déjà utilisées par Windows ou votre application cible.
4. Enregistrez le brouillon. Les modifications non enregistrées ne constituent pas la configuration enregistrée.
5. Testez avec une action inoffensive, telle que l’ouverture d’un document, et inspectez le feedback d’exécution.

Fermer la fenêtre principale laisse normalement l'application dans la zone de notification. Double-cliquez sur son icône dans cette zone pour la rouvrir, ou utilisez le menu de l'icône pour quitter. Le démarrage avec Windows est facultatif et initialement désactivé. La commande de pause suspend les affectations ; l'icône affiche un indicateur de pause. Annulez les macros en cours depuis l'interface, la zone de notification ou le raccourci d'annulation configuré ; par défaut : `Ctrl+Alt+Escape`.

## Tuiles, actions et palette de commandes

Les tuiles appartiennent à des profils et peuvent contenir plusieurs étapes ordonnées. L'éditeur permet d'enregistrer ou d'abandonner les modifications, de prévisualiser, d'annuler/rétablir et de réordonner les étapes. Le catalogue propose des préréglages pour les programmes, documents, URL, texte, presse-papiers, raccourcis, médias, audio, fenêtres, actions système et intégrations prises en charge. Un préréglage nécessitant des paramètres s'ouvre comme brouillon.

Ouvrez la palette avec `Ctrl+K` lorsque MAKRIVO a le focus. Recherchez les actions enregistrées, les préréglages, le changement de profil et les outils. Les touches fléchées sélectionnent ; Entrée active ; `Ctrl+Enter` modifie un élément pris en charge ; Échap ferme la palette. Certaines commandes prêtes à l'emploi s'exécutent immédiatement, tandis que les actions configurées nécessitent une vérification. Un raccourci global distinct peut être configuré pour la palette ; les nouvelles configurations utilisent `Ctrl+Shift+Space`.

Les étapes de texte et de presse-papiers prennent en charge `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` et `{{user}}`. Le format de la date est `yyyy-MM-dd` ; le formatage de l’heure est `HH:mm`. Les variables inconnues restent inchangées. La lecture du presse-papiers n’a lieu que sur demande. Une action du Presse-papiers écrit du texte sans le coller automatiquement.

Les actions lancées depuis l'interface peuvent cibler la dernière application externe. Si la cible disparaît ou si Windows bloque son activation au premier plan, activez-la manuellement puis réessayez. La transmission réussie des frappes ne prouve pas que l'application cible a terminé sa tâche.

## Profils et configuration du lieu de travail

Les profils peuvent correspondre à un nom de processus et à une partie du titre d'une fenêtre. Si les deux critères sont définis, les deux doivent correspondre. L'ordre des profils départage les correspondances concurrentes ; le profil par défaut sert de solution de repli. Activez explicitement la sélection automatique. Verrouiller le profil empêche les changements automatiques pour la session en cours.

Ouvrez la configuration de l'espace de travail depuis Outils ou la palette. Sélectionnez des programmes, des documents, des sites Web, une cible audio/d'affichage ou une scène OBS. L'assistant prépare des profils de travail, de réunion, de streaming, de recherche ou de projet. Inspectez les étapes générées avant d'enregistrer. La configuration n'exécute pas les actions préparées.

## Macros, conditions et enregistrement

Les macros exécutent leurs étapes dans l'ordre et prennent en charge les attentes, les répétitions et les annulations. La limite générale de répétition est 100. Utilisez Aperçu avant d'exécuter des actions qui saisissent dans une autre application ou modifient l'état du système.

L'action Attendre la fenêtre cible accepte un processus, une sous-chaîne du titre et un délai d'attente de 1 à 600000 ms. Au moins un filtre est requis. Une fenêtre visible correspondante doit répondre et passer au premier plan avant la poursuite des étapes suivantes. Une fenêtre prête ne prouve pas qu'un document ou un service a terminé son chargement.

L'éditeur de workflow ajoute des conditions de présence de fichier, de processus en cours et de titre de fenêtre. Une condition fausse saute l'étape et peut également sauter des étapes supplémentaires vers l'avant. Inversez la condition pour exprimer une branche alternative. Choisissez délibérément l'arrêt ou la poursuite en cas d'erreur ; poursuivre peut laisser les étapes suivantes sans les conditions préalables attendues.

L'enregistreur d'actions capture les combinaisons de raccourcis et les attentes, pas la saisie de texte ordinaire. Il prend en charge les raccourcis avec touches de modification et les touches de fonction. Échap arrête l'enregistrement. Examinez la liste et adoptez-la comme brouillon de tuile non enregistré, puis enregistrez explicitement. L'enregistrement des clics dans l'interface utilisateur est un outil distinct décrit ci-dessous.

## Claviers, gestes et couches

La page du clavier répertorie les interfaces détectées et les dispositions de référence. Un seul clavier physique peut exposer plusieurs interfaces. Sélectionnez la langue et la géométrie correctes ; un nom de produit à lui seul n’identifie pas la mise en page ISO ou ANSI.

Les raccourcis globaux standard s'appliquent à tous les claviers. Les affectations Raw Input propres à un périphérique observent un périphérique et un code de balayage précis, y compris les touches de fonction et multimédia prises en charge. **Raw Input ne bloque pas la saisie Windows ordinaire. L'utilisation exclusive d'un second clavier n'est pas prête pour la production.** Un backend de pilote de filtre signé, sous licence et testé reste nécessaire ; MAKRIVO n'en installe aucun.

Les affectations de touches spéciales prises en charge peuvent utiliser un appui immédiat, un appui bref, un appui maintenu ou un double appui. Les touches de couche maintenues sélectionnent temporairement une autre affectation et rétablissent la couche de base au relâchement ; les touches de sélection de couche n'exécutent pas leur propre macro. Les seuils de maintien vont de 150 à 5000 ms ; les délais du double appui vont de 150 à 1000 ms. Ces affectations sont distinctes des raccourcis globaux ordinaires.

L'assistant de touches spéciales détecte les touches compatibles sans exécuter leurs actions affectées pendant la capture. Le diagnostic explique les périphériques indisponibles, les affectations en pause, les profils non correspondants et les conflits. La superposition affiche la disposition et les affectations sélectionnées ; son raccourci par défaut à maintenir enfoncé est `Ctrl+Alt+F1`.

| Intégration | Exigences et limites |
| --- | --- |
| Corsair | Exécution de iCUE, accès au SDK et modèle compatible. G1–G6 et fonctions RVB prises en charge ; Les commandes propriétaires Fn, molette, verrouillage et profil ne sont généralement pas exposées par l'API G-key. |
| LogitechG | Les bibliothèques locales originales du SDK x64 G-key/LED nécessitent une approbation explicite. Les événements G-key n'identifient pas de manière unique plusieurs claviers physiques identiques. |
| Razer | Chroma RVB et entrée standard ; mappez les contrôles propriétaires dans Synapse si nécessaire. |
| SteelSeries | GameSense RVB et entrée standard ; mapper les contrôles propriétaires dans GG si nécessaire. |
| Wooting | SDK x64 local d'origine compatible pour les valeurs et seuils analogiques ; l'entrée HID normale reste active. |
| Autres claviers | Touches de raccourci standard ou liaisons de scan-code physique prises en charge. Les contrôles Fn et propriétaires dépendent du modèle et de la configuration du fournisseur. |

La recherche sur les fabricants décrit des capacités documentées ; elle ne prouve pas une compatibilité implémentée ou testée avec chaque appareil répertorié. Les nouveaux adaptateurs nécessitent encore une validation sur du matériel réel. Ne téléchargez pas les binaires SDK depuis des sources non fiables ; les droits de distribution des SDK sont distincts de la compatibilité technique.

## Audio, affichages et OBS

Les étapes audio règlent le volume de 0 à 100 pour cent et activent, désactivent ou basculent la sourdine des sorties audio ou microphones pris en charge. Choisissez un périphérique précis ou celui défini par défaut dans Windows. Si la cible enregistrée est absente, une erreur est signalée au lieu de contrôler un autre périphérique sans avertissement. Actualisez les périphériques après une reconnexion. Le contrôle des périphériques audio ne signifie pas qu'un mélangeur indépendant par application est pris en charge.

Les actions d'affichage augmentent/diminuent la luminosité de 10 points de pourcentage ou définissent une valeur de 0 à 100. Les écrans internes utilisent les interfaces WMI prises en charge ; les moniteurs externes nécessitent la prise en charge de DDC/CI et généralement son paramètre de menu du moniteur. Les stations d'accueil, les adaptateurs et les câbles peuvent bloquer le contrôle. Choisissez tous les écrans pris en charge ou un moniteur. La reconnexion peut modifier les identités disponibles ; revoir la cible enregistrée. Les lectures manquantes sont affichées comme indisponibles plutôt que comme valeurs mesurées.

OBS nécessite WebSocket v5 activé dans les outils OBS. Configurez le point de terminaison, testez la connexion et chargez les noms de scène/entrée avant de sauvegarder. Les actions prises en charge incluent la commutation de scène, le démarrage/arrêt/basculement du flux/enregistrement, la pause/reprise de l'enregistrement et la sourdine d'entrée. Les mots de passe peuvent être stockés dans Windows Credential Manager et sont exclus des exportations de configuration. Une commande OBS réussie n’établit pas tous les effets de streaming en aval.

## Vérification des résultats et historique d’exécution

Activez Vérifier le résultat pour les étapes appropriées. L'application distingue les vérifications préalables, les effets confirmés, les échecs, les annulations et les effets invérifiables. L'existence d'un processus ne signifie pas que l'application est prête. Les modifications relatives, les raccourcis et la saisie de texte ne peuvent pas être déclarés globalement réussis. Inspectez le journal d'activité et l'historique de vérification en cas d'échec. Corrigez les erreurs de configuration avant de relancer la macro complète.

## Automatisation de l'interface utilisateur et enregistrement des clics

Ajoutez une étape d'automatisation de l'interface utilisateur, sélectionnez l'opération et capturez la cible sous le pointeur après le délai affiché. La capture seule n'exécute rien. Vérifiez le sélecteur et exigez une correspondance unique. Les opérations prises en charge comprennent `Invoke`, `Select`, `Toggle`, `Focus`, `SetValue` et `WaitState`. Les champs de mot de passe, les contrôles désactivés et les sélecteurs ambigus sont rejetés.

L'automatisation de l'interface s'exécute dans un processus compagnon aux limites définies. Elle dépend de la prise en charge de l'accessibilité Windows et des autorisations de l'application cible. L'enregistreur de clics capture les actions prises en charge et les pauses ; il n'enregistre pas la saisie de texte générale. Examinez et validez son brouillon avant de l'adopter. Les changements d'interface d'une application externe peuvent invalider les sélecteurs. L'isolation des processus limite les conséquences des plantages et délimite l'annulation, mais ne constitue pas un bac à sable de sécurité.

## Plugins

Ouvrez Extensions pour inspecter un package avant de l'importer. La structure du package, le manifeste et les hachages sont vérifiés. Un package importé est initialement désactivé. Lisez les actions déclarées et accordez explicitement votre confiance à l'éditeur avant de l'activer. Les plugins s'exécutent avec vos autorisations utilisateur dans un processus distinct ; ce processus **n'est pas un bac à sable**. Installez uniquement des packages auxquels vous faites confiance.

Le gestionnaire prend en charge l'exportation, la désactivation, la désinstallation et la restauration/suppression d'une version précédente. Les dépendances manquantes bloquent l'invocation. L'exemple de package fournit un compte à rebours et des actions d'état local. Le schéma pris en charge est un sous-ensemble JSON-Schema documenté, et non toutes les fonctionnalités JSON-Schema possibles. Il n’existe pas de marché public de plugins.

## Panneau mobile

1. Ouvrez Mobile Panel, sélectionnez une adresse IP locale et choisissez explicitement les profils à exposer. L'adresse de bouclage par défaut n'est accessible que sur le PC lui-même ; sélectionnez l'adresse LAN appropriée pour un téléphone ou une tablette.
2. Démarrez le panneau et lisez son adresse HTTPS, l'empreinte digitale du certificat et le code d'appairage à six chiffres à usage unique.
3. Sur un appareil du même réseau approuvé, ouvrez l'adresse. Comparez l'empreinte digitale SHA-256 via un canal approuvé avant de faire confiance au certificat local. L'application n'installe pas automatiquement un certificat de confiance.
4. Associez-le en utilisant le code affiché et testez une tuile inoffensive. Une autorisation manuelle du pare-feu Windows peut être nécessaire pour l'accès au réseau local. N'exposez pas le port à Internet.
5. Arrêtez le panneau lorsque vous avez terminé ou révoquez les clients couplés via l'application.

Les clés privées du serveur sont protégées par Windows DPAPI ; les identifiants des clients sont stockés sous forme de hachages. Un changement d'adresse IP ou un renouvellement du certificat peut nécessiter de renouveler la confiance. Le panneau affiche les états des actions et de leur vérification, pas tous les capteurs en direct possibles. Les contrôles de mise en page dans le navigateur et de HTTPS local ne remplacent pas la validation réelle sur téléphone/tablette en réseau local.

## Assistant de réunion local IA

Cet assistant expérimental facultatif est désactivé par défaut. Ouvrez-le depuis Outils, activez-le explicitement, sélectionnez un environnement d'exécution/modèle et ajoutez uniquement les ressources à prendre en compte. Indiquez l'objectif, créez un brouillon, inspectez les demandes non résolues et transférez le résultat vers l'éditeur habituel. Rien ne s'exécute automatiquement ; l'enregistrement et l'exécution restent des actions explicites. Aucun basculement automatique vers un autre environnement d'exécution n'est prévu.

| Environnement d'exécution | Configuration |
| --- | --- |
| Ollama | Service local séparé et modèle installé localement à `127.0.0.1:11434`. Les modèles distants/cloud déclarés sont rejetés. |
| LM Studio | Serveur local avec un modèle chargé localement et prise en charge du schéma JSON sur `127.0.0.1:1234`. N'activez pas le routage à distance pour le traitement local. |
| llama.cpp | `llama-server` local et modèle compatible avec prise en charge du schéma JSON sur `127.0.0.1:8080`. |
| ONNX Runtime GenAI CPU | Dossier de modèle de texte GenAI local compatible et fiable ; sélectionnez `genai_config.json`. L'inférence utilise un processus compagnon intégré sans HTTP. |

Les clients HTTP utilisent uniquement le bouclage, n'utilisent pas de proxy et ne suivent pas les redirections. MAKRIVO ne peut pas établir de manière fiable si un serveur local compatible appelle en interne un service distant. Les serveurs locaux authentifiés ne sont pas configurables dans cette version. Les poids de modèle, Python et les serveurs d'inférence locaux ne sont pas automatiquement téléchargés ou installés.

Les packages ONNX nécessitent `tokenizer.json` et un modèle de conversation valide. Les formes prises en charge dans `tokenizer_config.json` sont une chaîne `chat_template`, un dictionnaire contenant une chaîne `default`, ou une liste non ambiguë de modèles nommés/par défaut. Si le modèle est absent ou nul, `chat_template.jinja` est utilisé en remplacement. Les modèles vides, ambigus, trop volumineux ou de type incorrect sont rejetés. Les fichiers ONNX quelconques, les modèles d'embedding/de vision et les packages réservés au GPU ne sont pas interchangeables avec les modèles textuels GenAI compatibles. La version de l'environnement GenAI fourni est 0.17.1, CPU. Utilisez uniquement des modèles de confiance ; la séparation des processus n'est pas un bac à sable.

L'assistant prend en charge les programmes sélectionnés sans arguments, les documents et sites Web sélectionnés, ainsi que la mise en sourdine du microphone sur demande expresse. Il ne crée pas de messages, de rendez-vous, de scripts arbitraires, de clics libres, de téléchargements ou de changements de casque. L'objectif et les noms des ressources sont envoyés à l'environnement d'exécution local sélectionné ; les chemins des ressources et le contenu des fichiers ne sont pas fournis séparément. Un objectif saisi par l'utilisateur peut lui-même contenir des données personnelles. Le comportement réseau propre à l'environnement local reste sous la responsabilité de l'utilisateur.

Le budget d'une requête ONNX est de 90 secondes, chargement du modèle compris, avec au maximum 3072 jetons d'entrée et 1024 jetons générés, également limités par le contexte du modèle. Le JSON mal formé ou tronqué et les identifiants de ressources inconnus sont rejetés. Annulez ou fermez pour terminer une requête. Un test d'exécution avec un modèle aléatoire prouve l'exécution, pas une qualité de planification utile ni un gain de temps. Les journaux des expériences locales contiennent des identifiants d'événements et des durées, pas le texte des objectifs ni le contenu des documents ; ils ne sont pas envoyés automatiquement.

L'évaluation ultérieure du petit modèle Instruct n'a produit aucun plan valide lors de quatre essais en allemand/anglais. L'inférence native a fonctionné, mais le modèle n'a pas respecté le contrat strict de planification. Il s'agit d'un résultat de qualité négatif, et non d'un modèle recommandé ou d'un assistant dont l'utilité est démontrée. Consultez le rapport daté sur les environnements d'exécution locaux pour connaître la révision exacte du modèle et les limites.

## Importation, exportation, synchronisation et sauvegarde

Examinez les aperçus d’importation avant de remplacer une configuration. Les actions importées sont désactivées au démarrage et la synchronisation portable supprime les liaisons spécifiques au matériel. Réaffectez les appareils et activez délibérément les actions après inspection. Examinez le contenu du texte/de l'action avant de partager une exportation ; l'exclusion des mots de passe ne rend pas non sensibles tous les contenus d'action créés par l'utilisateur.

La synchronisation des profils échange des fichiers dans un dossier sélectionné, éventuellement géré par votre client cloud existant. MAKRIVO ne fournit aucun compte cloud et ne stocke aucun identifiant cloud. Les révisions empêchent les écrasements silencieux. L'inclusion du contenu des actions est un choix explicite. Sauvegardez votre configuration avant un import de remplacement. Le fichier de configuration par défaut est `%LOCALAPPDATA%\AivanaWindowsController\settings.json` ; le nom historique du dossier est conservé pour assurer la compatibilité. Fermez l'application avant de le copier ou de le restaurer manuellement. Les approbations SDK enregistrées et les identifiants protégés par le système d'exploitation peuvent devoir être recréés sur un autre PC.

## Dépannage

| Symptôme | Que vérifier |
| --- | --- |
| Le raccourci ne fait rien | Application en cours d'exécution, affectations non suspendues, bon profil actif, tuile enregistrée/activée, conflits de raccourcis et cible disponible. |
| La fenêtre cible ne peut pas s'activer | Activez-le manuellement. Windows Les restrictions de premier plan et les différences de privilèges peuvent bloquer l'automatisation. |
| La touche spéciale n'est pas détectée | Service du fabricant/SDK/prise en charge du modèle ; si possible, configurez les commandes propriétaires pour qu'elles émettent une touche standard prise en charge. |
| Le deuxième clavier tape également normalement | Attendu avec Raw Input. La suppression exclusive n’est pas implémentée pour la production. |
| Audio ou luminosité indisponible | Actualiser les appareils ; inspectez l’identité sélectionnée, les sessions de point de terminaison, la prise en charge WMI/DDC/CI et les restrictions de station d’accueil/câble. |
| OBS hors ligne | WebSocket v5 activé, point de terminaison/mot de passe correct, service accessible et noms de scène/entrée valides. |
| L'automatisation de l'interface utilisateur échoue | Hôte compagnon inclus, sélecteur valide unique, cible activée accessible, contrôle et autorisations pris en charge. Réessayez un délai d'attente transitoire uniquement après avoir examiné la cible. |
| Panneau mobile inaccessible | Adresse LAN plutôt que de bouclage, même réseau, serveur en cours d'exécution, profils approuvés, empreinte digitale/confiance du certificat et autorisations manuelles du pare-feu. |
| Le plugin échoue | Intégrité du package, état de confiance/activé explicite, dépendances, compatibilité des protocoles et journal d’exécution. |
| La requête IA échoue | Corrigez le runtime local sélectionné, le service/modèle prêt, le schéma JSON pris en charge, le dossier de modèle compatible de confiance et les limites de requêtes. |
| La mise en page ou le texte semble incorrect | Séparez le langage de l'interface utilisateur de la géométrie du clavier ; redimensionnez la fenêtre, vérifiez l'échelle d'affichage et signalez la page/langue/version exacte. |

Pour un rapport de bogue utile, indiquez la version de Windows, la version compilée/le commit de l'application, la langue, la mise à l'échelle de l'affichage, le modèle du matériel, une séquence minimale et le journal pertinent expurgé des données sensibles. Supprimez les chemins personnels, le contenu des actions et les identifiants avant publication.

## Construction, tests et emballage

Utilisez la version du SDK fixée dans `global.json`. Depuis la racine du dépôt :

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test
./tools/publish-complete.ps1 -SelfContained
python tools/check_documentation.py
```

Les tests d'intégration Windows peuvent ouvrir des fenêtres isolées et configurer/libérer temporairement les liaisons de fournisseur prises en charge. Exécutez-les sans qu’un autre contrôleur ne possède ces liaisons. Les sondes de luminosité/audio par défaut sont en lecture seule ; les tests aller-retour matériels explicites sont distincts. Les protocoles simulés, les captures d'écran rendues et les graphiques d'inférence aléatoires ne doivent pas être représentés comme une véritable acceptation de la qualité du matériel/du modèle.

Le script de publication complète rassemble les hôtes compagnons, les ressources mobiles, l'exemple de plugin, l'environnement ONNX et les notices disponibles pour les dépendances. Conservez ses sous-dossiers. Une compilation réussie ne constitue ni un programme d'installation signé ni une validation juridique de la publication. Le code source et les versions compilées peuvent évoluer séparément ; comparez l'instantané de la documentation et le commit à la version installée.

## Licence, confidentialité et statut de publication

Le modèle commercial prévu est un achat unique donnant droit à l'utilisation perpétuelle de la version majeure achetée ; le prix final, le nombre d'appareils autorisés et les conditions de mise à niveau doivent être définis dans les conditions de vente effectives. Ce manuel ne démontre pas l'existence d'un système d'achat/activation opérationnel en production. Aucun compte cloud n'est nécessaire pour le contrôle quotidien.

Ne distribuez pas de binaires SDK propriétaires sans autorisation prouvée. Conservez la licence et les avis de tiers pour chaque dépendance livrée, y compris les packages .NET et ONNX. Les poids des modèles ont leurs propres termes. L’audit de licence daté est antérieur à certaines nouvelles dépendances et ne constitue pas une autorisation pour ces ajouts. Le fait que le référentiel soit public n'accorde pas lui-même une licence générale open source.

La validation sur le matériel réel des fabricants, le fonctionnement des téléphones/tablettes en réseau local, les transitions entre moniteurs aux DPI différents, l'utilisation prolongée, la révision linguistique complète, le support d'un pilote pour l'usage exclusif du clavier et une qualité de planification IA utile restent des travaux de validation distincts. Ne déduisez jamais une approbation de la seule présence d'une fonctionnalité.
