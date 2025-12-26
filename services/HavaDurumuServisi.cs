using System;
using System.Collections.Generic;
using System.Text;

namespace MauiApp4_odev3.Services
{
    // Hava durumu bilgilerini MGM üzerinden almak için yardýmcý araçlar sunan servis sýnýfý
    public static class HavaDurumuServisi
    {
        // MGM servislerinin doðru çalýþmasý için þehir isimlerini standart hale getiren metot
        public static string NormalizeCityName(string cityName)
        {
            // Þehir ismi boþsa hatayý önlemek için boþ string döner
            if (string.IsNullOrEmpty(cityName)) return "";

            // Önce tüm harfleri büyük harfe çevirir
            cityName = cityName.ToUpper();

            // Türkçe'ye özgü karakterleri Ýngilizce karakter setindeki karþýlýklarýyla deðiþtirir
            cityName = cityName.Replace("Ç", "C").Replace("ç", "C");
            cityName = cityName.Replace("Ö", "O").Replace("ö", "O");
            cityName = cityName.Replace("Þ", "S").Replace("þ", "S");
            cityName = cityName.Replace("Ý", "I").Replace("ý", "I");
            cityName = cityName.Replace("Ü", "U").Replace("ü", "U");
            cityName = cityName.Replace("Ð", "G").Replace("ð", "G");

            // MGM veritabanýndaki özel þehir ismi kýsaltmalarý veya farklarý için kurallar
            if (cityName == "KAHRAMANMARAS")
                cityName = "K.MARAS"; 
            else if (cityName == "AFYON")
                cityName = "AFYONKARAHISAR"; 

            return cityName;
        }

        // MGM'nin 5 günlük klasik tahmin tablosunu gösteren görsel URL'i oluþturur
        public static string HavaDurumu5gun(string sehir)
        {
            // Önce þehir ismini MGM standartlarýna getirir
            string temizSehir = NormalizeCityName(sehir);

            // MGM'nin sunum servisine ait parametreli URL þablonu
            return $"https://www.mgm.gov.tr/sunum/tahmin-klasik-5070.aspx?m={temizSehir}&basla=1&bitir=5&rC=111&rZ=fff";
        }
    }

    // Arayüz tarafýnda WebView veya Label gibi bileþenlere veri baðlamak için kullanýlan model
    public class SehirHavaDurumu
    {
        // Kullanýcýnýn girdiði veya seçtiði þehir adý 
        public string Name { get; set; }

        // XAML tarafýndaki WebView bileþeninin Source özelliðine baðlanacak olan tam URL
        public string Source => HavaDurumuServisi.HavaDurumu5gun(Name);
    }
}