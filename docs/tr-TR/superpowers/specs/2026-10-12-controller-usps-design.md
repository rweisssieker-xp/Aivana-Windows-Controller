<!-- generated-doc locale=tr-TR source=docs/superpowers/specs/2026-10-12-controller-usps-design.md sha256=4c3b2dbedb83af6bd48917515ae6559012afb6318ce69d1e32fb2765e0793327 -->

> Makine destekli çeviri. Yapı, kod örnekleri ve bağlantılar kontrol edilir; bu, ana dili konuşan birinin incelemesi veya hukuki onay anlamına gelmez. Geçmiş raporlar özgün tarihlerini ve kapsamlarını korur. [Orijinal Kaynak](../../../superpowers/specs/2026-10-12-controller-usps-design.md)

# MAKRIVO kontrol uzantıları

Kullanıcı, teklif edilen dokuz iyileştirme ve tanıdık bir yastık simgesi istedi. Mevcut konfigürasyonlar, özellikle G1/G2 parlaklık, uyumlu kalmalıdır. Mevcut marka ve belge düzenlemeleri ortak çalışma alanına aittir ve korunmalıdır.

## Teslimat ve Sınırlar

1. Işık eylemleri tüm destekli monitörleri veya sabit bir monitör kimliği seçin. Açıklanan hedefleri görsel olarak başarısız hale getirmek. Dashboard, mevcut parlaklık, çıkış hacmi, mikrofon durumu ve sınırlı asinkron okuma yoluyla OBS durumu gösterir.
2. Özel anahtarlar hemen baskı, dokunma, tutma ve çifte dokunma desteği ile, tekrarlayan bastırma. Bir katman seçicisi geçici olarak aktif haritalama değiştirir ve serbest bırakır. Mevcut bağlar davranışlarını korur. Dinlenme, profil değişiklikleri, bağlantı kesintileri ve iptal açık tutan hareketler.
3. Çalışma akışı tasarımcısı koşulları, ilerleme şubeleri ve durdurma / devam başarısızlık politikası bir çerçeve taslağı ile düzenler. Şartlar dosyaları, çalıştırma süreçleri ve pencere başlıkları kapsar. Sınırsız atlama döngüleri yok.
4. Açıklayıcı kayıtlar kısaltma kodları ve beklemeyi yakalar, enjekte edilen girişleri görmezden gelir ve kaçışta durur. Kayıtlı eylemler kabul edilmeden önce gözden geçirilir; sıradan metin kaydedilmez.
5. Diğer üreticilerden standart özel anahtarlar kullanılır Windows Raw Input. Mülkiyet anahtarları, destekli bir satıcı API veya satıcı anahtarına haritalama gerektirir; desteklenmemiş işlevselliği dürüstçe gösterilir. Diagnostik profil, duraklama, bağlayıcı çatışmalar ve gerçek adaptör durumu açıklıyor.
6. Profil değişimi, kullanıcı tarafından seçilen paylaşılan bir klasör kullanır (eski bir bulut istemcisi tarafından senkronize edilmiş klasörler de dahil). Kontrol kontrolü sessiz aşırı yazıları önler. Pull değişiklikleri tahmin eder ve donanım haritalama kaldırma ile devre dışı bırakılmış eylemler. Etkinlik içeriği açık. Hiçbir bulut kimlikleri kaydedilmez MAKRIVO.
7. UI zincirleri mevcut 27 dil kataloğuna aittir; DE/EN/FR/IT en az incelenen belge dilleri kalır. Çeviri boşlukları, iddia edilen yerine açıklanmalıdır.
8. Tanıdık bir teal M klavye işareti genetik Windows klavye simgesini değiştirir. Amber Pause moduna sahiptir. Aynı marka kullanır / aynı marka kullanır. Tooltip durum ve profil içerir.

## Kontrolü

Temel kontroller arka arkaya uyumluluk, snapshots, iptal, hareket zamanlaması, çalışma akışı dalları / hataları, kayıt filtresi ve senkronizasyon çatışmaları kapsamaktadır. Windows kontroller sadece okuma görüntüleme listesi, SDK serbest bırakma olayları ve Raw Input parsing. WPF duman kontrolleri gerçek monitör parlaklığını değiştirmeden yeni giriş noktaları ve kaynakları kapsamaktadır, başka bir uygulama veya kullanıcı profilleri yazdırmak. Yükleme bir yedekleme ve G1/G2 yapılandırma korur.
