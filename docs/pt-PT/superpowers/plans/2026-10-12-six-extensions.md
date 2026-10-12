<!-- generated-doc locale=pt-PT source=docs/superpowers/plans/2026-10-12-six-extensions.md sha256=0ac83f2f795ba08551082caee7c775af015828dd39e200dd9857aab504cdba01 -->

> Tradução assistida por máquina. A estrutura, os exemplos de código e as ligações são verificados; isto não constitui revisão por um falante nativo nem aprovação jurídica. Os relatórios históricos mantêm as datas e o âmbito originais. [Fonte original](../../../superpowers/plans/2026-10-12-six-extensions.md)

# Plano de implementação de extensões MAKRIVO

> **Para trabalhadores agentes:** SUB-HABILIDADE NECESSÁRIA: Use superpoderes:planos de execução para execução nativa ou superpoderes:desenvolvimento orientado por subagentes depois que o usuário seleciona a delegação. Acompanhe as etapas com caixas de seleção.

**Meta:** Fornecer seis extensões integradas e suporte de teclado de vários fabricantes baseado em capacidade.

**Arquitetura:** Preservar a separação Core / Windows / WPF. Introduzir processos isolados de plugins e serviços móveis com IPC autenticado restrito; toda a execução passa pelo controlador existente e pela fila do MacroRunner. Os adaptadores de hardware anunciam recursos detectados em vez de suporte inferido para toda a marca.

**Tech Stack: ** C#, .NET 10, WPF, Windows UI Automation, Win32 Raw Input, SDKs de fornecedores documentados, ASP.NET Core HTTPS/WebSocket, System.Text.Json.

**Spec:** `docs/superpowers/specs/2026-10-12-six-extensions-design.md` (aprovado pelo usuário).

## Restrições Globais

- A configuração existente deve ser carregada inalterada; anexe tipos de ação em vez de reordenar seus valores de enumeração persistentes.
- Preservar perfis existentes, atribuições G1/G2, operação local e uso sem conta.
- Raw Input fornece apenas observação. A supressão exclusiva por dispositivo requer um back-end de filtro de teclado real.
- Plug-ins executáveis importados não devem ser executados automaticamente.
- Padrão para desabilitar e restringir interfaces configuradas para o serviço móvel; nenhum substituto de autenticação de texto simples.
- A entrega de atalho/texto não é prova de que o aplicativo de destino aceitou o comando.
- Não substitua o aplicativo instalado, instale drivers, altere mapeamentos reais de teclado ou abra o acesso à rede como um teste incidental.
- Preservar alterações não confirmadas não relacionadas. Não confirme edições compartilhadas existentes como parte deste recurso. Prepare apenas blocos de recursos revisáveis ​​de forma independente se forem solicitadas confirmações.

## Revise o foco

1. Modelos de teclado idênticos duplicados: a reconexão ambígua deve exigir nova seleção (Tarefas 10–12).
2. Cancelamento durante esperas de UIA/provedor/plugin/rede: nenhuma ação adiada após o cancelamento (Tarefas 1, 4, 6, 8).
3. Edições do usuário enquanto a visualização do assistente ou do pacote está aberta: a incompatibilidade de revisão deve impedir a substituição silenciosa (Tarefas 3, 7).
4. O idioma e o layout físico são diferentes: seletores e ligações de código de leitura não devem depender de nomes de exibição localizados (Tarefas 4, 10).
5. Nova tentativa de rede e reinicialização do plugin: os comandos não devem ser executados silenciosamente duas vezes (Tarefas 6, 8).

## Contratos compartilhados e ordem de dependência

As tarefas 1–3 estabelecem execução e configuração. Tarefas 4–5 constroem UIA. As tarefas 6–7 entregam plug-ins. As tarefas 8–9 fornecem acesso móvel. As tarefas 10–12 fornecem adaptadores de teclado e integração de filtros. A tarefa 13 valida o produto completo.

Cada tarefa segue um ciclo de teste de falha → implementação mínima → teste de aprovação. Uma tarefa é concluída somente quando acessível por meio do aplicativo e suas verificações são aprovadas. Andaimes, adaptadores simulados e documentação por si só não satisfazem uma entrega.

