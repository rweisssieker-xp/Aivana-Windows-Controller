<!-- generated-doc locale=tr-TR source=docs/licensing/2026-10-12-commercial-license-audit.md sha256=b58c6861e0b38c4495265651b5ab10349c701d8fb4582dd7d7cae5d2d865bc5f -->

> Makine destekli çeviri. Yapı, kod örnekleri ve bağlantılar kontrol edilir; bu, ana dili konuşan birinin incelemesi veya hukuki onay anlamına gelmez. Geçmiş raporlar özgün tarihlerini ve kapsamlarını korur. [Orijinal Kaynak](../../licensing/2026-10-12-commercial-license-audit.md)

# MAKRIVO: Ticari tek satış için test

Şekil : 12 Ekim ayı 2026 Üretici sayısı: Aivana

## Sonuçlar

Kontrol edilen varlıkta, kendi kaynak kodunu açmak için genel bir yükümlülük tespit edilmedi MAKRIVO. Bir tek satış mümkün görünüyor, ancak henüz tam olarak hazırlanmamıştır. Özellikle mevcut Corsair SDK DLL için iletim izni kanıtlanmamıştır. Bu inceleme yasal serbest bırakma yerine geçmez.

## Teknik ve lisanslı bulgular

| bileşenleri | Bulundu | Gerekli adımlar |
| --- | --- | --- |
| Kendi Kaynak Kodu | Kendi LICENCE, COPYING veya NOTICE incelenen GIT içeriğinde ve yerel olarak kullanılabilir lisans dosyası geçmişi bulunmamaktadır. | Hak sahibi olmak, katkıları, alınan kod parçaları ve işverenin olası haklarını doğrulamak; kendi kullanım koşullarını oluşturmak. |
| .NET ve WindowsDesktop | Araştırılan bağımsız paket `artifacts/makrivo-win-x64` versiyonu 10.0.6 içerir. Yerel koşu paketleri, ticari teslimat için izin veren ve bildirimlerinin iletilmesini gerektiren MIT lisanslarını içerir. Araştırılan çıkış sıralamasında ilgili lisans / not dosyaları bulunamadı. | Doğru lisans metinleri ve gerçek üçüncü taraf bildirimleri, satış paketine dahil edilmektedir. |
| NuGet İlişkiler | Araştırılan proje dosyalarında doğrudan paket referans yazıları bulunamadı. Araştırılan masaüstü varlık dosyası kendi proje yükümlülükleri içerir. Ancak, Runtime paketleri lisans açısından önemlidir. | Son yayın sırasında, tüm projeleri, geçici paketleri ve iletilen dosyaları yeniden kaydetmek. |
| Corsair iCUE SDK için | `artifacts/win-x64/iCUESDK.x64_2019.dll`, dosya versiyonu 4.0.84.0 Yerel SDK referans PDF, baskı sayfalarında 35–38 EULA'yı içerir: Ücretsiz kullanım, ancak iletim / teslimat için kısıtlamalar. Müşterilere DLL'yi aktarma izni belirlenmemiştir. | Birleştirmeden önce yükleyici bir teslimat izni alın. Alternatif olarak, müşterinin tarafında yüklü bir SDK kullanan SDK olmayan bir paket kontrol edin; bu SDK koşulları da geçerlidir. |
| Farklı paketler | `artifacts/makrivo-win-x64` içerir bu Corsair-DLL değil; `artifacts/win-x64` Uygulama, SDK'yı çeşitli yerlerde dinamik olarak arıyor. | Gerçek satıcıyı belirleyin ve içeriğini kontrol edin. DLL olmadan bir paketin varlığı henüz tüm entegrasyon haklarını doğrulamıyor. |
| OBS | Kendi WebSocket müşterisi incelenen C# kodunda; hiçbir OBS müşteri paketi, OBS ikili veya alınan GPL kodu tanımlanmamıştır. | Gözlemlenen protokol entegrasyonu, GPL'nin belirlenmiş bir açıklama yükümlülüğüne dayanmaz. Mümkün olan parçaların kaynağı doğrulanmalıdır. |
| App Simbolları | Yerel Python bir senaryo, geometrik sembollerle Pillow işaretlenir. Bu araç bir yapı bağımlılığıdır. | Gerçekten satılan tüm resimlerin, seslerin, yazıların ve diğer varlıkların kaynağını ve haklarını belgelemek. Tek başına yazılar her varlığın haklarını kanıtlamaz. |
| lisans yönetimi | Tam satın alma lisansı / etkinleştirme kontrolü incelenmiş uygulama kodunda tespit edilmemiştir. | İsterseniz, imzalanan çevrimdışı lisans dosyalarını uygulayın. Teknik aktivasyon ve yasal kullanım koşulları ayrı görevlerdir. |

