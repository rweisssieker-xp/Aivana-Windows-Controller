<!-- generated-doc locale=pt-PT source=docs/HANDBOOK.md sha256=8c6422e18d8939164f91da28603ed597f478f1a9b8bb41e14cbe4f6630bab3fc -->

> Tradução assistida por máquina. A estrutura, os exemplos de código e as ligações são verificados; isto não constitui revisão por um falante nativo nem aprovação jurídica. Os relatórios históricos mantêm as datas e o âmbito originais. [Fonte original](../HANDBOOK.md)

# MAKRIVO — Manual do utilizador

Fabricante: **Aivana**. Instantâneo da documentação: **2026-10-12**. Este manual descreve a implementação atual da árvore de trabalho. Integrações experimentais e funções dependentes de hardware são identificadas explicitamente. Os relatórios históricos descrevem seu instantâneo datado, não uma garantia para uma versão mais recente.

## Documentação e idiomas

Utilize o seletor de idioma da documentação para abrir o manual completo, inventário de recursos, solução de problemas, notas de desenvolvimento, pesquisas e relatórios históricos de testes em qualquer um dos idiomas da aplicação 27. As traduções geradas são assistidas por máquina e possuem verificações estruturais; nem todos receberam avaliações de falantes nativos. Os identificadores técnicos e os exemplos executáveis ​​permanecem inalterados. A fonte original permanece disponível para comparação. A análise de licenciamento traduzida é informativa e não substitui os termos legais originais de terceiros ou um EULA final.

## Instalação e primeiro lançamento

O alvo inicial é Windows 11 x64. Mantenha todos os arquivos e subdiretórios da distribuição juntos. Inicie `MAKRIVO.exe`. Uma verificação de origem não é um instalador. Pacotes independentes incluem seu tempo de execução .NET; pacotes dependentes de estrutura requerem os tempos de execução .NET 10 Desktop e ASP.NET apropriados. ONNX e hosts complementares devem ser incluídos para seus respectivos recursos.

1. Inicie MAKRIVO e escolha o idioma da interface em Configurações. Automático segue Windows. O layout do teclado é uma configuração separada.
2. Escolha um perfil e adicione um bloco por meio do catálogo de ações pesquisável. Dê a ele um nome reconhecível e escolha suas ações.
3. Revise as etapas e atribua um atalho, se necessário. Evite combinações já utilizadas pelo Windows ou pela sua aplicação alvo.
4. Salve o rascunho. As alterações não salvas não são a configuração salva.
5. Teste com uma ação inofensiva, como abrir um documento, e inspecione o feedback de execução.

Fechar a janela principal normalmente deixa o aplicativo na área de notificação. Clique duas vezes no ícone da bandeja para reabri-lo ou use o menu da bandeja para sair. Começar com Windows é opcional e inicialmente desabilitado. O controle de pausa suspende as ligações; o ícone mostra um emblema de pausa. Cancele a execução de macros através da UI, bandeja ou atalho de cancelamento configurado; o padrão é `Ctrl+Alt+Escape`.

## Blocos, ações e paleta de comandos

Os blocos pertencem a perfis e podem conter diversas etapas ordenadas. O editor suporta salvar/descartar, visualizar, desfazer/refazer e reordenar etapas. O catálogo fornece predefinições para programas, documentos, URLs, texto, área de transferência, atalhos, mídia, áudio, janelas, ações do sistema e integrações suportadas. Uma predefinição que requer parâmetros é aberta como rascunho.

Abra a paleta com `Ctrl+K` enquanto MAKRIVO está em foco. Pesquise ações salvas, predefinições, troca de perfil e ferramentas. As teclas de seta selecionam; Digite ativa; `Ctrl+Enter` edita um item suportado; A fuga se fecha. Alguns comandos prontos para uso são executados imediatamente, enquanto as ações configuradas exigem revisão. Um atalho de paleta global separado pode ser configurado; novas configurações usam `Ctrl+Shift+Space`.

