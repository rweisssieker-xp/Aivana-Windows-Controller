<!-- generated-doc locale=fr-FR source=README.md sha256=eb6f3fe7810ed3e10793e8938e908e151a84c4afbea2f22cba2656bfe4e8dfc3 -->

> Traduction assistée par machine. La structure, les exemples de code et les liens sont vérifiés ; cela ne constitue ni une révision par un locuteur natif ni une validation juridique. Les rapports historiques conservent leur date et leur portée d'origine. [Source originale](../../README.md)

# MAKRIVO

[Documentation complète dans toutes les langues 27](README.md) · [Le manuel utilisateur complet](HANDBOOK.md)

**par Aivana.** [L’inventaire et les limites de sortie](FEATURES.md) · [Rapport de test d'extension](testing/2026-10-12-six-extensions.md) · [Le prototype de l'assistant](testing/2026-10-12-local-meeting-assistant.md)

**Anglais (US)** | [Deutsch](../de-DE/REFERENCE.md) | [Français](REFERENCE.md) | [Italiano](../it-IT/REFERENCE.md)

Un contrôleur de raccourci local Windows avec vignettes, profils, macros et détection de clavier de style Stream Deck. L'interface prend en charge les langues 27. Aucun compte ou connexion cloud n'est requis pour une utilisation quotidienne.

[Documentation](README.md) · [Vérification](VERIFICATION.md) · [Le rapport de test](testing/2026-10-09-usp-release.md)

## Commencer

Après publication, l'application se trouve à `artifacts/win-x64/MAKRIVO.exe`. Conservez l'intégralité du dossier de sortie ensemble ; vous pouvez le copier ailleurs. Windows 11 x64 est la cible initiale. Les artefacts de build sont exclus de Git ; une extraction de source n'inclut pas d'exécutable prédéfini.

