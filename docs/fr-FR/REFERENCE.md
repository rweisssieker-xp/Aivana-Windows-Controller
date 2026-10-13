<!-- generated-doc locale=fr-FR source=README.md sha256=eb6f3fe7810ed3e10793e8938e908e151a84c4afbea2f22cba2656bfe4e8dfc3 -->

> Traduction assistée par machine. La structure, les exemples de code et les liens sont vérifiés ; cela ne constitue ni une révision par un locuteur natif ni une validation juridique. Les rapports historiques conservent leur date et leur portée d'origine. [Source originale](../../README.md)

# MAKRIVO

[Documentation complète dans toutes les langues 27](README.md) · [Le manuel utilisateur complet](HANDBOOK.md)

**par Aivana.** [Inventaire actuel des fonctionnalités et limites de publication](FEATURES.md) · [Rapport de test d'extension](testing/2026-10-12-six-extensions.md) · [Prototype d'assistant IA local](testing/2026-10-12-local-meeting-assistant.md)

**Anglais (US)** | [Deutsch](../de-DE/REFERENCE.md) | [Français](REFERENCE.md) | [Italiano](../it-IT/REFERENCE.md)

Un contrôleur de raccourci local Windows avec vignettes, profils, macros et détection de clavier de style Stream Deck. L'interface prend en charge les langues 27. Aucun compte ou connexion cloud n'est requis pour une utilisation quotidienne.

[Documentation](README.md) · [Vérification](VERIFICATION.md) · [Le rapport de test](testing/2026-10-09-usp-release.md)

## Commencer

Après publication, l'application se trouve dans `artifacts/win-x64/MAKRIVO.exe`. Conservez ensemble l'intégralité du dossier de sortie ; vous pouvez le copier ailleurs. Windows 11 x64 est la cible initiale. Les artefacts de compilation sont exclus de Git ; une copie du dépôt source n'inclut pas d'exécutable précompilé.

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

Les profils propres à une application utilisent un nom de processus sans son chemin complet. L'ordre des profils départage les correspondances ; le profil par défaut sert de solution de repli. La sélection automatique des profils doit être activée dans Paramètres.

Fermer la fenêtre principale conserve par défaut l'application dans la zone de notification. Double-cliquez sur son icône pour la rouvrir ; utilisez le menu de l'icône pour quitter. Le démarrage avec Windows est facultatif et désactivé par défaut. Les raccourcis peuvent être mis en pause de manière centralisée.

## Palette d'actions et de commandes

Ajouter une action ouvre un catalogue consultable avec les préréglages 90 dans huit catégories. Chacun a une description et un aperçu. L'éditeur de macros utilise le même catalogue pour ajouter ou remplacer des étapes. La sélection d'un préréglage dans ce workflow d'éditeur crée un brouillon sans exécution.

La palette de commandes s'ouvre depuis l'en-tête ou avec Ctrl+K dans l'application. Elle recherche les actions enregistrées dans tous les profils, les préréglages du catalogue, le changement de profil, la navigation, la pause/reprise, l'annulation des macros et l'import/export. Filtrez par actions enregistrées, fonctions du catalogue ou commandes de l'application. Les flèches sélectionnent, Entrée active, Ctrl+Enter modifie une entrée modifiable et Échap ferme. Les favoris apparaissent en premier. Les commandes du catalogue prêtes à l'emploi peuvent s'exécuter immédiatement ; les préréglages nécessitant une configuration ouvrent un brouillon. Les paramètres permettent de définir un raccourci global pour la palette. Les nouvelles configurations utilisent Ctrl+Shift+Space ; les configurations existantes ne reçoivent pas automatiquement de nouveau raccourci global.

Les actions audio règlent précisément le volume de 0 à 100 pour cent et activent, désactivent ou basculent la sourdine des périphériques de sortie et des microphones. Choisissez un périphérique précis ou laissez ce choix vide pour utiliser celui défini par défaut dans Windows. Si un périphérique enregistré est indisponible, une erreur est signalée au lieu d'en contrôler un autre. Actualiser les périphériques recharge la liste des périphériques connectés.

