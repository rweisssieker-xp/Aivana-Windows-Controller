<!-- generated-doc locale=tr-TR source=docs/testing/2026-10-12-github-publication.md sha256=ab4d40cd12047e2cdacad359a12febe16d5689a82f4f792b484b3d4e2430e02a -->

> Makine destekli çeviri. Yapı, kod örnekleri ve bağlantılar kontrol edilir; bu, ana dili konuşan birinin incelemesi veya hukuki onay anlamına gelmez. Geçmiş raporlar özgün tarihlerini ve kapsamlarını korur. [Orijinal Kaynak](../../testing/2026-10-12-github-publication.md)

# GitHub-Yayıncılık MAKRIVO-Gelişme Durumu

Şekil : 12 Ekim ayı 2026 Kaynak kodu yayın, imzalanmamış satış.

- Çözümün kurulumu: başarılı, sıfır uyarılar ve hatalar.
- Temel testler: Başarılı
- WPF Smooke Test: Başarılı; Yerel İnşaat Planında Yeni Sonuç Raporu.
- İlk tam Windows süit: ön tabanlı pencereler, ses, ekran ve iCUE testleri başarılı; ardından UI otomatik çalışma yanıtında Timeout.
- Hedefli Tekrarlama `--extensions-only`: UI otomatikleştirme, çoklu dikkat, devre dışı bırakılmış hedefler, şifre hariçliği, eklenti süreci ve gerçek HTTPS mobil barındırma dahil. Orijinal Timeout bir zaman dilimi değişikliği ile gizlenmemiştir; nedeni henüz kesin olarak kanıtlanmamıştır.
- Sonraki tam Windows geri dönüşü: başarıyla ön taban pencereleri ve tüm uzantı testleri dahil. Başarılı bir tekrarlama, sporadik zaman çıkışlarını engellemez.
- Yayınlanmadan önce tipik özel anahtarlar veya üretken Stripe/GitHub/AWS anahtarları kontrol edilen kaynak dosyasında bulunamadı. Bu sınırlı örnek kontrolü tam bir gizlilik incelemesi değildir.
- Yapı gerçekleri ve yerel orkestrasyon verileri ile birlikte yayınlanmaz.

İşlem ve kalan indirimler: [Featur Listesi](../FEATURES.md). Önceki test raporları zamanında geçerlidir; herhangi bir donanım veya masaüstü bağlam için bir garanti değildir.
