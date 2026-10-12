<!-- generated-doc locale=pt-PT source=docs/testing/2026-10-12-dpi-scaling.md sha256=dcf6d4c1a21eec9be9f07a38cab0aff308587c78b85fb2b24c69f04d28d82864 -->

> Tradução assistida por máquina. A estrutura, os exemplos de código e as ligações são verificados; isto não constitui revisão por um falante nativo nem aprovação jurídica. Os relatórios históricos mantêm as datas e o âmbito originais. [Fonte original](../../testing/2026-10-12-dpi-scaling.md)

# Verificação de DPI

MAKRIVO declara PerMonitorV2 no manifesto do aplicativo incorporado. O teste WPF usa GetWindowDpiAwarenessContext e AreDpiAwarenessContextsEqual para verificar o contexto efetivo real da janela principal.

Os testes adicionais de layout/grade simulam a porcentagem 150 e 200 em uma tela 1920 × 1080 com reserva da barra de tarefas: dimensões lógicas da janela 1280 × 693 respectivamente 960 × 520 DIPs. Todas as cinco páginas principais são renderizadas como PNG em 144 ou 192 DPI. Os arquivos são chamados 150-* simulados em escala ou 200-* simulados em escala.

Esses testes não alteram nenhuma configuração do Windows. Eles não validam a verdadeira comutação de monitor WM_DPICHANGED, configuração mista de monitor e nitidez real em uma exibição diferente. Os testes de escala física permanecem abertos.

O aviso do analisador WinForms WFO0003 é suprimido exclusivamente para este projeto de inicialização WPF: WinForms é usado para integração da área de notificação, WPF controla a inicialização; O DPI é definido via manifesto antes da criação da janela.

Documentação Microsoft: https://learn.microsoft.com/en-us/windows/win32/hidpi/setting-the-default-dpi-awareness-for-a-process

Inspeção visual: O painel também muda para a exibição empilhada sob a altura da janela 650 DIPs para que os mapas e o editor não fiquem espremidos em uma área intermediária que é muito pequena.

Bancada de testes: Versão de lançamento sem erros/avisos; Passou no teste de fumaça WPF completo, incluindo PerMonitorV2-Fensterkontext real e 150/200-Prozent-Renderings simulado. O 200-Prozent-Dashboardaufnahme foi inspecionado visualmente após o ajuste de altura. git diff --check aprovado.
