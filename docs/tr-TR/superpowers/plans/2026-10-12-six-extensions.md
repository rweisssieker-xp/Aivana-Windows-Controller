<!-- generated-doc locale=tr-TR source=docs/superpowers/plans/2026-10-12-six-extensions.md sha256=0ac83f2f795ba08551082caee7c775af015828dd39e200dd9857aab504cdba01 -->

> Makine destekli çeviri. Yapı, kod örnekleri ve bağlantılar kontrol edilir; bu, ana dili konuşan birinin incelemesi veya hukuki onay anlamına gelmez. Geçmiş raporlar özgün tarihlerini ve kapsamlarını korur. [Orijinal Kaynak](../../../superpowers/plans/2026-10-12-six-extensions.md)

# MAKRIVO Genişletme Uygulama Planı

> **Güvenlik görevlileri için: ** İSTİYOR SUB YÖNETMELİĞİ: Süper Güçler kullanımı: Yerli uygulama planları veya süper Güçleri:Süper Güçlerin geliştirilmesi: Kullanıcı delegasyonu seçtikten sonra. Kontrol kutuları ile adımları izleyin.

**Hedef: ** altı entegre uzantısı ve kapasite tabanlı çok üretici klavye desteği sağlar.

**Mimarlık:** çekirdekleri koruyun / Windows / WPF Ayrılık. kapalı doğrulanmış IPC ile izole eklenti ve mobil servis süreçleri tanıtmak; tüm uygulama mevcut kontrolör ve MacroRunner kuve üzerinden gidiyor. donanım adaptörleri tespit edilebilir kapasiteleri yerine marka kapsamlı destek.

**Teknoloji Stack:** C ve .NET 10, WPF, Windows UI Otomatik, Win32 Raw Input, Satıcı SDK'ları, ASP.NET Core HTTPS/WebSocket, Yorum Yapılmış.Json.

**SPEC:** `docs/superpowers/specs/2026-10-12-six-extensions-design.md` (Kullanıcı tarafından onaylandı).

## küresel sınırlamalar

- Mevcut yapılandırma değişmez yüklemek zorundadır; devam eden enum değerlerini yeniden düzenlemek yerine eylem türleri eklemek.
- Mevcut profilleri, G1/G2 görevleri, yerel işletmeleri ve hesapsız kullanımları koruyun.
- Raw Input Yalnızca gözlem sağlar. Eksklusif cihaz başına kaldırma gerçek bir klavye filtre arka plan gerektirir.
- İthalat edilebilir eklentiler otomatik olarak çalıştırılmamalıdır.
- Mobil servis için varsayılan olarak devre dışı bırakın ve yapılandırılmış arayüzleri sınırlayın; şikayetçi doğrulama kaybı yoktur.
- Kısa / metin teslimatı, hedef uygulamanın komutu kabul ettiğini kanıtlamaz.
- Yüklü uygulamayı değiştirmeyin, sürücüleri yüklemeyin, gerçek klavye haritalamalarını değiştirin veya bir tesadüf testi olarak ağ erişimi açın.
- İlgili olmayan değişiklikleri koruyun. Mevcut paylaşımları bu özelliğin bir parçası olarak taahhüt etmeyin. Sadece bağımsız olarak incelenebilir özellikleri arama eğer emirler istenir.

## inceleme odak

1. Aynı klavye modellerinin çoğaltılması: İkiz yeniden bağlantı yeniden seçilmelidir (Tasks 10–12).
2. UIA / sağlayıcı / eklenti / ağ bekleme sırasında iptal: iptal sonrası iptal edilmemiş eylemler (Task 1, 4, 6, 8).
3. Sihirbaz veya paket öncesi açıldığında kullanıcı düzenlemeleri: revizyon yanlışlık sessiz aşırı yazılmasını önlemek gerekir (Tasks 3, 7).
4. Dil ve fiziksel düzen farklılık gösterir: seçiciler ve tarama kodu bağlantıları yerel görüntüleme isimlerine bağlı olmamalıdır (Tasks 4, 10).
5. Network retry ve plugin yeniden başlatma: komutlar sessizce iki kez çalıştırılmamalıdır (Tasks 6, 8).