OBS Studio utilise WebSocket v5 : démarrer/arrêter/basculer/mettre en pause/reprendre l'enregistrement ; démarrer/arrêter/basculer le streaming ; changer de scène ; basculer la sourdine d'une entrée. Activez le serveur WebSocket dans Outils dans OBS et définissez son point de connexion dans l'éditeur. Vérifier la connexion et charger les noms lit la version d'OBS, les scènes et les entrées sans démarrer d'enregistrement ni de streaming. Choisissez les noms dans la liste ou saisissez-les manuellement. Les mots de passe peuvent être enregistrés dans Windows Credential Manager et sont exclus des configurations exportées. Les connexions OBS indisponibles signalent une erreur.

Les profils peuvent correspondre à un nom de processus et à une partie du titre d'une fenêtre. Les deux conditions spécifiées doivent correspondre. Verrouiller le profil empêche le changement automatique de profil pendant la session. Capturer les conditions depuis la fenêtre au premier plan utilise la dernière application externe ayant eu le focus.

Les actions de texte et de presse-papiers prennent en charge `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` et `{{user}}`. Les dates utilisent `yyyy-MM-dd` ; les heures utilisent `HH:mm`. Les variables inconnues restent inchangées. `{{profile}}` identifie le profil de l'action lancée ; `{{process}}` identifie le contexte de l'application externe. Le contenu du presse-papiers n'est lu que lorsque cette variable est utilisée. L'action de presse-papiers écrit du texte sans le coller automatiquement.

Les actions de fenêtre minimisent, agrandissent, restaurent ou organisent une fenêtre sur le côté gauche/droit de la zone de travail de son moniteur. L'action du système verrouille Windows. Ces actions ne s'exécutent que lorsqu'elles sont explicitement invoquées.

L'aperçu affiche les étapes, les répétitions et les variables sans exécution. Annuler et Rétablir conservent jusqu'à 100 états de brouillon ; les zones de texte conservent l’édition native Ctrl+Z. Marquez les favoris dans l'éditeur ou dans le menu de vignettes et filtrez-les sur le tableau de bord. Le nombre d’exécutions s’applique uniquement à la session en cours.

Les actions de texte, de raccourci et de fenêtre lancées depuis l'interface activent la dernière fenêtre cible externe. La palette affiche ce contexte cible. Si la cible s'est fermée ou que Windows refuse l'activation, l'exécution s'arrête avec un message.

## Assistance et commentaires au clavier

L'assistant de touches spéciales se trouve sur la page Clavier et dans la palette. Il liste les touches G signalées par iCUE. Détecter un appui les capture temporairement sans exécuter leurs actions affectées. Les affectations normales sont rétablies après détection, annulation ou fermeture. Une fonction du catalogue et une touche sélectionnée s'ouvrent comme brouillon non enregistré ; les affectations en double sont vérifiées.

La superposition clavier affiche la disposition de référence sélectionnée et les affectations actives. Maintenez Ctrl+Alt+F1 ou activez/désactivez-la depuis la palette ou la page Clavier. Elle ne prend pas le focus et laisse passer les clics de souris. Les changements de profil et d'affectations se mettent à jour pendant son affichage ; les frappes ordinaires Windows sont mises en évidence. Les commandes propres au fabricant peuvent ne pas fournir de signal de touche Windows équivalent.

Les commentaires d'exécution montrent l'attente, la progression de l'étape/répétition, l'achèvement, l'annulation ou l'échec sans se concentrer ni exposer le texte saisi. Commands Executed confirme l’exécution de la commande, et non un état actif interrogé indépendamment dans une application tierce. Les détails de l'erreur apparaissent dans le journal d'activité.

## Appareils et Corsair

Corsair G1–G6 peuvent être affectées directement via le SDK officiel iCUE. Sélectionnez le périphérique et la touche dans l'éditeur d'actions, puis enregistrez, ou préparez un brouillon depuis la vue du clavier K100. Les affectations s'appliquent au périphérique sélectionné et au profil actif ; la pause les désactive également. iCUE doit être en cours d'exécution et autoriser l'accès. Fn, la molette iCUE et les commandes de profil et de verrouillage ne peuvent pas être affectés via cette API G-key.

La page Appareils répertorie les interfaces clavier Windows et s'actualise périodiquement et lorsque les appareils se connectent. Un clavier physique peut exposer plusieurs interfaces HID. Les raccourcis clavier globaux standard fonctionnent avec tous les fabricants.

