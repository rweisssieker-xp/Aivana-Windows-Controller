#!/usr/bin/env python3
"""Generate the complete 27-language documentation tree from a frozen snapshot.

The optional online path sends repository documentation prose to Google's
public endpoint. The local helper can fill the cache offline. Code, links and
identifiers are protected; --cache-only renders without network requests.
No application settings, credentials, model files or user profiles are read.
Cache and immutable source snapshot live in ignored artifacts/. Reruns resume.
"""
from __future__ import annotations

import argparse
import concurrent.futures
import csv
import hashlib
import html
import io
import json
import os
from pathlib import Path
import re
import threading
import time
import urllib.parse
import urllib.request
import urllib.error

from localize_added_keys import to_serbian_latin

ROOT = Path(__file__).resolve().parents[1]
DOCS = ROOT / "docs"
WORK = ROOT / "artifacts" / "documentation-translation"
LOCALES = {
    "en-US": ("en", "English (US)"), "de-DE": ("de", "Deutsch"),
    "fr-FR": ("fr", "Français"), "it-IT": ("it", "Italiano"),
    "es-ES": ("es", "Español"), "pt-PT": ("pt", "Português"),
    "nl-NL": ("nl", "Nederlands"), "pl-PL": ("pl", "Polski"),
    "cs-CZ": ("cs", "Čeština"), "sk-SK": ("sk", "Slovenčina"),
    "hu-HU": ("hu", "Magyar"), "ro-RO": ("ro", "Română"),
    "bg-BG": ("bg", "Български"), "el-GR": ("el", "Ελληνικά"),
    "da-DK": ("da", "Dansk"), "sv-SE": ("sv", "Svenska"),
    "nb-NO": ("no", "Norsk bokmål"), "fi-FI": ("fi", "Suomi"),
    "et-EE": ("et", "Eesti"), "lv-LV": ("lv", "Latviešu"),
    "lt-LT": ("lt", "Lietuvių"), "sl-SI": ("sl", "Slovenščina"),
    "hr-HR": ("hr", "Hrvatski"), "uk-UA": ("uk", "Українська"),
    "ru-RU": ("ru", "Русский"), "tr-TR": ("tr", "Türkçe"),
    "sr-Latn": ("sr", "Srpski (latinica)"),
}
COPY = {
    "title": "Documentation", "guide": "User handbook", "reference": "Detailed action reference",
    "features": "Feature inventory and limitations", "research": "Manufacturer research",
    "testing": "Verification and test reports", "development": "Historical development plans",
    "licensing": "Licensing and distribution", "notice": "Machine-assisted translation. Structure, code and links are checked; native-speaker and legal review are not implied. Historical reports retain their original dates and scope.",
    "source": "Original source", "snapshot": "Documentation snapshot", "build": "Build and documentation checks", "maintenance": "Maintaining the documentation",
}
PROTECTED = re.compile(
    r"`+[^`\n]+`+|https?://[^\s<>\])\";,]+|\]\([^\n)]*\)|"
    r"\b(?:MAKRIVO|Aivana|Windows|Microsoft|Corsair|Logitech|SteelSeries|Razer|Wooting|Keychron|QMK|VIA|Ducky|ASUS|HyperX|Roccat|Ollama|ONNX|GenAI|LM Studio|llama\.cpp|iCUE|Synapse|GameSense|OBS|WebSocket|Raw Input|DPAPI|WPF|WinForms|NuGet|GitHub|Pillow|Python|Lemon Squeezy|Paddle|Gumroad|Stripe|Secure Boot|DDC/CI)\b|"
    r"\b(?=[A-Za-z0-9_.:/-]*\d)(?=[A-Za-z0-9_.:/-]*[A-Z])[A-Za-z0-9_.:/-]+\b|"
    r"\b\d+(?:[.,:/–-]\d+)*\b|\*\*|__|\{\{[^}]+\}\}")
TOKEN = re.compile(r"ZZZPH\s*(\d{6})\s*ZZZ", re.I)
LINK = re.compile(r"(!?\[[^\]\n]*\]\()([^\n)]+)(\))")
LOCK = threading.Lock()
LAST_REQUEST = 0.0
REQUEST_INTERVAL = 0.75
CACHE_ONLY = False


def throttle():
    global LAST_REQUEST
    with LOCK:
        delay = REQUEST_INTERVAL - (time.monotonic() - LAST_REQUEST)
        if delay > 0: time.sleep(delay)
        LAST_REQUEST = time.monotonic()