## Ortak sözleşmeler ve bağımlılık emri

Görevler 1–3 Yürütme ve kurulum. Görevler 4–5 Yapımcılar - Görevler 6–7 Plugins - Görevler 8–9 Mobil erişim - Görevler 10–12 Keyboard adaptörleri ve filtre entegre etmek. 13 Tam ürünü doğrulamaktadır.

Her görev başarısızlık test → minimum uygulama → geçme test döngüsünü takip eder. Bir görev yalnızca başvuru ve kontrolü geçerken tamamlanır. Scaffolds, mock adaptörler ve belgeler tek başına bir teslimatçıyı tatmin etmez.

### Görev 1: Tipik doğrulama ve uyumlu dayanıklılık

**Dosyaları:** Yaratmak `src/Aivana.Core/ActionVerification.cs`, `tests/Aivana.Tests/VerificationChecks.cs`; değiştirme `src/Aivana.Core/Models.cs`, `src/Aivana.Core/MacroRunner.cs`, `src/Aivana.Core/ConfigurationValidator.cs`, `tests/Aivana.Tests/Program.cs`.

**Interferi:** Üretici `VerificationState { Requested, Completed, Verified, Failed, TimedOut, Cancelled, Unverifiable }`, `ActionVerificationResult(VerificationState State, string Code, string Message)`, `IActionVerificationProvider.PreflightAsync(ActionStep, CancellationToken)` ve `VerifyAsync(ActionStep, CancellationToken)`, İkisi de geri dönüyor `Task<ActionVerificationResult>`. Seçmeli serializasyon `VerificationOptions` Bayrak ve Timeout ile (100–60000 ms), varsayılan olarak devre dışı bırakılmıştır. çalışma kimliği, tile kimliği ve adım endeksi dahil olmak üzere adım başına yazılan sonuçları eylem ücretleri olmadan yayınlayın.

- [ ] Yazma kontrolleri: miras JSON mevcut eylem sayısal değerleri tutar; eksik seçenekler doğrulama engellenir; geçersiz zaman çıkışı doğrulama başarısız olur; başarısız postcondition asla doğrulanmış başarıyı üretmez; desteklenmemiş executor Unverifable; iptal doğrulama durdurur ve sonraki adımlar.
- [ ] Run `dotnet run --project tests/Aivana.Tests -c Release`; yeni kontrollerin uygulanmadan önce başarısız olduğunu doğrulayın.
- [ ] Uygulama sözleşmeleri, doğrulama, klonlama/snapshot koruma ve MacroRunner entegrasyonu. OnError durdurma / devam politikalarına uymak; mevcut uygulama geribildirim aboneleri korumak.
- [ ] çekirdek testleri çalıştırın; tüm kontroller geçmesi gerekir.

### Görev 2: Windows doğrulama sağlayıcıları ve sonuç UI

**Dosyaları:** Yaratmak `src/Aivana.Windows/WindowsActionVerification.cs`, `src/Aivana.Desktop/MainWindow.Verification.cs`, `tests/Aivana.Windows.Tests/VerificationChecks.cs`; değiştirme `src/Aivana.Windows/WinActionExecutor.cs`, `src/Aivana.Desktop/DesktopController.Feedback.cs`, `src/Aivana.Desktop/MainWindow.Editor.cs`, Test giriş noktaları.

**Araçlar: ** WindowsActionVerification mevcut ses, ekran, işlem/ pencere ve OBS müşterileri kullanarak Görev 1 sağlayıcısı uygulanır. Ön uçuşlar kullanılabilirlik testleri ve tanımlayıcıları; doğrulama sınırlı sorgulama sonrası istenen durumu karşılaştırır. Kontrolörün tarih görünümü WPF tarafından tüketilen sonuçları üretir.

