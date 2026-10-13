<!-- generated-doc locale=pt-PT source=README.md sha256=eb6f3fe7810ed3e10793e8938e908e151a84c4afbea2f22cba2656bfe4e8dfc3 -->

> Tradução assistida por máquina. A estrutura, os exemplos de código e as ligações são verificados; isto não constitui revisão por um falante nativo nem aprovação jurídica. Os relatórios históricos mantêm as datas e o âmbito originais. [Fonte original](../../README.md)

# MAKRIVO

[Documentação completa em todas as 27 línguas](README.md) · [Manual de usuário completo](HANDBOOK.md)

**por Aivana.** [Inventário atual de funcionalidades e limitações da publicação](FEATURES.md) · [Relatório de Extensão](testing/2026-10-12-six-extensions.md) · [Protótipo de assistente de IA local](testing/2026-10-12-local-meeting-assistant.md)

**Inglês (EUA)** | [Deutsch](../de-DE/REFERENCE.md) | [Français](../fr-FR/REFERENCE.md) | [Italiano](../it-IT/REFERENCE.md)

Um controlador de teclas de atalho Windows local com blocos, perfis, macros e detecção de teclado no estilo Stream Deck. A interface suporta idiomas 27. Nenhuma conta ou conexão com a nuvem é necessária para o uso diário.

[Documentação](README.md) · [Verificação](VERIFICATION.md) · [Relatório de testes](testing/2026-10-09-usp-release.md)

## Primeiros passos

Após a publicação, a aplicação encontra-se em `artifacts/win-x64/MAKRIVO.exe`. Mantenha toda a pasta de saída junta; pode copiá-la para outro local. Windows 11 x64 é a plataforma inicial. Os artefactos de compilação são excluídos do Git; uma cópia do repositório de código-fonte não inclui um executável pré-compilado.

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

Os perfis específicos da aplicação usam um nome de processo sem o caminho completo. A ordem dos perfis resolve os empates; o perfil predefinido é usado como alternativa. A seleção automática de perfis tem de ser ativada nas Definições.

Fechar a janela principal mantém a aplicação na área de notificação por predefinição. Faça duplo clique no respetivo ícone para a reabrir; use o menu do ícone para sair. O arranque com Windows é opcional e está desativado por predefinição. Os atalhos podem ser colocados em pausa de forma centralizada.

## Ações e paleta de comandos

Add Action abre um catálogo pesquisável com predefinições 90 em oito categorias. Cada um tem uma descrição e visualização. O editor de macro usa o mesmo catálogo para adicionar ou substituir etapas. Selecionar uma predefinição neste fluxo de trabalho do editor cria um rascunho sem execução.

A paleta de comandos abre-se pelo cabeçalho ou com Ctrl+K na aplicação. Pesquisa ações guardadas em todos os perfis, predefinições do catálogo, mudanças de perfil, navegação, pausa/retoma, cancelamento de macros e importação/exportação. Filtre por ações guardadas, funções do catálogo ou comandos da aplicação. As setas selecionam, Enter ativa, Ctrl+Enter edita uma entrada editável e Escape fecha. Os favoritos aparecem primeiro. Os comandos do catálogo prontos a usar podem ser executados imediatamente; as predefinições que exigem configuração abrem um rascunho. As Definições permitem definir um atalho global para a paleta. As novas configurações usam Ctrl+Shift+Space; as existentes não recebem automaticamente um novo atalho global.

As ações de áudio definem com precisão o volume de 0 a 100 por cento e silenciam, reativam ou alternam o silêncio dos dispositivos de saída e microfone. Escolha um dispositivo específico ou deixe a seleção vazia para usar o predefinido do Windows. Um dispositivo guardado indisponível gera um erro em vez de controlar outro dispositivo. Atualizar dispositivos volta a carregar os dispositivos ligados.

