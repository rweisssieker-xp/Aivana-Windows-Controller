<!-- generated-doc locale=tr-TR source=README.md sha256=eb6f3fe7810ed3e10793e8938e908e151a84c4afbea2f22cba2656bfe4e8dfc3 -->

> Makine destekli çeviri. Yapı, kod örnekleri ve bağlantılar kontrol edilir; bu, ana dili konuşan birinin incelemesi veya hukuki onay anlamına gelmez. Geçmiş raporlar özgün tarihlerini ve kapsamlarını korur. [Orijinal Kaynak](../../README.md)

# MAKRIVO

[Tüm 27 dillerde tam belge](README.md) · [Tam Kullanıcı Kitabı](HANDBOOK.md)

**tarafından Aivana.** [Mevcut özellikler listesi ve serbest bırakma sınırları](FEATURES.md) · [Uzantı Test Raporu](testing/2026-10-12-six-extensions.md) · [Yerel Asistan Prototype](testing/2026-10-12-local-meeting-assistant.md)

**İngilizce ( US ))** | [Deutsch](../de-DE/REFERENCE.md) | [Français](../fr-FR/REFERENCE.md) | [Italiano](../it-IT/REFERENCE.md)

Stream Deck tarzı çubuklar, profiller, makro ve klavye algılama ile yerel bir Windows sıcak anahtar kontrolörü. Bu arayüz 27 dillerini destekler. Günlük kullanım için hesap veya bulut bağlantısı gerektirmez.

[Dokümanlık](README.md) · [Kontrolü](VERIFICATION.md) · [Özellikler test raporu](testing/2026-10-09-usp-release.md)

## Başlamak için

Başvuru yapıldıktan sonra `artifacts/win-x64/MAKRIVO.exe` Tüm çıkış klasörünü bir araya getirin; başka bir yere kopyalayabilirsiniz. Windows 11 x64 başlangıç hedefi. Yapılan eserler Git'ten çıkarılır; bir kaynak kontrolü önceden yapılandırılmış bir uygulama içermez.

