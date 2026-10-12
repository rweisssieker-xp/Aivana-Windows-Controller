<!-- generated-doc locale=hr-HR source=docs/research/obs-integration.md sha256=a4e6ca4368b28daa73ffbdda4fb817863e3fa15d52da00d26bbd32ce1f05f9b7 -->

> Strojno potpomognut prijevod. Struktura, primjeri koda i poveznice provjeravaju se; to ne znači pregled izvornog govornika ili pravno odobrenje. Povijesna izvješća zadržavaju izvorne datume i opseg. [Originalni izvor](../../research/obs-integration.md)

# OBS Studijska integracija

[English (US)](obs-integration.md) | [Deutsch](obs-integration.md) | [Français](obs-integration.md) | [Italiano](obs-integration.md)

Aivana Koristi se OBS Studio obs-websocket v5 protokola preko ClientWebSocket; ne instalira treće strane OBS Knjižnica klijenata. protokol se otvara s serverom Hello (opcode) 0), Odgovori s Identify (opcode) 1), Slijedeći članakOtkriven (opcode) 2), Pošaljite zahtjev (opcode) 6), Odgovori na zahtjev (opcode) 7) Na zahtjev ID. lozinka za autentifikaciju slijedi OBS'Dvostruka dokumentacija SHA-256 Izazov izračuna.

omogućuje **Uređaji → WebSocket Server postavke** u OBS. Defektna točka je **WS://127.0.0.1:4455**. Sastavite akciju **OBS-WebSocket-Naslovnica** Polje za još jednu krajnju točku. udaljena krajnja točka mora koristiti wss://; nešifrirani ws:// prihvaća se samo za adresu za povrat. krajnja toka također može uključivati put ili prilagođeni port. Aivana Odbijanje ugrađenih URL-ova.

Kada OBS ima autentifikaciju, sačuvajte svoju lozinku iz OBS Redatelj djelovanja. Aivana Skladište u sadašnjem Windows Upravitelj povjerenja korisnika kao generički povjerenje **Aivana.OBS:<Normalizirani kraj>**. Nikada se ne piše na akcijskom ili postavljenom datoteku., **AIVANA_OBS_PASVORD** ostaje dostupan kao pad kada krajnja točka nema sačuvano lozinko. OBS može ostati neovjerena, u kojem slučaju nije potrebno. izbjegavajte argumente zapovijedi za lozinku jer se mogu provjeriti zapovijedačke linije.

Kontrolor katalog i konfiguracija podrška **StartRekord**, **StopRekord**, **ToggleRekord**, **Pauzijska**, **Recenzija**, **Startstream**, **Stopstream**, **Togglestream**, **Trenutačno programiranje**, i **ToggleUredi**. Prvi osam ne uzima nikakve argumente. SetCurrentProgramScene koristi ime scene urednika; ToggleInputMute koristi ime ulaza..

Svaka akcija dobiva svježu vezu s 10 sekundi i bez automatskog povlačenja. To je važno za toggle zapovijedi: nakon što se zahtjev šalje, timeout ili propusti povezanost može ostaviti rezultat nepoznat, a povlačenje bi moglo toggle stanje ponovno. OBS zahtjeva neuspjeh se vraća s njihovim status kod i (kada se pruža) server komentar.

Referencije za protokol:

- [OBS WebSocket specifikacija protokola v5](https://github.com/obsproject/obs-websocket/blob/master/docs/generated/protocol.md)
- [OBS WebSocket formula za autentifikaciju i opkoda poruke](https://github.com/obsproject/obs-websocket/blob/master/docs/generated/protocol.md#creating-an-authentication-string)
- [OBS WebSocket provedba zahtjeva za ulazak medija i prihvaćene vrijednosti](https://github.com/obsproject/obs-websocket/blob/master/src/requesthandler/RequestHandler_MediaInputs.cpp)

Protokoli i operativni nazivi provjereni su protiv dokumentacije projekta OBS na 2026-10-08.
