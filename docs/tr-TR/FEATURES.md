<!-- generated-doc locale=tr-TR source=docs/FEATURES.md sha256=ea712f39457ce69f772f09a1294201705a5f36a92a8bf15f356bd80132c3e723 -->

> Makine destekli çeviri. Yapı, kod örnekleri ve bağlantılar kontrol edilir; bu, ana dili konuşan birinin incelemesi veya hukuki onay anlamına gelmez. Geçmiş raporlar özgün tarihlerini ve kapsamlarını korur. [Orijinal Kaynak](../FEATURES.md)

# MAKRIVO - Özellikleri incelemek

Üretici: **Aivana**. Standı: **12. Ekim 2026**. Bu gelişme durumu, satış veya donanım serbest bırakma değil.

## Windows Yönetimi

- Kabileler, küresel hotkeys, komut paleti, favori ve uygulama bağlı profilleri.
- Birden fazla adım, tekrarlama, duraklama ve öngörme ile makro.
- Programları, dosyaları ve web sitelerini açın; metin, aralığı ve kısaltma kullanın.
- Pencereleri düzenleme, Windows oturumunu kilitleme, ses çıkışlarını ve mikrofonları kontrol etme ve desteklenen arayüzlerle ekran parlaklığını değiştirme.
- OBS-WebSocket-v5-Anbindung sahneler, kayıt, akış ve ses için.
- Şablonlar, çalışma alanı asistanı, cihaz tanıma ve klavye aşırı.
- Profil yapılandırmalarının ithalatı ve ihraç edilmesi; Tasarım kontrolü ve çatışma tespiti.
- Yerel olarak kaydedilen ayarlar; günlük kullanım için bir hesap gerektirmez.

## Güncel Kaynak Kodu'nda Gelişmiş Özellikler

| Bölge | fonksiyon kapsamı | Sınırları |
| --- | --- | --- |
| Sonuç incelemesi | Ön inceleme, inceleme ve inceleme | Sadece doğrulanabilir etkileri doğrulamak; başarılı bir düğme basıncı hedef etkisi kanıtlamaz. |
| Çalışma Asistanı | Seçilen programlardan, dosyaların, URL'lerin ve diğer eylemlerden profilleri ve işlemleri hazırlamak | Önizleme ve depolama gereklidir; kurulum hiçbir şey yapmaz. |
| Makro ve Workflows | Kayıt, koşullar, hareketler ve yerel profil senkronizasyonu | Davranış ve sınırlar uygulama ve test raporlarında belgelendirilmiştir. |
| UI otomatik | Hedefleri seçin, seçmenleri inceleyin, eylemleri ve sınırlı tıklama akışlarını girin | Ayrı çalışan işlem; belirsiz, devre dışı ve parola alanı hedefleri reddedilir; genel metin girişi kaydı yoktur. |
| Genişletmeler | Paketlerin incelenmesi, ithal edilmesi, açıkça güvenilir hale getirilmesi, ihraç edilmesi ve geri dönüştürülmesi | Süreç yalıtımı bir kum kutusu değildir; bir kamu eklenti pazarı değildir. |
| Mobil Panel | Yerel HTTPS paneli, bağlantı, seçilmiş profiller ve durum/sınav geçmişi | Gerçek telefon/tablet ile LAN kabul testleri henüz tamamlanmamıştır; evrensel bir sensör panosu değildir. |
| Üretici Adaptörü | Corsair, Logitech-G-SDK, Razer/SteelSeries-RGB ve Wooting-Analogadapter | Model / SDK bağımlı; yeni adaptörler gerçek donanım ile tam olarak doğrulanmamıştır. Yeni özel SDK'lar birleştirilmemiştir. |
| Fiziksel Düzenleme | Standart düğmeleri cihaz ve tarama kodu ile tespit etmek | Raw Input normal Windows girişini bastırmaz. |
| Yerel AI danışman yardımcısı | Opsiyonel Prototype Ollama, LM Studio, llama.cpp ve ONNX Runtime Yorumları GenAI CPU, açıkça seçilmiş kaynaklardan sınırlı tasarımlar üretir | Varsayılan olarak kapalıdır; yerel CPU çalışma zamanı rastgele modelle test edilmiştir, ancak planlama kalitesi veya zaman tasarrufu kanıtlanmamıştır; model ağırlıkları dahil değildir. |

## Satışa hazır olarak henüz teklif etmeyin

- **Özel kullanımlı ikinci klavye/makro tuş takımı:** İmzalı, lisanslı ve donanımda test edilmiş filtre sürücüsü eksiktir. Normal tuş girişleri Windows’a ulaşmaya devam edebilir.
- Tüm üretici bağdaştırıcıları için tam donanım kabulü ve gerçek mobil cihaz LAN testleri.
- Gerçek karışık monitör/DPI değişiklikleri ve tüm yeni çevirilerin profesyonel çekimi. PerMonitorV2 ve simüle edilmiş 150/200-Prozent-Layouts kontrol edildi; gerçek ekran testleri ayrı kalır. Yüzey 27 dil sunar; ayrıntılı yeni metinler henüz tam olarak dilde alınmamıştır.
- İmzalı ticari kurulum programı, nihai EULA, doğrulanmış SDK dağıtım hakları ve üretimde satın alma lisansı/ödeme entegrasyonu.

## Detaylar ve kanıtlar

- [Kullanım Belgeleri](README.md)
- [Genişletmeler: özellikler, testler ve açık sınırlar](testing/2026-10-12-six-extensions.md)
- [Yerel AI Asistanı: Prototype ve Test Durumu](testing/2026-10-12-local-meeting-assistant.md)
- [Yerel IŞ zamanları ve model gereksinimleri](testing/2026-10-12-local-ai-runtimes.md)
- [DPI ve ölçekleme testleri](testing/2026-10-12-dpi-scaling.md)
- [lisans incelemesi](licensing/2026-10-12-commercial-license-audit.md) – Tarihi kayıt; Yeni Publish Script Runtime İpuçlarını tamamlar. Son paket ayrı olarak incelenecektir.
- [Satış Platformları](licensing/makrivo-vertriebsplattformen-2026-10-12.html) – Karşılaştırma durumu; Stripe ayrıca bir ödeme platformu, mevcut sağlayıcı koşullarına bakın.

## Tam Kalkınma Paketi

```powershell
./tools/publish-complete.ps1 -SelfContained
```

Paket masaüstü uygulaması, UI otomatik işçi, mobil ev sahipliği, tarayıcı dosyaları, örnek eklenti ve çalıştırma zaman lisans bildirimi içerir. Yapı gerçekleri Git'te incelenmiyor. Bir GitHub kaynak kodu henüz yayınlanmamış Windows yüklü değildir.