### Tarefa 1: Verificação digitada e persistência compatível

**Arquivos:** Criar `src/Aivana.Core/ActionVerification.cs`, `tests/Aivana.Tests/VerificationChecks.cs`; modificar `src/Aivana.Core/Models.cs`, `src/Aivana.Core/MacroRunner.cs`, `src/Aivana.Core/ConfigurationValidator.cs`, `tests/Aivana.Tests/Program.cs`.

**Interfaces:** Produz `VerificationState { Requested, Completed, Verified, Failed, TimedOut, Cancelled, Unverifiable }`, `ActionVerificationResult(VerificationState State, string Code, string Message)`, `IActionVerificationProvider.PreflightAsync(ActionStep, CancellationToken)` e `VerifyAsync(ActionStep, CancellationToken)`, ambos retornando `Task<ActionVerificationResult>`. Adicione `VerificationOptions` serializável opcional com sinalizador habilitado e tempo limite (100–60000 ms), desabilitado por padrão. Publique resultados digitados por etapa, incluindo ID de execução, ID de bloco e índice de etapa sem cargas úteis de ação.

- [] Gravar verificações: JSON legado retém valores numéricos de ação existentes; opções ausentes desabilitam a verificação; tempo limite inválido falha na validação; a falha de comprovação impede a invocação do executor; a pós-condição com falha nunca produz sucesso verificado; o executor não suportado não pode ser verificado; o cancelamento interrompe a verificação e as etapas subsequentes.
- [ ] Execute `dotnet run --project tests/Aivana.Tests -c Release`; confirmar que novas verificações falham antes da implementação.
- [ ] Implementar contratos, validação, clonagem/preservação de snapshots e integração com MacroRunner. Respeite as políticas de parar/continuar do OnError; preservar os assinantes de feedback de execução existentes.
- [] Executar testes principais; exigir que todos os cheques sejam aprovados.

### Tarefa 2: provedores de verificação Windows e UI de resultados

**Arquivos:** Criar `src/Aivana.Windows/WindowsActionVerification.cs`, `src/Aivana.Desktop/MainWindow.Verification.cs`, `tests/Aivana.Windows.Tests/VerificationChecks.cs`; modificar `src/Aivana.Windows/WinActionExecutor.cs`, `src/Aivana.Desktop/DesktopController.Feedback.cs`, `src/Aivana.Desktop/MainWindow.Editor.cs`, testar pontos de entrada.

**Interfaces:** WindowsActionVerification implementa o provedor 1 da tarefa usando clientes existentes de áudio, exibição, processo/janela e OBS. A comprovação testa a disponibilidade e os identificadores; a verificação compara o estado desejado após a pesquisa limitada. Produza instantâneos de resultados do controlador consumidos pela visualização do histórico WPF.

- [] Adicionar testes com falha para IDs de áudio/exibição explícitos indisponíveis, tempo limite OBS, estado observado incompatível, efeitos de atalho desconhecidos, omissão de carga útil sensível e pesquisa cancelada, usando provedores de leitura injetáveis.
- [] Implemente verificação real de leitura para volume absoluto, mudo, brilho absoluto, prontidão de processo/janela e comandos OBS suportados. Ações relativas/de alternância requerem um pré-estado capturado; caso contrário, reporte Inverificável.
- [] Adicione controles de verificação por etapa e histórico de execução segura mostrando estado, requisito com falha e texto de recuperação.
- [] Execute verificações core/Windows e fumaça WPF com provedores de estado injetados; nenhum brilho real ou gravação de áudio.

### Tarefa 3: Criação guiada de espaço de trabalho

**Arquivos:** Criar `src/Aivana.Core/WorkspaceSetup.cs`, `src/Aivana.Windows/WorkspaceDiscovery.cs`, `src/Aivana.Desktop/MainWindow.WorkspaceWizard.cs`, `tests/Aivana.Tests/WorkspaceSetupChecks.cs`; modificar pontos de entrada da paleta e do painel.

**Interfaces:** `WorkspaceSetup.CreateProfile(WorkspaceSelection selection) -> Profile`; a seleção contém ID do modelo, caminhos executáveis ​​escolhidos, URLs, IDs de áudio/exibição, cena OBS e ligações selecionadas. A descoberta retorna candidatos nomeados com IDs e disponibilidade estáveis. Revisão de configuração de registros preliminares para detecção de conflitos.

