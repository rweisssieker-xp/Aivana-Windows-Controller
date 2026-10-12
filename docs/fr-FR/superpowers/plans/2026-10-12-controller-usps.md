<!-- generated-doc locale=fr-FR source=docs/superpowers/plans/2026-10-12-controller-usps.md sha256=b7af0d36209e04b458ce64a5db78ab7e552a84c5e9b450ea0bd97179a4eb7f68 -->

> Traduction assistée par machine. La structure, les exemples de code et les liens sont vérifiés ; cela ne constitue ni une révision par un locuteur natif ni une validation juridique. Les rapports historiques conservent leur date et leur portée d'origine. [Source originale](../../../superpowers/plans/2026-10-12-controller-usps.md)

# Plan de mise en œuvre de l'extension du contrôleur

**Objectif : ** Mettre en œuvre le moniteur, les gestes, le flux de travail, l'enregistrement, le diagnostic de l'appareil, l'échange de profils portables et les améliorations demandées.

**Architecture :** Étendre les couches Core/Windows/Desktop existantes. Les machines à états purs et la logique de configuration portable résident dans Core, les intégrations natives dans Windows et les éditeurs de brouillons modaux dans Desktop. Conservez les modifications existantes de l’espace de travail modifié et la configuration utilisateur déployée.

**Spec:** ../specs/2026-10-12-controller-usps-design.md

- [x] Surveiller le service, la persistance de la cible et l'état en direct asynchrone ; tester les identifiants indisponibles et l’annulation.
- [x] Machine à états gestuelle et couches maintenues ; tester les bords, répéter, réinitialiser et les liaisons immédiates existantes.
- [x] Conditions de flux de travail, branches transférées et politique d'erreur ; Test des instantanés et annulation.
- [x] Enregistrement de raccourci explicite avec révision du brouillon ; tester le filtrage et l’élimination des crochets.
- [x] Échange de profils de dossiers partagés avec vérifications de révision et aperçus ; tester les conflits et les mappages portables.
- [x] Clés spéciales et diagnostics standards génériques par appareil ; tester l'analyse, les mappages en double et le comportement de pause.
- [x] Ressources d'icônes de barre d'état/fenêtre/exécutable et apparence en pause ; vérifier les ressources dans l'exécutable publié.
- [x] Intégrer tous les points d'entrée de la palette/éditeur et le texte localisé ; exécutez les vérifications Core, Windows et WPF.
- [x] Publiez dans un répertoire intermédiaire distinct, conservez les paramètres utilisateur et redémarrez l'application mise à jour.
