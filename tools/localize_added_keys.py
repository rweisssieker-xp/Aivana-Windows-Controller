#!/usr/bin/env python3
"""Translate canonical localization strings missing from locale JSON files.

This uses Google's public web translation endpoint for UI text only. It never
reads application settings or secret files. Existing locale values are retained.
"""

from __future__ import annotations

import concurrent.futures
import hashlib
import json
import pathlib
import re
import sys
import time
import urllib.error
import urllib.parse
import urllib.request


ROOT = pathlib.Path(__file__).resolve().parents[1]
LOCALIZATION = ROOT / "src" / "Aivana.Core" / "Localization"
SOURCE = ROOT / "docs" / "localization" / "source-final.json"
LANGUAGES = {
    "de": "de", "en": "en", "fr": "fr", "es": "es", "it": "it",
    "pt": "pt", "nl": "nl", "pl": "pl", "cs": "cs", "sk": "sk",
    "hu": "hu", "ro": "ro", "bg": "bg", "el": "el", "da": "da",
    "sv": "sv", "nb": "no", "fi": "fi", "et": "et", "lv": "lv",
    "lt": "lt", "sl": "sl", "hr": "hr", "uk": "uk", "ru": "ru",
    "tr": "tr", "sr": "sr-Latn",
}
PLACEHOLDER = re.compile(r"\{[^{}]*\}")
ICON = re.compile(r"[\U0001F300-\U0001FAFF\u2600-\u27BF]")
CHUNK_SIZE = 12
WORKERS = 8
CHECKPOINT_DIR = ROOT / "tools" / ".localization-checkpoints"


def load_strings(path: pathlib.Path) -> list[str]:
    obj = json.loads(path.read_text(encoding="utf-8"))
    if isinstance(obj, dict):
        values = obj.get("keys", list(obj))
    elif isinstance(obj, list):
        values = obj
    else:
        raise ValueError(f"Unsupported source format: {path}")
    return [value for value in values if isinstance(value, str)]


def canonical_strings() -> list[str]:
    # source-final.json is the frozen authoritative source. Catalog files can
    # contain intermediate or superseded copies and must not expand the set.
    return list(dict.fromkeys(load_strings(SOURCE)))


def prepare(text: str, batch_id: int) -> tuple[str, dict[str, str]]:
    replacements: dict[str, str] = {}
    text = text.replace("\r\n", "\n")

    def marker() -> str:
        # This ASCII token survives the endpoint. Its letters transliterate
        # reversibly to Serbian Cyrillic, then back to Serbian Latin.
        return f"ZZZPH{batch_id:06d}{len(replacements):02d}ZZZ"

    def mask_placeholder(match: re.Match[str]) -> str:
        token = marker()
        replacements[token] = match.group(0)
        return token

    text = PLACEHOLDER.sub(mask_placeholder, text)

    def mask_icon(match: re.Match[str]) -> str:
        token = marker()
        replacements[token] = match.group(0)
        return token

    text = ICON.sub(mask_icon, text)
    if "\n" in text:
        token = marker()
        replacements[token] = "\n"
        text = text.replace("\n", token)
    return text, replacements


def translate_batch(strings: list[str], target: str, batch_index: int) -> list[str]:
    prepared: list[str] = []
    replacements: dict[str, str] = {}
    for i, value in enumerate(strings):
        result, found = prepare(value, batch_index * CHUNK_SIZE + i)
        prepared.append(result)
        replacements.update(found)
    # Punctuation-and-number separators survive Serbian translation, which
    # otherwise transliterates Latin marker letters into Cyrillic.
    separators = [f"⟦{batch_index:04d}{i:02d}⟧" for i in range(len(strings) - 1)]
    joined = "\n".join(
        part + ("\n" + separators[i] if i < len(separators) else "")
        for i, part in enumerate(prepared)
    )
    params = urllib.parse.urlencode({
        "client": "gtx", "sl": "de", "tl": target, "dt": "t", "q": joined,
    })
    url = "https://translate.googleapis.com/translate_a/single?" + params
    last_error: Exception | None = None
    for attempt in range(5):
        try:
            request = urllib.request.Request(url, headers={"User-Agent": "Mozilla/5.0"})
            with urllib.request.urlopen(request, timeout=30) as response:
                payload = json.loads(response.read().decode("utf-8"))
            translated = "".join(fragment[0] for fragment in payload[0] if fragment and fragment[0])
            break
        except (urllib.error.URLError, TimeoutError, json.JSONDecodeError) as exc:
            last_error = exc
            if attempt == 4:
                raise RuntimeError(f"Google Translate failed for {target}, batch {batch_index}: {exc}") from exc
            time.sleep(1.5 * (attempt + 1))
    else:
        raise RuntimeError(f"Google Translate failed for {target}, batch {batch_index}: {last_error}")

    # Some targets insert whitespace before closing punctuation in a marker.
    # Locate each numbered marker in order while allowing that harmless change.
    pieces: list[str] = []
    cursor = 0
    for i in range(len(separators)):
        pattern = re.compile(rf"⟦\s*{batch_index:04d}{i:02d}\s*⟧")
        match = pattern.search(translated, cursor)
        if not match:
            raise RuntimeError(f"Translation separator changed for {target}, batch {batch_index}, separator {i}")
        pieces.append(translated[cursor:match.start()])
        cursor = match.end()
    pieces.append(translated[cursor:])

    output: list[str] = []
    for source, value in zip(strings, pieces):
        value = value.strip()
        if target == "sr-Latn":
            value = to_serbian_latin(value)
        for token, original in replacements.items():
            value = value.replace(token, original)
        expected = sorted(PLACEHOLDER.findall(source))
        actual = sorted(PLACEHOLDER.findall(value))
        if expected != actual:
            raise RuntimeError(f"Placeholder mismatch for {target}: {source!r} -> {value!r}")
        output.append(value)
    return output


