<!-- generated-doc locale=pt-PT source=docs/licensing/2026-10-12-commercial-license-audit.md sha256=b58c6861e0b38c4495265651b5ab10349c701d8fb4582dd7d7cae5d2d865bc5f -->

> Tradução assistida por máquina. A estrutura, os exemplos de código e as ligações são verificados; isto não constitui revisão por um falante nativo nem aprovação jurídica. Os relatórios históricos mantêm as datas e o âmbito originais. [Fonte original](../../licensing/2026-10-12-commercial-license-audit.md)

# MAKRIVO: Verifique para venda comercial única

Status: 12. Outubro 2026. Fabricante: Aivana.

## Resultado

No inventário auditado não foi identificada qualquer obrigação geral de divulgação do próprio código-fonte MAKRIVO. Uma venda única proprietária parece possível, mas ainda não está totalmente preparada. Em particular, a permissão de redistribuição para o Corsair-SDK-DLL existente não está documentada. Esta revisão não substitui a aprovação legal.

## Descobertas técnicas e de licenciamento

| componente | Descobertas | Etapa necessária |
| --- | --- | --- |
| Código fonte próprio | Nenhuma LICENÇA, CÓPIA ou AVISO própria encontrada no inventário Git examinado e em seu histórico de arquivo de licença disponível localmente. | Confirmar a propriedade dos direitos, incluindo contribuições, fragmentos de código adotados e possíveis direitos do empregador; crie seus próprios termos de uso. |
| .NET/Área de Trabalho do Windows | O pacote independente examinado em `artifacts/makrivo-win-x64` contém a versão 10.0.6. Os pacotes de tempo de execução local contêm licenças do MIT que permitem a distribuição comercial e exigem a inclusão de avisos. Nenhum arquivo de licença/aviso correspondente foi encontrado nas pastas de saída examinadas. | Inclua textos de licença exatos e informações de terceiros aplicáveis para o tempo de execução realmente entregue no pacote de vendas. |
| Dependências NuGet | Nenhuma entrada direta de PackageReference encontrada nos arquivos de projeto examinados. O arquivo de ativos da área de trabalho que está sendo examinado contém suas próprias dependências de projeto. Os pacotes de tempo de execução ainda permanecem relevantes para a licença. | Reinventariar todos os projetos, pacotes transitivos e arquivos agrupados no lançamento final. |
| Corsair iCUE SDK | `artifacts/win-x64/iCUESDK.x64_2019.dll`, versão do arquivo 4.0.84.0. O PDF de referência do SDK local contém nas páginas impressas 35–38 o EULA: uso isento de royalties, mas com restrições de transferência/atribuição. Não havia permissão clara para repassar a DLL aos clientes. | Obtenha permissão robusta de redistribuição antes de agrupar. Como alternativa, verifique um pacote sem uma DLL SDK que use um SDK instalado pelo cliente; As condições do SDK também se aplicam a isso. |
| Pacotes diferentes | `artifacts/makrivo-win-x64` não contém a DLL Corsair; `artifacts/win-x64` os contém. O aplicativo procura dinamicamente o SDK em vários locais. | Defina o instalador de varejo real e verifique seu conteúdo. A mera existência de um pacote sem DLL não confirma todos os direitos de integração. |
| OBS | Próprio cliente WebSocket no código C# examinado; nenhum pacote de cliente OBS, binário OBS ou código GPL adotado identificado. | A integração de protocolo observada não justifica, por si só, qualquer obrigação de divulgação estabelecida pela GPL. A origem de quaisquer fragmentos que possam ter sido adotados ainda não foi confirmada. |
| Ícones de aplicativos | Um script Python local desenha símbolos geométricos usando Pillow. A ferramenta é uma dependência de construção. | Documente a origem e os direitos de todas as imagens, sons, fontes e outros bens efetivamente entregues. O script por si só não prova os direitos de cada ativo. |
| Gerenciamento de licenças | Nenhuma verificação completa de licença/ativação de compra identificada no código do aplicativo examinado. | Se desejar, implemente arquivos de licença offline assinados. A ativação técnica e os termos legais de uso são tarefas separadas. |

## Modelo de compra sensato – ainda não implementado

- pagamento único; Direito permanente de uso da versão principal adquirida.
- Uma pessoa em até dois PCs próprios; Permitir expressamente o uso profissional.
- Inclui correções de bugs e pequenas atualizações; Opcionalmente, ofereça versões principais posteriores mediante o pagamento de uma taxa.
- Arquivo de licença assinado digitalmente que pode ser verificado offline; Nunca entregue chaves de assinatura privadas com o aplicativo.
- Os direitos legais e as atualizações de segurança necessárias não podem ser excluídos de forma geral pela regra da versão principal.

## Aberto antes da primeira venda

1. Corsair Esclareça os direitos de distribuição ou prepare-se para vender sem o SDK DLL incluído.
2. Inclua textos de licença e de terceiros para as dependências exatas do lançamento e receba-os no instalador.
3. Confirme a propriedade dos direitos sobre seu próprio código e ativos; Crie um EULA com regras claras de dispositivo, usuário e atualização.
4. Para vendas ao consumidor, cuide do checkout, incluindo requisitos de informação e cancelamento. Uma declaração geral de “sem reembolso para downloads” não é suficiente. A caducidade do direito de rescisão se a mercadoria for disponibilizada imediatamente exige os requisitos legais.
5. Verifique novamente o instalador finalizado, incluindo plug-ins e todas as dependências transitivas. A cópia de trabalho é expandida em paralelo; Este inventário não é uma divulgação de arquivos futuros.

## GitHub

O repositório examinado é público. Sem uma licença de código aberto, o código não se torna automaticamente gratuito para qualquer uso posterior. Os direitos da plataforma GitHub para repositórios públicos ainda devem ser observados. Uma mudança posterior para privado não remove cópias ou bifurcações existentes. A visibilidade e a licença não foram alteradas durante esta revisão.

## Evidências e fontes

- Licenças locais .NET: `C:/Users/weiss/.nuget/packages/microsoft.netcore.app.runtime.win-x64/10.0.6/LICENSE.TXT` e `C:/Users/weiss/.nuget/packages/microsoft.windowsdesktop.app.runtime.win-x64/10.0.6/LICENSE`.
- Avisos locais de terceiros do .NET: `C:/Users/weiss/.nuget/packages/microsoft.netcore.app.runtime.win-x64/10.0.6/THIRD-PARTY-NOTICES.TXT`.
- SDK Corsair local: `artifacts/dependencies/iCUESDK_4.0.84.zip`; PDF de referência extraído em `sdk/iCUESDK/doc/iCUE SDK - Overview and Reference.pdf`.
- [.NET 10.0.6 Licença do MIT](https://raw.githubusercontent.com/dotnet/runtime/v10.0.6/LICENSE.TXT)
- [Corsair Documentação SDK com EULA](https://raw.githubusercontent.com/CorsairOfficial/cue-sdk/master/docs/index.html)
- [GitHub: Licenciar Repositorias](https://docs.github.com/en/repositories/managing-your-repositorys-settings-and-features/customizing-your-repository/licensing-a-repository)
- [BGB § 327f – atualizações](https://www.gesetze-im-internet.de/bgb/__327f.html)
- [BGB § 356 – Direito de revogação](https://www.gesetze-im-internet.de/bgb/__356.html)

Não verificado: histórico completo de contribuições externas e cadeias de direitos, liberação de marca registrada, verificação de vendas existente, pacotes de plug-ins futuros, eficácia legal total de um EULA ainda não disponível.
