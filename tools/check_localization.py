"""Check embedded dictionaries against the canonical authoring catalog, offline."""
import collections
import json
import pathlib
import re

root = pathlib.Path(__file__).resolve().parents[1]
keys = set(json.loads((root / 'docs/localization/source-final.json').read_text(encoding='utf-8-sig')))
token = re.compile(r'(?<!\{)\{\d+[^{}]*\}(?!\})')
issues = []
files = list((root / 'src/Aivana.Core/Localization').glob('*.json'))
for path in files:
    data = json.loads(path.read_text(encoding='utf-8-sig'))
    if set(data) != keys: issues.append(f'{path.stem}: keyset differs')
    for source, translated in data.items():
        if collections.Counter(token.findall(source)) != collections.Counter(token.findall(translated)):
            issues.append(f'{path.stem}: format mismatch {source!r}')
        if source.count('\n') != translated.count('\n'):
            issues.append(f'{path.stem}: newline mismatch {source!r}')
        if '\ufffd' in source + translated or re.search(r'ZZZPH\d+', translated):
            issues.append(f'{path.stem}: damaged text {source!r}')
        if path.stem == 'de' and source != translated: issues.append(f'de: source differs {source!r}')
print(f'{len(files)} languages, {len(keys)} keys per language, {len(issues)} issues')
for issue in issues: print(ascii(issue))
raise SystemExit(bool(issues))
