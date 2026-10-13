<!-- generated-doc locale=tr-TR source=README.md sha256=eb6f3fe7810ed3e10793e8938e908e151a84c4afbea2f22cba2656bfe4e8dfc3 -->

> Makine destekli çeviri. Yapı, kod örnekleri ve bağlantılar kontrol edilir; bu, ana dili konuşan birinin incelemesi veya hukuki onay anlamına gelmez. Geçmiş raporlar özgün tarihlerini ve kapsamlarını korur. [Orijinal Kaynak](../../README.md)

# MAKRIVO

[Tüm 27 dillerde tam belge](README.md) · [Tam Kullanıcı Kitabı](HANDBOOK.md)

**tarafından Aivana.** [Mevcut özellikler listesi ve serbest bırakma sınırları](FEATURES.md) · [Uzantı Test Raporu](testing/2026-10-12-six-extensions.md) · [Yerel Asistan Prototype](testing/2026-10-12-local-meeting-assistant.md)

**İngilizce ( US ))** | [Deutsch](../de-DE/REFERENCE.md) | [Français](../fr-FR/REFERENCE.md) | [Italiano](../it-IT/REFERENCE.md)

Stream Deck tarzı kutucuklar, profiller, makrolar ve klavye algılama sunan yerel Windows kısayol denetleyicisi. Arayüz 27 dili destekler. Günlük kullanımda hesap veya bulut bağlantısı gerekmez.

[Dokümanlık](README.md) · [Kontrolü](VERIFICATION.md) · [Özellikler test raporu](testing/2026-10-09-usp-release.md)

## Başlamak için

Yayımlama işleminden sonra uygulama `artifacts/win-x64/MAKRIVO.exe` konumundadır. Çıktı klasörünün tamamını birlikte tutun; başka yere kopyalayabilirsiniz. İlk hedef Windows 11 x64 platformudur. Derleme çıktıları Git’e dahil edilmez; kaynak kodu kopyası hazır yürütülebilir dosya içermez.

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

1. Bir profil seçin ve eylem kutucuğu oluşturun.
2. Adını, simgesini, rengini ve klavye kısayolunu ayarlayın. Ctrl/Alt/Shift birleşimleri yazarken yanlışlıkla etkinleştirmeyi azaltır.
3. Bir veya daha fazla adım ekleyin: uygulama, dosya, web sitesi, metin, kısayol, medya, ses düzeyi, profil değiştirme veya gecikme.
4. Kaydedin. Değişiklikler kaydedilene kadar taslaktır; vazgeçme kayıtlı sürümü geri getirir. Kutucuğun Play düğmesi ve kısayolu aynı yürütme yolunu kullanır.
5. Kutucukları ve makro adımlarını sürükleyip bırakarak veya ok düğmeleriyle sıralayın. Şablonlar kurulumu kolaylaştırır. Makrolar en fazla 100 kez yinelenebilir. Arayüzden, bildirim alanı menüsünden veya varsayılan Ctrl+Alt+Escape ile iptal edin.

Uygulamaya özgü profiller tam yol olmadan işlem adını kullanır. Eşit eşleşmelerde profil sırası belirleyicidir; eşleşme yoksa varsayılan profil kullanılır. Otomatik profil seçimi Ayarlar’da etkinleştirilmelidir.

Ana pencereyi kapatmak uygulamayı varsayılan olarak bildirim alanında tutar. Yeniden açmak için simgesine çift tıklayın; çıkmak için simge menüsünü kullanın. Windows ile başlatma isteğe bağlı ve varsayılan olarak kapalıdır. Kısayollar merkezi olarak duraklatılabilir.

## eylemler ve komut paleti

Eylem Ekle, sekiz kategoride 90 hazır ayar içeren aranabilir kataloğu açar. Her ayarın açıklaması ve önizlemesi vardır. Makro düzenleyici adım eklemek veya değiştirmek için aynı kataloğu kullanır. Bu düzenleme akışında hazır ayar seçmek yürütmeden taslak oluşturur.

Komut paleti başlıkta veya uygulamanın içindeki Ctrl+K ile açılır. Profiller, katalog öncesi setler, profil değiştirme, navigasyon, duraklama/resume, makro iptal ve ithalat/eksport üzerinden kaydedilen eylemleri arar. Filtre kaydedilen eylemler, katalog fonksiyonları veya uygulama komutları. Arrow anahtarları seçin, Enter etkinleştirir, Ctrl+Enter bir düzenlenebilir giriş düzenler ve Escape kapatılır. Favori ilk olarak ortaya çıkıyor. Kullanım için hazır katalog komutları hemen çalıştırılabilir; bir tasarıma açılmasını gerektiren ön ayarlar. Ayarlar, küresel bir palet kısaltması tanımlayabilir. Yeni yapılandırmalar Ctrl+Shift+Space kullanır; mevcut yapılandırmalar otomatik olarak yeni bir küresel kısaltma almaz.