- [ ] Kullanılamayan açık ses / ekran kimlikleri için başarısız testler ekleyin, OBS zaman çıkışı, yanlış gözlemli durum, bilinmeyen kısaltma etkisi, hassas ödeme yükü eksikliği ve iğne edilebilir okuma sağlayıcıları kullanılarak iptal edilmiş sorgulama.
- [ ] Mutlak hacim, sessiz, mutlak parlaklık, süreç / pencere hazırlığı ve desteklenen OBS komutlar için gerçek okuma destekli doğrulama uygulanır. Relatif/toggle eylemleri yakalanmış bir ön-devlet gerektirir; aksi takdirde raporu doğrulanamaz.
- [ ] Durumu, başarısız gereksinimleri ve kurtarma metni gösteren adım kontrolü ve güvenli uygulama geçmişi ekleyin.
- [ ] çekirdek / Windows kontrol ve WPF enjekte devlet sağlayıcıları ile duman; gerçek parlaklık veya ses yazma yoktur.

### Görev 3: Yönlendirilmiş çalışma alanı oluşturma

**Dosyaları:** Yaratmak `src/Aivana.Core/WorkspaceSetup.cs`, `src/Aivana.Windows/WorkspaceDiscovery.cs`, `src/Aivana.Desktop/MainWindow.WorkspaceWizard.cs`, `tests/Aivana.Tests/WorkspaceSetupChecks.cs`; Modify palet ve dashboard giriş noktaları.

**Araçlar: ** `WorkspaceSetup.CreateProfile(WorkspaceSelection selection) -> Profile`; seçim şablon kimliği, seçilmiş yürütülebilir yollar, URL'ler, ses / ekran kimliği, OBS sahne ve seçilmiş bağlantılar içerir. Discovery, istikrarlı kimlik ve kullanılabilirlik ile adlandırılan adayları geri getirir. Çatışma tespiti için yapılandırma revizyonu tasarımı.

- [ ] Tüm beş sorulan sihirbaz ailesi için başarısız testler ekleyin, bağımsız olarak oluşturulan adımlar, gerekli hedefleri kaybedin, mutasyon olmadan iptal edin ve değiştirilmiş revizyon karşısında tasarruf edin.
- [ ] Kayıtlı uygulama yolları ve Başlangıç Menüsü kısayolları aracılığıyla uygulamaları keşfedin; mevcut sağlayıcılar aracılığıyla cihazlar; sadece OBS sorguları aracılığıyla sahneler. Keşfedilmemiş uygulamalar için manuel seçenekleri tutun.
- [ ] Uygulama Çalışma akışını seç → Hedefleri seç → Kontrolü seç → Önizleme / geçerlilik → Kaydet. Düzenlenebilir profilleri oluşturun ve kurulum sırasında çalıştırmayın.
- [ ] Tamamlanma / iptal / çatışma için gerçek WPF sihirbaz sigara kontrolü ekleyin; tüm testleri çalıştırın.

### Görev 4: UIA seçicileri ve sınırlı eylemler

**Dosyaları:** Yaratmak `src/Aivana.Core/UiAutomationAction.cs`, `src/Aivana.Windows/UiAutomationService.cs`, `tests/Aivana.Windows.Tests/UiAutomationChecks.cs`; Modeller değiştirmek, yapılandırma doğrulama ve WinActionExecutor.

**Interferi:** Appendiye `ActionKind.UiAutomation`; `UiElementSelector` Mağaza işlemi, otomatik kimlik, kontrol türü ve sipariş edilen atalar kriterleri. `UiAutomationService.ExecuteAsync(UiAutomationAction action, CancellationToken)` Geri Dönüş `Task`; İşlemler Invoke, SetValue, Select, Toggle, Focus ve WaitState..

- [ ] Değiştirme/değiştirme için özel bir test penceresine karşı başarısız testler ekleyin, çifte maçlar, eksik öğeler, desteklenmemiş desenler, devre dışı bırakılmış kontroller, şifre değerleri, Unicode etiketleri ve timeout / iptal.
- [ ] Sınırlı talepleri ve süreci hedef alan özel bir işçi üzerinde UIA uygulanması. Potansiyel olarak asılan sağlayıcılar, geri çekilebilir bir işçi sınırının arkasında çalışırlar; bir talep bırakmak, yan etkileri bırakmamalıdır.
- [ ] Şifre değerlerini dışlayın; bunları aşmak yerine erişim / bütünlük kısıtlamalarını bildirin.
- [ ] Windows izole edilmiş sabitleme kullanılarak kontrolü çalıştırın; yalnızca enum değerleri ekleyin ve kalıntı tutarlılık kontrolü yapın.

