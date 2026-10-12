<!-- generated-doc locale=pt-PT source=docs/testing/2026-10-09-usp-release.md sha256=96a5b23b67032ef07f2f5dd93e9585e7bab0a522e5096ffc9f4d020823c4264e -->

> Tradução assistida por máquina. A estrutura, os exemplos de código e as ligações são verificados; isto não constitui revisão por um falante nativo nem aprovação jurídica. Os relatórios históricos mantêm as datas e o âmbito originais. [Fonte original](../../testing/2026-10-09-usp-release.md)

# Assistência de teclado, sobreposição e feedback de execução

[English (US)](2026-10-09-usp-release.md) | [Deutsch](2026-10-09-usp-release.md) | [Français](2026-10-09-usp-release.md) | [Italiano](2026-10-09-usp-release.md)

Implementadas as três melhorias priorizadas no aplicativo Windows existente:

- Assistente de chave especial: inventário real de dispositivo/chave compatível com iCUE, captura temporária de chave G, verificação de sucesso de configuração, validação de conflito e um rascunho de ação não salvo. Reversão de captura, invalidação de retorno de chamada na fila e restauração de ligação normal cobrem erros e fechamento de diálogo.
- Sobreposição de teclado: geometria de referência física, vinculações de perfil ativo, atualização de perfil/edição ao vivo e realce de tecla Windows comum. Segure Ctrl+Alt+F1 ou alterne pela página da paleta/teclado. Não ativado, clicável e ausente da barra de tarefas.
- Feedback de execução: fases de Espera/Em Execução/Etapa/Concluída/Cancelada/Falha/Rejeitada digitadas, progresso da etapa/repetição e um brinde não ativado. O feedback exclui conteúdo de ação e mensagens de exceção. O cancelamento da última etapa não pode relatar a conclusão. A conclusão descreve a execução, não uma consulta de estado OBS independente.

Verificação:

- 24 Verificações principais aprovadas, incluindo ordenação de fase, resultados de falha/cancelamento, cancelamento da etapa final, isolamento de exceção de ouvinte e exclusão de valores de ação privada.
- A versão da solução de lançamento foi aprovada sem avisos/erros.
- Windows verificações aprovadas; o K100 conectado relatou G1 – G6 e a configuração/liberação nativa foi bem-sucedida. Os endpoints de áudio foram inspecionados sem alterar seu estado.
- WPF smoke passou com todos os layouts de sobreposição de referência, persistência de assistente/catálogo/conflitos/rascunho, configuração de captura nativa ao vivo, encaminhamento de retorno de chamada, rejeição de retorno de chamada obsoleta e restauração de ligação pausada. A entrada de retorno de chamada nativa foi simulada; um pressionamento físico da tecla G continua sendo uma verificação manual.
- A invariância real da janela em primeiro plano foi verificada para o brinde de execução. As propriedades de interação de sobreposição e mostrar/ocultar foram verificadas; capturas de tela foram renderizadas para o assistente, sobreposição e brinde.
- As linguagens 27 contêm todas as cadeias canônicas 819; validação de espaço reservado/chave off-line aprovada.

Os instantâneos e o resultado completo da GUI estão em artefatos/usp-win-x64/smoke. Os testes usam perfis temporários; as configurações de produção são armazenadas em backup e verificadas por hash durante a implantação.
