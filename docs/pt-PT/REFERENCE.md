<!-- generated-doc locale=pt-PT source=README.md sha256=eb6f3fe7810ed3e10793e8938e908e151a84c4afbea2f22cba2656bfe4e8dfc3 -->

> Tradução assistida por máquina. A estrutura, os exemplos de código e as ligações são verificados; isto não constitui revisão por um falante nativo nem aprovação jurídica. Os relatórios históricos mantêm as datas e o âmbito originais. [Fonte original](../../README.md)

# MAKRIVO

[Documentação completa em todas as 27 línguas](README.md) · [Manual de usuário completo](HANDBOOK.md)

**por Aivana.** [Inventário de recursos e limitações de lançamento](FEATURES.md) · [Relatório de Extensão](testing/2026-10-12-six-extensions.md) · [Protópico de Assistente Local](testing/2026-10-12-local-meeting-assistant.md)

**Inglês (EUA)** | [Deutsch](../de-DE/REFERENCE.md) | [Français](../fr-FR/REFERENCE.md) | [Italiano](../it-IT/REFERENCE.md)

Um controlador de teclas de atalho Windows local com blocos, perfis, macros e detecção de teclado no estilo Stream Deck. A interface suporta idiomas 27. Nenhuma conta ou conexão com a nuvem é necessária para o uso diário.

[Documentação](README.md) · [Verificação](VERIFICATION.md) · [Relatório de testes](testing/2026-10-09-usp-release.md)

## Primeiros passos

Após a publicação, o aplicativo fica em `artifacts/win-x64/MAKRIVO.exe`. Mantenha toda a pasta de saída junta; você pode copiá-lo em outro lugar. Windows 11 x64 é o alvo inicial. Os artefatos de construção são excluídos do Git; uma verificação de origem não inclui um executável pré-construído.

