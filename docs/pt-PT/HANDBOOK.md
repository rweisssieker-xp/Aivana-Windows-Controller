<!-- generated-doc locale=pt-PT source=docs/HANDBOOK.md sha256=8c6422e18d8939164f91da28603ed597f478f1a9b8bb41e14cbe4f6630bab3fc -->

> Tradução assistida por máquina. A estrutura, os exemplos de código e as ligações são verificados; isto não constitui revisão por um falante nativo nem aprovação jurídica. Os relatórios históricos mantêm as datas e o âmbito originais. [Fonte original](../HANDBOOK.md)

# MAKRIVO — Manual do utilizador

Fabricante: **Aivana**. Instantâneo da documentação: **2026-10-12**. Este manual descreve a implementação atual da árvore de trabalho. Integrações experimentais e funções dependentes de hardware são identificadas explicitamente. Os relatórios históricos descrevem seu instantâneo datado, não uma garantia para uma versão mais recente.

## Documentação e idiomas

Utilize o seletor de idioma da documentação para abrir o manual completo, o inventário de funcionalidades, a resolução de problemas, as notas de desenvolvimento, a investigação e os relatórios históricos de testes em qualquer um dos 27 idiomas da aplicação. As traduções geradas são assistidas por máquina e sujeitas a verificações estruturais; nem todas foram revistas por falantes nativos. Os identificadores técnicos e os exemplos executáveis mantêm-se inalterados. A fonte original continua disponível para comparação. A análise de licenciamento traduzida é informativa e não substitui os termos jurídicos originais de terceiros nem um EULA final.

## Instalação e primeiro lançamento

A plataforma inicial é Windows 11 x64. Mantenha juntos todos os ficheiros e subpastas da distribuição. Inicie `MAKRIVO.exe`. Uma cópia do repositório de código-fonte não é um instalador. Os pacotes autónomos incluem o ambiente .NET; os pacotes dependentes da framework requerem os ambientes .NET 10 Desktop e ASP.NET adequados. ONNX e os processos anfitriões auxiliares têm de ser incluídos para as respetivas funcionalidades.

1. Inicie MAKRIVO e escolha o idioma da interface em Configurações. Automático segue Windows. O layout do teclado é uma configuração separada.
2. Escolha um perfil e adicione um mosaico pelo catálogo de ações pesquisável. Dê-lhe um nome reconhecível e escolha as ações.
3. Revise as etapas e atribua um atalho, se necessário. Evite combinações já utilizadas pelo Windows ou pela sua aplicação alvo.
4. Salve o rascunho. As alterações não salvas não são a configuração salva.
5. Teste com uma ação inofensiva, como abrir um documento, e inspecione o feedback de execução.

Fechar a janela principal normalmente mantém a aplicação na área de notificação. Faça duplo clique no respetivo ícone para a reabrir ou use o menu do ícone para sair. O arranque com Windows é opcional e está inicialmente desativado. O controlo de pausa suspende as associações; o ícone mostra um indicador de pausa. Cancele as macros em execução pela interface, pela área de notificação ou pelo atalho de cancelamento configurado; o predefinido é `Ctrl+Alt+Escape`.

## Blocos, ações e paleta de comandos

Os mosaicos pertencem a perfis e podem conter vários passos ordenados. O editor permite guardar ou descartar alterações, pré-visualizar, anular/refazer e reordenar passos. O catálogo oferece predefinições para programas, documentos, URL, texto, área de transferência, atalhos, multimédia, áudio, janelas, ações do sistema e integrações suportadas. Uma predefinição que requer parâmetros abre-se como rascunho.

Abra a paleta com `Ctrl+K` quando MAKRIVO tiver o foco. Procure ações guardadas, predefinições, mudanças de perfil e ferramentas. As setas selecionam; Enter ativa; `Ctrl+Enter` edita um item suportado; Escape fecha. Alguns comandos prontos a usar são executados imediatamente, enquanto as ações configuradas exigem revisão. Pode configurar um atalho global separado para a paleta; as novas configurações usam `Ctrl+Shift+Space`.

