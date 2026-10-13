<!-- generated-doc locale=tr-TR source=docs/testing/2026-10-12-local-ai-runtimes.md sha256=3dfbb80761aad758ea6a9307999ff530dfdd4a0cf685804e15052508cf107fd7 -->

> Makine destekli çeviri. Yapı, kod örnekleri ve bağlantılar kontrol edilir; bu, ana dili konuşan birinin incelemesi veya hukuki onay anlamına gelmez. Geçmiş raporlar özgün tarihlerini ve kapsamlarını korur. [Orijinal Kaynak](../../testing/2026-10-12-local-ai-runtimes.md)

# Yerel IŞ Zamanları

## Hizmetler

Araçlar → AI konuşma asistanı açın, açıkça etkinleştirin ve AI süresini seçin. Kaynakları paylaşmak, hedef oluşturmak, tasarım oluşturmak ve editörde incelemek. Hiçbir üretici projeyi otomatik olarak yürütmüyor. Otomatik servis değişimi yoktur.

| Çalışma Süresi | Önlemler | Bağlantı |
|---|---|---|
| Ollama | Yerel yüklü model | 127.0.0.1:11434 |
| LM Studio | Yerel yüklü model ve JSON şeması desteği ile çalışan yerel sunucular | 127.0.0.1:1234 |
| llama.cpp | Yerel model ve JSON şeması desteği ile çalışan llama sunucusu | 127.0.0.1:8080 |
| ONNX Çalışma Süresi GenAI CPU | uyumlu, güvenilir yerel GenAI metin modeli düzenleyicisi | ayrı bağlantılı süreç, HTTP değil |

HTTP müşterileri Proxys kullanmıyor ve yönlendirmeleri takip etmiyor. Yerel sunucular başka hizmetlere bile ulaşabilir; özellikle LM Link/Remote Routing yerel işlem için kullanılamaz. Bu MAKRIVO uyumlu arayüzü tarafından güvenilir bir şekilde tespit edilemez. Ollama uzaktan veri ve bulut isimleri ek olarak reddedilir. Bu sürümde doğrulanmış yerel sunucular yapılandırılamaz.

## ONNX

İletişim kutusunda genai_config.json seçin. Klasörde tokenizer.json ve sohbet şablonu bulunmalıdır. Dize türünde chat_template, default adlı şablon (Array veya Dictionary) ve satır içi şablon yoksa veya null ise ayrı chat_template.jinja desteklenir. Belirsiz şablonlar reddedilir; en fazla 65536 karakter. Rastgele .onnx dosyaları, Vision/Embedding modelleri veya yalnızca GPU/NPU dışa aktarımları toplantı modelleri değildir.

Birleştirilmiş: Microsoft.ML.OnnxRuntimeGenAI 0.17.1, CPU Tedarikçi konfigürasyonu CPU'ya iade edilir. Pakette indirme ve model ağırlığı yok. Python yükleme gerektirmez. Sadece güvenilir model paketleri kullanın; ayrı süreç bir çökme / çökme sınırı, güvenlik sandbox değil.

Sınırlar: 90 saniye dahil model yükleme, maksimum 3072 Prompttokens ve 1024 üretilen tokens, ek model bağlam sınırı; sıkı bir şekilde kontrol edilen JSON ve tanınmış kaynak kimlikleri. Yanlış / kesilmiş cevap reddedilecektir. İşe başlamak / işe başlamak işe başlamaktır. Ücretsiz model komutları ve Prompts'ın ayrı kaynak listesinde programlar / dosya yolları yoktur; Kullanıcı tarafından yazılan hedef metni bu bilgileri içerebilir.

## Kontrolü

- Core Suite, simüle edilmiş LM stüdyoları/llama.cpp/Ollama yanıtları ile Loopback taşıma testleri, şeması/çıkış/resur kontrolü.
- En uzun hedefler ve 24 kaynak adıyla Unicode protokol regresyonu.
- Microsoft/onnxruntime-genai, Commit 5c19002edce50beaa3a5c7eeefd8fe5269ee8d19'dan resmi tiny-random-gpt2-fp32-Testgraph ile gerçek yerli CPU referansı. Test şablonu tamamlar ve küçük tesadüfi grafik için byte tokenizer ayarlanır. Bu tesadüf modeli kaliteyi kontrol etmiyor ve serbest bırakılmıyor.
- Yeniden üretilebilir yerli çalışma testi: tools/check-onnx-runtime.ps1 -ModelDirectory <GenAI model sırası>.
- WPF Smoke, dört sağlayıcının seçimini ve test seviyesini inceler; tek başına seçim, bir model isteği oluşturmaz.

