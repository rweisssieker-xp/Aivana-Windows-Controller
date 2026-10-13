<!-- generated-doc locale=tr-TR source=docs/HANDBOOK.md sha256=8c6422e18d8939164f91da28603ed597f478f1a9b8bb41e14cbe4f6630bab3fc -->

> Makine destekli çeviri. Yapı, kod örnekleri ve bağlantılar kontrol edilir; bu, ana dili konuşan birinin incelemesi veya hukuki onay anlamına gelmez. Geçmiş raporlar özgün tarihlerini ve kapsamlarını korur. [Orijinal Kaynak](../HANDBOOK.md)

# MAKRIVO — Kullanım kılavuzu

Üretici: **Aivana**. Belge anlık görüntüsü: **2026-10-12**. Bu kılavuz deponun geçerli çalışma ağacındaki uygulamayı açıklar. Deneysel entegrasyonlar ve donanıma bağlı işlevler açıkça belirtilir. Geçmiş raporlar, tarihli durumlarını açıklar; daha yeni bir derleme için garanti oluşturmaz.

## Belgeler ve Diller

Tam kılavuzu, özellik envanterini, sorun gidermeyi, geliştirme notlarını, araştırmaları ve geçmiş test raporlarını uygulamanın 27 dilinden birinde açmak için belge dil seçicisini kullanın. Üretilen çeviriler makine desteklidir ve yapısal denetimden geçer; hepsi ana dil konuşuru tarafından incelenmemiştir. Teknik tanımlayıcılar ve çalıştırılabilir örnekler değiştirilmez. Özgün kaynak karşılaştırma için mevcuttur. Çevrilmiş lisans analizi bilgilendiricidir; özgün üçüncü taraf hukuki koşullarının veya nihai EULA’nın yerini almaz.

## Kurulum ve İlk Başlatma

İlk hedef Windows 11 x64 platformudur. Dağıtımın tüm dosyalarını ve alt dizinlerini birlikte tutun. `MAKRIVO.exe` dosyasını başlatın. Depodan alınan kaynak kodu bir kurulum programı değildir. Bağımsız paketler kendi .NET çalışma zamanını içerir; çerçeveye bağımlı paketler uygun .NET 10 Desktop ve ASP.NET çalışma zamanlarını gerektirir. İlgili özellikler için ONNX ve yardımcı ana bilgisayar programları pakete dahil edilmelidir.

1. MAKRIVO uygulamasını başlatın ve Ayarlar’da arayüz dilini seçin. Otomatik seçeneği Windows dilini izler. Klavye düzeni ayrı bir ayardır.
2. Bir profil seçip arama yapılabilen eylem kataloğundan kutucuk ekleyin. Tanınabilir bir ad verin ve eylemlerini seçin.
3. Adımları inceleyip gerekirse kısayol atayın. Windows veya hedef uygulamanızın zaten kullandığı birleşimlerden kaçının.
4. Taslağı kaydedin. Kaydedilmemiş değişiklikler kayıtlı yapılandırmanın parçası değildir.
5. Bir belgeyi açmak gibi zararsız bir eylemle test edin ve uygulama geribildirimini kontrol edin.

Ana pencereyi kapatmak uygulamayı genellikle bildirim alanında bırakır. Yeniden açmak için bildirim alanı simgesine çift tıklayın veya çıkmak için simgenin menüsünü kullanın. Windows ile başlatma isteğe bağlıdır ve başlangıçta kapalıdır. Duraklatma denetimi atamaları askıya alır; simgede duraklatma rozeti görünür. Çalışan makroları arayüzden, bildirim alanından veya yapılandırılan iptal kısayoluyla iptal edin; varsayılan `Ctrl+Alt+Escape` şeklindedir.

## Kutucuklar, eylemler ve komut paleti

Kutucuklar profillere aittir ve sırayla yürütülen birden çok adım içerebilir. Düzenleyici kaydetme/vazgeçme, önizleme, geri alma/yineleme ve adımları yeniden sıralamayı destekler. Katalog; programlar, belgeler, URL’ler, metin, pano, kısayollar, medya, ses, pencereler, sistem eylemleri ve desteklenen entegrasyonlar için hazır ayarlar sunar. Parametre gerektiren bir hazır ayar taslak olarak açılır.

