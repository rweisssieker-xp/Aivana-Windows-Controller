<!-- generated-doc locale=pt-PT source=docs/superpowers/specs/2026-10-08-keyboard-mapping.md sha256=fb766a2a2b4698ae3e1c3f63b50bb4f5c605da3dd56c61c05fcc0832b5cf8aeb -->

> Tradução assistida por máquina. A estrutura, os exemplos de código e as ligações são verificados; isto não constitui revisão por um falante nativo nem aprovação jurídica. Os relatórios históricos mantêm as datas e o âmbito originais. [Fonte original](../../../superpowers/specs/2026-10-08-keyboard-mapping.md)

# Visualização do teclado físico para mapeamento

[English (US)](2026-10-08-keyboard-mapping.md) | [Deutsch](2026-10-08-keyboard-mapping.md) | [Français](2026-10-08-keyboard-mapping.md) | [Italiano](2026-10-08-keyboard-mapping.md)

## Alvo

A visualização do teclado tem como objetivo mostrar as posições reais das teclas ao atribuir ações. Linhas da mesma largura não são suficientes: Enter, Shift, Espaço, Navegação, teclado numérico e controles adicionais do fabricante são diferentes.

## Implementação

- Catálogo com IDs físicos separados, geometria em unidades chave, legendas impressas e códigos de entrada Windows.
- ISO-DE e ANSI-US, cada um em formato completo e TKL; adicionalmente Corsair K100 RGB, que foi reconhecido como modelo conectado via iCUE.
- ISO Enter com formato L, teclado numérico com teclas altas de mais/enter e zero largo; Navegação e setas em suas próprias posições.
- Selecione explicitamente o modelo/layout. USB VID/PID e nome do modelo não comprovam variante de idioma. Perfis genéricos e perfis modelados com base em documentos do fabricante são claramente rotulados de forma diferente; nenhuma escala milimétrica ou garantia de compatibilidade de firmware.
- Mapeie o scancode para o layout de entrada Windows ativo para que um clique físico na tecla não ocupe acidentalmente outra chave, como Y/Z ao alternar entre US e DE.
- A tarefa abre um rascunho no editor de ações existente. Salvar, verificar conflitos e descartar continuam sendo os mecanismos existentes. Os cliques não iniciam ações.
- Teclas G proprietárias, teclas de perfil/bloqueio e roda iCUE são representadas, mas não são emitidas como teclas de atalho Windows normais sem um adaptador de entrada implementado. Atalhos configuráveis ​​do fabricante continuam sendo um caminho possível.
- As atribuições permanecem como teclas de atalho globais Windows; uma visualização do dispositivo não implica supressão específica do dispositivo ou atribuição exclusiva de um único teclado físico.

## Evidência

[K100-Handbuch com elementos de controle](https://assets.corsair.com/image/upload/corsairmedia/sys_master/productcontent/AP_K100_RGB_Mechanical_QSG_AD.pdf), [Corsair Declaração ANSI / ISO](https://www.corsair.com/au/en/explorer/gamer/keyboards/ansi-vs-iso-whats-the-best-keyboard-layout/).

## Exame

testes de geometria, variante e código de entrada; Builds e testes de núcleo/Windows existentes; WPF smoke para exibição e mapeamento sem executar ou alterar a configuração real do usuário. A pesquisa do fabricante documenta requisitos adicionais de modelo/SKU, independentemente do catálogo de layout atualmente implementado.
