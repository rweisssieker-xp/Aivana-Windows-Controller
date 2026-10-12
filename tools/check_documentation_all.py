#!/usr/bin/env python3
"""Offline coverage/provenance/link/code/table checks; not linguistic approval."""
from pathlib import Path
import csv
import hashlib
from html.parser import HTMLParser
import io
import json
import re
from urllib.parse import unquote
from localize_documentation import LOCALES, source_paths, target_path

ROOT = Path(__file__).resolve().parents[1]
MANIFEST = ROOT / "docs/documentation-manifest.json"

def digest(text): return hashlib.sha256(text.encode("utf-8")).hexdigest()
def code_blocks(text): return re.findall(r"^```[^\n]*\n.*?^```\s*$", text, re.M | re.S)
def without_fences(text): return re.sub(r"^```[^\n]*\n.*?^```\s*$", "", text, flags=re.M | re.S)
def links(text): return re.findall(r"\[[^\]\n]*\]\(([^)\n]+)\)", text)
def urls(text): return set(re.findall(r"https?://[^\s<>\)\]\";,]+", text))

class HtmlStructure(HTMLParser):
    def __init__(self):
        super().__init__(); self.tags, self.styles, self.in_style = [], [], False
    def handle_starttag(self, tag, attrs):
        self.tags.append(("start", tag))
        if tag == "style": self.in_style = True
    def handle_endtag(self, tag):
        self.tags.append(("end", tag))
        if tag == "style": self.in_style = False
    def handle_data(self, data):
        if self.in_style: self.styles.append(data)

