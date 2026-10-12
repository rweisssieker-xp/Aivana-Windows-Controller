<!-- generated-doc locale=fr-FR source=docs/superpowers/plans/2026-10-12-six-extensions-progress.md sha256=aaa5db1c77f8f730220146a230868b9df95d87653b48708f49aa1418db56fa5d -->

> Traduction assistée par machine. La structure, les exemples de code et les liens sont vérifiés ; cela ne constitue ni une révision par un locuteur natif ni une validation juridique. Les rapports historiques conservent leur date et leur portée d'origine. [Source originale](../../../superpowers/plans/2026-10-12-six-extensions-progress.md)

# Grand livre d'exécution : 2026-10-12-six-extensions
L'utilisateur a autorisé l'implémentation native sans autres questions.
Règle : utiliser la nouvelle branche codex/six-extensions dans la caisse actuelle pour préserver les fonctionnalités existantes non validées ; un nouvel arbre de travail omettrait les dépendances. Aucune modification d'installation ou de pilote pendant les tests.
Contrôle en amont : assistant de flux de vérification/UIA/plugin/vues des résultats mobiles ; les capacités du fabricant alimentent le mappage du clavier et le backend du pilote. Les valeurs d'énumération numérique existantes doivent rester stables.
Tâche 1 : en cours ; introduction d'un test de vérification d'échec.

Tâches 1–3 : vérification contrats/fournisseur/éditeur/historique et assistant mis en place ; les contrôles de base réussissent ; L'acceptation de l'interface utilisateur est toujours étendue.
Tâches 4–5 : implémentation d'un travailleur UIA isolé, d'un sélecteur et d'un enregistreur de clics examinés ; les tests de montage réels ont réussi l'exclusion d'invocation/ambiguïté/désactivation/mot de passe. Troncation du nom de processus en pointillés trouvée/corrigée.
Tâches 6–7 : révision/importation/désactivation du paquet, activation/exportation/désinstallation et implémentation de l'IPC exécutable isolé ; de vrais échantillons de passes IPC. Pas de marché public.
Tâches 8–9 : panneau HTTPS, couplage, informations d'identification hachées, clé TLS protégée et IPC sur liste blanche implémentés ; Vérification HTTP/mobile en attente.
Tâches 10–11 : codes de numérisation physiques, adaptateurs analogiques Logitech G SDK et Wooting ainsi que protocoles RVB implémentés ; aucun matériel de fournisseur compatible testé au-delà du Corsair existant.
Tâche 12 : la porte externe reste : le filtre exclusif Windows signé/sous licence de production 11 n'est pas disponible. Ne remplacez pas une fausse suppression.
Règle : créer des compagnons à l'aide des cibles MSBuild ; publiez tous les hôtes ensemble pour que UIA/mobile reste utilisable depuis l'application.
Décision : aucune DLL SDK redistribuable propriétaire regroupée sans droits vérifiés ; L’approbation explicite du SDK local hache le binaire sélectionné.

Révision finale : le nouveau réviseur /root/final_review a examiné l'arbre de travail actuel, le plan/les spécifications et les portes de publication. Aucune modification du réviseur.
Final : correction des commandes tardives du répartiteur mobile en appliquant l'annulation de la demande lors de l'envoi et de l'entrée de rappel.
Final : correction de l'état du terminal de vérification d'échec/annulation de l'exécuteur manquant ; régression d'échec d'exécution ROUGE → VERT ; état du délai d'attente avant le vol conservé.
Final : correction du processus de vérification du nom de base uniquement en comparant le chemin absolu du MainModule ou en signalant l'invérifiable.
Final : correction du cycle de vie du plugin avec contrôles de restauration/annulation de restauration ; nouvelle compilation de tests API ROUGE → VERT ; la désactivation annule les travailleurs actifs ; les dépendances manquantes rejettent l'invocation.
Final : correction du redémarrage du rythme cardiaque lors des sessions Razer/GameSense ayant échoué ; aucun test de périphérique RVB physique disponible.
Final : ajout de la validation des conflits de gestes/couches du fournisseur, test de compilation ROUGE→VERT.
Final : vrai HTTPS ROUGE→VERT : Schannel rejette les clés de serveur éphémères ; utilisez un conteneur de clé utilisateur temporaire et un PFX persistant protégé par DPAPI, sans PFX géré en texte clair.
Final : le budget d'action de l'UIA comprend une allocation de démarrage limitée pour les travailleurs ; un ouvrier est toujours tué en raison d'un rendement anormal.
Final : échec transitoire de lecture DDC reproduit (instantané vide puis même ID de moniteur stable) ; La nouvelle tentative de lecture limitée réussit le test d'énumération stable d'origine sans luminosité d'écriture.
Tâche 13 : avertissements/erreurs de version de build 0 ; suite de base verte ; Windows suite verte avec --skip-foreground-fixture explicite ; La suite complète d'origine présente un échec d'activation natif au premier plan, conservé. Le véritable UIA, le plugin IPC et HTTPS vérifient le vert. WPF vert fumé. Dispositions de téléphone et de tablette Edge/Playwright vertes avec simulation d'API, HTTPS réel séparément vert. Package autonome complet généré par tools/publish-complete.ps1 avec des notifications hosts/assets/example/runtime.
Règle : pas de fusion/validation de modifications utilisateur préexistantes mixtes. Laissez les modifications et les branches en place. Pas de menu de finition car l'utilisateur n'a posé aucune question et les critères de diffusion complets ne sont pas remplis.
Décision : tâche 12 incomplète : filtre de production signé/sous licence et tests matériels récupérables à deux claviers indisponibles. Aucun pilote installé. Aucune suppression exclusive revendiquée.
Décision : l'acceptation du matériel, l'acceptation réelle du réseau local mobile, le 150/200 à haute résolution et les nouvelles traductions complètes ne sont pas vérifiées ; divulgation détaillée docs/testing/2026-10-12-six-extensions.md. Demande globale non entièrement acceptée, malgré les ajouts de logiciels fonctionnels.
