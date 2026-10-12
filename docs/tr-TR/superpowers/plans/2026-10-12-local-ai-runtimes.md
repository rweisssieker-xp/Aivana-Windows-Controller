<!-- generated-doc locale=tr-TR source=docs/superpowers/plans/2026-10-12-local-ai-runtimes.md sha256=f24c6b3bce4d0b4dd9d42732c363941a36644f90cf8d664d61ea0c60eb950079 -->

> Makine destekli çeviri. Yapı, kod örnekleri ve bağlantılar kontrol edilir; bu, ana dili konuşan birinin incelemesi veya hukuki onay anlamına gelmez. Geçmiş raporlar özgün tarihlerini ve kapsamlarını korur. [Orijinal Kaynak](../../../superpowers/plans/2026-10-12-local-ai-runtimes.md)

# Yerel AI süresi ve kalan sınavlar

Hedef: Daha fazla yerel AI çalışma saatleri gerçekten bağlamak, tasarım incelemesi almak ve açık yayınları görülebilir hale getirmek. Kullanıcı soru sorulmamış uygulamaya izin verir. Mevcut değişiklikler devam ediyor.

1. Loopback taşımacılığı ve uygulama öncesi ONNX paket kontrolü için testler; ağ adresleri, harcama sınırları, bozulma ve bilinmeyen kaynakları incelemek.
2. Ortak Planlayıcı Haberleri; Ollama Alınan, LM Studio (127.0.0.1:1234) ve llama.cpp (127.0.0.1:8080) Hiçbir bulut URL'leri veya otomatik sağlayıcı değişiklikleri. yerel sunucular kendi başlarına yönlendirebilir; işleyişi açıkça operatörün sorumluluğu kalır.
3. ONNX Runtime GenAI CPU kendi süreci ile zaman sınırı, sınırlı JSON protokolü ve işlem kesintisi. Model sırası açıkça seçilir; hiçbir model otomatik olarak indirilir. Sadece GenAI uyumlu metin modelleri, herhangi bir .onnx ağı yok.
4. WPF seçenekleri çalışma süresi, model, ONNX dosyaları için; hepsi aynı sıkı denetlenmiş tasarım sağlar.
5. Hardware, DPI, mobil panel ve özel makropad için açık sınırlar ile deneme alanı. Fiziksel testler ve imzalanmış / lisanslı bir sürücü burada yazılım ile değiştirilemez.
6. Release-Build, Core/Windows testleri, WPF koku, işçi/paket kontrolü ve bağımsız inceleme. Yeni tam paket oluşturmak; model kalitesi yalnızca mevcut model için test edilmiş olarak bildirmek.

Sonuç: dört çalışma süresi uygulanır, ONNX nativ ayrı test edilmiş, tedarikçi seçimi ve deneme durumu WPF paketli koku. Core / Windows yeşil çerçeve İnceleme, ortak bir sabitlik ve regresyon ile birikim sınırı düzeltildi. Gerçek donanım / mobil / DPI değişimleri, sürücü serbest bırakma, tam profesyonel çeviri ve canlı instrukt kalitesi açık çalışma kalır; tam olarak kullanılmamıştır.

Devam: ayrı jinja ve adlandırılmış/default örnekleri uygulanır; tip/sınır/falback regresyon testleri geçti. Araç navigasyonu 27 kataloğunda tamamlanmıştır. Gerçek öğretim modeli ayrı indirildi ve SHA256 kontrol edildi: yerli CPU referans çalışıyor ama 0/4 planlama durumları JSON sözleşmesi ile uyumludur; hiçbir eylem yürütülmemiştir. Bağımsız inceleme: sıfır düşüş düzeltildi ve test edildi, daha sonra belirli başka bulgular yok. Temel ve Windows KI testleri geçti, Release-Build 0 uyarıları / hataları.
