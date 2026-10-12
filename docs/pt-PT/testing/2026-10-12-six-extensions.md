<!-- generated-doc locale=pt-PT source=docs/testing/2026-10-12-six-extensions.md sha256=ff2eb755b2f18dce8c7bd7c832018ae71ee706bcca65e37c191dbb55ff999c1e -->

> Tradução assistida por máquina. A estrutura, os exemplos de código e as ligações são verificados; isto não constitui revisão por um falante nativo nem aprovação jurídica. Os relatórios históricos mantêm as datas e o âmbito originais. [Fonte original](../../testing/2026-10-12-six-extensions.md)

# Seis extensões: implementação e banco de testes

Status: 12. Outubro 2026, Filial `codex/six-extensions`. Status de trabalho, sem liberação de produção.

## Serviço

A paleta de comandos abre as entradas **Configurar estação de trabalho**, **Fabricante de teclado e SDKs**, **Painel móvel**, **Extensões**, **Automação UI**, **Fluxo de registro UI** e **Teste de execução**.

- O Workspace Assistant cria perfis de local de trabalho, reunião, streaming, pesquisa e projeto. Programas do Registro/Menu Iniciar ou Seletor de Arquivos, URLs, Saída/Tela e Cena OBS são exibidos como etapas antes de salvar. A instalação não executa nenhuma ação.
- No editor de etapas, **Check result** ativa a verificação preliminar e a pós-verificação. O histórico e o painel móvel distinguem a confirmação real do efeito não verificável. Mudanças relativas, atalhos de teclado e entrada de texto não fornecem uma confirmação geral de eficácia. A comprovação do processo não comprova a prontidão técnica da aplicação.
- A Automação da Interface do Usuário usa um trabalhador separado e com tempo limitado. Seleção via posição do mouse, verificação do seletor, Invoke/Select/Toggle/Focus/SetValue/WaitState; múltiplos acessos, alvos desabilitados e campos de senha são rejeitados. A gravação captura ações de clique e pausas suportadas; As entradas de texto não são gravadas. A admissão será verificada antes da aceitação.
- Extensões: inspeção ZIP, hashes, esquema, importação desabilitada, confiança explícita antes da ativação, processo isolado com IPC limitado, exportação, desativação, desinstalação, restauração/descarte da versão anterior. O isolamento do processo não é uma sandbox: plug-ins compartilhados são executados com direitos de usuário. O pacote de amostra `makrivo.example.zip` contém contagem regressiva e ação de status. Os esquemas suportados representam um subconjunto de esquema JSON simples documentado no código-fonte; nenhum mercado público.
- Painel móvel: selecione IP local e perfis compartilhados, abra o endereço HTTPS, verifique o certificado usando o SHA-256-Fingerprints exibido e confie nele conscientemente e, em seguida, insira um código único de seis dígitos. As chaves privadas são protegidas com Windows-DPAPI; apenas hashes de acesso do cliente são salvos. Os certificados mudam após renovação ou mudança de IP. O firewall Windows é liberado pelo usuário se necessário. Nenhuma instalação automática de certificado, encaminhamento de porta ou alteração de firewall. O acesso do cliente pode ser revogado. O painel mostra o status da ação e o histórico de testes; ainda não é um painel universal de sensores ao vivo.

## Teclados

| Fabricante | Implementação | Limitação |
|---|---|---|
| Corsair | teclas iCUE-G existentes e RGB | É necessária conexão local iCUE e modelo adequado |
| Logitech G | SDK G-Key e SDK LED separado | liberar explicitamente x64-DLL local original; O G-Key SDK não fornece identidade individual de vários teclados |
| Razer | Chroma RGB e reconhecimento de chave padrão | Atribuir chaves proprietárias anteriormente em Synapse |
| SteelSeries | GameSense-RGB e reconhecimento de chave padrão | atribuir chaves proprietárias anteriormente no GG |
| Wooting | Valores analógicos relacionados ao dispositivo, limites de pressão/liberação | local original x64-SDK-DLL; a entrada HID normal permanece ativa |
| Keychron/QMK/VIA/Ducky/ASUS/HyperX/Roccat/outros | Botões físicos padrão por dispositivo e código de digitalização | Fn e controles proprietários dependem do modelo |

