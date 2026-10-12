<!-- generated-doc locale=pt-PT source=docs/superpowers/specs/2026-10-12-six-extensions-design.md sha256=a80dda083fbbe90404023ac784b47e7f16f89f72ce0b821e3cda122dc561317f -->

> Tradução assistida por máquina. A estrutura, os exemplos de código e as ligações são verificados; isto não constitui revisão por um falante nativo nem aprovação jurídica. Os relatórios históricos mantêm as datas e o âmbito originais. [Fonte original](../../../superpowers/specs/2026-10-12-six-extensions-design.md)

# MAKRIVO: seis extensões de produto e adaptadores de teclado

Data: 2026-10-12
Status: design proposto para revisão do usuário; a implementação ainda não começou.

## Objetivo
Entregue todas as seis extensões solicitadas na aplicação Windows/WPF existente, preservando os perfis existentes, atribuições G1/G2, operação local e uso sem conta. Expanda a integração do teclado para Razer e Logitech, com uma arquitetura reutilizável baseada em capacidade para outros fabricantes. O nome do fabricante ou o layout do teclado nunca é evidência de que controles proprietários são suportados.

## Decisão de arquitetura
Estenda Aivana.Core com contratos serializáveis e validação, Aivana.Windows com Windows e adaptadores de fornecedor e Aivana.Desktop com editores transacionais e visualizações de status. Use projetos complementares separados para execução isolada de plug-ins e serviço de painel móvel local. Evite uma reescrita ou um único processo privilegiado hospedando todas as extensões. A configuração existente deve ser carregada inalterada; anexe tipos de ação em vez de reordenar seus valores de enumeração persistentes.

Alternativas consideradas: uma implementação monolítica emaranharia a execução de hardware, rede e fluxo de trabalho; substituir WPF acrescentaria trabalho de migração não relacionado. Extensões modulares são recomendadas.

## 1. Teclado macro independente
Armazene vinculações por identidade de dispositivo, além de código de verificação física e sinalizadores de chave estendida. Adicione modos exclusivos e de observação explícita, descoberta de dispositivos, captura de chave, supressão de repetição, gestos, camadas retidas e diagnóstico de reconexão. Preservar distinções de entrada principal/numpad e testar layouts alemão/americano. As ligações têm como alvo um teclado físico selecionado; identidades de reconexão ambíguas exigem nova seleção em vez de controlar outro dispositivo.

Raw Input fornece apenas observação. A supressão exclusiva por dispositivo requer um back-end de filtro de teclado real; nunca correlacione ganchos globais de baixo nível com Raw Input e reivindique supressão confiável específica do dispositivo. Fornece um watchdog, liberação de emergência, comportamento de falha ao abrir e instalador/desinstalador. Não capture entradas seguras da área de trabalho. A instalação do driver e a ativação do modo exclusivo são ações explícitas e separadas do usuário.

Driver gate: selecione um backend suportado, devidamente licenciado e assinado compatível com as configurações Windows 11 de destino ou desenvolva/assine um filtro dedicado. A interceptação é uma candidata para avaliação, não uma dependência de produção comprometida: seu README oficial descreve testes por meio de Windows 10 e licenciamento comercial distinto. Até que este portão seja atingido, o modo exclusivo deve ser mostrado como indisponível; o recurso não pode ser declarado completo.

Aceitação: com dois teclados físicos, a mesma tecla executa uma macro e não produz entrada normal de aplicação apenas no dispositivo exclusivo selecionado; outra entrada do teclado permanece inalterada. Verifique a desconexão, falha, cancelamento, suspensão/retomada e remoção de driver no hardware real.

## 2. Configuração guiada do espaço de trabalho
Adicione um assistente para local de trabalho, reuniões, streaming, pesquisa e alternância de projetos. Descubra aplicativos instalados, dispositivos de áudio, monitores e cenas OBS. Os usuários escolhem alvos e ligações; visualize as etapas resultantes e valide os requisitos ausentes. Salve somente após revisão, sem nenhuma ação em execução durante a configuração. Os perfis gerados permanecem editáveis ​​nos editores existentes.