- [] Adicionar testes com falha para todas as cinco famílias de assistentes solicitadas, etapas geradas de forma independente, alvos necessários ausentes, cancelar sem mutação e salvar na revisão alterada.
- [ ] Descubra aplicativos através de caminhos de aplicativos registrados e atalhos do Menu Iniciar; dispositivos através de provedores existentes; cenas por meio da consulta OBS somente leitura. Mantenha a seleção manual disponível para aplicativos não descobertos.
- [] Implementar Escolher fluxo de trabalho → Selecionar destinos → Escolher controles → Visualizar/validar → Salvar. Gere perfis editáveis ​​e nunca execute durante a configuração.
- [] Adicionar verificações reais do assistente WPF para conclusão/cancelamento/conflito; execute todos os testes.

### Tarefa 4: Seletores UIA e ações limitadas

**Arquivos:** Criar `src/Aivana.Core/UiAutomationAction.cs`, `src/Aivana.Windows/UiAutomationService.cs`, `tests/Aivana.Windows.Tests/UiAutomationChecks.cs`; modificar modelos, validação de configuração e WinActionExecutor.

**Interfaces:** Anexar `ActionKind.UiAutomation`; `UiElementSelector` armazena processo, ID de automação, tipo de controle e critérios de ancestral ordenado. `UiAutomationService.ExecuteAsync(UiAutomationAction action, CancellationToken)` retorna `Task`; operações Invoke, SetValue, Select, Toggle, Focus e WaitState. A resolução do seletor deve retornar exatamente um elemento elegível.

- [] Adicione testes com falha em uma janela de teste dedicada para realocação/redimensionamento, correspondências duplicadas, elementos ausentes, padrões não suportados, controles desabilitados, valores de senha, rótulos Unicode e tempo limite/cancelamento.
- [] Implemente UIA em um trabalhador dedicado com solicitações limitadas e direcionamento de processo. Provedores potencialmente suspensos são executados atrás de um limite de trabalho reinicializável; abandonar uma solicitação não deve deixar efeitos colaterais na fila.
- [ ] Excluir valores de senha; reportar restrições de acesso/integridade em vez de contorná-las.
- [ ] Execute verificações Windows usando equipamentos isolados; anexe apenas valores enum e execute novamente as verificações de persistência herdadas.

### Tarefa 5: Seletor UIA e gravação revisada

**Arquivos:** Criar `src/Aivana.Desktop/MainWindow.UiAutomation.cs`, `src/Aivana.Windows/UiAutomationRecorder.cs`; modificar o catálogo de ações, a integração do editor e do gravador; criar verificações de fumaça UIA WPF.

**Interfaces:** O seletor retorna um `UiElementSelector`; O gravador retorna as etapas UIA propostas e nunca grava diretamente em um perfil salvo. O comando seletor de teste retorna a contagem de correspondências sem invocação.

- [] Adicionar verificações com falha: o seletor exclui MAKRIVO e captura de valor de senha; gravar/cancelar deixa o perfil existente inalterado; contagem de correspondências de relatórios de visualização ambíguos; aceitar etapas registradas cria um rascunho editável.
- [] Implementar destaque/seletor de elemento, parâmetros específicos da operação, visualização do seletor e janela de revisão do gravador. Mantenha o comportamento existente do gravador de atalhos.
- [] Adicionar entradas de catálogo e acesso à paleta; valide os erros do editor antes de salvar.
- [] Execute as verificações de fumaça Windows e WPF no aparelho; inspecione as capturas de tela do seletor/editor.

### Tarefa 6: Host e protocolo de plug-in isolado

**Arquivos:** Criar `src/Aivana.Core/Plugins/PluginContracts.cs`, `src/Aivana.Windows/Plugins/PluginProcessClient.cs`, `src/Aivana.PluginHost/Aivana.PluginHost.csproj`, `src/Aivana.PluginHost/Program.cs`, `tests/Aivana.Tests/PluginProtocolChecks.cs`; modificar o envio de soluções e ações.

