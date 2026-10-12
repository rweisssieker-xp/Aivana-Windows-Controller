<!-- generated-doc locale=fr-FR source=docs/testing/2026-10-12-local-ai-runtimes.md sha256=3dfbb80761aad758ea6a9307999ff530dfdd4a0cf685804e15052508cf107fd7 -->

> Traduction assistée par machine. La structure, les exemples de code et les liens sont vérifiés ; cela ne constitue ni une révision par un locuteur natif ni une validation juridique. Les rapports historiques conservent leur date et leur portée d'origine. [Source originale](../../testing/2026-10-12-local-ai-runtimes.md)

# Runtimes d'IA locaux

## Service

Outils → Ouvrez l'assistant de réunion AI, activez-le explicitement et sélectionnez le runtime AI. Libérez des ressources, formulez un objectif, créez un brouillon et vérifiez-le dans l'éditeur. Aucun fournisseur n'exécute la conception automatiquement. Il n’y a pas de changement automatique de fournisseur.

| Durée | Exigence | connexion |
|---|---|---|
| Ollama | modèle installé localement | 127.0.0.1:11434 |
| LM Studio | exécution d'un serveur local avec modèle chargé localement et prise en charge du schéma JSON | 127.0.0.1:1234 |
| llama.cpp | exécution d'un serveur Lama avec modèle local et prise en charge du schéma JSON | 127.0.0.1:8080 |
| ONNX Runtime GenAI CPU | dossier de modèle de texte GenAI local compatible et fiable | processus groupé séparé, pas de HTTP |

Les clients HTTP n'utilisent pas de proxy et ne suivent pas les redirections. Les serveurs locaux peuvent eux-mêmes adresser des services supplémentaires ; En particulier, LM Link/Remote Routing ne peut pas être utilisé pour le traitement local. MAKRIVO ne peut pas détecter cela de manière fiable via l'interface compatible. Les métadonnées distantes Ollama et les noms de cloud sont également rejetés. Les serveurs locaux authentifiés ne peuvent pas être configurés dans cette version.

## ONNX

Dans le dialogue genai_config.json. le dossier doit contenir tokenizer.json et un modèle de chat. chat_template est soutenu en tant que chaîne de caractères, un modèle par défaut nommé (Array ou Dictionnaire) ainsi qu'un chat_template.jinja séparé, si aucun modèle en ligne n'est disponible ou null. 65536 Les fichiers .onnx populaires, les modèles de vision/embedding ou les exportations purement GPU/NPU ne sont pas des modèles de rencontre.

Regroupé : Microsoft.ML.OnnxRuntimeGenAI 0.17.1, CPU. La configuration du fournisseur est réinitialisée sur le processeur. Aucun téléchargement et aucun poids de modèle dans le package. Aucune installation Python requise. Utilisez uniquement des packages de modèles fiables ; le processus distinct est une limite de crash/abandon, pas un bac à sable de sécurité.

Limites : 90 secondes, chargement du modèle compris, jetons d'invite 3072 maximum et jetons générés par 1024, limite de contexte de modèle supplémentaire ; JSON et ID de ressources connus rigoureusement vérifiés. Les réponses incorrectes/tronquées seront rejetées. Fermer/Annuler met fin au processus de travail démarré. Aucune commande de modèle libre et aucun chemin de programme/fichier dans la liste de ressources distincte de l'invite ; le texte cible rédigé par l'utilisateur peut lui-même contenir de telles données.

## Vérification

- Suite de base, tests de transport en boucle avec réponses simulées LM-Studio/llama.cpp/Ollama, vérification de schéma/abandon/ressource.
- Régression du protocole Unicode avec des cibles longues maximales et des noms de ressources 24.
- Véritable inférence CPU native avec tiny-random-gpt2-fp32-Testgraph officiel de Microsoft/onnxruntime-genai, commit 5c19002edce50beaa3a5c7eeefd8fe5269ee8d19. Modèle de test ajouté et tokenizer d'octets adapté au petit graphe aléatoire. Ce modèle aléatoire ne vérifie pas la qualité et n'est pas en version.
- Test d'exécution natif reproductible : tools/check-onnx-runtime.ps1 -ModelDirectory <Dossier de modèle GenAI>.
- WPF-Smoke vérifie la sélection des quatre fournisseurs et du banc de test ; La sélection seule ne génère pas de demande de modèle.

## Actions réelles restantes

Des tests de remplacement du matériel/smartphone/tablette/moniteur sont toujours nécessaires. La suppression exclusive du clavier reste bloquée sans pilote de filtre signé, sous licence et vérifié. Non remplacé par des pilotes factices ou Raw Input. Le changement de focus peut toujours être bloqué par Windows ; l'erreur mentionne désormais l'activation manuelle de la fenêtre cible. Des traductions ont été ajoutées aux commandes centrales d'IA dans tous les catalogues 27 ; Les nouveaux textes détaillés et l'approbation des traductions techniques sont encore ouverts. La qualité en direct avec un modèle d'instruction approprié, un fonctionnement continu et des gains de temps mesurés ne sont pas prouvés par des modèles aléatoires/faux tests HTTP.

## Sources

- https://onnxruntime.ai/docs/genai/api/csharp.html
- https://www.nuget.org/packages/Microsoft.ML.OnnxRuntimeGenAI/0.17.1
- https://lmstudio.ai/docs/developer/openai-compat/chat-completions
- https://github.com/ggml-org/llama.cpp/blob/master/tools/server/README.md

Test final : version de version sans avertissements/erreurs ; Réussite du noyau complet et de la suite Windows, y compris les tests de montage de focus natif, HTTPS, UIA, plugin IPC et de transport AI compatibles. Nouvelle version du package-20261012-190618 : fumée WPF réussie (horloge 19:07), inférence CPU ONNX groupée avec graphique de test réussi. Examen indépendant : limite du protocole Unicode corrigée, aucune autre conclusion concrète. Aucune acceptation physique du fabricant/téléphone/moniteur n’en découle.

## Suivant : vraie qualité et modèles

Plus de CPU avec allama-ai/SmolLM-360M-Instruct-onnx-int4, Révision 82a3d1a8a86c8053f2a859a90ed4d344fc0725b0 : inference autochtone. Modellgewichte-SHA256: 48e9c4ef12f7f995ca11fc7020b39990aa27bc67cc0fe1160676a000a04429.

Quatre tests réels de planification (allemand/anglais, document, programme, écran de microfon explicite): **0/4 Plans valides**; chaque JsonException en cas de réponse stricte. 63501, 6376, 72118 et 6774 ms. Aucune action n'est effectuée. Ce petit modèle ne répond pas au contrat de planification et ne sera pas recommandé comme approprié. Rapport: artifacts/local-ai-evaluation/smollm360-results.json. Windows-Tests --evaluate-onnx <modèle ordonnateur> <cours de rapport>. La fonction de temps de fonctionnement ne prouve pas la qualité du modèle.

Les outils de navigation et les rapports d'essai ont été ajoutés à tous 27 Les catalogues linguistiques sont traduits. les textes longs et les images linguistiques restent ouverts. la régression des modèles évalue également les types de JSON faux, les modèles vides/multi-duits/sur-longs et null avec le Jinja-Fallback.

Pacte actualisé : artifacts/release-20261012-201134Package WPF- La poussière à 12.10.2026 à 20:12 survivant; la référence de CPU native avec le modèle de test de l'instructeur séparé dans le travailleur lié. Windows-Testé par KI; Construire sans alertes/erreurs. 0/4.