Aceitação: crie um perfil funcional sem editar manualmente caminhos executáveis ou IDs de dispositivos; o cancelamento deixa as configurações inalteradas; o software ausente tem uma explicação prática.

## 3. Execução verificada
Introduza verificações de simulação, resultados digitados por etapa e pós-condições opcionais. Distinguir estados solicitados, concluídos, verificados, com falha, expirados, cancelados e não verificáveis. Verifique áudio/microfone, brilho, prontidão de processo/janela e estado OBS com leituras assíncronas limitadas. A entrega de atalho/texto não é prova de que o aplicativo de destino aceitou o comando. Os efeitos colaterais externos não são universalmente reversíveis; oferecer compensação apenas quando implementado.

Aceitação: alvos explícitos desconectados nunca recuam silenciosamente; estado observado errado nunca relata sucesso verificado; o cancelamento interrompe a votação; as falhas identificam a etapa e a ação de recuperação. O histórico de execução omite cargas de texto e segredos.

## 4. Plugins e pacotes compartilháveis
Os manifestos JSON versionados declaram ações, esquemas de parâmetros, provedores de estado, dependências e recursos. Execute plug-ins executáveis ​​em um processo separado em IPC local limitado com handshake, prazos e cancelamento. Rejeite mensagens malformadas e versões incompatíveis; lidar com travamentos sem encerrar o controlador. As declarações de capacidade são divulgação e mediação, não uma reivindicação de sandbox do sistema operacional.

Fornece visualização de exportação/importação de pacotes, hashes de conteúdo, proteção contra passagem de caminho, limites de tamanho, verificações de versão/dependência, ativação explícita e reversão de atualização. Plug-ins executáveis ​​importados não devem ser executados automaticamente. Inclua um plug-in de exemplo funcional com um fluxo de instalação/ação/estado/desinstalação de ponta a ponta. Nenhum serviço de mercado público é necessário para esta versão.

Aceitação: crie, exporte, instale e execute um exemplo real de integração; demonstrar tempo limite, isolamento de falhas, falha de dependência, rejeição de travessia e importação desabilitada.

## 5. Windows Automação de IU
Adicione um seletor de elemento e ações limitadas para invocar, valor, seleção, alternância, foco e espera por um elemento/estado. Persistir seletores usando processo, ID de automação, tipo de controle e contexto ancestral; evite gravação apenas de coordenadas. Mostrar contagem de correspondências do seletor e testar seletores somente leitura. Os campos de senha são excluídos da gravação. Lide explicitamente com controles não suportados e restrições de nível de integridade Windows.

Amplie a gravação com ações UIA revisáveis, mantendo a gravação de atalhos atual. Seletores ambíguos falham em vez de clicar em um controle arbitrário. Uma árvore normal de acessibilidade do navegador não é uma promessa de um mecanismo de automação DOM completo.

Aceitação: automatize uma aplicação de teste dedicada com seletores estáveis em mudanças de posição/tamanho; testar ambiguidade, elementos faltantes, controles desativados, tempos limite e cancelamento.

## 6. Painel local para telefone/tablet
Sirva um painel responsivo a partir de um serviço LAN opcional. Emparelhe explicitamente usando um código único de curta duração e emita credenciais revogáveis ​​protegidas localmente. Use HTTPS com confiança/fixação de certificado local documentada; nenhum substituto enviando credenciais em texto simples. O padrão é desabilitar e restringir interfaces configuradas. Validação de origem, comandos autenticados, limites de taxa e mensagens limitadas são obrigatórios.

Expor perfis/ações permitidas, estado ao vivo, progresso e cancelamento; use IDs estáveis ​​e a mesma fila de execução das chaves físicas e de desktop. Não exponha comandos arbitrários, caminhos executáveis, segredos ou instalação de plugins. Fornece telas de conexão, emparelhamento e revogação. Nenhuma conta na nuvem ou retransmissão de internet está incluída.

Aceitação: operar a partir de um telefone/tablet real na mesma LAN; clientes não pareados não podem listar ou executar ações; os tokens revogados falham; comandos duplicados/repetidos não são executados duas vezes; desconectar não tenta ações silenciosamente.

