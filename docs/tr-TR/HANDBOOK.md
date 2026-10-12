<!-- generated-doc locale=tr-TR source=docs/HANDBOOK.md sha256=8c6422e18d8939164f91da28603ed597f478f1a9b8bb41e14cbe4f6630bab3fc -->

> Makine destekli çeviri. Yapı, kod örnekleri ve bağlantılar kontrol edilir; bu, ana dili konuşan birinin incelemesi veya hukuki onay anlamına gelmez. Geçmiş raporlar özgün tarihlerini ve kapsamlarını korur. [Orijinal Kaynak](../HANDBOOK.md)

# MAKRIVO — Kullanım kılavuzu

Üretici: **Aivana**. Snapshot Belgeleri: **2026-10-12**. Bu el kitabı, mevcut çalışma ağaç uygulamalarını anlatıyor. deneysel entegrasyonlar ve donanım bağımlı fonksiyonlar açıkça tanımlanıyor. Tarihi raporlar tarihsel çekimlerini anlatıyor, yeni bir yapı için bir garanti değil.

## Belgeler ve Diller

Dokümanlama dil seçicisini kullanarak tam kılavuz, özellik listesi, sorun çözme, geliştirme notları, araştırma ve tarihsel test raporları 27 uygulama dillerinden herhangi birinde açılır. Oluşturulan çeviriler makine tarafından desteklenir ve yapısal kontrollere sahiptir; hepsi yerli konuşmacı incelemesine sahip değildir. Teknik tanımlayıcılar ve uygulanabilir örnekler değişmiyor. Orijinal kaynak karşılaştırma için mevcuttur. Çevirilen lisans analizi bilgilendirici ve orijinal üçüncü taraf yasal koşulları veya nihai bir EULA yerine geçmez.

## Kurulum ve İlk Başlatma

Başlangıç hedefi Windows 11 x64'tür. Her dosyayı ve dağıtım alt dizinini bir araya getirin. Başlangıç için `MAKRIVO.exe` Bir kaynak kontrolü bir yükleme değildir. Kendi içeriği olan paketler kendi .NET çalışma saatlerini içerir; çerçeve bağımlı paketler uygun .NET 10 masaüstü ve ASP.NET çalışma saatleri gerektirir. ONNX ve eşlik eden ev sahipleri ilgili özellikleri için dahil edilmelidir.

1. Başlat MAKRIVO ve Ayarlar'da arayüz dilini seçin. Otomatik takip Windows Keyboard layout ayrı bir ayar.
2. Bir profil seçin, ardından arama edilebilir eylem kataloğu aracılığıyla bir çubuk ekleyin. Bir isim ver ve eylemlerini seç.
3. Adımları inceleyin ve gerekirse kısaltma yapın. Windows veya hedef uygulamanız tarafından zaten kullanılan kombinasyonlardan kaçının.
4. Tasarımı kurtarın. Kayıp değişiklikler kurtarılmış yapılandırma değildir.
5. Bir belgeyi açmak gibi zararsız bir eylemle test edin ve uygulama geribildirimini kontrol edin.

Ana pencereyi kapatmak genellikle bildirim alanında uygulama bırakır. Tekrar tıklayın ve tekrar açın veya çıkmak için tekrar menüsü kullanın. Windows ile başlamak seçmeli ve başlangıçta engellenir. Pause kontrolü bağları askıya alır; simge bir pause işareti gösterir. UI, tray veya yapılandırılmış iptal kısaltması aracılığıyla çalışan makroları iptal edin; varsayılan olarak `Ctrl+Alt+Escape`.

## Teller, eylemler ve komut paleti

Kapaklar profillere aittir ve çok sayıda sipariş edilen adım içerebilir. Editör kaydetme/gizleme, önizleme, undo/redo ve adım yeniden düzenlemeyi destekler. Kataloğu programlar, belgeler, URL'ler, metin, klipler, kısayollar, medya, ses, pencereler, sistem eylemleri ve desteklenen entegrasyonlar için sunar. Parametreleri gerektiren bir ön ayar, bir taslak olarak açılıyor.

