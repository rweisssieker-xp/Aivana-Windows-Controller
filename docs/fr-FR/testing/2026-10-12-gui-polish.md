<!-- generated-doc locale=fr-FR source=docs/testing/2026-10-12-gui-polish.md sha256=ac39188358e51c68964421d3e855161d607b02a41da2b1a1fad70244f9feff97 -->

> Traduction assistée par machine. La structure, les exemples de code et les liens sont vérifiés ; cela ne constitue ni une révision par un locuteur natif ni une validation juridique. Les rapports historiques conservent leur date et leur portée d'origine. [Source originale](../../testing/2026-10-12-gui-polish.md)

# Révision de l'interface graphique de 12.10.2026

- Accès direct aux « Outils » dans la navigation ; trois groupes avec des actions déclarées pour la configuration, la création et les tests.
- Quatorze zones de dialogue existantes utilisent une classe de dialogue WPF commune avec les ressources de la fenêtre principale, une police et un positionnement cohérents chez le propriétaire.
- La taille de la boîte de dialogue est limitée à l'espace de travail Windows ; le contenu long reçoit des barres de défilement. Les paramètres et les pages de profil restent défilables même à faible hauteur.
- Le filtre d'action apparaît uniquement sur le tableau de bord. La sélection et la recherche de profils portent des noms explicites pour les technologies d'assistance. Les étiquettes des champs sont plus contrastées.
- Le test de fumée de dialogue comprend une présentation de l'outil et des boîtes de dialogue d'extension au format 560 × 440 et en taille normale ; Les enregistreurs et tests de langue existants ont été adaptés à la structure par défilement.

Limitations : Les fenêtres compactes ne remplacent pas les vrais tests DPI Windows avec le pourcentage 150/200. Les nouveaux textes allemands n'ont pas encore été entièrement traduits pour toutes les langues 27. Aucune nouvelle validation matérielle et aucune déclaration sur la convivialité mesurée.

Banc de test : version de publication sans avertissements/erreurs ; A réussi le test de fumée complet WPF, y compris les boîtes de dialogue d'outils et d'extensions compacts et les contrôles de changement de langue. git diff --check réussi.