def to_serbian_latin(text: str) -> str:
    """Convert Google's Serbian Cyrillic output to standard Serbian Latin."""
    table = str.maketrans({
        "А": "A", "а": "a", "Б": "B", "б": "b", "В": "V", "в": "v",
        "Г": "G", "г": "g", "Д": "D", "д": "d", "Ђ": "Đ", "ђ": "đ",
        "Е": "E", "е": "e", "Ж": "Ž", "ж": "ž", "З": "Z", "з": "z",
        "И": "I", "и": "i", "Ј": "J", "ј": "j", "К": "K", "к": "k",
        "Л": "L", "л": "l", "Љ": "Lj", "љ": "lj", "М": "M", "м": "m",
        "Н": "N", "н": "n", "Њ": "Nj", "њ": "nj", "О": "O", "о": "o",
        "П": "P", "п": "p", "Р": "R", "р": "r", "С": "S", "с": "s",
        "Т": "T", "т": "t", "Ћ": "Ć", "ћ": "ć", "У": "U", "у": "u",
        "Ф": "F", "ф": "f", "Х": "H", "х": "h", "Ц": "C", "ц": "c",
        "Ч": "Č", "ч": "č", "Џ": "Dž", "џ": "dž", "Ш": "Š", "ш": "š",
    })
    return text.translate(table)


def translate_locale(locale: str, target: str, strings: list[str], existing: dict[str, str]) -> tuple[str, dict[str, str]]:
    missing = [key for key in strings if key not in existing]
    if locale == "de":
        return locale, {key: key for key in missing}
    batches = [missing[i:i + CHUNK_SIZE] for i in range(0, len(missing), CHUNK_SIZE)]
    results: list[list[str] | None] = [None] * len(batches)
    with concurrent.futures.ThreadPoolExecutor(max_workers=WORKERS) as pool:
        futures = {
            pool.submit(translate_batch, batch, target, i): i
            for i, batch in enumerate(batches)
        }
        for future in concurrent.futures.as_completed(futures):
            i = futures[future]
            results[i] = future.result()
    translated: dict[str, str] = {}
    for batch, values in zip(batches, results):
        assert values is not None
        translated.update(zip(batch, values))
    return locale, translated


def main() -> int:
    strings = canonical_strings()
    source_fingerprint = hashlib.sha256("\n".join(strings).encode("utf-8")).hexdigest()
    locales = sorted(LOCALIZATION.glob("*.json"))
    source_set = set(strings)
    original: dict[str, dict[str, str]] = {}
    for path in locales:
        data = json.loads(path.read_text(encoding="utf-8"))
        if not isinstance(data, dict):
            raise ValueError(f"Locale must be a JSON object: {path}")
        original[path.stem] = data

    unknown = set(original) - set(LANGUAGES)
    if unknown:
        raise ValueError(f"No language mapping for locales: {sorted(unknown)}")
    missing_counts = {locale: len(source_set - set(data)) for locale, data in original.items()}
    print(f"Frozen canonical source strings: {len(strings)}; missing per locale: {missing_counts}", flush=True)

    additions: dict[str, dict[str, str]] = {}
    # Translate one locale at a time so request volume is bounded and any failed
    # locale can be reported without writing German placeholders into its file.
    for locale, target in LANGUAGES.items():
        path = LOCALIZATION / f"{locale}.json"
        if not path.exists():
            raise FileNotFoundError(path)
        checkpoint_path = CHECKPOINT_DIR / f"{locale}.json"
        cached: dict[str, str] = {}
        if checkpoint_path.exists():
            checkpoint = json.loads(checkpoint_path.read_text(encoding="utf-8"))
            if checkpoint.get("locale") == locale and checkpoint.get("sourceFingerprint") == source_fingerprint:
                cached = {
                    key: value for key, value in checkpoint.get("translations", {}).items()
                    if key in strings and key not in original[locale] and isinstance(value, str)
                }
        _, freshly_translated = translate_locale(locale, target, strings, {**original[locale], **cached})
        translated = {**cached, **freshly_translated}
        additions[locale] = translated
        CHECKPOINT_DIR.mkdir(parents=True, exist_ok=True)
        checkpoint_path.write_text(
            json.dumps({
                "locale": locale,
                "sourceFingerprint": source_fingerprint,
                "translations": translated,
            }, ensure_ascii=False, indent=2) + "\n",
            encoding="utf-8",
        )
        print(f"{locale}: translated {len(translated)} missing strings", flush=True)

    # Only commit after every requested translation succeeded and passed checks.
    for locale, translated in additions.items():
        data = dict(original[locale])
        data.update(translated)
        ordered = {key: data[key] for key in sorted(data)}
        (LOCALIZATION / f"{locale}.json").write_text(
            json.dumps(ordered, ensure_ascii=False, indent=2) + "\n", encoding="utf-8"
        )
    if CHECKPOINT_DIR.exists():
        for checkpoint in CHECKPOINT_DIR.glob("*.json"):
            checkpoint.unlink()
        CHECKPOINT_DIR.rmdir()
    print("Updated all locale JSON files without replacing existing values.")
    return 0


if __name__ == "__main__":
    try:
        raise SystemExit(main())
    except Exception as exc:
        print(f"ERROR: {exc}", file=sys.stderr)
        raise SystemExit(1)
