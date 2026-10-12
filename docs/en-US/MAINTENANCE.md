<!-- generated-doc locale=en-US source=docs/MAINTENANCE.md sha256=9bf5875b50386e698c0574eee390f790d4b1c29cb11288c856c98e03a05877f7 -->

> Machine-assisted translation. Structure, code and links are checked; native-speaker and legal review are not implied. Historical reports retain their original dates and scope. [Original source](../MAINTENANCE.md)

# Maintaining all documentation languages

The documentation inventory follows all 27 UI languages. The generated folders use regional codes; Norwegian maps to `nb-NO` and Serbian Latin to `sr-Latn`. Each folder contains an index, handbook, detailed action reference, feature inventory, verification notes, research including its CSV, historical plans and licensing analysis.

## Source of truth

Edit the root `README.md`, `docs/HANDBOOK.md`, `docs/FEATURES.md`, `docs/VERIFICATION.md`, this page, or the original files under `docs/research`, `docs/testing`, `docs/superpowers` and `docs/licensing`. Do not rewrite a historical report to imply it validates a later version. Keep implementation, simulation, real-device acceptance and commercial approval distinct.

## Generate and validate

```powershell
python tools/localize_documentation.py --refresh-sources
python tools/check_documentation.py
```

The optional online generator sends documentation prose to Google's public translation endpoint, following the existing localization tooling. It does not read user configuration, secrets or model files. Only run it on documentation suitable for that service. Code blocks, inline code, URLs, table structure and technical identifiers are protected. Translation caches and a frozen source snapshot are stored under ignored `artifacts/documentation-translation`.

When the online provider is rate limited, `tools/translate_documentation_local.py` fills the same cache using an explicitly supplied local M2M100 CTranslate2 model. The 2026-10-12 snapshot uses the MIT-licensed facebook/m2m100_418M model, converted as JustFrederik/m2m_100_418m_ct2_int8 at revision `1aeed44db4dd61a486bba44acf54c76507082a2c`. CTranslate2 4.8.2 and `SentencePiece` 0.2.2 are build-only tools. No model weights or translation dependencies are shipped with MAKRIVO. Run the generator with `--cache-only` after filling the cache to render without network requests. The local helper does not download or execute remote model code.

```powershell
python -m pip install ctranslate2==4.8.2 sentencepiece==0.2.2
python tools/translate_documentation_local.py --model C:\models\m2m100-ct2 --refresh-sources
python tools/localize_documentation.py --cache-only
python tools/check_documentation.py
```

Without `--refresh-sources`, a run resumes its frozen snapshot. Use the refresh option after editing originals. Cached segments are reused. A partial run can select locale codes using `--locales`; finish all locales before publishing. Interrupted runs do not silently substitute untranslated prose for a failed translation.

The manifest records every source and generated file with SHA-256 fingerprints of normalized UTF-8 text: LF line endings and no byte-order mark. The offline checker verifies complete per-language coverage, fingerprints, local links, code examples, table/CSV structure, source URLs and unreplaced placeholders. It rejects stale sources. A passing structural check is not a native-speaker review or legal approval. Generated pages carry this qualification visibly.

## Publication and packages

Publish the documentation, generator and checker together. Preserve original third-party license terms; translated license analysis is informational. Build artifacts, translation cache and local orchestration files do not belong in Git. A source-documentation release does not itself publish a Windows installer.

User-facing app translations are separate JSON catalogs under `src/Aivana.Core/Localization`. Documentation generation does not modify those catalogs. An app build and its documentation should share a versioned snapshot; newly described experimental runtime support may precede an installed package.