Palet ile açılır `Ctrl+K` O zamanlar MAKRIVO arama kaydedilen eylemler, ön ayarları, profil değiştirme ve araçları. arama anahtarları seçin; giriş etkinleştirir; `Ctrl+Enter` Desteklenen bir öğeyi düzenler; Kaçış kapatılır. Bazı kullanım için hazır komutlar hemen uygulanır, yapılandırılmış eylemler inceleme gerektirir. ayrı bir küresel palet kısaltması yapılandırılabilir; yeni yapılandırmalar kullanın `Ctrl+Shift+Space`.

Yazı ve clipboard adımları desteği `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` ve `{{user}}`. Tarih Formatı `yyyy-MM-dd`; Zaman formatı `HH:mm`. Bilinmeyen değişkenler değişmez.Clipboard okuma yalnızca talep edildiğinde gerçekleşir.Clip board eylemi otomatik olarak düzeltmeden metni yazar..

UI'den başlatılan eylemler son dış uygulamayı hedefleyebilir. Hedef kaybolursa veya Windows ön aktivasyon bloklar, manuel olarak hedef etkinleştirin ve geri çekin. Başarılı anahtar teslimat, hedefin görevini tamamladığını kanıtlamaz.

## Profiller ve çalışma alanı ayarları

Profiller bir süreç adı ve bir pencere başlığı parçası ile eşleşebilir. Her ikisi de birleştiğinde, ikisi de eşleşmelidir. Profil siparişi rekabetçi maçları çözür; varsayılan profil düşüştür. Otomatik seçimi açıkça yapabilirsiniz. Lock Profile, mevcut oturum için otomatik değişiklikleri engeller.

Açık çalışma alanı ayarları araçlar veya palet. Programlar, belgeler, web siteleri, bir ses / ekran hedefi veya bir OBS sahne seçin. Asistan çalışma alanı, toplantı, akış, araştırma veya proje profilleri hazırlar. Tasarruf yapmadan önce oluşturulan adımları kontrol edin. Bu program hazırlanan eylemleri gerçekleştirmez.

## Macros, koşullar ve kayıt

Macros, adımlarını sırada ve bekleme, tekrarlama ve iptal desteği ile gerçekleştirir. Genel tekrarlama sınırı 100 Başka bir uygulama veya sistem durumunu değiştiren eylemleri çalıştırmadan önce Önizleme'yi kullanın.

Hedef penceresi için bekleyin bir süreç, bir başlık altyapısı ve 1 ile 600,000 ms arasında bir zaman çıkışı kabul eder. En az bir filtre gerektirir. Bir görünür pencere, bir sonraki adımın devam etmesi için yanıt vermeli ve ön plana çıkmalıdır. Hazır bir pencere, bir belgenin veya hizmetin yüklemeyi tamamladığını kanıtlamaz.

Çalışma akışı editörü dosya varlıkları, süreç çalıştırma ve pencere başlığı koşulları ekler. Sahte bir durum adım atar ve daha fazla adım atar. Alternatif bir dalı ifade etme koşulunu reddetmek. Hatayı durdurmak veya hatayı devam ettirmek için kasıtlı olarak seçin; devam etmek beklenen ön koşullar olmadan sonraki adımları bırakabilir.

Aksiyon kaydedici kısa süreli kodları ve beklemeyi yakalar, sıradan yazdırma değil. Modifikasyon anahtarları ve fonksiyon anahtarlarını destekler. Kaynaklar kaydedilmesini durdurur. Listeyi inceleyin ve saklanmamış bir çerçeve taslağı olarak kabul edin, sonra açıkça kaydedin. UI tıklama kaydı aşağıda açıklanan ayrı bir araçtır.

## Keyboardlar, hareketler ve katmanlar