As etapas de texto e área de transferência suportam `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` e `{{user}}`. A formatação da data é `yyyy-MM-dd`; a formatação da hora é `HH:mm`. Variáveis ​​desconhecidas permanecem inalteradas. A leitura da área de transferência ocorre somente quando solicitada. Uma ação da área de transferência grava texto sem colá-lo automaticamente.

As ações iniciadas na IU podem ter como alvo o último aplicativo externo. Se o alvo desaparecer ou Windows bloquear a ativação em primeiro plano, ative manualmente o alvo e tente novamente. A entrega bem-sucedida da chave não prova que o alvo completou sua tarefa.

## Perfis e configuração do local de trabalho

Os perfis podem corresponder a um nome de processo e parte de um título de janela. Quando ambos estão definidos, ambos devem corresponder. A ordem dos perfis resolve partidas concorrentes; o perfil padrão é o substituto. Habilite a seleção automática explicitamente. Bloquear perfil impede alterações automáticas na sessão atual.

Abra Configuração do Workplace em Ferramentas ou na paleta. Selecione programas, documentos, sites, um alvo de áudio/exibição ou uma cena OBS. O assistente prepara perfis de local de trabalho, reunião, streaming, pesquisa ou projeto. Inspecione as etapas geradas antes de salvar. A instalação não executa as ações preparadas.

## Macros, condições e gravação

As macros executam suas etapas em ordem e suportam esperas, repetições e cancelamentos. O limite geral de repetição é 100. Use a Visualização antes de executar ações digitadas em outro aplicativo ou alterar o estado do sistema.

A janela Wait for Target aceita um processo, uma substring de título e um tempo limite de 1 a 600,000 ms. É necessário pelo menos um filtro. Uma janela visível correspondente deve responder e ficar em primeiro plano antes que as etapas subsequentes continuem. Uma janela pronta não prova que um documento ou serviço terminou de carregar.

O editor de fluxo de trabalho adiciona condições de existência de arquivo, execução de processo e título de janela. Uma condição falsa ignora a etapa e pode avançar etapas adicionais. Negue a condição para expressar uma ramificação alternativa. Selecione parar em caso de erro ou continuar em caso de erro deliberadamente; continuar pode deixar etapas posteriores sem as pré-condições esperadas.

O gravador de ação captura acordes de atalho e espera, e não digitação comum. Suporta atalhos modificadores e teclas de função. Escape interrompe a gravação. Revise a lista e adote-a como um rascunho de bloco não salvo e salve explicitamente. A gravação de cliques na IU é uma ferramenta separada descrita abaixo.

## Teclados, gestos e camadas

A página do teclado lista interfaces detectadas e layouts de referência. Um único teclado físico pode expor diversas interfaces. Selecione a linguagem e geometria corretas; o nome de um produto por si só não identifica o layout ISO ou ANSI.

As teclas de atalho globais padrão se aplicam a todos os teclados. As ligações Raw Input específicas do dispositivo observam um dispositivo específico e verificam o código, incluindo funções suportadas e teclas de mídia. **Raw Input não suprime a entrada Windows comum. Um segundo teclado exclusivo não está pronto para produção.** Um back-end de driver de filtro assinado, licenciado e testado ainda é necessário; nenhum é instalado por MAKRIVO.

As associações de teclas especiais suportadas podem usar pressionar, tocar, segurar ou tocar duas vezes imediatamente. As chaves da camada retida selecionam temporariamente outro mapeamento e restauram a camada base na liberação; as teclas seletoras de camada não executam sua própria macro. Os limites de retenção variam de 150 a 5,000 ms; toque duas vezes nas janelas de 150 para 1,000 ms. Esses mapeamentos são distintos das teclas de atalho globais comuns.

