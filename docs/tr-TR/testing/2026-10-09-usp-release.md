<!-- generated-doc locale=tr-TR source=docs/testing/2026-10-09-usp-release.md sha256=96a5b23b67032ef07f2f5dd93e9585e7bab0a522e5096ffc9f4d020823c4264e -->

> Makine destekli çeviri. Yapı, kod örnekleri ve bağlantılar kontrol edilir; bu, ana dili konuşan birinin incelemesi veya hukuki onay anlamına gelmez. Geçmiş raporlar özgün tarihlerini ve kapsamlarını korur. [Orijinal Kaynak](../../testing/2026-10-09-usp-release.md)

# Keyboard yardım, overlay ve uygulama geribildirim

[English (US)](2026-10-09-usp-release.md) | [Deutsch](2026-10-09-usp-release.md) | [Français](2026-10-09-usp-release.md) | [Italiano](2026-10-09-usp-release.md)

Mevcut Windows uygulamasında üç öncelikli iyileştirme uygulanmıştır:

- Özel anahtar sihirbazı: gerçek iCUE destekli cihaz / anahtar depolama, geçici G anahtar yakalama, yapılandırma başarısı doğrulama, çatışma doğrulama ve kaydedilmemiş bir eylem taslağı. Capture rollback, queued callback invalidation ve normal-binding restorasyon hataları ve diyalog kapatma.
- Klavye aşırı: fiziksel referans geometri, aktif profil bağlantıları, canlı profil / düzenleme yenileme ve sıradan Windows anahtar vurgulama. Ctrl+Alt+F1 tutun veya palet/klavye sayfasını geçin. Çözünürlüğün, tıklama ve eksikliği.
- Yönlendirme geribildirim: yazılı Bekle / Çalışma / Adım / Tamamlandı / İptal / reddedilen aşamalar, adım / tekrarlama ilerlemesi ve bir etkinleştirme tastası. geribildirme eylem içeriği ve istisna mesajları dışlar. son adım iptal tamamlanmasını bildiremez. OBS Devlet soruları.

Kontrol için :

- 24 Temel kontroller, aşama siparişleri, başarısızlık / iptal sonuçları, son adım iptal, dinleyici istisna ve özel eylem değerlerinin dışlanması dahil.
- Serbest bırakma çözümü uyarı / hata olmadan geçti.
- Windows Çekler geçti; bağlantılı K100 ⁇ G1–G6 ve yerli konfigürasyon / serbest bırakma başarılı oldu. ses son noktaları durumunu değiştirmeden kontrol edildi.
- WPF duman tüm referans aşırı düzenlemeleri, sihirbaz / katalog / çatışma / sürtünme dayanıklılığı, canlı yerli yakalama konfigürasyonu, çağrı geri iletimi, çelik çağrı geri reddetimi ve durdurulmuş bağlama restorasyonu ile geçti. Native callback girişi simüle edilmiştir; bir fiziksel insan G anahtarı bir manuel kontrol olarak kalır.
- Gerçek ön pencere değişkenliği yürütme tuast için kontrol edildi. Overlay etkileşim özellikleri ve show/hide kontrol edildi; çekimler sihirbaz, overlay ve toast için sunuldu.
- 27 diller tüm 819 kanonik çizgiler içerir; çevrimdışı yer tutucu / anahtar doğrulama geçti.

Snapshots ve tam GUI sonuçları artifakt/usp-win-x64/duman içerir. Testler geçici profilleri kullanır; üretim ayarları yükleme sırasında desteklenir ve hash kontrol edilir.
