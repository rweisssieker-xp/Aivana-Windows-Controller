<!-- generated-doc locale=fr-FR source=docs/licensing/2026-10-12-commercial-license-audit.md sha256=b58c6861e0b38c4495265651b5ab10349c701d8fb4582dd7d7cae5d2d865bc5f -->

> Traduction assistée par machine. La structure, les exemples de code et les liens sont vérifiés ; cela ne constitue ni une révision par un locuteur natif ni une validation juridique. Les rapports historiques conservent leur date et leur portée d'origine. [Source originale](../../licensing/2026-10-12-commercial-license-audit.md)

# MAKRIVO : Chèque pour vente commerciale unique

Statut : 12. Octobre 2026. Fabricant : Aivana.

## Résultat

Dans l'inventaire audité, aucune obligation générale de divulguer son propre code source MAKRIVO n'a été identifiée. Une vente unique et exclusive semble possible, mais n'est pas encore entièrement préparée. En particulier, l’autorisation de redistribution pour la DLL Corsair-SDK-DLL existante n’est pas documentée. Cet examen ne remplace pas l’approbation légale.

## Résultats techniques et de licence

| composant | Résultats | Étape obligatoire |
| --- | --- | --- |
| Propre code source | Aucune LICENCE, COPIE ou AVIS propre trouvé dans l'inventaire Git examiné et dans son historique de fichiers de licence disponible localement. | Confirmer la propriété des droits, y compris les cotisations, les fragments de code adoptés et les éventuels droits de l'employeur ; créez vos propres conditions d'utilisation. |
| .NET / Bureau Windows | Le package autonome examiné sous `artifacts/makrivo-win-x64` contient la version 10.0.6. Les packages d'exécution locaux contiennent des licences MIT qui permettent une distribution commerciale et nécessitent l'inclusion de notifications. Aucun fichier de licence/avis correspondant n'a été trouvé dans les dossiers de sortie examinés. | Incluez les textes de licence exacts et les informations tierces applicables pour le runtime réellement fourni dans le package de vente. |
| Dépendances NuGet | Aucune entrée PackageReference directe trouvée dans les fichiers de projet examinés. Le fichier de ressources de bureau examiné contient ses propres dépendances de projet. Les packages d'exécution restent toujours pertinents pour la licence. | Réinventez tous les projets, packages transitifs et fichiers groupés lors de la version finale. |
| Corsair iCUE SDK | `artifacts/win-x64/iCUESDK.x64_2019.dll`, version du fichier 4.0.84.0. Le PDF de référence du SDK local contient sur les pages imprimées 35–38 le CLUF : utilisation libre de droits, mais restrictions de transfert/cession. Il n'y avait pas d'autorisation claire pour transmettre la DLL aux clients. | Obtenez une autorisation de redistribution robuste avant le regroupement. Vous pouvez également vérifier un package sans DLL SDK qui utilise un SDK installé par le client ; Les conditions du SDK s'appliquent également à cela. |
| Différents forfaits | `artifacts/makrivo-win-x64` ne contient pas la DLL Corsair ; `artifacts/win-x64` les contient. L'application recherche dynamiquement le SDK à plusieurs emplacements. | Définissez le programme d'installation de vente au détail actuel et vérifiez son contenu. La simple existence d'un package sans DLL ne confirme pas tous les droits d'intégration. |
| OBS | Posséder le client WebSocket dans le code C# examiné ; aucun package client OBS, binaire OBS ou code GPL adopté identifié. | L’intégration protocolaire observée ne justifie en elle-même aucune obligation de divulgation GPL établie. L'origine des fragments qui auraient pu être adoptés reste à confirmer. |
| Icônes d'application | Un script local Python dessine des symboles géométriques à l'aide de Pillow. L'outil est une dépendance de build. | Documentez l’origine et les droits de toutes les images, sons, polices et autres actifs réellement livrés. Le script à lui seul ne prouve pas les droits sur chaque actif. |
| Gestion des licences | Aucune vérification complète de licence d'achat/d'activation identifiée dans le code de la demande examinée. | Si vous le souhaitez, implémentez des fichiers de licence hors ligne signés. L'activation technique et les conditions juridiques d'utilisation sont des tâches distinctes. |