Installez le SDK .NET 10 spécifié dans `global.json` pour créer et exécuter à partir des sources :

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project src/Aivana.Desktop -c Release
```

Publiez une version autonome sans nécessiter un runtime .NET installé séparément :

```powershell
dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64
```

## Flux de travail de base

1. Sélectionnez un profil et créez une vignette d'action.
2. Définissez son nom, son icône, sa couleur et son raccourci clavier. Les combinaisons Ctrl/Alt/Shift réduisent l'activation accidentelle lors de la frappe.
3. Ajoutez une ou plusieurs étapes : applications, fichiers, sites Web, texte, raccourcis, médias, volume, changement de profil ou retards.
4. Enregistrez. Les modifications restent à l'état de brouillon jusqu'à ce qu'elles soient enregistrées ; ignorer restaure la version enregistrée. Le bouton Jouer de la vignette et son raccourci clavier utilisent le même chemin d'exécution.
5. Réorganisez les vignettes et les étapes macro par glisser-déposer ou par boutons fléchés. Les modèles simplifient la configuration. Les macros peuvent se répéter jusqu'à 100 fois. Annulez via l'interface, le menu de la barre d'état système ou Ctrl+Alt+Échap par défaut.

Les profils spécifiques à l'application utilisent un nom de processus sans son chemin complet. L’ordre des profils brise les égalités ; le profil par défaut est la solution de secours. La sélection automatique du profil doit être activée dans Paramètres.

La fermeture de la fenêtre principale conserve l'application dans la barre d'état système par défaut. Double-cliquez sur son icône dans la barre d'état pour le rouvrir ; utilisez le menu de la barre d'état pour quitter. Le démarrage avec Windows est facultatif et désactivé par défaut. Les raccourcis clavier peuvent être mis en pause de manière centralisée.

## Palette d'actions et de commandes

Ajouter une action ouvre un catalogue consultable avec les préréglages 90 dans huit catégories. Chacun a une description et un aperçu. L'éditeur de macros utilise le même catalogue pour ajouter ou remplacer des étapes. La sélection d'un préréglage dans ce workflow d'éditeur crée un brouillon sans exécution.

La palette de commandes s'ouvre depuis l'en-tête ou avec Ctrl+K dans l'application. Il recherche les actions enregistrées dans les profils, les préréglages du catalogue, le changement de profil, la navigation, la pause/reprise, l'annulation de macro et l'importation/exportation. Filtrez par actions enregistrées, fonctions de catalogue ou commandes d'application. Les touches fléchées sélectionnent, Entrée active, Ctrl+Entrée modifie une entrée modifiable et Échap se ferme. Les favoris apparaissent en premier. Les commandes de catalogue prêtes à l'emploi peuvent être exécutées immédiatement ; les préréglages nécessitant une configuration ouvrent un brouillon. Les paramètres peuvent définir un raccourci de palette global. Les nouvelles configurations utilisent Ctrl+Shift+Espace ; les configurations existantes ne reçoivent pas automatiquement un nouveau raccourci global.

Les actions audio règlent le volume avec précision de 0 à 100 pour cent et coupent, réactivent ou basculent les périphériques de sortie et de microphone. Choisissez un point de terminaison spécifique ou laissez-le non défini pour la valeur par défaut Windows. Un point de terminaison enregistré indisponible génère une erreur au lieu de contrôler un autre appareil. Actualiser les appareils recharge les appareils connectés.

OBS Studio utilise WebSocket v5 : démarrer/arrêter/basculer/mettre en pause/reprendre l'enregistrement ; démarrer/arrêter/basculer le streaming ; changer de scène ; basculer l'entrée en sourdine. Activez le serveur WebSocket sous Outils dans OBS et définissez son point de terminaison dans l'éditeur. Vérifier les noms de connexion et de charge lit la version, les scènes et les entrées de OBS sans démarrer un enregistrement ou un flux. Choisissez des noms dans la liste ou saisissez-les manuellement. Les mots de passe peuvent être enregistrés dans Windows Credential Manager et sont exclus des configurations exportées. Les connexions OBS non disponibles signalent une erreur.

Les profils peuvent correspondre à un nom de processus et à une partie du titre d'une fenêtre. Les deux conditions spécifiées doivent correspondre. Lock Profile empêche le changement automatique de session. Capturer les conditions du premier plan utilise la dernière application externe ciblée.

Les actions de texte et de presse-papiers prennent en charge `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` et `{{user}}`. Les dates utilisent `yyyy-MM-dd` ; utilisez parfois `HH:mm`. Les espaces réservés inconnus restent inchangés. `{{profile}}` identifie le profil de l'action lancée ; `{{process}}` identifie le contexte de l'application externe. Le contenu du Presse-papiers est lu uniquement lorsque cet espace réservé est utilisé. L'action du Presse-papiers écrit du texte mais ne le colle pas automatiquement.

Les actions de fenêtre minimisent, agrandissent, restaurent ou organisent une fenêtre sur le côté gauche/droit de la zone de travail de son moniteur. L'action du système verrouille Windows. Ces actions ne s'exécutent que lorsqu'elles sont explicitement invoquées.

L'aperçu affiche les étapes, les répétitions et les variables sans exécution. Annuler et Rétablir conservent jusqu'à 100 états de brouillon ; les zones de texte conservent l’édition native Ctrl+Z. Marquez les favoris dans l'éditeur ou dans le menu de vignettes et filtrez-les sur le tableau de bord. Le nombre d’exécutions s’applique uniquement à la session en cours.

Les actions de texte, de raccourci et de fenêtre lancées depuis l'interface activent la dernière fenêtre cible externe. La palette affiche ce contexte cible. Si la cible s'est fermée ou que Windows refuse l'activation, l'exécution s'arrête avec un message.

## Assistance et commentaires au clavier

L'assistant de touches spéciales se trouve sur la page Clavier et dans la palette. Il répertorie les touches G signalées par iCUE. Detect Key Press les capture temporairement sans exécuter les actions qui leur sont assignées. Les liaisons normales reviennent après détection, annulation ou fermeture. Une fonction de catalogue et une clé sélectionnée ouverte en tant que brouillon non enregistré ; les liaisons en double sont vérifiées.

La superposition de clavier affiche la disposition de référence sélectionnée et les liaisons actives. Maintenez Ctrl+Alt+F1 ou basculez-le depuis la palette ou la page Clavier. Il ne nécessite pas de focus et laisse passer les clics de souris. Les modifications de profil et de liaison sont mises à jour lorsqu'elles sont visibles ; Les pressions sur les touches Windows ordinaires sont mises en évidence. Les commandes spécifiques au fabricant peuvent ne pas fournir un signal de clé Windows équivalent.

Les commentaires d'exécution montrent l'attente, la progression de l'étape/répétition, l'achèvement, l'annulation ou l'échec sans se concentrer ni exposer le texte saisi. Commands Executed confirme l’exécution de la commande, et non un état actif interrogé indépendamment dans une application tierce. Les détails de l'erreur apparaissent dans le journal d'activité.

## Appareils et Corsair

Corsair G1–G6 peut être attribué directement via le SDK officiel iCUE. Sélectionnez l'appareil et la touche dans l'éditeur d'actions et enregistrez, ou préparez un brouillon à partir de la vue clavier K100. Les liaisons s'appliquent au périphérique sélectionné et au profil actif ; la pause les désactive également. iCUE doit être en cours d’exécution et autoriser l’accès. Fn, les commandes de roue, de profil et de verrouillage iCUE ne peuvent pas être mappées via cette API G-key.

La page Appareils répertorie les interfaces clavier Windows et s'actualise périodiquement et lorsque les appareils se connectent. Un clavier physique peut exposer plusieurs interfaces HID. Les raccourcis clavier globaux standard fonctionnent avec tous les fabricants.

La détection Corsair utilise les informations disponibles sur l'appareil. Le contrôle RVB nécessite un matériel compatible, exécutant iCUE et le SDK officiel iCUE ; l'application signale la disponibilité réelle. D'autres fabricants bénéficient actuellement de détections et d'actions standard. En dehors de l'intégration de la touche G de Corsair, les commandes propriétaires fonctionnent lorsque Windows les reçoit en entrée régulière. L'application n'installe pas de pilote de clavier.

## Langues

Les paramètres proposent les langues 27 : allemand, anglais, français, espagnol, italien, portugais, néerlandais, polonais, tchèque, slovaque, hongrois, roumain, bulgare, grec, danois, suédois, norvégien, finnois, estonien, letton, lituanien, slovène, croate, ukrainien, russe, turc et serbe en écriture latine. Automatique suit la langue d'affichage Windows. Les traductions sont regroupées localement. Les mises en page, les raccourcis clavier et les noms créés par l'utilisateur sont indépendants de la langue de l'interface.

La documentation complète du repository est disponible dans toutes les langues d'application 27 à travers le [Indice de langue](README.md). Les traductions sont assistées par la machine, avec des liens source originaux et des contrôles structurels; l'examen de la langue maternelle est séparé. L'application accepte également les codes régionaux `en-US`, `de-DE`, `fr-FR` et `it-IT` et utilise leurs formats numériques. Les codes linguistiques existants restent compatibles.

## Mises en page et recherche de fabricants

La vue du clavier fournit des références ISO-DE/ANSI-US en taille réelle et sans dix touches, ainsi que des dispositions RVB Corsair K100. La géométrie représente les positions, les touches larges/hautes et la saisie ISO. Les mises en page standard sont des références génériques. Les dispositions K100 suivent des contrôles documentés sans revendiquer des dimensions précises au millimètre près. Choisissez la bonne variante de langue/géométrie : un nom de modèle ou un USB VID/PID n'établit pas de variante spécifique.

Le mappage résout les codes d'analyse physique à l'aide de la disposition d'entrée active Windows. Le flux de travail prépare un brouillon et conserve les vérifications de sauvegarde/conflit. Les liaisons standard sont des raccourcis clavier globaux Windows, non isolés sur un clavier connecté. Corsair G1–G6 utilisent l'intégration iCUE spécifique à l'appareil. D'autres contrôles propriétaires nécessitent un adaptateur ou des raccourcis configurés par le fabricant. Entrée principale et Entrée du clavier numérique partagent une identité de clé virtuelle pour les raccourcis clavier globaux.

Le [Recherche de fabricant](research/tastaturhersteller-sonderfunktionen.md) et [CSV](research/tastaturhersteller-sonderfunktionen.csv) couverture 74 groupes de marque / fabricant, distinguer les configurateurs, les SDK d'entrée et les API de lumière. [Layout des preuves](research/keyboard-layout-evidence.md) Modèles, modèles et différences. La couverture de recherche ne signifie pas que le support matériel est mis en œuvre.

## Données

La configuration est stockée dans les données d'application locales. L'import/export utilise JSON. L'importation prévisualise la configuration, remplace la configuration actuelle après confirmation et désactive les liaisons importées jusqu'à ce qu'elles soient activées manuellement. Le contenu des actions textuelles n’est pas collecté sous forme de journal de frappe. Les mots de passe Credential Manager sont distincts des profils exportés.

## Vérification

```powershell
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
```

Les vérifications Windows peuvent inspecter le matériel et configurer/libérer temporairement les liaisons iCUE prises en charge. Exécutez-les sans un autre contrôleur utilisant ces liaisons. Les vérifications des points de terminaison audio ne modifient pas les paramètres de volume en direct ou de sourdine.

L'entrée native ne peut pas contourner les privilèges Windows. Un contrôleur normalement lancé ne peut pas envoyer d'entrée à une application d'administrateur. Les raccourcis clavier réservés ou occupés apparaissent comme des conflits.

Le [Indice de documentation](README.md) liens à des concepts historiques, des plans, des recherches et des rapports de test datés. Les plans historiques décrivent leur portée enregistrée, pas une garantie actuelle de support.

### Modèles de workflow et attentes de la fenêtre cible

Le catalogue comprend six flux de travail modifiables : espace de travail, réunion, streaming, recherche, changement de projet et extrait de texte. Recherchez `workflow.` dans le catalogue ou la palette de commandes. La sélection d'un flux de travail ouvre un brouillon non enregistré ; il ne fonctionne jamais immédiatement. Toutes les étapes sont conservées lors de l'ajout d'un workflow à une macro existante ou de son attribution à une touche Corsair. Configurez des exemples d'URL, de chemins d'accès aux programmes, de profils et de scènes OBS avant d'enregistrer.

L'étape **Wait for target window** accepte un nom de processus (avec ou sans `.exe`), une sous-chaîne de titre insensible à la casse et un délai d'expiration de 1–600,000 ms. Au moins un filtre est requis ; les deux filtres doivent correspondre lorsqu’ils sont fournis. La macro ne continue qu'après qu'une fenêtre visible correspondante réponde et atteigne le premier plan. Utilisez un titre spécifique lorsque plusieurs fenêtres partagent un processus. L’annulation ou l’expiration du délai arrête les étapes suivantes. La disponibilité de la fenêtre ne garantit pas qu'un document a été chargé ou qu'un service externe tel que OBS WebSocket est connecté.

Luminosité de l'écran : recherchez **Augmenter la luminosité de l'écran**, **Diminuer la luminosité de l'écran** ou **Luminosité de l'écran à 50 pour cent** dans le catalogue d'actions. Les actions relatives modifient la luminosité de 10 points de pourcentage ; les valeurs fixes vont de 0 à 100. Les écrans internes pris en charge utilisent WMI ; les moniteurs externes compatibles nécessitent que DDC/CI soit activé dans leur menu. Par défaut, l'action affecte tous les affichages pris en charge ; un moniteur explicite peut être sélectionné dans l'éditeur. Les connexions et les stations d'accueil peuvent restreindre la prise en charge.

### Contrôles étendus

Ouvrez la palette de commandes avec **Ctrl+K tandis que la fenêtre de l'application a focus**. Recherchez **Workflow designer**, **Macro d'enregistrement**, **Synchroniser les profils** ou **Key diagnostics** (le libellé suit la langue sélectionnée). Le concepteur de flux de travail modifie le brouillon de vignette sélectionné : conditions de fichier/processus/fenêtre, nombres de sauts en avant et arrêt/continuation de la gestion des erreurs. L'enregistreur capture les accords de raccourci et attend, pas la saisie ordinaire ; Escape arrête l'enregistrement. Examinez le résultat avant de l’adopter.

Dans une étape de luminosité, sélectionnez tous les écrans pris en charge ou un moniteur spécifique. Un moniteur sélectionné indisponible produit une erreur plutôt que de modifier silencieusement un autre affichage. Le tableau de bord affiche les lectures en direct de la luminosité, de l'audio et de OBS lorsque leurs intégrations respectives sont disponibles ; une lecture non disponible n'est pas présentée comme une valeur mesurée.

Dans l'éditeur de vignettes, attribuez une touche physique spéciale prise en charge et choisissez d'appuyer immédiatement, d'appuyer, de maintenir ou d'appuyer deux fois. Les sélecteurs de couche maintenus activent temporairement un autre mappage et restaurent la couche précédente lors de la libération. Les gestes et les couches s'appliquent aux touches G du SDK Corsair et aux touches spéciales standard spécifiques à l'appareil, et non aux touches de raccourci globales ordinaires. La capture par touches standard prend en charge F13–F24 et les touches multimédias du clavier. Les commandes propriétaires Fn, profil et numérotation nécessitent toujours un adaptateur de fournisseur compatible ou un mappage du fournisseur à une clé standard. Raw Input observe la clé et ne supprime pas son comportement Windows normal. Les diagnostics clés expliquent les problèmes de profil, de pause, de connexion et de liaison.

La synchronisation des profils échange des fichiers via un dossier que vous choisissez, y compris un dossier géré par votre client cloud existant. Vérifiez l'aperçu avant l'importation ; les importations démarrent désactivées et suppriment les mappages spécifiques au matériel. Les révisions empêchent les écrasements silencieux et l'inclusion du contenu des actions est un choix explicite. MAKRIVO ne fournit pas de compte cloud et ne conserve pas les informations d'identification cloud. Le plateau utilise désormais la propre icône M bleu sarcelle de MAKRIVO et affiche un badge de pause orange en cas de pause.