OBS Studio usa WebSocket v5: iniciar/parar/alternar/pausar/retomar a gravação; iniciar/parar/alternar a transmissão; mudar cenas; alternar o silêncio de uma entrada. Ative o servidor WebSocket em Ferramentas no OBS e defina o ponto de ligação no editor. Verificar ligação e carregar nomes lê a versão do OBS, as cenas e as entradas sem iniciar uma gravação ou transmissão. Escolha os nomes na lista ou introduza-os manualmente. As palavras-passe podem ser guardadas no Windows Credential Manager e são excluídas das configurações exportadas. As ligações OBS indisponíveis apresentam um erro.

Os perfis podem corresponder a um nome de processo e a parte do título de uma janela. Ambas as condições especificadas têm de corresponder. Bloquear perfil impede a mudança automática de perfil durante a sessão. Capturar condições da janela em primeiro plano usa a última aplicação externa que teve o foco.

As ações de texto e área de transferência suportam `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` e `{{user}}`. As datas usam `yyyy-MM-dd`; as horas usam `HH:mm`. Os marcadores desconhecidos mantêm-se inalterados. `{{profile}}` identifica o perfil da ação iniciada; `{{process}}` identifica o contexto da aplicação externa. O conteúdo da área de transferência só é lido quando esse marcador é usado. A ação da área de transferência escreve texto, mas não o cola automaticamente.

As ações da janela minimizam, maximizam, restauram ou organizam uma janela no lado esquerdo/direito da área de trabalho do monitor. A ação do sistema bloqueia Windows. Essas ações são executadas somente quando invocadas explicitamente.

A visualização mostra etapas, repetições e variáveis sem execução. Desfazer e Refazer retêm até 100 estados de rascunho; as caixas de texto mantêm a edição nativa de Ctrl + Z. Marque os favoritos no editor ou no menu lado a lado e filtre-os no painel. As contagens de execução aplicam-se apenas à sessão atual.

Ações de texto, atalho e janela iniciadas na interface ativam a última janela de destino externa. A paleta exibe esse contexto de destino. Se o destino for fechado ou Windows recusar a ativação, a execução será interrompida com uma mensagem.

## Assistência e feedback do teclado

O assistente de teclas especiais está na página Teclado e na paleta. Lista as teclas G comunicadas por iCUE. Detetar pressão de tecla captura-as temporariamente sem executar as ações atribuídas. As associações normais são restauradas após a deteção, o cancelamento ou o fecho. Uma função do catálogo e uma tecla selecionada abrem-se como rascunho por guardar; são verificadas associações duplicadas.

A sobreposição do teclado mostra a disposição de referência selecionada e as associações ativas. Mantenha Ctrl+Alt+F1 premido ou ative/desative a sobreposição pela paleta ou pela página Teclado. Não toma o foco e deixa passar os cliques do rato. As mudanças de perfil e associações são atualizadas enquanto está visível; as pressões normais de teclas Windows são destacadas. Os controlos específicos do fabricante poderão não fornecer um sinal de tecla Windows equivalente.

O feedback de execução mostra espera, progresso de etapa/repetição, conclusão, cancelamento ou falha sem tirar o foco ou expor o texto inserido. Comandos executados confirma a execução do comando, e não um estado ativo consultado de forma independente em um aplicativo de terceiros. Os detalhes do erro aparecem no log de atividades.

## Dispositivos e Corsair

Corsair G1–G6 podem ser atribuídas diretamente através do SDK oficial iCUE. Selecione o dispositivo e a tecla no editor de ações e guarde, ou prepare um rascunho a partir da vista do teclado K100. As associações aplicam-se ao dispositivo selecionado e ao perfil ativo; a pausa também as desativa. iCUE tem de estar em execução e permitir o acesso. Fn, a roda iCUE e os controlos de perfil e bloqueio não podem ser mapeados através desta API G-key.

A página Dispositivos lista as interfaces do teclado Windows e é atualizada periodicamente e quando os dispositivos são conectados. Um teclado físico pode expor múltiplas interfaces HID. As teclas de atalho globais padrão funcionam entre fabricantes.

