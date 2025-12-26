using Newtonsoft.Json;
using System.Collections.Generic;

namespace MauiApp4_odev3.Model
{
    // Haber kategorilerini (Gündem, Ekonomi vb.) ve linklerini tutan yardýmcý sýnýf
    public class NewsCategory
    {
        public string Category { get; set; } // Kategorinin adý (Görünecek metin)
        public string Url { get; set; }      // Kategorinin haberlerini çekecek olan link

        public NewsCategory(string category, string url)
        {
            Category = category;
            Url = url;
        }
    }

    // RSS içerisindeki ana kanal (Channel) bilgilerini tutar (Gazete adý, dili, haber listesi vb.)
    public class Channel
    {
        public string lastBuildDate { get; set; } // Haberlerin son güncellenme tarihi
        public string title { get; set; }         // Kanalýn baþlýðý (Örn: AA Haberleri)
        public string description { get; set; }   // Kanalýn açýklamasý
        public string link { get; set; }          // Kanalýn web sitesi linki
        public string language { get; set; }      // Yayýn dili (Örn: tr)
        public List<Item> item { get; set; }      // Kanaldaki haberlerin listesi
    }

    // CDATA formatýndaki (HTML içeren) uzun içerik kýsýmlarý için
    public class ContentEncoded
    {
        [JsonProperty("#cdata-section")] // JSON'daki CDATA etiketini bu özelliðe baðlar
        public string cdatasection { get; set; }
    }

    // Haber özeti/açýklamasý için kullanýlan CDATA alaný
    public class Description
    {
        [JsonProperty("#cdata-section")]
        public string cdatasection { get; set; }
    }

    // Haberle birlikte gelen ek dosyalar (Görsel, Video vb. meta verileri)
    public class Enclosure
    {
        [JsonProperty("@url")] // JSON'daki @url niteliðini baðlar
        public string url { get; set; }

        [JsonProperty("@length")]
        public string length { get; set; }

        [JsonProperty("@type")]
        public string type { get; set; }
    }

    // Her bir haberin detaylarýný tutan temel sýnýf
    public class Item
    {
        public string guid { get; set; }      // Haberin benzersiz kimliði
        public string pubDate { get; set; }   // Haberin yayýnlanma tarihi
        public string title { get; set; }     // Haberin baþlýðý
        public Description description { get; set; } // Haberin kýsa özeti

        [JsonProperty("media:content")]       // RSS'deki medya içeriði (Resim genellikle buradadýr)
        public MediaContent mediacontent { get; set; }

        public Enclosure enclosure { get; set; } // Alternatif resim/dosya alaný
        public string link { get; set; }         // Haberin web sayfasý linki
        public string author { get; set; }       // Haberi yazan kiþi/kurum

        [JsonProperty("content:encoded")]     // Haberin tam metni (HTML içerebilir)
        public ContentEncoded contentencoded { get; set; }
    }

    // Görselin geniþlik, yükseklik ve URL gibi teknik detaylarýný tutar
    public class MediaContent
    {
        [JsonProperty("@url")]
        public string url { get; set; }

        [JsonProperty("@type")]
        public string type { get; set; }

        [JsonProperty("@expression")]
        public string expression { get; set; }

        [JsonProperty("@width")]
        public string width { get; set; }

        [JsonProperty("@height")]
        public string height { get; set; }
    }

    // API'den dönen JSON verisinin en dýþ kapsayýcýsý
    public class Root
    {
        [JsonProperty("?xml")] 
        public Xml xml { get; set; }
        public Rss rss { get; set; } 
    }

    // RSS standardýna ait versiyon ve kanal verilerini tutan sýnýf
    public class Rss
    {
        [JsonProperty("@version")]
        public string version { get; set; }

        [JsonProperty("@xmlns:content")]
        public string xmlnscontent { get; set; }

        [JsonProperty("@xmlns:media")]
        public string xmlnsmedia { get; set; }

        public Channel channel { get; set; } // Asýl haberlerin olduðu kanal verisi
    }

    // XML dosyasýnýn baþlýk bilgilerini tutar
    public class Xml
    {
        [JsonProperty("@version")]
        public string version { get; set; }

        [JsonProperty("@encoding")]
        public string encoding { get; set; }
    }
}