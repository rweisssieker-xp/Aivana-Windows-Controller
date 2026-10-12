<!-- generated-doc locale=sr-Latn source=docs/research/obs-integration.md sha256=a4e6ca4368b28daa73ffbdda4fb817863e3fa15d52da00d26bbd32ce1f05f9b7 -->

> Mašinski potpomognut prevod. Struktura, primeri koda i veze se proveravaju; to ne znači pregled izvornog govornika ili pravno odobrenje. Istorijski izveštaji zadržavaju izvorne datume i obim. [Originalni izvor](../../research/obs-integration.md)

# OBS Integracija studija

[English (US)](obs-integration.md) | [Deutsch](obs-integration.md) | [Français](obs-integration.md) | [Italiano](obs-integration.md)

Aivana koristi protokol OBS Studio obs-websocket v5 preko ClientWebSocket; on ne instalira biblioteku klijenta treće strane OBS. Protokol se otvara sa serverom Helo (opkod 0), odgovori sa Identifi (opkode 1), čeka Identifikovani (opkoda 2), šalje bele zapit (opkodi 6), a zatim odgovara njenom Reakciji (opkodu 7) po zahtevu ID. Autentifikacija lozinke prati OBS dokumentisano dvostepeno izračunavanje SHA-256 izazova.

Omogućiti **Instrumenti → WebSocket Server podešavanja** u OBS. Defolt krajnjeg tačka je **US://127.0.0.1:4455**. Postavite akciju **OBS-WebSocket-Adresa** Polje za još jednu krajnju tačku. udaljena krajnja tačka mora koristiti wss://; nešifrovane ws:// prihvata se samo za obratnu adresu.. Aivana Odbijaju ugrađene URL krediti.

Kada je OBS ima autentifikaciju, sačuvati svoj lozinku iz OBS Aktivni urednik. Aivana Skladi ga u trenutnom Windows korisnički poverenja menadžer kao generalno imenovan **Aivana.OBS:<Normalizovani kraj>**. Nikada nije napisana na datoteku akcije ili podešavanja., **Aivana_OBS_Pasvord** ostaje dostupna kao padanje kada krajnja tačka nema spaseno lozinku. OBS može ostati neidentifikovana, u kojem slučaju nije potrebno. Izbegavajte argumente komandne linije za lozinku jer se mogu proveriti komandne line.

Kontroler katalog i konfiguracija podrška **Startrekord**, **StopRekord**, **ToggleRecord**, **Pauzarekor**, **Rezolucija**, **Startstream**, **Stopstream**, **ToggleStream**, **Savremeni program**, i **ToggleInputMute**. Prvih osam ne uzima argumente. SetCurrentProgramScene koristi ime scene uređaja; ToggleInputMute koristi svoje ulazno ime..

Svaka akcija dobija svežu vezu sa 10 sekundi i bez automatskog povlačenja. Ovo je važno za ukidanje naloga: nakon što je zahtev poslat, vremenski izlaz ili prekinuta veza može ostaviti rezultat nepoznat, a povlačenje može ponovo ukinuti stanje. OBS ukidanja zahteva se vraćaju sa njihovim kod statusa i (kada je obezbeđen) komentar servera.

Protokol reference:

- [OBS WebSocket Protokol specifikacija](https://github.com/obsproject/obs-websocket/blob/master/docs/generated/protocol.md)
- [OBS WebSocket autentifikacione formule i poruke opkode](https://github.com/obsproject/obs-websocket/blob/master/docs/generated/protocol.md#creating-an-authentication-string)
- [OBS WebSocket medijski zahtev za uvođenje i prihvaćene vrednosti](https://github.com/obsproject/obs-websocket/blob/master/src/requesthandler/RequestHandler_MediaInputs.cpp)

Protokol beleške i imena operacija su provereni protiv OBS projektne dokumentacije na 2026-10-08.
