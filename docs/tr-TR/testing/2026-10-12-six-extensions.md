<!-- generated-doc locale=tr-TR source=docs/testing/2026-10-12-six-extensions.md sha256=ff2eb755b2f18dce8c7bd7c832018ae71ee706bcca65e37c191dbb55ff999c1e -->

> Makine destekli çeviri. Yapı, kod örnekleri ve bağlantılar kontrol edilir; bu, ana dili konuşan birinin incelemesi veya hukuki onay anlamına gelmez. Geçmiş raporlar özgün tarihlerini ve kapsamlarını korur. [Orijinal Kaynak](../../testing/2026-10-12-six-extensions.md)

# 6 Geliştirme: Uygulama ve deneme

Şekil : 12 Ekim 2026, Endüstri `codex/six-extensions` Çalışma durumu, üretim serbest bırakma değil.

## Hizmetler

Komut tablosu girişleri açıyor **Çalışma alanı kurmak**, **Yazıcılar ve SDK'lar**, **Mobil Panel**, **Genişletmeler**, **UI otomatik**, **UI’nin bitmesini** ve **Uygulama Denetimi**.

- Çalışma alanı asistanı, çalışma alanı, toplantı, akış, araştırma ve proje için profilleri oluşturur. Kayıt/Başlangıç menüsü veya dosya seçimi, URL, çıkış/ekran ve OBS sahnesinden programlar kaydetmeden önce adım olarak görüntülenir. Kuruluş hiçbir eylem yapmıyor.
- Adım Editöründe ** Sonuçları kontrol edin ** ön kontrol ve geri kontrolü girer. Geçerlilik ve mobil panel gerçek doğrulama kontrol edilemez etkilerden ayırt eder. Relatif değişiklikler, klavye kısaltması ve metin girişleri sabit bir etki doğrulaması sağlamaz. Bir süreç kanıtı, uygulama için profesyonel bir istek göstermez.
- UI otomatikliği ayrı, zaman sınırlı bir işçi kullanır. Mouse pozisyonu, selektör kontrolü, Invoke/Select/Toggle/Focus/SetValue/WaitState aracılığıyla seçim yapılır; birden fazla gol, devre dışı bırakılmış hedefler ve şifre alanları reddedilir. Kayıt desteklenen tıklama ve dinlenme işlemlerini içerir; metin girişleri kesilmez. Kabul etmeden önce kontrol edilecektir.
- Genişletmeler: ZIP test, hash, şeması, devre dışı bırakılmış ithalat, açıkça etkinleştirme öncesi güven, sınırlı IPC ile izole edilmiş süreç, devre, devre kurma, önceki sürümü geri yükleme / reddetme. İş yalıtımı kum kutusu değildir: Paylaşılan eklentiler kullanıcı hakları ile çalışır. Örnek paketi `makrivo.example.zip` sayım ve durum eylemi içerir. Desteklenen şemalar kaynağı kodunda belgelendirilmiş basit bir JSON şeması parçası oluşturur; kamu piyasası değildir.
- Mobil panel: yerel IP ve paylaşılmış profilleri seçin, HTTPS adresini açın, sertifikayı görüntüleyin SHA-256-Fingerprints ve bilinçli olarak güvenin, sonra tek bir altı karakter kodunu girin. Özel anahtarlar Windows-DPAPI ile korunur; yalnızca müşteri erişimi hashları kaydedilir. Sertifikalar yenileme veya IP değişikliğinden sonra değişir. İhtiyaç duyulduğunda Windows Firewall kullanıcısı tarafından serbest bırakılır. Otomatik sertifika kurulumu, port genişliği veya ateş duvarı değişikliği yok. Müşteri girişleri iptal edilebilir. Panel eylem durumu ve test geçmişi gösterir; henüz evrensel canlı sensör tablosu değildir.

## Klavye

| Üretici | Uygulama | Sınırlama |
|---|---|---|
| Corsair | Mevcut iCUE G tuşları ve RGB | Yerel iCUE bağlantısı ve uygun model gereklidir |
| Logitech G | G-Key SDK ve ayrı LED SDK | Orijinal yerel x64-DLL açıkça serbest bırakın; G-Key SDK birden fazla tuşu bireysel kimliği vermez |
| Razer | Chroma RGB ve Standart Düğme Tanıma | Önceki İçerikDüzenle Synapse |
| SteelSeries | GameSenseRGB ve standart düğme tanımlama | Önceki İçerikKaynaklar GG’ye |
| Wooting | Analog değerler, basınç/kırılma dalgaları | Orijinal yerel x64-SDK-DLL; normal HID giriş aktif kalır |
| Keychron/QMK/VIA/Ducky/ASUS/HyperX/Roccat/Diğerleri | Standart fiziksel düğmeler cihaz ve tarama kodu | Fn ve Protein Kontrol Elemanları |

