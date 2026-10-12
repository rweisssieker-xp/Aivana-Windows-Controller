<!-- generated-doc locale=pt-PT source=docs/testing/2026-10-12-local-meeting-assistant.md sha256=56631ca85dca5f785448d12586b0741628728925a649f6124f7aebffb19bb615 -->

> Tradução assistida por máquina. A estrutura, os exemplos de código e as ligações são verificados; isto não constitui revisão por um falante nativo nem aprovação jurídica. Os relatórios históricos mantêm as datas e o âmbito originais. [Fonte original](../../testing/2026-10-12-local-meeting-assistant.md)

# Assistente de reunião local de IA – protótipo

Status: 12. Outubro 2026. Hipótese do agente de resultado ForgeMind, fluxo de reunião limitado. Nenhuma vantagem competitiva comprovada e nenhuma economia de tempo medida sem testes de comparação reais.

## Uso

1. Paleta de Comandos → **KI Assistente de Reunião**. O experimento está desativado por padrão e é redefinido durante a importação da configuração ou exportação portátil.
2. Habilite o assistente local. Ollama deve ser executado separadamente em `127.0.0.1:11434`; um modelo local adequado já deve estar instalado. **Carregue modelos locais** e selecione o modelo. MAKRIVO não instala modelos/serviços, não entra em contato com OpenAI/Cloud API ou usa chaves de API. A configuração offline e a fiabilidade do serviço local Ollama cabe ao utilizador; os modelos remotos/nuvem declarados são rejeitados.
3. Insira o destino e adicione explicitamente programas/documentos/sites. Destino, ID do recurso, nome e tipo são transmitidos; Os caminhos de destino e o conteúdo dos arquivos selecionados não são transmitidos adicionalmente; a própria entrada de destino é enviada literalmente. Os rótulos do site incluem apenas esquema/host, o link completo permanece local.
4. **KI cria rascunho**. Máximo de segundos 90, tokens de saída 1024, tamanho de resposta limitado. Pré-visualização de descarte de alterações de alvo/modelo/recurso; Cancelar/Fechar encerra a solicitação. O modelo só pode sugerir IDs selecionados. IDs desconhecidos, comandos livres, campos JSON adicionais e tipos de documentos executáveis ​​são rejeitados.
5. Verifique a visualização e os pontos abertos → **Confira no editor**. O resultado é inicialmente um rascunho não salvo. Primeiro salve no editor existente e execute-o explicitamente, se necessário. Validação normal de macro, verificação pré/pós, pausa e aborto permanecem responsáveis.

Suportado: iniciar programas sem argumentos, abrir PDF/DOCX/XLSX/PPTX/TXT/MD locais explicitamente selecionados, abrir sites HTTP(S), silenciar o microfone mediante solicitação explícita. Nenhuma alteração padrão do fone de ouvido, nenhuma mensagem, compromisso, download, script, clique gratuito ou entrada de texto. Desejos que não podem ser representados aparecem como pontos em aberto se o modelo os reconhecer corretamente; O usuário deve verificar se os objetivos técnicos foram alcançados. A abertura do documento não é automaticamente verificada tecnicamente.

## Experimento de comparação

**Medir configuração manual · Início / Fim** mede o fluxo de comparação manual completo. As suposições do assistente medem o tempo desde a abertura do assistente até o primeiro salvamento no editor; `generated`, por outro lado, contém apenas a duração do planejamento. Não compare duas durações desiguais para economizar tempo.

Os eventos locais estão na pasta de configuração em `Experiments/meeting-experiment.jsonl`: carimbo de data/hora, tentativa aleatória/ID do bloco, evento, duração, correção sim/não. Sem textos de destino, nomes de recursos, caminhos, respostas de modelo ou conteúdo de documento. O arquivo gira de 2 MiB, um arquivo anterior é retido. Erros de medição não devem anular o salvamento de macros e configurações.

Captura: linha de base manual, geração/erros/anulação de modelo, descarte de alterações/fechamentos de visualização, salvamentos iniciais e correções no editor e estados finais reais do corredor para blocos salvos a partir deles. `run_completed` significa apenas que o executor foi concluído; Os efeitos alvo confirmados são listados separadamente no processo de teste. As repetições da mesma macro não são novas tentativas de configuração independentes. A experiência inicial pode continuar após o reinício porque o ID da experiência permanece no bloco salvo. Os blocos duplicados compartilham essa origem; agrupar por ID de teste ao avaliar.

Primeiro experimento: 20 instalações para reuniões comparáveis ou duas semanas com usuários voluntários de teste; inicialmente uso interno. Compare o tempo de configuração manual versus o tempo de configuração totalmente assistida; Além disso, avalie a taxa de correção/rejeição, erros do corredor e resultados reais de testes. Se não houver melhoria ou se os limites de proteção forem violados, desative a chave. Nenhum uso ou disposição a pagar foi demonstrado no repositório. Sem transmissão automática de telemetria ou ajuste de preços.

## Bancada de testes

- Núcleo: Limites, tipos, IDs desconhecidos/duplicados, exclusão de script, links longos, configuração legada opcional, aceitação de importação/exportação, medição de preservação de dados e descarte único e confirmação correta.
- Transporte com modelo falso: esquema JSON Ollama, destino de loopback, nenhum caminho de arquivo no prompt, nome da nuvem e alias remoto rejeitados, resposta corrompida/muito longa rejeitada, abortado.
- WPF-Smoke: Diálogo renderizado; O envio do rascunho não altera os blocos salvos, revisar/salvar preserva a procedência e a correção dos registros, descartar não salva nada.
- Revisão independente: Visualização descartada e links longos encontrados e corrigidos; Teste de regressão VERMELHO → VERDE.
- Versão de lançamento com zero avisos/erros, conjunto principal inteiro e fumaça WPF bem-sucedida. O conjunto Windows completo existia inicialmente, incluindo mudanças de foco. Outra execução falhou novamente porque a alteração do primeiro plano foi recusada; o teste depende do contexto e permanece inalterado. Os testes Windows restantes, incluindo o transporte do modelo, passam com o teste de sucesso em primeiro plano explicitamente omitido.
- A inspeção do pacote revelou um bug existente na troca atômica de arquivos de configuração abertos brevemente. Um bloqueio de leitura reproduziu o mesmo erro de acesso negado; a repetição limitada apenas da troca atômica é marcada em VERMELHO → VERDE. Erros de acesso permanentes permanecem visíveis.
- Pacote autônomo atual `artifacts/release-20261012-184105`: WPF fumaça bem-sucedida após correção de memória, incluindo diálogo de IA e aceitação/rejeição de rascunho.
- Não é um verdadeiro teste de modelo: o serviço local Ollama não respondeu quando testado. Nenhuma declaração sobre qualidade do modelo, latência, integridade semântica ou economia de tempo. As limitações de hardware/driver/escalamento da versão anterior permanecem.

Fontes de Protocolo: [Ollama Chat](https://docs.ollama.com/api/chat), [Outputas estruturadas](https://docs.ollama.com/capabilities/structured-outputs), [Definições oficiais de tipo, incluindo dados de modelo remoto](https://github.com/ollama/ollama/blob/main/api/types.go).
