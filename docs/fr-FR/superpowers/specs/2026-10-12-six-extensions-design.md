<!-- generated-doc locale=fr-FR source=docs/superpowers/specs/2026-10-12-six-extensions-design.md sha256=a80dda083fbbe90404023ac784b47e7f16f89f72ce0b821e3cda122dc561317f -->

> Traduction assistée par machine. La structure, les exemples de code et les liens sont vérifiés ; cela ne constitue ni une révision par un locuteur natif ni une validation juridique. Les rapports historiques conservent leur date et leur portée d'origine. [Source originale](../../../superpowers/specs/2026-10-12-six-extensions-design.md)

# MAKRIVO : six extensions de produit et adaptateurs de clavier

Date : 2026-10-12
Statut : conception proposée pour examen par les utilisateurs ; la mise en œuvre n’a pas commencé.

## Objectif
Fournissez les six extensions demandées dans l'application Windows/WPF existante, en préservant les profils existants, les affectations G1/G2, le fonctionnement local et l'utilisation sans compte. Étendez l'intégration du clavier aux Razer et Logitech, avec une architecture basée sur les capacités réutilisable pour d'autres fabricants. Le nom du fabricant ou la disposition du clavier ne constituent jamais une preuve que les contrôles propriétaires sont pris en charge.

## Décision architecturale
Étendez Aivana.Core avec des contrats et une validation sérialisables, Aivana.Windows avec Windows et des adaptateurs de fournisseur, et Aivana.Desktop avec des éditeurs transactionnels et des vues d'état. Utilisez des projets compagnons distincts pour l’exécution de plugins isolés et le service de panneau mobile local. Évitez une réécriture ou un seul processus privilégié hébergeant chaque extension. La configuration existante doit se charger sans modification ; ajoutez des types d'action plutôt que de réorganiser leurs valeurs d'énumération persistantes.

Alternatives envisagées : une mise en œuvre monolithique enchevêtrerait l'exécution du matériel, du réseau et du flux de travail ; le remplacement de WPF ajouterait un travail de migration sans rapport. Des extensions modulaires sont recommandées.

## 1. Pavé macro du clavier indépendant
Stockez les liaisons par identité de périphérique, ainsi que par code d'analyse physique et indicateurs de clé étendue. Ajoutez des modes d'observation explicite et exclusifs, la découverte de périphériques, la capture de clés, la suppression de répétitions, les gestes, les couches maintenues et les diagnostics de reconnexion. Préserver le pavé principal/numéro Saisissez les distinctions et testez les dispositions allemandes/américaines. Les liaisons ciblent un clavier physique sélectionné ; les identités de reconnexion ambiguës nécessitent une nouvelle sélection plutôt que le contrôle d'un autre appareil.

Raw Input fournit uniquement une observation. La suppression exclusive par appareil nécessite un véritable backend de filtre de clavier ; ne corrélez jamais les hooks globaux de bas niveau avec Raw Input et revendiquez une suppression fiable spécifique à l'appareil. Fournissez un chien de garde, une version d'urgence, un comportement d'ouverture en cas d'échec et un programme d'installation/désinstallation. Ne capturez pas les entrées sécurisées du bureau. L'installation du pilote et l'activation du mode exclusif sont des actions utilisateur distinctes et explicites.

Driver Gate : sélectionnez un backend pris en charge, correctement licencié et signé, compatible avec les configurations cibles Windows 11, ou développez/signez un filtre dédié. Interception est un candidat à évaluer, et non une dépendance de production engagée : son README officiel décrit les tests via Windows 10 et des licences commerciales distinctes. Jusqu'à ce que cette porte soit remplie, le mode exclusif doit être affiché comme indisponible ; la fonctionnalité ne peut pas être déclarée complète.

Acceptation : avec deux claviers physiques, la même touche exécute une macro et ne produit aucune entrée d'application normale uniquement sur le périphérique exclusif sélectionné ; les autres saisies au clavier ne sont pas affectées. Vérifiez la déconnexion, le crash, l'annulation, la suspension/reprise et la suppression du pilote sur le matériel réel.

## 2. Configuration guidée de l'espace de travail
Ajoutez un assistant pour le lieu de travail, les réunions, le streaming, la recherche et le changement de projet. Découvrez les applications installées, les appareils audio, les moniteurs et les scènes OBS. Les utilisateurs choisissent les cibles et les liaisons ; prévisualisez les étapes résultantes et validez les exigences manquantes. Enregistrez uniquement après examen, sans aucune action exécutée pendant la configuration. Les profils générés restent modifiables dans les éditeurs existants.

