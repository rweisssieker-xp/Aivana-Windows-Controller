<!-- generated-doc locale=pt-PT source=docs/testing/2026-10-12-gui-polish.md sha256=ac39188358e51c68964421d3e855161d607b02a41da2b1a1fad70244f9feff97 -->

> Tradução assistida por máquina. A estrutura, os exemplos de código e as ligações são verificados; isto não constitui revisão por um falante nativo nem aprovação jurídica. Os relatórios históricos mantêm as datas e o âmbito originais. [Fonte original](../../testing/2026-10-12-gui-polish.md)

# Revisão da GUI de 12.10.2026

- Acesso direto a “Ferramentas” na navegação; três grupos com ações declaradas para configuração, criação e teste.
- Quatorze áreas de diálogo existentes usam uma classe de diálogo comum WPF com os recursos da janela principal, fonte consistente e posicionamento no proprietário.
- O tamanho da caixa de diálogo é limitado à área de trabalho Windows; conteúdo longo recebe barras de rolagem. As configurações e as páginas de perfil permanecem roláveis ​​mesmo em alturas baixas.
- O filtro de ação aparece apenas no painel. A seleção e a pesquisa de perfis têm nomes explícitos para tecnologias assistivas. Os rótulos dos campos são mais contrastantes.
- O teste de fumaça de diálogo inclui visão geral da ferramenta e diálogos de extensão em 560 × 440 e tamanho normal; Os testes de gravador e idioma existentes foram adaptados à estrutura de rolagem.

Limitações: As janelas compactas não substituem os testes reais de DPI Windows com porcentagem 150/200. Os novos textos em alemão ainda não foram totalmente traduzidos para todos os idiomas 27. Nenhuma nova validação de hardware e nenhuma declaração sobre usabilidade medida.

Bancada de testes: Versão de lançamento sem avisos/erros; Passou no teste de fumaça WPF completo, incluindo ferramenta compacta e caixas de diálogo de extensão e verificações de troca de idioma. git diff --check aprovado.
