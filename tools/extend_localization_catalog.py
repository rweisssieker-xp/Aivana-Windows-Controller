"""Merge explicitly authored source keys into the offline canonical localization catalog."""
import json
import pathlib
import sys

root = pathlib.Path(__file__).resolve().parents[1]
source = root / 'docs/localization/source-final.json'
previous = json.loads(source.read_text(encoding='utf-8-sig'))
keys = list(previous)
for argument in sys.argv[1:]:
    data = json.loads((root / argument).read_text(encoding='utf-8-sig'))
    keys.extend(data.get('keys', list(data)) if isinstance(data, dict) else data)
keys = list(dict.fromkeys(keys))
source.write_text(json.dumps(keys, ensure_ascii=False, indent=2) + '\n', encoding='utf-8')
delta = [key for key in keys if key not in previous]
(root / 'docs/localization/delta-latest.json').write_text(json.dumps(delta, ensure_ascii=False, indent=2) + '\n', encoding='utf-8')
print(f'{len(keys)} source keys; {len(delta)} new')