def sha(text: str) -> str:
    return hashlib.sha256(text.encode("utf-8")).hexdigest()


def source_paths() -> list[Path]:
    files = [ROOT / "README.md", DOCS / "HANDBOOK.md", DOCS / "FEATURES.md", DOCS / "VERIFICATION.md", DOCS / "MAINTENANCE.md"]
    for folder in ("research", "testing", "superpowers", "licensing"):
        files += sorted(p for p in (DOCS / folder).rglob("*") if p.suffix in (".md", ".csv", ".html"))
    return files


def frozen_sources(refresh: bool) -> dict[str, str]:
    cache = WORK / "sources.json"
    if cache.exists() and not refresh:
        return json.loads(cache.read_text(encoding="utf-8"))
    sources = {p.relative_to(ROOT).as_posix(): p.read_text(encoding="utf-8-sig") for p in source_paths()}
    WORK.mkdir(parents=True, exist_ok=True)
    cache.write_text(json.dumps(sources, ensure_ascii=False, indent=2), encoding="utf-8")
    return sources


def source_language(text: str) -> str:
    words = re.findall(r"\b\w+\b", text.lower())
    german = sum(words.count(w) for w in ("und", "der", "die", "das", "nicht", "mit", "prüfung"))
    english = sum(words.count(w) for w in ("the", "and", "with", "not", "for", "this", "from"))
    return "de" if german > english else "en"


class Document:
    def __init__(self, path: str, text: str):
        self.path, self.original = path, text
        self.language = source_language(text)
        self.parts: list[tuple[bool, str]] = []
        if path.endswith(".csv"):
            self.rows = list(csv.reader(io.StringIO(text)))
            for ri, row in enumerate(self.rows):
                for ci, cell in enumerate(row):
                    self.add(cell, (ri == 0 or ci not in (0, len(row)-1)) and not cell.startswith("http"))
        elif path.endswith(".html"):
            for part in re.split(r"(<style\b[^>]*>.*?</style>|<[^>]*>)", text, flags=re.S | re.I):
                self.add(part, not part.startswith("<"))
        else:
            fenced = False
            for line in text.splitlines(keepends=True):
                if line.lstrip().startswith("```"):
                    fenced = not fenced
                    self.add(line, False)
                elif fenced or not line.strip() or re.fullmatch(r"[\s|:\-]+", line) or line.lstrip().startswith("<"):
                    self.add(line, False)
                else:
                    prefix = re.match(r"^(\s*(?:(?:#{1,6}|[-*+]|\d+\.)\s+|>\s*)?)", line).group(0)
                    self.add(prefix, False)
                    body = line[len(prefix):].rstrip("\r\n")
                    # Preserve table delimiters and cell whitespace outside translation.
                    for cell_index, cell in enumerate(re.split(r"((?<!\\)\|)", body)):
                        if cell == "|": self.add(cell, False)
                        else:
                            leading = cell[:len(cell)-len(cell.lstrip())]
                            trailing = cell[len(cell.rstrip()):]
                            self.add(leading, False)
                            manufacturer = self.path.startswith("docs/research/") and Path(self.path).name != "obs-integration.md" and body.startswith("|") and cell_index == 2 and cell.strip() not in ("Hersteller", "Hersteller / Modell", "Hersteller/Marke", "Modell", "Marke", "Manufacturer")
                            self.add(cell.strip(), not manufacturer)
                            self.add(trailing, False)
                    self.add("\n" if line.endswith("\n") else "", False)

    def add(self, text: str, translate: bool):
        if translate and not self.path.endswith((".csv", ".html")) and LINK.search(text):
            def prose_fragment(fragment):
                if not fragment.strip():
                    self.add(fragment, False)
                    return
                self.add(fragment[:len(fragment)-len(fragment.lstrip())], False)
                self.add(fragment.strip(), True)
                self.add(fragment[len(fragment.rstrip()):], False)
            cursor = 0
            for match in LINK.finditer(text):
                prose_fragment(text[cursor:match.start()])
                opening = "![" if match.group(1).startswith("![") else "["
                self.add(opening, False)
                label = match.group(1)[len(opening):-2]
                self.add(label, label not in {name for _, name in LOCALES.values()})
                self.add("](" + match.group(2) + ")", False)
                cursor = match.end()
            prose_fragment(text[cursor:])
            return
        self.parts.append((translate and bool(re.search(r"[^\W\d_]", PROTECTED.sub("", text), re.UNICODE)), text))

    def render(self, values: dict[str, str]) -> str:
        parts = [values.get(self.language + "\0" + text, text) if trans else text for trans, text in self.parts]
        if self.path.endswith(".csv"):
            result = io.StringIO(newline="")
            writer = csv.writer(result, lineterminator="\n")
            cursor = 0
            for row in self.rows:
                writer.writerow(parts[cursor:cursor+len(row)])
                cursor += len(row)
            return result.getvalue()
        return "".join(parts)


