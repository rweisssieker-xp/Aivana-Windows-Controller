<!-- generated-doc locale=tr-TR source=docs/research/obs-integration.md sha256=a4e6ca4368b28daa73ffbdda4fb817863e3fa15d52da00d26bbd32ce1f05f9b7 -->

> Makine destekli çeviri. Yapı, kod örnekleri ve bağlantılar kontrol edilir; bu, ana dili konuşan birinin incelemesi veya hukuki onay anlamına gelmez. Geçmiş raporlar özgün tarihlerini ve kapsamlarını korur. [Orijinal Kaynak](../../research/obs-integration.md)

# OBS İletişim Bilgileri

[English (US)](obs-integration.md) | [Deutsch](obs-integration.md) | [Français](obs-integration.md) | [Italiano](obs-integration.md)

Aivana Kullanmak için OBS ClientWebSocket üzerinden obs-websocket v5 protokolü; üçüncü bir taraf yüklemiyor OBS Müşteri Kütüphanesi. protokol Hello ile açılır (opcode 0), Etiket Arşivi: Opcode 1), Onaylanmış için bekleyin (opcode 2), Beyazlatılmış bir başvuru gönderir (opcode 6), Bu sorunun cevabı (opcode) 7) İsteğe bağlı olarak ID. şifre doğrulama OBS'Doküman 2 Adım SHA-256 Challenge Hesaplama.

Yetenek **Araçlar → WebSocket Server ayarları** içinde OBS. Default Endpoint’e göre **WS://127.0.0.1:4455**. eylemi ayarlayın **OBS-WebSocket-Adresi** Bir uzaktan son nokta wss:// kullanmak zorundadır; şifrelenmemiş ws:// sadece bir loopback adresi için kabul edilir.. Aivana Yerleşik URL'leri reddediyor.

Ne zaman OBS kimlik doğrulaması etkinleştirilmiştir, şifresini kaydeder OBS eylem editörü. Aivana Şimdiki bölümde yerleştirin Windows Kullanıcının Credential Manager adı **Aivana.OBS:<Normalleştirilmiş Endpoint>**. Hiçbir zaman bir eylem veya ayar dosyasına yazılmaz., **AIVANA_OBS_PASSWORD İÇİN** Son nokta kaydedilmediğinde bir düşüş olarak kullanılabilir kalır. OBS Belirsiz kalabilir, bu durumda gerekli değildir. şifre için komut çizgisi argümanları önlemek çünkü işlem komut çizgileri kontrol edilebilir.

Kontrol kataloğu ve konfigürasyon desteği **Başlangıç**, **Durdurma**, **ToggleRekord**, **PauseRekord**, **ResumeDevamı**, **Başlangıç**, **StopStream için**, **ToggleStream için**, **Gelişmiş Program**, ve **Toggleİnternet**. İlk sekiz hiçbir argüman almaz. SetCurrentProgramScene editörün sahne adını kullanır; ToggleInputMute giriş adını kullanır..

Her eylem, 10 saniye sınırı ile yeni bir bağlantı elde eder ve otomatik geri çekilme yoktur. Bu, talep komutları için geçerlidir: talep gönderildikten sonra, bir zaman kaybı veya kaybolan bağlantı sonucu bilinmeyen bırakabilir ve geri çekilme durumunu yeniden kaybeder. OBS talep başarısızlıkları durum kodları ve (desteklendiğinde) sunucunun yorumları ile iade edilir.

Protokol referansları :

- [OBS WebSocket v5 protokol özellikleri](https://github.com/obsproject/obs-websocket/blob/master/docs/generated/protocol.md)
- [OBS WebSocket doğrulama formülü ve mesaj kodları](https://github.com/obsproject/obs-websocket/blob/master/docs/generated/protocol.md#creating-an-authentication-string)
- [OBS WebSocket medya giriş talebinin uygulanması ve kabul edilen eylem enum değerleri](https://github.com/obsproject/obs-websocket/blob/master/src/requesthandler/RequestHandler_MediaInputs.cpp)

Protokol notları ve operasyon isimleri kontrol edildi OBS Proje Belgeleri 2026-10-08.
