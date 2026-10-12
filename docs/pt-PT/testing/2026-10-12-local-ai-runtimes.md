<!-- generated-doc locale=pt-PT source=docs/testing/2026-10-12-local-ai-runtimes.md sha256=3dfbb80761aad758ea6a9307999ff530dfdd4a0cf685804e15052508cf107fd7 -->

> Tradução assistida por máquina. A estrutura, os exemplos de código e as ligações são verificados; isto não constitui revisão por um falante nativo nem aprovação jurídica. Os relatórios históricos mantêm as datas e o âmbito originais. [Fonte original](../../testing/2026-10-12-local-ai-runtimes.md)

# Tempos de execução locais de IA

## Serviço

Ferramentas → Abra o assistente de reunião AI, ative-o explicitamente e selecione o tempo de execução AI. Libere recursos, formule uma meta, crie um rascunho e confira no editor. Nenhum provedor executa o design automaticamente. Não há mudança automática de provedor.

| Duração | Requisito | conexão |
|---|---|---|
| Ollama | modelo instalado localmente | 127.0.0.1:11434 |
| LM Studio | executando servidor local com modelo carregado localmente e suporte a esquema JSON | 127.0.0.1:1234 |
| llama.cpp | executando o servidor lhama com modelo local e suporte ao esquema JSON | 127.0.0.1:8080 |
| CPU ONNX Tempo de execução GenAI | pasta de modelo de texto local GenAI compatível e confiável | processo agrupado separado, sem HTTP |

Os clientes HTTP não utilizam proxies e não seguem redirecionamentos. Os próprios servidores locais podem endereçar serviços adicionais; Em particular, o LM Link/Remote Routing não pode ser usado para processamento local. MAKRIVO não consegue detectar isso de forma confiável por meio da interface compatível. Ollama metadados remotos e nomes de nuvens também são rejeitados. Servidores locais autenticados não podem ser configurados nesta versão.

## ONNX

No diálogo genai_config.json selecione. a pasta deve conter tokenizer.json e um modelo de bate-papo. chat_template é suportado como uma cadeia de caracteres, um modelo padrão nomeado (Array ou Dicionário) e um chat_template.jinja separado, se não houver uma modelo de linha ou este é nulo. modelos multi-dignados são rejeitados; 65536 Os arquivos .onnx populares, os modelos de visão/embedagem ou as exportações puras de GPU/NPU não são modelos de reunião.

Incluído: Microsoft.ML.OnnxRuntimeGenAI 0.17.1, CPU. A configuração do provedor é redefinida para CPU. Sem download e sem pesos de modelo no pacote. Nenhuma instalação do Python é necessária. Use apenas pacotes de modelos confiáveis; o processo separado é um limite de falha/anulação, não uma área restrita de segurança.

Limites: 90 segundos incluindo carregamento do modelo, máximo de tokens de prompt 3072 e tokens gerados 1024, limite adicional de contexto do modelo; JSON verificado rigorosamente e IDs de recursos conhecidos. Resposta incorreta/truncada será descartada. Fechar/Cancelar encerra o processo de trabalho iniciado. Nenhum comando de modelo livre e nenhum programa/caminho de arquivo na lista de recursos separada do prompt; o texto alvo escrito pelo usuário pode conter tais dados.

## Verificação

- Conjunto principal, testes de transporte de loopback com respostas simuladas do LM-Studio/llama.cpp/Ollama, verificação de esquema/aborto/recurso.
- Regressão de protocolo Unicode com destinos longos máximos e nomes de recursos 24.
- Inferência de CPU nativa real com tiny-random-gpt2-fp32-Testgraph oficial da microsoft/onnxruntime-genai, commit 5c19002edce50beaa3a5c7eeefd8fe5269ee8d19. Adicionado modelo de teste e tokenizador de bytes adaptado ao pequeno gráfico aleatório. Este modelo aleatório não verifica qualidade e não está em lançamento.
- Teste de tempo de execução nativo reproduzível: tools/check-onnx-runtime.ps1 -ModelDirectory <pasta do modelo GenAI>.
- WPF-Smoke verifica a seleção de todos os quatro fornecedores e a bancada de testes; A seleção por si só não gera uma solicitação de modelo.

## Restantes ações reais

Testes de substituição de hardware/smartphone/tablet/monitor ainda são necessários. A supressão exclusiva do teclado permanece bloqueada sem um driver de filtro assinado, licenciado e verificado. Não substituído por drivers fictícios ou Raw Input. A mudança de foco ainda pode ser bloqueada pelo Windows; o erro agora menciona a ativação manual da janela de destino. Foram adicionadas traduções aos controles centrais de IA em todos os catálogos 27; O detalhamento de novos textos e a aprovação da tradução técnica ainda estão em aberto. Qualidade ao vivo com um modelo de instrução adequado, operação contínua e economia de tempo medida não são comprovadas por testes de modelo aleatório/HTTP falso.

## Fontes

- https://onnxruntime.ai/docs/genai/api/csharp.html
- https://www.nuget.org/packages/Microsoft.ML.OnnxRuntimeGenAI/0.17.1
- https://lmstudio.ai/docs/developer/openai-compat/chat-completions
- https://github.com/ggml-org/llama.cpp/blob/master/tools/server/README.md

Teste final: versão build sem avisos/erros; Aprovado no núcleo completo e no conjunto Windows, incluindo fixação de foco nativo, HTTPS, UIA, plugin IPC e testes de transporte de IA compatíveis. Novo pacote release-20261012-190618: WPF smoke aprovado (relógio 19:07), inferência de CPU ONNX agrupada com gráfico de teste aprovado. Revisão independente: limite do protocolo Unicode corrigido, nenhuma outra descoberta concreta. Nenhuma aceitação física do fabricante/telefone/monitor derivou disso.

## Continuação: Qualidade real de modelos e modelos

Mais CPU com allama-ai/SmolLM-360M-Instruct-onnx-int4Revisão 82a3d1a8a86c8053f2a859a90ed4d344fc0725b0: nativo infertilidade. Modellgewichte-SHA256: 48e9c4ef12f7f995ca11fc7020b39990aa27bc67cc0fe1160676a000a04429.

Quatro testes de planejamento reais (alemão/inglês, documento, programa, microfone explícito): **0/4 Plano válido**; cada JsonException em caso de teste de resposta rigoroso. 63501, 6376, 72118 e 6774 ms. Não executado nenhuma ação. Este pequeno modelo não cumpre o contrato de planejamento e não é recomendado como adequado. relatório: artifacts/local-ai-evaluation/smollm360-results.json. reprodução: Windows-Test --evaluate-onnx <modelo de ordem> <caminho de relatório>. a função de tempo de execução não demonstra qualidade do modelo.

Navegação de ferramentas e relatórios de teste foram adicionados em todos os 27 Os catálogos de idiomas são traduzidos. textos longos e descontos linguísticos permanecem abertos. regressão de modelos também examina os tipos JSON falsos, modelos vazio/multiplique/translantes e zero com Jinja Fallback.

Pacote atualizado: Artifact / Release-20261012-201134embalagem WPF- A vingança em 12.10.2026 O 20:12 sobreviveram; a referência nativa da CPU com o modelo de teste de instrução separado no trabalhador ligado. Windows-Testado de TI; Construir sem aviso/erro. qualidade de planejamento do modelo continua 0/4.
