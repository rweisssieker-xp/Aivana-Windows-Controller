<!-- generated-doc locale=tr-TR source=docs/testing/2026-10-12-dpi-scaling.md sha256=dcf6d4c1a21eec9be9f07a38cab0aff308587c78b85fb2b24c69f04d28d82864 -->

> Makine destekli çeviri. Yapı, kod örnekleri ve bağlantılar kontrol edilir; bu, ana dili konuşan birinin incelemesi veya hukuki onay anlamına gelmez. Geçmiş raporlar özgün tarihlerini ve kapsamlarını korur. [Orijinal Kaynak](../../testing/2026-10-12-dpi-scaling.md)

# DPI Sınavı

MAKRIVO Deklarasyon PerMonitorV2 Uygulamalar için uygulama mekanizması: WPF-GetWindowDpiAwarenessContext ve AreDpiAwaressContextsEqual ile ana pencerenin gerçekten etkili bağını inceler.

Ek Layout / Raster Testleri Simülasyonu 150 ve 200 Yüzde birinde 1920 × 1080 Etiket Arşivi: mantıksal pencere boyutu 1280 × 693 İhracat 960 × 520 Tüm beş ana sayfalar 144 İhracat 192 DPI PNG olarak düşürülür. dosya sıcak ölçekli simülasyon-150-* veya scale-simulated-200-*.

Bu testler Windows ayarlarını değiştirmez. Gerçek WM_DPICHANGED monitör değişimi, karışık monitör konfigürasyonu ve başka bir ekran üzerinde gerçek yoğunluğu doğrulamıyorlar. Fiziksel ölçekleme testleri açık kalır.

WinForms analiz uyarısı WFO0003 yalnızca bu WPF başlangıç projesi için bastırılır: WinForms bilgi alanı entegrasyonu için kullanılır, WPF başlatma kontrolü; DPI pencere oluşturmadan önce manifesto ile belirlenir.

Microsoft belgeleri için: https://learn.microsoft.com/en-us/windows/win32/hidpi/setting-the-default-dpi-awareness-for-a-process

Görüntüleme: Dashboard ayrıca 650 DIP'nin penceresi yüksekliğinde yerleştirilmiş görüntülemeye geçiyor, böylece kartlar ve editörler çok küçük bir orta alana kaydırılmıyor.

Test durumu: hatasız serbest bırakma / uyarı; gerçek PerMonitorV2-Fensterkontext ve simüle edilmiş 150/200-Prozent-Renderings dahil tam WPF koku testi geçti. 200-Prozent-Dashboardaufnahme yükseklik ayarlamasından sonra görsel olarak incelenmiştir. git diff - kontrolü devam etti.
