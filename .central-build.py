import json,re
from pathlib import Path
root=Path(__file__).parent
keys=json.loads((root/'docs/localization/source-final.json').read_text(encoding='utf-8-sig'))
missing=json.loads((root/'.central-missing.json').read_text(encoding='utf-8-sig'))
draft=json.loads((root/'.initial-175-draft.json').read_text(encoding='utf-8-sig'))
for n in range(1,5):
    for line in (root/f'.central-{n}.tsv').read_text(encoding='utf-8').splitlines():
        cols=line.split('\t')
        assert len(cols)==5,(n,cols)
        draft[missing[int(cols[0])]]=[s.replace('\\n','\n') for s in cols[1:]]
for key in keys:
    if key not in draft and (key.startswith('#') or key in ['A','AIVANA','Aivana','Aivana Controller','Corsair','Corsair G{0}','Ctrl+K','{TemplateBinding SelectionBoxItem}']):
        draft[key]=[key]*4
remaining=[k for k in keys if k not in draft]
assert not remaining,remaining
for i,code in enumerate(['pl','cs','sk','hu']):
    data={k:draft[k][i] for k in keys}
    for key,value in data.items():
        assert sorted(re.findall(r'\{[^{}]*\}',key))==sorted(re.findall(r'\{[^{}]*\}',value)),(code,key,value)
        assert key.count('\n')==value.count('\n'),(code,key,value)
        assert value.strip(),(code,key)
    (root/f'src/Aivana.Core/Localization/{code}.json').write_text(json.dumps(data,ensure_ascii=False,indent=2)+'\n',encoding='utf-8')
    print(code,len(data),'keys; placeholder/newline parity verified')