As etapas de texto e área de transferência suportam `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` e `{{user}}`. A formatação da data é `yyyy-MM-dd`; a formatação da hora é `HH:mm`. Variáveis ​​desconhecidas permanecem inalteradas. A leitura da área de transferência ocorre somente quando solicitada. Uma ação da área de transferência grava texto sem colá-lo automaticamente.

As ações iniciadas pela interface podem dirigir-se à última aplicação externa. Se o destino desaparecer ou Windows bloquear a sua ativação em primeiro plano, ative-o manualmente e tente novamente. A entrega bem-sucedida das pressões de teclas não prova que a aplicação de destino tenha concluído a tarefa.

## Perfis e configuração do local de trabalho

Os perfis podem corresponder a um nome de processo e a parte do título de uma janela. Se ambos os critérios forem definidos, ambos têm de corresponder. A ordem dos perfis resolve correspondências concorrentes; o perfil predefinido é usado como alternativa. Ative explicitamente a seleção automática. Bloquear perfil impede mudanças automáticas na sessão atual.

Abra a configuração do ambiente de trabalho em Ferramentas ou na paleta. Selecione programas, documentos, sites, um destino de áudio/ecrã ou uma cena OBS. O assistente prepara perfis de trabalho, reunião, streaming, investigação ou projeto. Inspecione os passos gerados antes de guardar. A configuração não executa as ações preparadas.

## Macros, condições e gravação

As macros executam suas etapas em ordem e suportam esperas, repetições e cancelamentos. O limite geral de repetição é 100. Use a Visualização antes de executar ações digitadas em outro aplicativo ou alterar o estado do sistema.

A ação Esperar pela janela de destino aceita um processo, uma subcadeia do título e um tempo limite de 1 a 600000 ms. É necessário pelo menos um filtro. Uma janela visível correspondente tem de responder e passar para primeiro plano antes de os passos seguintes continuarem. Uma janela pronta não prova que um documento ou serviço tenha terminado de carregar.

O editor de fluxos de trabalho adiciona condições de existência de ficheiro, processo em execução e título de janela. Uma condição falsa ignora o passo e pode saltar outros passos seguintes. Inverta a condição para expressar um ramo alternativo. Escolha deliberadamente parar ou continuar em caso de erro; continuar pode deixar os passos posteriores sem as pré-condições esperadas.

O gravador de ações captura combinações de teclas de atalho e esperas, não a escrita normal. Suporta atalhos com teclas modificadoras e teclas de função. Escape para a gravação. Reveja a lista e adote-a como rascunho de mosaico por guardar; depois guarde explicitamente. A gravação de cliques na interface é uma ferramenta distinta descrita abaixo.

## Teclados, gestos e camadas

A página do teclado lista interfaces detectadas e layouts de referência. Um único teclado físico pode expor diversas interfaces. Selecione a linguagem e geometria corretas; o nome de um produto por si só não identifica o layout ISO ou ANSI.

Os atalhos globais comuns aplicam-se a todos os teclados. As associações Raw Input específicas de um dispositivo observam um dispositivo e um código de varrimento concretos, incluindo as teclas de função e multimédia suportadas. **Raw Input não suprime a entrada normal do Windows. A utilização exclusiva de um segundo teclado não está pronta para produção.** Continua a ser necessário um backend com controlador de filtro assinado, licenciado e testado; MAKRIVO não instala nenhum.

As associações suportadas de teclas especiais podem usar pressão imediata, toque, pressão prolongada ou duplo toque. As teclas de camada mantidas premidas selecionam temporariamente outro mapeamento e restauram a camada base ao serem libertadas; as teclas de seleção de camada não executam a sua própria macro. Os limiares de pressão prolongada vão de 150 a 5000 ms; os intervalos de duplo toque vão de 150 a 1000 ms. Estes mapeamentos são distintos dos atalhos globais comuns.