## Gerçek serbest bırakma

Hardware/Smartphone/Tablet/Monitor Değiştirme Testleri devam ediyor. Eksklusif klavye baskısı, imzalanan, lisanslı ve kontrol edilen filtre sürücüleri olmadan engellenir. Düşük veya Raw Input ile değiştirilmez. Fonksiyon değişikliği Windows ile engellenmeye devam edebilir; hatası artık hedef penceresinin manuel etkinleştirilmesi olarak adlandırılır. Tüm 27 kataloglarda çeviriler eklendi; ayrıntılı yeni metinler ve profesyonel çeviri eksikliği hala açıktır. Uygun bir öğretim modeli, sürekli çalışma ve ölçülen zaman kazanımı ile canlı kalite tesadüf / sahte HTTP testleri tarafından kanıtlanmamaktadır.

## Kaynaklar

- https://onnxruntime.ai/docs/genai/api/csharp.html
- https://www.nuget.org/packages/Microsoft.ML.OnnxRuntimeGenAI/0.17.1
- https://lmstudio.ai/docs/developer/openai-compat/chat-completions
- https://github.com/ggml-org/llama.cpp/blob/master/tools/server/README.md

Son test durumu: Release derlemesinde uyarı/hata yok; yerel odak test düzeneği, HTTPS, UIA, Plugin-IPC ve uyumlu AI taşıma testleri dahil tam Core ve Windows süitleri geçti. Yeni paket release-20261012-190618: WPF-Smoke geçti (19:07), paketteki ONNX-CPU çıkarımı test grafiğiyle geçti. Bağımsız inceleme: Unicode protokol sınırı düzeltildi, başka somut bulgu yok. Buradan fiziksel üretici/telefon/monitör kabulü çıkarılmaz.

## Sonraki: Gerçek model kalitesi ve örnekler

allama-ai/SmolLM-360M-Instruct-onnx-int4 ile ek CPU çalıştırması, revizyon 82a3d1a8a86c8053f2a859a90ed4d344fc0725b0: yerel çıkarım testi geçti. Model ağırlıkları SHA256: 48e9c4ef12f7f995ca11fc7020b39990aa27bc67cc0fe116067676a000a04429. Topluluk dışa aktarımı yalnızca test alanındadır, dağıtılan sürüme dahil değildir.

Dört gerçek planlama testi (Almanca/İngilizce, belge, program, açık mikrofon sessize alma isteği): **0/4 geçerli plan**; katı yanıt doğrulamasında her seferinde JsonException. Süreler 63501, 6376, 72118 ve 6774 ms. Hiçbir eylem yürütülmedi. Bu küçük model planlama sözleşmesini karşılamaz ve uygun model olarak önerilmez. Rapor: artifacts/local-ai-evaluation/smollm360-results.json. Yeniden üretme: Windows-Tests --evaluate-onnx <Modellordner> <Berichtspfad>. Çalışma zamanının çalışması model kalitesini kanıtlamaz.

Araç gezinmesi ve test raporu ayrıca 27 dil kataloğunun tamamında çevrildi. Uzun metinler ve dil kabul incelemesi henüz tamamlanmadı. Şablon regresyonu yanlış JSON türlerini, boş/belirsiz/aşırı uzun şablonları ve Jinja yedeğiyle null durumunu da denetler.

Güncellenmiş paket: artifacts/release-20261012-201134. Paketli WPF-Smoke 12.10.2026 tarihinde 20:12’de geçti; ayrı Instruct test modeliyle yerel CPU çıkarımı paketteki Worker içinde geçti. Core ve Windows-KI testleri geçti; derlemede uyarı/hata yok. Modelin planlama kalitesi hâlâ 0/4.
