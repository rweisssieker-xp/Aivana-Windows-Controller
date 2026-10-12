<!-- generated-doc locale=fr-FR source=docs/testing/2026-10-12-github-publication.md sha256=ab4d40cd12047e2cdacad359a12febe16d5689a82f4f792b484b3d4e2430e02a -->

> Traduction assistée par machine. La structure, les exemples de code et les liens sont vérifiés ; cela ne constitue ni une révision par un locuteur natif ni une validation juridique. Les rapports historiques conservent leur date et leur portée d'origine. [Source originale](../../testing/2026-10-12-github-publication.md)

# GitHub version du statut de développement MAKRIVO

Statut : 12. Octobre 2026. Version du code source, pas une version commerciale signée.

- Version de la solution : réussie, aucun avertissement ni erreur.
- Examens de base : réussi.
- WPF test de fumée : réussi ; nouveau rapport de résultats dans le dossier de construction local.
- Première suite complète Windows : vérifications de la fenêtre de premier plan, de l'audio, de l'écran et de iCUE ; puis la réponse du travailleur UI Automation expire.
- Nouvelle tentative ciblée `--extensions-only` : réussie, y compris l'automatisation de l'interface utilisateur, l'ambiguïté, les cibles désactivées, l'exclusion de mot de passe, le processus de plug-in et un véritable hôte mobile HTTPS. Le délai d'attente d'origine n'a pas été masqué par un changement de délai d'attente ; la cause n’a pas encore été prouvée de manière concluante.
- Réexécution complète ultérieure de Windows : réussite, y compris la fenêtre de premier plan et toutes les vérifications d'extension. Une réexécution réussie n’exclut pas des délais d’attente sporadiques.
- Avant la publication, aucune clé privée typique ou clé productive Stripe/GitHub/AWS n'a été trouvée dans l'inventaire source audité. Ce contrôle à échantillon limité n’est pas un contrôle secret complet.
- Les artefacts de build et les données d’orchestration locale ne sont pas publiés.

La portée et les réductions restantes : [Liste des feutres](../FEATURES.md). Les rapports d'essai antérieurs maintiennent leur validité temporelle; ils ne sont pas une garantie pour tout matériel ou contexte de bureau.