O assistente de teclas especiais deteta teclas compatíveis sem executar as ações atribuídas durante a captura. O diagnóstico explica dispositivos indisponíveis, associações em pausa, perfis que não correspondem e conflitos. A sobreposição mostra a disposição e as associações selecionadas; o atalho predefinido, a manter premido, é `Ctrl+Alt+F1`.

| Integração | Requisitos e limitações |
| --- | --- |
| Corsair | Executando iCUE, acesso SDK e modelo compatível. G1–G6 e funções RGB suportadas; Os controles proprietários Fn, wheel, lock e profile geralmente não são expostos pela API G-key. |
| Logitech G | As bibliotecas locais originais x64 G-key/LED SDK exigem aprovação explícita. Os eventos da tecla G não identificam exclusivamente vários teclados físicos idênticos. |
| Razer | Chroma RGB e entrada padrão; mapeie controles proprietários em Synapse quando necessário. |
| SteelSeries | GameSense RGB e entrada padrão; mapeie controles proprietários no GG quando necessário. |
| Wooting | SDK x64 local original compatível para valores e limites analógicos; a entrada HID normal permanece ativa. |
| Outros teclados | Teclas de atalho padrão ou ligações de código de leitura física suportadas. Os controles Fn e proprietários dependem do modelo e da configuração do fornecedor. |

A investigação sobre os fabricantes descreve capacidades documentadas, não compatibilidade implementada ou testada com todos os dispositivos indicados. Os novos adaptadores ainda precisam de validação com equipamento real. Não transfira binários de SDK de fontes não fiáveis; os direitos de distribuição do SDK são distintos da compatibilidade técnica.

## Áudio, monitores e OBS

Os passos de áudio definem o volume de 0 a 100 por cento e silenciam, reativam ou alternam o silêncio das saídas de áudio ou dos microfones suportados. Escolha um dispositivo específico ou o predefinido do Windows. Se o destino guardado não estiver disponível, é apresentado um erro em vez de controlar outro dispositivo sem aviso. Atualize os dispositivos após voltar a ligá-los. O controlo dos dispositivos de áudio não implica suporte para um misturador independente por aplicação.

As ações de exibição aumentam/diminuem o brilho em pontos percentuais 10 ou definem um valor de 0 a 100. Os monitores internos usam interfaces WMI suportadas; monitores externos requerem suporte DDC/CI e geralmente sua configuração no menu do monitor. Docks, adaptadores e cabos podem bloquear o controle. Escolha todos os monitores suportados ou um monitor. A reconexão pode alterar as identidades disponíveis; revise o alvo salvo. As leituras faltantes são mostradas como indisponíveis em vez de valores medidos.

OBS requer WebSocket v5 habilitado em OBS Tools. Configure o endpoint, teste a conexão e carregue os nomes de cena/entrada antes de salvar. As ações suportadas incluem troca de cena, início/parada/alternância de transmissão/gravação, pausa/retomada de gravação e silenciamento de entrada. As senhas podem ser armazenadas no Windows Credential Manager e são excluídas das exportações de configuração. Um comando OBS bem-sucedido não estabelece todos os efeitos de streaming downstream.

## Verificação de resultados e histórico de execução

Ative Verificar resultado nos passos adequados. A aplicação distingue verificações prévias, efeitos confirmados, falhas, cancelamentos e efeitos que não podem ser verificados. A existência de um processo não significa que a aplicação esteja pronta. As alterações relativas, os atalhos e a introdução de texto não podem ser declarados sempre bem-sucedidos. Inspecione o registo de atividade e o histórico de verificação quando uma ação falhar. Corrija os erros de configuração antes de voltar a executar a macro completa.

## Automação da UI e gravação de cliques

Adicione um passo de automação da interface, selecione a operação e capture o destino sob o ponteiro após o atraso apresentado. A captura, por si só, não executa nada. Verifique o seletor e exija uma correspondência única. As operações suportadas incluem `Invoke`, `Select`, `Toggle`, `Focus`, `SetValue` e `WaitState`. Os campos de palavra-passe, os controlos desativados e os seletores ambíguos são rejeitados.