## Modèle d’achat judicieux – pas encore mis en œuvre

- paiement unique ; Droit d'utilisation permanent de la version principale achetée.
- Une personne utilisant jusqu'à deux de ses propres PC ; Autoriser expressément un usage professionnel.
- Incluez des corrections de bogues et des mises à jour mineures ; Proposez éventuellement des versions majeures ultérieures moyennant des frais.
- Fichier de licence signé numériquement qui peut être vérifié hors ligne ; Ne fournissez jamais de clés de signature privées avec l’application.
- Les droits légaux et les mises à jour de sécurité requises ne peuvent pas être exclus de manière générale par la règle des versions majeures.

## Ouvert avant la première vente

1. Corsair Clarifiez les droits de distribution ou préparez-vous à vendre sans DLL SDK groupée.
2. Incluez les textes de licence et de tiers pour les dépendances exactes de la version et recevez-les dans le programme d'installation.
3. Confirmez la propriété des droits sur votre propre code et vos actifs ; Créez un CLUF avec des règles claires sur les appareils, les utilisateurs et les mises à niveau.
4. Pour les ventes aux consommateurs, gérez le paiement, y compris les exigences d’informations et l’annulation. Une déclaration générale selon laquelle « aucun remboursement pour les téléchargements » n’est pas suffisante. L'expiration du droit de rétractation en cas de mise à disposition immédiate des marchandises nécessite les exigences légales.
5. Revérifiez le programme d'installation terminé, y compris les plugins et toutes les dépendances transitives. La copie de travail est développée en parallèle ; Cet inventaire ne constitue pas une version des fichiers futurs.

## GitHub

Le référentiel examiné est public. Sans licence open source, le code ne devient pas automatiquement libre pour toute utilisation ultérieure. Les droits de plateforme GitHub pour les référentiels publics doivent toujours être respectés. Un passage ultérieur au mode privé ne supprime pas les copies ou forks existants. La visibilité et la licence n'ont pas été modifiées lors de cet examen.

## Preuves et sources

- Licences .NET locales : `C:/Users/weiss/.nuget/packages/microsoft.netcore.app.runtime.win-x64/10.0.6/LICENSE.TXT` et `C:/Users/weiss/.nuget/packages/microsoft.windowsdesktop.app.runtime.win-x64/10.0.6/LICENSE`.
- Avis tiers .NET locaux : `C:/Users/weiss/.nuget/packages/microsoft.netcore.app.runtime.win-x64/10.0.6/THIRD-PARTY-NOTICES.TXT`.
- SDK Corsair local : `artifacts/dependencies/iCUESDK_4.0.84.zip` ; PDF de référence extrait à `sdk/iCUESDK/doc/iCUE SDK - Overview and Reference.pdf`.
- [.NET 10.0.6 Licence MIT](https://raw.githubusercontent.com/dotnet/runtime/v10.0.6/LICENSE.TXT)
- [Corsair Documentation SDK avec EULA](https://raw.githubusercontent.com/CorsairOfficial/cue-sdk/master/docs/index.html)
- [GitHub : Licence des repositories](https://docs.github.com/en/repositories/managing-your-repositorys-settings-and-features/customizing-your-repository/licensing-a-repository)
- [BGB § 327f – mises à jour](https://www.gesetze-im-internet.de/bgb/__327f.html)
- [BGB § 356 – Droit de rétractation](https://www.gesetze-im-internet.de/bgb/__356.html)

Non vérifié : historique complet des contributions externes et chaînes de droits, libération de la marque, paiement des ventes existantes, futurs packages de plugins, pleine efficacité juridique d'un CLUF non encore disponible.
