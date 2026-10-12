<!-- generated-doc locale=pt-PT source=docs/VERIFICATION.md sha256=f19afb9e7b5ffac90c83167416ab3af0964a4481dd16b7357f8585480c6aae65 -->

> Tradução assistida por máquina. A estrutura, os exemplos de código e as ligações são verificados; isto não constitui revisão por um falante nativo nem aprovação jurídica. Os relatórios históricos mantêm as datas e o âmbito originais. [Fonte original](../VERIFICATION.md)

# Exame – 8. Outubro 2026

[English (US)](../en-US/VERIFICATION.md) | [Deutsch](../de-DE/VERIFICATION.md) | [Français](../fr-FR/VERIFICATION.md) | [Italiano](../it-IT/VERIFICATION.md)

O aplicativo foi desenvolvido em Windows x64 com o SDK .NET estável 10.0.202 e lançado como um Windows-x64-Ausgabe independente.

## Verificado automaticamente

- Construção geral: sem avisos e sem erros.
- 19 Testes de comportamento principal: configuração padrão, validação, dados nulos/duplicados, prioridade de perfil, importação/exportação e restauração de backup, ordem e nova tentativa de macro, anulação, acionamento duplo, serialização, instantâneo de execução imutável e falha de etapa.
- Verificação Windows: normalização e aliases de teclas de atalho, tamanhos de estrutura nativa, lista real de dispositivos de teclado, registro seguro e novo compartilhamento, WM_HOTKEY sintético para bloco e cancelamento, registro de bloco desativado durante a simulação de entrada.
- O EXE autônomo foi iniciado com `--smoke-test`: todas as cinco visualizações WPF são renderizadas, os blocos podem ser criados/copiados/excluídos, os rascunhos são retidos durante a atualização, a configuração é salva e recarregada. Este teste usa um diretório de configuração temporário isolado e não executa nenhuma ação de texto ou mídia.
- Dashboard, editor e representação compacta (900 × 720) foram inspecionados visualmente como renderizações reais de WPF. Salvamento de rascunho, descarte, ordem de bloco/macro, aliases de teclas de atalho alemãs, cancelamentos de macro individuais e progresso da etapa são verificados.

## Limites dependentes de hardware

A última execução local detectou quatro interfaces de teclado Windows. Este número descreve interfaces HID/Windows, não necessariamente quatro teclados físicos.

O iCUE SDK oficial está instalado e localizado próximo ao EXE. O teste ao vivo se conecta ao iCUE e detecta um teclado Corsair com modelo e informações de LED. As alterações de iluminação não foram acionadas automaticamente; Chaves especiais dependentes do modelo e de outros fabricantes não são testadas em termos de hardware. As teclas de atalho genéricas se aplicam a teclados.

A entrada de texto/mídia não foi injetada em outros agentes de usuário abertos. As estruturas de dados nativas e a lógica de erro/liberação foram verificadas; O comportamento num programa alvo específico depende, entre outras coisas, do seu nível de autorização.

## Repetir

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64
& ./artifacts/win-x64/MAKRIVO.exe --smoke-test
```

O teste GUI grava o resultado e a captura de tela em `smoke` próximo ao EXE testado. Destina-se a ser um modo de diagnóstico e não inicia uma instância persistente em segundo plano após a conclusão.


## Controles avançados

Verificado adicionalmente: condições de perfil com processo e título de janela, substituição de variável sem expansão recursiva, visualização de macro sem execução, validação de nova ação e compatibilidade retroativa de arquivos JSON antigos. As verificações nativas cobrem a paleta de comandos com registro, conflitos, envio e liberação de WM_HOTKEY, bem como divisão pura de monitor e acesso de leitura sob demanda à área de transferência. O teste WPF abre e renderiza a paleta real, verifica desfazer/refazer e favoritos, verifica rascunhos limpos após a renderização e conclui o processo com sucesso.

Windows bloqueio, alterações na área de transferência e movimento de janela não foram aplicados aos agentes do usuário durante o teste. Postar ações da janela Solicitações Windows de forma assíncrona; O programa alvo específico pode ter seus próprios tamanhos mínimos ou limites de autorização. O bloqueio de perfil e as estatísticas de execução se aplicam à sessão atual.


Exame de extensão do 12.10.2026: exames básicos do 30 aprovados; Adaptador Windows incluindo liberação durante a injeção de entrada, detecção de monitor somente leitura e simulação de status OBS aprovada. WPF verifica confirmação/cancelamento de fluxo, início/parada do gravador, visualização de troca de perfil, gestos e recursos de símbolos. Localização: 27 cataloga cada um com chaves 967; Documentação: páginas 84 em quatro idiomas. Botões proprietários diferentes de Corsair não foram testados em hardware; O suporte genérico inclui eventos padrão específicos do dispositivo F13–F24 e chaves de mídia. O transporte na nuvem ocorre por meio de uma pasta sincronizada selecionada; As importações são verificadas e permanecem desativadas.