Klavye sayfası tespit edilen arayüzleri ve referans düzenlerini listeler. Tek bir fiziksel klavye birkaç arayüzü ortaya çıkarabilir. Doğru dil ve geometri seçin; tek bir ürün adı ISO veya ANSI düzenini tanımlamaz.

Standart küresel hotkeys tüm klavyelerde uygulanır. Raw Input Bağlantılar belirli bir cihazı ve destekli fonksiyon ve medya anahtarları dahil tarama kodunu izler.. **Raw Input sıradanı bastırmıyor Windows İkinci klavye üretilmez.** imzalanan, lisanslı ve test edilmiş bir filtre sürücüsü arka planı hala gerekli; hiçbir MAKRIVO.

Desteklenen özel anahtar bağlantıları hemen basılabilir, dokunabilir, tutabilir veya çift dokunabilir. Held-layer anahtarları geçici olarak başka bir haritalama seçin ve serbest bırakılan temel katmanı geri getirin; katman-selector anahtarlar kendi makro çalıştırmaz. 150 için 5,000 MS; iki katlı pencereler 150 için 1,000 MS. Bu haritalar sıradan küresel hotkeys farklıdır.

Özel anahtar sihirbaz, yakalanma sırasında atanan eylemleri yapmadan uyumlu anahtarları tespit eder. Diagnostikler mevcut olmayan cihazları, durdurulmuş bağlantıları, profil karışıklıkları ve çatışmaları açıklıyor. Yüzey seçilmiş düzen ve bağları gösterir; varsayılan tutma kısaltması `Ctrl+Alt+F1`.

| bütünleşme | Gereksinimler ve Sınırlamalar |
| --- | --- |
| Corsair | iCUE, SDK erişimi ve uyumlu bir model. G1-G6 ve desteklenen RGB fonksiyonları; özel Fn, tekerlek, kilit ve profil kontrolleri genellikle G anahtar API tarafından maruz kalmaz. |
| Logitech G | Orijinal x64 G/LED SDK kütüphaneleri açık onay gerektirir. G anahtarı etkinlikleri birkaç benzer fiziksel klavyeyi benzersiz bir şekilde tanımlamaz. |
| Razer | Chroma RGB ve standart giriş; gerekli olduğunda harita özelleştirme kontrolü Synapse. |
| SteelSeries | GameSense RGB ve standart giriş; GG'de harita mülkiyet kontrolü gerekli olduğunda. |
| Wooting | Analog değerler ve sınırlar için uyumlu orijinal x64 SDK; normal HID girişi aktif kalır. |
| Diğer Anahtarlar | Standart hotkeys veya destekli fiziksel tarama kodu bağları. Fn ve mülkiyet kontrolü model ve satıcı yapılandırmasına bağlıdır. |

Üretici araştırması, her listelenen cihazla uygulanmamış veya test edilmemiş belgelendirilmiş kapasiteleri tanımlar. Yeni adaptörler hala gerçek donanım kabul gerektirir. Güvenilmemiş kaynaklardan SDK ikili indirmeyin; SDK dağıtım hakları teknik uyumluluktan ayrılır.

## Ses, ekran ve OBS

Sesli Sürüşler Volume Set 0 için 100 % ve mute/unmute/toggle destekli çıkış veya mikrofon son noktaları. belirli bir son nokta seçin veya Windows varsayılan olarak kaybolan bir hedef başka bir cihazı sessizce kontrol etmek yerine bir hata üretir. yeniden bağlandıktan sonra cihazları yenilemek. son nokta kontrolü bağımsız bir uygulama karıştırıcı desteği değil.

Görüntüleme eylemleri parlaklık artırmak / azaltmak 10 yüzde puan veya bir değer ayarlamak 0 için 100. İç ekranlar desteklenen WMI arayüzlerini kullanır; dış monitörler destek gerektirir DDC/CI ve genellikle monitör menüsü ayarları. Dokular, adaptörler ve kablolar kontrolü engelleyebilir. Tüm ekranları veya bir monitör seçin. Yeniden bağlantı mevcut kimlikleri değiştirebilir; kaydedilen hedefi inceleyebilir. Kayıp okuma değeri ölçülen değerler yerine kullanılamaz olarak gösterilir.