O assistente de chave especial detecta chaves compatíveis sem executar as ações atribuídas durante a captura. O diagnóstico explica dispositivos indisponíveis, vinculações pausadas, incompatibilidades de perfil e conflitos. A sobreposição mostra o layout e as ligações selecionadas; seu atalho de espera padrão é `Ctrl+Alt+F1`.

| Integração | Requisitos e limitações |
| --- | --- |
| Corsair | Executando iCUE, acesso SDK e modelo compatível. G1–G6 e funções RGB suportadas; Os controles proprietários Fn, wheel, lock e profile geralmente não são expostos pela API G-key. |
| Logitech G | As bibliotecas locais originais x64 G-key/LED SDK exigem aprovação explícita. Os eventos da tecla G não identificam exclusivamente vários teclados físicos idênticos. |
| Razer | Chroma RGB e entrada padrão; mapeie controles proprietários em Synapse quando necessário. |
| SteelSeries | GameSense RGB e entrada padrão; mapeie controles proprietários no GG quando necessário. |
| Wooting | SDK x64 local original compatível para valores e limites analógicos; a entrada HID normal permanece ativa. |
| Outros teclados | Teclas de atalho padrão ou ligações de código de leitura física suportadas. Os controles Fn e proprietários dependem do modelo e da configuração do fornecedor. |

A pesquisa do fabricante descreve recursos documentados, não implementados ou testados quanto à compatibilidade com todos os dispositivos listados. Novos adaptadores ainda precisam de aceitação real de hardware. Não baixe binários do SDK de fontes não confiáveis; Os direitos de distribuição do SDK são separados da compatibilidade técnica.

## Áudio, monitores e OBS

Avaliação de volume de 0 Para 100 percentuais e saída mute/unmute/toggle suportada ou pontos finais de microfone. escolha um ponto final específico ou use o Windows Um objetivo salvo perdido produz um erro em vez de controlar silenciosamente outro dispositivo. Revisão de dispositivos após a reconectar. controle de ponto final não é uma reivindicação de suporte independente por aplicativo.

As ações de exibição aumentam/diminuem o brilho em pontos percentuais 10 ou definem um valor de 0 a 100. Os monitores internos usam interfaces WMI suportadas; monitores externos requerem suporte DDC/CI e geralmente sua configuração no menu do monitor. Docks, adaptadores e cabos podem bloquear o controle. Escolha todos os monitores suportados ou um monitor. A reconexão pode alterar as identidades disponíveis; revise o alvo salvo. As leituras faltantes são mostradas como indisponíveis em vez de valores medidos.

OBS requer WebSocket v5 habilitado em OBS Tools. Configure o endpoint, teste a conexão e carregue os nomes de cena/entrada antes de salvar. As ações suportadas incluem troca de cena, início/parada/alternância de transmissão/gravação, pausa/retomada de gravação e silenciamento de entrada. As senhas podem ser armazenadas no Windows Credential Manager e são excluídas das exportações de configuração. Um comando OBS bem-sucedido não estabelece todos os efeitos de streaming downstream.

## Verificação de resultados e histórico de execução

Ative Verificar resultado para etapas adequadas. O aplicativo distingue verificações prévias, efeitos confirmados, falhas, cancelamento e efeitos que não podem ser verificados. A existência do processo não é a prontidão do aplicativo. Alterações relativas, atalhos e entrada de texto não podem receber uma reivindicação geral de sucesso. Inspecione o log de atividades e o histórico de verificação quando uma ação falhar. Os erros de configuração devem ser corrigidos antes de executar novamente a macro completa.

## Automação da UI e gravação de cliques

Adicione um passo de automação de UI, selecione a operação e capte o objetivo abaixo do indicador após o atraso exibido. Capturar sozinho não executa nada. `Invoke`, `Select`, `Toggle`, `Focus`, `SetValue` e `WaitState`. Os campos de senha, controles desativados e selecionadores ambíguos são rejeitados.

