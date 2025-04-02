# 🔎 .NET 9 + Elasticsearch Entegrasyonu  
### Ürün Arama Sistemi (MongoDB + Elasticsearch)

# 🔍 Elasticsearch Nedir?

**Elasticsearch**, büyük veri kümelerinde çok hızlı arama ve analiz yapılmasını sağlayan açık kaynaklı, dağıtık bir arama motorudur.

Verileri **JSON formatında** saklar ve **REST API** üzerinden erişim sağlar. Genellikle tam metin arama, log analizi ve veri keşfi gibi alanlarda tercih edilir.

---

## 🚀 Temel Özellikler

- ⚡ **Gerçek zamanlı arama** performansı (milisaniyelerle ölçülür)
- 📦 **JSON tabanlı** veri yapısı
- 🔁 **Dağıtık ve ölçeklenebilir** mimari
- 🔍 **Full-text search** desteği (Google gibi arama yapabilme)
- 🧠 **Akıllı eşleştirme** (fuzzy, suggestion, autocomplete)
- 📊 **Kibana entegrasyonu** ile görsel analiz

---

## 🧠 Elasticsearch Nasıl Çalışır?

### 🔄 Inverted Index (Ters Dizin)

Elasticsearch, metin aramasını hızlı yapmak için geleneksel veritabanlarından farklı olarak **ters dizin** kullanır.

> Örnek: `mouse` kelimesi geçen dökümanlar: `[1, 3, 7]`

---

## 🧱 Temel Yapılar

| Yapı        | Açıklama                           |
|-------------|------------------------------------|
| **Index**   | Verilerin tutulduğu koleksiyon     |
| **Document**| JSON formatındaki her kayıt        |
| **Field**   | Document içindeki veri alanı       |

---

## 🛠 Kullanım Senaryoları

✅ E-ticaret ürün arama motoru  
✅ Uygulama loglarının analizi  
✅ Auto-complete ve typo düzeltme  
✅ Gerçek zamanlı dashboard  
✅ Veri keşfi ve analitik

---

## 📊 Kibana ile Entegrasyon

- Web tabanlı kullanıcı arayüzü
- Arama sorgularını görselleştirme
- Filtreleme ve grafiklerle analiz
- Dashboard oluşturma

![Kibana Screenshot](https://github.com/busenurdmb/ElasticMongoDemo/blob/master/image/kibana.png)

---


---

## 🧩 Proje Özeti

Bu projede ürün verileri **MongoDB** üzerinde saklanırken, arama işlemleri için aynı veriler **Elasticsearch** üzerinde indekslenmiştir.  
Amaç; geleneksel veri saklama ve modern arama motorlarının avantajlarını bir arada kullanmaktır.

📦 Katmanlı mimari (onion architecture) ile yapılandırılmıştır.  
📊 Kibana üzerinden görselleştirme desteği mevcuttur.  

---
![Arama Örneği](https://github.com/busenurdmb/ElasticMongoDemo/blob/master/image/elasticksearchekleme.jpeg)
![Arama Örneği](https://github.com/busenurdmb/ElasticMongoDemo/blob/master/image/elasticksearchsearh.jpeg)
![Arama Örneği](https://github.com/busenurdmb/ElasticMongoDemo/blob/master/image/elastiksearput.jpeg)
![Arama Örneği](https://github.com/busenurdmb/ElasticMongoDemo/blob/master/image/sahte%C3%BCr%C3%BCneklemefake.png)
![Arama Örneği](https://github.com/busenurdmb/ElasticMongoDemo/blob/master/image/kibana1.png)

## ⚙️ Kullanılan Teknolojiler

| Teknoloji | Açıklama |
|----------|----------|
| 💻 **.NET 9** | Web API geliştirme çatısı |
| 🧅 **Onion Architecture** | Katmanlı yapı: API - Application - Domain - Infrastructure |
| 🍃 **MongoDB** | Veri saklama (NoSQL) |
| 🔍 **Elasticsearch** | Hızlı ve güçlü arama motoru |
| 📊 **Kibana** | Elasticsearch verilerini görselleştirme |
| 🧪 **Swagger UI** | API dökümantasyonu ve endpoint testleri |
| 🐳 **Docker** | Elasticsearch ve Kibana container desteği (opsiyonel) |
| 🧰 ** Bogus** | Fake ürün verisi oluşturmak için kullanılan C# kütüphanesi |

---

## 🏗️ Proje Yapısı (Onion Architecture)
📁 ElasticMongoDemo
- ├── 📦 ElasticMongoDemo.API → Controller & Swagger 
- ├── 📦 ElasticMongoDemo.Application → Business logic, DTOs 
- ├── 📦 ElasticMongoDemo.Domain → Entity'ler ve interface’ler 
- └── 📦 ElasticMongoDemo.Infrastructure → MongoDB ve Elasticsearch

![Arama Örneği](https://github.com/busenurdmb/ElasticMongoDemo/blob/master/image/elasticearch2.png)
![Arama Örneği](https://github.com/busenurdmb/ElasticMongoDemo/blob/master/image/dockerek.png)

## 🐳 Docker Kurulumu

`docker-compose.txt` dosyasını `.yml` olarak yeniden adlandırın (`docker-compose.yml`) ve terminalden çalıştırın:

```bash
docker-compose up -d


## ⚙️ Hızlı Başlatma (Setup)

`setup.bat` dosyası ile Visual Studio üzerinde çözümü kolayca açabilirsiniz:

