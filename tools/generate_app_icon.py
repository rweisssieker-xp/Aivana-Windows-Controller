"""Generate the small-size MAKRIVO keyboard mark and Windows ICO resources."""
from pathlib import Path
from PIL import Image, ImageDraw

root = Path(__file__).resolve().parents[1] / 'src/Aivana.Desktop/Assets'
root.mkdir(parents=True, exist_ok=True)
for paused in (False, True):
    im = Image.new('RGBA', (256, 256))
    d = ImageDraw.Draw(im)
    d.rounded_rectangle((12, 12, 244, 244), radius=48, fill='#101719')
    d.rounded_rectangle((30, 30, 226, 226), radius=34, outline='#F2B66D' if paused else '#35D0C1', width=14)
    color = '#F2B66D' if paused else '#35D0C1'
    # Large geometric M: readable at 16px, no font or system dependency.
    d.line([(65, 179), (65, 78), (128, 141), (191, 78), (191, 179)], fill=color, width=22, joint='curve')
    if paused:
        d.rounded_rectangle((153, 150, 243, 243), radius=22, fill='#F2B66D')
        d.rectangle((174, 169, 186, 224), fill='#101719')
        d.rectangle((205, 169, 217, 224), fill='#101719')
    name = 'makrivo-paused' if paused else 'makrivo'
    im.save(root / (name + '.ico'), sizes=[(16,16),(20,20),(24,24),(32,32),(40,40),(48,48),(64,64),(128,128),(256,256)])
    im.save(root / (name + '.png'))