Yeni özel SDK DLL'ler teslim edilmez. Yeni üretici adaptörleri uygun fiziksel cihazlar olmadan henüz donanımlı değildir.

**Sadece ikinci makropad tamamlanmamıştır: ** Raw Input cihazları ayrı izler, ancak normal Windows girişini bastırmaz. Üretim serbest bırakılması için lisanslı, Windows 11/Secure tekne altında kontrol edilen bir filtre sürücüsü ve acil serbest bırakma, çökme, askıya alınma ve yeniden bağlantı testleri gerektirir. Hiçbir sürücü kurulmadı. Uygulama bu sınırı açıkça belirler.

## Sınavlar

- Release Build: Başarılı, sıfır uyarılar / hatalar.
- Temel konsollar: Tüm test grupları, üretici tuşlarında çatışmalar, plug-in rollback ve test sürecinde gerçek performans hataları da dahil olmak üzere başarılı.
- Genişletme entegrasyonu: WPF deneme uygulaması ile gerçek UIA işçisi; benzersiz invoke, çoklu, devre dışı bırakılmış hedefler, şifre hariçliği. Adlandırılmış tüp eklenti işlemi, protokol sınırları, analog histerisi ve Logitech ABI dekodasyonu başarılı.
- Gerçek HTTPS barındırma: çarpıcı sertifika, bağlantısız erişim, yanlış / tek seferlik kod, kaynak koruması, tarayıcı dosyaları, hash depolama ve korunan anahtar başarıyla. Sınavda tespit edilen efemer anahtarlar ile bir kanal hatası düzeltildi.
- Windows-Açıkça reddedilen temel başarı testleri ile Suite: başarılı; Audio/OBS/iCUE/Raw Input Ekran durumu kontrol edildi. önceki sürüşte, odaklanma değiştirme reddedildiğinde tam süit başarısız oldu. sonraki sürüşte AI prototipi de odaklanma ve bağlantı değiştirme dahil tam süit vardı iCUE-Tekrarlanan bir tekrarlama yeniden odak değişikliğinde başarısız oldu; bağlamlı test bu şekilde istikrarlı yeşil değil. Bu test zayıflamadı. Kısa sürede eksik olan DDC okuma izleri sınırlı tekrarlanır; parlaklık değişmedi.
- WPF Smooke: tüm eski ve yeni diyaloglar, yapılandırma değişkenliği, editör ve CRUD başarılı; yapılandırma tablosunda ekran görüntüleri `smoke`.
- Playwright ile Edge, 390×844 ve 1024×768, Bağlantı akışı /UUID / tek seferlik gönderme / hiçbir yatay geçiş başarılı. API Mock; gerçek HTTPS test ayrı. `artifacts/mobile-browser`. Tarayıcı eklentisi mevcut değildi, bu nedenle düzenli Playwright Fallback.
- Bağımsız kod incelemesi yapılır; gecikmiş mobil komutlar, eksik terminal kontrol sonuçları, işlem yolu incelemesi, eklenti dönüş ve RGB kalp atış düzeltildi.

Açık: Eksklusif sürücü entegrasyonu, fiziksel üretici/telefon/tablet LAN testleri, Windows ölçekleme 150/200 yüzde ve tüm yeni iletişim metinlerinin tam çeviri incelemesi. Yeni diyalog metinleri çoğunlukla Almanca; mevcut dil katalogları korunur. Bu nedenle, toplam talep ** tamamen düşürülmemiştir**.

## Paket üretmek

`./tools/publish-complete.ps1 -SelfContained` masaüstü, UIA işçileri, HTTPS barındırma, tarayıcı dosyaları, ithal edilebilir örnek eklenti ve çalıştırma zaman lisansları dahil olmak üzere `artifacts/release-*` altında yeni bir klasör oluşturur. Yüklenen uygulama yazılmamıştır. `-SelfContained` olmadan, paket uygun .NET-10-Desktop-/ASP.NET-Runtimes gerektirir.

Paket yerel bir test noktasıdır, imzalanmış bir satış kurucusu değildir. Kendi Kullanım Koşulları ve SDK Satış Hakları ayrı yayınlardır; mevcut belgeleri `docs/licensing` sayfasına bakın.