MAKRIVO odaktayken paleti `Ctrl+K` ile açın. Kayıtlı eylemleri, hazır ayarları, profil değiştirmeyi ve araçları arayın. Ok tuşları seçer; Enter etkinleştirir; `Ctrl+Enter` desteklenen öğeyi düzenler; Escape kapatır. Bazı hazır komutlar hemen çalışır, yapılandırılmış eylemler inceleme gerektirir. Ayrı bir genel palet kısayolu ayarlanabilir; yeni yapılandırmalar `Ctrl+Shift+Space` kullanır.

Metin ve pano adımları `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` ve `{{user}}` değişkenlerini destekler. Tarih biçimi `yyyy-MM-dd`, saat biçimi `HH:mm` şeklindedir. Bilinmeyen değişkenler değişmeden kalır. Pano yalnızca istendiğinde okunur. Pano eylemi metni panoya yazar, otomatik olarak yapıştırmaz.

Arayüzden başlatılan eylemler son kullanılan harici uygulamayı hedefleyebilir. Hedef kaybolursa veya Windows ön plana getirmeyi engellerse hedefi elle etkinleştirip yeniden deneyin. Tuşların başarıyla iletilmesi hedefin görevini tamamladığını kanıtlamaz.

## Profiller ve çalışma alanı ayarları

Profiller bir işlem adıyla ve pencere başlığının bir bölümüyle eşleşebilir. Her ikisi de ayarlanmışsa ikisi de eşleşmelidir. Birden fazla eşleşmede profil sırası belirleyicidir; eşleşme yoksa varsayılan profil kullanılır. Otomatik seçimi açıkça etkinleştirin. Lock Profile geçerli oturumda otomatik değişiklikleri önler.

Araçlar’dan veya paletten Çalışma Alanı Kurulumu’nu açın. Programlar, belgeler, web siteleri, ses/ekran hedefi veya OBS sahnesi seçin. Sihirbaz çalışma alanı, toplantı, yayın, araştırma veya proje profilleri hazırlar. Kaydetmeden önce oluşturulan adımları inceleyin. Kurulum hazırlanan eylemleri yürütmez.

## Macros, koşullar ve kayıt

Makrolar adımlarını sırayla yürütür; bekleme, yineleme ve iptali destekler. Genel yineleme sınırı 100’dür. Başka uygulamaya metin yazan veya sistem durumunu değiştiren eylemleri çalıştırmadan önce Önizleme’yi kullanın.

Hedef Pencereyi Bekle; işlem, başlık alt dizesi ve 1 ile 600000 ms arasında zaman aşımı kabul eder. En az bir filtre gerekir. Eşleşen görünür pencere yanıt vermeli ve sonraki adımlar devam etmeden önce ön plana gelmelidir. Pencerenin hazır olması belge veya hizmetin yüklenmesini tamamladığını kanıtlamaz.

İş akışı düzenleyicisi dosyanın var olması, işlemin çalışması ve pencere başlığı koşulları ekler. Yanlış olan koşul ilgili adımı atlar ve sonraki ek adımları da atlayabilir. Alternatif bir dalı ifade etmek için koşulu tersine çevirin. Hata durumunda durdurmayı veya devam etmeyi bilinçli seçin; devam etmek sonraki adımları beklenen ön koşullardan yoksun bırakabilir.

Eylem kaydedici kısayol tuş birleşimlerini ve beklemeleri kaydeder, normal yazı girişini kaydetmez. Değiştirici tuş içeren kısayolları ve işlev tuşlarını destekler. Escape kaydı durdurur. Listeyi inceleyip kaydedilmemiş bir kutucuk taslağı olarak aktarın, ardından açıkça kaydedin. Arayüz tıklamalarının kaydı aşağıda açıklanan ayrı bir araçtır.

## Keyboardlar, hareketler ve katmanlar

Klavye sayfası algılanan arayüzleri ve referans düzenleri listeler. Tek fiziksel klavye birden fazla arayüz sunabilir. Doğru dili ve geometriyi seçin; ürün adı tek başına ISO veya ANSI düzenini belirlemez.