A detecção Corsair usa informações disponíveis do dispositivo. O controle RGB requer hardware compatível, executando iCUE e o SDK iCUE oficial; o aplicativo informa a disponibilidade real. Outros fabricantes recebem atualmente detecção e ações padrão. Fora da integração da chave G Corsair, os controles proprietários funcionam quando Windows os recebe como entrada regular. O aplicativo não instala um driver de teclado.

## Idiomas

As configurações oferecem os idiomas 27: alemão, inglês, francês, espanhol, italiano, português, holandês, polonês, tcheco, eslovaco, húngaro, romeno, búlgaro, grego, dinamarquês, sueco, norueguês, finlandês, estoniano, letão, lituano, esloveno, croata, ucraniano, russo, turco e sérvio em escrita latina. Automático segue o idioma de exibição Windows. As traduções são agrupadas localmente. Layouts, teclas de atalho e nomes criados pelo usuário são independentes do idioma da interface.

A documentação completa do repositório está disponível em todos os idiomas de aplicação 27 através do [Índice de Língua](README.md). As traduções são assistidas por máquina, com ligações às fontes originais e verificações estruturais; a revisão por falantes nativos é separada. A aplicação também aceita os códigos regionais `en-US`, `de-DE`, `fr-FR` e `it-IT` e usa os respetivos formatos numéricos. Os códigos de idioma existentes mantêm-se compatíveis.

## Layouts e pesquisa de fabricante

A vista do teclado oferece referências ISO-DE/ANSI-US completas e sem teclado numérico, além de disposições Corsair K100 RGB. A geometria representa posições, teclas largas/altas e a tecla Enter ISO. As disposições comuns são referências genéricas. As disposições K100 seguem os controlos documentados sem afirmar dimensões exatas ao milímetro. Escolha a variante correta de idioma/geometria: um nome de modelo ou um VID/PID USB não determina uma variante específica.

O mapeamento resolve os códigos de varrimento físicos com a disposição de entrada Windows ativa. O fluxo prepara um rascunho e mantém as verificações de gravação e conflitos. As associações comuns são atalhos globais Windows, não associações isoladas num único teclado ligado. Corsair G1–G6 usam a integração iCUE específica do dispositivo. Os outros controlos proprietários requerem um adaptador ou atalhos configurados pelo fabricante. O Enter principal e o do teclado numérico partilham uma identidade de tecla virtual para os atalhos globais.

O [Pesquisa do fabricante](research/tastaturhersteller-sonderfunktionen.md) e [CSV](research/tastaturhersteller-sonderfunktionen.csv) abrangem 74 grupos de marcas/fabricantes, distinguindo configuradores, SDK de entrada e API de iluminação. [Documentação das disposições](research/keyboard-layout-evidence.md) documenta as diferenças entre modelos, referências SKU e módulos. A cobertura da investigação não significa que o suporte de equipamento esteja implementado.

## Dados

A configuração é armazenada nos dados do aplicativo local. Importação/exportação usa JSON. A importação visualiza a configuração, substitui a configuração atual após a confirmação e desativa as ligações importadas até serem ativadas manualmente. O conteúdo de ação de texto não é coletado como um registro de pressionamento de tecla. As senhas do Credential Manager são separadas dos perfis exportados.

## Verificação

```powershell
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
```

As verificações Windows podem inspecionar o hardware e configurar/liberar temporariamente ligações iCUE suportadas. Execute-os sem outro controlador usando essas ligações. As verificações de endpoint de áudio não alteram as configurações de volume ao vivo ou mudo.

A entrada nativa não pode contornar os privilégios Windows. Um controlador iniciado normalmente poderá não conseguir enviar entradas para uma aplicação executada como administrador. Os atalhos reservados ou ocupados aparecem como conflitos.

O [Indicadores de Documentação](README.md) liga a conceções históricas, planos, investigação e relatórios de testes datados. Os planos históricos descrevem o âmbito registado na altura, não uma garantia atual de suporte de funcionalidades.

### Modelos de fluxo de trabalho e esperas de janela de destino

