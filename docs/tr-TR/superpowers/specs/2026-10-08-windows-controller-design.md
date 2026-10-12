<!-- generated-doc locale=tr-TR source=docs/superpowers/specs/2026-10-08-windows-controller-design.md sha256=f36d496c6af5190889e9a31487a8fc6afdeba5af18facdd600639ede5ddbd169 -->

> Makine destekli çeviri. Yapı, kod örnekleri ve bağlantılar kontrol edilir; bu, ana dili konuşan birinin incelemesi veya hukuki onay anlamına gelmez. Geçmiş raporlar özgün tarihlerini ve kapsamlarını korur. [Orijinal Kaynak](../../../superpowers/specs/2026-10-08-windows-controller-design.md)

# MAKRIVO - Tasarım

[English (US)](2026-10-08-windows-controller-design.md) | [Deutsch](2026-10-08-windows-controller-design.md) | [Français](2026-10-08-windows-controller-design.md) | [Italiano](2026-10-08-windows-controller-design.md)

Şekil : 8 Ekim ayı 2026 Durum: Uygulamadan önce incelenmesi için.

## Hedef ve anlaşılmış kapsam

Bağımsız bir Windows uygulama, normal bir PC tuşuna bir aksiyon paneli gibi özgürce yapılandırılabilir hale getirir. Kullanıcı tüm önerilen eylem türleri, makro, cihaz tanıma, profilleri ve iyi bir GUI doğruladı. Bağlanan Corsair ve diğer tuşlar tespit edilir; ek üretici özellikleri sadece gerçekten desteklenir.

Varsayımlar: Alman yüzeyi, hesap olmadan yerel kullanım, Windows 11 x64 ilk hedef, sürücü yükleme yok. Bu yazıyı normal yazmak için kullanabilirsiniz. Küresel düğmeler standart olarak değiştiricilerle düğme kombinasyonlarını kullanır. Bireysel harfler standart olarak küresel olarak rezervasyon yapılmaz.

## Teknik Karar

Native C#/ NET-10-App ile WPF ve MVVM. Sabit .NET-10-SDK çalışma alanında mevcuttur. WPF bireysel olarak tasarlanmış bir masaüstü arayüzü ve doğrudan Windows entegrasyonu sağlar. Alternatifler ayrı bir Windows hizmeti olan bir web arayüzü veya platformlar arası bir masaüstü uygulaması olacaktır; her ikisi de bu Windows projesine ek çalışma ve iletişim karmaşıklığı getirecektir. Bu nedenle seçilen temel seçenektir.

Bölünme: Görüntüleme ve Tray için masaüstü projesi, yapılandırma ve uygulama mantığı için çekirdek projesi, Windows Hotkeys projesi, giriş simülasyonu ve cihazlar, platform bağımsız kurallar için test projesi. Üretici adaptörleri sabit bir kapasite arayüzüne bağlıdır ve standart işlemlerden bağımsızdır.

## Yüzey

Sessiz anthrazit renkli alanlar, iyi okunabilir yazı ve Türk renkli aksan ile karanlık tasarım. Dashboard, profiller, cihazlar ve ayarlar için bağlantı navigasyonu. Yukarıda aktif profil, arama ve tüm hotkeys durdurmak için anahtar. Yüzey 100–200 yüzde Windows ölçekte kullanılabilir kalır.

Dashboard'da ayarlanabilir bir çerçeve vardır. Her kutu simge, isim, hotkey ve performans statüsünü gösterir. Hotkey ile aynı eylemi yapar. Sağda, eylem türü, parametreler, hotkey kaydı, simge/renk, etkinleştirme ve test düğmesi ile bir editör açılıyor. Yeni, çoğaltma, silme ve düzenleme mevcuttur. Bir klavye görüntüsü, Alman standart düzen için şematik olarak belgeleri gösterir; tespit edilen cihazın kesin geometri olmadığını iddia eder.