Standart genel kısayol tuşları tüm klavyelerde geçerlidir. Cihaza özgü Raw Input atamaları, desteklenen işlev ve medya tuşları dahil olmak üzere belirli bir cihazı ve tarama kodunu izler. **Raw Input normal Windows girişini engellemez. Özel kullanım için ayrılmış ikinci klavye üretim kullanımına hazır değildir.** İmzalı, lisanslı ve test edilmiş bir filtre sürücüsü altyapısı hâlâ gereklidir; MAKRIVO böyle bir sürücü kurmaz.

Desteklenen özel tuş atamaları anında basma, kısa basma, basılı tutma veya çift basma kullanabilir. Basılı tutulan katman tuşları geçici olarak başka bir eşleme seçer ve bırakılınca temel katmana döner; katman seçici tuşlar kendi makrosunu çalıştırmaz. Basılı tutma eşikleri 150 ile 5000 ms, çift basma aralıkları 150 ile 1000 ms arasındadır. Bu eşlemeler normal genel kısayollardan ayrıdır.

Özel tuş sihirbazı yakalama sırasında atanmış eylemleri çalıştırmadan uyumlu tuşları algılar. Tanılama kullanılamayan cihazları, duraklatılmış atamaları, profil uyuşmazlıklarını ve çakışmaları açıklar. Ekran katmanı seçilen düzeni ve atamaları gösterir; basılı tutularak kullanılan varsayılan kısayolu `Ctrl+Alt+F1` şeklindedir.

| bütünleşme | Gereksinimler ve Sınırlamalar |
| --- | --- |
| Corsair | Çalışan iCUE, SDK erişimi ve uyumlu model gerekir. G1–G6 ve desteklenen RGB işlevleri; tescilli Fn, tekerlek, kilit ve profil denetimleri genellikle G-key API üzerinden sunulmaz. |
| Logitech G | Özgün yerel x64 G-key/LED SDK kitaplıkları açık onay gerektirir. G-key olayları birden fazla aynı model fiziksel klavyeyi benzersiz biçimde ayırt etmez. |
| Razer | Chroma RGB ve standart giriş; gerektiğinde tescilli denetimleri Synapse içinde eşleyin. |
| SteelSeries | GameSense RGB ve standart giriş; gerektiğinde tescilli denetimleri GG içinde eşleyin. |
| Wooting | Analog değerler ve eşikler için uyumlu özgün yerel x64 SDK; normal HID girişi etkin kalır. |
| Diğer klavyeler | Standart kısayollar veya desteklenen fiziksel tarama kodu atamaları. Fn ve tescilli denetimler modele ve üretici yapılandırmasına bağlıdır. |

Üretici araştırması belgelenmiş yetenekleri açıklar; listelenen her cihazla uygulanmış veya test edilmiş uyumluluk anlamına gelmez. Yeni bağdaştırıcılar gerçek donanım kabul testleri gerektirir. Güvenilmeyen kaynaklardan SDK ikili dosyası indirmeyin; SDK dağıtım hakları teknik uyumluluktan ayrıdır.

## Ses, ekran ve OBS

Ses adımları ses düzeyini 0 ile 100 yüzde arasında ayarlar ve desteklenen çıkış veya mikrofon uç noktalarının sesini kapatır/açar/değiştirir. Belirli bir uç nokta veya Windows varsayılanını seçin. Kayıtlı hedef bulunamazsa başka cihazı sessizce kontrol etmek yerine hata verilir. Yeniden bağlantıdan sonra Cihazları Yenile’yi kullanın. Uç nokta kontrolü uygulama başına bağımsız mikser desteği anlamına gelmez.

Ekran eylemleri parlaklığı 10 yüzde puanı artırır/azaltır veya 0 ile 100 arasında ayarlar. Dahili ekranlar desteklenen WMI arayüzlerini kullanır; harici monitörler DDC/CI desteği ve genellikle monitör menüsünden etkinleştirme gerektirir. Bağlantı istasyonları, adaptörler ve kablolar kontrolü engelleyebilir. Desteklenen tüm ekranları veya tek monitörü seçin. Yeniden bağlantı kullanılabilir kimlikleri değiştirebilir; kayıtlı hedefi inceleyin. Eksik okumalar ölçülen değer olarak değil, kullanılamıyor olarak gösterilir.