O catálogo inclui seis fluxos de trabalho editáveis: espaço de trabalho, reunião, streaming, pesquisa, troca de projeto e trecho de texto. Procure `workflow.` no catálogo ou na paleta de comandos. A seleção de um fluxo de trabalho abre um rascunho não salvo; ele nunca é executado imediatamente. Todas as etapas são mantidas ao adicionar um fluxo de trabalho a uma macro existente ou ao atribuí-lo a uma chave Corsair. Configure URLs de exemplo, caminhos de programa, perfis e cenas OBS antes de salvar.

O passo **Esperar pela janela de destino** aceita um nome de processo (com ou sem `.exe`), uma subcadeia do título sem distinguir maiúsculas e minúsculas e um tempo limite de 1–600000 ms. É necessário pelo menos um filtro; ambos têm de corresponder se forem fornecidos. A macro continua apenas quando uma janela visível correspondente responde e passa para primeiro plano. Use um título específico quando várias janelas partilharem um processo. O cancelamento ou o fim do tempo limite para os passos seguintes. Uma janela pronta não garante que um documento esteja carregado ou que um serviço externo como OBS WebSocket esteja ligado.

Brilho da tela: pesquise **Aumentar o brilho da tela**, **Diminuir o brilho da tela** ou **Brilho da tela em 50 por cento** no catálogo de ações. As ações relativas alteram o brilho em pontos percentuais 10; os valores fixos variam de 0 a 100. Os monitores internos suportados usam WMI; monitores externos compatíveis requerem DDC/CI ativado em seu menu. Por padrão, a ação afeta todos os monitores suportados; um monitor explícito pode ser selecionado no editor. Conexões e docks podem restringir o suporte.

### Controles estendidos

Abra a paleta de comandos com **Ctrl+K quando a janela da aplicação tiver o foco**. Procure **Designer de fluxos de trabalho**, **Gravar macro**, **Sincronizar perfis** ou **Diagnóstico de teclas** (o texto depende do idioma selecionado). O designer edita o rascunho do mosaico selecionado: condições de ficheiro/processo/janela, número de passos a saltar para a frente e paragem/continuação em caso de erro. O gravador captura combinações de teclas de atalho e esperas, não a escrita normal; Escape para a gravação. Reveja o resultado antes de o adotar.

Em uma etapa de brilho, selecione todos os monitores suportados ou um monitor específico. Um monitor selecionado indisponível produz um erro em vez de alterar silenciosamente outro monitor. O painel mostra brilho ao vivo, áudio e leituras OBS quando suas respectivas integrações estão disponíveis; uma leitura indisponível não é apresentada como um valor medido.

No editor de mosaicos, atribua uma tecla especial física suportada e escolha pressão imediata, toque, pressão prolongada ou duplo toque. Os seletores de camada mantidos premidos ativam temporariamente outro mapeamento e restauram a camada anterior ao serem libertados. Os gestos e as camadas aplicam-se às teclas G do SDK Corsair e às teclas especiais comuns específicas do dispositivo, não aos atalhos globais comuns. A captura de teclas comuns suporta F13–F24 e as teclas multimédia. Os controlos proprietários Fn, perfil e roda continuam a exigir um adaptador compatível do fabricante ou um mapeamento do fabricante para uma tecla comum. Raw Input observa a tecla e não suprime o seu comportamento normal no Windows. O diagnóstico de teclas explica problemas de perfil, pausa, ligação e associação.

A sincronização de perfis troca arquivos por meio de uma pasta que você escolher, incluindo uma pasta gerenciada pelo seu cliente de nuvem existente. Revise a visualização antes de importar; as importações iniciam desabilitadas e removem mapeamentos específicos de hardware. As revisões evitam substituições silenciosas e incluir o conteúdo da ação é uma escolha explícita. MAKRIVO não fornece uma conta na nuvem nem retém credenciais da nuvem. A bandeja agora usa o ícone M verde-azulado do próprio MAKRIVO e mostra um emblema de pausa âmbar quando pausado.
