<!-- generated-doc locale=tr-TR source=docs/testing/2026-10-12-responsive-layout.md sha256=8b3710a27f806575e88b4d9a0c4e80de4f26539ea1b5826cfe291764228b4ca6 -->

> Makine destekli çeviri. Yapı, kod örnekleri ve bağlantılar kontrol edilir; bu, ana dili konuşan birinin incelemesi veya hukuki onay anlamına gelmez. Geçmiş raporlar özgün tarihlerini ve kapsamlarını korur. [Orijinal Kaynak](../../testing/2026-10-12-responsive-layout.md)

# Responsive WPF yüzeyi

Ana sayfalar kullanışlı içerik genişliğine göre yönlendirilir, sabit bir pencere genişliğine değil. 900 DIP'lerin içeriği aralığında, Dashboard ve Editör birbirine bağlanır. 620 DIP’ler iki taraflı olarak belirlenir. 820 DIP'nin pencere genişliği altında, yan çubuğu yapıştırılır. En az boyutu: 640 × 480 DIPs.

Profil ve ayar alanları ve klavye kontrol elemanları, gerçek ağ genişliğine göre sütunlarını değiştirir. Mevcut kayıtları yeniden düzenlemek yerine yeniden düzenleme yapılır. Aksiyon kartları mevcut çizgilerin genişliğini paylaşır. Araç çubuğu kırıldı. Klavye tarafı dikey olarak ulaşılabilir, fiziksel klavye oranlarını korur.

Otomatik WPF-Etiket: tüm beş ana sayfalar 640 × 480, 900 × 620 ve 1440 × 900; Ekran görüntüleri her sayfada; elde edilen metni kontrol ederek her iki yönde sütun değiştirme. WPF-Birimler - Gerçek Windows-Sınıflandırma 150/200 Yüzde ve birkaç monitör henüz doğrulanmadı.

Sınav Durumu: Hatalar / Uyarılar olmadan Release-Build. Tam WPF tüm boyutları, sütun değişiklikleri, klavye düzenleri ve dil testleri ile test edilmiştir. git diff - kontrolü devam etti.