**Interfaces:** O manifesto Version-1 especifica ID/versão do pacote, ponto de entrada, esquemas de parâmetros, ações, provedores de estado e declarações de capacidade. As mensagens IPC contêm versão do protocolo, ID de solicitação exclusivo, método, parâmetros JSON e resultado digitado. `PluginProcessClient.InvokeAsync(string actionId, JsonElement parameters, CancellationToken)` retorna um resultado digitado. Anexe `ActionKind.Plugin`.

- [] Adicionar testes com falha para protocolo incompatível, mensagem malformada/superdimensionada (> 1 MiB), ID de solicitação duplicada, cancelamento, falha, processo sem resposta e ação ausente.
- [] Implemente IPC de pipe nomeado do mesmo usuário com handshake de segundo 10 e tempo limite de execução configurável limitado a segundos 60. Demitir o trabalhador que não responde; não reproduza automaticamente uma ação em andamento após a reinicialização.
- [] Aplicar esquemas e serviços de controlador permitidos. Explique a confiança do plugin executável; o isolamento do processo não é uma sandbox do sistema operacional.
- [] Execute testes principais e testes de integração em processos reais de plug-ins de teste.

### Tarefa 7: Pacotes de plug-ins e exemplo de integração

**Arquivos:** Criar `src/Aivana.Core/Plugins/PluginPackage.cs`, `src/Aivana.Desktop/MainWindow.Plugins.cs`, `samples/Aivana.ExamplePlugin/`, testes de pacote; modificar entradas de paleta/configurações.

**Interfaces:** O serviço de pacote expõe `InspectAsync(path)`, `InstallAsync(reviewedPackage, revision)`, `ExportAsync(packageId, destination)`, `DisableAsync(packageId)` e `UninstallAsync(packageId)`. Revisou hashes de registros de pacotes, dependências, versão e recursos declarados.

- [] Adicionar testes com falha para passagem de zip, caminhos absolutos, escape de link simbólico/nova análise, entradas duplicadas, dados expandidos > 100 MiB, incompatibilidade de hash, downgrade, revisão obsoleta e importações desabilitadas.
- [] Implementar visualização e ativação explícita, erros de dependência, instalação em etapas e reversão de atualização; descarregar o trabalhador antes da remoção.
- [ ] Entregue um plugin de exemplo real retornando uma ação de contagem regressiva configurável e estado ao vivo através de IPC, incluindo cancelamento; empacotar/exportar/instalar/desinstalar através da UI.
- [] Execute testes de pacote/host e verificações ponta a ponta WPF. Verifique se nenhum executável importado é executado antes da ativação.

### Tarefa 8: serviço móvel HTTPS e emparelhamento

**Arquivos:** Criar projeto `src/Aivana.MobileHost/` ASP.NET Core, `src/Aivana.Core/MobilePanelContracts.cs`, `src/Aivana.Windows/MobilePanelClient.cs`, testes de integração; modificar solução e controlador.

**Interfaces:** O serviço móvel se comunica com o controlador por meio do IPC do mesmo usuário. Rotas: emparelhamento, perfis/ações permitidas, invocação de ação autenticada, cancelamento e status WebSocket. A invocação inclui um ID de comando exclusivo; controlador possui desduplicação limitada. As credenciais são revogáveis ​​e armazenadas através da proteção Windows.

- [] Adicione testes com falha para enumeração/execução não pareada, código expirado, código reutilizado, token revogado, origem inválida, comando duplicado, quadro superdimensionado e limites de inundação.
- [] Implementar ouvinte somente HTTPS, interfaces selecionadas pelo usuário, instruções de provisionamento/confiança de certificados locais, códigos de emparelhamento únicos de dígitos 6 que expiram após 120 segundos, máximo de cinco tentativas por código e revogação de token. Armazene tokens aleatórios com hash; nunca registre credenciais.
- [] Implementar IDs de ação estável permitidos e roteamento de fila existente; recuse comandos arbitrários de shell, caminhos e instalação de pacotes. Configure limites de taxa explícitos por cliente (comandos 10/s, explosão 30) e limite de mensagens 64 KiB.
- [ ] Verifique testes de integração de loopback sem adicionar regras de firewall ou habilitar acesso à LAN; teste de desligamento e persistência de token.