Instale o SDK .NET 10 especificado em `global.json` para compilar e executar a partir da origem:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project src/Aivana.Desktop -c Release
```

Publique uma compilação independente sem exigir um tempo de execução .NET instalado separadamente:

```powershell
dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64
```

## Fluxo de trabalho básico

1. Selecione um perfil e crie um bloco de ação.
2. Defina seu nome, ícone, cor e atalho de teclado. As combinações Ctrl/Alt/Shift reduzem a ativação acidental durante a digitação.
3. Adicione uma ou mais etapas: aplicativos, arquivos, sites, texto, atalhos, mídia, volume, troca de perfil ou atrasos.
4. Salve. As alterações permanecem como rascunho até serem salvas; descartar restaura a versão salva. O botão Play do bloco e sua tecla de atalho usam o mesmo caminho de execução.
5. Reordene blocos e etapas de macro com arrastar e soltar ou botões de seta. Os modelos simplificam a configuração. As macros podem ser repetidas até 100 vezes. Cancele através da interface, menu da bandeja do sistema ou Ctrl+Alt+Escape por padrão.

Perfis específicos do aplicativo usam um nome de processo sem o caminho completo. A ordem dos perfis rompe laços; o perfil padrão é o substituto. A seleção automática de perfil deve estar habilitada em Configurações.

Fechar a janela principal mantém o aplicativo na bandeja do sistema por padrão. Clique duas vezes no ícone da bandeja para reabri-lo; use o menu da bandeja para sair. A inicialização com Windows é opcional e desabilitada por padrão. As teclas de atalho podem ser pausadas centralmente.

## Ações e paleta de comandos

Add Action abre um catálogo pesquisável com predefinições 90 em oito categorias. Cada um tem uma descrição e visualização. O editor de macro usa o mesmo catálogo para adicionar ou substituir etapas. Selecionar uma predefinição neste fluxo de trabalho do editor cria um rascunho sem execução.

A paleta de comandos é aberta no cabeçalho ou com Ctrl+K dentro do aplicativo. Ele pesquisa ações salvas em perfis, predefinições de catálogo, troca de perfil, navegação, pausa/retomada, cancelamento de macro e importação/exportação. Filtre por ações salvas, funções de catálogo ou comandos de aplicativos. As teclas de seta selecionam, Enter ativa, Ctrl+Enter edita uma entrada editável e Escape fecha. Os favoritos aparecem primeiro. Comandos de catálogo prontos para uso podem ser executados imediatamente; predefinições que exigem configuração abrem um rascunho. As configurações podem definir um atalho de paleta global. Novas configurações usam Ctrl+Shift+Espaço; as configurações existentes não recebem um novo atalho global automaticamente.

As ações de áudio definem o volume com precisão de 0 a 100 por cento e ativam, ativam ou alternam dispositivos de saída e microfone. Escolha um endpoint específico ou deixe-o sem definição para o padrão Windows. Um endpoint salvo indisponível produz um erro em vez de controlar outro dispositivo. Atualizar dispositivos recarrega os dispositivos conectados.

OBS Studio usa WebSocket v5: iniciar/parar/alternar/pausar/retomar gravação; iniciar/parar/alternar streaming; mudar cenas; alternar entrada mudo. Habilite o servidor WebSocket em Ferramentas em OBS e defina seu endpoint no editor. Verificar nomes de conexão e carregamento lê a versão OBS, cenas e entradas sem iniciar uma gravação ou stream. Escolha nomes na lista ou insira-os manualmente. As senhas podem ser salvas no Windows Credential Manager e são excluídas das configurações exportadas. Conexões OBS indisponíveis relatam um erro.

Os perfis podem corresponder a um nome de processo e parte de um título de janela. Ambas as condições especificadas devem corresponder. Bloquear perfil impede a troca automática da sessão. As condições de captura do primeiro plano usam o último aplicativo externo em foco.

Ações de texto e área de transferência suportam `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` e `{{user}}`. As datas usam `yyyy-MM-dd`; vezes use `HH:mm`. Os espaços reservados desconhecidos permanecem inalterados. `{{profile}}` identifica o perfil da ação lançada; `{{process}}` identifica o contexto do aplicativo externo. O conteúdo da área de transferência é somente lido quando esse espaço reservado é usado. A ação da área de transferência grava texto, mas não cola automaticamente.

As ações da janela minimizam, maximizam, restauram ou organizam uma janela no lado esquerdo/direito da área de trabalho do monitor. A ação do sistema bloqueia Windows. Essas ações são executadas somente quando invocadas explicitamente.

A visualização mostra etapas, repetições e variáveis sem execução. Desfazer e Refazer retêm até 100 estados de rascunho; as caixas de texto mantêm a edição nativa de Ctrl + Z. Marque os favoritos no editor ou no menu lado a lado e filtre-os no painel. As contagens de execução aplicam-se apenas à sessão atual.

Ações de texto, atalho e janela iniciadas na interface ativam a última janela de destino externa. A paleta exibe esse contexto de destino. Se o destino for fechado ou Windows recusar a ativação, a execução será interrompida com uma mensagem.

## Assistência e feedback do teclado

O Assistente de Teclas Especiais está na página Teclado e na paleta. Ele lista as teclas G relatadas por iCUE. Detectar pressionamento de tecla os captura temporariamente sem executar as ações atribuídas. As ligações normais retornam após detecção, cancelamento ou fechamento. Uma função de catálogo e uma chave selecionada são abertas como um rascunho não salvo; ligações duplicadas são verificadas.

A sobreposição do teclado mostra o layout de referência selecionado e as ligações ativas. Segure Ctrl+Alt+F1 ou alterne-o na paleta ou na página Teclado. Ele não tira o foco e permite a passagem dos cliques do mouse. As alterações de perfil e vinculação são atualizadas enquanto visíveis; os pressionamentos de tecla Windows comuns são destacados. Os controles específicos do fabricante podem não fornecer um sinal de tecla Windows equivalente.

O feedback de execução mostra espera, progresso de etapa/repetição, conclusão, cancelamento ou falha sem tirar o foco ou expor o texto inserido. Comandos executados confirma a execução do comando, e não um estado ativo consultado de forma independente em um aplicativo de terceiros. Os detalhes do erro aparecem no log de atividades.

## Dispositivos e Corsair

Corsair G1–G6 pode ser atribuído diretamente por meio do SDK iCUE oficial. Selecione o dispositivo e digite no editor de ações e salve ou prepare um rascunho na visualização do teclado K100. As vinculações se aplicam ao dispositivo selecionado e ao perfil ativo; a pausa também os desativa. iCUE deve estar em execução e permitir acesso. Fn, a roda iCUE, o perfil e os controles de bloqueio não podem ser mapeados por meio desta API G-key.

A página Dispositivos lista as interfaces do teclado Windows e é atualizada periodicamente e quando os dispositivos são conectados. Um teclado físico pode expor múltiplas interfaces HID. As teclas de atalho globais padrão funcionam entre fabricantes.

A detecção Corsair usa informações disponíveis do dispositivo. O controle RGB requer hardware compatível, executando iCUE e o SDK iCUE oficial; o aplicativo informa a disponibilidade real. Outros fabricantes recebem atualmente detecção e ações padrão. Fora da integração da chave G Corsair, os controles proprietários funcionam quando Windows os recebe como entrada regular. O aplicativo não instala um driver de teclado.

## Idiomas

As configurações oferecem os idiomas 27: alemão, inglês, francês, espanhol, italiano, português, holandês, polonês, tcheco, eslovaco, húngaro, romeno, búlgaro, grego, dinamarquês, sueco, norueguês, finlandês, estoniano, letão, lituano, esloveno, croata, ucraniano, russo, turco e sérvio em escrita latina. Automático segue o idioma de exibição Windows. As traduções são agrupadas localmente. Layouts, teclas de atalho e nomes criados pelo usuário são independentes do idioma da interface.

A documentação completa do repositório está disponível em todos os idiomas de aplicação 27 através do [Índice de Língua](README.md). As traduções são assistidas por máquina, com links originais e verificações estruturais; a revisão do falante nativo é separada. O aplicativo também aceita os códigos regionais `en-US`, `de-DE`, `fr-FR` e `it-IT` e usa seus formatos de número. Os códigos linguísticos existentes permanecem compatíveis.

## Layouts e pesquisa de fabricante

A visualização do teclado fornece referências ISO-DE/ANSI-US em tamanho real e sem tenkey, além de layouts RGB Corsair K100. A geometria representa posições, teclas largas/altas e ISO Enter. Layouts padrão são referências genéricas. Os layouts K100 seguem controles documentados sem reivindicar dimensões com precisão milimétrica. Escolha a variante de idioma/geometria correta: um nome de modelo ou USB VID/PID não estabelece uma variante específica.

O mapeamento resolve códigos de leitura física usando o layout de entrada Windows ativo. O fluxo de trabalho prepara um rascunho e retém verificações de salvamento/conflito. As ligações padrão são teclas de atalho globais Windows, não isoladas em um teclado conectado. Corsair G1 – G6 usa integração iCUE específica do dispositivo. Outros controles proprietários exigem um adaptador ou atalhos configurados pelo fabricante. O Enter principal e o Enter do teclado numérico compartilham uma identidade de chave virtual para teclas de atalho globais.

O [Pesquisa do fabricante](research/tastaturhersteller-sonderfunktionen.md) e [Cv](research/tastaturhersteller-sonderfunktionen.csv) cobre 74 grupos de marca/fabricante, distinguindo configuradores, SDKs de entrada e APIs de iluminação. [Layout evidência](research/keyboard-layout-evidence.md) Modelo de registro, SKU e diferenças de módulo. A cobertura de pesquisa não significa que o suporte de hardware seja implementado.

## Dados

A configuração é armazenada nos dados do aplicativo local. Importação/exportação usa JSON. A importação visualiza a configuração, substitui a configuração atual após a confirmação e desativa as ligações importadas até serem ativadas manualmente. O conteúdo de ação de texto não é coletado como um registro de pressionamento de tecla. As senhas do Credential Manager são separadas dos perfis exportados.

## Verificação

```powershell
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
```

As verificações Windows podem inspecionar o hardware e configurar/liberar temporariamente ligações iCUE suportadas. Execute-os sem outro controlador usando essas ligações. As verificações de endpoint de áudio não alteram as configurações de volume ao vivo ou mudo.

A entrada nativa não pode ignorar os privilégios Windows. Um controlador iniciado normalmente pode não enviar entrada para um aplicativo de administrador. As teclas de atalho reservadas ou ocupadas aparecem como conflitos.

O [Indicadores de Documentação](README.md) Links de projetos históricos, planos, pesquisas e relatórios de testes datados. Os planos históricos descrevem seu alcance registrado, não uma garantia atual de suporte.

### Modelos de fluxo de trabalho e esperas de janela de destino

O catálogo inclui seis fluxos de trabalho editáveis: espaço de trabalho, reunião, streaming, pesquisa, troca de projeto e trecho de texto. Procure `workflow.` no catálogo ou na paleta de comandos. A seleção de um fluxo de trabalho abre um rascunho não salvo; ele nunca é executado imediatamente. Todas as etapas são mantidas ao adicionar um fluxo de trabalho a uma macro existente ou ao atribuí-lo a uma chave Corsair. Configure URLs de exemplo, caminhos de programa, perfis e cenas OBS antes de salvar.

A etapa **Wait for target window** aceita um nome de processo (com ou sem `.exe`), uma substring de título que não diferencia maiúsculas de minúsculas e um tempo limite de 1–600,000 ms. É necessário pelo menos um filtro; ambos os filtros devem corresponder quando fornecidos. A macro continua somente depois que uma janela visível correspondente responder e atingir o primeiro plano. Use um título específico quando várias janelas compartilharem um processo. O cancelamento ou o tempo limite interrompe as etapas subsequentes. A prontidão da janela não garante que um documento tenha sido carregado ou que um serviço externo como OBS WebSocket esteja conectado.

Brilho da tela: pesquise **Aumentar o brilho da tela**, **Diminuir o brilho da tela** ou **Brilho da tela em 50 por cento** no catálogo de ações. As ações relativas alteram o brilho em pontos percentuais 10; os valores fixos variam de 0 a 100. Os monitores internos suportados usam WMI; monitores externos compatíveis requerem DDC/CI ativado em seu menu. Por padrão, a ação afeta todos os monitores suportados; um monitor explícito pode ser selecionado no editor. Conexões e docks podem restringir o suporte.

### Controles estendidos

Abra a paleta de comandos com **Ctrl+K enquanto a janela do aplicativo tem focus**. Pesquisar **Designer de fluxo de trabalho**, **Gravar macro**, **Sincronizar perfis** ou **Diagnóstico de chaves** (o texto segue a sua seleção idioma). O designer de fluxo de trabalho edita o rascunho do bloco selecionado: condições de arquivo/processo/janela, contagens de avanço e interrupção/continuação do tratamento de erros. O gravador captura acordes de atalho e espera, e não digitação comum; Escape interrompe a gravação. Revise o resultado antes de adotá-lo.

Em uma etapa de brilho, selecione todos os monitores suportados ou um monitor específico. Um monitor selecionado indisponível produz um erro em vez de alterar silenciosamente outro monitor. O painel mostra brilho ao vivo, áudio e leituras OBS quando suas respectivas integrações estão disponíveis; uma leitura indisponível não é apresentada como um valor medido.

No editor de blocos, atribua uma tecla especial física compatível e escolha pressionar, tocar, segurar ou tocar duas vezes imediatamente. Os seletores de camada retidos ativam temporariamente outro mapeamento e restauram a camada anterior ao serem liberados. Gestos e camadas se aplicam às teclas G do SDK Corsair e às teclas especiais padrão específicas do dispositivo, não às teclas de atalho globais comuns. A captura de chave padrão suporta F13–F24 e teclas de mídia do teclado. Os controles proprietários de Fn, perfil e discagem ainda exigem um adaptador de fornecedor compatível ou um mapeamento de fornecedor para uma chave padrão. Raw Input observa a chave e não suprime seu comportamento normal Windows. Os principais diagnósticos explicam problemas de perfil, pausa, conexão e ligação.

A sincronização de perfis troca arquivos por meio de uma pasta que você escolher, incluindo uma pasta gerenciada pelo seu cliente de nuvem existente. Revise a visualização antes de importar; as importações iniciam desabilitadas e removem mapeamentos específicos de hardware. As revisões evitam substituições silenciosas e incluir o conteúdo da ação é uma escolha explícita. MAKRIVO não fornece uma conta na nuvem nem retém credenciais da nuvem. A bandeja agora usa o ícone M verde-azulado do próprio MAKRIVO e mostra um emblema de pausa âmbar quando pausado.