Acceptation : créez un profil de travail sans modifier manuellement les chemins d'exécution ou les identifiants de périphérique ; l'annulation laisse les paramètres inchangés ; le logiciel manquant a une explication exploitable.

## 3. Exécution vérifiée
Introduisez des contrôles en amont, des résultats saisis par étape et des postconditions facultatives. Distinguer les états demandé, terminé, vérifié, échoué, expiré, annulé et invérifiable. Vérifiez l'audio/le microphone, la luminosité, l'état de préparation du processus/de la fenêtre et l'état de OBS avec des lectures asynchrones limitées. La livraison d'un raccourci/texte ne constitue pas une preuve que l'application cible a accepté la commande. Les effets secondaires externes ne sont pas universellement réversibles ; offrir une compensation uniquement lorsqu’elle est mise en œuvre.

Acceptation : les cibles explicites déconnectées ne reculent jamais silencieusement ; un mauvais état observé ne signale jamais un succès vérifié ; l'annulation arrête le sondage ; les échecs identifient l’étape et l’action de récupération. L’historique d’exécution omet les charges utiles et les secrets de texte.

## 4. Plugins et packages partageables
Les manifestes JSON versionnés déclarent les actions, les schémas de paramètres, les fournisseurs d'état, les dépendances et les capacités. Exécutez les plugins exécutables dans un processus distinct sur un IPC local limité avec poignée de main, délais et annulation. Rejeter les messages mal formés et les versions incompatibles ; gérer les plantages sans mettre fin au contrôleur. Les déclarations de capacités sont une divulgation et une médiation, et non une revendication de sandboxing du système d'exploitation.

Fournissez un aperçu de l'exportation/importation du package, des hachages de contenu, une protection contre le parcours, des limites de taille, des vérifications de version/dépendance, une activation explicite et une restauration de la mise à niveau. Les plugins exécutables importés ne doivent pas s'exécuter automatiquement. Incluez un exemple de plugin fonctionnel avec un flux d'installation/action/état/désinstallation de bout en bout. Aucun service de marché public n'est requis pour cette version.

Acceptation : créer, exporter, installer et exécuter un exemple réel d'intégration ; démontrer le délai d'attente, l'isolation des crashs, l'échec des dépendances, le rejet de traversée et l'importation désactivée.

## 5. Windows Automatisation de l'interface utilisateur
Ajoutez un sélecteur d'élément et des actions limitées pour l'invocation, la valeur, la sélection, la bascule, le focus et l'attente d'un élément/état. Conserver les sélecteurs en utilisant le processus, l'ID d'automatisation, le type de contrôle et le contexte ancêtre ; évitez l’enregistrement de coordonnées uniquement. Afficher le nombre de correspondances des sélecteurs et tester les sélecteurs en lecture seule. Les champs de mot de passe sont exclus de l'enregistrement. Gérez explicitement les contrôles non pris en charge et les restrictions de niveau d’intégrité Windows.

Prolongez l'enregistrement avec des actions UIA révisables, en conservant l'enregistrement de raccourci actuel. Les sélecteurs ambigus échouent au lieu de cliquer sur un contrôle arbitraire. Une arborescence d'accessibilité de navigateur normale n'est pas la promesse d'un moteur d'automatisation DOM complet.

Acceptation : automatisez une application de test dédiée avec des sélecteurs stables lors des changements de position/taille ; Ambiguïté des tests, éléments manquants, contrôles désactivés, délais d'attente et annulation.

## 6. Panneau local téléphone/tablette
Servez un panel réactif à partir d’un service LAN opt-in. Associez-le explicitement à l’aide d’un code unique de courte durée et émettez des informations d’identification révocables protégées localement. Utilisez HTTPS avec une approbation/épinglage de certificat local documenté ; pas de solution de secours pour l'envoi des informations d'identification en texte brut. Par défaut, désactivé et restreindre les interfaces configurées. La validation de l'origine, les commandes authentifiées, les limites de débit et les messages limités sont obligatoires.

Exposer les profils/actions autorisés, l'état en direct, la progression et l'annulation ; utilisez des ID stables et la même file d’attente d’exécution que les clés de bureau et physiques. N'exposez pas de commandes arbitraires, de chemins d'exécutables, de secrets ou d'installations de plugins. Fournir des écrans de connexion, d’appairage et de révocation. Aucun compte cloud ni relais Internet n'est inclus.

