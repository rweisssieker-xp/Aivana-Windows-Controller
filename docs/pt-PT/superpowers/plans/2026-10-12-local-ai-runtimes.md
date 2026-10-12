<!-- generated-doc locale=pt-PT source=docs/superpowers/plans/2026-10-12-local-ai-runtimes.md sha256=f24c6b3bce4d0b4dd9d42732c363941a36644f90cf8d664d61ea0c60eb950079 -->

> Tradução assistida por máquina. A estrutura, os exemplos de código e as ligações são verificados; isto não constitui revisão por um falante nativo nem aprovação jurídica. Os relatórios históricos mantêm as datas e o âmbito originais. [Fonte original](../../../superpowers/plans/2026-10-12-local-ai-runtimes.md)

# Tempos de execução locais de IA e verificações restantes

Objetivo: realmente conectar tempos de execução de IA locais adicionais, receber análises de design e tornar visíveis as versões abertas. O usuário autoriza a implementação sem questionar. As alterações existentes são mantidas.

1. Transporte de loopback e testes de inspeção de pacotes de pré-implementação ONNX; Verifique endereços de rede, limites de saída, aborto e recursos desconhecidos.
2. Mensagens compartilhadas do planejador; Ollama recebido, LM Studio (127.0.0.1:1234) e llama.cpp (127.0.0.1:8080) adicionados. Sem URLs de nuvem ou troca automática de provedor. Os servidores locais podem encaminhar-se; A sua operação continua a ser da responsabilidade do operador.
3. ONNX Runtime GenAI CPU em processo próprio com timeout, protocolo JSON limitado e encerramento de processo. Selecione explicitamente a pasta do modelo; nenhum modelo é baixado automaticamente. Somente modelos de texto compatíveis com GenAI, sem redes .onnx arbitrárias.
4. Seleção WPF para tempo de execução, modelo, pasta ONNX; todos oferecem o mesmo design rigorosamente testado.
5. Bancada de testes com limites claros de hardware, DPI, painel móvel e macro pad exclusivo. Verificações físicas e um driver assinado/licenciado não podem ser substituídos por software aqui.
6. Versão de compilação, teste de núcleo/Windows, fumaça WPF, teste de trabalhador/pacote e revisão independente. Crie um novo pacote completo; Relate apenas a qualidade do modelo testada se o modelo realmente existir.

Resultado: quatro tempos de execução implementados, ONNX testado nativamente separadamente, escolha do fornecedor e bancada de testes na fumaça compactada do WPF aprovada. Núcleo/Windows suítes verdes. Revise a localização do limite Unicode com constante comum e regressão corrigida. Aprovações reais de alterações de hardware/dispositivos móveis/DPI, lançamento de driver, traduções técnicas completas e qualidade de instrução ao vivo permanecem um trabalho explícito e aberto; não emitido como completo.

Continuação: Implementado modelos separados de Jinja e designados/default; Passado testes de regressão de tipo/limite/falback. 27 Catálogo complementar. modelo de instrução real descarregado separadamente e SHA256 Verificado: a referência nativa do CPU funciona, mas 0/4 Casos de planejamento cumprir o contrato JSON; não executar ações. revisão independente: zero-falback corrigido e testado, depois de nenhuma descoberta mais concreta. Windows-Testado de TI, Edição de Release 0 Aviso / Erros