OBS WebSocket v5 OBS araçlarda etkinleştirilmesini gerektirir. Son noktayı ayarlayın, bağlantıyı test edin ve kaydetmeden önce sahne/giriş isimlerini yükleyin. Desteklenen eylemler arasında sahne değiştirme, akış / kayıt başlangıç / durdurma / kaydetme duraklama ve giriş sessizliği yer alır. Şifreleri Windows Credential Manager'da kaydedilebilir ve konfigürasyon ihracatından hariç tutulabilir. Başarılı bir komut OBS her düşük akış etkisi kurmaz.

## Sonuç doğrulama ve uygulama geçmişi

Uygun adımlar için sonuçları kontrol etme yeteneği. Uygulama, doğrulanmamış ön görüntüleme kontrollerini, doğrulanmış etkileri, iptal ve etkilerini ayırt eder. İşlem varlığı uygulama hazırlığı değildir. Karşılıklı değişiklikler, kısaltmalar ve metin girişleri bir başarı ifadesini alamaz. Bir eylem başarısız olduğunda aktivite günlüğünü ve doğrulama geçmişini kontrol edin. Tam makro çalıştırmadan önce yapılandırma hataları düzeltilmelidir.

## UI otomatikleştirme ve tıklama kaydı

Bir UI Otomatik Adım ekleyin, işlemi seçin ve gösterilen gecikme sonrasında göstergenin altındaki hedefi yakalayın. yalnızca yakalama hiçbir şey yapmaz. seçiciyi kontrol edin ve benzersiz bir karşılaşma gerektirir. `Invoke`, `Select`, `Toggle`, `Focus`, `SetValue` ve `WaitState`. Şifre alanları, engellenmiş kontroller ve aptallı seçiciler reddedilir.

UI otomatikliği sınırlı bir ortaklık sürecinde çalışır. Hedef uygulamasının Windows erişilebilirliği desteğine ve izinlerine bağlıdır. Tıklama kaydeti desteklenen eylemleri ve durakları yakalar; genel metin girişini kaydetmez. Tasarımı kabul etmeden önce incelemek ve onaylamak. Bir dış uygulamada UI değişiklikleri seçicileri devre dışı bırakabilir. İşlem yalıtımı kısıtlamaları ve iptal, ama bir güvenlik kum kutusu değildir.

## Pluginler

Bir paket ithal etmeden önce kontrol etmek için uzantıları açın. Paket yapısı, manifesto ve hash kontrol edilir. İhracat engelli başlıyor. Açıklanan eylemleri okuyun ve izin vermeden önce yayıncılara açıkça güvenin. Plugins kullanıcı izni ile ayrı bir süreçte çalışır; bu süreç ** değil bir kum kutusu**. Sadece güvenebileceğiniz paketleri kurun.

Yönetici, önceki versiyonun ihraç edilmesini, devre dışı bırakılmasını ve geri yüklemesini destekler. Kayıp bağımlılıklar çağrıyı engeller. Örnek paketi sayım ve yerel devlet eylemleri sağlar. Desteklenen şemalar, herhangi bir JSON-Şeması özelliği değil, belgelendirilmiş bir JSON Şeması alt kümesidir. Plugin piyasası yok.

## Mobil Panel

1. Mobile Panel'i açın, yerel bir IP'yi seçin ve açıkça gösterilecek profilleri seçin. Varsayılan loopback adresi yalnızca PC'de erişilebilir; bir telefon veya tablet için uygun LAN adresini seçin.
2. Paneli başlatın ve HTTPS adresini, parmak izini ve tek kullanımlı altı rakamlı çift kodunu okuyun.
3. Aynı güvenilir ağdaki bir cihazda, adresi açın. Yerel sertifikaya güvenmeden önce güvenilir bir kanal aracılığıyla SHA-256 parmak izini karşılaştırın. Uygulama otomatik olarak güvenilir bir sertifika yüklemez.
4. Gösterilen kodu kullanın ve bir zararsız çubuk test edin. LAN erişimi için manuel Windows Firewall izni gerekebilir. Portı internetten çıkarmayın.
5. Paneli bitirdikten sonra durdurun veya uygulama aracılığıyla müşterileri iptal edin.