La détection Corsair utilise les informations disponibles sur l'appareil. Le contrôle RVB nécessite un matériel compatible, exécutant iCUE et le SDK officiel iCUE ; l'application signale la disponibilité réelle. D'autres fabricants bénéficient actuellement de détections et d'actions standard. En dehors de l'intégration de la touche G de Corsair, les commandes propriétaires fonctionnent lorsque Windows les reçoit en entrée régulière. L'application n'installe pas de pilote de clavier.

## Langues

Les paramètres proposent les langues 27 : allemand, anglais, français, espagnol, italien, portugais, néerlandais, polonais, tchèque, slovaque, hongrois, roumain, bulgare, grec, danois, suédois, norvégien, finnois, estonien, letton, lituanien, slovène, croate, ukrainien, russe, turc et serbe en écriture latine. Automatique suit la langue d'affichage Windows. Les traductions sont regroupées localement. Les mises en page, les raccourcis clavier et les noms créés par l'utilisateur sont indépendants de la langue de l'interface.

La documentation complète du repository est disponible dans toutes les langues d'application 27 à travers le [Indice de langue](README.md). Les traductions sont assistées par machine, avec des liens vers les sources originales et des contrôles structurels ; la révision par des locuteurs natifs est distincte. L'application accepte aussi les codes régionaux `en-US`, `de-DE`, `fr-FR` et `it-IT` et utilise leurs formats numériques. Les codes de langue existants restent compatibles.

## Mises en page et recherche de fabricants

La vue du clavier propose des dispositions de référence ISO-DE/ANSI-US avec ou sans pavé numérique, ainsi que des dispositions Corsair K100 RGB. La géométrie représente les positions, les touches larges/hautes et la touche Entrée ISO. Les dispositions standard sont des références génériques. Les dispositions K100 suivent les commandes documentées sans prétendre à des dimensions exactes au millimètre. Choisissez la bonne variante de langue/géométrie : un nom de modèle ou un VID/PID USB ne détermine pas une variante précise.

L'affectation résout les codes de balayage physiques avec la disposition de saisie Windows active. Le workflow prépare un brouillon et conserve les vérifications d'enregistrement et de conflits. Les affectations standard sont des raccourcis globaux Windows, pas des affectations isolées à un seul clavier connecté. Corsair G1–G6 utilisent l'intégration iCUE propre au périphérique. Les autres commandes propriétaires nécessitent un adaptateur ou des raccourcis configurés par le fabricant. La touche Entrée principale et celle du pavé numérique partagent le même identifiant de touche virtuelle pour les raccourcis globaux.

Le [Recherche de fabricant](research/tastaturhersteller-sonderfunktionen.md) et [CSV](research/tastaturhersteller-sonderfunktionen.csv) couvrent 74 groupes de marques/fabricants en distinguant configurateurs, SDK d'entrée et API d'éclairage. [Éléments documentant les dispositions](research/keyboard-layout-evidence.md) consigne les différences entre modèles, références SKU et modules. La couverture de la recherche ne signifie pas que la prise en charge matérielle soit implémentée.

## Données

La configuration est stockée dans les données d'application locales. L'import/export utilise JSON. L'importation prévisualise la configuration, remplace la configuration actuelle après confirmation et désactive les liaisons importées jusqu'à ce qu'elles soient activées manuellement. Le contenu des actions textuelles n’est pas collecté sous forme de journal de frappe. Les mots de passe Credential Manager sont distincts des profils exportés.

## Vérification

```powershell
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
```

Les vérifications Windows peuvent inspecter le matériel et configurer/libérer temporairement les liaisons iCUE prises en charge. Exécutez-les sans un autre contrôleur utilisant ces liaisons. Les vérifications des points de terminaison audio ne modifient pas les paramètres de volume en direct ou de sourdine.

La saisie native ne peut pas contourner les privilèges Windows. Un contrôleur lancé normalement peut être incapable d'envoyer des entrées à une application exécutée en administrateur. Les raccourcis réservés ou déjà utilisés apparaissent comme des conflits.

Le [Indice de documentation](README.md) renvoie aux conceptions historiques, aux plans, aux recherches et aux rapports de tests datés. Les plans historiques décrivent leur portée enregistrée, pas une garantie actuelle de prise en charge des fonctionnalités.

### Modèles de workflow et attentes de la fenêtre cible

