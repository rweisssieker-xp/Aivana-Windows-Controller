# Aivana Windows Controller — conception

[English (en-US)](../../../en-US/superpowers/specs/2026-10-08-windows-controller-design.md) | [Deutsch (de-DE)](../../../de-DE/superpowers/specs/2026-10-08-windows-controller-design.md) | [Français (fr-FR)](2026-10-08-windows-controller-design.md) | [Italiano (it-IT)](../../../it-IT/superpowers/specs/2026-10-08-windows-controller-design.md)

État au 8 octobre 2026. Statut : à examiner avant la mise en œuvre.

## Objectif et périmètre convenu

Une application Windows autonome transforme un clavier PC ordinaire en panneau d’actions librement configurable, semblable à un Stream Deck. L’utilisateur a approuvé tous les types d’action proposés, les macros, la détection des périphériques, les profils et une interface soignée. Les claviers Corsair et autres connectés sont détectés ; les fonctions supplémentaires des fabricants ne sont proposées que lorsqu’elles sont réellement prises en charge.

Hypothèses : interface allemande, usage local sans compte, Windows 11 x64 comme première cible, aucune installation de pilote. Le clavier reste utilisable pour la saisie ordinaire. Par défaut, les affectations globales utilisent des combinaisons avec modificateurs. Les touches alphabétiques seules ne sont pas réservées globalement par défaut.

## Décision technique

Application native C#/.NET 10 avec WPF et MVVM. Le SDK .NET 10 stable est disponible dans l’espace de travail. WPF permet une interface de bureau personnalisée et une intégration directe à Windows. Les autres options seraient une interface Web avec un service Windows séparé ou une application de bureau multiplateforme ; toutes deux ajoutent de la complexité d’exécution et de communication pour ce projet Windows. La variante native est donc la base choisie.

Répartition : projet Desktop pour les vues et la zone de notification ; projet Core pour la configuration et la logique d’exécution ; projet Windows pour les raccourcis, la simulation des entrées et les périphériques ; projet Tests pour les règles indépendantes de la plateforme. Les adaptateurs fabricant reposent sur une interface de capacités fixe et restent indépendants des actions standard.

## Interface

Thème sombre avec surfaces anthracite sobres, texte lisible et accent turquoise. Navigation à gauche vers le tableau de bord, les profils, les périphériques et les paramètres. En haut : profil actif, recherche et commande de mise en pause de tous les raccourcis. L’interface reste utilisable avec une mise à l’échelle Windows de 100 à 200 %.

Le tableau de bord contient une grille de tuiles personnalisable. Chaque tuile affiche une icône, un nom, un raccourci et l’état d’exécution. Un clic déclenche la même action que le raccourci. Un éditeur s’ouvre à droite avec le type d’action, ses paramètres, la capture du raccourci, l’icône/couleur, l’activation et un bouton de test. Création, duplication, suppression et réorganisation sont disponibles. Une vue clavier présente schématiquement les affectations selon la disposition allemande standard ; elle ne prétend pas reproduire exactement la géométrie du périphérique détecté.

Les profils peuvent être créés, copiés, renommés et activés manuellement. Il est possible d’associer un nom de processus à un profil. Une application correspondante au premier plan active son profil ; sinon, le profil standard global s’applique. Si plusieurs profils correspondent, un ordre explicitement visible les départage. L’éditeur signale les conflits de raccourcis directement à côté de l’affectation.

## Raccourcis et entrées

Windows RegisterHotKey traite les combinaisons globales ordinaires sans répéter les touches maintenues. Seul le profil actif est enregistré. Les combinaisons réservées ou déjà utilisées sont signalées comme erreurs ; l’application ne prétend pas qu’elles ont été activées. La capture dans l’éditeur ne déclenche aucune action.

Avant de simuler des entrées, les touches modificatrices qui ont déclenché l’action sont relâchées ou leur relâchement physique est attendu. Le texte Unicode et les combinaisons sont envoyés avec SendInput. Chaque touche enfoncée par l’application est également relâchée en cas d’annulation. Les actions s’exécutent hors du fil d’interface ; ni leur exécution ni le changement de profil ne doivent bloquer l’interface.

## Actions et macros

Types d’action : lancer un programme avec arguments et répertoire de travail ; ouvrir un fichier/dossier avec Windows ; ouvrir une page HTTP/HTTPS ; saisir du texte Unicode ; envoyer un raccourci ; contrôler les médias Lecture/Pause/Suivant/Précédent/Arrêt ; augmenter/réduire/couper le volume ; changer de profil. Les paramètres sont validés avant l’enregistrement et une nouvelle fois avant l’exécution. Les programmes sont lancés directement ; les arguments ne sont pas implicitement interprétés comme des commandes shell.

Les macros contiennent une liste ordonnée des actions citées et des étapes de pause explicites. Les étapes peuvent être ajoutées, modifiées et déplacées. Le nombre de répétitions est limité et configurable (1 à 100) ; les boucles infinies ne font pas partie de la première version. Une macro en cours peut être arrêtée depuis l’interface ou avec un raccourci dédié. Un nouveau déclenchement de la même affectation en cours ne lance pas d’exécution parallèle. Les différentes macros qui simulent des entrées sont sérialisées afin que leurs appuis ne se mélangent pas. En cas d’échec d’une étape, la macro s’arrête et indique l’étape concernée.