Özel sunucu anahtarları Windows DPAPI ile korunur; müşteri kimlikleri hash olarak kaydedilir. IP'yi değiştirmek veya sertifikayı yenilemek, yeniden güven gerektirir. Panel eylem ve doğrulama durumları gösterir, tüm canlı sensör değil. Tarayıcı tablosu ve yerel HTTPS kontrolü gerçek telefon/tablet LAN kabulünü değiştirmez.

## Toplantı Asistanı

Bu seçmeli deney asistanı varsayılan olarak engellenir. Aracı açın, açıkça etkinleştirin, bir çalışma süresi/model seçin ve sadece dikkate almak istediğiniz kaynakları ekleyin. Hedefi belirleyin, bir tasarıma sahip olun, çözülmemiş talepleri inceleyin ve sonuçları normal editöre aktarın. Hiçbir şey otomatik olarak uygulanmaz; kurtarma ve yürütme açık eylemler kalır. Otomatik runtime fallback yok.

| koşu zaman | Setuplar |
| --- | --- |
| Ollama | Ayrı yerel servis ve yerel olarak yüklü bir model `127.0.0.1:11434`. Açıklanan uzaktan / bulut modeller reddedilir. |
| LM Studio | Yerel sunucular yerel olarak yüklü bir model ve JSON şeması desteği ile `127.0.0.1:1234`. Yerel işlem için uzaktan yönlendirme yapmayın. |
| llama.cpp | Yerel `llama-server` ve JSON şeması ile uyumlu model `127.0.0.1:8080`. |
| ONNX Çalışma Süresi GenAI CPU | Güvenilir yerel GenAI metin modeli klasörü; seçin `genai_config.json`. Inference, HTTP olmadan bir ortaklık süreci kullanır. |

HTTP müşterileri yalnızca loopback kullanırlar, proxy kullanmazlar ve yönlendirmeleri takip etmezler. MAKRIVO Uyumlu bir yerel sunucunun içeride uzaktan hizmet çağırıp çağırmadığını güvenilir bir şekilde belirleyemezler. Bu versiyonda doğrulanmış yerel sunucular yapılandırılamaz. Model ağırlıkları, Python ve yerel referans sunucuları otomatik olarak indirilemez veya yüklenmez.

ONNX paketleri `tokenizer.json` ve geçerli bir sohbet şablonu gerektirir. Desteklenen şablon formları `tokenizer_config.json` bir satır `chat_template`, bir satır ile bir sözlük `default` veya ikiye ayrılmaz bir isim / varsayılan şablon listesidir. Şablon eksik veya sıfırsa `chat_template.jinja` bir düşüş olarak kullanılır. Boş, aptallı, aşırı veya yanlış yazılmış şablonlar reddedilir. Arbitrary ONNX dosyaları, entegre/görüntülü modeller ve sadece GPU paketleri uyumlu GenAI metin modelleriyle değiştirilemez. Birleşik sürüm GenAI çalıştırma süresi 0.17.1, CPU'dur. Sadece güvenilir modeller kullanın; işlem ayrımı bir kum kutusu değildir.

Asistan, seçilmiş programları tartışmadan, seçilmiş belgeleri ve web sitelerini ve açıkça talep edildiğinde mikrofonu destekler. Mesajlar, randevular, isteksiz senaryolar, ücretsiz tıklama, indirme veya kulaklık değiştirme oluşturmaz. Hedef ve kaynak isimleri seçilen yerel çalışma saatine gönderilir; kaynak yolları ve dosya içeriği ayrı olarak sağlanmaz. Kullanıcı tarafından belirlenen bir amaç kişisel verileri içerebilir. Bir yerel çalışma süresi kendi ağ davranışı kullanıcı sorumluluğundadır.

