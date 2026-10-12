<!-- generated-doc locale=tr-TR source=docs/superpowers/plans/2026-10-08-controller-polish.md sha256=1a076a67eea31048f6305f30a121c474d592451d1ddcd9b46bc339fd8ec068f2 -->

> Makine destekli çeviri. Yapı, kod örnekleri ve bağlantılar kontrol edilir; bu, ana dili konuşan birinin incelemesi veya hukuki onay anlamına gelmez. Geçmiş raporlar özgün tarihlerini ve kapsamlarını korur. [Orijinal Kaynak](../../../superpowers/plans/2026-10-08-controller-polish.md)

# Kullanılabilirlik ve Kapasite Geliştirme Planı

[English (US)](2026-10-08-controller-polish.md) | [Deutsch](2026-10-08-controller-polish.md) | [Français](2026-10-08-controller-polish.md) | [Italiano](2026-10-08-controller-polish.md)

Kullanıcı isteği: görünümünü, kullanılabilirliğini ve işlevselliğini iyileştirir; ön onay gerekli küçük modeller ve otonom tamamlanma için maksimum yararlı ajanları gerektirir.

## Sınırı
Mevcut profilleri korumak ve çalışmak Corsair SDK’nın geliştirilmesi, mevcut WPF Prototype oluşturmak yerine uygulama.

- [x] Shell/dashboard işçisi: tepki verici düzen, aktif navigasyon, okunabilir kontroller, kompakt aktivite/status, çekme ve atma çubuk yeniden düzen, yararlı boş durumlar ve şablonlar. MainWindow.xaml, MainWindov.xam.cs ve MainWind.Dashboard.cs ile ilgili bilgiler
- [x] Editör çalışanı: MainWindow.Editor.cs ve seçmeli editör-sadece kısmi dosyaları. Açıklama aşamalı düzenlemeler, Kaydet/Kaldır, doğrulama inline, çekme ve atma ile özel makro listesi, çift adımlar, güvenilir hotkey yakalama ve dosya/URL alanları.
- [x] Windows işçi: yerli doğrulama, sağlam cihaz isimleri ve SDK kapasiteleri, sınırlı modifier bekleri ve güvenli enjeksiyon; kendi Windows ve Windows testleri.
- [x] Ebeveyn: çekirdek uygulama ilerlemesi, kontrolör arayüzleri, yapılandırma koruması, entegrasyon testleri, gerçek render ekran görüntüleri, yayın / paketleme ve çalışan uygulamaların değiştirilmesi.

## Interface Sözleşmesi
Kontrol cihazı mevcut APIs artı çizgi LastStatus, bool HasRunningActions, bool IsRunning(Guid), çizgi GetTileStatus(Guid)、 çizgi ForegroundProcess, bool CorsairAvailable, IReadOnlyList<CorsairKeyboardInfo> CorsairKayboards, Task TestCorsairColorAsync(string), void OpenIcue(). Editör bool HasUnsavedChanges, void DiscardDraft(), bool CommitDraft(); mevcut alan/metod isimleri kısmi sınıflar arasında kullanılabilir kalır.

## kabul
Gerçek kullanıcı yapılandırması yeniden ayarlanmamalıdır; geçerli adım değişiklikleri bir kez kaydedilir, geçersiz taslaklar düzenlenebilir kalır, kaydedilen verileri kaydeder. Native hotkey aliazları, bağlantısız pencerelere test girişleri enjekte etmeden doğrulanır. Tile ve macro reorder test edilmiştir; tüm görüntüler masaüstü boyutlarında render edilmiştir; çalıştırılmış / eksik / tam durum görülebilir; gerçek SDK cihaz modeli görüntülenmiştir. İnşaat / test ve yayınlanan sigara geçiş, ve güncellenmiş uygulama başlatılır.
