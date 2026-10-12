<!-- generated-doc locale=fr-FR source=docs/testing/2026-10-12-local-meeting-assistant.md sha256=56631ca85dca5f785448d12586b0741628728925a649f6124f7aebffb19bb615 -->

> Traduction assistée par machine. La structure, les exemples de code et les liens sont vérifiés ; cela ne constitue ni une révision par un locuteur natif ni une validation juridique. Les rapports historiques conservent leur date et leur portée d'origine. [Source originale](../../testing/2026-10-12-local-meeting-assistant.md)

# Assistant de réunion local IA – prototype

Statut : 12. Octobre 2026. Hypothèse de l'agent de résultat ForgeMind, flux de réunion limité. Pas d’avantage concurrentiel avéré et pas de gain de temps mesuré sans de véritables tests comparatifs.

## Utilisation

1. Palette de commandes → **KI Assistant de réunion**. L'expérience est désactivée par défaut et est réinitialisée lors de l'importation de la configuration ou de l'exportation portable.
2. Activer l'assistant local. Ollama doit s'exécuter séparément sur `127.0.0.1:11434` ; un modèle local approprié doit déjà être installé. **Chargez les modèles locaux** et sélectionnez le modèle. MAKRIVO n'installe pas de modèles/services, ne contacte pas l'API OpenAI/Cloud et n'utilise pas de clés API. La configuration hors ligne et la fiabilité du service local Ollama appartiennent à l'utilisateur ; les modèles distants/cloud déclarés sont rejetés.
3. Entrez la destination et ajoutez explicitement des programmes/documents/sites Web. La cible, l'ID de ressource, le nom et le type sont transmis ; Les chemins cibles et le contenu des fichiers sélectionnés ne sont pas transmis en plus ; l'entrée de destination elle-même est envoyée textuellement. Les étiquettes du site Web incluent uniquement le schéma/l'hôte, le lien complet reste local.
4. **KI crée un brouillon**. Nombre maximum de secondes 90, jetons de sortie 1024, taille de réponse limitée. Les modifications de cible/modèle/ressource suppriment l'aperçu ; Annuler/Fermer met fin à la demande. Le modèle ne peut suggérer que des identifiants sélectionnés. Les identifiants inconnus, les commandes gratuites, les champs JSON supplémentaires et les types de documents exécutables sont rejetés.
5. Vérifiez l'aperçu et les points ouverts → **Vérifiez dans l'éditeur**. Le résultat est initialement un brouillon non enregistré. Enregistrez d'abord dans l'éditeur existant et exécutez-le explicitement si nécessaire. La validation normale des macros, la vérification pré/post, la pause et l'abandon restent responsables.

Pris en charge : démarrez des programmes sans arguments, ouvrez les PDF/DOCX/XLSX/PPTX/TXT/MD locaux explicitement sélectionnés, ouvrez les sites Web HTTP(S), coupez le microphone sur demande explicite. Pas de changement par défaut du casque, pas de messages, rendez-vous, téléchargements, scripts, clics gratuits ou saisie de texte. Les souhaits qui ne peuvent pas être représentés apparaissent comme des points ouverts si le modèle les reconnaît correctement ; L'utilisateur doit vérifier si les objectifs techniques ont été atteints. L’ouverture du document n’est pas automatiquement vérifiée techniquement.

## Expérience de comparaison

**Mesurer la configuration manuelle · Début / Fin** mesure le flux de comparaison manuel complet. Les hypothèses de l'assistant mesurent le temps écoulé entre l'ouverture de l'assistant et la première sauvegarde dans l'éditeur ; `generated`, en revanche, contient uniquement la durée de planification. Ne comparez pas deux durées inégales pour gagner du temps.

Les événements locaux se trouvent dans le dossier de configuration sous `Experiments/meeting-experiment.jsonl` : horodatage, tentative aléatoire/ID de tuile, événement, durée, correction oui/non. Pas de textes cibles, de noms de ressources, de chemins, de réponses modèles ou de contenu de document. Le fichier pivote à partir de 2 MiB, un fichier précédent est conservé. Les erreurs de mesure ne doivent pas interrompre les macros et la sauvegarde de la configuration.

Captures : ligne de base manuelle, génération/erreurs/abandon de modèle, suppression des modifications/fermetures d'aperçu, sauvegardes et corrections initiales dans l'éditeur, et états finaux réels du coureur pour les tuiles enregistrées à partir de celles-ci. `run_completed` signifie simplement que le coureur a terminé ; Les effets cibles confirmés sont répertoriés séparément dans le processus de test. Les répétitions de la même macro ne constituent pas de nouvelles tentatives de configuration indépendantes. L'expérience initiale peut continuer après le redémarrage, car l'ID de l'expérience reste sur la vignette enregistrée. Les tuiles dupliquées partagent cette origine ; regrouper par ID d’essai lors de l’évaluation.

Première expérience : 20 des installations de réunion comparables ou deux semaines avec des utilisateurs tests volontaires ; usage initialement interne. Comparez le temps de configuration manuelle au temps de configuration entièrement assistée ; Évaluez également le taux de correction/rejet, les erreurs des coureurs et les résultats de tests réels. S'il n'y a pas d'amélioration ou si les limites de protection sont dépassées, désactivez l'interrupteur. Aucune utilisation ou volonté de payer n'a été démontrée dans le référentiel. Pas de transmission automatique de télémétrie ni d'ajustement de prix.

## Banc d'essai

- Noyau : limites, types, identifiants inconnus/en double, exclusion de scripts, liens longs, configuration héritée facultative, opt-in d'importation/exportation, mesure de la sauvegarde des données, suppression unique et validation correcte.
- Transport avec un faux modèle : schéma JSON Ollama, destination de bouclage, aucun chemin de fichier dans l'invite, nom du cloud et alias distant rejetés, réponse corrompue/trop longue rejetée, abandon.
- WPF-Smoke : dialogue rendu ; La soumission du brouillon ne modifie pas les tuiles enregistrées, la révision/l'enregistrement préserve la provenance et la correction des enregistrements, la suppression n'enregistre rien.
- Examen indépendant : aperçu rejeté et liens longs trouvés et corrigés ; Test de régression ROUGE → VERT.
- Version de version sans avertissement/erreur, suite complète de base et fumée WPF réussie. La suite complète Windows existait initialement, y compris les changements de focus. Une autre exécution a encore échoué car le changement de premier plan a été refusé ; le test dépend du contexte et reste inchangé. Les tests Windows restants, y compris la réussite du transport du modèle, le test de réussite au premier plan étant explicitement omis.
- L'inspection des paquets a révélé un bug existant dans l'échange atomique de fichiers de configuration brièvement ouverts. Un verrou en lecture reproduisait la même erreur d’accès refusé ; la répétition limitée du seul échange atomique est cochée en ROUGE → VERT. Les erreurs d'accès permanentes restent visibles.
- Package autonome actuel `artifacts/release-20261012-184105` : WPF fume avec succès après correction de la mémoire, y compris la boîte de dialogue IA et l'acceptation/rejet du brouillon.
- Il ne s'agit pas d'un véritable test de modèle : le service local Ollama n'a pas répondu lors du test. Aucune déclaration sur la qualité du modèle, la latence, l'exhaustivité sémantique ou le gain de temps. Les limitations matérielles/pilotes/mise à l’échelle de la version précédente demeurent.

Les sources de protocoles : [Ollama Chat](https://docs.ollama.com/api/chat), [Outputs structurés](https://docs.ollama.com/capabilities/structured-outputs), [Définitions de type officielles, y compris les données de modèle à distance](https://github.com/ollama/ollama/blob/main/api/types.go).