ONNX talep bütçesi 90 model yükleme dahil, 3,072 hızlı tokenler ve 1,024 üretilen tokenler, aynı zamanda model bağlamı ile sınırlıdır. Kötü şekillendirilmiş / bozulmuş JSON ve bilinmeyen kaynak kimlikleri reddedilir. Bir başvuru iptal etmek veya sona erdirmek. Bir rastgele model çalışma süresi testi, uygulanması, yararlı olmayan planlama kalitesi veya zaman tasarrufu sağlar. Yerel deneme günlükleri etkinlik tanımlayıcıları ve zamanlamaları içerir, hedef metin veya belge içeriği değildir; otomatik olarak yüklenmez.

Daha sonraki küçük Instruct-model değerlendirmesi dört Alman/İngilizce denemede sıfır geçerli planlar üretti. Yerli sonuçlar çalıştı, ancak model sıkı planlama sözleşmesi başarısız oldu. Bu olumsuz bir kalite sonucu, tavsiye edilen bir model veya kanıtlanmış yararlı bir asistan değildir. Doğru model incelemesi ve sınırları için tarihi yerel çalışma raporuna bakın.

## İthalat, ihracat, senkronizasyon ve yedekleme

Bir konfigürasyonu değiştirmeden önce ithalat tahminlerini inceleyin. İthal edilen eylemler devre dışı bırakılır ve taşınabilir senkronizasyon donanımlı bağları kaldırır. Cihazları yeniden atın ve denetimden sonra kasıtlı olarak eylemler yapın. Bir ihracat paylaşmadan önce metin / eylem içeriğini inceleyin; şifreyi hariç tutmak, kullanıcı tarafından oluşturulan tüm eylem İçeriğini hassas hale getirmez.

Profil senkronizasyonu, mevcut bulut istemcisi tarafından seçilen bir klasörde dosyaları değiştirir. MAKRIVO hiçbir bulut hesabı sağlar ve bulut kimliklerini saklamaz. Sessiz aşırı yazıları engellemek için denetimler yapılır. Aksiyon içeriği açık bir seçimdir. Bir değiştirme ithalatından önce konfigürasyonunuzu yedekleyin. Varsayılan yapılandırma `%LOCALAPPDATA%\AivanaWindowsController\settings.json`; tarihi dizin adı uyumluluk için saklanır. Uygulamayı manuel olarak kopyalamadan önce kapatın. Depolanan SDK onayları ve OS korumalı kimlikler başka bir PC'de yeniden oluşturulabilir.

## Sorun Çözümü

| Semptomlar | Ne kontrol etmek |
| --- | --- |
| Shortcut hiçbir şey yapmaz | Uygulama çalıştırma, kesintisiz bağlantılar, doğru aktif profil, kaydedilen/aktif çubuklar, kısaltma çatışmaları ve mevcut hedefler. |
| Hedef penceresi etkinleştirilemez | Manuel olarak etkinleştirin. Windows ön sınırlamalar ve ayrıcalık farklılıkları otomatikleşmeyi engelleyebilir. |
| Özel anahtar tespit edilmemiştir | Satıcı Hizmetleri / SDK / Model Desteği; Mümkünse destekli bir standart anahtar yayınlamak için özel kontroller ayarlayın. |
| İkinci klavye de normal | beklenen ile Raw Input. Üretim için özel baskı uygulanmamaktadır.. |
| Ses veya parlaklık mevcut değil | Cihazları yenilemek; seçilmiş kimlikleri, son nokta oturumlarını, WMI/DDC/CI desteğini ve doku/kablo kısıtlamalarını kontrol etmek. |
| OBS offline | WebSocket v5 etkinleştirilmiş, doğru son nokta / şifre, erişilebilir hizmet ve geçerli sahne / giriş isimleri. |
| UI otomatik eksikliği | Kompanyon ev sahibi dahil, benzersiz geçerli seçici, erişilebilir etkin hedef, destekli kontrol ve izinler. Hedefleri inceledikten sonra geçici bir zaman çıkışı geri alın. |
| Mobil Panel Kullanılamaz | LAN yerine loopback adresi, aynı ağ, sunucu çalıştırma, onaylanmış profiller, sertifika parmak izi / güven ve manuel ateş duvarı izinleri. |
| Plugin başarısız | Paket bütünlüğü, açık güven / izinli durum, bağımlılıklar, protokol uyumluluğu ve yürütme kaydı. |
| İstek başarısız | Doğru seçilmiş yerel çalışma saatleri, servis / model hazır, desteklenen JSON şeması, güvenilir uyumlu model klasörü ve talep sınırları. |
| Layout veya yazı yanlış görünüyor | Klavye geometri ile UI dilini ayırın; pencereyi yeniden düzenleyin, ekran ölçeğini kontrol edin ve doğru sayfa / dil / yapıya bildirin. |

