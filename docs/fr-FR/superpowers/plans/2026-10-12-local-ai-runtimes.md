<!-- generated-doc locale=fr-FR source=docs/superpowers/plans/2026-10-12-local-ai-runtimes.md sha256=f24c6b3bce4d0b4dd9d42732c363941a36644f90cf8d664d61ea0c60eb950079 -->

> Traduction assistée par machine. La structure, les exemples de code et les liens sont vérifiés ; cela ne constitue ni une révision par un locuteur natif ni une validation juridique. Les rapports historiques conservent leur date et leur portée d'origine. [Source originale](../../../superpowers/plans/2026-10-12-local-ai-runtimes.md)

# Durées d'exécution de l'IA locale et vérifications restantes

Objectif : connecter réellement des environnements d'exécution d'IA locaux supplémentaires, recevoir des critiques de conception et rendre visibles les versions ouvertes. L'utilisateur autorise la mise en œuvre sans questions. Les modifications existantes sont conservées.

1. Transport en boucle et tests d'inspection des paquets avant la mise en œuvre ONNX ; Vérifiez les adresses réseau, les limites de sortie, les abandons et les ressources inconnues.
2. Messages du planificateur partagé ; Ollama reçu, LM Studio (127.0.0.1:1234) et llama.cpp (127.0.0.1:8080) ajoutés. Pas d'URL cloud ni de changement automatique de fournisseur. Les serveurs locaux peuvent se transmettre eux-mêmes ; Leur exploitation reste sous la responsabilité de l'exploitant.
3. ONNX Runtime GenAI CPU dans son propre processus avec délai d'attente, protocole JSON limité et arrêt du processus. Sélectionnez explicitement le dossier modèle ; aucun modèle n'est téléchargé automatiquement. Uniquement les modèles de texte compatibles GenAI, pas de réseaux .onnx arbitraires.
4. Sélection WPF pour l'exécution, le modèle, le dossier ONNX ; tous offrent la même conception rigoureusement testée.
5. Banc de test avec des limites claires pour le matériel, le DPI, le panneau mobile et le macro pad exclusif. Les contrôles physiques et un pilote signé/sous licence ne peuvent pas être remplacés par un logiciel ici.
6. Version de version, tests de base/Windows, fumée WPF, tests de travailleurs/packages et examen indépendant. Créer un nouveau package complet ; Ne signalez la qualité du modèle telle que testée que si le modèle existe réellement.

Résultat : quatre runtimes implémentés, ONNX testé nativement séparément, choix du fournisseur et banc de test dans le pack WPF smoke réussi. Core/Windows suites vertes. Examinez la recherche de la limite Unicode avec la constante commune et la régression corrigée. Les approbations de modifications matérielles/mobiles/DPI réelles, la publication de pilotes, les traductions techniques complètes et la qualité des instructions en direct restent un travail ouvert et explicite ; pas publié comme complet.

Continuation: Implémentation de modèles Jinja séparés et nommés/defaits; Tests de type/grande/faux-back ont été effectués. 27 Catalogue complète. réel modèle Instruct téléchargé séparément et SHA256 Vérifié: la référence CPU native fonctionne, mais 0/4 Les cas de planification sont conformes au contrat JSON; aucune action n'est effectuée. révision indépendante: nul-Fallback corrigé et testé, après quoi aucune découverte concrète. WindowsTests de mise en place, Release-Build 0 Avertissements et erreurs.
