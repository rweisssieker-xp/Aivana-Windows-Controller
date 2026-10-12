<!-- generated-doc locale=sl-SI source=docs/MAINTENANCE.md sha256=9bf5875b50386e698c0574eee390f790d4b1c29cb11288c856c98e03a05877f7 -->

> Strojno podprt prevod. Struktura, primeri kode in povezave se preverjajo; to ne pomeni pregleda maternega govorca ali pravne odobritve. Zgodovinska poročila ohranijo izvirne datume in obseg. [Izvirni vir](../MAINTENANCE.md)

# Udrževanje vseh jezikov dokumentacije

Dokumentarno poročilo spremlja vse 27 UI jeziki. ustvarjene mape uporabljajo regionalne kode; norveške zemljevidov za `nb-NO` in srbski latinski `sr-Latn`. Vsak mape vsebuje indeks, ročaj, podrobno referenco za ukrepe, inventuro funkcij, preverjanje zapiskov, vključno s CSV, zgodovinskimi načrti in analizo licenc.

## Vir resnice

Urejanje korena `README.md`, `docs/HANDBOOK.md`, `docs/FEATURES.md`, `docs/VERIFICATION.md`, to stran ali izvirne datoteke pod `docs/research`, `docs/testing`, `docs/superpowers` in `docs/licensing`. Ne ponovite zgodovinskega poročila, ki pomeni, da potrjuje kasnejšo različico. ohranite izvedbo, simulacijo, sprejemanje resničnih naprav in komercialno odobritev ločeno.

## Ustvarjanje in validacija

```powershell
python tools/localize_documentation.py --refresh-sources
python tools/check_documentation.py
```

Opcionalni spletni generator pošilja dokumentacijo na Googlov končni kraj prevajanja, ki sledi obstoječemu orodju za lokacijo. Ne bere konfiguracije uporabnika, skrivnosti ali model datoteke. Uporablja se le na dokumentaciji, ki je primerna za to storitev. Bloki kode, kodo v vrstici, URL-je, strukturo tabele in tehnične identifikacije so zaščitene. Prevajalske šifre in zamrznjeni snop so shranjeni pod ignoriranim `artifacts/documentation-translation`.

Ko je ponudnik na spletu omejen, `tools/translate_documentation_local.py` izpolnjuje isto cache z izrecno zagotovljeno lokalno M2M100 CTranslate2 Model je 2026-10-12 Snapshot uporablja MIT licenco facebook/m2m100_418M Model, pretvorjen kot JustFrederik/m2m_100_418m_ct2_int8 V reviziji `1aeed44db4dd61a486bba44acf54c76507082a2c`. CTranslate2 4.8.2 in `SentencePiece` 0.2.2 so zgolj orodja. nobenih modelnih teže ali prevajanja odvisnosti se pošlje z MAKRIVO. Uporabite generator z `--cache-only` po izpolnitvi cache za oddajo brez omrežnih zahtev. lokalni pomočnik ne prenese ali izvaja kodo oddaljenega modela.

```powershell
python -m pip install ctranslate2==4.8.2 sentencepiece==0.2.2
python tools/translate_documentation_local.py --model C:\models\m2m100-ct2 --refresh-sources
python tools/localize_documentation.py --cache-only
python tools/check_documentation.py
```

Brez `--refresh-sources`, se vrti zamrznjen snapshot. Uporabite možnost osvoboditve po urejanju originalov. Slednje segmente se ponovno uporabljajo. Delno izvedbo lahko izberete lokalnih kod z uporabo `--locales`; končati vse lokalne pred objavo. Prekinjeni tečaji tiho ne nadomeščajo nepreklicane proze za neuspešno prevajanje.

Manifesto snemanje vsakega vira in ustvarjene datoteke z SHA-256 prstnim odtisom normaliziranega UTF-8 besedila: LF linije končanja in brez bajtno oznako. Offline preverja popolno jezikovno pokritost, prstne odtise, lokalne povezave, kodne primere, tabelo/CSV strukturo, vir URL in nepovratne lokacije. Zavrača steklene vire. Prehodni strukturni pregled ni pregleda domačega govornika ali pravna odobritev. Ustvarjene strani imajo to kvalifikacijo vidno.

## Objava in paketi

Objavite dokumentacijo, generator in preverjalec skupaj. Upoštevajte izvirne pogoje licence tretje osebe; prevedeno analizo licence je informativno. Izdelava artefakta, prevajanje cache in lokalne orkestra datoteke ne spadajo v Git. Izdaja izvora dokumentacije sama po sebi ne objavi Windows namestnika.

Uporabniški prevodi aplikacij so ločeni JSON katalogi pod `src/Aivana.Core/Localization`. Dokumentacija generacije ne spremeni teh katalogov. Ustvarjanje aplikacije in njena dokumentacija bi morala deliti različico snopa; novo opisana podpora za izvedbo časa lahko predvideva nameščen paket.