def translate_batch(items: list[tuple[str, str]], target: str) -> dict[str, str]:
    masks: dict[str, str] = {}
    prepared = []
    for _, text in items:
        def protect(match):
            token = f"ZZZPH{len(masks):06d}ZZZ"
            masks[token] = match.group(0)
            return token
        prepared.append(PROTECTED.sub(protect, text))
    joined = "\n".join(text + (f"\n⟦{i:04d}⟧\n" if i < len(prepared)-1 else "") for i, text in enumerate(prepared))
    params = urllib.parse.urlencode({"client": "gtx", "sl": items[0][0], "tl": target, "dt": "t", "q": joined})
    request = urllib.request.Request("https://translate.googleapis.com/translate_a/single?" + params, headers={"User-Agent": "Mozilla/5.0"})
    for attempt in range(5):
        try:
            throttle()
            with urllib.request.urlopen(request, timeout=45) as response:
                data = json.loads(response.read().decode("utf-8"))
            output = "".join(p[0] for p in data[0] if p and p[0])
            if target == "sr": output = to_serbian_latin(output)
            values = re.split(r"\s*⟦\s*\d{4}\s*⟧\s*", output)
            if len(values) != len(items): raise ValueError("Translation separator count changed")
            result = {}
            for (language, source), translated in zip(items, values):
                translated = TOKEN.sub(lambda m: f"ZZZPH{m.group(1)}ZZZ", translated.strip())
                # Every masked occurrence must survive exactly once in its original segment.
                source_tokens = re.findall(r"ZZZPH\d{6}ZZZ", prepared[len(result)])
                if sorted(source_tokens) != sorted(re.findall(r"ZZZPH\d{6}ZZZ", translated)):
                    raise ValueError("Protected identifier changed")
                for token in source_tokens: translated = translated.replace(token, masks[token])
                if "\n" in translated: translated = " ".join(translated.splitlines())
                result[language + "\0" + source] = translated
            return result
        except ValueError:
            if len(items) > 1:
                mid = len(items)//2
                return {**translate_batch(items[:mid], target), **translate_batch(items[mid:], target)}
            language, source = items[0]
            # A provider may drop an adjacent mask. Translate only prose spans
            # and reconstruct protected values locally rather than losing them.
            pieces, cursor = [], 0
            for match in PROTECTED.finditer(source):
                span = source[cursor:match.start()]
                if span.strip() and re.search(r"[^\W\d_]", span, re.UNICODE):
                    leading = span[:len(span)-len(span.lstrip())]
                    trailing = span[len(span.rstrip()):]
                    span = leading + translate_batch([(language, span.strip())], target)[language + "\0" + span.strip()] + trailing
                pieces.extend((span, match.group(0)))
                cursor = match.end()
            span = source[cursor:]
            if span.strip() and re.search(r"[^\W\d_]", span, re.UNICODE):
                leading = span[:len(span)-len(span.lstrip())]
                trailing = span[len(span.rstrip()):]
                span = leading + translate_batch([(language, span.strip())], target)[language + "\0" + span.strip()] + trailing
            pieces.append(span)
            return {language + "\0" + source: "".join(pieces)}
        except urllib.error.HTTPError as error:
            if error.code == 429:
                raise RuntimeError("Translation service rate limit reached; cached work retained. Resume later with a lower request rate.") from error
            if attempt == 4: raise
            time.sleep(1 + attempt * 2)
        except Exception:
            if attempt == 4:
                if len(items) > 1:
                    mid = len(items)//2
                    return {**translate_batch(items[:mid], target), **translate_batch(items[mid:], target)}
                raise
            time.sleep(1 + attempt * 2)
    raise RuntimeError("No translation response")


def batches(items: list[tuple[str, str]]):
    group, size = [], 0
    for item in items:
        length = len(PROTECTED.sub("ZZZPH000000ZZZ", item[1]))
        if group and (size + length > 2400 or len(group) >= 30 or group[0][0] != item[0]):
            yield group
            group, size = [], 0
        group.append(item); size += length + 12
    if group: yield group


def target_path(locale: str, source: str) -> Path:
    return DOCS / locale / ("REFERENCE.md" if source == "README.md" else source.removeprefix("docs/"))