Ses eylemleri düzeyi 0 ile 100 yüzde arasında hassas ayarlar ve çıkış/mikrofon cihazlarını sessize alır, sesini açar veya sessizliği değiştirir. Belirli bir uç nokta seçin ya da Windows varsayılanı için boş bırakın. Kayıtlı uç nokta kullanılamıyorsa başka cihazı kontrol etmek yerine hata verilir. Cihazları Yenile bağlı cihazları yeniden yükler.

OBS Studio WebSocket v5 kullanır: kaydı başlatma/durdurma/değiştirme/duraklatma/sürdürme; yayını başlatma/durdurma/değiştirme; sahne değiştirme; giriş sessizliğini değiştirme. OBS Araçlar altında WebSocket sunucusunu açın ve düzenleyicide uç noktasını ayarlayın. Bağlantıyı Kontrol Et ve Adları Yükle, kayıt veya yayın başlatmadan OBS sürümünü, sahneleri ve girişleri okur. Adları listeden seçin veya elle girin. Parolalar Windows Credential Manager içinde saklanabilir ve dışa aktarılan yapılandırmalardan çıkarılır. Kullanılamayan OBS bağlantıları hata bildirir.

Profiller bir süreç adı ve bir pencere başlığı parçası ile eşleşebilir. Bu iki koşul da eşleşmelidir. Lock Profile oturum için otomatik olarak geçiş yapmayı engeller. Foreground'dan çekim koşulları son odaklanmış dış uygulama kullanır.

Metin ve pano eylemleri `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` ve `{{user}}` destekler. Tarihler `yyyy-MM-dd`, saatler `HH:mm` biçimindedir. Bilinmeyen yer tutucular değişmez. `{{profile}}` başlatılan eylemin profilini, `{{process}}` harici uygulama bağlamını belirtir. Pano içeriği yalnızca ilgili yer tutucu kullanıldığında okunur. Pano eylemi metni yazar ancak otomatik yapıştırmaz.

Pencere eylemleri pencereyi küçültür, büyütür, geri yükler veya monitörünün çalışma alanının sol/sağ tarafına yerleştirir. Sistem eylemi Windows oturumunu kilitler. Bu eylemler yalnızca açıkça çağrıldığında çalışır.

Önizleme adımları, yinelemeleri ve değişkenleri yürütmeden gösterir. Geri Al ve Yinele en fazla 100 taslak durumunu tutar; metin kutuları yerleşik Ctrl+Z düzenlemesini korur. Düzenleyicide veya kutucuk menüsünde favorileri işaretleyip panoda filtreleyin. Yürütme sayıları yalnızca geçerli oturum için geçerlidir.

Arayüzden başlatılan metin, kısayol ve pencere eylemleri son harici hedef pencereyi etkinleştirir. Palet bu hedef bağlamını gösterir. Hedef kapanmışsa veya Windows etkinleştirmeyi reddederse yürütme mesajla durur.

## Keyboard Yardım ve Feedback

Özel Tuş Sihirbazı Klavye sayfasında ve palettedir. iCUE tarafından bildirilen G tuşlarını listeler. Tuş Basımını Algıla atanan eylemleri yürütmeden geçici yakalama yapar. Normal atamalar algılama, iptal veya kapatma sonrası geri gelir. Katalog işlevi ve seçilen tuş kaydedilmemiş taslak olarak açılır; yinelenen atamalar kontrol edilir.

Klavye ekran katmanı seçilen referans düzeni ve etkin atamaları gösterir. Ctrl+Alt+F1 basılı tutun veya paletten ya da Klavye sayfasından açıp kapatın. Odağı almaz ve fare tıklamalarını geçirir. Görünürken profil ve atama değişiklikleri güncellenir; normal Windows tuş basımları vurgulanır. Üreticiye özgü denetimler eşdeğer Windows tuş sinyali vermeyebilir.

Yürütme geri bildirimi odağı almadan veya girilen metni göstermeden beklemeyi, adım/yineleme ilerlemesini, tamamlanmayı, iptali veya hatayı gösterir. Komutlar Yürütüldü, komut yürütmesini doğrular; üçüncü taraf uygulamadaki bağımsız sorgulanmış canlı durumu doğrulamaz. Hata ayrıntıları etkinlik günlüğündedir.

## Üretici ve Corsair

Corsair G1–G6 resmi iCUE SDK ile doğrudan atanabilir. Eylem düzenleyicide cihaz ve tuş seçip kaydedin veya K100 klavye görünümünden taslak hazırlayın. Atamalar seçilen cihaz ve etkin profil için geçerlidir; duraklatma bunları da kapatır. iCUE çalışmalı ve erişime izin vermelidir. Fn, iCUE tekerleği, profil ve kilit denetimleri bu G-key API ile eşlenemez.

