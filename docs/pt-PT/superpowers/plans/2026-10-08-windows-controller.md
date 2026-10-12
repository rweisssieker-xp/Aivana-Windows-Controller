<!-- generated-doc locale=pt-PT source=docs/superpowers/plans/2026-10-08-windows-controller.md sha256=10a5ba49bbefc617bce40563c668e3c4628647822117087deba766713fb9b3af -->

> Tradução assistida por máquina. A estrutura, os exemplos de código e as ligações são verificados; isto não constitui revisão por um falante nativo nem aprovação jurídica. Os relatórios históricos mantêm as datas e o âmbito originais. [Fonte original](../../../superpowers/plans/2026-10-08-windows-controller.md)

# Plano de Implementação do Controlador Windows

[English (US)](2026-10-08-windows-controller.md) | [Deutsch](2026-10-08-windows-controller.md) | [Français](2026-10-08-windows-controller.md) | [Italiano](2026-10-08-windows-controller.md)

> Para trabalhadores agentes: utilizar propriedade isolada paralela; o usuário solicitou explicitamente a implementação completa com o máximo de subagentes úteis.

**Meta:** Fornecer o painel de teclas de atalho nativo Windows no design aprovado.
**Arquitetura:** Configuração do núcleo e execução cancelável; Adaptadores Windows; Composição da área de trabalho WPF.
**Pilha tecnológica:** .NET 10, WPF, Win32, JSON. Sem dependências pagas.
**Spec:** ../specs/2026-10-08-windows-controller-design.md

## Restrições Globais
IU alemã; armazenamento local; Windows x64; status verdadeiro da capacidade do hardware; sem registro de teclado; sem inicialização automática padrão.

## Revise o foco
As importações malformadas não devem substituir as configurações salvas; modificadores retidos não devem contaminar a saída; o hardware desconectado não deve travar; o fechamento deve preservar a disponibilidade da bandeja; macros canceladas devem liberar recursos.

## Tarefas e propriedade
- [x] Agente principal: src/Aivana.Core e testes/Aivana.Tests. Modelos, persistência JSON, validação, seleção de perfis e MacroRunner. Execute testes comportamentais com falha e, em seguida, implemente e execute todos os testes principais.
- [x] Agente Windows: src/Aivana.Windows. RegisterHotKey, inventário de dispositivos, SendInput, processo em primeiro plano e adaptador de iluminação Corsair opcional. Verifique os tamanhos de estrutura nativa e a limpeza de registro com testes de fumaça seguros.
- [x] Agente GUI: src/Aivana.Desktop/MainWindow.xaml e .cs mais apenas auxiliares de UI. Painel escuro, editor completo, perfis, dispositivos, configurações, visão geral do teclado e editor de macro. Construa assim que os contratos existirem.
- [x] Pai: contratos compartilhados, arquivos de projeto, composição do DesktopController, ciclo de vida do aplicativo/bandeja, caixas de diálogo de importação/exportação, documentação, verificações de integração, publicação e teste de fumaça do aplicativo em execução.

## Interfaces compartilhadas
Os contratos residem em Core/Models.cs. A implementação do controlador reside em Desktop/DesktopController.cs. A UI chama métodos de controlador por meio dessa classe concreta. Os agentes recebem assinaturas exatas em suas tarefas. Cada um possui arquivos separados. O pai resolve falhas entre projetos depois que todos os agentes terminam.

## Verificação e entrega
- [x] dotnet build Aivana.slnx -c Liberação: zero erros.
- [x] dotnet run --project testes/Aivana.Tests -c Liberação: as verificações comportamentais são aprovadas.
- [x] dotnet publicar src/Aivana.Desktop -c Release -r win-x64 --autocontido true -o artefatos/win-x64.
- [x] Inicie o executável publicado, inspecione a interface do usuário Windows e a estabilidade do processo, exercite o fluxo seguro de perfil/edição/persistência.
- [x] Escreva README com instruções de construção/inicialização e limitações de hardware; verificações de registros e testes restantes apenas de hardware.