def rewrite_links(text: str, source: str, target: Path, locale: str, sources: dict[str, str]) -> str:
    def rewrite(match):
        href = match.group(2)
        if re.match(r"(?:[a-z]+:|#)", href): return match.group(0)
        raw, sep, anchor = href.partition("#")
        resolved = (ROOT / source).parent / urllib.parse.unquote(raw)
        relative = Path(os.path.relpath(resolved.resolve(), ROOT)).as_posix()
        link_locale = locale
        if relative.startswith("docs/"):
            pieces = relative.split("/")
            if len(pieces) > 2 and pieces[1] in LOCALES:
                # The dated verification page has explicit language selectors.
                # General manual/index links should follow the reader's locale.
                if source == "docs/VERIFICATION.md" and pieces[-1] == "VERIFICATION.md":
                    link_locale = pieces[1]
                relative = "docs/" + "/".join(pieces[2:])
        if relative == "docs/README.md": destination = DOCS / link_locale / "README.md"
        elif re.fullmatch(r"README(?:\.[a-z]{2}-[A-Z]{2})?\.md", relative):
            matched = re.fullmatch(r"README\.([a-z]{2}-[A-Z]{2})\.md", relative)
            if matched: link_locale = matched.group(1)
            destination = DOCS / link_locale / "REFERENCE.md"
        elif relative in sources: destination = target_path(link_locale, relative)
        else: destination = resolved
        href = Path(os.path.relpath(destination, target.parent)).as_posix() + (sep + anchor if sep else "")
        return match.group(1) + href + match.group(3)
    return LINK.sub(rewrite, text)


