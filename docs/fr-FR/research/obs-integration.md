<!-- generated-doc locale=fr-FR source=docs/research/obs-integration.md sha256=a4e6ca4368b28daa73ffbdda4fb817863e3fa15d52da00d26bbd32ce1f05f9b7 -->

> Traduction assistée par machine. La structure, les exemples de code et les liens sont vérifiés ; cela ne constitue ni une révision par un locuteur natif ni une validation juridique. Les rapports historiques conservent leur date et leur portée d'origine. [Source originale](../../research/obs-integration.md)

# OBS Intégration Studio

[English (US)](obs-integration.md) | [Deutsch](obs-integration.md) | [Français](obs-integration.md) | [Italiano](obs-integration.md)

Aivana utilise le OBS Protocole Studio obs-websocket v5 sur ClientWebSocket ; il n'installe pas de tiers OBS bibliothèque cliente. Le protocole s'ouvre avec le serveur Hello (opcode 0), répond avec Identifier (opcode 1), attend Identifié (opcode 2), envoie une requête sur liste blanche (opcode 6), puis correspond à sa RequestResponse (opcode 7) par ID de demande. L'authentification par mot de passe suit OBSest documenté en deux étapes SHA-256 calcul du défi.

Activez **Tools → WebSocket Paramètres du serveur** dans OBS. Le point de terminaison par défaut est **ws://127.0.0.1:4455**. Définissez le champ **OBS-WebSocket-Adresse** de l'action pour un autre point de terminaison. Un point de terminaison distant doit utiliser wss:// ; ws:// non chiffré est accepté uniquement pour une adresse de bouclage. Le point de terminaison peut également inclure un chemin ou un port personnalisé. Aivana rejette les informations d'identification d'URL intégrées.

Lorsque l'authentification est activée pour OBS, enregistrez son mot de passe à partir de l'éditeur d'actions OBS. Aivana le stocke dans le gestionnaire d'informations d'identification de l'utilisateur Windows actuel en tant qu'informations d'identification génériques nommées **Aivana.OBS:<point de terminaison normalisé>**. Il n'est jamais écrit dans un fichier d'action ou de paramètres. Pour les déploiements gérés, **AIVANA_OBS_PASSWORD** reste disponible comme solution de secours lorsque le point de terminaison n'a pas de mot de passe enregistré. OBS peut rester non authentifié, auquel cas ni l'un ni l'autre n'est nécessaire. Évitez les arguments de ligne de commande pour le mot de passe, car les lignes de commande du processus peuvent être inspectées.

Le catalogue du contrôleur et la configuration prennent en charge **StartRecord**, **StopRecord**, **ToggleRecord**, **PauseRecord**, **ResumeRecord**, **StartStream**, **StopStream**, **ToggleStream**, **SetCurrentProgramScene** et **ToggleInputMute**. Les huit premiers n’acceptent aucun argument. SetCurrentProgramScene utilise le nom de scène de l'éditeur ; ToggleInputMute utilise son nom d'entrée. Le client de protocole accepte la même liste verte.

Chaque action obtient une nouvelle connexion avec une limite de 10-seconde et aucune nouvelle tentative automatique. Ceci est important pour les commandes bascule : une fois la demande envoyée, un délai d'attente ou une connexion interrompue peut laisser le résultat inconnu, et une nouvelle tentative pourrait à nouveau basculer l'état. Les échecs de requête OBS sont renvoyés avec leur code d'état et (le cas échéant) le commentaire du serveur.

Références du protocole :

- [OBS WebSocket spécification du protocole](https://github.com/obsproject/obs-websocket/blob/master/docs/generated/protocol.md)
- [OBS WebSocket formule d'authentification et code de message](https://github.com/obsproject/obs-websocket/blob/master/docs/generated/protocol.md#creating-an-authentication-string)
- [OBS WebSocket la mise en œuvre des demandes d'entrée de médias et les valeurs enum acceptées](https://github.com/obsproject/obs-websocket/blob/master/src/requesthandler/RequestHandler_MediaInputs.cpp)

Les notes de protocole et les noms d'opérations ont été vérifiés par rapport à la documentation du projet OBS sur 2026-10-08.
