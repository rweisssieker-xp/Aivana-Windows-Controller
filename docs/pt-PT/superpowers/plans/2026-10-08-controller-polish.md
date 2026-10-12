<!-- generated-doc locale=pt-PT source=docs/superpowers/plans/2026-10-08-controller-polish.md sha256=1a076a67eea31048f6305f30a121c474d592451d1ddcd9b46bc339fd8ec068f2 -->

> Tradução assistida por máquina. A estrutura, os exemplos de código e as ligações são verificados; isto não constitui revisão por um falante nativo nem aprovação jurídica. Os relatórios históricos mantêm as datas e o âmbito originais. [Fonte original](../../../superpowers/plans/2026-10-08-controller-polish.md)

# Usabilidade do controlador e plano de melhoria de capacidade

[English (US)](2026-10-08-controller-polish.md) | [Deutsch](2026-10-08-controller-polish.md) | [Français](2026-10-08-controller-polish.md) | [Italiano](2026-10-08-controller-polish.md)

Solicitação do usuário: melhorar aparência, usabilidade e funcionalidade; a autorização prévia solicita o máximo de subagentes úteis em modelos menores necessários e preenchimento autônomo.

## Escopo
Preservar perfis existentes e integração funcional do SDK Corsair. Melhore o aplicativo WPF existente em vez de criar um protótipo separado.

- [x] Trabalhador de shell/painel: layout responsivo, navegação ativa, controles legíveis, atividade/status compacto, reordenação de blocos de arrastar e soltar, estados e modelos vazios úteis. Possui MainWindow.xaml, MainWindow.xaml.cs e MainWindow.Dashboard.cs.
- [x] Trabalhador do editor: MainWindow.Editor.cs e arquivos parciais opcionais somente para editor. Edições explícitas em etapas, salvar/descartar, validação em linha, lista de macros dedicada com arrastar e soltar, etapas duplicadas, captura confiável de teclas de atalho e campos de arquivo/URL.
- [x] Trabalhador Windows: validação nativa, nomes de dispositivos robustos e recursos de SDK, esperas de modificadores limitados e injeção segura; próprios testes Windows e Windows.
- [x] Pai: progresso de execução principal, interfaces de controlador, preservação de configuração, testes de integração, capturas de tela renderizadas reais, publicação/pacote e substituição de aplicativo em execução.

## Contrato de interface
O controlador fornece APIs existentes mais string LastStatus, bool HasRunningActions, bool IsRunning(Guid), string GetTileStatus(Guid), string ForegroundProcess, bool CorsairAvailable, IReadOnlyList<CorsairKeyboardInfo> CorsairKeyboards, Task TestCorsairColorAsync(string), void OpenIcue(). Editor expõe bool HasUnsavedChanges, void DiscardDraft(), bool CommitDraft(); os nomes de campos/métodos existentes permanecem disponíveis nas classes parciais.

## Aceitação
Nenhuma redefinição da configuração real do usuário; alterações preparadas válidas são salvas uma vez, rascunhos inválidos permanecem editáveis, descarte restaura dados salvos. Aliases de teclas de atalho nativas validadas sem injetar entradas de teste em janelas não relacionadas. Reordenação de blocos e macros testada; todas as visualizações são renderizadas em tamanhos de desktop; status em execução/falha/concluído visível; modelo real do dispositivo SDK exibido. Build/testes e aprovação de fumaça publicada, e aplicativo atualizado é lançado.
