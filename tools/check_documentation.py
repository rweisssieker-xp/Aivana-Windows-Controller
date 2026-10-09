"""Validate four-language GitHub docs without fetching external resources."""
from pathlib import Path
import csv
import re
from urllib.parse import unquote

ROOT = Path(__file__).resolve().parents[1]
LOCALES = ("en-US", "de-DE", "fr-FR", "it-IT")
sources = [ROOT / "docs/VERIFICATION.md"]
for directory in ("research", "superpowers", "testing"):
    sources.extend(sorted((ROOT / "docs" / directory).rglob("*.md")))
issues = []
documents = []
for locale in LOCALES:
    documents.extend([ROOT / ("README.md" if locale == "en-US" else f"README.{locale}.md"), ROOT / "docs" / locale / "README.md"])
    for source in sources:
        target = ROOT / "docs" / locale / source.relative_to(ROOT / "docs")
        documents.append(target)
        if not target.exists():
            continue
        original = source.read_text(encoding="utf-8-sig")
        translated = target.read_text(encoding="utf-8-sig")
        source_tables = [line for line in original.splitlines() if line.lstrip().startswith("|")]
        translated_tables = [line for line in translated.splitlines() if line.lstrip().startswith("|")]
        if len(source_tables) != len(translated_tables):
            issues.append(f"{target.relative_to(ROOT)}: Markdown table row count changed")
        elif [len(re.findall(r"(?<!\\)\|", row)) for row in source_tables] != [len(re.findall(r"(?<!\\)\|", row)) for row in translated_tables]:
            issues.append(f"{target.relative_to(ROOT)}: Markdown table column structure changed")
        for url in set(re.findall(r"https?://[^\s<>\)]+", original)):
            if url not in translated:
                issues.append(f"{target.relative_to(ROOT)}: source URL missing: {url}")
        for block in re.findall(r"```(?:powershell|bash|sh|json|csharp|cs|python)\s*\n(.*?)```", original, re.S):
            if block.strip() not in translated:
                issues.append(f"{target.relative_to(ROOT)}: executable/data example changed")
    source_csv = ROOT / "docs/research/tastaturhersteller-sonderfunktionen.csv"
    target_csv = ROOT / "docs" / locale / "research" / source_csv.name
    if not target_csv.exists():
        issues.append(f"Missing {target_csv.relative_to(ROOT)}")
    else:
        with source_csv.open(encoding="utf-8-sig", newline="") as handle:
            source_rows = list(csv.reader(handle))
        with target_csv.open(encoding="utf-8-sig", newline="") as handle:
            target_rows = list(csv.reader(handle))
        if len(source_rows) != len(target_rows):
            issues.append(f"{locale}: CSV row count changed")
        if [len(row) for row in source_rows] != [len(row) for row in target_rows]:
            issues.append(f"{locale}: CSV structure changed")
        if [row[0] for row in source_rows[1:]] != [row[0] for row in target_rows[1:]]:
            issues.append(f"{locale}: CSV brands/order changed")
        if len(source_rows) == len(target_rows):
            for row_number, (source_row, target_row) in enumerate(zip(source_rows[1:], target_rows[1:]), 2):
                if source_row[-1] != target_row[-1]:
                    issues.append(f"{locale}: CSV source URLs changed in row {row_number}")
                source_prose = " ".join(source_row[1:4])
                translated_prose = " ".join(target_row[1:4])
                identifiers = set(re.findall(r"\b(?=[A-Za-z0-9]*\d)(?=[A-Za-z0-9]*[A-Z])[A-Za-z0-9]+\b", source_prose))
                for identifier in identifiers:
                    if identifier not in translated_prose:
                        issues.append(f"{locale}: model identifier {identifier} missing in CSV row {row_number}")
        original_csv = source_csv.read_text(encoding="utf-8-sig")
        translated_csv = target_csv.read_text(encoding="utf-8-sig")
        for url in set(re.findall(r'https?://[^\s\";,]+', original_csv)):
            if url not in translated_csv:
                issues.append(f"{locale}: CSV source URL missing: {url}")

documents.extend(sources)
documents.append(ROOT / "docs/README.md")
for document in documents:
    if not document.exists():
        issues.append(f"Missing {document.relative_to(ROOT)}")
        continue
    content = document.read_text(encoding="utf-8-sig")
    if "\ufffd" in content:
        issues.append(f"{document.relative_to(ROOT)}: Unicode replacement character")
    for link in re.findall(r"\[[^\]]+\]\(([^)]+)\)", content):
        if re.match(r"(?:https?://|mailto:|#)", link):
            continue
        destination = unquote(link.split("#", 1)[0].split("?", 1)[0].strip("<>"))
        if destination and not (document.parent / destination).exists():
            issues.append(f"{document.relative_to(ROOT)}: broken link {link}")

for issue in issues:
    print(issue)
print(f"{len(LOCALES)} languages, {len(sources)} source documents, {len(documents)} documentation pages, {len(issues)} issues")
raise SystemExit(bool(issues))
