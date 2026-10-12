<!-- generated-doc locale=pt-PT source=docs/research/obs-integration.md sha256=a4e6ca4368b28daa73ffbdda4fb817863e3fa15d52da00d26bbd32ce1f05f9b7 -->

> Tradução assistida por máquina. A estrutura, os exemplos de código e as ligações são verificados; isto não constitui revisão por um falante nativo nem aprovação jurídica. Os relatórios históricos mantêm as datas e o âmbito originais. [Fonte original](../../research/obs-integration.md)

# OBS Integração com estúdio

[English (US)](obs-integration.md) | [Deutsch](obs-integration.md) | [Français](obs-integration.md) | [Italiano](obs-integration.md)

Aivana usa o protocolo OBS Studio obs-websocket v5 sobre ClientWebSocket; ele não instala uma biblioteca cliente OBS de terceiros. O protocolo abre com o servidor Hello (opcode 0), responde com Identificar (opcode 1), espera por Identified (opcode 2), envia uma solicitação na lista de permissões (opcode 6) e, em seguida, combina seu RequestResponse (opcode 7) por ID de solicitação. A autenticação por senha segue o cálculo de desafio SHA-256 documentado em dois estágios do OBS.

Habilite **Ferramentas → WebSocket Configurações do servidor** em OBS. O terminal padrão é **ws://127.0.0.1:4455**. Defina o campo **OBS-WebSocket-Adresse** para outro endpoint. Um terminal remoto deve usar wss://; ws:// não criptografado é aceito apenas para um endereço de loopback. O endpoint também pode incluir um caminho ou porta personalizada. Aivana rejeita credenciais de URL incorporadas.

Quando OBS estiver com a autenticação habilitada, salve sua senha no editor de ações OBS. Aivana a armazena no Credential Manager do usuário Windows atual como uma credencial genérica chamada **Aivana.OBS:<ponto final normalizado>**. Nunca é gravado em um arquivo de ação ou configurações. Para implantações gerenciadas, **AIVANA_OBS_PASSWORD** permanece disponível como substituto quando o terminal não tem senha salva. OBS pode permanecer não autenticado e, nesse caso, nenhum dos dois é necessário. Evite argumentos de linha de comando para a senha porque as linhas de comando do processo podem ser inspecionadas.

O catálogo do controlador e suporte de configuração **StartRecord**, **StopRecord**, **ToggleRecord**, **PauseRecord**, **ResumeRecord**, **StartStream**, **StopStream**, **ToggleStream**, **SetCurrentProgramScene** e **ToggleInputMute**. Os primeiros oito não aceitam argumentos. SetCurrentProgramScene usa o nome da cena do editor; ToggleInputMute usa seu nome de entrada. O cliente de protocolo aceita a mesma lista de permissões.

Cada ação obtém uma nova conexão com um limite de segundo 10 e nenhuma nova tentativa automática. Isso é importante para comandos de alternância: após o envio da solicitação, um tempo limite ou queda de conexão pode deixar o resultado desconhecido e uma nova tentativa pode alterar o estado novamente. As falhas de solicitação OBS são retornadas com seu código de status e (quando fornecido) comentário do servidor.

Referências de protocolo:

- [OBS WebSocket especificação de protocolo v5](https://github.com/obsproject/obs-websocket/blob/master/docs/generated/protocol.md)
- [OBS WebSocket fórmula de autenticação e código de mensagem](https://github.com/obsproject/obs-websocket/blob/master/docs/generated/protocol.md#creating-an-authentication-string)
- [OBS WebSocket implementação de pedidos de entrada de mídia e valores de ação aceitos](https://github.com/obsproject/obs-websocket/blob/master/src/requesthandler/RequestHandler_MediaInputs.cpp)

As notas do protocolo e os nomes das operações foram verificados na documentação do projeto OBS em 2026-10-08.
