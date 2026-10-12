<!-- generated-doc locale=tr-TR source=docs/testing/2026-10-12-local-meeting-assistant.md sha256=56631ca85dca5f785448d12586b0741628728925a649f6124f7aebffb19bb615 -->

> Makine destekli çeviri. Yapı, kod örnekleri ve bağlantılar kontrol edilir; bu, ana dili konuşan birinin incelemesi veya hukuki onay anlamına gelmez. Geçmiş raporlar özgün tarihlerini ve kapsamlarını korur. [Orijinal Kaynak](../../testing/2026-10-12-local-meeting-assistant.md)

# Yerel AI Konuşma Asistanı - Prototype

Şekil : 12 Ekim ayı 2026 ForgeMind Hipotezi “Outcome Agent”, Sınırlı Toplantı Süresi. Hiçbir kanıtlanmış rekabet avantajı ve gerçek karşılaştırma denemeleri olmadan ölçülen zaman tasarrufu yoktur.

## Kullanım

1. Komut paleti → **KI görüşme asistanı** Deney standarttır ve konfigürasyon ithalatında yapılır. Portföy ihracatı geri çekildi.
2. Yerel Asistanları etkinleştirin. Ollama ayrı olarak çalıştırılmalıdır `127.0.0.1:11434`; uygun bir yerel model zaten yüklü olmalıdır. ** Yerel modeller yüklemek** ve model seçin. MAKRIVO hiçbir model/hizmet yüklemez, OpenAI/Cloud API ile iletişime geçmez ve API anahtarı kullanmaz. Yerel Ollama hizmetinin çevrimdışı yapılandırılması ve güvenilirliği kullanıcıya aittir; bildirilen uzaktan/gölge modelleri reddedilir.
3. Hedefleri girin ve açıkça programlar / belgeler / web siteleri ekleyin. Hedef, kaynak kimliği, adı ve türü iletilir; hedef yolu ve seçilen dosya içeriği ek olarak iletilmez; hedef giriş kendisi sözlü olarak iletilir. Web sitesi adları yalnızca şemayı/host içerir, tam bağlantı yerel kalır.
4. **AI Tasarımı**. Maksimum 90 saniye, 1024 Çıkış kutuları, sınırlı yanıt boyutu. hedef / model / kaynak değişiklikleri öngörmeyi reddediyor; iptal / kapatma isteği sona eriyor. model yalnızca seçilmiş kimlikler önerebilir. bilinmeyen kimlikler, ücretsiz komutlar, ek JSON alanları ve yürütülebilir belge türleri reddediliyor.
5. Önceden ve açık noktaları kontrol edin → **Editor'da kontrol edin**. Sonuç, öncelikle kaydedilmemiş bir tasarımdır. Sadece mevcut editörde kaydetmek ve gerektiğinde açıkça çalıştırmak. Normal makrovalitasyon, ön / inceleme, duraklama ve iptal sorumluluğu kalır.

Desteklenen: Programlar argüman olmadan başlatılır, açıkça seçilmiş yerel PDF/DOCX/XLSX/PPTX/MD açılır, HTTP(S) web siteleri açılır, açık isteğe bağlı olarak mikrofonu kapatılır. Hiçbir kulaklık değiştirme, hiçbir mesajlar, randevular, indirme, senaryo, ücretsiz tıklama veya metin girişleri. Görünmez arzular, modelin onları doğru bir şekilde tanımladığı sürece açık noktalar olarak görünür; profesyonel hedefleri kullanıcı tarafından kontrol edilmelidir. Belge açılımı otomatik olarak doğrulanmamıştır.

## karşılaştırma deneyimi

**Dönüşü ölçüm · Başlangıç / Son** tam manuel karşılaştırma süresi ölçülür. Asistan alımları, asistanın açılmasından editörde ilk depolama süresine kadar olan süreyi ölçer; `generated` ancak sadece planlama süresini içerir. Bu iki süreyi zaman tasarrufu olarak karşılaştırmayın.

Yerel olaylar konfigürasyon sıralamasında `Experiments/meeting-experiment.jsonl`: zaman çubuğu, tesadüfi deneme/kırışıklık kimliği, olay, süresi, düzeltme evet/hayır. Hedef metinleri, kaynak adları, yollar, model cevapları veya belge içeriği yoktur. Dosya 2 MiB'den döndürülür, bir önceki dosya kalır. Makro ve yapılandırma hataları kesilmemelidir.