### Görev 5: UIA picker ve incelenmiş kayıt

**Dosyaları:** Yaratmak `src/Aivana.Desktop/MainWindow.UiAutomation.cs`, `src/Aivana.Windows/UiAutomationRecorder.cs`; eylem kataloğu, editör ve kayıt integrasyonu değiştirmek; UIA oluşturmak WPF Sigara kontrolü.

**Araçlar: ** Picker bir `UiElementSelector` iade eder; kaydediciler önerilen UIA adımlarını iade eder ve asla doğrudan bir kaydedilmiş profille yazmaz. Test-selector komutu, maç sayısını çağrısız olarak geri getirir.

- [ ] Başarısız kontroller ekleyin: picker MAKRIVO ve şifre değerini yakalar; kayıt / iptal mevcut profil değişmez; çelişkili önizleme raporları eşleşme sayısı; kaydedilen adımları kabul ederse düzenlenebilir bir tasarıma sahiptir.
- [ ] Uygulama öğesi vurgulama / boyama, işlevsellik parametreleri, selektör önizleme ve kayıt inceleme penceresi. Mevcut kısa kayıt davranışlarını koruyun.
- [ ] Katalog girişleri ve palet erişimi ekleyin; kaydetmeden önce editör hatalarını doğrulayın.
- [ ] Windows ve WPF sabitleme üzerinde duman kontrolü çalıştırın; Picker / editör ekran görüntüleri kontrol edin.

### Görev 6: Yalıtılmış Plugin Host ve Protokol

**Dosyaları:** Yaratmak `src/Aivana.Core/Plugins/PluginContracts.cs`, `src/Aivana.Windows/Plugins/PluginProcessClient.cs`, `src/Aivana.PluginHost/Aivana.PluginHost.csproj`, `src/Aivana.PluginHost/Program.cs`, `tests/Aivana.Tests/PluginProtocolChecks.cs`; Modifikasyon ve Aksiyon Çözümü.

**Interferi:** Version-1 manifesto paket kimliği / versiyonu, giriş noktası, parametreler şemaları, eylemler, devlet sağlayıcıları ve kapasite beyanları belirtmektedir. IPC mesajları protokol versiyonu içerir, benzersiz talep kimliği, yöntem, JSON parametreleri ve yazılı sonuç. `PluginProcessClient.InvokeAsync(string actionId, JsonElement parameters, CancellationToken)` Sonuçlar için tıklayınız.Aplend `ActionKind.Plugin`.

- [ ] Uyumsuz protokoller için başarısız testler ekleyin, düzensiz / aşırı (>1 MiB) mesajı, çoğaltma isteği kimliği, iptal, çarpışma, yanıtsız süreç ve eksik eylem.
- [ ] Aynı kullanıcı adı IPC uygulama 10-saniyede el sıkıştırma ve yapılandırılabilir sürüş süresi ile 60 saniyede kaplanmıştır. Sorumsuz çalışanları sona erdirin; yeniden başlatıldıktan sonra otomatik olarak bir uçuş eylemi yeniden oynamayın.
- [ ] Programları ve izin verilen denetleyici hizmetleri uygulamak. Açıklama uygulanabilir-plugin güven; işlem yalıtımı bir OS kum kutusu değildir.
- [ ] Temel testleri ve gerçek test eklentileri karşı entegrasyon testleri çalıştırın.

### Görev 7: Plugin paketleri ve örnek entegrasyon

**Dosyaları:** Yaratmak `src/Aivana.Core/Plugins/PluginPackage.cs`, `src/Aivana.Desktop/MainWindow.Plugins.cs`, `samples/Aivana.ExamplePlugin/`, paket testleri; palet / ayarları değiştirmek.

**Interferi:** Paket Servisleri `InspectAsync(path)`, `InstallAsync(reviewedPackage, revision)`, `ExportAsync(packageId, destination)`, `DisableAsync(packageId)` ve `UninstallAsync(packageId)`. Anasayfa Etiket Arşivleri Hash, Bağımlılıklar, Versiyon ve Açıklanan Yetenekler.