## Escopo do fabricante do teclado

| Adaptador/família | Integração planejada | Limite explícito |
| --- | --- | --- |
| Corsair | Preservar a atual entrada G-key iCUE e integração RGB | A detecção de capacidade de modelo/software continua necessária |
| Logitech G | Adaptador SDK G-key com verificações de disponibilidade de imprensa/liberação e tempo de execução; iluminação por meio de seu SDK separado, quando compatível | Não compare a detecção do G HUB com a API G-key suportada em todos os modelos |
| Razer | Adaptador de iluminação Chroma e mapeamento guiado Synapse para chave padrão, além de entrada HID genérica | Chroma é uma API de iluminação; não a rotule como entrada de macro-chave proprietária nativa |
| SteelSeries | Adaptador de estado/iluminação GameSense mais entrada HID genérica | O envio de eventos GameSense não é uma API de captura de chave proprietária geral |
| Wooting | Adaptador SDK analógico para entrada analógica suportada, além de chaves HID normais | Os recursos e os termos de distribuição do SDK devem ser verificados por modelo compatível |
| Keychron/QMK/VIA | Chaves HID padrão e instruções de mapeamento de firmware; layouts conhecidos apenas com evidências | O suporte de configuração VIA não é uma API de macro-chave de tempo de execução universal |
| Outros teclados HID padrão | Descoberta e mapeamento de chave física por dispositivo; modo exclusivo apenas com o backend de filtro validado | Nenhum suporte de tecla proprietária, discagem ou Fn específico do modelo fabricado |

Cada dispositivo mostra status detectado, observado, mapeado por meio de software do fornecedor, SDK nativo, driver exclusivo, indisponível ou hardware não testado, conforme aplicável. Preservar evidências de capacidade com versões de adaptadores e identificadores de modelo/firmware testados.

## Ordem de entrega
1. Compatibilidade de configuração, contratos de capacidade e execução verificada.
2. Configuração guiada usando descoberta real e executores de ações existentes.
3. UI Automation e seu gravador/acessório de teste.
4. Host de plug-in, fluxo de pacote e exemplo de integração.
5. Painel LAN emparelhado e verificação móvel.
6. Adaptadores de fornecedores expandidos e integração exclusiva de back-end de teclado, com validação de hardware real.

Todos os seis permanecem obrigatórios. Assinatura/licenciamento de driver, SDKs inacessíveis ou hardware de teste ausente são portas de lançamento, não motivos para substituir o comportamento simulado e concluir o trabalho.

## Critérios de verificação e liberação
Execute verificações de núcleo existentes, Windows e WPF e adicione testes de integração focados para cada novo limite. Verifique viagens de ida e volta JSON e perfis legados; teste conteúdo executável importado, autenticação de rede, cancelamento, travamentos de plugins e reconexão de dispositivos. Inspecione os layouts de desktop e dispositivos móveis. Valide a escala Windows alta separadamente. Construa artefatos de lançamento somente após passar nas verificações; distinguir funcionalidades implementadas/testadas, implementadas/não testadas por hardware e bloqueadas externamente nas notas de versão. Não substitua o aplicativo instalado, instale drivers, altere mapeamentos reais de teclado ou abra o acesso à rede como um teste incidental.

## Evidência
- Semântica Microsoft Raw Input: https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-rawinputdevice
- Logitech SDKs de desenvolvedor G: https://www.logitechg.com/en-us/programs/developer-alliance
- Razer Perguntas frequentes sobre o SDK do Chroma: https://developer.razer.com/works-with-chroma/chroma-faq/
- Repositório de interceptação e condições de dependência: https://github.com/oblitum/Interception
- Inventário existente do fabricante do repositório: docs/de-DE/research/tastaturhersteller-sonderfunktionen.md

## Rever decisões
O usuário é solicitado a aprovar este projeto escrito antes do plano de implementação. A principal dependência externa é um back-end de driver de entrada exclusivo pronto para produção. O escopo recomendado mantém isso como uma entrega fechada, em vez de reduzi-lo silenciosamente à observação Raw Input.
