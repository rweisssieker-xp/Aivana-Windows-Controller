<!-- generated-doc locale=fr-FR source=docs/superpowers/specs/2026-10-08-keyboard-mapping.md sha256=fb766a2a2b4698ae3e1c3f63b50bb4f5c605da3dd56c61c05fcc0832b5cf8aeb -->

> Traduction assistée par machine. La structure, les exemples de code et les liens sont vérifiés ; cela ne constitue ni une révision par un locuteur natif ni une validation juridique. Les rapports historiques conservent leur date et leur portée d'origine. [Source originale](../../../superpowers/specs/2026-10-08-keyboard-mapping.md)

# Vue du clavier physique pour le mappage

[English (US)](2026-10-08-keyboard-mapping.md) | [Deutsch](2026-10-08-keyboard-mapping.md) | [Français](2026-10-08-keyboard-mapping.md) | [Italiano](2026-10-08-keyboard-mapping.md)

## Cible

La vue du clavier est destinée à afficher les positions clés réelles lors de l'attribution d'actions. Les lignes de même largeur ne suffisent pas : Entrée, Maj, Espace, Navigation, pavé numérique et commandes supplémentaires du fabricant diffèrent.

## Mise en œuvre

- Catalogue avec identifiants physiques séparés, géométrie en unités clés, légendes imprimées et codes d'entrée Windows.
- ISO-DE et ANSI-US chacun en format complet et TKL ; en outre Corsair K100 RGB, qui a été reconnu comme modèle connecté via iCUE.
- Entrée ISO en forme de L, clavier numérique avec touches plus/entrée hautes et zéro large ; Navigation et flèches dans leurs propres positions.
- Sélectionnez explicitement le modèle/la mise en page. USB VID/PID et le nom du modèle ne prouvent pas la variante linguistique. Les profils génériques et les profils modélisés sur la base des documents du fabricant sont clairement étiquetés différemment ; aucune échelle millimétrique ni garantie de compatibilité du micrologiciel.
- Mappez le scancode à la disposition d'entrée Windows active afin qu'un clic sur une touche physique n'occupe pas accidentellement une autre touche, telle que Y/Z lors du basculement entre les États-Unis et l'Allemagne.
- L'affectation ouvre un brouillon dans l'éditeur d'actions existant. La sauvegarde, la vérification des conflits et la suppression restent les mécanismes existants. Les clics ne déclenchent pas d'actions.
- Les touches G propriétaires, les touches de profil/verrouillage et la molette iCUE sont représentées, mais ne sont pas émises comme les touches de raccourci Windows normales sans un adaptateur d'entrée implémenté. Les raccourcis constructeur configurables restent une voie possible.
- Les affectations restent des raccourcis clavier globaux Windows ; une vue de périphérique n'implique pas la suppression spécifique au périphérique ou l'affectation exclusive d'un seul clavier physique.

## Preuve

[K100-Handbuch avec des éléments de commande](https://assets.corsair.com/image/upload/corsairmedia/sys_master/productcontent/AP_K100_RGB_Mechanical_QSG_AD.pdf), [Corsair Déclaration ANSI / ISO](https://www.corsair.com/au/en/explorer/gamer/keyboards/ansi-vs-iso-whats-the-best-keyboard-layout/).

## Examen

tests de géométrie, de variantes et de code d'entrée ; Builds et tests de base/Windows existants ; WPF fume pour l'affichage et la cartographie sans exécuter ni modifier la configuration utilisateur réelle. La recherche du fabricant documente les exigences supplémentaires en matière de modèle/SKU indépendamment du catalogue de présentation actuellement mis en œuvre.
