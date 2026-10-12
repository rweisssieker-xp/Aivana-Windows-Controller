<!-- generated-doc locale=tr-TR source=docs/MAINTENANCE.md sha256=9bf5875b50386e698c0574eee390f790d4b1c29cb11288c856c98e03a05877f7 -->

> Makine destekli çeviri. Yapı, kod örnekleri ve bağlantılar kontrol edilir; bu, ana dili konuşan birinin incelemesi veya hukuki onay anlamına gelmez. Geçmiş raporlar özgün tarihlerini ve kapsamlarını korur. [Orijinal Kaynak](../MAINTENANCE.md)

# Tüm belgelendirme dilleri

Belgelendirme listesi tüm 27 UI dilini takip eder. Üretilen klasörler bölgesel kodları kullanır; Norveç haritaları `nb-NO` ve Sırp Latince `sr-Latn`. Her klasör bir endeks, el kitabı, ayrıntılı eylem referansı, özellik listesi, doğrulama notları, araştırma dahil CSV, tarihsel planlar ve lisans analizi içerir.

## Gerçeğin kaynağı

Edit kökü `README.md`, `docs/HANDBOOK.md`, `docs/FEATURES.md`, `docs/VERIFICATION.md`, bu sayfada veya orijinal dosyaları `docs/research`, `docs/testing`, `docs/superpowers` ve `docs/licensing`. Daha sonraki bir versiyonu doğrulaması anlamına gelen bir tarihsel raporu yeniden yazmayın. uygulama, simülasyon, gerçek cihaz kabul ve ticari onay ayrı tutun.

## Doğurmak ve doğrulamak

```powershell
python tools/localize_documentation.py --refresh-sources
python tools/check_documentation.py
```

Seçmeli çevrimiçi jeneratör, mevcut konumlandırma aracını takip ederek Google'ın kamu çevirisi sonuna belgeleri gönderir. Kullanıcı konfigürasyonu, sırları veya model dosyalarını okumaz. Sadece bu hizmet için uygun belgeler üzerinde çalıştırın. Kod blokları, inline kodları, URL'ler, tablo yapısı ve teknik tanımlayıcılar korunmaktadır. Çeviri cache ve dondurulmuş bir kaynağı görüntüsü göz ardı altında kaydedilir `artifacts/documentation-translation`.

İnternet sağlayıcısı sınırlı olduğunda, `tools/translate_documentation_local.py` Aynı cacheyi açıkça sağlanan bir yerle doldurur M2M100 CTranslate2 Model : The 2026-10-12 Snapshot MIT lisanslı kullanıyor facebook/m2m100_418M Modeli, dönüştürülmüş JustFrederik/m2m_100_418m_ct2_int8 revizyonda `1aeed44db4dd61a486bba44acf54c76507082a2c`. CTranslate2 4.8.2 ve `SentencePiece` 0.2.2 sadece yapı araçlarıdır. hiçbir model ağırlığı veya çeviri bağımlılığı gönderilmez MAKRIVO. Generatörle çalıştırın `--cache-only` cache'yi ağ istekleri olmadan sunmak için doldurduktan sonra. yerel yardımcı uzaktan model kodu indirmez veya çalıştırmaz.

```powershell
python -m pip install ctranslate2==4.8.2 sentencepiece==0.2.2
python tools/translate_documentation_local.py --model C:\models\m2m100-ct2 --refresh-sources
python tools/localize_documentation.py --cache-only
python tools/check_documentation.py
```

olmadan `--refresh-sources`, Bir çalışma dondurulmuş görüntüsünü yeniden yapar. orijinalleri düzenledikten sonra yenileme seçeneğini kullanır. gizli segmentler yeniden kullanılır. bir kısmi çalışma yerel kodları kullanarak seçebilir `--locales`; Tüm yerleri yayınlamadan önce tamamlayın. durdurulmuş sürüşler sessizce tercüme edilmemiş bir tercüme için değiştirmez.

Manifesto kaynağı ve SHA-256 normal UTF-8 metin parmak izi ile her dosyayı kaydeder: LF çizgi sonları ve hiçbir byte sipariş işareti. Çevrimdışı denetleyici tam dil kapsama, parmak izi, yerel bağlantılar, kod örnekleri, tablo/CSV yapısı, kaynak URL'leri ve değiştirilmemiş yer tutucuları doğrulamaktadır. Çelik kaynakları reddediyor. Geçen yapısal kontrol, yerli konuşmacı bir inceleme veya yasal onay değildir. Bu sayfalar bu nitelikleri görsel olarak taşıyor.

## Yayın ve Paketler

Belgeleri, generatörü ve kontrolörü bir araya getirin. Orijinal üçüncü taraf lisans koşullarını koruyun; tercüme edilen lisans analizi bilgilendiricidir. Yapım eserleri, çeviri cache ve yerel orkestrasyon dosyaları Git'e ait değildir. Kaynak-doküman yayıncılığı kendisi bir Windows yüklemeyi yayınlamaz.

Kullanıcı yüzü uygulama çevirileri ayrı JSON katalogları altında `src/Aivana.Core/Localization`. Bu belgeler bu katalogları değiştirmez. Bir uygulama oluşturma ve belgeleri bir sürümlü snapshot paylaşmalıdır; yeni açıklanan deneysel çalışma süresi desteği, yüklü bir paket öncesi olabilir.