A automação da UI é executada em um processo complementar limitado. Depende do suporte e das permissões de acessibilidade Windows do aplicativo de destino. O gravador de cliques captura ações e pausas suportadas; ele não registra entrada de texto geral. Revise e valide seu projeto antes de adotá-lo. Alterações na interface do usuário em um aplicativo externo podem invalidar os seletores. O isolamento do processo limita falhas e cancelamentos, mas não é uma área restrita de segurança.

## Plug-ins

Abra Extensões para inspecionar um pacote antes de importá-lo. A estrutura do pacote, o manifesto e os hashes são verificados. Uma importação é iniciada desabilitada. Leia as ações declaradas e confie explicitamente no editor antes de ativar. Os plug-ins são executados com suas permissões de usuário em um processo separado; esse processo é ** e não uma sandbox**. Instale apenas pacotes em que você confia.

O gerenciador suporta exportar, desabilitar, desinstalar e restaurar/descartar uma versão anterior. Dependências ausentes bloqueiam a invocação. O pacote de exemplo fornece ações de contagem regressiva e de estado local. O esquema suportado é um subconjunto do esquema JSON documentado, e não todos os recursos possíveis do esquema JSON. Não existe um mercado público de plugins.

## Painel móvel

1. Abra o Mobile Panel, selecione um IP local e escolha explicitamente os perfis a serem expostos. O endereço de loopback padrão só pode ser acessado no próprio PC; selecione o endereço LAN apropriado para um telefone ou tablet.
2. Inicie o painel e leia seu endereço HTTPS, impressão digital do certificado e código de emparelhamento de seis dígitos de uso único.
3. Em um dispositivo na mesma rede confiável, abra o endereço. Compare a impressão digital SHA-256 por meio de um canal confiável antes de confiar no certificado local. O aplicativo não instala um certificado confiável automaticamente.
4. Emparelhe usando o código exibido e teste uma peça inofensiva. Uma permissão manual do Firewall Windows pode ser necessária para acesso à LAN. Não exponha a porta à Internet.
5. Pare o painel quando terminar ou revogue clientes emparelhados por meio do aplicativo.

As chaves privadas do servidor são protegidas com Windows DPAPI; as credenciais do cliente são armazenadas como hashes. Alterar o IP ou renovar o certificado pode exigir confiança renovada. O painel mostra estados de ação e verificação, e não todos os sensores ativos possíveis. As verificações de layout do navegador e HTTPS local não substituem a aceitação real da LAN por telefone/tablet.

## Assistente de reunião de IA local

Este assistente experimental opcional está desabilitado por padrão. Abra-o em Ferramentas, ative-o explicitamente, selecione um tempo de execução/modelo e adicione apenas os recursos que deseja considerar. Estabeleça o objetivo, crie um rascunho, inspecione as solicitações não resolvidas e transfira o resultado para o editor normal. Nada é executado automaticamente; salvar e executar permanecem ações explícitas. Não há fallback automático de tempo de execução.

| Tempo de execução | Configuração |
| --- | --- |
| Ollama | Serviço local separado e modelo instalado localmente em `127.0.0.1:11434`. Os modelos remotos/de nuvem declarados são rejeitados. |
| LM Studio | Servidor local com modelo carregado localmente e suporte ao esquema JSON em `127.0.0.1:1234`. Não habilite o roteamento remoto para processamento local. |
| llama.cpp | `llama-server` local e modelo compatível com suporte ao esquema JSON em `127.0.0.1:8080`. |
| CPU ONNX Tempo de execução GenAI | Pasta de modelo de texto GenAI local compatível e confiável; selecione `genai_config.json`. A inferência usa um processo complementar agrupado sem HTTP. |

Os clientes HTTP usam apenas loopback, não usam proxies e não seguem redirecionamentos. MAKRIVO não pode estabelecer com segurança se um servidor local compatível chama internamente um serviço remoto. Os servidores locais autenticados não são configuráveis ​​nesta versão. Os pesos do modelo, Python e servidores de inferência locais não são baixados ou instalados automaticamente.

