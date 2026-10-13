<!-- generated-doc locale=tr-TR source=docs/MAINTENANCE.md sha256=9bf5875b50386e698c0574eee390f790d4b1c29cb11288c856c98e03a05877f7 -->

> Makine destekli çeviri. Yapı, kod örnekleri ve bağlantılar kontrol edilir; bu, ana dili konuşan birinin incelemesi veya hukuki onay anlamına gelmez. Geçmiş raporlar özgün tarihlerini ve kapsamlarını korur. [Orijinal Kaynak](../MAINTENANCE.md)

# Tüm belge dillerinin bakımı

Belge envanteri 27 arayüz dilinin tamamını kapsar. Üretilen klasörler bölgesel kodları kullanır; Norveççe `nb-NO`, Latin harfli Sırpça `sr-Latn` koduna eşlenir. Her klasör dizin, kılavuz, ayrıntılı eylem referansı, özellik envanteri, doğrulama notları, CSV dahil araştırma, geçmiş planlar ve lisans analizi içerir.

## Gerçeğin kaynağı

Edit kökü `README.md`, `docs/HANDBOOK.md`, `docs/FEATURES.md`, `docs/VERIFICATION.md`, bu sayfada veya orijinal dosyaları `docs/research`, `docs/testing`, `docs/superpowers` ve `docs/licensing`. Daha sonraki bir versiyonu doğrulaması anlamına gelen bir tarihsel raporu yeniden yazmayın. uygulama, simülasyon, gerçek cihaz kabul ve ticari onay ayrı tutun.

## Doğurmak ve doğrulamak

```powershell
python tools/localize_documentation.py --refresh-sources
python tools/check_documentation.py
```

İsteğe bağlı çevrimiçi üretici, mevcut yerelleştirme araçlarını izleyerek belge metnini Google’ın herkese açık çeviri uç noktasına gönderir. Kullanıcı yapılandırmasını, sırları veya model dosyalarını okumaz. Yalnızca bu hizmete uygun belgelerde çalıştırın. Kod blokları, satır içi kod, URL’ler, tablo yapısı ve teknik tanımlayıcılar korunur. Çeviri önbelleği ve dondurulmuş kaynak anlık görüntüsü Git’in yok saydığı `artifacts/documentation-translation` altında saklanır.

Çevrimiçi sağlayıcı istek hızını sınırladığında `tools/translate_documentation_local.py`, açıkça sağlanan yerel M2M100 CTranslate2 modeliyle aynı önbelleği doldurur. 2026-10-12 anlık görüntüsü MIT lisanslı facebook/m2m100_418M modelini, `1aeed44db4dd61a486bba44acf54c76507082a2c` revizyonundaki JustFrederik/m2m_100_418m_ct2_int8 dönüşümü olarak kullanır. CTranslate2 4.8.2 ve `SentencePiece` 0.2.2 yalnızca derleme araçlarıdır. MAKRIVO ile model ağırlıkları veya çeviri bağımlılıkları dağıtılmaz. Önbelleği doldurduktan sonra ağ isteği olmadan üretmek için `--cache-only` kullanın. Yerel yardımcı uzak model kodunu indirmez veya çalıştırmaz.

```powershell
python -m pip install ctranslate2==4.8.2 sentencepiece==0.2.2
python tools/translate_documentation_local.py --model C:\models\m2m100-ct2 --refresh-sources
python tools/localize_documentation.py --cache-only
python tools/check_documentation.py
```

`--refresh-sources` olmadan işlem dondurulmuş kaynak anlık görüntüsünden devam eder. Özgün belgeleri düzenledikten sonra yenileme seçeneğini kullanın. Önbellekteki parçalar yeniden kullanılır. Kısmi çalıştırmada `--locales` ile dil kodları seçilebilir; yayımlamadan önce tüm dilleri tamamlayın. Kesintiye uğrayan işlemler başarısız çevirinin yerine sessizce çevrilmemiş metin koymaz.

Bildirim dosyası her kaynak ve üretilen dosyayı, normalleştirilmiş UTF-8 metnin SHA-256 parmak iziyle kaydeder: LF satır sonları kullanılır ve bayt sırası işareti bulunmaz. Çevrimdışı denetleyici her dilin tam kapsamını, parmak izlerini, yerel bağlantıları, kod örneklerini, tablo/CSV yapısını, kaynak URL’lerini ve değiştirilmemiş yer tutucuları doğrular. Güncelliğini yitirmiş kaynakları reddeder. Yapısal denetimin geçmesi ana dil incelemesi veya hukuki onay değildir. Üretilen sayfalar bu açıklamayı görünür biçimde taşır.

## Yayın ve Paketler

Belgeleri, generatörü ve kontrolörü bir araya getirin. Orijinal üçüncü taraf lisans koşullarını koruyun; tercüme edilen lisans analizi bilgilendiricidir. Yapım eserleri, çeviri cache ve yerel orkestrasyon dosyaları Git'e ait değildir. Kaynak-doküman yayıncılığı kendisi bir Windows yüklemeyi yayınlamaz.

Kullanıcı arayüzü çevirileri `src/Aivana.Core/Localization` altında ayrı JSON kataloglarıdır. Belge üretimi bu katalogları değiştirmez. Uygulama derlemesi ve belgeleri aynı sürümlenmiş anlık görüntüyü paylaşmalıdır; yeni açıklanan deneysel çalışma zamanı desteği kurulu paketten önce mevcut olabilir.