def main():
    if not MANIFEST.exists():
        print("Missing documentation manifest. Run tools/localize_documentation.py first."); return 1
    manifest = json.loads(MANIFEST.read_text(encoding="utf-8"))
    issues = []
    localization_source = (ROOT / "src/Aivana.Core/Localization.cs").read_text(encoding="utf-8-sig")
    language_array = re.search(r"Languages\s*=\s*\[(.*?)\]", localization_source, re.S)
    application_languages = set(re.findall(r'"([a-z]{2})"', language_array.group(1))) if language_array else set()
    documentation_languages = {"nb" if code == "no" else code for code, _ in LOCALES.values()}
    if application_languages != documentation_languages:
        issues.append("Documentation language inventory differs from application supported languages")
    expected = {p.relative_to(ROOT).as_posix() for p in source_paths()}
    recorded = set(manifest["sources"])
    for missing in sorted(expected - recorded): issues.append(f"Missing source in manifest: {missing}")
    for extra in sorted(recorded - expected): issues.append(f"Removed source remains in manifest: {extra}")
    sources = {}
    for source, fingerprint in manifest["sources"].items():
        path = ROOT / source
        if not path.exists(): issues.append(f"Missing source: {source}"); continue
        sources[source] = path.read_text(encoding="utf-8-sig")
        if digest(sources[source]) != fingerprint: issues.append(f"Source changed after translation: {source}")
    if set(manifest["locales"]) != set(LOCALES): issues.append("Manifest language inventory differs from generator")
    translated = {locale: set() for locale in LOCALES}
    documents = [ROOT / "docs/README.md", ROOT / "README.md"]
    documents += [ROOT / "docs" / locale / "README.md" for locale in LOCALES]
    for entry in manifest["documents"]:
        source, target = entry["source"], entry["target"]
        locale = target.split("/")[1]
        if source in translated[locale]: issues.append(f"Duplicate source: {locale}/{source}")
        translated[locale].add(source)
        path = ROOT / target
        if not path.exists(): issues.append(f"Missing translated document: {target}"); continue
        content = path.read_text(encoding="utf-8-sig")
        original = sources.get(source, "")
        if digest(content) != entry["translationSha256"]: issues.append(f"Translation changed after generation: {target}")
        if entry["sourceSha256"] != manifest["sources"].get(source): issues.append(f"Incorrect source fingerprint: {target}")
        if "\ufffd" in content: issues.append(f"Unicode replacement character: {target}")
        if re.search(r"ZZZPH\s*\d|⟦\s*\d{4}", content): issues.append(f"Unrestored translation marker: {target}")
        if re.search(r"\[\s*9\d{5}\s*\]", content): issues.append(f"Unrestored local translation marker: {target}")
        if path.suffix == ".md":
            documents.append(path)
            if not content.startswith(f"<!-- generated-doc locale={locale} source={source}"):
                issues.append(f"Missing translation provenance: {target}")
            if code_blocks(original) != code_blocks(content): issues.append(f"Executable/data blocks changed: {target}")
            if len(links(without_fences(content))) != len(links(without_fences(original))) + 1:
                issues.append(f"Markdown link structure changed: {target}")
            if sorted(re.findall(r"`[^`\n]+`", without_fences(original))) != sorted(re.findall(r"`[^`\n]+`", without_fences(content))):
                issues.append(f"Inline technical examples changed: {target}")
            source_tables = [len(re.findall(r"(?<!\\)\|", row)) for row in original.splitlines() if row.lstrip().startswith("|")]
            target_tables = [len(re.findall(r"(?<!\\)\|", row)) for row in content.splitlines() if row.lstrip().startswith("|")]
            if source_tables != target_tables: issues.append(f"Markdown table structure changed: {target}")
        elif path.suffix == ".csv":
            a, b = list(csv.reader(io.StringIO(original))), list(csv.reader(io.StringIO(content)))
            if [len(r) for r in a] != [len(r) for r in b]: issues.append(f"CSV row/column structure changed: {target}")
            elif [(r[0], r[-1]) for r in a[1:]] != [(r[0], r[-1]) for r in b[1:]]:
                issues.append(f"CSV manufacturer identities/source URLs changed: {target}")
        elif path.suffix == ".html":
            documents.append(path)
            banners = re.findall(r'<aside data-documentation-translation="[^"]+">.*?</aside>', content, re.S)
            if len(banners) != 1: issues.append(f"Missing HTML translation notice: {target}")
            structure_content = re.sub(r'<aside data-documentation-translation="[^"]+">.*?</aside>', "", content, flags=re.S)
            a, b = HtmlStructure(), HtmlStructure(); a.feed(original); b.feed(structure_content)
            if a.tags != b.tags or a.styles != b.styles: issues.append(f"HTML structure/styles changed: {target}")
        if urls(original) - urls(content): issues.append(f"External source URL missing: {target}")
    for locale, available in translated.items():
        for missing in sorted(recorded - available): issues.append(f"Missing {locale} translation: {missing}")
        index = ROOT / "docs" / locale / "README.md"
        if index.exists():
            destinations = {(index.parent / unquote(link.split("#", 1)[0])).resolve() for link in links(index.read_text(encoding="utf-8-sig")) if not re.match(r"(?:[a-z]+:|#)", link)}
            for source in recorded:
                if target_path(locale, source).resolve() not in destinations:
                    issues.append(f"Document missing from {locale} index: {source}")
    for document in documents:
        if not document.exists(): issues.append(f"Missing documentation index: {document.relative_to(ROOT)}"); continue
        content = without_fences(document.read_text(encoding="utf-8-sig"))
        document_links = links(content)
        if document.suffix == ".html": document_links += re.findall(r'\bhref="([^"\n]+)"', content)
        for link in document_links:
            if re.match(r"(?:[a-z]+:|#)", link): continue
            destination = unquote(link.split("#", 1)[0].split("?", 1)[0].strip("<>"))
            if destination and not (document.parent / destination).exists():
                issues.append(f"{document.relative_to(ROOT)}: broken link {link}")
    for issue in issues: print(issue)
    print(f"{len(LOCALES)} languages, {len(recorded)} source documents, {len(manifest['documents'])} translations, {len(issues)} issues")
    return bool(issues)

if __name__ == "__main__": raise SystemExit(main())