Profiller oluşturulabilir, kopyalanabilir, değiştirilebilir ve manuel olarak etkinleştirilebilir. Seçmeli olarak bir profil için bir süreç adı atanır. Uygun bir temel program profilini etkinleştirir, aksi takdirde küresel standart profil geçerlidir. Birden fazla uygun profil için açıkça görülebilir bir sıralama uygulanır. Editör, Hotkey çatışmalarını doğrudan yerleşim yerinde açıklıyor.

## Hotkeys ve girişler

Windows RegisterHotKey baskılı düğme geri dönüşü ile normal küresel kombinasyonları işler. Sadece aktif profil kaydedilir. Rezervli veya zaten kaydedilen kombinasyonlar hatalar olarak görünür; uygulama onları başarıyla etkinleştirilmediğini bildirir. Editörün içindeki kayıtlar herhangi bir eylemi çözmez.

Simülasyonlar öncesinde, değiştiricilerin düğmeleri serbest bırakılır veya fiziksel serbest bırakılmasını bekler. Unicode metni ve düğme kombinasyonları SendInput ile gönderilir. Kendi kendine basılan her düğme, kırılma durumunda bile serbest bırakılır. Eylemler UI çubuğunun dışında çalışır; Uygulama ve profil değiştirme yüzeyi engellemez.

## Etkinlikler ve makro

Aksiyon türleri: Programı argümanlar ve çalışma dizinleri ile başlatın, dosya / klasörleri Windows üzerinden açın, HTTP / HTTPS web sayfası açın, Unicode metni girin, düğme kombinasyonu gönderin, Medya Play / Pause / Sonraki / Önceki / Durdurun, yüksek / düşük / gürültü ve profil değiştirin. Parametreler depolamadan önce ve yeniden yürütülmeden önce doğrulanır. Programlar doğrudan başlatılır; argümanlar kasıtlı olarak Shell komutları olarak yorumlanmaz.

Makro, belirtilen eylemlerin ve açık adımların düzenli bir listesini içerir. Adımları eklemek, değiştirmek ve ertelemek mümkündür. Tekrarlamalar nihayet yapılandırılabilir (1–100); Sona ermeyenler ilk versiyonuna ait değildir. Çalışan bir makro yüzey veya kendi çarpışma hotkey üzerinden sona erebilir. Tekrarlayan aynı devam eden yerleştirme paralel bir koşu başlatmaz. Giriş simülasyonu ile farklı makrolar serialize edilir, böylece tuşları karıştırılmaz. Bir adım hatası durumunda, makro durur ve etkilenen adım gösterir.

## Ekipmanlar ve Üreticiler

Ekipman listesi Windows Raw Input ve ek Ekipman Bilgileri: Görüntüleme Adı, Ekipman Yolu, Kullanılabilir Olarak Üretici / Ürün Tanımı, Bağlantı Durumu ve Tanımlanan Yetenekler. Giriş/Giriş listesini yeniden başlatmadan güncelleştirir. Bir fiziksel cihazın birden fazla HID girişine sahip olabileceği; bunlar yükleyici kimliklere göre gruplandırılır, aksi takdirde ayrı girişler olarak görülebilir.

Normal Hotkeys klavye çapında geçerlidir. Cihaz Görüntüsü bilgi ve becerileri sunar; yalnızca kilitlenmiş düğmeleri olan ayrı bir tuş bu anlaşılmış modun bir parçası değildir.

Corsair tanımlama iCUE olmadan da çalışır. Seçmeli bir iCUE SDK adaptörü, SDK, iCUE ve uyumlu cihazlar mevcut olduğunda bağlantı durumu ve desteklenen aydınlatma işlemleri sağlar. UI fark, SDK mevcut, bağlantılı ve gerçekten kontrol edilebilir. SDK öncelikle LED kontrolü ile belgelendirilir; özel G düğmeleri yalnızca Windows uygun girişleri aldığınızda normal hotkeys gibi desteklenir. Tüm son basınçlar için sabit bir söz yoktur.