### Tarefa 9: Painel móvel e controles de serviço de desktop

**Arquivos:** Criar painel responsivo `src/Aivana.MobileHost/wwwroot/` e `src/Aivana.Desktop/MainWindow.MobilePanel.cs`; adicione scripts de verificação de navegador/móvel e cobertura de fumaça WPF.

**Interfaces:** Consumir tarefa 8 somente rotas. Exponha a grade de ação de toque, seletor de perfil, valores/progresso ao vivo, cancelamento, status de conexão e emparelhamento. A área de trabalho controla o início/parada do serviço, instruções de endereço/certificado, perfis permitidos e revogação de clientes emparelhados.

- [] Adicionar verificações de integração com falha para fluxo de autenticação, IDs de ação obsoletos/ausentes, reconectar sem repetição de ação, cliente revogado e parada de serviço.
- [] Implemente UI responsiva acessível usando ativos locais, sem scripts de terceiros ou dependências da Internet. Os tokens nunca aparecem em URLs.
- [ ] Teste o layout do navegador em 360/768/1280 CSS px; inspecione as capturas de tela reais. Verifique com um telefone/tablet real somente depois que o usuário ativar explicitamente o serviço LAN.
- [] Documente e relate a verificação de hardware móvel separadamente da emulação do navegador.

### Tarefa 10: Capacidades do dispositivo e observação de chave comum

**Arquivos:** Criar `src/Aivana.Core/KeyboardCapabilities.cs`, `src/Aivana.Windows/KeyboardAdapterRegistry.cs`, `src/Aivana.Desktop/MainWindow.KeyboardAdapters.cs`; modificar RawKeyboardBindingService, KeyboardBinding, validador e diagnóstico de dispositivo; adicione testes.

**Interfaces:** `IKeyboardAdapter` expõe instantâneos de capacidade, identidade de dispositivo estável e bordas de chave física (código de verificação, sinalizadores estendidos, imprensa/liberação). Os modos distinguem observação, entrada padrão mapeada, SDK nativo e driver exclusivo. As ligações especiais de chave virtual existentes mantêm sua representação e comportamento de migração.

- [] Adicionar testes com falha para ligações antigas, distinções de entrada, independência de idioma/layout, interrupções repetidas, desconectar enquanto segurado, ambiguidade de modelo duplicado e rejeição de entrada injetada.
- [] Estenda a observação às chaves físicas comuns sem suprimi-las. Adicione captura explícita e reconecte o tratamento de identidade.
- [] Mostrar recursos reais do adaptador e status de hardware não testado na interface do usuário do dispositivo; nunca deduza o suporte Fn/dial de marcas.
- [ ] Execute verificações core/Windows/WPF com relatórios simulados e enumeração de dispositivos somente leitura.

### Tarefa 11: Fabricante de adaptadores

**Files:** Crie arquivos separados em `src/Aivana.Windows/KeyboardAdapters/` para LogitechG, RazerChroma, SteelSeriesGameSense, WootingAnalog e orientação genérica QmkVia; modifique o registro/UI e adicione verificações de protocolo do adaptador.

**Interfaces:** Consumir tarefa Contratos do adaptador 10. Bibliotecas nativas são carregadas apenas de locais validados de aplicativos/fornecedores; A ausência do SDK é um recurso indisponível. Os recursos de iluminação e entrada são separados.

- [] Verifique os cabeçalhos oficiais atuais do SDK, os termos de redistribuição, as dependências de software e os modelos suportados antes de implementar cada adaptador; fontes de registro e versões exatas.
- [] Adicione testes com falha para arquitetura ausente/errada do SDK, pressionamento/liberação/repetição de evento, desconexão, resposta malformada e dispositivo não suportado. Use abstrações injetáveis ​​de chamada nativa para verificações determinísticas.
- [] Implementar entrada de tecla G Logitech e iluminação suportada separadamente; Razer Iluminação Chroma mais assistente de mapeamento de teclas padrão Synapse; SteelSeries GameSense estado/iluminação; Wooting polling analógico com limites/histerese configuráveis; QMK/VIA orientação de configuração de chave padrão. Não reivindique informações proprietárias de APIs de iluminação.
- [] Valide dispositivos reais compatíveis, quando disponíveis. Marcar outros implementados/hardware não testados, com evidência de modelo/firmware; faltando SDK utilizável é uma porta externa divulgada.

