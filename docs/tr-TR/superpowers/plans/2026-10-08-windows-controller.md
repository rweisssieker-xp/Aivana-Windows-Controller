<!-- generated-doc locale=tr-TR source=docs/superpowers/plans/2026-10-08-windows-controller.md sha256=10a5ba49bbefc617bce40563c668e3c4628647822117087deba766713fb9b3af -->

> Makine destekli çeviri. Yapı, kod örnekleri ve bağlantılar kontrol edilir; bu, ana dili konuşan birinin incelemesi veya hukuki onay anlamına gelmez. Geçmiş raporlar özgün tarihlerini ve kapsamlarını korur. [Orijinal Kaynak](../../../superpowers/plans/2026-10-08-windows-controller.md)

# Windows Kontrol Planı Uygulama

[English (US)](2026-10-08-windows-controller.md) | [Deutsch](2026-10-08-windows-controller.md) | [Français](2026-10-08-windows-controller.md) | [Italiano](2026-10-08-windows-controller.md)

> Agent işçiler için: paralel olarak yalıtılmış mülkiyet kullanın; kullanıcı, maksimum yararlı subaktiflerle tam uygulama isteyecektir.

**Hedef: ** Onaylanmış tasarımda yerli Windows anahtar paneli teslim edin.
**Mimarlık:** Temel yapılandırma ve iptal edilebilir uygulama; Windows Adaptörler; WPF Desktop kompozisyonu.
**Tech Stack: ** .NET 10, WPF ve Win32 Ücretli bağımlılık yok.
**Spec: ** ../specs/2026-10-08-Windows kontrol cihazı tasarımı.md

## küresel sınırlamalar
Alman UI; yerel depolama; Windows x64; gerçek donanım kapasitesi durumu; klavye kaydı yok; varsayılan otomatik başlatma yok.

## inceleme odak
Kötü şekillerde ithalat, kaydedilen ayarları değiştirmemelidir; tutulan modifikasyonlar çıkışları kirletmemelidir. eklemeden donanım çökmemelidir, kapak kullanılabilirliğini korumalıdır; iptal edilen makro kaynakları serbest bırakmalıdır.

## Görevler ve Mülkiyet
- [x] Temel ajan: src/Aivana.Kore ve testler/Aivana.Testler Modeller, JSON dayanıklılığı, doğrulama, profil seçimi ve MacroRunner. Başarısız davranış testi yapın ve sonra tüm çekirdek testleri uygulayın.
- [x] Windows Üretici : src/Aivana.Windows. RegisterHotKey, cihaz listesi, SendInput, önceki süreç ve seçmeli Corsair Aydınlatma adaptörü. güvenli duman testleri ile yerli yapı boyutlarını ve kayıt temizliğini kontrol edin.
- [x] GUI aracı: src/Aivana.Desktop/MainWindow.xaml ve .cs artı UI yardımcıları sadece. Karanlık tablo, tam editör, profiller, ayarlar, klavye genel bakış ve makro editör. Bir zamanlar sözleşmeler yapılır.
- [x] Ebeveyn: Paylaşım sözleşmeleri, proje dosyaları, DesktopController bileşimi, uygulama / sürüş yaşam döngüsü, ithalat / ihracat diyalogları, belgelendirme, entegrasyon kontrolleri, yayın ve uygulama çalıştırma duman testi.

## Paylaşımlar
Ürünler Core/Models.cs adresinde mevcuttur. Kontrolörün uygulanması Desktop/DesktopController.cs'de yaşar. UI, bu belirli sınıfta kontrol yöntemlerini çağırır. Görevlerinde görevliler tam imzalar alırlar. Her birinin ayrı dosyaları vardır. Aile, tüm ajanların bitiminden sonra çapraz proje başarısızlıklarını çözür.

## Kontrol ve Teslimat
- [x] dotnet build Aivana.slnx -c Yayın: sıfır hatalar.
- [x] dotnet run --project tests/Aivana.Tests -c Release: davranış kontrolü geçer.
- [x] dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64.
- [x] Yayınlanan uygulanabilir başlatma, denetim Windows UI ve süreç istikrarı, güvenli profil / düzenleme / dayanıklılık akışı.
- [x] Yapım / başlatma talimatları ve donanım kısıtlamaları ile README yazın; kayıt kontrolleri ve kalan donanım-sadece testleri.
