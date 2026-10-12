<!-- generated-doc locale=tr-TR source=docs/superpowers/specs/2026-10-08-keyboard-mapping.md sha256=fb766a2a2b4698ae3e1c3f63b50bb4f5c605da3dd56c61c05fcc0832b5cf8aeb -->

> Makine destekli çeviri. Yapı, kod örnekleri ve bağlantılar kontrol edilir; bu, ana dili konuşan birinin incelemesi veya hukuki onay anlamına gelmez. Geçmiş raporlar özgün tarihlerini ve kapsamlarını korur. [Orijinal Kaynak](../../../superpowers/specs/2026-10-08-keyboard-mapping.md)

# Mapping için Fiziksel Klavye Görünümü

[English (US)](2026-10-08-keyboard-mapping.md) | [Deutsch](2026-10-08-keyboard-mapping.md) | [Français](2026-10-08-keyboard-mapping.md) | [Italiano](2026-10-08-keyboard-mapping.md)

## Hedef

Tıklama görüntüsü, eylemleri atarken gerçek düğme pozisyonlarını gösterir. Aynı geniş çizgiler yeterli değildir: Enter, Shift, Space, Navigation, Numaralar Bloku ve Üretici Ek Kontrol Elemanları farklıdır.

## Uygulama

- Ayrı fiziksel kimlikler, klavye birimleri geometri, basılı efsaneler ve Windows giriş kodları ile katalog.
- ISO-DE ve ANSI-US her biri tam biçim ve TKL olarak; ek olarak Corsair K100 RGB, iCUE üzerinden birleştirilmiş model olarak tanınmıştır.
- ISO Enter L-Form, yüksek artı/giriş düğmeleri ile sayı blokları ve geniş sıfır; navigasyon ve kendi konumlarında merdivenler.
- Modeli/Layout seçeneğini açıkça seçin. USB VID/PID ve model adı herhangi bir dil varyantı kanıtlamaz. Genetik profiller ve üretici belgelerine göre modellenen profiller farklı olarak tanımlanabilir; milimetre veya firmware uyumluluğu garantisi yoktur.
- Scan kodu aktif Windows giriş ayarında görüntülenir, böylece bir fiziksel düğme tıklaması yanlışlıkla ABD ve DE arasındaki değişimde Y/Z gibi başka bir düğmeye girmez.
- Ayarlama mevcut eylem editöründe bir tasarıma sahiptir. Depolama, çatışma incelemesi ve reddetme mevcut mekanizmaları kalır. Click’lar harekete geçmiyor.
- Mülkiyet G düğmeleri, profil / kilit düğmeler ve iCUE radı görüntülenir, ancak uygulanan giriş adaptörü olmadan normal Windows hotkeys olarak yayınlanmaz. Yapılandırılabilir üretici kısayolları mümkün bir yol kalır.
- Yerleşimler küresel Windows hotkeys kalır; bir cihaz görüntüsü, bir cihazın özel baskısı veya tek bir fiziksel tuşunun özel yerleştirilmesi anlamına gelmez.

## Belgeler

[K100-Handbuch İşlem Elemanları ile](https://assets.corsair.com/image/upload/corsairmedia/sys_master/productcontent/AP_K100_RGB_Mechanical_QSG_AD.pdf), [Corsair ANSI / ISO Açıklaması](https://www.corsair.com/au/en/explorer/gamer/keyboards/ansi-vs-iso-whats-the-best-keyboard-layout/).

## Sınav

Geometri, varyant ve giriş kodu testleri; yapılandırma ve mevcut çekirdek/Windows testleri; gerçek kullanıcı konfigürasyonunu gerçekleştirmeden ve değiştirmeden görüntüleme ve ayarlamak için WPF çubukları. Üretici Araştırmaları, mevcut düzenleme kataloğuna bakılmaksızın daha fazla model / SKU gereksinimlerini belgelemektedir.
