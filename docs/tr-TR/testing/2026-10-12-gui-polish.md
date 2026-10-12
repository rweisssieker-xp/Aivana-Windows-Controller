<!-- generated-doc locale=tr-TR source=docs/testing/2026-10-12-gui-polish.md sha256=ac39188358e51c68964421d3e855161d607b02a41da2b1a1fad70244f9feff97 -->

> Makine destekli çeviri. Yapı, kod örnekleri ve bağlantılar kontrol edilir; bu, ana dili konuşan birinin incelemesi veya hukuki onay anlamına gelmez. Geçmiş raporlar özgün tarihlerini ve kapsamlarını korur. [Orijinal Kaynak](../../testing/2026-10-12-gui-polish.md)

# GUI'nin yeniden düzenlenmesi 12.10.2026

- Doğrudan giriş “Araçlar” Navigasyon; kurmak, oluşturmak ve denetlemek için açıklanan eylemler ile üç grup.
- Mevcut on dört diyalog alanı, ana pencere kaynakları ile ortak bir WPF diyalog sınıfı kullanır, birleşik yazı ve sahibi konumlandırma.
- Dialog boyutu Windows çalışma alanına sınırlıdır; uzun içerikler kaydırma satırları alır. Ayarlar ve profil sayfaları düşük yükseklikte bile kaydırılabilir kalır.
- Aksiyon filtresi yalnızca Dashboard'da görünür. Profil seçimi ve arama, yardımcı teknolojiler için açık isimler içerir. Alan tanımları daha çelişkili.
- Dialog Smooke testi, 560 × 440 boyutunda araç incelemesini ve genişleme diyaloglarını içerir; mevcut kayıt ve dil testleri kaydırma yapısına göre ayarlanmıştır.

Sınırlar: Kompakt pencereler gerçek Windows DPI testleri için bir yedek değildir 150/200 yüzde. Yeni Alman metinleri henüz tüm 27 diller için tam olarak tercüme edilmemiştir. Yeni bir donanım doğrulaması ve ölçülen kullanılabilirlik hakkında hiçbir açıklama yok.

Sınav Durumu: Uyarı / hata olmadan yayın yapımı; kompakt araç ve uzantı diyalogları ve dil değişikliği testleri de dahil olmak üzere tam WPF koku testi geçti. git diff - kontrolü devam etti.
