<!-- generated-doc locale=pt-PT source=docs/FEATURES.md sha256=ea712f39457ce69f772f09a1294201705a5f36a92a8bf15f356bd80132c3e723 -->

> Tradução assistida por máquina. A estrutura, os exemplos de código e as ligações são verificados; isto não constitui revisão por um falante nativo nem aprovação jurídica. Os relatórios históricos mantêm as datas e o âmbito originais. [Fonte original](../FEATURES.md)

# MAKRIVO – Visão geral dos recursos

Fabricante: **Aivana**. Status: **12. Outubro 2026**. Este é o status de desenvolvimento, não uma venda ou lançamento de hardware.

## Controle Windows

- Blocos, teclas de atalho globais, paleta de comandos, favoritos e perfis dependentes de aplicativos.
- Multipasso, repetição, pausa, cancelamento e visualização de macros.
- Abra programas, arquivos e sites; Use texto, área de transferência e atalhos de teclado.
- Organize janelas, bloqueie Windows, controle saídas de áudio e microfones e altere o brilho da tela por meio de interfaces suportadas.
- OBS-WebSocket-v5-Anbindung para cenas, gravação, streaming e áudio.
- Modelos, Workplace Assistant, descoberta de dispositivos e sobreposição de teclado.
- Importação e exportação de configurações de perfis; Revisão de projeto e detecção de conflitos.
- Configurações salvas localmente; nenhuma conta é necessária para o uso diário.

## Recursos avançados no código-fonte atual

| área | Gama de funções | Limites |
| --- | --- | --- |
| Verificação de resultados | Pré-inspeção, pós-inspeção e histórico de testes | Confirme apenas efeitos verificáveis; Um pressionamento de tecla bem-sucedido não prova o efeito desejado. |
| Assistente de local de trabalho | Prepare perfis e processos de programas, arquivos, URLs e outras ações selecionados | Pré-visualizar e salvar é necessário; A configuração não faz nada. |
| Gravação de macros e fluxos de trabalho | Gravação, condições, gestos e sincronização de perfil local | O comportamento e os limites são documentados nos relatórios de implementação e teste. |
| Automação da IU | Selecione alvos, verifique seletores, registre ações e sequências de cliques limitadas | Trabalhador separado; alvos ambíguos, desabilitados e senhas são rejeitados; nenhuma gravação geral de entrada de texto. |
| Extensões | Inspecione, importe, ative com base em confiança explícita, exporte e reverta pacotes | O isolamento do processo não é uma área restrita; não é um mercado público de plugins. |
| Painel móvel | Painel HTTPS local, emparelhamento, perfis selecionados e histórico de status/auditoria | Aceitação de LAN real de telefone/tablet ainda aberta; nenhum painel de sensor universal. |
| Adaptador do fabricante | Adaptador analógico Corsair, Logitech-G-SDK, Razer/SteelSeries-RGB e Wooting | Dependente do modelo/SDK; novos adaptadores não totalmente validados com hardware real. Nenhuma nova DLL proprietária do SDK incluída. |
| Mapeamento físico do teclado | Reconheça chaves padrão com base no dispositivo e no código de leitura | Raw Input não suprime a entrada Windows normal. |
| Assistente de reunião de IA local | Protótipo opcional para Ollama, LM Studio, llama.cpp e ONNX Tempo de execução A CPU GenAI produz designs limitados a partir de recursos explicitamente selecionados | Desativado por padrão; tempo de execução de CPU nativo testado com modelo aleatório, mas sem qualidade de planejamento ou economia de tempo comprovadas; nenhum peso de modelo incluído. |

## Ainda não disponível como recurso de vendas finalizado

- **Teclado/macro pad secundário exclusivo:** Falta um driver de filtro assinado, licenciado e testado em hardware. As teclas normais ainda podem atingir Windows.
- Aceitação completa de hardware de todos os adaptadores do fabricante, bem como testes reais de LAN de dispositivos móveis.
- Verdadeiras alterações mistas de monitor/DPI e aprovação técnica de todas as novas traduções. PerMonitorV2 e 150/200-Prozent-Layouts simulado foram verificados; Os testes de exibição reais permanecem separados. A interface oferece idiomas 27; Novos textos detalhados ainda não foram completamente aprovados em termos linguísticos.
- Instalador de varejo assinado, EULA final, direitos de distribuição de SDK verificados e integração produtiva de licença de compra/checkout.

## Detalhes e evidências

- [Documentação de serviço](README.md)
- [Extensões: funções, testes e fronteiras abertas](testing/2026-10-12-six-extensions.md)
- [Assistente de IA local: protótipo e nível de teste](testing/2026-10-12-local-meeting-assistant.md)
- [Tempo de execução local e requisitos de modelo](testing/2026-10-12-local-ai-runtimes.md)
- [DPI e exame de escalação](testing/2026-10-12-dpi-scaling.md)
- [Exame de Licença](licensing/2026-10-12-commercial-license-audit.md) – Histórico de arquivo; o novo script do Publish complementa a instrução do tempo de execução. O pacote final deve ser examinado separadamente.
- [Plataformas de Venda](licensing/makrivo-vertriebsplattformen-2026-10-12.html) – estado de comparação; Stripe é, além disso, uma plataforma de pagamento possível, veja as condições atuais do fornecedor.

## Gere pacote de desenvolvimento completo

```powershell
./tools/publish-complete.ps1 -SelfContained
```

O pacote inclui aplicativo de desktop, operador de automação de UI, host móvel, arquivos de navegador, plug-in de amostra e notas de licença de tempo de execução. Os artefatos de construção não são verificados no Git. Uma versão do código-fonte GitHub ainda não é um instalador Windows publicado.
