<!-- generated-doc locale=fr-FR source=docs/MAINTENANCE.md sha256=9bf5875b50386e698c0574eee390f790d4b1c29cb11288c856c98e03a05877f7 -->

> Traduction assistée par machine. La structure, les exemples de code et les liens sont vérifiés ; cela ne constitue ni une révision par un locuteur natif ni une validation juridique. Les rapports historiques conservent leur date et leur portée d'origine. [Source originale](../MAINTENANCE.md)

# Maintenir toutes les langues de documentation

L'inventaire de la documentation suit tous les 27 Les dossiers générés utilisent des codes régionaux ; les cartes norvégiennes `nb-NO` Le latin serbe à `sr-Latn`Chaque dossier contient un indice, un manuel, une référence d'action détaillée, un enregistrement de caractéristiques, des notes de vérification, des recherches y compris son CSV, des plans historiques et une analyse de licence.

## source de vérité

Edit la racine `README.md`, `docs/HANDBOOK.md`, `docs/FEATURES.md`, `docs/VERIFICATION.md`Cette page ou les fichiers originaux ci-dessous `docs/research`, `docs/testing`, `docs/superpowers` et `docs/licensing`Ne réécrire pas un rapport historique pour indiquer qu'il valide une version ultérieure. Gardez la mise en œuvre, la simulation, l'acceptation de dispositifs réels et l'approbation commerciale distinct.

## Gérer et valider

```powershell
python tools/localize_documentation.py --refresh-sources
python tools/check_documentation.py
```

Le générateur en ligne facultatif envoie la documentation à la fin de la traduction publique de Google, suivant l'outil de localisation existant. Il ne lit pas la configuration de l'utilisateur, les secrets ou les fichiers de modèle. Seulement le fonctionner sur la documentation appropriée pour ce service. Les blocs de code, les codes en ligne, les URL, la structure de table et les identifiants techniques sont protégés. Les cache de traduction et un snapshot de source congelée sont stockés sous ignoré `artifacts/documentation-translation`.

Lorsque le fournisseur en ligne est limité, `tools/translate_documentation_local.py` remplir le même cache en utilisant un local explicitement fourni M2M100 CTranslate2 Le modèle, le 2026-10-12 Snapshot utilise la licence MIT facebook/m2m100_418M Modèles convertis comme JustFrederik/m2m_100_418m_ct2_int8 à la révision `1aeed44db4dd61a486bba44acf54c76507082a2c`. CTranslate2 4.8.2 et `SentencePiece` 0.2.2 sont des outils de construction uniquement. aucun poids de modèle ou dépendances de traduction sont envoyés avec MAKRIVO. Faire fonctionner le générateur `--cache-only` après avoir rempli le cache pour rendre sans requêtes réseau. l'assistant local ne télécharge pas ou exécute le code de modèle à distance.

```powershell
python -m pip install ctranslate2==4.8.2 sentencepiece==0.2.2
python tools/translate_documentation_local.py --model C:\models\m2m100-ct2 --refresh-sources
python tools/localize_documentation.py --cache-only
python tools/check_documentation.py
```

Sans `--refresh-sources`, une rentrée récupère son coup de vue gelé. Utilisez l'option de refroidissement après l'édition des originaux. Les segments cachés sont réutilisés. Une rentrée partielle peut sélectionner des codes locaux en utilisant `--locales`Les cours interrompus ne remplacent pas silencieusement la prose non traduite par une traduction échouée.

Les fichiers manifestes sont enregistrés à chaque source et générés avec SHA-256 Les empreintes digitales normalisées UTF-8 texte: finitions de la ligne LF et pas de marque d'ordre byte. Le contrôleur hors ligne vérifie la couverture complète par langue, les empreintes digitales, les liens locaux, les exemples de code, la structure du tableau/CSV, les URL source et les locataires non remplacés. Il rejette les sources d'acier. Un contrôle structurel passant n'est pas une revue de l'appareil indigène ou l'approbation légale..

## Publication et paquets

Publicer la documentation, le générateur et le checker ensemble. préserver les termes de licence d'origine de tiers; l'analyse de licence traduite est informative. Construire des documents, la cache de traduction et les fichiers d'orchestre locaux ne sont pas des Git. Une source de documentation ne publie pas lui-même un Windows Installateur

Les traductions d’applications sont séparées par les catalogues JSON `src/Aivana.Core/Localization`La génération de la documentation ne modifie pas ces catalogues. Une application construite et sa documentation devraient partager un snapshot versionné; le support expérimental récemment décrit peut précéder un paquet installé.
