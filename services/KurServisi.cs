using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml;

namespace MauiApp4_odev3.Services
{
    // Merkez Bankasý üzerinden döviz kurlarýný çeken ve iþleyen servis sýnýfý
    public static class KurServisi
    {
        // Belirtilen URL'den (Genellikle TCMB XML linki) kurlarý yükleyen metot
        public static (string tarih, List<Currency> kurlar) KurlariYukle(string url)
        {
            // XML verisini okumak için XmlDocument nesnesi oluþturulur
            XmlDocument doc = new XmlDocument();
            // Verilen URL'deki XML içeriði belleðe yüklenir
            doc.Load(url);

            // XML hiyerarþisi, JSON formatýna dönüþtürülür 
            var jsonData = JsonConvert.SerializeXmlNode(doc);

            // JSON metni, aþaðýda tanýmlanan Root modeline dönüþtürülür 
            Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(jsonData);

            // Dönüþtürülen veriden Tarih bilgisini ve Döviz listesini ayýklayarak döndürür
            return (myDeserializedClass.Tarih_Date.Tarih,
                myDeserializedClass.Tarih_Date.Currency);
        }
    }

    // Her bir para biriminin detaylarýný tutan sýnýf
    public class Currency
    {
        // JSON'daki '@' ile baþlayan nitelikleri C# özelliklerine baðlar
        [JsonProperty("@CrossOrder")]
        public string CrossOrder { get; set; }

        [JsonProperty("@Kod")]
        public string Kod { get; set; }

        [JsonProperty("@CurrencyCode")]
        public string CurrencyCode { get; set; }

        public string Unit { get; set; }           // Birim 
        public string Isim { get; set; }           // Türkçe isim 
        public string CurrencyName { get; set; }   // Ýngilizce isim 
        public string ForexBuying { get; set; }    // Döviz alýþ fiyatý
        public string ForexSelling { get; set; }   // Döviz satýþ fiyatý
        public string BanknoteBuying { get; set; } // Efektif alýþ fiyatý
        public string BanknoteSelling { get; set; } // Efektif satýþ fiyatý
        public string CrossRateUSD { get; set; }   // Dolar paritesi
        public string CrossRateOther { get; set; } // Diðer pariteler
    }

    // JSON verisinin en dýþ katmanýný temsil eden kök sýnýf
    public class Root
    {
        [JsonProperty("?xml")]
        public Xml xml { get; set; } // XML versiyon ve kodlama bilgisi

        [JsonProperty("?xml-stylesheet")]
        public string xmlstylesheet { get; set; }

        // Asýl döviz verilerinin bulunduðu Tarih_Date düðümü
        public TarihDate Tarih_Date { get; set; }
    }

    // XML'deki <Tarih_Date> etiketine denk gelen, bülten bilgilerini ve listeyi tutan sýnýf
    public class TarihDate
    {
        [JsonProperty("@Tarih")]
        public string Tarih { get; set; } // Kurlarýn geçerli olduðu tarih 

        [JsonProperty("@Date")]
        public string Date { get; set; }

        [JsonProperty("@Bulten_No")]
        public string Bulten_No { get; set; } // Merkez bankasý bülten numarasý

        // Bülten içerisindeki tüm para birimlerinin listesi
        public List<Currency> Currency { get; set; }
    }

    // XML deklarasyon bilgilerini tutan sýnýf
    public class Xml
    {
        [JsonProperty("@version")]
        public string version { get; set; }

        [JsonProperty("@encoding")]
        public string encoding { get; set; }
    }
}