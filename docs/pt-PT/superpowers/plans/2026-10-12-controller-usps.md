<!-- generated-doc locale=pt-PT source=docs/superpowers/plans/2026-10-12-controller-usps.md sha256=b7af0d36209e04b458ce64a5db78ab7e552a84c5e9b450ea0bd97179a4eb7f68 -->

> Tradução assistida por máquina. A estrutura, os exemplos de código e as ligações são verificados; isto não constitui revisão por um falante nativo nem aprovação jurídica. Os relatórios históricos mantêm as datas e o âmbito originais. [Fonte original](../../../superpowers/plans/2026-10-12-controller-usps.md)

# Plano de implementação de extensão do controlador

**Meta:** Implementar o monitor solicitado, gesto, fluxo de trabalho, gravação, diagnóstico de dispositivo, troca de perfil portátil e melhorias de bandeja.

**Arquitetura:** Estenda as camadas Core/Windows/Desktop existentes. Máquinas de estado puro e lógica de configuração portátil vivem no Core, integrações nativas no Windows e editores de rascunho modais no Desktop. Preserve as edições sujas existentes no espaço de trabalho e a configuração do usuário implantada.

**Spec:** ../specs/2026-10-12-controller-usps-design.md

- [x] Monitorar serviço, persistência de alvo e status ao vivo assíncrono; testar IDs indisponíveis e cancelamento.
- [x] Máquina de estados de gestos e camadas retidas; testar bordas, repetir, redefinir e ligações imediatas existentes.
- [x] Condições de fluxo de trabalho, encaminhamento de ramificações e política de erros; instantâneos de teste e cancelamento.
- [x] Gravação de atalho explícito com revisão de rascunho; teste de filtragem e descarte de gancho.
- [x] Troca de perfil de pasta compartilhada com verificações de revisão e visualizações; testar conflitos e mapeamentos portáteis.
- [x] Chaves especiais e diagnósticos padrão genéricos por dispositivo; análise de teste, mapeamentos duplicados e comportamento de pausa.
- [x] Recursos de bandeja/janela/ícones executáveis e aparência pausada; verifique os recursos no executável publicado.
- [x] Integrar todos os pontos de entrada da paleta/editor e texto localizado; execute as verificações Core, Windows e WPF.
- [x] Publique em um diretório de teste separado, preserve as configurações do usuário e reinicie o aplicativo atualizado.
