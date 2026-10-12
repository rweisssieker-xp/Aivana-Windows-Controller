<!-- generated-doc locale=tr-TR source=docs/superpowers/plans/2026-10-12-controller-usps.md sha256=b7af0d36209e04b458ce64a5db78ab7e552a84c5e9b450ea0bd97179a4eb7f68 -->

> Makine destekli çeviri. Yapı, kod örnekleri ve bağlantılar kontrol edilir; bu, ana dili konuşan birinin incelemesi veya hukuki onay anlamına gelmez. Geçmiş raporlar özgün tarihlerini ve kapsamlarını korur. [Orijinal Kaynak](../../../superpowers/plans/2026-10-12-controller-usps.md)

# Kontrolü Genişletme Planı

**Hedef: ** Gerekli monitör, hareket, çalışma akışı, kayıt, cihaz teşhisi, taşınabilir profil değişikliği ve iz geliştirmeleri uygulanır.

**Mimarlık: ** Mevcut Core / Windows / Masaüstü katmanlarını genişletin. Temiz devlet makineleri ve taşınabilir yapılandırma mantığı, çekirdekte, Windows yerli entegrasyonlarda ve masaüstünde modal tasarım editörleri. Mevcut kirli çalışma alanı düzenlemeleri ve yerleştirilmiş kullanıcı yapılandırması saklayın.

**Spec: ** ../specs/2026-10-12-kontroller-usps-design.md

- [x] İzleme hizmeti, hedef tutarlılığı ve asinkron canlı durumu; mevcut olmayan kimlikleri test etmek ve iptal etmek.
- [x] Yönlendirme makine ve tutulan katmanlar; test kenarları, tekrarlama, yeniden ayarlama ve mevcut anlık bağlar.
- [x] Çalışma akışı koşulları, ilerleme dalları ve hata politikası; test görüntüleri ve iptal.
- [x] Tasarım incelemesi ile açık kısaltma kaydı; test filtresi ve hokeyi dağıtma.
- [x] Değerlendirme kontrolleri ve tahminleri ile paylaşılmış dosya profili değişimi; test çatışmaları ve taşınabilir haritalama.
- [x] Genetik cihaz başına standart özel anahtarlar ve teşhis; test parsing, çifte haritalama ve durak davranışları.
- [x] Tray/window/executable ikonu kaynakları ve durdurulmuş görünüm; yayınlanan kaynakları doğrulayın.
- [x] Tüm palet / editör giriş noktaları ve yerleştirilmiş metni entegre edin; Core, Windows ve WPF kontrolü çalıştırın.
- [x] Ayrı bir çerçeve dizinine yayınlayın, kullanıcı ayarlarını kaydedin ve güncellenmiş uygulamayı yeniden başlatın.
