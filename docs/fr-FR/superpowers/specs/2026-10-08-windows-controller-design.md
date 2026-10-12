<!-- generated-doc locale=fr-FR source=docs/superpowers/specs/2026-10-08-windows-controller-design.md sha256=f36d496c6af5190889e9a31487a8fc6afdeba5af18facdd600639ede5ddbd169 -->

> Traduction assistée par machine. La structure, les exemples de code et les liens sont vérifiés ; cela ne constitue ni une révision par un locuteur natif ni une validation juridique. Les rapports historiques conservent leur date et leur portée d'origine. [Source originale](../../../superpowers/specs/2026-10-08-windows-controller-design.md)

# MAKRIVO – Brouillon

[English (US)](2026-10-08-windows-controller-design.md) | [Deutsch](2026-10-08-windows-controller-design.md) | [Français](2026-10-08-windows-controller-design.md) | [Italiano](2026-10-08-windows-controller-design.md)

Statut : 8. Octobre 2026. Statut : pour examen avant mise en œuvre.

## Objectif et portée convenue

Une application autonome Windows transforme un clavier de PC normal en un panneau d'action librement configurable similaire à un stream deck. L'utilisateur a confirmé tous les types d'actions suggérés, les macros, la détection de périphérique, les profils et une bonne interface graphique. Le Corsair connecté et les autres claviers sont reconnus ; Les fonctions constructeur supplémentaires ne sont proposées que si elles sont réellement prises en charge.

Hypothèses : interface allemande, utilisation locale sans compte, Windows 11 x64 comme première cible, pas d'installation de pilote. Le clavier reste utilisable pour une saisie normale. Par défaut, les mappages globaux utilisent des raccourcis clavier avec des modificateurs. Les touches de lettres individuelles ne sont pas globalement réservées par défaut.

## Décision technique

C#/.NET-10-App natif avec WPF et MVVM. La version stable .NET-10-SDK est disponible dans l'espace de travail. WPF permet une interface de bureau personnalisée et une intégration directe de Windows. Les alternatives seraient une interface Web avec un service Windows distinct ou une application de bureau multiplateforme ; les deux apportent un temps d'exécution et une complexité de communication supplémentaires à ce projet Windows. La variante native est donc la base choisie.

Division : projet de bureau pour les vues et la barre d'état, projet principal pour la logique de configuration et d'exécution, projet Windows pour les raccourcis clavier, simulation d'entrée et périphériques, projet de test pour les règles indépendantes de la plate-forme. Les adaptateurs du fabricant dépendent d'une interface à capacité fixe et sont indépendants des actions standard.

## surface

Design sombre avec des surfaces calmes de couleur anthracite, une police facile à lire et des accents turquoise. Liens de navigation pour le tableau de bord, les profils, les appareils et les paramètres. Profil actif, recherchez et basculez pour mettre en pause tous les raccourcis clavier en haut. L'interface reste utilisable à la mise à l'échelle 100–200 pour cent Windows.

Le tableau de bord comprend une grille de tuiles personnalisable. Chaque vignette affiche l'icône, le nom, le raccourci clavier et l'état d'exécution. Le clic effectue la même action que le raccourci clavier. Un éditeur s'ouvre à droite avec le type d'action, les paramètres, l'enregistrement des raccourcis clavier, le symbole/couleur, l'activation et le bouton de test. Nouveau, Dupliquer, Supprimer et Réorganiser sont disponibles. Une vue clavier montre schématiquement les affectations pour la disposition allemande standard ; il ne revendique pas la géométrie exacte du dispositif détecté.

Les profils peuvent être créés, copiés, renommés et activés manuellement. Un nom de processus est éventuellement attribué à un profil. Un programme de premier plan approprié active son profil, sinon le profil standard global s'applique. S'il existe plusieurs profils correspondants, un ordre explicitement visible s'applique. L'éditeur explique les conflits de raccourcis clavier directement lors de l'affectation.

## Raccourcis clavier et entrées

Windows RegisterHotKey gère les combinaisons globales normales avec la répétition des touches supprimée. Seul le profil actif est enregistré. Les combinaisons réservées ou déjà occupées apparaissent sous forme d'erreurs ; l'application ne le signale pas comme étant activé avec succès. L'enregistrement dans l'éditeur ne déclenche aucune action.

Avant les entrées simulées, les touches de modification de déclenchement sont relâchées ou on attend qu'elles soient physiquement relâchées. Le texte Unicode et les combinaisons de touches sont envoyés à l'aide de SendInput. Chaque touche sur laquelle vous appuyez sera à nouveau relâchée même si vous annulez. Les actions s'exécutent en dehors du thread de l'interface utilisateur ; L'exécution et les modifications de profil ne doivent pas bloquer la surface.

## Actions et macros

Types d'actions : démarrer le programme avec les arguments et le répertoire de travail, ouvrir le fichier/dossier via Windows, ouvrir le site Web HTTP/HTTPS, saisir le texte Unicode, envoyer la combinaison de touches, lecture/pause/suivant/précédent/arrêt du média, augmenter/diminuer/muet et changer de profil. Les paramètres sont validés avant la sauvegarde et à nouveau avant l'exécution. Les programmes sont démarrés directement ; Les arguments ne sont pas implicitement interprétés comme des commandes shell.