Le catalogue comprend six flux de travail modifiables : espace de travail, réunion, streaming, recherche, changement de projet et extrait de texte. Recherchez `workflow.` dans le catalogue ou la palette de commandes. La sélection d'un flux de travail ouvre un brouillon non enregistré ; il ne fonctionne jamais immédiatement. Toutes les étapes sont conservées lors de l'ajout d'un workflow à une macro existante ou de son attribution à une touche Corsair. Configurez des exemples d'URL, de chemins d'accès aux programmes, de profils et de scènes OBS avant d'enregistrer.

L'étape **Attendre la fenêtre cible** accepte un nom de processus (avec ou sans `.exe`), une sous-chaîne du titre sans distinction de casse et un délai d'attente de 1–600000 ms. Au moins un filtre est requis ; les deux doivent correspondre lorsqu'ils sont fournis. La macro continue seulement lorsqu'une fenêtre visible correspondante répond et passe au premier plan. Utilisez un titre précis lorsque plusieurs fenêtres partagent un processus. L'annulation ou l'expiration du délai arrête les étapes suivantes. Une fenêtre prête ne garantit pas qu'un document soit chargé ou qu'un service externe comme OBS WebSocket soit connecté.

Luminosité de l'écran : recherchez **Augmenter la luminosité de l'écran**, **Diminuer la luminosité de l'écran** ou **Luminosité de l'écran à 50 pour cent** dans le catalogue d'actions. Les actions relatives modifient la luminosité de 10 points de pourcentage ; les valeurs fixes vont de 0 à 100. Les écrans internes pris en charge utilisent WMI ; les moniteurs externes compatibles nécessitent que DDC/CI soit activé dans leur menu. Par défaut, l'action affecte tous les affichages pris en charge ; un moniteur explicite peut être sélectionné dans l'éditeur. Les connexions et les stations d'accueil peuvent restreindre la prise en charge.

### Contrôles étendus

Ouvrez la palette de commandes avec **Ctrl+K lorsque la fenêtre de l'application a le focus**. Recherchez **Concepteur de workflow**, **Enregistrer une macro**, **Synchroniser les profils** ou **Diagnostic des touches** (le libellé dépend de la langue sélectionnée). Le concepteur modifie le brouillon de tuile sélectionné : conditions de fichier/processus/fenêtre, nombre d'étapes à sauter vers l'avant et arrêt/poursuite en cas d'erreur. L'enregistreur capture les combinaisons de raccourcis et les attentes, pas la saisie de texte ordinaire ; Échap arrête l'enregistrement. Examinez le résultat avant de l'adopter.

Dans une étape de luminosité, sélectionnez tous les écrans pris en charge ou un moniteur spécifique. Un moniteur sélectionné indisponible produit une erreur plutôt que de modifier silencieusement un autre affichage. Le tableau de bord affiche les lectures en direct de la luminosité, de l'audio et de OBS lorsque leurs intégrations respectives sont disponibles ; une lecture non disponible n'est pas présentée comme une valeur mesurée.

Dans l'éditeur de tuiles, affectez une touche physique spéciale prise en charge et choisissez un appui immédiat, un appui bref, un appui maintenu ou un double appui. Les sélecteurs de couche maintenus activent temporairement une autre affectation et rétablissent la couche précédente au relâchement. Les gestes et les couches s'appliquent aux touches G du SDK Corsair et aux touches spéciales standard propres au périphérique, pas aux raccourcis globaux ordinaires. La capture des touches standard prend en charge F13–F24 et les touches multimédias. Les commandes propriétaires Fn, profil et molette nécessitent toujours un adaptateur compatible du fabricant ou une affectation par le fabricant à une touche standard. Raw Input observe la touche sans bloquer son comportement Windows normal. Le diagnostic des touches explique les problèmes de profil, de pause, de connexion et d'affectation.

La synchronisation des profils échange des fichiers via un dossier que vous choisissez, y compris un dossier géré par votre client cloud existant. Vérifiez l'aperçu avant l'importation ; les importations démarrent désactivées et suppriment les mappages spécifiques au matériel. Les révisions empêchent les écrasements silencieux et l'inclusion du contenu des actions est un choix explicite. MAKRIVO ne fournit pas de compte cloud et ne conserve pas les informations d'identification cloud. Le plateau utilise désormais la propre icône M bleu sarcelle de MAKRIVO et affiche un badge de pause orange en cas de pause.
