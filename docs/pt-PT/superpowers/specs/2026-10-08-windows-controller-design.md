<!-- generated-doc locale=pt-PT source=docs/superpowers/specs/2026-10-08-windows-controller-design.md sha256=f36d496c6af5190889e9a31487a8fc6afdeba5af18facdd600639ede5ddbd169 -->

> Tradução assistida por máquina. A estrutura, os exemplos de código e as ligações são verificados; isto não constitui revisão por um falante nativo nem aprovação jurídica. Os relatórios históricos mantêm as datas e o âmbito originais. [Fonte original](../../../superpowers/specs/2026-10-08-windows-controller-design.md)

# MAKRIVO – Rascunho

[English (US)](2026-10-08-windows-controller-design.md) | [Deutsch](2026-10-08-windows-controller-design.md) | [Français](2026-10-08-windows-controller-design.md) | [Italiano](2026-10-08-windows-controller-design.md)

Status: 8. Outubro 2026. Status: para revisão antes da implementação.

## Objetivo e escopo acordado

Um aplicativo Windows independente transforma um teclado normal de PC em um painel de ação livremente configurável, semelhante a um deck de stream. O usuário confirmou todos os tipos de ação sugeridos, macros, detecção de dispositivos, perfis e uma boa GUI. Corsair conectado e outros teclados são reconhecidos; funções adicionais do fabricante só são oferecidas se forem realmente suportadas.

Premissas: interface alemã, uso local sem conta, Windows 11 x64 como primeiro alvo, sem instalação de driver. O teclado permanece utilizável para digitação normal. Por padrão, os mapeamentos globais usam atalhos de teclado com modificadores. As teclas de letras individuais não são reservadas globalmente por padrão.

## Decisão técnica

C#/.NET-10-App nativo com WPF e MVVM. O .NET-10-SDK estável está disponível na área de trabalho. WPF permite uma interface de desktop personalizada e integração direta com Windows. As alternativas seriam uma interface web com um serviço Windows separado ou um aplicativo de desktop multiplataforma; ambos trazem tempo de execução adicional e complexidade de comunicação para este projeto Windows. A variante nativa é, portanto, a base escolhida.

Divisão: Projeto desktop para visualizações e bandeja, projeto core para configuração e lógica de execução, projeto Windows para hotkeys, simulação de entrada e dispositivos, projeto de testes para regras independentes de plataforma. Os adaptadores do fabricante dependem de uma interface de capacidade fixa e são independentes das ações padrão.

## superfície

Design escuro com superfícies calmas em cor antracite, fonte fácil de ler e detalhes em turquesa. Navegação de links para painel, perfis, dispositivos e configurações. Perfil ativo, pesquise e alterne para pausar todas as teclas de atalho na parte superior. A interface permanece utilizável na escala 100–200 percentual Windows.

O painel inclui uma grade de blocos personalizável. Cada bloco mostra ícone, nome, tecla de atalho e status de execução. Clicar executa a mesma ação que a tecla de atalho. Um editor é aberto à direita com tipo de ação, parâmetros, gravação de teclas de atalho, símbolo/cor, ativação e botão de teste. Novo, Duplicar, Excluir e Reordenar estão disponíveis. Uma visualização do teclado mostra esquematicamente as atribuições para o layout alemão padrão; não reivindica a geometria exata do dispositivo detectado.

Os perfis podem ser criados, copiados, renomeados e ativados manualmente. Um nome de processo é opcionalmente atribuído a um perfil. Um programa de primeiro plano adequado ativa seu perfil, caso contrário, o perfil padrão global se aplica. Se existirem vários perfis correspondentes, aplica-se uma ordem explicitamente visível. O editor explica os conflitos de teclas de atalho diretamente na tarefa.

## Teclas de atalho e entradas

Windows RegisterHotKey lida com combinações globais normais com repetição de chave suprimida. Apenas o perfil ativo é registrado. Combinações reservadas ou já ocupadas aparecem como erros; o aplicativo não informa que foi ativado com sucesso. A gravação no editor não aciona nenhuma ação.

Antes das entradas simuladas, as teclas modificadoras de disparo são liberadas ou aguarda-se que sejam liberadas fisicamente. Texto Unicode e combinações de teclas são enviados usando SendInput. Cada tecla pressionada será liberada novamente, mesmo se você cancelar. As ações são executadas fora do thread da UI; A execução e as alterações de perfil não devem bloquear a superfície.

## Ações e macros

Tipos de ação: iniciar o programa com argumentos e diretório de trabalho, abrir arquivo/pasta via Windows, abrir site HTTP/HTTPS, inserir texto Unicode, enviar combinação de teclas, reproduzir/pausar mídia/próximo/anterior/parar, aumentar/diminuir volume/mudo e alternar perfil. Os parâmetros são validados antes de salvar e novamente antes da execução. Os programas são iniciados diretamente; Os argumentos não são interpretados implicitamente como comandos shell.