## Anlamsız Satış Modeli - henüz uygulanmadı

- Tek ödeme; satın alınan ana versiyonda kalıcı kullanım hakkı.
- Bir kişi iki kendi PC'ye kadar; profesyonel kullanım açıkça izin.
- Hata düzeltmeleri ve daha küçük güncellemeleri içerir; sonraki ana sürümleri ücretsiz olarak sunar.
- Çevrimdışı kontrol edilebilir, dijital imza lisans dosyası; özel imza anahtarları asla uygulama ile göndermek.
- Yasal haklar ve gerekli güvenlik güncellemeleri, ana sürüm kuralları tarafından sabit olarak dışlanamaz.

## İlk satıştan önce

1. Corsair ilave hakları açık veya SDK DLL'ye bağlı olmadan satış hazırlamak.
2. Lisans ve üçüncü taraf sözleşmeleri ekleyin ve yüklemeye alın.
3. Kendi koduna ve varlıklarına hak sahibi olmayı doğrulamak; EULA'yı cihaz, kullanıcı ve yükseltme kuralları ile oluşturmak.
4. Tüketici satışları için, bilgi yükümlülükleri ve iptal dahil Checkout işlenir. “Downloads’ta hiçbir geri ödeme” yeterli değil. İptal hakkının iptal edilmesi, hemen sağlanması durumunda yasal koşulları öngörür.
5. Pluginler ve tüm geçici bağımlılıklar da dahil olmak üzere bitmiş yüklemeyi yeniden kontrol edin. Çalışma kopyası paralel olarak genişletilir; bu dosya gelecekteki dosyaların yayınlanması değildir.

## GitHub

Soruşturma alanı kamuya açık. Açık kaynak lisansı olmadan, kod herhangi bir kullanım için otomatik olarak serbest bırakılamaz. Bununla birlikte, kamu kayıtları için GitHub platform hakları dikkate alınmalıdır. Daha sonra özel bir dönüşüm mevcut kopyaları veya forks kaldırmaz. Görünürlük ve lisans bu sınavda değiştirilmemiştir.

## Kanıt ve Kaynaklar

- Yerel .NET lisansları: `C:/Users/weiss/.nuget/packages/microsoft.netcore.app.runtime.win-x64/10.0.6/LICENSE.TXT` ve `C:/Users/weiss/.nuget/packages/microsoft.windowsdesktop.app.runtime.win-x64/10.0.6/LICENSE`
- Yerel .NET üçüncü taraf sağlayıcı bildirimi: `C:/Users/weiss/.nuget/packages/microsoft.netcore.app.runtime.win-x64/10.0.6/THIRD-PARTY-NOTICES.TXT`.
- Yerel Corsair SDK: `artifacts/dependencies/iCUESDK_4.0.84.zip`; `sdk/iCUESDK/doc/iCUE SDK - Overview and Reference.pdf` altında alınan referans PDF.
- [.NET 10.0.6 MIT lisansı](https://raw.githubusercontent.com/dotnet/runtime/v10.0.6/LICENSE.TXT)
- [Corsair EULA ile SDK belgeleri](https://raw.githubusercontent.com/CorsairOfficial/cue-sdk/master/docs/index.html)
- [GitHub: Repositories lisans](https://docs.github.com/en/repositories/managing-your-repositorys-settings-and-features/customizing-your-repository/licensing-a-repository)
- [BGB § 327f – Güncellemeler](https://www.gesetze-im-internet.de/bgb/__327f.html)
- [BGB § 356 – İptal hakkı](https://www.gesetze-im-internet.de/bgb/__356.html)

Denetlenmemiş: tam dış katkı geçmişi ve hak zinciri, marka serbest bırakma, mevcut satış şekku, gelecekteki eklenti paketleri, henüz mevcut olmayan bir EULA'nın tam yasal etkinliği.