Manual baseline, model oluşturma/hata/kırılma, tahmin edilmeyen değişiklikler/kaplama, editörün ilk depolama ve düzeltmeleri ve kaydedilen çubuklar için gerçek Runner son durumları. `run_completed` yalnızca Runner'ın tamamlandığı anlamına gelir; doğrulanmış hedef etkisi test sürecinde ayrı olarak yer alır. Aynı makro tekrarlamaları yeni bağımsız düzenleme denemeleri değildir. Başlangıç deneyi yeniden başlatıldıktan sonra devam edebilir, çünkü deneme kimliği kaydedilen çubukta kalır. Çoğaltılmış köpekler bu kaynağı paylaşır; deneme kimliğine göre değerlendirirken gruplandırılır.

İlk deneme: 20 benzer toplantı tesisleri veya iki hafta gönüllü test kullanıcıları ile; öncelikle iç kullanım. Manuel kurulum süresini tam destekli kurulum süresi ile karşılaştırın; ek olarak düzeltme/düzeltme oranı, koşu hataları ve gerçek test sonuçlarını değerlendirin. Koruma sınırlarının düzeltilmesi veya ihlali durumunda, anahtarı devre dışı bırakın. Repository'de herhangi bir kullanım veya ödeme isteği kanıtlanmamıştır. Otomatik telemetrik aktarım veya fiyat ayarlaması yok.

## Deneme Durumu

- Temel: Sınırlamalar, türler, bilinmeyen / çoğaltılmış kimlikler, senaryo kapatma, uzun bağlantılar, seçmeli alt yapılandırma, ithalat / ihracat kapatma, veri tasarrufu ölçümleri ve tek bir reddetme ve doğru iletim.
- Sahte model ile nakliye: Ollama JSON şeması, Loopback hedefi, prompt dosya çizgisi yok, bulut adı ve uzaktan kumanda reddedildi, hasar görmüş / uzun cevap reddedildi.
- WPF-Smoke: diyalog azaltılır; Tasarım teslimat kaydedilen kayıpları değiştirmez, inceleme/gizleme kaynağı alır ve düzeltme kaydeder, reddedilemez hiçbir şey kaydedilir.
- Bağımsız inceleme: Önceden reddedildi ve uzun bağlantılar bulundu ve düzeltildi; Regresyon testleri RED→GREEN.
- Sıfır uyarılar/hata, tüm çekirdek çerçeve ve WPF smoke ile kurulumu başarıyla. Tüm Windows suit başlangıçta odak değişikliği de dahil olmak üzere oluşuyordu. Tekrar bir koşu reddedilen temel değişikliğinde başarısız oldu; test bağlamlı ve değişmez kaldı. Model taşımacılığı da dahil olmak üzere kalan Windows sınavlar, açıkça reddedilen bir başlangıç sınavına sahiptir.
- Paket incelemesi, kısa sürede açılmış yapılandırma dosyalarının nükleer değişiminde mevcut bir hatayı ortaya koydu. Bir okuma engeli aynı erişim reddedilme hatası çoğaltılmıştır; yalnızca atom değişikliği sınırlı tekrarlama RED→GREEN kontrol edilmiştir. Kalıcı erişim hataları görülebilir kalır.
- Mevcut bağımsız paket `artifacts/release-20261012-184105`: WPF hafıza düzeltme sonrası zehir, AI diyaloğu ve tasarım kabul / reddetme dahil.
- Gerçek bir model testi yok: yerel Ollama servis sınavda cevap vermedi. Model kalitesi, yavaşlama, semantik bütünlük veya zaman tasarrufu hakkında hiçbir açıklama yoktur. Hardware/Driver/Skalerasyon sınırları mevcuttur.

Protokol Kaynakları : [Ollama İletişim](https://docs.ollama.com/api/chat), [Yapılandırılmış çıkışlar](https://docs.ollama.com/capabilities/structured-outputs), [Uzaktan Model Metadata dahil resmi tip tanımları](https://github.com/ollama/ollama/blob/main/api/types.go).