Kaynaktan oluşturmak ve çalıştırmak için `global.json` belirtilen .NET 10 SDK'yı kurun:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project src/Aivana.Desktop -c Release
```

Ayrı yüklü bir .NET çalışma süresi gerektirmeden kendi içeriği olan bir yapı yayınlayın:

```powershell
dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64
```

## Temel çalışma akışı

1. Bir profil seçin ve bir eylem çubuğu oluşturun.
2. Adını, simgesini, renkini ve klavyenin kısaltmasını ayarlayın. Ctrl/Alt/Shift kombinasyonları, yazma sırasında tesadüfi etkinliği azaltır.
3. Bir veya daha fazla adım ekleyin: uygulamalar, dosyalar, web siteleri, metin, kısaltmalar, medya, hacim, profil değiştirme veya gecikmeler.
4. kurtarma Değişiklikler kurtarılana kadar bir tasarıma sahiptir; kaydedilen versiyonu geri getirir. Tile Play düğmesi ve sıcak anahtarı aynı yürütme yolu kullanır.
5. Çekme ve atma düğmeleri ile yeniden düzenleme çubukları ve makro adımları. şablonlar ayarları kolaylaştırır. 100 zaman. arayüzü, sistem sürüş menüsü veya varsayılan olarak Ctrl+Alt+Escape.

Uygulama özel profilleri, tam yolu olmadan bir süreç adı kullanır. Profil siparişi bağlantıları kırar; varsayılan profil düşüştür. Profil seçimi ayarlarda etkinleştirilmelidir.

Ana pencereyi kapatırken, uygulama varsayılan olarak sistemde tutar. Tekrar tıklayın ve yeniden açın; çıkış için tekrar menüsü kullanın. Windows ile başlatma seçmeli ve varsayılan olarak devre dışı bırakılır. Hotkeys merkezli olarak durdurulabilir.

## eylemler ve komut paleti

Aksiyon ekleme, sekiz kategoride 90 önizlemelerle arama yapabilen bir katalog açıyor. Her birinin bir öyküsü ve öyküsü vardır. Makro editörü, adımları eklemek veya değiştirmek için aynı katalog kullanır. Bu editör çalışma akışında bir önceden ayar seçmek, çalışmadan bir tasarıma sahiptir.

Komut paleti başlıkta veya uygulamanın içindeki Ctrl+K ile açılır. Profiller, katalog öncesi setler, profil değiştirme, navigasyon, duraklama/resume, makro iptal ve ithalat/eksport üzerinden kaydedilen eylemleri arar. Filtre kaydedilen eylemler, katalog fonksiyonları veya uygulama komutları. Arrow anahtarları seçin, Enter etkinleştirir, Ctrl+Enter bir düzenlenebilir giriş düzenler ve Escape kapatılır. Favori ilk olarak ortaya çıkıyor. Kullanım için hazır katalog komutları hemen çalıştırılabilir; bir tasarıma açılmasını gerektiren ön ayarlar. Ayarlar, küresel bir palet kısaltması tanımlayabilir. Yeni yapılandırmalar Ctrl+Shift+Space kullanır; mevcut yapılandırmalar otomatik olarak yeni bir küresel kısaltma almaz.

Ses eylemleri tam olarak 0 ile 100 yüzdesi arasında hacmi ayarlar ve sessiz, değişmez veya kaydırma çıkışı ve mikrofon cihazları. Belirli bir son nokta seçin veya varsayılan Windows için ayarlanmaz bırakın. Kullanılamayan bir son nokta, başka bir cihazı kontrol etmek yerine bir hata üretir. Refriger Devices bağlantılı cihazları yeniden yüklüyor.

OBS Stüdyo Kullanımı WebSocket v5: start/stop/toggle/pause/resume kaydı; start/stop /toggle akışı; sahneleri değiştirmek; toggle giriş mute. WebSocket Aracın altındaki server OBS Editörün son noktasını ayarlayın.Connection ve Load Names okuyor OBS Bir kayıt veya akış başlatmadan versiyonlar, sahneler ve girişler. listeden isimleri seçin veya onları manuel olarak girin. Windows Sertifika Yöneticisi ve ihraç edilen yapılandırmalardan hariç. OBS Bağlantı hatası bildirir.

Profiller bir süreç adı ve bir pencere başlığı parçası ile eşleşebilir. Bu iki koşul da eşleşmelidir. Lock Profile oturum için otomatik olarak geçiş yapmayı engeller. Foreground'dan çekim koşulları son odaklanmış dış uygulama kullanır.

Video ve clipboard desteği `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}`, ve `{{user}}`. Tarih Kullanımı `yyyy-MM-dd`; Zaman Kullanımı `HH:mm`. Bilinmeyenler Değişmez. `{{profile}}` Başlatılan eylemin profilini belirleyin; `{{process}}` Clipboard içeriği yalnızca bu siteyi kullanırken okunur.Clipboard eylemi metni yazar ama otomatik olarak yapıştırmaz.

Pencere eylemleri, monitörün çalışma alanının sol/sağ tarafındaki bir pencereyi en aza indirmek, en üst düzeye çıkarmak veya düzenlemek. İşletme sisteminin kapatılması Windows Bu eylemler yalnızca açıkça talep edildiğinde yürütülür.

Önizleme, adımlar, tekrarlar ve değişkenler uygulanmadan gösterir. Undo ve Redo, 100 tasarım durumuna kadar tutar; metin kutuları yerli Ctrl+Z düzenini tutar. Editör veya tablo menüsünde favori işaretleri işaretleyin ve bunları panelde filtreleyin. Yürütme sayısı yalnızca mevcut oturum için geçerlidir.

Yazı, kısaltma ve arayüzden başlatılan pencere eylemleri son hedef penceresini etkinleştirir. Hedef bu konuyu gösterir. Hedef kapatıldı veya Windows etkinleştirmeyi reddederse, uygulama bir mesajla durur.

## Keyboard Yardım ve Feedback

Özel anahtar sihirbazı klavye sayfasında ve paletinde yer almaktadır. G-Key'leri kaydetti iCUE Anahtar Baskı, görevlendirilen eylemleri yerine getirmeden geçici olarak yakalar. Normal bağlar tespit, iptal veya kapatma sonrasında geri döner. Bir katalog fonksiyonu ve seçilmiş anahtar kaydedilmemiş bir taslak olarak açılır; çift bağlantılar kontrol edilir.

Keyboard Overlay seçilen referans düzenini ve aktif bağları gösterir. Ctrl+Alt+F1 tutun veya palet veya Klavye sayfasından çıkarın. Konforu almaz ve fare tıklamalarını geçmesine izin verir. Profil ve zorunlu değişiklikler güncellenir; sıradan Windows anahtar baskıları vurgulanır. Üretici özellikli kontroller eşdeğer bir Windows anahtar sinyal sağlayamaz.

Yönlendirme geribildirimleri bekleme, adım / tekrarlama ilerlemesi, tamamlanma, iptal veya odaklanmadan veya girilen metni ortaya çıkarmadan gösterir. Commands Executed, komut uygulamasında bağımsız olarak istenen bir canlı durum değil, komut uygulaması doğrulamaktadır. Hataların ayrıntıları aktivite logunda görünür.

## Üretici ve Corsair

Corsair G1 - G6 resmi iCUE SDK aracılığıyla doğrudan atanabilir. Eylem editöründe cihazı ve anahtarı seçin ve kaydedin veya K100 tuş görüntüsünden bir tasarıma hazırlayın. Bağlantılar seçilen cihaz ve aktif profil için geçerlidir; duraklar da bunları engeller. iCUE çalıştırılmalı ve erişim sağlanmalıdır. Fn, iCUE tekerlek, profil ve kilit kontrolü bu G anahtar API aracılığıyla haritalamıyor.

Cihazlar sayfası Windows klavye arayüzlerini ve düzenli olarak ve cihazların bağlandığında yeniliyor. Bir fiziksel klavye çok sayıda HID arayüzüne maruz kalabilir. Standart küresel hotkeys üreticiler arasında çalışır.

Corsair tespit mevcut cihaz bilgileri kullanır. RGB kontrolü uyumlu donanım, çalıştırma iCUE ve resmi iCUE SDK gerektirir; uygulama gerçek kullanılabilirliği bildirir. Diğer üreticiler şu anda tespit ve standart eylemler alırlar. Corsair G anahtarı entegrasyonu dışında, mülkiyet kontrolü Windows bunları düzenli giriş olarak aldığında çalışır. Uygulama bir klavye sürücüsü yüklemiyor.

## Diller

Settings Teklifler 27 İngilizce, Fransızca, İspanyolca, İtalyanca, Portekizce, Hollanda, Polonyalı, Çekçe, Slovakca, Macarca, Bulgarca, Yunanca, İsveççe, Norveççe, Finlandiya, Estonya, Litvanya, Slovenya, Hırvatça, Ukrayna, Rusça, Türkçe ve Sırpça Windows Görüntüleme dilini. çeviriler yerel olarak bağlanmıştır. ayarlar, hotkeys ve kullanıcı tarafından oluşturulan isimler arayüz dilinden bağımsızdır.

Tüm kayıt defterleri 27 dilinde mevcuttur. [Dil Endeksi](README.md)Bu yüzden Çeviriler, orijinal kaynak bağlantıları ve yapısal kontroller ile makine tarafından desteklenir; yerli konuşmacı incelemesi ayrıdır. Uygulama ayrıca bölgesel kodları `en-US`, `de-DE`, `fr-FR` ve `it-IT` kabul eder ve bunların sayısı biçimlerini kullanır. Mevcut dil kodları uyumludur.

## Layouts ve Üretici Araştırmaları

Klavye görünümü tam boyutlu ve düşünülemez ISO-DE/ANSI-US referansları artı Corsair K100 RGB düzenleri sağlar. Geometri pozisyonları, geniş / anahtarları ve ISO Enter'i temsil eder. Standart düzenler genetik referanslardır. K100 düzenler milimetre doğru boyutları talep etmeden belgelendirilmiş kontrolleri izler. Doğru dil/geometri varyantını seçin: bir model adı veya USB VID/PID belirli bir varyant belirlemez.

Haritalama, aktif Windows giriş düzenini kullanarak fiziksel tarama kodlarını çözür. Çalışma akışı bir taslak hazırlar ve kurtarma / çatışma kontrollerini korur. Standart bağlar küresel Windows sıcak anahtarlar, tek bir bağlantılı klavye ile izole edilmez. Corsair G1- G6 cihaz özel iCUE entegrasyonu kullanır. Diğer mülkiyet kontrolü bir adaptör veya üretici tarafından yapılandırılmış kısayol gerektirir. Ana Enter ve sayı anahtarı Enter, küresel hotkeys için bir sanal anahtar kimliği paylaşır.

The için [Üretici Araştırmaları](research/tastaturhersteller-sonderfunktionen.md) ve [CSV](research/tastaturhersteller-sonderfunktionen.csv) 74 marka / üretici grupları, konfigüratörleri, giriş SDK'ları ve aydınlatma API'leri ayırt eder. [Layout Kanıtı](research/keyboard-layout-evidence.md) Modeller, modeller ve modeller arasındaki fark. Araştırma kapsamı, donanım desteğinin uygulanması anlamına gelmez.

## veri

Yerel uygulama verilerinde yapılandırma kaydedilir. İthalat / ihracat kullanımı JSON. İthalat konfigürasyonu öngörür, doğrulandıktan sonra mevcut konfigürasyonu değiştirir ve ithal edilen bağlantıları manuel olarak etkinleştirene kadar devre dışı bırakır. Tekstil eylem içeriği bir anahtarlık olarak toplanmaz. Credential Manager şifreleri ihraç edilen profillerden ayrıdır.

## Kontrolü

```powershell
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
```

Windows kontroller donanımını kontrol edebilir ve geçici olarak destekli iCUE bağlantıları ayarlayabilir. Bu bağlantıları başka bir kontrolör kullanmadan çalıştırın. Ses son noktası kontrolleri canlı hacim veya değişken ayarları değiştirmez.

Yurt içi girişler Windows ayrıcalığı aşamaz. Normal olarak başlatılan bir denetleyici, bir yönetici uygulamasına giriş gönderemez. Korunan veya işgal edilen hotkeys çatışmalar olarak görünüyor.

The için [Belgelendirme Index](README.md) Tarihi tasarımlar, planlar, araştırma ve tarihli test raporları. Tarihi planlar kaydedilen kapsamını, mevcut bir özellik desteği garantisi değil anlatır.

### Çalışma akışı şablonları ve hedef penceresi bekliyor

Katalog altı düzenlenebilir çalışma akışları içerir: çalışma alanı, toplantı, akış, araştırma, proje anahtarı ve metin snippet. Katalog veya komut paletinde `workflow.` arama. Bir çalışma akışını seçmek kayıp bir tasarıma açıyor; asla hemen çalışmıyor. Tüm adımlar mevcut bir makroya bir çalışma akışı eklendiğinde veya bir Corsair anahtarına atıfta bulunur. Örnek URL'leri, program yolları, profilleri ve kaydetmeden önce OBS sahneleri ayarlayın.

The için **Hedef penceresini bekleyin** adım bir süreç adı kabul eder (veya olmadan `.exe`), bir anahtarlık ve bir zaman kaybı 1–600,000 ms. En az bir filtre gerektirir; her iki filtre teslim edildiğinde eşleşmelidir. makro yalnızca eşleşen görülebilir pencere yanıtladıktan sonra devam eder. bir süreci paylaştığında belirli bir başlık kullanın. iptal veya zaman çıkışı sonraki adımları durdurur. pencere hazırlığı bir belge yüklendiğini veya bir dış hizmet gibi garanti etmez OBS WebSocket Bağlantılı.

Gösterim parlaklığı: **Gelişmiş ekran parlaklığı için arama**, **Düşük ekran parlaklık** veya **Gizem parlaklığı 50 yüzde** eylem kataloğunda. Karşılıklı eylemlerin parlaklığı yüzde 10 ile değişir; sabit değerler 0 ile 100 arasındadır. Desteklenen iç ekranlar WMI'yi kullanır; uyumlu dış monitörler menüsünde DDC/CI etkinleştirilmelidir. Varsayılan olarak, eylem tüm desteklenen ekranları etkiler; editörde açık bir monitör seçilebilir. Bağlantılar ve dokular desteği kısıtlayabilir.

### Genişletilmiş kontroller

Komut paleti ile açılır **Ctrl+K uygulama penceresi odaklanırken**. Arama için **Workflow Tasarımcı**, **Makro kayıtları**, **Profil Sinkronizasyonu**, veya **Anahtar Diagnostik** (iş akışı tasarımcısı seçilen çerçeve tasarısını düzenler: dosya / süreç / pencere koşulları, ileri kaydırma sayısı ve durdurma / hata işleme devam eder. kayıt defterleri yakalar ve bekler, sıradan yazdırma değil; kaydırma durdurur..

Bir parlaklık aşamasında, tüm desteklenen ekranları veya belirli bir monitör seçin. Kullanılamayan seçilmiş bir monitör, sessizce başka bir ekran değiştirmek yerine bir hata üretir. Tablo, ilgili entegrasyonları mevcut olduğunda canlı parlaklık, ses ve OBS okuma gösterir; kullanılamayan bir okuma ölçülen bir değer olarak sunulmaz.

Tile editöründe, desteklenen bir fiziksel özel anahtar atın ve hemen basın, dokunun, tutun veya çift dokunun seçin. Held katman seçicileri geçici olarak başka bir haritalama etkinleştirir ve önceki katmanı serbest bırakır. Gezi ve katmanlar Corsair SDK G anahtarları ve cihaz özel standart özel anahtarlar için geçerlidir, sıradan küresel sıcak anahtar değil. Standart anahtar yakalama F13-F24 ve klavye medya anahtarları destekler. Mülkiyetli Fn, profil ve diy kontrolü hala uyumlu bir satıcı adaptörü veya standart bir anahtarı haritalayan bir satıcı gerektirir. Raw Input anahtarını gözlemler ve normal davranışını bastırmaz Windows. Anahtar teşhisler profil, duraklama, bağlantı ve bağlama sorunlarını açıklıyor.

Profil senkronizasyonu, mevcut bulut istemcisi tarafından yönetilen bir klasör de dahil olmak üzere seçtiğiniz bir klasör üzerinden dosyaları değiştirir. İthalat öncesi önizlemeyi gözden geçirin; ithalatlar devre dışı bırakılır ve donanım özel haritalama kaldırılır. Değerlendirmeler sessiz aşırı yazıları önler ve eylem içeriği de dahil olmak üzere açık bir seçimdir. MAKRIVO bir bulut hesabı sunmaz veya bulut kimliklerini korur. Sürücü şimdi MAKRIVO kendi teal M simgesini kullanır ve durduğunda bir amber dinlenme işareti gösterir.
