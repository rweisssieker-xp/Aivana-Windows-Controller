<!-- generated-doc locale=pt-PT source=docs/testing/2026-10-12-github-publication.md sha256=ab4d40cd12047e2cdacad359a12febe16d5689a82f4f792b484b3d4e2430e02a -->

> Tradução assistida por máquina. A estrutura, os exemplos de código e as ligações são verificados; isto não constitui revisão por um falante nativo nem aprovação jurídica. Os relatórios históricos mantêm as datas e o âmbito originais. [Fonte original](../../testing/2026-10-12-github-publication.md)

# GitHub lançamento do status de desenvolvimento MAKRIVO

Status: 12. Outubro 2026. Liberação do código-fonte, não uma versão de varejo assinada.

- Compilação de versão da solução: bem-sucedida, zero avisos e erros.
- Exames básicos: bem sucedido.
- Teste de fumaça WPF: bem-sucedido; novo relatório de resultados na pasta de construção local.
- Primeiro conjunto Windows completo: janela de primeiro plano, áudio, tela e verificações iCUE passam; então, a resposta do trabalhador UI Automation atinge o tempo limite.
- Nova tentativa direcionada `--extensions-only`: bem-sucedida, incluindo automação da interface do usuário, ambiguidade, alvos desativados, exclusão de senha, processo de plug-in e host móvel HTTPS real. O tempo limite original não foi obscurecido por uma alteração no tempo limite; a causa ainda não foi comprovada de forma conclusiva.
- Nova execução Windows completa subsequente: bem-sucedida, incluindo janela de primeiro plano e todas as verificações de extensão. Uma nova execução bem-sucedida não exclui tempos limite esporádicos.
- Antes da publicação, nenhuma chave privada típica ou chave produtiva Stripe/GitHub/AWS foi encontrada no inventário de origem auditado. Esta verificação de amostra limitada não é uma verificação secreta completa.
- Os artefatos de construção e os dados de orquestração local não são publicados.

Funções e descontos restantes: [Featural Lista](../FEATURES.md). Os relatórios de teste anteriores mantêm sua validade temporária; eles não são uma garantia para qualquer hardware ou contexto de escritório.