Diğer üreticiler öncelikle genetik tanımlama ve standart eylemler alır. Adaptör arayüzü daha sonraki entegrasyonları sağlar; desteklenmeyen beceriler dürüstçe gösterilir. Mevcut olmayan modeller için donanım testleri mevcut olarak belirtilmez.

## Depolama ve Günlük

Yerel uygulama veritabanı altında JSON yapılandırması. Son geçerli yedekleme ile nükleer depolama; hatalı dosyalar sessizce yazılmaz. İthalat, format, versiyonu, kimlikleri ve parametreleri kontrol eder ve alınmadan önce bir önizleme gösterir. İthalat edilen eylemler yalnızca açıkça etkinleştirildikten sonra yapılabilir. İhracat profilleri ve belgeleri içerir.

Tray menüsü: Açma, profil seçimi, hotkeys durdurma, makro kırma, bitirmek. Pencereler standart olarak kapalıdır ve ilk kez açıklanır. Otomobil seçenekli ve standart. Sadece bir kuruluş Hotkeys yönetiyor. Hotkeys kaydedilir, sonuçları kırar ve kaynakları serbest bırakır.

Sınırlı bir etkinlik vizyonu başarı, hatalar ve çöküş için. Tüm yazılım dosyalarını kaydetmeyin. Yazı içeriği hata mesajlarında yazılmaz. Hiçbir bulut hizmetleri ve telemetrisi yok.

## Hatalar ve Sınırlar

Eksik programlar, geçersiz URL'ler, kaybolan hotkeys, hasarlı profilleri ve erişilemez üretici SDK'lar belirli Alman mesajları alır. Bir adaptör hatası standart işlemleri etkilemez. Daha yüksek ayrıcalıklı uygulamalarda girişler Windows izin sınırlarında başarısız olabilir; hatalar açıklanır, uygulama standart olarak yönetici hakları gerektirmez.

## Deneme ve İndirim

Profil öncelikleri, çatışma tespiti, yapılandırma ithalatı, atom depolama, makro sırası, tekrarlama, kırılma ve hata propagandası için otomatik testler. Windows gerçek hotkeys, metin ve düğme kombinasyonları, medya eylemleri, cihaz depoları, tarama ve temiz bitirme için. Herhangi bir program veya web sitesi tesadüfen başlatmak için test yok.

GUI testleri: Yükleme ve düzenleme, uygulama olmadan kaydetme, eşit etkiye sahip tıklama/Hotkey, makro kırılma, çatışma bildirimi, profil değiştirme, cihaz görünümü, ithalat/eksport ve ölçekleme. Üretici özel donanım kontrolü, mevcut cihazla ayrı olarak belgelendirilir.

Teslimat: Kaynak kodu, çoğaltılabilir yapı / başlangıç kılavuzu, Alman kullanım kılavuzu ve yerel Windows-x64-Publish-Artefakt. Başarı, tüm genel işlevleri olan yürütülebilir bir uygulama anlamına gelir; Corsair becerileri yalnızca SDK / cihaz desteği ile kullanılabilir olarak işaretlenir.

## Teknik Kaynaklar

- Microsoft Klavye Giriş / KayıtHotKey: https://learn.microsoft.com/en-us/windows/win32/inputdev/about-keyboard-input
- Microsoft Raw Input: https://learn.microsoft.com/en-us/windows/win32/inputdev/about-raw-input
- Microsoft WPF Giriş : https://learn.microsoft.com/dotnet/desktop/wpf/advanced/input-overview
- Resmi Corsair iCUE SDK: https://github.com/CorsairOfficial/cue-sdk/blob/master/README.md

## Bir sonraki adım

Bu projeyi inceledikten sonra, belirli bir uygulama planı hazırlamak ve uygulama seçimi için sunmak. Boş çalışma programında henüz bir Git repository yoktur; bu nedenle bu belge henüz komited değildir.