- [ ] Zip traversal, mutlak yollar, simlink/reparse kaçış, çifte girişler için başarısız testler ekleyin, >100 MiB genişletilmiş veriler, hash hatası, indirme, çelik inceleme ve engellenmiş ithalat.
- [ ] Uygulama önizlemesi ve açık etkinleştirme, bağımlılık hataları, aşamalı kurulum ve yükseltme rollback; kaldırılmadan önce işçi yükleme.
- [ ] IPC aracılığıyla yapılandırılabilir bir hesaplama eylemi ve canlı durumunu geri getiren gerçek bir örnek eklenti sunar; iptal dahil; paketleme / ihraç / yükleme / UI aracılığıyla kaldırma.
- [ ] Paket / ev sahibi testleri ve WPF son kontrolü. İthalat edilebilir koşuları etkinleştirmeden önce kontrol edin.

### Görev 8: HTTPS mobil hizmet ve çiftleşme

**Dosyaları:** Yaratmak `src/Aivana.MobileHost/` ASP.NET Projesi, `src/Aivana.Core/MobilePanelContracts.cs`, `src/Aivana.Windows/MobilePanelClient.cs`, Geliştirme Testleri; Modifikasyon Çözümü ve Kontrolü.

**Araçlar: ** Aynı kullanıcı IPC aracılığıyla denetleyici için mobil hizmet konuşmaları. Yollar: çiftleşme, izin verilen profiller / eylemler, doğrulanmış eylem çağrısı, iptal ve WebSocket durumu. İtiraz tek bir komut kimliği içerir; denetleyici sınırlı deduplikasyon sahiptir. İtiraflar iptal edilebilir ve Windows koruma aracılığıyla depolanır.

- [ ] Başarısız listeleme/yürütme testleri ekleyin, geçerli kod, yeniden kullanılan kod, iptal token, geçersiz köken, çoğaltma komutu, aşırı çerçeve ve sular sınırları.
- [ ] Uygulama sadece HTTPS dinleyici, kullanıcı tarafından seçilmiş arayüzler, yerel sertifika sağlama / güven talimatları, tek seferde 6-digit çift kodlar sona eren sonra 120 saniye, maksimum beş denemeler kod ve token iptal. Hasatlı tokenleri saklayın; asla kimlik doğrulamaları.
- [ ] Uygulama listelenen istikrarlı eylem kimlikleri ve mevcut yoldan yönlendirme izin verir; isteğe bağlı şelaleleri, yolları ve paket yükleme komutları reddedilir. Müşteri başına açık oran sınırları (10 komutlar/s, 30 burst) ve 64 KiB mesaj sınırı ayarlayın.
- [ ] Firewall kuralları eklemeden veya LAN erişimi sağlamadan loopback entegrasyon testleri kontrol edin; test kapatma ve token tutarlılığı.

### Görev 9: Mobil panel ve masaüstü servis kontrolü

**Dosyaları:** Yaratmak `src/Aivana.MobileHost/wwwroot/` Reaksiyon Paneli ve `src/Aivana.Desktop/MainWindow.MobilePanel.cs`; Mobil/Browser Verifikasyon Yazıları ve WPF Duman Kapakları.

** arayüzleri: ** Kullanım Görevleri 8 sadece yolları. Dokunmatik eylem ağı, profil picker, canlı değerler / ilerleme, iptal, bağlantı durumu ve çiftleşme. Masaüstü kontrolü başlangıç / duraklama, adres / sertifika talimatları, izin verilen profilleri ve müşteri iptal.

- [ ] Auth akışı için başarısız bir entegrasyon kontrolü ekleyin, durdurma / kayıp eylem kimlikleri, eylem yeniden oynatma, iptal müşteri ve servis durdurma.
- [ ] Yerel varlıklar, üçüncü taraf yazı tipleri veya internet bağımlılıkları kullanarak erişilebilir yanıtlı bir UI uygulanması. Tokenler URL'lerde hiç görünmüyor.
- [ ] Test tarayıcı düzenini 360/768/1280 CSS px; gerçek ekran görüntüleri kontrol edin. Gerçek bir telefon/tablet ile yalnızca kullanıcı açıkça LAN hizmeti sağladıktan sonra kontrol edin.
- [ ] Belge ve rapor mobil donanım doğrulama ayrı tarayıcı emülasyonu.