As macros contêm uma lista ordenada de ações nomeadas e etapas de pausa explícitas. As etapas podem ser adicionadas, editadas e movidas. As repetições são finalmente configuráveis ​​(1–100); Loops infinitos não fazem parte da primeira versão. Uma macro em execução pode ser encerrada através da interface ou de sua própria tecla de atalho de cancelamento. O acionamento repetido da mesma atribuição atual não inicia uma execução paralela. Diferentes macros com simulação de entrada são serializadas para que seus pressionamentos de tecla não se misturem. Se ocorrer um erro de etapa, a macro para e exibe a etapa afetada.

## Dispositivos e fabricantes

Inventário de dispositivos via Windows Raw Input e informações adicionais do dispositivo: nome de exibição, caminho do dispositivo, identificador do fabricante/produto, se disponível, status da conexão e recursos detectados. Conectar/desconectar atualiza a lista sem reiniciar. Um dispositivo físico pode ter diversas entradas HID; Estes são agrupados de acordo com identificadores confiáveis, caso contrário, são mantidos visivelmente como entradas separadas.

As teclas de atalho normais se aplicam aos teclados. A visualização do dispositivo fornece informações e recursos; um teclado separado com teclas exclusivamente interceptadas não faz parte deste modo acordado.

A detecção Corsair também funciona sem iCUE. Um adaptador SDK iCUE opcional fornece status de conexão e ações de iluminação suportadas quando SDK, iCUE e dispositivo compatível estão presentes. A IU diferencia o SDK reconhecido, disponível, conectado e realmente controlável. O SDK documenta principalmente o controle de LED; As teclas G proprietárias só são suportadas como teclas de atalho normais se Windows receber a entrada apropriada. Não existe uma promessa geral para todas as chaves especiais.

Outros fabricantes recebem inicialmente detecção genérica e ações padrão. A interface do adaptador permite integrações posteriores; habilidades não suportadas são exibidas honestamente. Os testes de hardware para modelos inexistentes não são considerados aprovados.

## Armazenamento e vida cotidiana

Configuração JSON versionada no diretório de dados do aplicativo local. Salvamento atômico com último backup válido; arquivos incorretos não são sobrescritos silenciosamente. A importação verifica formato, versão, IDs e parâmetros e mostra uma prévia antes da aplicação. As ações importadas só podem ser executadas após ativação explícita. A exportação contém perfis e atribuições.

Menu da bandeja: Abrir, seleção de perfil, pausar teclas de atalho, cancelar macros, sair. Fechar janela é minimizada na bandeja por padrão e explica isso na primeira vez. A inicialização automática é opcional e desativada por padrão. Apenas uma instância gerencia teclas de atalho. Exit registra teclas de atalho, aborta execuções e libera recursos.

Visualização de atividades locais com histórico limitado de sucesso, falha e cancelamento. Nenhum registro de todas as teclas digitadas. O conteúdo da ação de texto não é explicado nas mensagens de erro. Sem serviço de nuvem e sem telemetria.

## Tratamento de erros e limites

Programas ausentes, URLs inválidos, teclas de atalho ocupadas, perfis danificados e SDK do fabricante inacessível recebem mensagens específicas em alemão. Uma falha no adaptador não afeta as ações padrão. As entradas em aplicativos com privilégios mais elevados podem falhar devido aos limites de permissão Windows; Os erros são explicados, o aplicativo não requer direitos de administrador por padrão.

## Teste e aceitação

Verificações automatizadas de prioridade de perfil, detecção de conflitos, importação de configuração, armazenamento atômico, ordenação de macro, nova tentativa, cancelamento e propagação de erros. Windows testes de fumaça para teclas de atalho reais, atalhos de texto e teclado, ações de mídia, inventário de dispositivos, bandeja e saída limpa. Nenhum teste que inicie acidentalmente programas ou sites arbitrários.

Verificação de GUI: Criar e editar atribuição, gravação sem execução, clique/tecla de atalho com o mesmo efeito, cancelamento de macro, mensagem de conflito, alteração de perfil, visualização de dispositivo, importação/exportação e dimensionamento. Os testes de hardware específicos do fabricante são documentados separadamente do dispositivo real.

Entrega: Código fonte, instruções de construção/inicialização reproduzíveis, manual do usuário em alemão e Windows-x64-Publish-Artefakt local. Sucesso significa um aplicativo executável com todos os recursos genéricos; Os recursos Corsair são marcados como disponíveis apenas com suporte comprovado a SDK/dispositivo.

## Fontes técnicas

- Microsoft Entrada de teclado / RegisterHotKey: https://learn.microsoft.com/en-us/windows/win32/inputdev/about-keyboard-input
- Microsoft Raw Input: https://learn.microsoft.com/en-us/windows/win32/inputdev/about-raw-input
- Microsoft WPF Entrada: https://learn.microsoft.com/dotnet/desktop/wpf/advanced/input-overview
- Corsair iCUE SDK oficial: https://github.com/CorsairOfficial/cue-sdk/blob/master/README.md

## Próxima etapa

Depois de revisar este rascunho: crie um plano de implementação concreto e envie-o para seleção de implementação. Ainda não há repositório Git na pasta de trabalho vazia; Este documento ainda não foi, portanto, autorizado.
