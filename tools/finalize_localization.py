import json, pathlib, re, collections
root = pathlib.Path(__file__).resolve().parents[1]
docs = root / 'docs/localization'
keys = json.loads((docs / 'source-final.json').read_text(encoding='utf-8-sig'))
extras = json.loads((docs / 'delta-native-final.json').read_text(encoding='utf-8-sig'))
overrides = json.loads((docs / 'final-overrides.json').read_text(encoding='utf-8-sig'))
keys = [k for k in keys if k != '{TemplateBinding SelectionBoxItem}']
keys = list(dict.fromkeys(keys + extras))
issues = []
for code, values in overrides.items():
    path = root / f'src/Aivana.Core/Localization/{code}.json'
    data = json.loads(path.read_text(encoding='utf-8-sig'))
    data.pop('{TemplateBinding SelectionBoxItem}', None)
    data.update(dict(zip(extras, values)))
    if set(data) != set(keys): issues.append(f'{code}: key mismatch')
    for key, value in data.items():
        pattern = r'(?<!\{)\{\d+(?:[^{}]*)\}(?!\})'
        if collections.Counter(re.findall(pattern, key)) != collections.Counter(re.findall(pattern, value)):
            issues.append(f'{code}: placeholder mismatch {key!r}')
        if key.count('\n') != value.count('\n'): issues.append(f'{code}: newline mismatch {key!r}')
    path.write_text(json.dumps(data, ensure_ascii=False, indent=2) + '\n', encoding='utf-8')
(docs / 'source-final.json').write_text(json.dumps(keys, ensure_ascii=False, indent=2) + '\n', encoding='utf-8')
print(f'{len(overrides)} languages, {len(keys)} keys; {len(issues)} issues')
for issue in issues: print(issue)
raise SystemExit(bool(issues))
