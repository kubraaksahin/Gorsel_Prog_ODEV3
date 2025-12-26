using Newtonsoft.Json;
using MauiApp4_odev3.Model; // Haber modeli yapýlarýný kullanmak için eklenen referans
using System.Xml;
using MyNewsRoot = MauiApp4_odev3.Model.Root; // Çakýþmalarý önlemek için Root modeline takma isim verildi

namespace MauiApp4_odev3.Services
{
    // Ýnternet üzerinden haber verilerini çeken servis sýnýfý
    public class NewsServices
    {
        // 1. Að istekleri için kullanýlan istemci 
        private static readonly HttpClient client = new HttpClient();

        // Verilen URL'den XML formatýndaki haber verisini çekip JSON formatýna dönüþtüren metot
        public static async Task<string> GetRSSDataAsJSON(string url)
        {
            try
            {
                // Belirtilen adrese HTTP isteði gönderilir
                var message = await client.GetAsync(url);
                // Ýsteðin baþarýlý olup olmadýðý kontrol edilir
                message.EnsureSuccessStatusCode();

                // Gelen ham XML verisi metin olarak okunur
                var xmlData = await message.Content.ReadAsStringAsync();

                // XML verisini iþlemek için bir XmlDocument nesnesi oluþturulur ve yüklenir
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(xmlData);

                // NewtonSoft kütüphanesi kullanýlarak XML yapýsý JSON metnine dönüþtürülür
                return JsonConvert.SerializeXmlNode(xmlDocument);
            }
            catch (Exception ex)
            {
                // Baðlantý hatasý veya geçersiz XML durumunda hatayý konsola yazdýrýr
                Console.WriteLine($"RSS Hatasý: {ex.Message}");
                return null;
            }
        }

        // Belirli bir kategoriye ait haber listesini döndüren ana metot
        public static async Task<List<Item>> GetCategoryNews(NewsCategory category)
        {
            // Önce kategorinin URL'inden JSON formatýnda veriyi alýr
            var jsonData = await GetRSSDataAsJSON(category.Url);

            // JSON formatýndaki metni, C# nesne modelimize  dönüþtürür (Deserialization)
            MyNewsRoot myDeserializedClass = JsonConvert.DeserializeObject<MyNewsRoot>(jsonData);

            // Hiyerarþiyi takip ederek sadece haberlerin olduðu listeyi döndürür
            return myDeserializedClass.rss.channel.item;
        }
    }
}