Cihazlar sayfası Windows klavye arayüzlerini ve düzenli olarak ve cihazların bağlandığında yeniliyor. Bir fiziksel klavye çok sayıda HID arayüzüne maruz kalabilir. Standart küresel hotkeys üreticiler arasında çalışır.

Corsair algılama mevcut cihaz bilgilerini kullanır. RGB kontrolü uyumlu donanım, çalışan iCUE ve resmi iCUE SDK gerektirir; uygulama gerçek kullanılabilirliği bildirir. Diğer üreticiler şu anda algılama ve standart eylemler alır. Corsair G-key entegrasyonu dışındaki tescilli denetimler, Windows bunları normal giriş olarak aldığında çalışır. Uygulama klavye sürücüsü kurmaz.

## Diller

Ayarlar 27 dil sunar: Almanca, İngilizce, Fransızca, İspanyolca, İtalyanca, Portekizce, Felemenkçe, Lehçe, Çekçe, Slovakça, Macarca, Rumence, Bulgarca, Yunanca, Danca, İsveççe, Norveççe, Fince, Estonca, Letonca, Litvanca, Slovence, Hırvatça, Ukraynaca, Rusça, Türkçe ve Latin harfli Sırpça. Otomatik seçeneği Windows görüntüleme dilini izler. Çeviriler yerel olarak pakete dahildir. Düzenler, kısayollar ve kullanıcının oluşturduğu adlar arayüz dilinden bağımsızdır.

Tüm kayıt defterleri 27 dilinde mevcuttur. [Dil Endeksi](README.md)Bu yüzden Çeviriler, orijinal kaynak bağlantıları ve yapısal kontroller ile makine tarafından desteklenir; yerli konuşmacı incelemesi ayrıdır. Uygulama ayrıca bölgesel kodları `en-US`, `de-DE`, `fr-FR` ve `it-IT` kabul eder ve bunların sayısı biçimlerini kullanır. Mevcut dil kodları uyumludur.

## Layouts ve Üretici Araştırmaları

Klavye görünümü tam boy ve sayısal tuş takımı olmayan ISO-DE/ANSI-US referansları ile Corsair K100 RGB düzenlerini sunar. Geometri konumları, geniş/uzun tuşları ve ISO Enter’ı temsil eder. Standart düzenler genel referanstır. K100 düzenleri milimetre hassasiyeti iddiası olmadan belgelenmiş denetimleri izler. Doğru dil/geometri varyantını seçin; model adı veya USB VID/PID belirli varyantı kanıtlamaz.

Eşleme fiziksel tarama kodlarını etkin Windows giriş düzeniyle çözer. İş akışı taslak hazırlar ve kaydetme/çakışma denetimlerini korur. Standart atamalar tek bağlı klavyeye yalıtılmış değildir; genel Windows kısayollarıdır. Corsair G1–G6 cihaza özgü iCUE entegrasyonu kullanır. Diğer tescilli denetimler bağdaştırıcı veya üreticinin yapılandırdığı kısayollar gerektirir. Ana Enter ile sayısal tuş takımı Enter genel kısayollarda aynı sanal tuş kimliğini paylaşır.

The için [Üretici Araştırmaları](research/tastaturhersteller-sonderfunktionen.md) ve [CSV](research/tastaturhersteller-sonderfunktionen.csv) 74 marka / üretici grupları, konfigüratörleri, giriş SDK'ları ve aydınlatma API'leri ayırt eder. [Layout Kanıtı](research/keyboard-layout-evidence.md) Modeller, modeller ve modeller arasındaki fark. Araştırma kapsamı, donanım desteğinin uygulanması anlamına gelmez.

## veri

Yapılandırma yerel uygulama verilerinde saklanır. İçe/dışa aktarma JSON kullanır. İçe aktarma önizleme sunar, onaydan sonra geçerli yapılandırmayı değiştirir ve içe aktarılan atamaları elle etkinleştirilene kadar kapatır. Metin eylemi içeriği tuş kaydı olarak toplanmaz. Credential Manager parolaları dışa aktarılan profillerden ayrıdır.

## Kontrolü

```powershell
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
```

Windows kontrolleri donanımı inceleyebilir ve desteklenen iCUE atamalarını geçici yapılandırıp serbest bırakabilir. Bu atamaları kullanan başka denetleyici olmadan çalıştırın. Ses uç noktası kontrolleri canlı ses düzeyini veya sessizlik ayarlarını değiştirmez.

Yerel giriş Windows yetkilerini aşamaz. Normal başlatılan denetleyici yönetici uygulamasına giriş gönderemeyebilir. Ayrılmış veya kullanımda olan kısayollar çakışma olarak görünür.

