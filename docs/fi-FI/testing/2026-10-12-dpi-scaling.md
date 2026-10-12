<!-- generated-doc locale=fi-FI source=docs/testing/2026-10-12-dpi-scaling.md sha256=dcf6d4c1a21eec9be9f07a38cab0aff308587c78b85fb2b24c69f04d28d82864 -->

> Koneavusteinen käännös. Rakenne, koodiesimerkit ja linkit tarkistetaan; tämä ei tarkoita äidinkielisen tarkistajan arviota tai oikeudellista hyväksyntää. Historialliset raportit säilyttävät alkuperäiset päivämääränsä ja rajauksensa. [Alkuperäinen lähde](../../testing/2026-10-12-dpi-scaling.md)

# DPI-testi

MAKRIVO ilmoitettu PerMonitorV2 sisäänrakennetussa sovellusmanifestissä. WPF-testi tutkii GetWindowDpiAwarenessContextin ja AreDpiAwaressContextsEqualin avulla pääikkunan tosiasiallisesti tehokasta kontekstia.

Lisäluokitus / Raster-testiä simuloida 150 ja 200 prosenttiyksikköä yksi 1920 × 1080 Näytön tarjonta: logiikka ikkunat 1280 × 693 tai suhteessa 960 × 520 Kaikki viisi sivua ovat mukana 144 tai suhteessa 192 DPI vähennetään PNG. tiedostot kuumalla skala-simulated-150-* tai scale-simulated-200-*.

Nämä testit eivät muuta Windows asetuksia. He eivät vahvista todellista WM_DPICHANGED -näyttömuutosta, ei sekoitettua näyttökonfiguratiota eikä todellista terävyyttä toisessa näytössä. Fyysiset skalaatiot pysyvät auki.

Se on WinForms-Analyyttinen varoitus WFO0003 Se on yksinomaan tämän WPF-Aloitushanke tukahduttanut: WinForms Käytetään infopalveluiden yhdentämiseen, WPF ohjaa käynnistys; DPI määritetään Manifestin kautta ennen ikkunan luomista.

Microsoft dokumentti: https://learn.microsoft.com/en-us/windows/win32/hidpi/setting-the-default-dpi-awareness-for-a-process

Näytön tarkastelu: Pöytä vaihtaa 650 DIP: n ikkunan korkeuden alla, jotta kartat ja muokkaajat eivät liity liian pienelle keskipisteelle.

Testitilanne: Release-rakennus ilman virheitä/varoituksia; täydellinen WPF Smooke-testi mukaan lukien todellinen PerMonitorV2-Fensterkontext ja simuloitu 150/200-Prozent-Renderings. 200-Prozent-Dashboardaufnahme tarkastettiin visuaalisesti korkeuden mukautuksen jälkeen. Git diff - tarkistaa
