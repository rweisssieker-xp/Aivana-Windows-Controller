#!/usr/bin/env python3
"""Fill documentation caches offline using a supplied M2M100 CTranslate2 model.

Install ctranslate2 and sentencepiece in a tooling environment. No downloads are
performed. Model artifacts and caches stay outside the application package.
"""
import argparse
import json
import re
from pathlib import Path

import localize_documentation as docs


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--model", type=Path, required=True)
    parser.add_argument("--locales", nargs="*", choices=list(docs.LOCALES))
    parser.add_argument("--refresh-sources", action="store_true")
    parser.add_argument("--threads", type=int, default=2)
    parser.add_argument("--batch-size", type=int, default=16)
    parser.add_argument("--device", choices=("cpu", "cuda"), default="cpu")
    parser.add_argument("--cuda-libraries", type=Path, help="Optional local cuBLAS bin directory on Windows")
    args = parser.parse_args()
    library_handles = []
    if args.cuda_libraries:
        import ctypes
        for name in ("cublasLt64_12.dll", "cublas64_12.dll"):
            library_handles.append(ctypes.WinDLL(str((args.cuda_libraries / name).resolve(strict=True))))
    if args.device == "cuda" and args.batch_size == 16:
        args.batch_size = 64
    import ctranslate2
    import sentencepiece
    tokenizer = sentencepiece.SentencePieceProcessor(model_file=str(args.model / "spm.128k.model"))
    translator = ctranslate2.Translator(str(args.model), device=args.device, compute_type="float16" if args.device == "cuda" else "int8", intra_threads=args.threads, inter_threads=1 if args.device == "cuda" else 4)
    sources = docs.frozen_sources(args.refresh_sources)
    documents = [docs.Document(path, text) for path, text in sources.items()]
    items = dict.fromkeys((doc.language, value) for doc in documents for trans, value in doc.parts if trans)
    items.update({("en", value): None for value in docs.COPY.values()})
    for locale in args.locales or docs.LOCALES:
        target = docs.LOCALES[locale][0]
        cache_path = docs.WORK / f"{locale}.json"
        cache = json.loads(cache_path.read_text(encoding="utf-8")) if cache_path.exists() else {}
        span_path = docs.WORK / f"{locale}-spans.json"
        spans = json.loads(span_path.read_text(encoding="utf-8")) if span_path.exists() else {}
        recipes, pending = {}, {}
        missing = [(language, value) for language, value in items if language != target and language+"\0"+value not in cache]
        # Numeric placeholders often survive full-sentence translation. Accept
        # that output only if every placeholder occurs exactly once; otherwise
        # use the deterministic protected-span fallback below.
        print(f"{locale}: {len(missing)} complete prose segments to translate", flush=True)
        masked, sentence_results, sentence_counts = [], {}, {}
        for language, value in missing:
            protected = []
            def mask(match):
                protected.append(match.group(0))
                return f"[{900000+len(protected)-1}]"
            text = docs.PROTECTED.sub(mask, value)
            sentences = re.split(r"(?<=[.!?])\s+(?=[^\W\d_])", text)
            sentence_counts[(language, value)] = len(sentences)
            for index, sentence in enumerate(sentences):
                masked.append((language, value, sentence, protected, index))
        masked.sort(key=lambda row: (row[0], len(row[2])))
        from concurrent.futures import ThreadPoolExecutor
        def translate_full(batch):
            encoded = [[f"__{lang}__"]+tokenizer.encode(text, out_type=str)+["</s>"] for lang, _, text, _, _ in batch]
            if any(len(tokens) > 1024 for tokens in encoded):
                raise ValueError("Source exceeds model context; split the paragraph")
            limit = min(1024, max(map(len, encoded))*3+32)
            futures = translator.translate_batch(encoded, target_prefix=[[f"__{target}__"] for _ in batch], beam_size=2, repetition_penalty=1.05, no_repeat_ngram_size=4, max_input_length=1024, max_decoding_length=limit, max_batch_size=8192, batch_type="tokens", asynchronous=True)
            results = [future.result() for future in futures]
            return [result if len(result.hypotheses[0]) < limit else None for result in results]
        group_size = 128 if args.device == "cuda" else 32
        groups = [masked[start:start+group_size] for start in range(0,len(masked),group_size)]
        with ThreadPoolExecutor(max_workers=4) as pool:
            jobs = [pool.submit(translate_full, batch) for batch in groups]
            for group_index, (batch, job) in enumerate(zip(groups, jobs)):
                for (language, value, sentence, protected, index), result in zip(batch, job.result()):
                    if result is None: continue
                    tokens = result.hypotheses[0][1:]
                    output = tokenizer.decode(tokens).strip()
                    found = re.findall(r"\[\s*(9\d{5})\s*\]", output)
                    wanted = re.findall(r"\[(9\d{5})\]", sentence)
                    if sorted(found) != sorted(wanted) or not output or "\ufffd" in output or len(tokens) >= 1023:
                        continue
                    output = re.sub(r"\[\s*(9\d{5})\s*\]", lambda match: protected[int(match.group(1))-900000], output)
                    if re.search(r"(?<!\d)9\d{5}(?!\d)", output): continue
                    sentence_results.setdefault((language,value), {})[index] = docs.to_serbian_latin(output) if target == "sr" else output
                for (language,value), results in sentence_results.items():
                    if len(results) == sentence_counts[(language,value)]:
                        cache[language+"\0"+value] = " ".join(results[index] for index in range(len(results)))
                cache_path.write_text(json.dumps(cache, ensure_ascii=False), encoding="utf-8")
                if group_index % 10 == 0:
                    print(f"{locale}: {min((group_index+1)*group_size,len(masked))}/{len(masked)} full segments", flush=True)
        for language, value in items:
            key = language + "\0" + value
            if language == target:
                cache[key] = value
            if key in cache:
                continue
            pieces, cursor = [], 0
            # Keep executable syntax and identities local. Translate prose spans
            # independently instead of trusting a model to copy placeholders.
            for match in list(docs.PROTECTED.finditer(value)) + [None]:
                end = match.start() if match else len(value)
                span = value[cursor:end]
                boundaries = re.match(r"^([^\w]*)(.*?)([^\w]*)$", span, re.S)
                leading, core, trailing = boundaries.groups()
                if re.search(r"[^\W\d_]", core, re.UNICODE):
                    span_key = language + "\0" + core
                    pieces.append((leading, span_key, trailing))
                    if span_key not in spans:
                        pending[span_key] = (language, core)
                else:
                    pieces.append(span)
                if match:
                    pieces.append(match.group(0))
                    cursor = match.end()
            recipes[key] = pieces
        pending = sorted(pending.items(), key=lambda item: (item[1][0], len(item[1][1])))
        print(f"{locale}: {len(recipes)} segments, {len(pending)} unique prose spans", flush=True)
        from collections import deque
        queue = deque()
        for start in range(0, len(pending), args.batch_size):
            batch = pending[start:start+args.batch_size]
            encoded = [[f"__{lang}__"] + tokenizer.encode(text, out_type=str) + ["</s>"] for _, (lang, text) in batch]
            if any(len(tokens) > 1024 for tokens in encoded):
                raise ValueError("Prose exceeds model input limit; split the source paragraph explicitly")
            results = translator.translate_batch(encoded, target_prefix=[[f"__{target}__"] for _ in batch], beam_size=2, repetition_penalty=1.05, no_repeat_ngram_size=4, max_input_length=1024, max_decoding_length=1024, asynchronous=True)
            queue.append((start, batch, results))
            if len(queue) < 4 and start + args.batch_size < len(pending):
                continue
            start, batch, results = queue.popleft()
            results = [result.result() for result in results]
            for (key, _), result in zip(batch, results):
                tokens = result.hypotheses[0]
                if tokens and tokens[0] == f"__{target}__":
                    tokens = tokens[1:]
                output = tokenizer.decode(tokens).strip()
                if not output or "\ufffd" in output or len(tokens) >= 1024:
                    raise ValueError(f"Invalid or truncated translation for {key!r}")
                spans[key] = docs.to_serbian_latin(output) if target == "sr" else output
            span_path.write_text(json.dumps(spans, ensure_ascii=False), encoding="utf-8")
            if start % (args.batch_size * 20) == 0:
                print(f"{locale}: {min(start+len(batch),len(pending))}/{len(pending)} spans", flush=True)
        while queue:
            _, batch, results = queue.popleft()
            for (key, _), future in zip(batch, results):
                tokens = future.result().hypotheses[0][1:]
                output = tokenizer.decode(tokens).strip()
                if not output or "\ufffd" in output or len(tokens) >= 1023:
                    raise ValueError(f"Invalid or truncated translation for {key!r}")
                spans[key] = docs.to_serbian_latin(output) if target == "sr" else output
            span_path.write_text(json.dumps(spans, ensure_ascii=False), encoding="utf-8")
        for key, pieces in recipes.items():
            cache[key] = "".join(piece if isinstance(piece, str) else piece[0]+spans[piece[1]]+piece[2] for piece in pieces)
        cache_path.write_text(json.dumps(cache, ensure_ascii=False), encoding="utf-8")
        print(f"{locale}: cache complete", flush=True)


if __name__ == "__main__":
    main()
