<!-- generated-doc locale=tr-TR source=docs/superpowers/specs/2026-10-12-six-extensions-design.md sha256=a80dda083fbbe90404023ac784b47e7f16f89f72ce0b821e3cda122dc561317f -->

> Makine destekli çeviri. Yapı, kod örnekleri ve bağlantılar kontrol edilir; bu, ana dili konuşan birinin incelemesi veya hukuki onay anlamına gelmez. Geçmiş raporlar özgün tarihlerini ve kapsamlarını korur. [Orijinal Kaynak](../../../superpowers/specs/2026-10-12-six-extensions-design.md)

# MAKRIVO: altı ürün uzantısı ve klavye adaptörü

Tarih: 2026-10-12
Durum: Kullanıcı incelemesi için önerilen tasarım; uygulama başlamadı.

## Hedef
Mevcut Windows / WPF uygulamasındaki altı talep edilen uzantıyı, mevcut profilleri, G1/G2 görevleri, yerel işletme ve hesapsız kullanımları korumak. Klavye entegrasyonunu Razer ve Logitech, diğer üreticiler için yeniden kullanılabilir kapasite tabanlı bir mimarisi ile genişletin. Bir üretici adı veya klavye düzeninin asla mülkiyet kontrolünün desteklenmediğini kanıtlamaz.

## Mimarlık Kararları
Genişletme Aivana.Seriye edilebilir sözleşmeler ve doğrulama, Aivana.Windows ile Windows Adaptörler ve Aivana.İşlem editörleri ve durum görüntüleri ile masaüstü. izole eklenti çalıştırma ve yerel mobil panel hizmeti için ayrı ortak projeleri kullanın. her uzantıya bir yeniden yazma veya tek bir ayrıcalıklı süreç barındırma önlemek. mevcut yapılandırma değişmez yüklemek gerekir; devam eden enum değerlerini yeniden düzenlemek yerine eylem türleri eklemek.

Alternatifler göz önünde bulunduruldu: monolitik bir uygulama donanım, ağ ve çalışma akışının uygulanmasını engelleyecek; değiştirmek WPF ilişkisiz göç çalışmaları ekleyecektir. Modüler uzantılar önerilir.

## 1. Bağımsız makro tuşu
Cihaz kimliğine ve fiziksel tarama koduna göre bağları kaydetmek ve genişletilmiş anahtar bayrakları. Açık gözlem ve özel modlar, cihaz keşfi, anahtar yakalama, tekrarlayan bastırma, hareketler, tutulan katmanlar ve yeniden bağlantı teşhisi eklemek. Ana/numpad Enter ayırtları saklayın ve Alman/ABD düzenlerini test edin. Bağlantılar, seçilmiş bir fiziksel klavyeyi hedeflemektedir; ambigu yeniden bağlantı kimlikleri, başka bir cihazın kontrolü yerine yeniden seçilmesini gerektirir.

Raw Input Eksklusif cihaz başına kaldırma gerçek bir klavye filtre arka plan gerektirir; asla küresel düşük düzeyde çubuklar ile ilişkilendirilir Raw Input ve güvenilir cihaz özel kaldırma talep. bir gözlemci, acil serbest bırakma, hata-açık davranış ve yükleme / uninstaller sağlar. güvenli masaüstü giriş yakalamak değil. sürücü kurulum ve özel mod etkinleştirme açıkça ayrı kullanıcı eylemleri.

Sürücü kapısı: Hedefle uyumlu destekli, uygun lisanslı ve imzalanan bir arka plan seçin Windows 11 konfigürasyonlar, ya da özel bir filtre geliştirmek / imzalamak. interception bir değerlendirme aday değil, bir üretim bağımlılığı: resmi README test Windows 10 Bu kapı yerine gelene kadar, özel mod kullanılamaz olarak gösterilmelidir; özellik tamamlanamaz.

Kabul: İki fiziksel klavye ile, aynı anahtar bir makro çalıştırır ve yalnızca seçilen özel cihazda normal bir uygulama girişi üretmez; diğer klavye girişi etkilenmez. Çözünürlüğünü, çarpışmayı, iptal etmeyi, askıya almayı ve sürücü kaldırmayı gerçek donanımda kontrol edin.

## 2. Yönlendirilmiş çalışma alanı
Çalışma alanı, toplantı, akış, araştırma ve proje değiştirme için bir büyücü ekleyin. Yüklü uygulamaları, ses cihazları, monitörleri ve OBS sahneleri keşfedin. Kullanıcılar hedefleri ve bağları seçer; sonuçlanan adımları öngörür ve eksik gereksinimleri doğrular. Sadece inceledikten sonra kaydedin, kurulum sırasında çalışmayan eylemler. Oluşturulan profiller mevcut editörlerde düzenlenebilir kalır.