The için [Belgelendirme Index](README.md) Tarihi tasarımlar, planlar, araştırma ve tarihli test raporları. Tarihi planlar kaydedilen kapsamını, mevcut bir özellik desteği garantisi değil anlatır.

### Çalışma akışı şablonları ve hedef penceresi bekliyor

Katalog altı düzenlenebilir iş akışı içerir: çalışma alanı, toplantı, yayın, araştırma, proje değiştirme ve metin parçası. Katalogda veya paletten `workflow.` arayın. İş akışı seçmek kaydedilmemiş taslak açar; hiçbir zaman hemen çalıştırmaz. Mevcut makroya akış eklendiğinde veya Corsair tuşuna atandığında tüm adımlar korunur. Kaydetmeden önce örnek URL’leri, program yollarını, profilleri ve OBS sahnelerini yapılandırın.

**Hedef pencereyi bekle** adımı işlem adı (`.exe` ile veya olmadan), büyük/küçük harfe duyarsız başlık alt dizesi ve 1–600000 ms zaman aşımı kabul eder. En az bir filtre gerekir; ikisi verilmişse ikisi de eşleşmelidir. Makro yalnızca eşleşen görünür pencere yanıt verip ön plana geldiğinde sürer. Birden fazla pencere aynı işlemi paylaşıyorsa belirli başlık kullanın. İptal veya zaman aşımı sonraki adımları durdurur. Pencerenin hazır olması belgenin yüklendiğini veya OBS WebSocket gibi harici hizmetin bağlı olduğunu garanti etmez.

Ekran parlaklığı: katalogda **Ekran parlaklığını artır**, **Ekran parlaklığını azalt** veya **Ekran parlaklığı yüzde 50** arayın. Göreli eylemler parlaklığı 10 yüzde puanı değiştirir; sabit değerler 0 ile 100 arasındadır. Desteklenen dahili ekranlar WMI kullanır; uyumlu harici monitörlerin menüsünde DDC/CI açık olmalıdır. Varsayılan olarak tüm desteklenen ekranlar etkilenir; düzenleyicide belirli monitör seçilebilir. Bağlantılar ve bağlantı istasyonları desteği kısıtlayabilir.

### Genişletilmiş kontroller

Paleti **uygulama penceresi odaktayken Ctrl+K** ile açın. **İş akışı tasarımcısı**, **Makro kaydet**, **Profilleri eşitle** veya **Tuş tanılama** arayın (ifadeler seçilen dile bağlıdır). Tasarımcı seçilen kutucuk taslağını düzenler: dosya/işlem/pencere koşulları, ileri atlama sayıları ve hatada durma/devam etme. Kaydedici kısayol birleşimlerini ve beklemeleri yakalar, normal yazıyı kaydetmez; Escape kaydı durdurur. Aktarmadan önce sonucu inceleyin.

Parlaklık adımında desteklenen tüm ekranları veya belirli monitörü seçin. Seçilen monitör kullanılamıyorsa başka ekranı sessizce değiştirmek yerine hata verilir. Pano ilgili entegrasyonlar varsa canlı parlaklık, ses ve OBS okumalarını gösterir; kullanılamayan okuma ölçülmüş değer olarak sunulmaz.

Kutucuk düzenleyicide desteklenen fiziksel özel tuş atayın ve anında basma, kısa basma, basılı tutma veya çift basma seçin. Basılı katman seçicileri geçici başka eşlemeyi etkinleştirir ve bırakılınca önceki katmanı geri getirir. Hareketler ve katmanlar Corsair SDK G tuşlarına ve cihaza özgü standart özel tuşlara uygulanır, normal genel kısayollara uygulanmaz. Standart tuş yakalama F13–F24 ve klavye medya tuşlarını destekler. Tescilli Fn, profil ve kadran denetimleri uyumlu üretici bağdaştırıcısı veya standart tuşa üretici eşlemesi gerektirir. Raw Input tuşu gözlemler, normal Windows davranışını engellemez. Tuş tanılama profil, duraklama, bağlantı ve atama sorunlarını açıklar.

Profil eşitleme seçtiğiniz klasör üzerinden dosya alışverişi yapar; mevcut bulut istemcinizin yönettiği klasör de kullanılabilir. İçe aktarmadan önce önizlemeyi inceleyin; içe aktarımlar kapalı başlar ve donanıma özgü eşlemeleri kaldırır. Revizyonlar sessiz üzerine yazmayı önler; eylem içeriklerini dahil etmek açık seçimdir. MAKRIVO bulut hesabı sağlamaz veya bulut kimlik bilgilerini saklamaz. Bildirim alanı artık MAKRIVO’nun kendi turkuaz M simgesini kullanır ve duraklatıldığında kehribar duraklatma rozeti gösterir.
