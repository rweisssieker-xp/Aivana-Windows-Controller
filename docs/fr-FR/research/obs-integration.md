# Intégration à OBS Studio

[English (en-US)](../../en-US/research/obs-integration.md) | [Deutsch (de-DE)](../../de-DE/research/obs-integration.md) | [Français (fr-FR)](obs-integration.md) | [Italiano (it-IT)](../../it-IT/research/obs-integration.md)

Aivana utilise le protocole obs-websocket v5 d’OBS Studio via ClientWebSocket ; elle n’installe aucune bibliothèque cliente OBS tierce. Le protocole commence par le message Hello du serveur (opcode 0), répond avec Identify (opcode 1), attend Identified (opcode 2), envoie une requête autorisée (opcode 6), puis associe sa RequestResponse (opcode 7) grâce à l’identifiant de requête. L’authentification par mot de passe suit le calcul de défi SHA-256 en deux étapes documenté par OBS.

Dans OBS, activez **Outils → Paramètres du serveur WebSocket**. Le point de terminaison par défaut est **ws://127.0.0.1:4455**. Pour un autre point de terminaison, renseignez le champ **Adresse OBS-WebSocket** de l’action. Un point de terminaison distant doit utiliser wss:// ; ws:// sans chiffrement n’est accepté que pour une adresse de bouclage. Le point de terminaison peut également inclure un chemin ou un port personnalisé. Aivana refuse les identifiants intégrés à l’URL.

Lorsque l’authentification OBS est activée, enregistrez le mot de passe dans l’éditeur d’action OBS. Aivana le conserve dans le Gestionnaire d’identifiants du compte Windows actuel sous la forme d’un identifiant générique nommé **Aivana.OBS:<point de terminaison normalisé>**. Il n’est jamais écrit dans un fichier d’action ou de paramètres. Pour les déploiements gérés, **AIVANA_OBS_PASSWORD** reste disponible comme solution de repli si aucun mot de passe n’est enregistré pour le point de terminaison. OBS peut rester sans authentification ; dans ce cas, aucun mot de passe n’est nécessaire. Évitez de passer le mot de passe en argument de ligne de commande, car les lignes de commande des processus peuvent être consultées.

Le catalogue et la configuration du contrôleur prennent en charge **StartRecord**, **StopRecord**, **ToggleRecord**, **PauseRecord**, **ResumeRecord**, **StartStream**, **StopStream**, **ToggleStream**, **SetCurrentProgramScene** et **ToggleInputMute**. Les huit premières commandes ne prennent aucun argument. SetCurrentProgramScene utilise le nom de scène saisi dans l’éditeur ; ToggleInputMute utilise le nom de son entrée. Le client de protocole applique la même liste d’autorisations.

Chaque action ouvre une nouvelle connexion avec une limite de 10 secondes et sans nouvelle tentative automatique. C’est important pour les commandes de bascule : après l’envoi de la requête, une expiration ou une déconnexion peut laisser le résultat inconnu, et une nouvelle tentative risque de basculer l’état une seconde fois. Les échecs de requête OBS sont renvoyés avec leur code d’état et, s’il est fourni, le commentaire du serveur.

Références du protocole :

- [Spécification du protocole OBS WebSocket v5](https://github.com/obsproject/obs-websocket/blob/master/docs/generated/protocol.md)
- [Formule d’authentification OBS WebSocket et opcodes des messages](https://github.com/obsproject/obs-websocket/blob/master/docs/generated/protocol.md#creating-an-authentication-string)
- [Implémentation des requêtes OBS WebSocket pour les entrées média et valeurs d’énumération d’action acceptées](https://github.com/obsproject/obs-websocket/blob/master/src/requesthandler/RequestHandler_MediaInputs.cpp)

Les notes de protocole et les noms d’opération ont été vérifiés à partir de la documentation du projet OBS le 2026-10-08.