Faydalı bir hata raporu için Windows versiyonu, uygulama oluşturma/komite, dil, ekran ölçeklenmesi, donanım modeli, minimum bir sıralama ve ilgili düzenlenmiş günlüğü içerir. Kişisel yolları, eylem içeriğini ve kimlik doğrulamalarını yayınlamadan önce kaldırın.

## İnşaat, test ve ambalaj

SDK'yı kullanın `global.json`. ROSİTORY’den:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test
./tools/publish-complete.ps1 -SelfContained
python tools/check_documentation.py
```

Windows entegrasyon testleri izole edilmiş pencereleri açabilir ve geçici olarak destekli satıcı bağlantılarını ayarlayabilir. Bu bağları taşıyan başka bir kontrolör olmadan çalıştırın. Varsayılan parlaklık/audio testleri yalnızca okunur; açık donanım yuvarlak testleri ayrıdır. Mock protokolleri, renderli ekran görüntüleri ve rastgele sonuç grafikleri gerçek donanım / model kalitesi kabul olarak temsil edilmemelidir.

Tam yayıncı eşlik eden ev sahipleri, mobil varlıkları, örnek eklenti, ONNX çalışma süresi ve mevcut bağımlılık bildirimleri bir araya getirir. Subdirektiflerini koruyun. Başarılı bir yapı, imzalanan bir yüklü veya yasal serbest bırakma onayını değildir. Kaynak ve yapıları ayrı ayrı geliştirebilir; belge snapshot karşılaştırın ve yüklü versiyonu ile taahhüt.

## Lisans, gizlilik ve serbest bırakma durumu

Önemli ticari model, satın alınan büyük versiyonun sürekli kullanımı ile tek seferlik bir satın alma; nihai fiyat, cihaz ücreti ve yükseltme koşulları gerçek satış koşullarında belirtilmelidir. Bir üretim satın alma / etkinleştirme sistemi bu kılavuz tarafından kurulmamaktadır. Günlük kontrol için bulut hesabı gerekli değildir.

Kanıtlanmış izin olmadan özel SDK binaları dağıtmayın. Her gönderilen bağımlılık için lisans ve üçüncü taraf bildirimleri saklayın, .NET ve ONNX paketleri de dahil olmak üzere. Model ağırlıkları kendi terimleri vardır. Tarihli lisans denetimi bazı yeni bağımlılıklar getirir ve bu eklemeler için bir açıklama değildir. Kendi kendine açık kaynak lisansı verilmez.

Gerçek satıcı donanımı, telefon/tablet LAN işlevi, karışık monitör/DPI geçişleri, uzun süreli kullanım, tam dil incelemesi, özel sürücü desteği ve yararlı AI planlama kalitesi ayrı kabul çalışmaları kalır. Hiçbir zaman yalnızca bir varlıktan onay almayalım.
