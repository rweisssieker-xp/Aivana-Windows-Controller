<!-- generated-doc locale=tr-TR source=docs/VERIFICATION.md sha256=f19afb9e7b5ffac90c83167416ab3af0964a4481dd16b7357f8585480c6aae65 -->

> Makine destekli çeviri. Yapı, kod örnekleri ve bağlantılar kontrol edilir; bu, ana dili konuşan birinin incelemesi veya hukuki onay anlamına gelmez. Geçmiş raporlar özgün tarihlerini ve kapsamlarını korur. [Orijinal Kaynak](../VERIFICATION.md)

# Sınav – 8. Ekim 2026

[English (US)](../en-US/VERIFICATION.md) | [Deutsch](../de-DE/VERIFICATION.md) | [Français](../fr-FR/VERIFICATION.md) | [Italiano](../it-IT/VERIFICATION.md)

Uygulama istikrarlı .NET SDK 10.0.202 ile Windows x64 altında inşa edildi ve bağımsız olarak yayınlandı Windows-x64-Ausgabe.

## Otomatik olarak kontrol

- Genel yapı: hiçbir uyarı ve hata yok.
- 19 Temel davranış testleri: Standart yapılandırma, doğrulama, sıfır / çiftlik verileri, profil öncelikleri, ithalat / ihracat ve yedekleme, makro sırası ve tekrarlama, çöküş, çift çözünürlük, serializasyon, değişmez performans düğmesi ve adım hatası.
- Windows sınavı: Hotkey normalleştirme ve alia isimleri, yerli yapı boyutları, gerçek tuş listesi, güvenli kayıt ve yeniden serbest bırakma, sintetik WM_HOTKEY kaydırma ve kaydırma, giriş simülasyonu sırasında kaydırma devre dışı bırakma.
- Bağımsız EXE `--smoke-test` ile başlatıldı: tüm beş WPF görüntüleri kaydetmek, çubuklar kurulabilir / kopyalanabilir / silinebilir, tasarımlar refresh sırasında kaydedilir, yapılandırma kaydedilir ve yeniden yüklenir. Bu test, izole edilmiş bir geçici yapılandırma dizinini kullanır ve metin veya medya eylemleri yapmaz.
- Dashboard, editör ve kompakt görüntü (900 × 720) gerçek WPF rendering olarak görsel olarak kontrol edildi. Tasarım depolama, reddedilme, makro sırası, Alman hotkey ittifakı, bireysel makro kırılma ve adım ilerleme kontrol edildi.

## Hardware Bağımlı Sınırları

Son yerel geçiş, dört Windows tuş arayüzünü tespit etti. Bu sayı, HID/Windows arayüzlerini tanımlar, zorunlu olarak dört fiziksel klavye değildir.

Resmi iCUE SDK, EXE'nin yanında yer almaktadır. Canlı test iCUE ile bağlantılıdır ve model ve LED bilgileri ile bir Corsair tuşu tespit eder. Aydınlatma değişiklikleri otomatik olarak yapılmamıştır; model bağımlı arka baskı ve diğer üreticiler donanım test edilmemiştir. Genetik Hotkeys klavye çapında geçerlidir.

Yazı/Medya girişleri diğer açık kullanıcı programlarına enjekte edilmemiştir. Yerli veri yapıları ve hatalar/özgürlük mantığı incelenmiştir; belirli bir hedef programında davranış, diğerleri arasında yetkinlik seviyesine bağlıdır.

## tekrarlamak

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64
& ./artifacts/win-x64/MAKRIVO.exe --smoke-test
```

GUI testi sonuçları ve ekran görüntüleri `smoke` kontrol edilen EXE'nin yanında yazar. Diagnostik bir mod olarak düşünülür ve tamamlandıktan sonra kalıcı bir arka plan başlatmaz.


## Genişletilmiş kontrol

Ek olarak incelenmiştir: süreç ve pencere başlıkları ile profil koşulları, rekursiv genişleme olmadan değişkenler, makro görüntüleme olmadan, yeni eylem doğrulama ve eski JSON dosyaları uyumluluğu. Yerli sınavlar, kayıt, çatışma, WM_HOTKEY dispatch ve serbest bırakma, temiz monitör dağılımı ve ihtiyaç kontrolü ile klipleme okuma erişimi ile komut paketini kapsar. WPF testi gerçek yelpazesini açır ve geri alır, geriye döner / geri alır ve tercih eder, geri çekildikten sonra temiz tasarımları inceler ve süreci başarıyla tamamlar.

Windows engelleme, aralık değişiklikleri ve pencere hareketleri testler sırasında kullanıcı programlarına uygulanmadı. Aksiyonlar Windows soruları asynkron yayınlar; belirli hedef programı kendi minimum boyutları veya izin sınırları olabilir. Profil kapatma ve uygulama istatistikleri devam eden toplantı için geçerlidir.


Genişletme incelemesi 12.10.2026: 30 Core testleri geçti; Windows-Adaptörler, girme sırasında serbest bırakma, yazılı koruyucu monitör algılama ve OBS-Status-Mock’un devamı. WPF Sürüş alımı/gözaltıları, kayıt başlatma/ durdurma, profil değişikliği önizlemesi, hareketler ve sembol kaynakları.: 27 Kataloğu ile 967 Anahtarlar; Belgelendirme: 84 Dört dilde sayfalar. diğer üreticilerin sahip düğmeleri Corsair donanım üzerinde test edilmemiştir; genel destek cihaz özel standart olaylar içerir F13–F24 Bulut taşımacılığı seçilmiş bir senkronize klasör aracılığıyla yapılır; ithalat kontrol edilir ve devre dışı bırakılır.
