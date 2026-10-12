<!-- generated-doc locale=fr-FR source=docs/testing/2026-10-12-responsive-layout.md sha256=8b3710a27f806575e88b4d9a0c4e80de4f26539ea1b5826cfe291764228b4ca6 -->

> Traduction assistée par machine. La structure, les exemples de code et les liens sont vérifiés ; cela ne constitue ni une révision par un locuteur natif ni une validation juridique. Les rapports historiques conservent leur date et leur portée d'origine. [Source originale](../../testing/2026-10-12-responsive-layout.md)

# Interface WPF réactive

Les pages principales sont basées sur la largeur de contenu utilisable et non sur une largeur de fenêtre fixe. Sous la largeur de contenu des DIP 900, le tableau de bord et l'éditeur sont l'un en dessous de l'autre. Sous les DIP 620, les ratios sont répartis sur deux colonnes. La barre latérale est réduite sous la largeur de la fenêtre 820 DIP. Taille minimale : 640 × 480 DIP.

Les volets de profil et de paramètres et les commandes du clavier changent de colonne en fonction de la largeur réelle de la grille. Les contrôles existants sont réorganisés au lieu de créer de nouvelles entrées. Les cartes d'action divisent la largeur de ligne disponible. Les barres d'outils se cassent. Le côté clavier est accessible verticalement, le clavier physique conserve ses proportions.

Test automatique WPF : les cinq pages principales à 640 × 480, 900 × 620 et 1440 × 900 ; Captures d'écran par page ; Changement de colonne dans les deux sens avec vérification du texte reçu. Les DIP sont des unités logiques WPF. La véritable mise à l'échelle Windows au pourcentage 150/200 et plusieurs moniteurs ne sont pas encore validées.

Banc de test : version de publication sans erreurs/avertissements. Test de fumée complet WPF réussi avec toutes les tailles, changements de colonnes, dispositions de clavier et vérifications de langue. git diff --check réussi.