ONNX Pacotes necessários `tokenizer.json` e um modelo de chat válido. formulários de modelo suportados em `tokenizer_config.json` É uma string `chat_template`Um dicionário com uma linha `default`, ou uma lista de modelos de nome/default indiscutible. se o modelo for ausente ou nulo, `chat_template.jinja` Os modelos vazios, ambíguos, excessivos ou incorretamente tipados são rejeitados. ONNX Os arquivos, modelos de inserção/visão e pacotes GPU-só não são intercambiáveis com compatíveis GenAI Modelo de texto: O Bandeiro GenAI A versão do Runtime é 0.17.1Use apenas modelos confiáveis; a separação de processos não é uma caixa de areia.

O assistente oferece suporte a programas selecionados sem argumentos, documentos e sites selecionados e microfone mudo quando solicitado expressamente. Não cria mensagens, compromissos, scripts arbitrários, cliques gratuitos, downloads ou troca de fone de ouvido. Os nomes da meta e dos recursos são enviados para o tempo de execução local selecionado; caminhos de recursos e conteúdos de arquivos não são fornecidos separadamente. Uma meta digitada pelo usuário pode conter dados pessoais. O comportamento de rede do próprio tempo de execução local permanece de responsabilidade do usuário.

O orçamento de solicitação ONNX é de 90 segundos, incluindo carregamento do modelo, até tokens de prompt 3,072 e tokens gerados 1,024, também limitados pelo contexto do modelo. JSON malformado/truncado e IDs de recursos desconhecidos são rejeitados. Cancelar ou fechar para encerrar uma solicitação. Um teste de tempo de execução de modelo aleatório estabelece a execução, e não a qualidade útil do planejamento ou economia de tempo. Os registros de experimentos locais contêm identificadores e horários de eventos, e não textos de metas ou conteúdos de documentos; eles não são carregados automaticamente.

A avaliação do modelo Instruct mais tarde produziu planos de nulidade em quatro ensaios alemã / inglês. a inferência nativa funcionou, mas o modelo falhou no contrato de planejamento rigoroso. Este é um resultado de qualidade negativo, não um modelo recomendado ou um assistente útil comprovado. Ver o relatório de teste de tempo de execução local para a revisão exata do modelo e limites.

## Importação, exportação, sincronização e backup

Revise as visualizações de importação antes de substituir uma configuração. As ações importadas começam desabilitadas e a sincronização portátil remove ligações específicas de hardware. Reatribua dispositivos e ative ações deliberadamente após a inspeção. Revise o conteúdo do texto/ação antes de compartilhar uma exportação; a exclusão de senhas não torna todos os conteúdos de ações criados pelo usuário não confidenciais.

A sincronização de perfis troca arquivos em uma pasta selecionada, opcionalmente uma gerenciada pelo seu cliente de nuvem existente. MAKRIVO fornece nenhuma conta em nuvem e não armazenam credenciais em nuvem. Revisões impedem sobreescritos silenciosos. Incluir conteúdos de ação é uma escolha explícita. Backup sua configuração antes de uma importação de substituição. `%LOCALAPPDATA%\AivanaWindowsController\settings.json`O nome histórico do directorio é mantido para compatibilidade.Close o aplicativo antes de copiá-lo manualmente.As aprovações SDK armazenadas e as credenciais protegidas pelo sistema operativo podem precisar ser recreadas em outro PC.

## Solução de problemas