## Périphériques et fabricants

Inventaire des périphériques via Windows Raw Input et informations complémentaires : nom affiché, chemin, identifiant du fabricant/produit lorsqu’il est disponible, état de connexion et capacités détectées. La connexion ou le débranchement actualise la liste sans redémarrage. Un périphérique physique peut avoir plusieurs entrées HID ; elles sont regroupées selon des identifiants fiables ou, à défaut, présentées séparément de manière visible.

Les raccourcis ordinaires fonctionnent pour tous les claviers. La vue Périphériques affiche les informations et les capacités ; le mode convenu ne prévoit pas de clavier distinct dont les touches seraient interceptées exclusivement.

La détection Corsair fonctionne même sans iCUE. Un adaptateur facultatif au SDK iCUE indique l’état de connexion et propose les commandes d’éclairage prises en charge si le SDK, iCUE et un appareil compatible sont disponibles. L’interface distingue périphérique détecté, SDK disponible, connecté et réellement contrôlable. Le SDK documente surtout le contrôle des LED ; les touches G propriétaires ne sont prises en charge comme raccourcis ordinaires que si Windows reçoit les entrées correspondantes. Aucune prise en charge générale de toutes les touches spéciales n’est promise.

Les autres fabricants bénéficient d’abord de la détection générique et des actions standard. L’interface d’adaptateur permet des intégrations futures ; les capacités non prises en charge sont affichées honnêtement. Un test matériel sur un modèle absent ne sera pas présenté comme réussi.

## Stockage et utilisation quotidienne

Configuration JSON versionnée dans le répertoire local des données de l’application. Enregistrement atomique avec la dernière sauvegarde valide ; les fichiers erronés ne sont pas silencieusement écrasés. L’import vérifie le format, la version, les identifiants et les paramètres, puis affiche un aperçu avant application. Les actions importées ne peuvent être exécutées qu’après activation explicite. L’export comprend les profils et affectations.

Menu de la zone de notification : ouvrir, choisir un profil, mettre en pause les raccourcis, annuler les macros, quitter. Par défaut, fermer la fenêtre la réduit dans cette zone et l’explique la première fois. Le démarrage automatique est facultatif et désactivé par défaut. Une seule instance gère les raccourcis. À la fermeture, l’application désenregistre les raccourcis, annule les exécutions et libère les ressources.

Vue d’activité locale avec historique limité des réussites, erreurs et annulations. Aucun journal de toutes les frappes. Le contenu des actions de texte n’est pas recopié dans les messages d’erreur. Aucun service cloud ni télémétrie.

## Gestion des erreurs et limites

Les programmes manquants, URL invalides, raccourcis déjà utilisés, profils endommagés et SDK fabricant inaccessible produisent des messages explicites en allemand. Une erreur d’adaptateur n’affecte pas les actions standard. L’envoi d’entrées vers des applications plus privilégiées peut échouer à cause des limites d’autorisation Windows ; l’erreur est expliquée et l’application ne demande pas les privilèges administrateur par défaut.

## Vérification et réception

Vérifications automatisées de la priorité des profils, des conflits, de l’import de configuration, de l’enregistrement atomique, de l’ordre des macros, des répétitions, de l’annulation et de la propagation des erreurs. Tests rapides Windows des vrais raccourcis, du texte et des combinaisons, des actions média, de l’inventaire de périphériques, de la zone de notification et d’une fermeture propre. Aucun test ne doit lancer accidentellement des programmes ou sites arbitraires.

Contrôle GUI : créer et modifier une affectation, la capturer sans exécution, vérifier que clic et raccourci ont le même effet, annuler une macro, afficher un conflit, changer de profil, inspecter les périphériques, importer/exporter et vérifier la mise à l’échelle. Les tests matériels propres aux fabricants sont consignés séparément avec les appareils réellement disponibles.

Livraison : code source, instructions reproductibles de compilation/démarrage, guide d’utilisation allemand et artefact de publication Windows x64 local. Le succès signifie une application exécutable avec toutes les fonctions génériques ; les fonctions Corsair sont indiquées comme disponibles uniquement si la prise en charge SDK/périphérique est attestée.

## Sources techniques

- Microsoft Keyboard Input / RegisterHotKey : https://learn.microsoft.com/en-us/windows/win32/inputdev/about-keyboard-input
- Microsoft Raw Input : https://learn.microsoft.com/en-us/windows/win32/inputdev/about-raw-input
- Microsoft WPF Input : https://learn.microsoft.com/dotnet/desktop/wpf/advanced/input-overview
- SDK Corsair iCUE officiel : https://github.com/CorsairOfficial/cue-sdk/blob/master/README.md

## Étape suivante

Après examen de cette conception : préparer un plan d’implémentation concret et le présenter pour décider de son exécution. Le répertoire de travail étant vide, il ne contient encore aucun dépôt Git ; ce document n’a donc pas encore été validé.