Nenhuma nova DLL proprietária do SDK está incluída. Os adaptadores do novo fabricante ainda não são validados por hardware sem dispositivos físicos adequados.

**O segundo macropad exclusivo não está pronto:** Raw Input observa os dispositivos separadamente, mas não suprime sua entrada Windows normal. Uma versão de produção requer um driver de filtro licenciado e assinado, verificado sob Windows 11/Secure Teste de inicialização e liberação de emergência, travamento, suspensão e reconexão. Nenhum driver foi instalado. O aplicativo marca explicitamente esse limite.

## Exames

- Versão de lançamento: bem-sucedida, zero avisos/erros.
- Core Console: todos os grupos de teste são aprovados, incluindo conflitos de chave de fornecedor, reversão de plug-ins e erros reais de execução no histórico de testes.
- Integração de extensão: trabalhador UIA real com aplicação de teste WPF; invocação exclusiva, ambiguidade, alvos desabilitados, exclusão de senha. Processo de plug-in de pipe com nome real, limites de protocolo, histerese analógica e decodificação Logitech ABI bem-sucedida.
- Host HTTPS real: certificado fixado, acesso não pareado, código falso/único, proteção de origem, arquivos do navegador, armazenamento de hash e chave protegida com sucesso. Um bug do Schannel com chaves efêmeras descoberto em testes foi corrigido.
- Suíte Windows com teste de sucesso em primeiro plano explicitamente omitido: bem-sucedido; Áudio/OBS/iCUE/Raw Input e status da tela verificados. Na execução anterior, o conjunto completo falhou porque a mudança de foco para a janela de teste isolada foi recusada. A execução subsequente do protótipo AI também incluiu o conjunto completo, incluindo troca de foco e um teclado iCUE conectado. Outra repetição falhou novamente devido à mudança de foco; O teste dependente do contexto, portanto, não é verde estável. Este teste não foi atenuado. Os acessos de leitura DDC que falham por um curto período são repetidos por um tempo limitado; nenhum brilho foi alterado.
- WPF-Smoke: todos os diálogos anteriores e novos renderizados, invariância de configuração, editor e CRUD bem-sucedidos; Capturas de tela na pasta de construção `smoke`.
- Navegador: Playwright com Edge, 390×844 e 1024×768, fluxo de acoplamento/UUID/envio único/sem estouro horizontal bem-sucedido. Simulação de API; teste HTTPS real separadamente. Fotos em `artifacts/mobile-browser`. O plug-in do navegador não estava disponível, portanto, o substituto regular do Playwright.
- Revisão de código independente realizada; Corrigidos comandos móveis atrasados, resultados de verificação de terminal ausentes, verificação de caminho de processo, reversão de plug-in e reinicialização de pulsação RGB.

Ainda em aberto: integração de driver exclusiva, teste de LAN do fabricante físico/telefone/tablet, escala Windows percentual 150/200 e verificação completa da tradução de todos os novos textos de diálogo. Os novos textos de diálogo são predominantemente alemães; Os catálogos de idiomas existentes são mantidos. Portanto, o requisito geral ** não foi totalmente aceito**.

## Gerar pacote

`./tools/publish-complete.ps1 -SelfContained` cria uma nova pasta em `artifacts/release-*`, incluindo desktop, trabalhador UIA, host HTTPS, arquivos de navegador, plug-in de amostra importável e notas de licença de tempo de execução. O aplicativo instalado não é substituído. Sem `-SelfContained`, o pacote precisa corresponder a .NET-10-Desktop-/ASP.NET-Runtimes.

O pacote é um teste local, não um instalador de varejo assinado. Os próprios Termos de Uso e os direitos de distribuição do SDK são versões separadas; consulte a documentação existente em `docs/licensing`.