| Sintoma | O que verificar |
| --- | --- |
| Atalho não faz nada | Aplicativo em execução, vinculações não pausadas, perfil ativo correto, bloco salvo/ativado, conflitos de atalho e destino disponível. |
| A janela de destino não pode ser ativada | Ative-o manualmente. Windows restrições de primeiro plano e diferenças de privilégio podem bloquear a automação. |
| Chave especial não foi detectada | Serviço de fornecedor/SDK/suporte de modelo; configure controles proprietários para emitir uma chave padrão suportada, se possível. |
| O segundo teclado também digita normalmente | Esperado com Raw Input. A supressão exclusiva não é implementada para produção. |
| Áudio ou brilho indisponíveis | Atualizar dispositivos; inspecione a identidade selecionada, sessões de endpoint, suporte WMI/DDC/CI e restrições de dock/cabo. |
| OBS off-line | WebSocket v5 ativado, endpoint/senha corretos, serviço acessível e nomes de cena/entrada válidos. |
| A automação da IU falha | Host complementar incluído, seletor válido exclusivo, destino habilitado acessível, controle e permissões compatíveis. Tente novamente um tempo limite transitório somente após revisar o destino. |
| Painel móvel inacessível | LAN em vez de endereço de loopback, mesma rede, servidor em execução, perfis aprovados, impressão digital/confiança de certificado e permissões manuais de firewall. |
| O plug-in falha | Integridade do pacote, confiança explícita/estado habilitado, dependências, compatibilidade de protocolo e log de execução. |
| A solicitação de IA falha | Correção do tempo de execução local selecionado, serviço/modelo pronto, esquema JSON compatível, pasta de modelo compatível confiável e limites de solicitação. |
| Layout ou texto parece errado | Separe a linguagem da UI da geometria do teclado; redimensione a janela, verifique a escala de exibição e relate a página/idioma/construção exata. |

Para um relatório de bug útil, inclua a versão Windows, construção/confirmação do aplicativo, idioma, escala de exibição, modelo de hardware, uma sequência mínima e o log redigido relevante. Remova caminhos pessoais, conteúdos de ação e credenciais antes de publicar.

## Construção, teste e embalagem

Use o SDK fixado em `global.json`. Da raiz do repositório:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test
./tools/publish-complete.ps1 -SelfContained
python tools/check_documentation.py
```

Os testes de integração Windows podem abrir janelas isoladas e configurar/liberar temporariamente ligações de fornecedores compatíveis. Execute-os sem que outro controlador possua essas ligações. As sondagens padrão de brilho/áudio são somente leitura; testes explícitos de ida e volta de hardware são separados. Protocolos simulados, capturas de tela renderizadas e gráficos de inferência aleatória não devem ser representados como aceitação real de hardware/qualidade do modelo.

O editor completo reúne hosts complementares, ativos móveis, o plug-in de exemplo, o tempo de execução ONNX e avisos de dependência disponíveis. Mantenha seus subdiretórios. Uma compilação bem-sucedida não é um instalador assinado ou uma aprovação de lançamento legal. O código-fonte e as compilações podem evoluir separadamente; compare o instantâneo da documentação e confirme com a versão instalada.

## Licenciamento, privacidade e status de lançamento

O modelo comercial pretendido é uma compra única com uso perpétuo da versão principal adquirida; o preço final, o limite do dispositivo e os termos de atualização devem ser definidos nas condições de venda reais. Um sistema de compra/ativação de produção não é estabelecido neste manual. Não há conta na nuvem necessária para controle diário.

Não distribua binários SDK proprietários sem permissão comprovada. Preserve avisos de licença e de terceiros para cada dependência enviada, incluindo pacotes .NET e ONNX. Os pesos do modelo têm seus próprios termos. A auditoria de licença datada é anterior a algumas novas dependências e não é uma autorização para essas adições. O fato de o repositório ser público não concede por si só uma licença geral de código aberto.

Hardware real do fornecedor, operação LAN de telefone/tablet, transições mistas de monitor/DPI, uso prolongado, revisão linguística completa, suporte exclusivo de driver e qualidade útil de planejamento de IA permanecem trabalhos de aceitação separados. Nunca deduza a aprovação apenas da presença do recurso.