A automação da interface é executada num processo auxiliar com limites definidos. Depende do suporte de acessibilidade Windows e das permissões da aplicação de destino. O gravador de cliques captura ações suportadas e pausas; não grava a introdução geral de texto. Reveja e valide o rascunho antes de o adotar. As alterações da interface de uma aplicação externa podem invalidar os seletores. O isolamento do processo limita as consequências das falhas e delimita o cancelamento, mas não é uma sandbox de segurança.

## Plug-ins

Abra Extensões para inspecionar um pacote antes de o importar. São verificados a estrutura do pacote, o manifesto e os hashes. Um pacote importado fica inicialmente desativado. Leia as ações declaradas e confie explicitamente no editor antes de o ativar. Os plugins são executados com as suas permissões de utilizador num processo separado; esse processo **não é uma sandbox**. Instale apenas pacotes em que confie.

O gerenciador suporta exportar, desabilitar, desinstalar e restaurar/descartar uma versão anterior. Dependências ausentes bloqueiam a invocação. O pacote de exemplo fornece ações de contagem regressiva e de estado local. O esquema suportado é um subconjunto do esquema JSON documentado, e não todos os recursos possíveis do esquema JSON. Não existe um mercado público de plugins.

## Painel móvel

1. Abra o Mobile Panel, selecione um IP local e escolha explicitamente os perfis a serem expostos. O endereço de loopback padrão só pode ser acessado no próprio PC; selecione o endereço LAN apropriado para um telefone ou tablet.
2. Inicie o painel e leia seu endereço HTTPS, impressão digital do certificado e código de emparelhamento de seis dígitos de uso único.
3. Em um dispositivo na mesma rede confiável, abra o endereço. Compare a impressão digital SHA-256 por meio de um canal confiável antes de confiar no certificado local. O aplicativo não instala um certificado confiável automaticamente.
4. Emparelhe usando o código exibido e teste uma peça inofensiva. Uma permissão manual do Firewall Windows pode ser necessária para acesso à LAN. Não exponha a porta à Internet.
5. Pare o painel quando terminar ou revogue clientes emparelhados por meio do aplicativo.

As chaves privadas do servidor são protegidas com Windows DPAPI; as credenciais dos clientes são guardadas como hashes. Alterar o IP ou renovar o certificado pode exigir uma nova confirmação de confiança. O painel mostra os estados das ações e da verificação, não todos os sensores possíveis em tempo real. As verificações da disposição no navegador e de HTTPS local não substituem a validação real de telefone/tablet na LAN.

## Assistente de reunião de IA local

Este assistente experimental opcional está desativado por predefinição. Abra-o em Ferramentas, ative-o explicitamente, selecione um ambiente de execução/modelo e adicione apenas os recursos a considerar. Indique o objetivo, crie um rascunho, inspecione os pedidos não resolvidos e transfira o resultado para o editor habitual. Nada é executado automaticamente; guardar e executar continuam a ser ações explícitas. Não existe mudança automática para outro ambiente de execução.

| Ambiente de execução | Configuração |
| --- | --- |
| Ollama | Serviço local separado e modelo instalado localmente em `127.0.0.1:11434`. Os modelos remotos/de nuvem declarados são rejeitados. |
| LM Studio | Servidor local com modelo carregado localmente e suporte ao esquema JSON em `127.0.0.1:1234`. Não habilite o roteamento remoto para processamento local. |
| llama.cpp | `llama-server` local e modelo compatível com suporte ao esquema JSON em `127.0.0.1:8080`. |
| CPU ONNX Tempo de execução GenAI | Pasta de modelo de texto GenAI local compatível e confiável; selecione `genai_config.json`. A inferência usa um processo complementar agrupado sem HTTP. |

Os clientes HTTP usam apenas loopback, não usam proxies e não seguem redirecionamentos. MAKRIVO não pode estabelecer com segurança se um servidor local compatível chama internamente um serviço remoto. Os servidores locais autenticados não são configuráveis ​​nesta versão. Os pesos do modelo, Python e servidores de inferência locais não são baixados ou instalados automaticamente.