Kabul: Yapılabilir yolları veya cihaz kimliklerini manuel olarak düzenlemeden bir çalışma profili oluşturun; iptal sayfa ayarları değişmez; eksik yazılımın etkin bir açıklama vardır.

## 3. Doğrulanmış idam
Ön ışık kontrolleri, adım adım sonuçları ve seçmeli sonrası koşulları girin. İsteyen, tamamlanan, doğrulanmış, başarısız, zamanlanmamış, iptal edilemez durumları ayırt edin. Ses / mikrofon, parlaklık, süreç / pencere hazırlığı ve sınırlı asinkron okuma ile OBS durumu kontrol edin. Kısa / metin teslimatı, hedef uygulamanın komutu kabul ettiğini kanıtlamaz. Dış yan etkileri genel olarak geri dönüşümsüz değildir; sadece uygulandığı yerde tazminat sunar.

Kabul: Bağlantısız açık hedefler asla sessizce geri düşmez; yanlış gözlemli durum asla doğrulanmış başarı bildirir; iptal sorgu durdurur; başarısızlıklar adım ve iyileşme eylemi tanımlar. Uygulama geçmişi metin ücretleri ve sırları göz ardı eder.

## 4. Plugins ve Paylaşılabilir Paketler
Versiyonlu JSON gösterileri eylemleri, parametreler şemaları, devlet sağlayıcıları, bağımlılıkları ve yetenekleri ifade eder. Yürütülebilir eklentileri el sıkışıklığı, zamanlamalar ve iptal ile sınırlı yerel IPC üzerinde ayrı bir süreçte çalıştırın. Kötü biçimli mesajları ve uyumsuz versiyonları reddetmek; denetleyiciyi sona erdirmeden kazaları ele almak. Yetenek beyanları açıklama ve ortaklık, bir OS sandboxing iddiası değildir.

Paket ihracat / ithalat önizlemesi, içerik hashleri, yol-traversal koruma, boyut sınırları, versiyon/ bağımlılık kontrolleri, açık izin ve yükseltme geri dönüşü sağlar. İthalat edilebilir eklentiler otomatik olarak çalıştırılmamalıdır. Bir end-to-end install/action/state/uninstall akışı ile çalışma örneği eklenti içerir. Bu serbest bırakılma için kamu hizmetleri gerekli değildir.

Kabul: Gerçek bir örnek entegrasyonu oluşturmak, ihraç etmek, kurmak ve yürütmek; zaman çıkışı, kaza izole edilmesi, bağımlılık başarısızlığı, geçiş reddetme ve engellenmiş ithalat göstermek.

## 5. Windows UI Otomatik
Bir öğe piker ve bir öğe / devlet için çağrı, değer, seçim, çekme ve bekleme için sınırlı eylemler ekleyin. Süreç, otomatik kimlik, kontrol türü ve ileri bağlamı kullanarak seçici tutun; koordinasyon-sadece kaydetme önlemek. Seçici maç sayımı ve test seçicileri sadece okuyun. Şifre alanları kaydedilmeden çıkarılır. Destek dışı kontrolleri ve Windows bütünlük seviyesine açıkça bakın.

UIA eylemleri ile kayıtları genişletin, mevcut kısa kayıtları koruyun. Çoğaltıcı seçiciler bir kontrolü tıklamak yerine başarısız olur. Normal bir tarayıcı erişilebilirliği ağaç tam bir DOM otomasyon motoru için bir söz değildir.

Kabul: pozisyon/büyüklük değişiklikleri üzerinde istikrarlı seçiciler ile özel bir test uygulaması otomatikleştirin; test çelişkisi, eksik öğeler, engellenmiş kontroller, zaman çıkışı ve iptal.

## 6. Telefon / Tablet Paneli
Bir opt-in LAN hizmetinden bir yanıt paneli servis edin. Kısa ömürlü tek seferlik bir kod kullanarak açıkça çift ve yerel olarak korunan iptal edilebilir kimlikleri verin. Belgelendirilmiş yerel sertifikalarla HTTPS'i kullanın; şikayet metninde kimlik doğrulamaları göndermek için geri dönüş yok. Default ile engellenir ve yapılandırılmış arayüzleri kısıtlar. Kaynak doğrulama, doğrulanmış komutlar, oran sınırları ve sınırlı mesajlar zorunludur.

izin verilen profiller / eylemler, canlı durum, ilerleme ve iptal; istikrarlı ID'ler ve masaüstü ve fiziksel anahtarlar ile aynı uygulama çubuğu kullanın. Arzu edilen komutları, yürütülebilir yolları, sırları veya eklenti yüklemeyi göstermeyin. Bağlantı, çiftleşme ve iptal ekranları sağlar. Hiçbir bulut hesabı veya internet bağlantısı dahil değildir.

