<!-- generated-doc locale=sl-SI source=docs/MAINTENANCE.md sha256=9bf5875b50386e698c0574eee390f790d4b1c29cb11288c856c98e03a05877f7 -->

> Strojno podprt prevod. Struktura, primeri kode in povezave se preverjajo; to ne pomeni pregleda maternega govorca ali pravne odobritve. Zgodovinska poročila ohranijo izvirne datume in obseg. [Izvirni vir](../MAINTENANCE.md)

# Udrževanje vseh jezikov dokumentacije

Dokumentacija zajema vseh 27 jezikov vmesnika. Ustvarjene mape uporabljajo regionalne kode; norveščina ustreza `nb-NO`, srbščina v latinici pa `sr-Latn`. Vsaka mapa vsebuje kazalo, priročnik, podrobno referenco dejanj, pregled funkcij, zapiske o preverjanju, raziskave z datoteko CSV, zgodovinske načrte in analizo licenc.

## Vir resnice

Urejajte korenski `README.md`, `docs/HANDBOOK.md`, `docs/FEATURES.md`, `docs/VERIFICATION.md`, to stran ali izvirne datoteke v `docs/research`, `docs/testing`, `docs/superpowers` in `docs/licensing`. Zgodovinskega poročila ne spreminjajte tako, da bi nakazovalo preverjanje poznejše različice. Razlikujte implementacijo, simulacijo, sprejemno preverjanje pravih naprav in komercialno odobritev.

## Ustvarjanje in validacija

```powershell
python tools/localize_documentation.py --refresh-sources
python tools/check_documentation.py
```

Izbirni spletni generator pošilja besedilo dokumentacije na Googlov javni prevajalski vmesnik v skladu z obstoječimi orodji za lokalizacijo. Ne bere uporabniške konfiguracije, skrivnosti ali datotek modelov. Uporabljajte ga samo za dokumentacijo, primerno za to storitev. Bloki kode, koda v vrstici, URL-ji, struktura tabel in tehnični identifikatorji so zaščiteni. Predpomnilniki prevodov in zamrznjeno stanje virov se shranjujejo v prezrti `artifacts/documentation-translation`.

Ko spletni ponudnik omeji hitrost zahtev, `tools/translate_documentation_local.py` napolni isti predpomnilnik z izrecno zagotovljenim lokalnim modelom M2M100 CTranslate2. Stanje z dne 2026-10-12 uporablja model facebook/m2m100_418M z licenco MIT, pretvorjen kot JustFrederik/m2m_100_418m_ct2_int8 pri reviziji `1aeed44db4dd61a486bba44acf54c76507082a2c`. CTranslate2 4.8.2 in `SentencePiece` 0.2.2 sta orodji samo za gradnjo. Z MAKRIVO se ne dobavljajo uteži modelov ali prevajalske odvisnosti. Po polnjenju predpomnilnika zaženite generator z `--cache-only`, da ustvari strani brez omrežnih zahtev. Lokalni pomočnik ne prenaša ali izvaja oddaljene kode modela.

```powershell
python -m pip install ctranslate2==4.8.2 sentencepiece==0.2.2
python tools/translate_documentation_local.py --model C:\models\m2m100-ct2 --refresh-sources
python tools/localize_documentation.py --cache-only
python tools/check_documentation.py
```

Brez `--refresh-sources` izvajanje nadaljuje svoje zamrznjeno stanje virov. Po urejanju izvirnikov uporabite možnost osveževanja. Predpomnjeni segmenti se ponovno uporabijo. Delno izvajanje lahko izbere regionalne kode z `--locales`; pred objavo dokončajte vse jezike. Prekinjena izvajanja neuspešnega prevoda ne nadomestijo tiho z neprevedenim besedilom.

Manifest beleži vsak izvorni in ustvarjeni dokument s prstnimi odtisi SHA-256 normaliziranega besedila UTF-8: konci vrstic LF in brez oznake vrstnega reda bajtov. Preverjalnik brez povezave preveri popolno pokritost vseh jezikov, prstne odtise, lokalne povezave, primere kode, strukturo tabel/CSV, izvorne URL-je in nenadomeščena nadomestna polja. Zavrne zastarele vire. Uspešno strukturno preverjanje ni pregled maternega govorca ali pravna odobritev. Ustvarjene strani to omejitev vidno navajajo.

## Objava in paketi

Dokumentacijo, generator in preverjalnik objavite skupaj. Ohranite izvirne licenčne pogoje tretjih oseb; prevedena analiza licenc je informativna. Rezultati gradnje, predpomnilnik prevodov in lokalne datoteke za orkestracijo ne sodijo v Git. Izdaja izvorne dokumentacije sama po sebi ne objavi namestitvenega programa Windows.

Prevodi uporabniškega vmesnika aplikacije so ločeni katalogi JSON v `src/Aivana.Core/Localization`. Ustvarjanje dokumentacije teh katalogov ne spreminja. Gradnja aplikacije in njena dokumentacija naj se nanašata na isto različico stanja; novo opisana eksperimentalna podpora izvajalnega okolja je lahko novejša od nameščenega paketa.