Les macros contiennent une liste ordonnée d'actions nommées et d'étapes de pause explicites. Les étapes peuvent être ajoutées, modifiées et déplacées. Les répétitions sont enfin configurables (1–100) ; Les boucles infinies ne font pas partie de la première version. Une macro en cours d'exécution peut être terminée via l'interface ou votre propre raccourci clavier d'annulation. Le déclenchement répété de la même affectation de courant ne démarre pas une exécution parallèle. Différentes macros avec simulation d'entrée sont sérialisées afin que leurs frappes ne se mélangent pas. Si une erreur d'étape se produit, la macro s'arrête et affiche l'étape affectée.

## Appareils et fabricants

Inventaire des appareils via Windows Raw Input et informations supplémentaires sur l'appareil : nom d'affichage, chemin de l'appareil, identifiant du fabricant/produit si disponible, état de connexion et capacités détectées. Le branchement/débranchement met à jour la liste sans redémarrer. Un périphérique physique peut avoir plusieurs entrées HID ; Ceux-ci sont regroupés selon des identifiants fiables, sinon ils sont conservés visiblement sous forme d'entrées distinctes.

Les raccourcis clavier normaux s'appliquent à tous les claviers. La vue du périphérique fournit des informations et des fonctionnalités ; un clavier séparé avec des touches exclusivement interceptées ne fait pas partie de ce mode convenu.

La détection Corsair fonctionne également sans iCUE. Un adaptateur SDK iCUE en option fournit l'état de la connexion et les actions d'éclairage prises en charge lorsque le SDK, le iCUE et un appareil compatible sont présents. L'interface utilisateur différencie les SDK reconnus, disponibles, connectés et réellement contrôlables. Le SDK documente principalement le contrôle des LED ; Les touches G propriétaires ne sont prises en charge comme les raccourcis clavier normaux que si Windows reçoit une entrée appropriée. Il n’y a pas de promesse générale pour toutes les clés spéciales.

D'autres fabricants reçoivent dans un premier temps une détection générique et des actions standard. L'interface de l'adaptateur permet des intégrations ultérieures ; les compétences non prises en charge sont honnêtement affichées. Les tests matériels pour les modèles inexistants ne sont pas signalés comme réussis.

## Rangement et vie quotidienne

Configuration JSON versionnée dans le répertoire de données de l'application locale. Sauvegarde atomique avec la dernière sauvegarde valide ; les fichiers incorrects ne sont pas écrasés silencieusement. Importez le format, la version, les identifiants et les paramètres des chèques et affiche un aperçu avant de postuler. Les actions importées ne peuvent être exécutées qu'après une activation explicite. L'exportation contient des profils et des affectations.

Menu de la barre d'état : Ouvrir, sélection de profil, mettre en pause les raccourcis clavier, annuler les macros, quitter. Fermer la fenêtre réduit la barre d'état par défaut et l'explique pour la première fois. Le démarrage automatique est facultatif et désactivé par défaut. Une seule instance gère les raccourcis clavier. Quitter enregistre les raccourcis clavier, abandonne les exécutions et libère les ressources.

Vue des activités locales avec un historique limité des réussites, des échecs et des annulations. Aucun journal de toutes les frappes. Le contenu de l'action textuelle n'est pas précisé dans les messages d'erreur. Pas de service cloud et pas de télémétrie.

## Gestion des erreurs et limites

Les programmes manquants, les URL invalides, les raccourcis clavier occupés, les profils endommagés et le SDK du fabricant inaccessible reçoivent des messages allemands spécifiques. Une panne d'adaptateur n'affecte pas les actions standard. Les entrées dans des applications à privilèges plus élevés peuvent échouer en raison des limites d'autorisation Windows ; Les erreurs sont expliquées, l'application ne nécessite pas de droits d'administrateur par défaut.

## Tests et acceptation

Vérifications automatisées de la priorité des profils, de la détection des conflits, de l'importation de la configuration, du stockage atomique, de l'ordre des macros, des nouvelles tentatives, de l'annulation et de la propagation des erreurs. Tests de fumée Windows pour les vrais raccourcis clavier, les raccourcis texte et clavier, les actions multimédias, l'inventaire des appareils, la barre d'état et la sortie propre. Aucun test qui lance accidentellement des programmes ou des sites Web arbitraires.

Vérification de l'interface graphique : créer et modifier une affectation, enregistrement sans exécution, clic/raccourci clavier avec le même effet, annulation de macro, message de conflit, changement de profil, vue de l'appareil, importation/exportation et mise à l'échelle. Les tests matériels spécifiques au fabricant sont documentés séparément de l'appareil lui-même.

Livraison : Code source, instructions de construction/démarrage reproductibles, manuel d'utilisation allemand et Windows-x64-Publish-Artefakt local. Le succès signifie une application exécutable avec toutes les fonctionnalités génériques ; Les fonctionnalités de Corsair sont uniquement marquées comme disponibles avec une prise en charge éprouvée du SDK/périphérique.

## Sources techniques

- Microsoft Entrée clavier / RegistreHotKey : https://learn.microsoft.com/en-us/windows/win32/inputdev/about-keyboard-input
- Microsoft Raw Input: https://learn.microsoft.com/en-us/windows/win32/inputdev/about-raw-input
- Microsoft WPF Entrée : https://learn.microsoft.com/dotnet/desktop/wpf/advanced/input-overview
- SDK officiel Corsair iCUE : https://github.com/CorsairOfficial/cue-sdk/blob/master/README.md

## Étape suivante

Après avoir examiné ce projet : créez un plan de mise en œuvre concret et soumettez-le pour sélection de mise en œuvre. Il n'y a pas encore de dépôt Git dans le dossier de travail vide ; Ce document n'a donc pas encore été engagé.