OBS, Araçlar menüsünde WebSocket v5 özelliğinin etkin olmasını gerektirir. Uç noktayı yapılandırın, bağlantıyı test edin ve kaydetmeden önce sahne/giriş adlarını yükleyin. Desteklenen eylemler sahne değiştirme, yayın ve kaydı başlatma/durdurma/açıp kapatma, kaydı duraklatma/sürdürme ve giriş sesini kapatmayı içerir. Parolalar Windows Credential Manager içinde saklanabilir ve yapılandırma dışa aktarımlarına dahil edilmez. Başarılı bir OBS komutu yayının sonraki tüm etkilerini doğrulamaz.

## Sonuç doğrulama ve uygulama geçmişi

Uygun adımlarda Sonucu Doğrula’yı etkinleştirin. Uygulama ön kontrolleri, doğrulanan etkileri, hataları, iptali ve doğrulanamayan etkileri ayırt eder. İşlemin varlığı uygulamanın hazır olduğu anlamına gelmez. Göreli değişikliklere, kısayollara ve metin girişine genel bir başarı onayı verilemez. Eylem başarısız olduğunda etkinlik günlüğünü ve doğrulama geçmişini inceleyin. Tüm makroyu tekrar çalıştırmadan önce yapılandırma hatalarını düzeltin.

## UI otomatikleştirme ve tıklama kaydı

UI Automation adımı ekleyin, işlemi seçin ve gösterilen gecikmeden sonra işaretçinin altındaki hedefi yakalayın. Yakalama tek başına hiçbir şey yürütmez. Seçiciyi kontrol edin ve tek eşleşme şartı arayın. Desteklenen işlemler `Invoke`, `Select`, `Toggle`, `Focus`, `SetValue` ve `WaitState` şeklindedir. Parola alanları, devre dışı denetimler ve belirsiz seçiciler reddedilir.

Arayüz otomasyonu sınırlandırılmış yardımcı işlemde çalışır. Hedef uygulamanın Windows erişilebilirlik desteğine ve izinlerine bağlıdır. Tıklama kaydedici desteklenen eylemleri ve duraklamaları kaydeder, genel metin girişini kaydetmez. Taslağı aktarmadan önce inceleyip doğrulayın. Harici uygulamanın arayüz değişiklikleri seçicileri geçersiz kılabilir. İşlem yalıtımı çökmelerin etkisini ve iptali sınırlar, ancak güvenlik sanal alanı değildir.

## Pluginler

Bir paketi içe aktarmadan önce incelemek için Uzantılar bölümünü açın. Paket yapısı, bildirim dosyası ve özet değerleri kontrol edilir. İçe aktarılan paket başlangıçta devre dışıdır. Etkinleştirmeden önce bildirilen eylemleri okuyun ve yayıncıya açıkça güven verin. Eklentiler ayrı bir işlemde kullanıcı izinlerinizle çalışır; bu işlem **bir sanal alan değildir**. Yalnızca güvendiğiniz paketleri kurun.

Yönetici dışa aktarma, devre dışı bırakma, kaldırma ve önceki sürümü geri yükleme/silme işlemlerini destekler. Eksik bağımlılıklar çağrıyı engeller. Örnek paket geri sayım ve yerel durum eylemleri sunar. Desteklenen şema belgelenmiş bir JSON-Schema alt kümesidir; tüm JSON-Schema özellikleri desteklenmez. Herkese açık bir eklenti pazarı yoktur.

## Mobil panel

1. Mobil Panel’i açın, yerel IP seçin ve sunulacak profilleri açıkça seçin. Varsayılan loopback adresine yalnızca PC’nin kendisinden erişilebilir; telefon veya tablet için uygun LAN adresini seçin.
2. Paneli başlatın; HTTPS adresini, sertifika parmak izini ve tek kullanımlık altı haneli eşleştirme kodunu okuyun.
3. Aynı güvenilir ağdaki bir cihazda, adresi açın. Yerel sertifikaya güvenmeden önce güvenilir bir kanal aracılığıyla SHA-256 parmak izini karşılaştırın. Uygulama otomatik olarak güvenilir bir sertifika yüklemez.
4. Gösterilen kodla eşleştirip zararsız bir kutucuğu test edin. LAN erişimi için elle Windows Firewall izni vermek gerekebilir. Bağlantı noktasını internete açmayın.
5. İşiniz bitince paneli durdurun veya uygulamadan eşleştirilmiş istemcilerin yetkisini iptal edin.

