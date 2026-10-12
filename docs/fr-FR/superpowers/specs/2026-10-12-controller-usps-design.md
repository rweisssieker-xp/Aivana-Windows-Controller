<!-- generated-doc locale=fr-FR source=docs/superpowers/specs/2026-10-12-controller-usps-design.md sha256=4c3b2dbedb83af6bd48917515ae6559012afb6318ce69d1e32fb2765e0793327 -->

> Traduction assistée par machine. La structure, les exemples de code et les liens sont vérifiés ; cela ne constitue ni une révision par un locuteur natif ni une validation juridique. Les rapports historiques conservent leur date et leur portée d'origine. [Source originale](../../../superpowers/specs/2026-10-12-controller-usps-design.md)

# MAKRIVO extensions de contrôle

L'utilisateur a demandé les neuf améliorations proposées et une icône de barre d'état reconnaissable. Les configurations existantes, notamment la luminosité G1/G2, doivent rester compatibles. Les modifications existantes de marque et de documentation non validées appartiennent à l’espace de travail partagé et doivent être conservées.

## Livrables et limites

1. Les actions de luminosité sélectionnent tous les moniteurs pris en charge ou un ID de moniteur spécifique stable. Les cibles explicites manquantes échouent visiblement. Le tableau de bord affiche la luminosité actuelle, le volume de sortie, l'état du microphone et l'état du OBS via des lectures asynchrones limitées.
2. Les touches spéciales prennent en charge les pressions immédiates, les pressions, les maintiens et les doubles pressions, avec suppression répétée. Un sélecteur de couche maintenu modifie temporairement le mappage actif et le relâchement le restaure. Les liaisons immédiates existantes conservent leur comportement. La pause, les changements de profil, les déconnexions et l'annulation effacent les gestes en attente.
3. Le concepteur de flux de travail modifie les conditions, transfère les branches et la politique d'arrêt/continuation des échecs de manière transactionnelle dans un brouillon de vignettes. Les conditions couvrent les fichiers, les processus en cours d’exécution et les titres des fenêtres. Pas de cycles de sauts illimités.
4. L'enregistrement explicite capture les accords de raccourci et attend, ignore les entrées injectées et s'arrête sur Escape. Les actions enregistrées sont examinées avant adoption ; le texte ordinaire n’est pas enregistré.
5. Les clés spéciales standard par appareil d'autres fabricants utilisent Windows Raw Input. Les clés propriétaires nécessitent une API de fournisseur prise en charge ou un mappage de fournisseur vers une clé standard ; les fonctionnalités non prises en charge sont affichées honnêtement. Le diagnostic explique le profil, la pause, les conflits de liaison et l'état réel de l'adaptateur.
6. L'échange de profils utilise un dossier partagé sélectionné par l'utilisateur (y compris les dossiers synchronisés par un client cloud existant). Les contrôles de révision empêchent les écrasements silencieux. Pull prévisualise les modifications et importe les actions désactivées avec les mappages matériels supprimés. L'inclusion du contenu de l'action est explicite. Aucune information d'identification cloud n'est stockée par MAKRIVO.
7. Les chaînes d'interface utilisateur appartiennent aux catalogues de langues 27 existants ; DE/EN/FR/IT restent les langues minimales de la documentation révisée. Les lacunes de traduction doivent être divulguées plutôt que déclarées complètes.
8. Une marque de clavier M sarcelle reconnaissable remplace l'icône générique de la barre d'état Windows. Le mode pause comporte un badge de pause orange. La marque de l'exécutable/de la fenêtre utilise la même marque. L'info-bulle inclut l'état et le profil.

## Vérification

Les vérifications principales couvrent la rétrocompatibilité, les instantanés, l'annulation, la synchronisation des gestes, les branches/erreurs de flux de travail, le filtrage des enregistreurs et les conflits de synchronisation. Les vérifications Windows couvrent l'énumération d'affichage en lecture seule, les événements de version du SDK et l'analyse Raw Input. Les contrôles de fumée WPF couvrent de nouveaux points d'entrée et de nouvelles ressources sans modifier la luminosité réelle du moniteur, ni taper dans une autre application ni écraser les profils utilisateur. Le déploiement conserve une sauvegarde et une configuration G1/G2.
