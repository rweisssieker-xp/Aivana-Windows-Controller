<!-- generated-doc locale=pt-PT source=docs/testing/2026-10-12-responsive-layout.md sha256=8b3710a27f806575e88b4d9a0c4e80de4f26539ea1b5826cfe291764228b4ca6 -->

> Tradução assistida por máquina. A estrutura, os exemplos de código e as ligações são verificados; isto não constitui revisão por um falante nativo nem aprovação jurídica. Os relatórios históricos mantêm as datas e o âmbito originais. [Fonte original](../../testing/2026-10-12-responsive-layout.md)

# Interface WPF responsiva

As páginas principais são baseadas na largura utilizável do conteúdo, não em uma largura fixa da janela. Na largura do conteúdo dos DIPs 900, o painel e o editor ficam um abaixo do outro. Nos DIPs 620, os índices estão em duas colunas. A barra lateral é recolhida sob a largura da janela 820 DIPs. Tamanho mínimo: 640 × 480 DIPs.

Os painéis de perfil e configurações e os controles do teclado alternam suas colunas com base na largura real da grade. Os controles existentes são reorganizados em vez de criar novas entradas. As cartas de ação dividem a largura da linha disponível. Barras de ferramentas quebram. A lateral do teclado é acessível verticalmente, o teclado físico mantém suas proporções.

Teste WPF automático: todas as cinco páginas principais em 640 × 480, 900 × 620 e 1440 × 900; Capturas de tela por página; Mudança de coluna em ambas as direções com verificação do texto recebido. DIPs são unidades lógicas WPF. A verdadeira escala Windows na porcentagem 150/200 e vários monitores ainda não foram validados.

Banco de testes: versão de lançamento sem erros/avisos. Teste de fumaça WPF completo aprovado com todos os tamanhos, alterações de coluna, layouts de teclado e verificações de idioma. git diff --check aprovado.