### Tarefa 12: Backend de filtro de teclado exclusivo

**Arquivos:** Crie `src/Aivana.Windows/KeyboardFilter/`, fonte do instalador em `tools/keyboard-filter/`, testes de integração de back-end e documentação de implantação. Um projeto de driver assinado dedicado será necessário se nenhum back-end licenciado adequado for aprovado na avaliação.

**Interfaces:** Backend relata estado instalado/compatível/assinado; expõe início/parada de captura específica do dispositivo, bordas principais e liberação de emergência. Um watchdog separado restaura a passagem em caso de perda do controlador. O teclado principal nunca se torna exclusivo por padrão.

- [ ] Avalie o licenciamento do driver candidato, a procedência binária assinada e a compatibilidade de inicialização atual do Windows 11/Secure. Registre uma decisão de avançar/não prosseguir. Nenhuma instalação de driver durante testes normais.
- [] Escreva testes de contrato para seleção de alvo de filtro, falha de abertura, expiração de pulsação, perda de IPC, ambigüidade de reconexão, pausa/cancelamento, liberação e descarte de chave retida.
- [] Implementar back-end, UI de seleção/ativação explícita, liberação de emergência e instalador/desinstalador. Não substitua a heurística de temporização de gancho de baixo nível/Raw Input.
- [ ] Validar em máquina de teste recuperável com dois teclados: tecla selecionada suprimida apenas no dispositivo selecionado; digitação principal não afetada; travar/suspender/desinstalar restaura a entrada.
- [ ] Conclua a produção do back-end real assinado/licenciado e dos testes físicos. Se a assinatura/licenciamento externo não estiver disponível, mantenha a tarefa incompleta com evidências precisas de dependência.

### Tarefa 13: Verificação integrada, traduções e pacote de lançamento

**Arquivos:** Adicionar `docs/testing/2026-10-12-six-extensions.md`; atualizar a documentação relevante DE/EN/FR/IT, catálogos de localização e pontos de entrada para verificação de fumaça. Preservar outras edições da documentação.

- [ ] Integrar todos os pontos de entrada e instantâneos; concluir traduções usando o fluxo de trabalho de catálogo existente no idioma 27 e divulgar traduções não revisadas.
- [ ] Execute `dotnet build Aivana.slnx -c Release`, `dotnet run --project tests/Aivana.Tests -c Release`, `dotnet run --project tests/Aivana.Windows.Tests -c Release` e `dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test`.
- [] Inspecione as capturas de tela de desktop/dispositivos móveis e verifique a porcentagem de 100/150/200 de escala Windows, ida e volta de configuração, ciclo de vida do plug-in, revogação de rede e reconexão real do teclado.
- [] Publique em um novo diretório de artefato com `dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/six-extensions/win-x64`; incluem binários de serviço/host complementares e avisos de licença. Não substitua o aplicativo instalado.
- [] Relate testes concluídos, adaptadores de hardware não testados e portas de liberação externas. Todas as seis entregas devem atender aos seus critérios de aceitação antes de concluir toda a solicitação.

## Planeje a autoavaliação

Todas as seis seções de especificações são mapeadas para Tarefas 1–12; A tarefa 13 abrange localização, verificação integrada e liberação. Relatórios de prontidão, serialização de execução, configuração legada, segredos, confiança de pacotes e ambiguidade de dispositivos físicos têm proprietários e testes explícitos. As portas externas de driver/SDK/hardware permanecem incompletas até serem comprovadas, em vez de serem consideradas sucesso de software.

## Escolha de execução para revisão do usuário

Recomendado: execução nativa neste chat, tarefa por tarefa, porque a maioria dos módulos compartilha contratos e o espaço de trabalho atual contém edições existentes substanciais. A execução delegada está disponível se o usuário escolher. Revise este plano e selecione o método de execução antes da implementação, conforme exigido pelo fluxo de trabalho de redação de planos.