Özel sunucu anahtarları Windows DPAPI ile korunur; istemci kimlik bilgileri özet değerleri olarak saklanır. IP değiştirme veya sertifika yenileme yeniden güven onayı gerektirebilir. Panel eylem ve doğrulama durumlarını gösterir, olası tüm canlı sensörleri göstermez. Tarayıcı düzeni ve yerel HTTPS kontrolleri gerçek telefon/tablet LAN kabul testinin yerini almaz.

## Yerel AI toplantı asistanı

Bu isteğe bağlı deneysel asistan varsayılan olarak kapalıdır. Araçlar’dan açıp açıkça etkinleştirin, çalışma zamanı/model seçin ve yalnızca dikkate almasını istediğiniz kaynakları ekleyin. Hedefi belirtin, taslak oluşturun, çözümlenmemiş istekleri inceleyip sonucu normal düzenleyiciye aktarın. Hiçbir şey otomatik yürütülmez; kaydetme ve çalıştırma açık işlemler olarak kalır. Otomatik alternatif çalışma zamanına geçiş yoktur.

| Çalışma zamanı | Setuplar |
| --- | --- |
| Ollama | Ayrı yerel servis ve yerel olarak yüklü bir model `127.0.0.1:11434`. Açıklanan uzaktan / bulut modeller reddedilir. |
| LM Studio | `127.0.0.1:1234` adresinde yerel olarak yüklenmiş model ve JSON şeması desteğiyle yerel sunucu. Yerel işleme için uzak yönlendirmeyi etkinleştirmeyin. |
| llama.cpp | `127.0.0.1:8080` adresinde yerel `llama-server` ve JSON şeması destekli uyumlu model. |
| ONNX Çalışma Süresi GenAI CPU | Güvenilir ve uyumlu yerel GenAI metin modeli klasörü; `genai_config.json` seçin. Çıkarım HTTP kullanmadan paketteki yardımcı işlemde yapılır. |

HTTP istemcileri yalnızca loopback kullanır, proxy kullanmaz ve yönlendirmeleri izlemez. MAKRIVO, uyumlu bir yerel sunucunun kendi içinde uzak bir hizmet çağırıp çağırmadığını güvenilir biçimde belirleyemez. Bu sürümde kimlik doğrulaması gerektiren yerel sunucular yapılandırılamaz. Model ağırlıkları, Python ve yerel çıkarım sunucuları otomatik olarak indirilmez veya kurulmaz.

ONNX paketleri `tokenizer.json` ve geçerli bir sohbet şablonu gerektirir. `tokenizer_config.json` içinde desteklenen biçimler: dize türünde `chat_template`, dize türünde `default` içeren sözlük veya belirsizlik içermeyen adlandırılmış/varsayılan şablon listesidir. Şablon yoksa veya null ise `chat_template.jinja` yedek olarak kullanılır. Boş, belirsiz, aşırı büyük veya yanlış veri türündeki şablonlar reddedilir. Rastgele ONNX dosyaları, gömme/görüntü modelleri ve yalnızca GPU paketleri, uyumlu GenAI metin modellerinin yerine kullanılamaz. Paketle gelen GenAI çalışma zamanı sürümü 0.17.1, CPU’dur. Yalnızca güvenilir modeller kullanın; işlemleri ayırmak bir sanal alan oluşturmaz.

Asistan seçilen programları bağımsız değişken olmadan, seçilen belgeleri ve web sitelerini ve açıkça istendiğinde mikrofonu sessize almayı destekler. Mesaj, randevu, rastgele betik, serbest tıklama, indirme veya kulaklık değiştirme oluşturmaz. Hedef ve kaynak adları seçilen yerel çalışma zamanına gönderilir; kaynak yolları ve dosya içerikleri ayrı olarak verilmez. Kullanıcının yazdığı hedefin kendisi kişisel veri içerebilir. Yerel çalışma zamanının kendi ağ davranışı kullanıcının sorumluluğundadır.

