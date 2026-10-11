# Aivana Windows Controller

[English (US)](README.md) | [Deutsch](README.de-DE.md) | **Français** | [Italiano](README.it-IT.md)

Un contrôleur local de raccourcis Windows avec des tuiles de type Stream Deck, des profils, des macros et la détection des claviers. L’interface prend en charge 27 langues. L’usage quotidien ne nécessite ni compte ni connexion au cloud.

[Documentation](docs/fr-FR/README.md) · [Vérification](docs/fr-FR/VERIFICATION.md) · [Rapport de test fonctionnel](docs/fr-FR/testing/2026-10-09-usp-release.md)

## Démarrage

Après publication, l’application se trouve dans `artifacts/win-x64/Aivana.Controller.exe`. Gardez tout le dossier de sortie ensemble ; vous pouvez le copier ailleurs. Windows 11 x64 est le système cible initial. Les artefacts de build sont exclus de Git ; un clone des sources ne comprend pas d’exécutable précompilé.

Installez le SDK .NET 10 spécifié dans `global.json` pour compiler et lancer l’application depuis les sources :

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project src/Aivana.Desktop -c Release
```

Publiez une version autonome qui ne nécessite pas l’installation séparée du runtime .NET :

```powershell
dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64
```

## Utilisation de base

1. Sélectionnez un profil et créez une tuile d’action.
2. Définissez son nom, son icône, sa couleur et son raccourci clavier. Les combinaisons avec Ctrl/Alt/Maj réduisent les déclenchements accidentels pendant la saisie.
3. Ajoutez une ou plusieurs étapes : applications, fichiers, sites Web, texte, raccourcis, médias, volume, changement de profil ou délais.
4. Enregistrez. Les modifications restent à l’état de brouillon jusque-là ; abandonner rétablit la version enregistrée. Le bouton Lecture de la tuile et son raccourci utilisent le même chemin d’exécution.
5. Réorganisez les tuiles et les étapes de macro par glisser-déposer ou avec les flèches. Les modèles simplifient la configuration. Une macro peut se répéter jusqu’à 100 fois. Annulez via l’interface, le menu de la zone de notification ou Ctrl+Alt+Échap par défaut.

Les profils propres à une application utilisent le nom du processus sans son chemin complet. En cas de correspondance multiple, l’ordre des profils détermine la priorité ; le profil par défaut sert de solution de repli. La sélection automatique des profils doit être activée dans les paramètres.

Par défaut, fermer la fenêtre principale laisse l’application dans la zone de notification. Double-cliquez sur son icône pour la rouvrir ; utilisez le menu de cette zone pour quitter. Le démarrage avec Windows est facultatif et désactivé par défaut. Les raccourcis peuvent être mis en pause de façon centralisée.

## Actions et palette de commandes

« Ajouter une action » ouvre un catalogue consultable de 90 préréglages répartis en huit catégories. Chacun comporte une description et un aperçu. L’éditeur de macros utilise le même catalogue pour ajouter ou remplacer des étapes. Dans ce parcours de l’éditeur, sélectionner un préréglage crée un brouillon sans l’exécuter.

La palette de commandes s’ouvre depuis l’en-tête ou avec Ctrl+K dans l’application. Elle recherche les actions enregistrées dans tous les profils, les préréglages du catalogue, le changement de profil, la navigation, la pause/reprise, l’annulation des macros et l’import/export. Filtrez les actions enregistrées, les fonctions du catalogue ou les commandes de l’application. Les flèches sélectionnent, Entrée active, Ctrl+Entrée modifie une entrée modifiable et Échap ferme la palette. Les favoris apparaissent en premier. Les commandes du catalogue prêtes à l’emploi peuvent être exécutées immédiatement ; les préréglages à configurer ouvrent un brouillon. Les paramètres permettent de définir un raccourci global pour la palette. Les nouvelles configurations utilisent Ctrl+Maj+Espace ; aucun nouveau raccourci global n’est attribué automatiquement aux configurations existantes.

Les actions audio règlent précisément le volume de 0 à 100 % et coupent, rétablissent ou basculent le son des périphériques de sortie et du microphone. Choisissez un point de terminaison précis ou laissez le champ vide pour utiliser le périphérique Windows par défaut. Si un point de terminaison enregistré n’est plus disponible, une erreur s’affiche au lieu de commander un autre périphérique. « Actualiser les périphériques » recharge les périphériques connectés.

OBS Studio utilise WebSocket v5 : démarrer/arrêter/basculer/mettre en pause/reprendre l’enregistrement ; démarrer/arrêter/basculer le streaming ; changer de scène ; basculer la sourdine d’une entrée. Activez le serveur WebSocket dans le menu Outils d’OBS et renseignez son point de terminaison dans l’éditeur. « Vérifier la connexion et charger les noms » lit la version d’OBS, les scènes et les entrées sans démarrer d’enregistrement ni de diffusion. Choisissez un nom dans la liste ou saisissez-le manuellement. Les mots de passe peuvent être enregistrés dans le Gestionnaire d’identifiants Windows et ne sont pas inclus dans les configurations exportées. Une connexion OBS indisponible signale une erreur.

Les profils peuvent correspondre à un nom de processus et à une partie du titre de fenêtre. Toutes les conditions précisées doivent correspondre. « Verrouiller le profil » empêche le changement automatique pendant la session. « Reprendre les conditions de la fenêtre active » utilise la dernière application externe qui a reçu le focus.

Les actions de texte et du presse-papiers prennent en charge `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` et `{{user}}`. Le format des dates est `yyyy-MM-dd`, celui de l’heure `HH:mm`. Les espaces réservés inconnus restent inchangés. `{{profile}}` identifie le profil de l’action lancée ; `{{process}}` identifie le contexte de l’application externe. Le contenu du presse-papiers n’est lu que si cet espace réservé est utilisé. L’action presse-papiers écrit du texte, sans le coller automatiquement.

Les actions de fenêtre minimisent, agrandissent, restaurent ou placent une fenêtre à gauche/droite de la zone de travail de son moniteur. L’action système verrouille Windows. Elles ne s’exécutent que lorsqu’elles sont explicitement lancées.

L’aperçu affiche les étapes, répétitions et variables sans exécution. Annuler et Rétablir conservent jusqu’à 100 états du brouillon ; les champs de texte gardent l’édition native avec Ctrl+Z. Marquez les favoris dans l’éditeur ou le menu de la tuile, puis filtrez-les dans le tableau de bord. Le nombre d’exécutions ne concerne que la session actuelle.

Les actions de texte, de raccourci et de fenêtre lancées depuis l’interface activent la dernière fenêtre cible externe. La palette affiche le contexte de cette cible. Si la fenêtre cible est fermée ou si Windows refuse son activation, l’exécution s’arrête avec un message.

## Assistance clavier et retours

L’assistant des touches spéciales est accessible depuis la page Clavier et la palette. Il répertorie les touches G signalées par iCUE. « Détecter l’appui » les capture temporairement sans exécuter leurs actions attribuées. Les affectations normales sont restaurées après la détection, l’annulation ou la fermeture. Une fonction du catalogue et la touche choisie ouvrent un brouillon non enregistré ; les affectations en double sont détectées.

La superposition du clavier montre la disposition de référence sélectionnée et les affectations actives. Maintenez Ctrl+Alt+F1 ou activez/désactivez-la depuis la palette ou la page Clavier. Elle ne prend pas le focus et laisse passer les clics de souris. Les changements de profil et d’affectation sont répercutés pendant l’affichage ; les appuis ordinaires sur les touches Windows sont mis en évidence. Les commandes propres à un fabricant ne produisent pas toujours un signal de touche Windows équivalent.

Le retour d’exécution indique l’attente, la progression des étapes/répétitions, la fin, l’annulation ou l’échec, sans prendre le focus ni exposer le texte saisi. « Commandes exécutées » confirme l’exécution des commandes ; il ne vérifie pas séparément l’état en direct d’une application tierce. Les détails des erreurs figurent dans le journal d’activité.

## Périphériques et Corsair

Les touches Corsair G1–G6 peuvent être affectées directement via le SDK iCUE officiel. Choisissez le périphérique et la touche dans l’éditeur d’action et enregistrez, ou préparez un brouillon depuis la vue clavier K100. Les affectations s’appliquent au périphérique choisi et au profil actif ; la pause les désactive également. iCUE doit fonctionner et autoriser l’accès. L’API des touches G ne permet pas d’affecter Fn, la molette iCUE, les commandes de profil ou de verrouillage.

La page Périphériques répertorie les interfaces clavier Windows et s’actualise périodiquement ainsi qu’à la connexion d’un appareil. Un clavier physique peut exposer plusieurs interfaces HID. Les raccourcis globaux standard fonctionnent avec toutes les marques.

La détection Corsair s’appuie sur les informations disponibles du périphérique. Le contrôle RGB nécessite du matériel compatible, iCUE en cours d’exécution et le SDK iCUE officiel ; l’application indique la disponibilité réelle. Les autres fabricants bénéficient actuellement de la détection et des actions standard. En dehors de l’intégration des touches G Corsair, les commandes propriétaires fonctionnent lorsque Windows les reçoit comme entrées ordinaires. L’application n’installe pas de pilote clavier.

## Langues

Les paramètres proposent 27 langues : allemand, anglais, français, espagnol, italien, portugais, néerlandais, polonais, tchèque, slovaque, hongrois, roumain, bulgare, grec, danois, suédois, norvégien, finnois, estonien, letton, lituanien, slovène, croate, ukrainien, russe, turc et serbe en alphabet latin. Le mode Automatique suit la langue d’affichage de Windows. Les traductions sont intégrées localement. Les dispositions, raccourcis et noms définis par l’utilisateur sont indépendants de la langue de l’interface.

La documentation du dépôt est disponible en anglais américain (`en-US`), allemand (`de-DE`), français (`fr-FR`) et italien (`it-IT`). L’application accepte également ces quatre codes régionaux et utilise leurs formats numériques. Les codes de langue existants restent compatibles.

## Dispositions et recherche fabricants

La vue clavier propose des références ISO-DE et ANSI-US en format complet et TKL, ainsi que des dispositions Corsair K100 RGB. La géométrie représente les positions, les touches larges/hautes et la touche Entrée ISO. Les dispositions standard sont des références génériques. Les dispositions K100 suivent les commandes documentées sans prétendre à une précision millimétrique. Choisissez la bonne variante de langue/géométrie : un nom de modèle ou un VID/PID USB ne suffit pas à identifier une variante précise.

Lors d’une affectation, les scancodes physiques sont résolus selon la disposition d’entrée Windows active. Le parcours prépare un brouillon et conserve les contrôles d’enregistrement et de conflit. Les affectations standard sont des raccourcis Windows globaux, sans isolation à un clavier connecté donné. Les touches Corsair G1–G6 utilisent l’intégration iCUE propre au périphérique. Les autres commandes propriétaires nécessitent un adaptateur ou des raccourcis configurés avec le logiciel du fabricant. Entrée principale et Entrée du pavé numérique partagent la même identité de touche virtuelle pour les raccourcis globaux.

La [recherche fabricants](docs/fr-FR/research/tastaturhersteller-sonderfunktionen.md) et le [fichier CSV](docs/fr-FR/research/tastaturhersteller-sonderfunktionen.csv) couvrent 74 groupes de marques/fabricants et distinguent les configurateurs, SDK d’entrée et API d’éclairage. Les [preuves de disposition](docs/fr-FR/research/keyboard-layout-evidence.md) consignent les différences de modèles, SKU et modules. La couverture de recherche ne signifie pas que la prise en charge matérielle est implémentée.

## Données

La configuration est stockée dans les données locales de l’application. L’import/export utilise JSON. L’import affiche un aperçu de la configuration, remplace la configuration actuelle après confirmation et désactive les affectations importées jusqu’à leur activation manuelle. Le contenu des actions de texte n’est pas recueilli comme journal de frappe. Les mots de passe du Gestionnaire d’identifiants sont séparés des profils exportés.

## Vérification

```powershell
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
```

Les contrôles Windows peuvent inspecter le matériel et configurer temporairement puis libérer les affectations iCUE prises en charge. Exécutez-les en l’absence d’un autre contrôleur qui utilise ces affectations. Les contrôles des points de terminaison audio ne modifient pas le volume ni la sourdine en cours.

Les entrées natives ne peuvent pas contourner les privilèges Windows. Un contrôleur lancé normalement peut être incapable d’envoyer des entrées à une application exécutée en tant qu’administrateur. Les raccourcis réservés ou déjà utilisés apparaissent comme des conflits.

L’[index de documentation](docs/fr-FR/README.md) relie les conceptions historiques, plans, recherches et rapports de tests datés. Les plans historiques décrivent le périmètre consigné à l’époque et ne garantissent pas la prise en charge actuelle des fonctions.

### Modèles de workflows et attente d’une fenêtre cible

Le catalogue propose six workflows modifiables : espace de travail, réunion, streaming, recherche, changement de projet et texte prédéfini. Recherchez `workflow.` dans le catalogue ou la palette de commandes. La sélection ouvre un brouillon non enregistré sans rien exécuter. Tous les pas sont conservés lors de l’ajout à une macro ou de l’affectation à une touche Corsair. Adaptez les URL, chemins, profils et scènes OBS avant l’enregistrement.

**Attendre la fenêtre cible** accepte un processus avec ou sans `.exe`, une partie du titre sans distinction de casse et un délai de 1 à 600 000 ms. Au moins un filtre est requis ; si les deux sont fournis, les deux doivent correspondre. La macro continue lorsque la fenêtre correspondante est visible, répond et passe au premier plan. Précisez le titre si plusieurs fenêtres partagent le même processus. Une annulation ou un dépassement du délai arrête les étapes suivantes. Une fenêtre prête ne garantit ni le chargement du document ni la connexion à un service externe tel qu’OBS WebSocket.

Luminosité : rechercher **Augmenter la luminosité**, **Diminuer la luminosité** ou **Luminosité de l’écran à 50 pour cent** dans le catalogue. Les actions relatives ajustent la luminosité de 10 points de pourcentage ; les valeurs fixes vont de 0 à 100. Les écrans internes compatibles utilisent WMI ; les moniteurs externes compatibles nécessitent DDC/CI activé dans leur menu. Tous les écrans compatibles sont concernés. Les connexions et stations d’accueil peuvent limiter la prise en charge.
