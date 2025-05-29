# StockControl - Stok Yönetim Sistemi

## Proje Hakkında
Bu proje, "StockControl" adında bir stok yönetim sisteminin geliştirilmesi ve Docker konteynerları üzerinde çalıştırılması sürecini kapsamaktadır. Proje, küçük ve orta ölçekli işletmelerin stok takibini kolaylaştırmak, makine ve parça yönetimini etkin bir şekilde gerçekleştirmek amacıyla geliştirilmiştir. ASP.NET Core framework’ü ve Docker teknolojisi kullanılarak modern bir yazılım çözümü sunulmuştur.

Proje, işletmelerin günlük operasyonlarında karşılaştıkları manuel stok yönetim zorluklarını azaltmayı ve bu süreçleri dijital bir platformda otomatik hale getirmeyi hedeflemektedir. Makine ve parça bilgilerinin eklenmesi, güncellenmesi ve görüntülenmesi gibi temel işlevsellikler sağlanmıştır.

## Proje Amacı ve Kapsamı
StockControl projesinin temel amacı, işletmelerin stok yönetim süreçlerini dijitalleştirmek ve bu süreçleri Docker konteynerları ile platform bağımsız, ölçeklenebilir ve kolay dağıtılabilir bir hale getirmektir. Uygulama, makine ve parça bilgilerinin saklanmasını, eklenmesini, güncellenmesini ve görüntülenmesini sağlayan bir web tabanlı sistem sunar. Proje kapsamında, temel stok yönetimi işlevleri geliştirilmiş ve başlangıç verileriyle (örneğin, "Balya Makinesi" ve "Balya" gibi örnekler) test edilmiştir.

### Kapsam Dışı Bırakılanlar
- Gelişmiş raporlama ve analiz özellikleri.
- Kullanıcı rolleri ve yetkilendirme.
- Bulut tabanlı yedekleme.

## Kullanılan Teknolojiler
Proje geliştirme sürecinde aşağıdaki teknolojiler ve araçlar kullanılmıştır:
- **ASP.NET Core**: Web uygulaması geliştirmek için tercih edilen modern bir framework’tür. MVC mimarisi sayesinde kullanıcı arayüzü ve iş mantığı ayrı bir şekilde tasarlanmıştır.
- **SQLite**: Hafif ve dosya tabanlı bir veritabanı olarak seçilmiştir. Küçük ölçekli projeler için uygunluğu ve kolay entegrasyonu nedeniyle tercih edilmiştir.
- **Docker**: Uygulamanın konteynerleştirilmesi ve farklı işletim sistemlerinde tutarlı bir şekilde çalıştırılması için kullanılmıştır.
- **Visual Studio**: Geliştirme ortamı olarak kullanılmış, Docker entegrasyonu sayesinde konteyner oluşturma ve test etme süreçleri kolaylaştırılmıştır.

## Kullanılan NuGet Paketleri
Proje, aşağıdaki NuGet paketlerine bağımlıdır. Bu paketler, uygulamanın çalışması için gereklidir. Eğer projeyi Visual Studio’da açtığınızda bu paketler eksikse, aşağıdaki adımları takip ederek yükleyebilirsiniz:

- **Microsoft.EntityFrameworkCore**: Veritabanı işlemleri için Entity Framework Core kullanılmıştır.
- **Microsoft.EntityFrameworkCore.Sqlite**: SQLite veritabanı ile entegrasyon için gerekli.
- **Microsoft.EntityFrameworkCore.Design**: Veritabanı migration’ları ve tasarım zamanı araçları için.
- **Microsoft.AspNetCore.DataProtection.EntityFrameworkCore**: Veri koruma anahtarlarını veritabanında saklamak için.

### NuGet Paketlerini Yükleme
1. Visual Studio’da projeyi açın (`StockControl.sln`).
2. "Tools" > "NuGet Package Manager" > "Manage NuGet Packages for Solution" seçeneğine gidin.
3. Yukarıdaki paketleri arayın ve yükleyin.
4. Alternatif olarak, aşağıdaki komutları Package Manager Console’da çalıştırabilirsiniz:

**Not**: Eğer Visual Studio’da otomatik restore özelliği açıksa, projeyi açtığınızda NuGet paketleri otomatik olarak indirilecektir.

## Proje Geliştirme Süreci
StockControl projesi, aşağıdaki adımlarla geliştirilmiştir:
1. **Uygulama Tasarımı**: ASP.NET Core MVC kullanılarak bir web uygulaması oluşturulmuştur. `program.cs` dosyasında, uygulamanın `4209` portunda çalışması için `builder.WebHost.UseUrls("http://+:4209")` yapılandırması yapılmıştır.
2. **Veritabanı Entegrasyonu**: SQLite veritabanı ile veri saklama ve yönetim işlemleri gerçekleştirilmiştir. Başlangıç verileri olarak "Balya Makinesi" (Çelik gövde, Mil) ve "Balya" (Tekerlek, Toplama yayı) gibi örnek makineler eklenmiştir.
3. **Docker Konteynerizasyonu**: `Dockerfile` ve `docker-compose.yml` dosyaları hazırlanarak uygulama konteynerleştirilmiştir. `docker-compose.yml` dosyasında `ports: - "4209:4209"` ile port eşlemesi yapılmış, `ASPNETCORE_URLS` ortam değişkeni ile uygulamanın portu sabitlenmiştir.
4. **Test ve Dağıtım**: Uygulama, `docker-compose up` komutuyla çalıştırılmış ve `http://localhost:4209` adresinde test edilmiştir.

## Çalıştırma Talimatları
Proje, iki farklı şekilde çalıştırılabilir: Visual Studio’da normal F5 ile veya Docker kullanılarak. Aşağıda her iki yöntem için detaylı talimatlar verilmiştir.

### 1. Visual Studio ile Çalıştırma (F5)
1. Depoyu klonlayın:
2. Proje dizinine gidin ve `StockControl.sln` dosyasını Visual Studio ile açın.
3. NuGet paketlerinin yüklendiğinden emin olun (yukarıdaki "Kullanılan NuGet Paketleri" bölümüne bakın).
4. Visual Studio’da F5 tuşuna basarak projeyi çalıştırın.
5. Tarayıcıda `http://localhost:4209` adresini açın.

**Not**: Eğer Visual Studio’da port farklıysa, `launchSettings.json` dosyasını kontrol ederek `4209` portunu kullanın.

### 2. Docker ile Çalıştırma
1. Depoyu klonlayın:
2. Proje dizinine gidin:
3. Docker Desktop’ın çalıştığından emin olun.
4. PowerShell veya terminalde aşağıdaki komutları sırasıyla çalıştırın:
- Önce Docker imajını oluşturun:"docker-compose build"
- Ardından konteyneri çalıştırın:"docker-compose up"

Tarayıcıda `http://localhost:4209` adresini açın ve uygulamayı gözlemleyin.

## Ekran Görüntüleri
Aşağıda uygulamanın çalıştığını gösteren ekran görüntüleri yer alacaktır:

### Ana Sayfa
![image](https://github.com/user-attachments/assets/54c1a5b7-2c12-427c-afbd-3561738933e2)
![image](https://github.com/user-attachments/assets/f1be1549-0a33-4fb2-8534-bf6fc01efb18)

### Yeni Makine Ekleme Sayfası 
![image](https://github.com/user-attachments/assets/83e8a1f8-6d37-4ed0-828e-3932c58daa32)

### Detaylar ve makine parça Stoğu güncelleme 
![image](https://github.com/user-attachments/assets/40aea0c5-1b05-4003-9014-00395466b80f)
![image](https://github.com/user-attachments/assets/5ec1ce79-4c6e-42c3-8031-55544b777872)

### Var olan Makineye Parça Ekleme 
![image](https://github.com/user-attachments/assets/2f18b653-d3dc-449b-b17c-354a3a73aed1)


## Docker İle Çalıştırma
![image](https://github.com/user-attachments/assets/f75f5852-5ce9-4423-b885-05192eaec7e0)
![image](https://github.com/user-attachments/assets/19e7bc15-a1a4-4802-a6de-a31ac5bede10)
![image](https://github.com/user-attachments/assets/321fb4e4-39bf-47e3-915c-843d49d6fc45)






























