### Görev 10: Cihaz kapasiteleri ve sıradan anahtar gözlem

**Dosyaları:** Yaratmak `src/Aivana.Core/KeyboardCapabilities.cs`, `src/Aivana.Windows/KeyboardAdapterRegistry.cs`, `src/Aivana.Desktop/MainWindow.KeyboardAdapters.cs`; modify RawKeyboardBindingService, KeyboardBinding, validator ve cihaz teşhisi; test ekleyin.

**Araçlar: ** `IKeyboardAdapter` kapasite çekimleri, istikrarlı cihaz kimliği ve fiziksel anahtar kenarları (scan kodu, genişletilmiş bayraklar, baskı / serbest bırakma). Modlar gözlem, haritalama standart giriş, yerli SDK ve sürücü-eksklusif ayırt eder. Mevcut sanal anahtar özel bağlar temsil ve göç davranışlarını korur.

- [ ] Eski bağlar için başarısız testler ekleyin, Enter farklılıkları, dil / düzen bağımsızlığı, tekrarlanan indirimler, tutma sırasında unplug, çiftleştirme-model ikili ve enjekte edilen giriş reddedilme.
- [ ] Gözlemleri baskı yapmadan sıradan fiziksel anahtarlara uzatın. Açık yakalama ekleyin ve kimlik işlemini yeniden bağlayın.
- [ ] Gerçek adaptör kapasitelerini ve donanım test edilmemiş durumunu cihaz arayüzünde gösterin; hiçbir zaman marka adlarından Fn/dial desteği girmeyin.
- [ ] Run core/Windows/WPF simüle edilmiş raporlar ve sadece okuma cihaz listesi ile kontrol edilir.

### Görev 11: Üretici adaptörleri

**Dosyaları:** Ayrı dosyaları oluşturun `src/Aivana.Windows/KeyboardAdapters/` LogitechG, RazerChroma, SteelSeriesGameSense, WootingAnalog ve genel QmkVia rehberliği için; kayıt / UI değiştirmek ve adaptör protokol kontrolü ekleyin.

** Karşıtlıklar: ** Tüketici Görevleri 10 adaptör sözleşmeleri. Yerli kütüphaneler yalnızca doğrulanmış uygulama/satıcı yerlerinden yüklendi; SDK eksikliği kullanılamaz bir kapasite. Aydınlatma ve giriş kapasiteleri ayrıdır.

- [ ] Her adaptörün uygulanmasından önce mevcut resmi SDK başlıklarını, yeniden dağıtım koşullarını, yazılım bağımlılıklarını ve desteklenen modellerini kontrol edin; kaydedilen kaynaklar ve doğru sürümler.
- [ ] SDK eksik / kaybolan mimarisi, etkinlik baskı / yayın / tekrarlama, bağlanma, yanlış yanıt ve desteklenmemiş cihaz için başarısız testler ekleyin. Determinist kontroller için enjekte edilebilir yerli çağrı abstraksiyonları kullanın.
- [ ] Uygulama Logitech G anahtar girişi ve desteklenen aydınlatma ayrı; Razer Chroma Işıkları Synapse Standart Anahtar Haritalama; SteelSeries GameSense Işık / Lighting; Wooting Analog Soruşturma/Konfigurasyon Sınırları; QMK/VIA Standart anahtar ayar talimatı. Aydınlatma API'den mülkiyet girişini talep etmeyin.
- [ ] Uygun olan gerçek cihazları doğrulayın. Başkalarını işaretleyin / donanım test edilmemiş, model / firmware kanıtı ile; eksik kullanılabilir SDK bir açık dış kapıdır.

### Görev 12: Özel klavye filtre arka planı

**Dosyaları:** Yaratmak `src/Aivana.Windows/KeyboardFilter/`, Kaynaklar aşağıda `tools/keyboard-filter/`, backend entegrasyon testleri ve dağıtım belgeleri. uygun lisanslı backend değerlendirmesi geçmediği takdirde özel bir imzalı sürücü projesi gereklidir.