Os pacotes ONNX precisam de `tokenizer.json` e de um modelo de formatação de conversação válido. As formas suportadas em `tokenizer_config.json` são uma cadeia `chat_template`, um dicionário com uma cadeia `default` ou uma lista inequívoca de modelos de formatação com nome/predefinidos. Se o modelo de formatação estiver ausente ou for nulo, é usado `chat_template.jinja` como alternativa. São rejeitados modelos de formatação vazios, ambíguos, demasiado grandes ou com tipo incorreto. Ficheiros ONNX arbitrários, modelos de embeddings/visão e pacotes exclusivos para GPU não são intercambiáveis com modelos de texto GenAI compatíveis. A versão do ambiente GenAI incluído é 0.17.1, CPU. Utilize apenas modelos de confiança; a separação de processos não é uma sandbox.

O assistente suporta programas selecionados sem argumentos, documentos e sites selecionados e o silenciamento do microfone quando expressamente solicitado. Não cria mensagens, compromissos, scripts arbitrários, cliques livres, transferências nem mudanças de auscultadores. O objetivo e os nomes dos recursos são enviados para o ambiente de execução local selecionado; os caminhos dos recursos e o conteúdo dos ficheiros não são fornecidos separadamente. Um objetivo escrito pelo utilizador pode conter dados pessoais. O comportamento de rede próprio do ambiente local continua a ser responsabilidade do utilizador.

O limite de uma solicitação ONNX é de 90 segundos, incluindo o carregamento do modelo, com um máximo de 3072 tokens de entrada e 1024 tokens gerados, também limitado pelo contexto do modelo. JSON malformado/truncado e identificadores de recursos desconhecidos são rejeitados. Cancele ou feche para terminar uma solicitação. Um teste do ambiente com um modelo aleatório demonstra a execução, não qualidade útil de planeamento nem poupança de tempo. Os registos de experiências locais contêm identificadores de eventos e durações, não o texto dos objetivos nem o conteúdo de documentos; não são enviados automaticamente.

A avaliação posterior do pequeno modelo Instruct não produziu nenhum plano válido em quatro ensaios em alemão/inglês. A inferência nativa funcionou, mas o modelo não cumpriu o contrato rigoroso de planeamento. É um resultado negativo de qualidade, não um modelo recomendado nem um assistente de utilidade comprovada. Consulte o relatório datado dos testes dos ambientes de execução locais para conhecer a revisão exata do modelo e os limites.

## Importação, exportação, sincronização e backup

Revise as visualizações de importação antes de substituir uma configuração. As ações importadas começam desabilitadas e a sincronização portátil remove ligações específicas de hardware. Reatribua dispositivos e ative ações deliberadamente após a inspeção. Revise o conteúdo do texto/ação antes de compartilhar uma exportação; a exclusão de senhas não torna todos os conteúdos de ações criados pelo usuário não confidenciais.

A sincronização de perfis troca ficheiros numa pasta selecionada, opcionalmente gerida pelo cliente de nuvem que já utiliza. MAKRIVO não fornece contas de nuvem nem guarda credenciais de nuvem. As revisões impedem substituições silenciosas. A inclusão do conteúdo das ações é uma escolha explícita. Faça uma cópia de segurança da configuração antes de uma importação que a substitua. A configuração predefinida é `%LOCALAPPDATA%\AivanaWindowsController\settings.json`; o nome histórico da pasta é mantido por compatibilidade. Feche a aplicação antes de a copiar ou restaurar manualmente. As aprovações de SDK guardadas e as credenciais protegidas pelo sistema operativo poderão ter de ser recriadas noutro PC.

## Solução de problemas

