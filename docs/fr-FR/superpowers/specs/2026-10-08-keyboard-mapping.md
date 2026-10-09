# Vue de clavier physique pour l’affectation

[English (en-US)](../../../en-US/superpowers/specs/2026-10-08-keyboard-mapping.md) | [Deutsch (de-DE)](../../../de-DE/superpowers/specs/2026-10-08-keyboard-mapping.md) | [Français (fr-FR)](2026-10-08-keyboard-mapping.md) | [Italiano (it-IT)](../../../it-IT/superpowers/specs/2026-10-08-keyboard-mapping.md)

## Objectif

La vue de clavier doit afficher les positions réelles des touches lors de l’affectation d’actions. Des rangées de largeur uniforme ne suffisent pas : Entrée, Maj, Espace, navigation, pavé numérique et commandes supplémentaires du fabricant ont des formes différentes.

## Mise en œuvre

- Catalogue avec identifiants physiques distincts, géométrie exprimée en unités de touche, légendes imprimées et codes d’entrée Windows.
- ISO-DE et ANSI-US en format complet et TKL ; ajout du Corsair K100 RGB, reconnu comme modèle connecté via iCUE.
- Touche Entrée ISO en forme de L, pavé numérique avec touches Plus et Entrée hautes et touche zéro large ; touches de navigation et flèches à leurs positions propres.
- Sélection explicite du modèle/de la disposition. VID/PID USB et nom de modèle ne prouvent pas la variante de langue. Les profils génériques et ceux modélisés à partir de documents du fabricant portent des libellés distincts ; aucune garantie de dimensions au millimètre ni de compatibilité du micrologiciel.
- Associer le scancode à la disposition d’entrée Windows active pour qu’un clic sur une touche physique n’affecte pas par erreur une autre touche, par exemple Y/Z lors du passage de l’anglais américain à l’allemand.
- L’affectation ouvre un brouillon dans l’éditeur d’action existant. L’enregistrement, la vérification des conflits et l’abandon conservent les mécanismes existants. Les clics ne lancent aucune action.
- Les touches G propriétaires, les touches Profil/Verrouillage et la molette iCUE sont représentées, mais n’émettent pas de raccourcis Windows ordinaires sans adaptateur d’entrée implémenté. Les raccourcis configurables du fabricant restent une possibilité.
- Les affectations restent des raccourcis Windows globaux ; une vue par périphérique ne signifie ni suppression propre au périphérique, ni affectation exclusive à un clavier physique donné.

## Références

[Guide K100 présentant les commandes](https://assets.corsair.com/image/upload/corsairmedia/sys_master/productcontent/AP_K100_RGB_Mechanical_QSG_AD.pdf), [explication Corsair des dispositions ANSI/ISO](https://www.corsair.com/au/en/explorer/gamer/keyboards/ansi-vs-iso-whats-the-best-keyboard-layout/).

## Vérification

Tests de géométrie, de variantes et de codes d’entrée ; compilations et tests Core/Windows existants ; test rapide WPF du rendu et des affectations sans exécution ni modification de la configuration réelle de l’utilisateur. La recherche fabricant documente d’autres exigences liées aux modèles/SKU, indépendamment du catalogue de dispositions actuellement implémenté.