**Araçlar: ** Backend raporları yüklü / uyumlu / imzalanmış durum; cihaz özel yakalama başlangıç / durdurma, anahtar kenarları ve acil serbest bırakma. Ayrı bir gözlemci kontrolörün kaybına geçerek geri döner. Ana klavye asla varsayılan olarak yalnızca olmayacaktır.

- [ ] Aday sürücü lisansı, imzalanan ikili kaynak ve mevcut Windows 11/Secure Boot uyumluluğu değerlendirmek. Go/No kararını kaydedin. Sıradan testler sırasında sürücü kurulmamıştır.
- [ ] Filtre hedef seçimi için sözleşme testleri yazın, açılma, kalp atışı sona erdi, IPC kaybı, yeniden bağlanma, duraklama / iptal, anahtar tutma serbest bırakma ve kaldırma.
- [ ] Uygulama arka planı, açık seçim / etkinleştirme arayüzü, acil serbest bırakma ve yükleme / uninstaller. Düşük düzeyde heykel / Raw Input zamanlamayı değiştirmeyin.
- [ ] İki klavye ile geri dönüştürülebilir bir test makinesinde doğrulama: seçilen anahtar yalnızca seçilen cihazda bastırılır; ana yazdırma etkilenmez; çarpışma / durdurma / kaldırma girişleri.
- [ ] Gerçek imzalanmış / lisanslı arka plan ve fiziksel testler için kapı üretimi tamamlanması. Dış imza / lisans kullanılamazsa, görev eksik tutmak ile kesin bağımlılık kanıtı.

### Görev 13: Entegre doğrulama, çeviri ve serbest bırakma paketi

** Dosyalar: ** Ekleme `docs/testing/2026-10-12-six-extensions.md`; ilgili DE/EN/FR/IT belgeleri, konumlandırma kataloğu ve sigara kontrolü giriş noktaları güncellenir. Diğer belgeleri saklayın.

- [ ] Tüm giriş noktaları ve anahtar görüntüleri entegre edin; mevcut 27 dilinde katalog çalışma akışını kullanarak çeviri tamamlayın ve incelenmemiş çevirileri açıklayın.
- [ ] koşu `dotnet build Aivana.slnx -c Release`, `dotnet run --project tests/Aivana.Tests -c Release`, `dotnet run --project tests/Aivana.Windows.Tests -c Release`, ve `dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test`.
- [ ] Masaüstü / mobil ekran görüntüleri ve doğrulama 100/150/200 yüzde Windows ölçeklenmesi, yapılandırma döngüsü gezileri, eklenti yaşam döngüsü, ağ geri dönüşü ve gerçek klavye yeniden bağlantısı.
- [ ] `dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/six-extensions/win-x64` ile yeni bir eser dizinine yayınlayın; eşlik hizmetleri / ev sahibi ikili ve lisans bildirimleri içerir. Yüklü uygulamayı aşırı yazmayın.
- [ ] Rapor tamamlanmış testler, donanım test edilmemiş adaptörler ve dış serbest bırakma kapıları. Tüm altı teslimatçı, tüm talepleri tamamlamadan önce kabul kriterlerini karşılamalıdır.

## Kendini incelemek planı

Tüm altı spec bölümleri Görevler 1–12 haritasını kapsar; Görev 13 yerleştirme, entegre doğrulama ve serbest bırakma. Hazırlık raporlama, uygulama serializasyonu, miras konfigürasyonu, sırlar, paket güvenliği ve fiziksel-devlet haksızlığı açık sahipleri ve testleri vardır. Dış sürücü / SDK / donanım kapıları kanıtlanana kadar eksik kalır, yazılımın başarısı olarak sayılır.

## Kullanıcı incelemesi için uygulama seçimi

Önerilen: bu sohbet, görevi görevi, çünkü çoğu modül sözleşmeleri paylaşır ve mevcut çalışma alanı önemli mevcut düzenlemeler içerir. Kullanıcı tarafından seçildiği takdirde Delegated execution mevcuttur. Bu planı inceleyin ve uygulama öncesi uygulama yöntemi seçin, yazma planları çalışma akışına göre.
