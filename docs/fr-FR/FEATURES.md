<!-- generated-doc locale=fr-FR source=docs/FEATURES.md sha256=ea712f39457ce69f772f09a1294201705a5f36a92a8bf15f356bd80132c3e723 -->

> Traduction assistée par machine. La structure, les exemples de code et les liens sont vérifiés ; cela ne constitue ni une révision par un locuteur natif ni une validation juridique. Les rapports historiques conservent leur date et leur portée d'origine. [Source originale](../FEATURES.md)

# MAKRIVO – Aperçu des fonctionnalités

Fabricant : **Aivana**. Statut : **12. Octobre 2026**. Il s'agit de l'état de développement, pas d'une version commerciale ou matérielle.

## Contrôle Windows

- Tuiles, raccourcis clavier globaux, palette de commandes, favoris et profils dépendants de l'application.
- Macros en plusieurs étapes, répétition, pause, annulation et aperçu.
- Ouvrir des programmes, des fichiers et des sites Web ; Utilisez le texte, le presse-papiers et les raccourcis clavier.
- Organisez les fenêtres, verrouillez Windows, contrôlez les sorties audio et les microphones et modifiez la luminosité de l'écran via les interfaces prises en charge.
- OBS-WebSocket-v5-Anbindung pour les scènes, l'enregistrement, le streaming et l'audio.
- Modèles, Workplace Assistant, découverte de périphériques et superposition de clavier.
- Importation et exportation de configurations de profils ; Revue de conception et détection de conflits.
- Paramètres enregistrés localement ; aucun compte requis pour une utilisation quotidienne.

## Fonctionnalités avancées dans le code source actuel

| zone | Gamme de fonctions | Limites |
| --- | --- | --- |
| Vérification des résultats | Historique des pré-inspections, des post-inspections et des tests | Confirmez uniquement les effets vérifiables ; Une pression réussie sur une touche ne prouve pas l'effet recherché. |
| Assistante en milieu de travail | Préparer des profils et des processus à partir de programmes, fichiers, URL et autres actions sélectionnés | Aperçu et enregistrement requis ; L'installation ne fait rien. |
| Enregistrement de macros et flux de travail | Enregistrement, conditions, gestes et synchronisation de profil local | Le comportement et les limites sont documentés dans les rapports d’implémentation et de test. |
| Automatisation de l'interface utilisateur | Sélectionnez des cibles, vérifiez les sélecteurs, enregistrez les actions et les séquences de clics limitées | Travailleur séparé ; Les cibles ambiguës, désactivées et de mot de passe sont rejetées ; pas d'enregistrement général de saisie de texte. |
| Rallonges | Inspecter, importer, activer en fonction de la confiance explicite, exporter et restaurer les packages | L'isolation des processus n'est pas un bac à sable ; pas un marché public de plugins. |
| Panneau mobile | Panneau HTTPS local, appairage, profils sélectionnés et historique d'état/d'audit | L'acceptation du réseau local réel pour téléphone/tablette est toujours ouverte ; pas de tableau de bord à capteurs universels. |
| Adaptateur du fabricant | Adaptateur analogique Corsair, Logitech-G-SDK, Razer/SteelSeries-RGB et Wooting | Dépend du modèle/SDK ; nouveaux adaptateurs non entièrement validés avec du matériel réel. Aucune nouvelle DLL SDK propriétaire n'est fournie. |
| Mappage du clavier physique | Reconnaître les clés standard en fonction de l'appareil et scanner le code | Raw Input ne supprime pas l'entrée Windows normale. |
| Assistant de réunion local IA | Prototype en option pour Ollama, LM Studio, llama.cpp et ONNX Runtime GenAI Le processeur produit des conceptions limitées à partir de ressources explicitement sélectionnées. | Désactivé par défaut ; l'exécution du processeur natif a été testée avec un modèle aléatoire, mais aucune qualité de planification ni aucun gain de temps n'ont été prouvés ; aucun poids de modèle inclus. |

## Pas encore disponible en tant que fonctionnalité de vente terminée

- **Clavier secondaire/pavé macro exclusif : ** Il manque un pilote de filtre signé, sous licence et testé par le matériel. Les frappes normales peuvent toujours atteindre Windows.
- Acceptation matérielle complète de tous les adaptateurs du fabricant ainsi que de véritables tests LAN d'appareils mobiles.
- Véritables modifications mixtes moniteur/DPI et approbation technique de toutes les nouvelles traductions. PerMonitorV2 et 150/200-Prozent-Layouts simulés ont été vérifiés ; Les tests d'affichage réels restent séparés. L'interface propose les langues 27 ; Les nouveaux textes détaillés n'ont pas encore été complètement approuvés linguistiquement.
- Programme d'installation au détail signé, CLUF final, droits de distribution SDK vérifiés et intégration de licence d'achat/paiement productive.

## Détails et preuves

- [Documents d'utilisation](README.md)
- [Extensions: fonctionnalités, tests et limites ouvertes](testing/2026-10-12-six-extensions.md)
- [Assistant d'IA local: prototype et niveau d'essai](testing/2026-10-12-local-meeting-assistant.md)
- [Temps d'intelligence locale et exigences de modèle](testing/2026-10-12-local-ai-runtimes.md)
- [DPI et évaluation](testing/2026-10-12-dpi-scaling.md)
- [Vérification de licence](licensing/2026-10-12-commercial-license-audit.md) – enregistrement historique; le nouveau script de publication complète les indications de fonctionnement. Le paquet final reste à vérifier séparément.
- [Plateformes de vente](licensing/makrivo-vertriebsplattformen-2026-10-12.html) – État de comparaison; Stripe est en outre une plate-forme de paiement possible, voir les conditions actuelles du fournisseur.

## Générer un package de développement complet

```powershell
./tools/publish-complete.ps1 -SelfContained
```

Le package comprend une application de bureau, un travailleur d'automatisation de l'interface utilisateur, un hôte mobile, des fichiers de navigateur, un exemple de plug-in et des notes de licence d'exécution. Les artefacts de build ne sont pas archivés dans Git. Une version du code source de GitHub n'est pas encore un programme d'installation de Windows publié.