| Sintoma | O que verificar |
| --- | --- |
| Atalho não faz nada | Aplicação em execução, associações sem pausa, perfil ativo correto, mosaico guardado/ativado, conflitos de atalhos e destino disponível. |
| A janela de destino não pode ser ativada | Ative-o manualmente. Windows restrições de primeiro plano e diferenças de privilégio podem bloquear a automação. |
| A tecla especial não é detetada | Serviço do fabricante/SDK/suporte do modelo; se possível, configure os controlos proprietários para emitirem uma tecla comum suportada. |
| O segundo teclado também digita normalmente | Esperado com Raw Input. A supressão exclusiva não é implementada para produção. |
| Áudio ou brilho indisponíveis | Atualizar dispositivos; inspecione a identidade selecionada, sessões de endpoint, suporte WMI/DDC/CI e restrições de dock/cabo. |
| OBS off-line | WebSocket v5 ativado, endpoint/senha corretos, serviço acessível e nomes de cena/entrada válidos. |
| A automação da IU falha | Host complementar incluído, seletor válido exclusivo, destino habilitado acessível, controle e permissões compatíveis. Tente novamente um tempo limite transitório somente após revisar o destino. |
| Painel móvel inacessível | LAN em vez de endereço de loopback, mesma rede, servidor em execução, perfis aprovados, impressão digital/confiança de certificado e permissões manuais de firewall. |
| O plug-in falha | Integridade do pacote, confiança explícita/estado habilitado, dependências, compatibilidade de protocolo e log de execução. |
| A solicitação de IA falha | Correção do tempo de execução local selecionado, serviço/modelo pronto, esquema JSON compatível, pasta de modelo compatível confiável e limites de solicitação. |
| Layout ou texto parece errado | Separe a linguagem da UI da geometria do teclado; redimensione a janela, verifique a escala de exibição e relate a página/idioma/construção exata. |

Para um relatório de erro útil, inclua a versão do Windows, a compilação/o commit da aplicação, o idioma, a escala do ecrã, o modelo do equipamento, uma sequência mínima e o registo relevante com os dados sensíveis removidos. Remova caminhos pessoais, conteúdos de ações e credenciais antes de publicar.

## Construção, teste e embalagem

Utilize a versão do SDK fixada em `global.json`. A partir da raiz do repositório:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test
./tools/publish-complete.ps1 -SelfContained
python tools/check_documentation.py
```

Os testes de integração Windows podem abrir janelas isoladas e configurar/liberar temporariamente ligações de fornecedores compatíveis. Execute-os sem que outro controlador possua essas ligações. As sondagens padrão de brilho/áudio são somente leitura; testes explícitos de ida e volta de hardware são separados. Protocolos simulados, capturas de tela renderizadas e gráficos de inferência aleatória não devem ser representados como aceitação real de hardware/qualidade do modelo.

O script de publicação completa reúne os processos anfitriões auxiliares, os recursos móveis, o plugin de exemplo, o ambiente ONNX e os avisos de dependências disponíveis. Mantenha as subpastas. Uma compilação bem-sucedida não constitui um instalador assinado nem uma aprovação jurídica da publicação. O código-fonte e as compilações podem evoluir separadamente; compare o instantâneo da documentação e o commit com a versão instalada.

## Licenciamento, privacidade e status de lançamento

O modelo comercial previsto é uma compra única com utilização perpétua da versão principal adquirida; o preço final, o número de dispositivos permitido e as condições de atualização têm de ser definidos nas condições de venda efetivas. Este manual não demonstra a existência de um sistema de compra/ativação operacional em produção. Não é necessária uma conta de nuvem para o controlo diário.

Não distribua binários SDK proprietários sem permissão comprovada. Preserve avisos de licença e de terceiros para cada dependência enviada, incluindo pacotes .NET e ONNX. Os pesos do modelo têm seus próprios termos. A auditoria de licença datada é anterior a algumas novas dependências e não é uma autorização para essas adições. O fato de o repositório ser público não concede por si só uma licença geral de código aberto.

A validação com equipamento real dos fabricantes, o funcionamento de telefones/tablets na LAN, as transições entre monitores com DPI diferentes, a utilização prolongada, a revisão linguística completa, o suporte de um controlador para uso exclusivo do teclado e uma qualidade útil de planeamento de IA continuam a ser trabalhos de validação separados. Nunca deduza aprovação apenas da presença de uma funcionalidade.
