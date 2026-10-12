<!-- generated-doc locale=pt-PT source=docs/superpowers/specs/2026-10-12-controller-usps-design.md sha256=4c3b2dbedb83af6bd48917515ae6559012afb6318ce69d1e32fb2765e0793327 -->

> Tradução assistida por máquina. A estrutura, os exemplos de código e as ligações são verificados; isto não constitui revisão por um falante nativo nem aprovação jurídica. Os relatórios históricos mantêm as datas e o âmbito originais. [Fonte original](../../../superpowers/specs/2026-10-12-controller-usps-design.md)

# Extensões de controle MAKRIVO

O usuário solicitou todas as nove melhorias propostas e um ícone reconhecível na bandeja. As configurações existentes, especialmente o brilho do G1/G2, devem permanecer compatíveis. As edições existentes de identidade visual e documentação não confirmadas pertencem ao espaço de trabalho compartilhado e devem ser preservadas.

## Entregáveis e limites

1. As ações de brilho selecionam todos os monitores suportados ou um ID de monitor específico estável. A falta de alvos explícitos falha visivelmente. O painel mostra o brilho atual, o volume de saída, o estado do microfone e o status OBS por meio de leituras assíncronas limitadas.
2. As teclas especiais suportam pressionar, tocar, segurar e tocar duas vezes imediatamente, com supressão de repetição. Um seletor de camada retido altera temporariamente o mapeamento ativo e a liberação o restaura. As ligações imediatas existentes mantêm seu comportamento. Pausar, alterar perfil, desconectar e cancelar gestos pendentes.
3. O designer de fluxo de trabalho edita condições, encaminha ramificações e interrompe/continua a política de falha transacionalmente em um rascunho de bloco. As condições abrangem arquivos, processos em execução e títulos de janelas. Sem ciclos de salto ilimitados.
4. A gravação explícita captura acordes de atalho e espera, ignora a entrada injetada e para no Escape. As ações registradas são revisadas antes da adoção; o texto comum não é gravado.
5. Chaves especiais padrão por dispositivo de outros fabricantes usam Windows Raw Input. As chaves proprietárias exigem uma API de fornecedor compatível ou mapeamento de fornecedor para uma chave padrão; funcionalidade não suportada é mostrada honestamente. O diagnóstico explica perfil, pausa, conflitos de ligação e estado real do adaptador.
6. A troca de perfis usa uma pasta compartilhada selecionada pelo usuário (incluindo pastas sincronizadas por um cliente de nuvem existente). As verificações de revisão evitam substituições silenciosas. Pull visualiza alterações e importa ações desabilitadas com mapeamentos de hardware removidos. A inclusão do conteúdo da ação é explícita. Nenhuma credencial de nuvem é armazenada pelo MAKRIVO.
7. As strings de UI pertencem aos catálogos existentes na linguagem 27; DE/EN/FR/IT continuam sendo os idiomas de documentação mínimos revisados. As lacunas de tradução devem ser divulgadas em vez de declaradas completas.
8. Uma marca reconhecível de teclado verde-azulado M substitui o ícone genérico da bandeja Windows. O modo pausado possui um emblema de pausa âmbar. A marca do executável/janela usa a mesma marca. A dica de ferramenta inclui estado e perfil.

## Verificação

As verificações principais cobrem compatibilidade com versões anteriores, instantâneos, cancelamento, temporização de gestos, ramificações/erros de fluxo de trabalho, filtragem de gravador e conflitos de sincronização. As verificações Windows cobrem enumeração de exibição somente leitura, eventos de lançamento do SDK e análise Raw Input. As verificações de fumaça WPF cobrem novos pontos de entrada e recursos sem alterar o brilho real do monitor, digitar em outro aplicativo ou substituir perfis de usuário. A implementação preserva um backup e uma configuração G1/G2.