def generate_locale(locale: str, docs: list[Document], sources: dict[str, str], workers: int):
    target, native = LOCALES[locale]
    cache_path = WORK / f"{locale}.json"
    cache = json.loads(cache_path.read_text(encoding="utf-8")) if cache_path.exists() else {}
    all_items = dict.fromkeys((doc.language, value) for doc in docs for trans, value in doc.parts if trans)
    all_items.update({("en", value): None for value in COPY.values()})
    missing = []
    for lang, value in all_items:
        key = lang + "\0" + value
        if target == lang: cache[key] = value
        elif key not in cache: missing.append((lang, value))
    if CACHE_ONLY and missing:
        raise RuntimeError(f"{locale}: {len(missing)} uncached segments; fill the cache before using --cache-only")
    todo = list(batches(sorted(missing, key=lambda i: i[0])))
    print(f"{locale}: {len(docs)} documents, {len(missing)} text segments, {len(todo)} requests", flush=True)
    with concurrent.futures.ThreadPoolExecutor(max_workers=workers) as pool:
        futures = [pool.submit(translate_batch, batch, target) for batch in todo]
        for i, future in enumerate(concurrent.futures.as_completed(futures), 1):
            cache.update(future.result())
            cache_path.write_text(json.dumps(cache, ensure_ascii=False), encoding="utf-8")
            if i % 20 == 0 or i == len(todo):
                print(f"{locale}: {i}/{len(todo)} translation batches", flush=True)
    cache_path.write_text(json.dumps(cache, ensure_ascii=False), encoding="utf-8")
    overrides_path = DOCS / "localization" / "documentation-overrides.json"
    if overrides_path.exists():
        corrections = json.loads(overrides_path.read_text(encoding="utf-8")).get(locale, {})
        for source, corrected in corrections.items():
            for language in ("en", "de"):
                if language + "\0" + source in cache:
                    cache[language + "\0" + source] = corrected
    phrase = lambda key: cache["en\0" + COPY[key]]
    entries = []
    for doc in docs:
        destination = target_path(locale, doc.path)
        translated = rewrite_links(doc.render(cache), doc.path, destination, locale, sources)
        if doc.path.endswith(".html"):
            translated = re.sub(r'(<html\s+lang=")[^"]*(")', lambda m: m.group(1) + locale + m.group(2), translated)
            relative = Path(os.path.relpath(ROOT / doc.path, destination.parent)).as_posix()
            banner = '<aside data-documentation-translation="' + locale + '"><p>' + html.escape(phrase('notice')) + '</p><a href="' + html.escape(relative, quote=True) + '">' + html.escape(phrase('source')) + '</a></aside>'
            container = r"(<body[^>]*>)" if re.search(r"<body(?:\s|>)", translated, re.I) else r"(<main[^>]*>)"
            translated, count = re.subn(container, lambda m: m.group(1) + "\n" + banner, translated, count=1, flags=re.I)
            if count != 1: raise ValueError(f"HTML has no translation-notice container: {doc.path}")
        if doc.path.endswith(".md"):
            relative = Path(os.path.relpath(ROOT / doc.path, destination.parent)).as_posix()
            translated = f"<!-- generated-doc locale={locale} source={doc.path} sha256={sha(doc.original)} -->\n\n> {phrase('notice')} [{phrase('source')}]({relative})\n\n" + translated
            if translated.rstrip().splitlines()[-1] != "```":
                translated = translated.rstrip() + "\n"
        destination.parent.mkdir(parents=True, exist_ok=True)
        destination.write_text(translated, encoding="utf-8")
        entries.append({"source": doc.path, "target": destination.relative_to(ROOT).as_posix(), "sourceSha256": sha(doc.original), "translationSha256": sha(translated)})
    links = " · ".join(f"[{name}](../{code}/README.md)" for code, (_, name) in LOCALES.items())
    index = f"# MAKRIVO — {phrase('title')}\n\n{links}\n\n> {phrase('notice')}\n\n{phrase('snapshot')}: 2026-10-12 · Aivana\n\n"
    index += f"- [{phrase('guide')}](HANDBOOK.md)\n- [{phrase('reference')}](REFERENCE.md)\n- [{phrase('features')}](FEATURES.md)\n- [{phrase('build')}](VERIFICATION.md)\n- [{phrase('maintenance')}](MAINTENANCE.md)\n"
    for directory, key in (("testing", "testing"), ("research", "research"), ("licensing", "licensing"), ("superpowers", "development")):
        index += f"\n## {phrase(key)}\n\n"
        for doc in docs:
            if not doc.path.startswith("docs/" + directory + "/"): continue
            path = target_path(locale, doc.path)
            content = path.read_text(encoding="utf-8")
            heading = re.search(r"^#\s+(.+)$", content, re.M)
            title = heading.group(1) if heading else Path(doc.path).name
            index += f"- [{title}]({path.relative_to(DOCS / locale).as_posix()})\n"
    (DOCS / locale / "README.md").write_text(index, encoding="utf-8")
    print(f"{locale}: complete", flush=True)
    return entries


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--locales", nargs="*", choices=list(LOCALES))
    parser.add_argument("--workers", type=int, default=3)
    parser.add_argument("--request-interval", type=float, default=0.75)
    parser.add_argument("--refresh-sources", action="store_true")
    parser.add_argument("--cache-only", action="store_true", help="Render offline; fail on missing translations")
    args = parser.parse_args()
    global REQUEST_INTERVAL, CACHE_ONLY
    CACHE_ONLY = args.cache_only
    REQUEST_INTERVAL = max(0.25, args.request_interval)
    sources = frozen_sources(args.refresh_sources)
    docs = [Document(path, text) for path, text in sources.items()]
    selected = args.locales or list(LOCALES)
    manifest_path = DOCS / "documentation-manifest.json"
    previous = json.loads(manifest_path.read_text(encoding="utf-8")) if manifest_path.exists() else {}
    entries = [row for row in previous.get("documents", []) if row["target"].split("/")[1] not in selected]
    for locale in selected:
        entries += generate_locale(locale, docs, sources, args.workers)
        manifest_path.write_text(json.dumps({"schemaVersion": 1, "hashNormalization": "UTF-8 without BOM; universal newlines normalized to LF", "snapshot": "2026-10-12", "method": "Google public endpoint plus local MIT M2M100 translation; curated overrides. Structural validation is not native-speaker or legal approval.", "translationProvenance": "docs/localization/documentation-translation-provenance.json", "locales": {code: {"language": lang, "name": name} for code, (lang, name) in LOCALES.items()}, "sources": {path: sha(text) for path, text in sources.items()}, "documents": entries}, ensure_ascii=False, indent=2) + "\n", encoding="utf-8")
    index = "# MAKRIVO — Documentation\n\nAivana · 2026-10-12\n\n"
    index += "\n".join(f"- [{name}]({code}/README.md)" for code, (_, name) in LOCALES.items())
    index += "\n\nEach language includes the complete handbook, action reference, features, research, test reports, licensing notes and historical development documents. Translations are machine-assisted; see each page and the manifest for source provenance. Executable examples and source URLs are preserved.\n\n```powershell\npython tools/check_documentation.py\n```\n"
    (DOCS / "README.md").write_text(index, encoding="utf-8")


if __name__ == "__main__":
    main()