Kabul: Aynı LAN'da gerçek bir telefon/tabletten çalıştırılır; eşitsiz müşteriler listelenemez veya eylemleri çalıştıramaz; iptal edilen tokenler başarısız olur; çoğaltılmış / yeniden oynatılan komutlar ikiye katlanmaz; diskon sessizce eylemlerini geri çekmez.

## Keyboard üretici kapsamı

| Adaptör / Aile | Planlanan Entegrasyon | Açıklama sınırı |
| --- | --- | --- |
| Corsair | Aktif iCUE G anahtar girişi ve RGB entegrasyonu | Model/Software kapasite tespiti gereklidir |
| Logitech G | G-key SDK adaptörü baskı/düzeltme ve çalışma saatleri kullanılabilirliği kontrolü ile; ayrı SDK üzerinden aydınlatma | Her modelde desteklenen G-key API ile G HUB algılamasını eşitleştirme |
| Razer | Chroma aydınlatma adaptörü ve yönlendirilmiş Synapse-standart anahtar haritalama, artı genel HID giriş | Chroma bir aydınlatma API'dir; yerli makro anahtar girişi etiketlemeyin |
| SteelSeries | GameSense durum / aydınlatma adaptörü artı genetik HID girişi | GameSense etkinlik sunumu genel bir anahtar yakalama API değildir |
| Wooting | Analog SDK adaptörü, destekli analog girişler ve normal HID anahtarları için | Kapasite ve SDK dağıtım koşulları desteklenen model başına doğrulanmalıdır |
| Keychron/QMK/VIA | Standart HID anahtarları ve firmware haritalama talimatları; yalnızca kanıtla bilinen düzenler | VIA yapılandırma desteği evrensel bir çalışma anahtarı değil |
| Diğer standart HID anahtarları | Per-device fiziksel anahtar keşif ve haritalama; yalnızca doğrulanmış filtre arka planı ile özel mod | Üretilen model özel Fn, diy veya özel anahtar desteği yok |

Her cihaz, satıcı yazılımı, yerli SDK, sürücü-eksklusif, kullanılamaz veya donanım test edilmemiş durumunu gösterir. Adaptör versiyonları ve test edilmiş model / firmware tanımlayıcıları ile kapasite kanıtı saklayın.

## Teslimat Siparişi
1. Konfigürasyon uyumluluğu, kapasite sözleşmeleri ve doğrulanmış uygulanması.
2. Gerçek keşif ve mevcut eylem uygulayıcıları kullanarak yönlendirilmiş ayarlar.
3. UI otomatik ve kayıt / test sabitleme.
4. Plugin ev sahibi, paket akışı ve örnek entegrasyon.
5. Payed LAN Paneli ve Mobil Kontrolü
6. Genişletilmiş satıcı adaptörleri ve gerçek donanım doğrulaması ile özel klavye arka plan entegrasyonu.

Her altı da gerekli. Sürücü imzası / lisansı, erişilemez SDK veya eksik test donanımı serbest bırakma kapıları, mock davranışını değiştirmek ve işin tamamlanmasını çağırmak için bir neden değildir.

## Serbest bırakma ve serbest bırakma kriterleri
Mevcut çekirdekleri, Windows ve WPF kontrol edin ve her yeni sınır için odaklanmış bir entegrasyon testi ekleyin. JSON yuvarlak gezileri ve miras profilleri kontrol edin; ithal edilebilir içerik, ağ doğrulama, iptal, eklenti kazaları ve cihaz yeniden bağlantısı. Masaüstü ve mobil düzenleri kontrol edin. Yüksek ölçü Windows ayrı olarak doğrulanır. Serbest bırakma belgelerini yalnızca kontrolden sonra oluşturun; serbest bırakma notlarında uygulanmış/test edilmiş, uygulanmamış/hardware-test edilmemiş ve dışarıdan engellenmiş işlevleri ayırt edin. Yüklü uygulamayı değiştirmeyin, sürücüleri yüklemeyin, gerçek klavye haritalamalarını değiştirin veya bir tesadüf testi olarak ağ erişimi açın.

## Kanıtlar
- Microsoft Raw Input Semantik: https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-rawinputdevice
- Logitech G geliştiricisi SDK: https://www.logitechg.com/en-us/programs/developer-alliance
- Razer Chroma SDK FAQ: https://developer.razer.com/works-with-chroma/chroma-faq/
- Bağımlılık ve bağımlılık koşulları: https://github.com/oblitum/Interception
- Mevcut depolama üreticisi listesi: docs/de-DE/research/tastaturhersteller-sonderfunktionen.md

## Değerlendirme Kararları
Kullanıcı, uygulama planı öncesinde bu yazılı tasarımın onaylanması istenir. Ana dış bağımlılık, üretime hazır özel giriş sürücüsüdür. Önerilen aralığı, sessizce Raw Input gözlemlere düşürmek yerine bir kapı olarak tutar.
