<!-- generated-doc locale=pt-PT source=docs/MAINTENANCE.md sha256=9bf5875b50386e698c0574eee390f790d4b1c29cb11288c856c98e03a05877f7 -->

> Tradução assistida por máquina. A estrutura, os exemplos de código e as ligações são verificados; isto não constitui revisão por um falante nativo nem aprovação jurídica. Os relatórios históricos mantêm as datas e o âmbito originais. [Fonte original](../MAINTENANCE.md)

# Manter todas as linguagens de documentação

O inventário segue todos os 27 linguagens de UI. Os folhetos gerados usam códigos regionais; mapas noruegueses para `nb-NO` O latim serbio para `sr-Latn`Cada pasta contém um índice, manual, referência de ação detalhada, inventário de recursos, notas de verificação, pesquisa incluindo seu CSV, planos históricos e análise de licenças.

## Fonte da Verdade

Editando a raiz `README.md`, `docs/HANDBOOK.md`, `docs/FEATURES.md`, `docs/VERIFICATION.md`Esta página, ou os arquivos originais abaixo `docs/research`, `docs/testing`, `docs/superpowers` e `docs/licensing`Não reescreva um relatório histórico para implicar que ele valida uma versão posterior. Manter a implementação, simulação, aceitação de dispositivos reais e aprovação comercial separada.

## Gerar e Validar

```powershell
python tools/localize_documentation.py --refresh-sources
python tools/check_documentation.py
```

O gerador on-line opcional envia a documentação ao ponto final de tradução pública do Google, seguindo a ferramenta de localização existente. não lê a configuração do usuário, segredos ou arquivos de modelo. apenas executa a documentação adequada para esse serviço. blocos de código, código de linha, URLs, estrutura de tabela e identificadores técnicos são protegidos. cache de tradução e um resumo de fonte congelado são armazenados sob ignorado `artifacts/documentation-translation`.

Quando o fornecedor on-line é limitado, `tools/translate_documentation_local.py` Enche o mesmo cache usando um local explicitamente fornecido M2M100 CTranslate2 O modelo, o 2026-10-12 O Snapshot usa a licença MIT facebook/m2m100_418M Modelo, convertido como JustFrederik/m2m_100_418m_ct2_int8 Na revisão `1aeed44db4dd61a486bba44acf54c76507082a2c`. CTranslate2 4.8.2 e `SentencePiece` 0.2.2 Não são ferramentas construídas.Não são enviados pesos de modelo ou dependências de tradução com MAKRIVO. Com o gerador `--cache-only` Depois de preencher o cache para render sem solicitações de rede. o assistente local não descarrega ou executa o código de modelo remoto.

```powershell
python -m pip install ctranslate2==4.8.2 sentencepiece==0.2.2
python tools/translate_documentation_local.py --model C:\models\m2m100-ct2 --refresh-sources
python tools/localize_documentation.py --cache-only
python tools/check_documentation.py
```

Sem `--refresh-sources`Um curso reutiliza o seu resgate congelado. Use a opção de refresco após a edição dos originais. Os segmentos ocultos são reutilizados. Um curso parcial pode selecionar códigos locais usando `--locales`- Encerrar todos os locais antes de publicar. correntes interrompidas não substituem silenciosamente a prosa não traduzida por uma tradução falha.

O manifesto registra cada fonte e arquivo gerado com SHA-256 impressões digitais de texto normalizado UTF-8: finais da linha LF e sem marcação de pedido de byte. O checker offline verifica cobertura completa por língua, impressões digitais, links locais, exemplos de código, estrutura de tabela/CSV, URLs de origem e lugares não substituídos. Ele rejeita fontes de ferro. Um exame estrutural passando não é uma revisão de orador nativo ou aprovação legal. As páginas geradas apresentam essa qualificação visivelmente.

## Publicação e pacotes

Publicar a documentação, o gerador e o checker juntos. Preservar os termos de licença de terceiros originais; a análise de licença traduzida é informativa. Artigos de construção, cache de tradução e arquivos de orquestração locais não pertencem ao Git. Uma edição de documentação de origem não publica um Windows Instalador

As traduções de aplicativos de usuário são catalogados JSON separados `src/Aivana.Core/Localization`A geração de documentação não altera esses catálogos. um aplicativo construído e sua documentação deve compartilhar uma imagem de imagem versada; o suporte de tempo de execução experimental recentemente descrito pode preceder um pacote instalado.