Acceptation : fonctionne à partir d'un vrai téléphone/tablette sur le même réseau local ; les clients non appariés ne peuvent pas répertorier ou exécuter des actions ; les jetons révoqués échouent ; les commandes dupliquées/rejouées ne s'exécutent pas deux fois ; La déconnexion ne réessaye pas silencieusement les actions.

## Portée du fabricant de clavier

| Adaptateur/famille | Intégration prévue | Limite explicite |
| --- | --- | --- |
| Corsair | Préserver l'entrée actuelle iCUE G-key et l'intégration RVB | La détection des capacités du modèle/logiciel reste nécessaire |
| LogitechG | Adaptateur SDK G-key avec contrôles de disponibilité en mode presse/libération et lors de l'exécution ; éclairage via son SDK séparé là où il est pris en charge | N'assimilez pas la détection G HUB à l'API G-key prise en charge sur chaque modèle |
| Razer | Adaptateur d'éclairage Chroma et mappage guidé Synapse-à-touche standard, plus entrée HID générique | Chroma est une API d'éclairage ; ne pas l'étiqueter entrée de macro-clé propriétaire native |
| SteelSeries | GameSense adaptateur état/éclairage plus entrée HID générique | La soumission d'événements GameSense n'est pas une API de capture de clé propriétaire générale |
| Wooting | Adaptateur SDK analogique pour entrée analogique prise en charge, ainsi que touches HID normales | Les fonctionnalités et les conditions de distribution du SDK doivent être vérifiées par modèle pris en charge |
| Keychron/QMK/VIA | Clés HID standard et instructions de mappage du micrologiciel ; tracés connus uniquement avec preuve | La prise en charge de la configuration VIA n'est pas une API de macro-clé d'exécution universelle |
| Autres claviers HID standard | Découverte et mappage de clés physiques par appareil ; mode exclusif uniquement avec le backend de filtre validé | Pas de prise en charge de Fn, de cadran ou de clé propriétaire spécifique au modèle |

Chaque appareil affiche l'état détecté, observé, cartographié via le logiciel du fournisseur, le SDK natif, le pilote exclusif, indisponible ou matériel non testé, selon le cas. Préservez les preuves de capacité avec les versions d’adaptateur et les identifiants de modèle/micrologiciel testés.

## Bon de livraison
1. Compatibilité de configuration, contrats de capacité et exécution vérifiée.
2. Configuration guidée à l'aide de découvertes réelles et d'exécuteurs d'actions existants.
3. UI Automation et son enregistreur/appareil de test.
4. Hôte du plugin, flux de packages et exemple d'intégration.
5. Panneau LAN couplé et vérification mobile.
6. Adaptateurs de fournisseur étendus et intégration exclusive du backend du clavier, avec validation matérielle réelle.

Les six restent obligatoires. La signature/licence des pilotes, les SDK inaccessibles ou l'absence de matériel de test sont des portes de sortie, et non des raisons pour substituer un comportement simulé et qualifier le travail de terminé.

## Critères de vérification et de publication
Exécutez les vérifications de base existantes, Windows et WPF et ajoutez des tests d'intégration ciblés pour chaque nouvelle limite. Vérifier les allers-retours JSON et les profils hérités ; testez le contenu exécutable importé, l’authentification réseau, l’annulation, les pannes de plug-in et la reconnexion de l’appareil. Inspectez les mises en page de bureau et mobiles. Validez séparément la mise à l’échelle élevée de Windows. Créez des artefacts de version uniquement après avoir réussi les vérifications ; distinguer les fonctionnalités implémentées/testées, implémentées/non testées sur le matériel et bloquées en externe dans les notes de version. Ne remplacez pas l'application installée, n'installez pas de pilotes, ne modifiez pas les véritables mappages de clavier et n'ouvrez pas l'accès au réseau à titre de test accidentel.

## Preuve
- Microsoft Raw Input sémantique : https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-rawinputdevice
- Logitech SDK du développeur G : https://www.logitechg.com/en-us/programs/developer-alliance
- FAQ sur le SDK Chroma Razer : https://developer.razer.com/works-with-chroma/chroma-faq/
- Référentiel d'interception et conditions de dépendance : https://github.com/oblitum/Interception
- Inventaire des fabricants de référentiels existants : docs/de-DE/research/tastaturhersteller-sonderfunktionen.md

## Réviser les décisions
L'utilisateur est invité à approuver cette conception écrite avant le plan de mise en œuvre. La principale dépendance externe est un backend de pilote à entrée exclusive prêt pour la production. La portée recommandée conserve cela comme un livrable limité plutôt que de le réduire silencieusement à l'observation Raw Input.