ONNX istek sınırı model yükleme dahil 90 saniye, en fazla 3072 istem tokenı ve 1024 üretilen tokendır; modelin bağlam sınırı da geçerlidir. Hatalı/kesilmiş JSON ve bilinmeyen kaynak kimlikleri reddedilir. İsteği bitirmek için iptal edin veya kapatın. Rastgele modelle çalışma zamanı testi yürütmenin çalıştığını gösterir; yararlı planlama kalitesini veya zaman tasarrufunu kanıtlamaz. Yerel deney günlükleri olay kimliklerini ve süreleri içerir, hedef metnini veya belge içeriğini içermez; otomatik olarak yüklenmez.

Daha sonraki küçük Instruct modeli değerlendirmesi dört Almanca/İngilizce denemede sıfır geçerli plan üretti. Yerel çıkarım çalıştı ancak model katı planlama sözleşmesini karşılayamadı. Bu, olumsuz kalite sonucudur; önerilen model veya yararı kanıtlanmış asistan değildir. Tam model revizyonu ve sınırlar için tarihli yerel çalışma zamanı test raporuna bakın.

## İthalat, ihracat, senkronizasyon ve yedekleme

Yapılandırmayı değiştirmeden önce içe aktarma önizlemelerini inceleyin. İçe aktarılan eylemler başlangıçta kapalıdır; taşınabilir eşitleme donanıma özgü atamaları kaldırır. İncelemeden sonra cihazları yeniden atayın ve eylemleri bilinçli olarak etkinleştirin. Dışa aktarımı paylaşmadan önce metin/eylem içeriklerini inceleyin; parolaları hariç tutmak kullanıcının oluşturduğu tüm eylem içeriklerini hassas olmaktan çıkarmaz.

Profil eşitleme seçilen klasörde dosya alışverişi yapar; klasör isteğe bağlı olarak mevcut bulut istemcinizce yönetilebilir. MAKRIVO bulut hesabı sağlamaz ve bulut kimlik bilgilerini saklamaz. Revizyonlar sessiz üzerine yazmayı önler. Eylem içeriğini dahil etmek açık bir seçimdir. Değiştiren içe aktarmadan önce yapılandırmayı yedekleyin. Varsayılan yapılandırma `%LOCALAPPDATA%\AivanaWindowsController\settings.json` konumundadır; eski dizin adı uyumluluk için korunur. Elle kopyalamadan/geri yüklemeden önce uygulamayı kapatın. Saklanan SDK onaylarının ve işletim sistemi tarafından korunan kimlik bilgilerinin başka bir PC’de yeniden oluşturulması gerekebilir.

## Sorun Çözümü

| Semptomlar | Ne kontrol etmek |
| --- | --- |
| Shortcut hiçbir şey yapmaz | Uygulama çalışıyor olmalı; atamalar duraklatılmamış, etkin profil doğru, kutucuk kaydedilmiş/etkin ve hedef kullanılabilir olmalı. Kısayol çakışmalarını kontrol edin. |
| Hedef penceresi etkinleştirilemez | Elle etkinleştirin. Windows ön plan kısıtlamaları ve yetki farkları otomasyonu engelleyebilir. |
| Özel anahtar tespit edilmemiştir | Satıcı Hizmetleri / SDK / Model Desteği; Mümkünse destekli bir standart anahtar yayınlamak için özel kontroller ayarlayın. |
| İkinci klavye de normal yazı giriyor | Raw Input ile beklenen davranıştır. Özel kullanım için normal girişi engelleme üretim için uygulanmamıştır. |
| Ses veya parlaklık kullanılamıyor | Cihazları yenileyin; seçilen kimliği, uç nokta oturumlarını, WMI/DDC/CI desteğini ve bağlantı istasyonu/kablo kısıtlamalarını inceleyin. |
| OBS offline | WebSocket v5 etkinleştirilmiş, doğru son nokta / şifre, erişilebilir hizmet ve geçerli sahne / giriş isimleri. |
| UI otomatik eksikliği | Yardımcı ana bilgisayar programı dahil, seçici geçerli ve tek eşleşmeli, hedef erişilebilir ve etkin, denetim destekleniyor ve izinler uygun olmalı. Geçici zaman aşımında yalnızca hedefi inceledikten sonra yeniden deneyin. |
| Mobil panele erişilemiyor | Loopback yerine LAN adresi, aynı ağ, çalışan sunucu, onaylanmış profiller, sertifika parmak izi/güveni ve elle verilen güvenlik duvarı izinleri. |
| Plugin başarısız | Paket bütünlüğü, açık güven / izinli durum, bağımlılıklar, protokol uyumluluğu ve yürütme kaydı. |
| İstek başarısız | Doğru seçilmiş yerel çalışma saatleri, servis / model hazır, desteklenen JSON şeması, güvenilir uyumlu model klasörü ve talep sınırları. |
| Düzen veya metin yanlış görünüyor | Arayüz dilini klavye geometrisinden ayrı değerlendirin; pencereyi yeniden boyutlandırın, ekran ölçeğini kontrol edin ve tam sayfayı/dili/derlemeyi bildirin. |

Yararlı hata raporuna Windows sürümü, uygulama derlemesi/commit’i, dil, ekran ölçeği, donanım modeli, en kısa yeniden üretme adımları ve hassas bilgileri çıkarılmış ilgili günlük dahil edin. Yayımlamadan önce kişisel yolları, eylem içeriklerini ve kimlik bilgilerini kaldırın.

## İnşaat, test ve ambalaj

`global.json` içinde sabitlenen SDK sürümünü kullanın. Depo kök dizininden:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test
./tools/publish-complete.ps1 -SelfContained
python tools/check_documentation.py
```

Windows entegrasyon testleri yalıtılmış pencereler açabilir ve desteklenen üretici atamalarını geçici olarak yapılandırıp serbest bırakabilir. Bunları, atamaları sahiplenen başka denetleyici yokken çalıştırın. Varsayılan parlaklık/ses kontrolleri salt okunurdur; açık donanım değiştirme ve geri yükleme testleri ayrıdır. Sahte protokoller, oluşturulan ekran görüntüleri ve rastgele çıkarım grafikleri gerçek donanım/model kalitesi kabulü olarak sunulmamalıdır.

Tam yayımlama aracı yardımcı ana bilgisayar programlarını, mobil varlıkları, örnek eklentiyi, ONNX çalışma zamanını ve mevcut bağımlılık bildirimlerini bir araya getirir. Alt dizinleri koruyun. Başarılı derleme imzalı kurulum programı veya hukuki sürüm onayı değildir. Kaynak ve derlemeler ayrı gelişebilir; belge anlık görüntüsünü ve commit’i kurulu sürümle karşılaştırın.

## Lisans, gizlilik ve serbest bırakma durumu

Amaçlanan ticari model satın alınan ana sürümün süresiz kullanımı için tek seferlik satın almadır; nihai fiyat, izin verilen cihaz sayısı ve yükseltme koşulları gerçek satış şartlarında belirlenmelidir. Bu kılavuz üretimde bir satın alma/etkinleştirme sisteminin varlığını ortaya koymaz. Günlük kontrol için bulut hesabı gerekli değildir.

Kanıtlanmış izin olmadan tescilli SDK ikili dosyalarını dağıtmayın. .NET ve ONNX paketleri dahil dağıtılan her bağımlılığın lisans ve üçüncü taraf bildirimlerini koruyun. Model ağırlıklarının kendi koşulları vardır. Tarihli lisans denetimi bazı yeni bağımlılıklardan önce yapılmıştır ve bu eklemeler için izin sayılmaz. Deponun herkese açık olması tek başına genel bir açık kaynak lisansı vermez.

Gerçek üretici donanımı, telefon/tablet LAN kullanımı, karma monitör/DPI geçişleri, uzun süreli kullanım, tam dil incelemesi, özel klavye sürücüsü desteği ve yararlı AI planlama kalitesi ayrı kabul çalışmaları olarak kalır. Bir özelliğin bulunmasından tek başına onay sonucu çıkarmayın.
