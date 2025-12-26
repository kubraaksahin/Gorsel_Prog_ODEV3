using System.Collections.ObjectModel;
using System.Text.Json;
using MauiApp4_odev3.Services; // SehirHavaDurumu sınıfına ve normalizasyon servisine erişim sağlar

namespace MauiApp4_odev3;

// Uygulamanın hava durumu takibi yapılan sayfasının arka plan kodları
public partial class HavaDurumu : ContentPage
{
    // Şehir listesi değiştiğinde arayüzün  otomatik güncellenmesini sağlayan özel koleksiyon
    ObservableCollection<SehirHavaDurumu> Sehirler;

    public HavaDurumu()
    {
        // XAML arayüz bileşenlerini yükler
        InitializeComponent();

        // Uygulama açıldığında kayıtlı şehirleri dosyadan yükler
        Sehirler = LoadSehirler();

        // Eğer arayüzdeki liste mevcutsa, veri kaynağını şehirler listesine bağlar
        if (lstSehirler != null)
            lstSehirler.ItemsSource = Sehirler;
    }

    // Yerel depolama alanındaki JSON dosyasından şehir listesini okuyan metot
    ObservableCollection<SehirHavaDurumu> LoadSehirler()
    {
        // Dosyanın kaydedileceği uygulama dizini yolunu oluşturur
        string filePath = Path.Combine(FileSystem.AppDataDirectory, "sehirler.json");

        // Eğer dosya daha önce oluşturulmuşsa içeriğini oku
        if (File.Exists(filePath))
        {
            var json = File.ReadAllText(filePath);
            // JSON formatındaki metni tekrar C# koleksiyonuna dönüştürür
            return JsonSerializer.Deserialize<ObservableCollection<SehirHavaDurumu>>(json);
        }

        // Eğer dosya yoksa varsayılan olarak "ANKARA" şehrini içeren yeni bir liste döndürür
        return new ObservableCollection<SehirHavaDurumu>() { new SehirHavaDurumu { Name = "ANKARA" } };
    }

    // Yeni bir şehir ekleme butonuna tıklandığında çalışan metot
    private async void AddSehir_Clicked(object sender, EventArgs e)
    {
        // Kullanıcıdan şehir adını girmesi için bir giriş penceresi açar
        var sehirInput = await DisplayPromptAsync("Şehir Ekle", "Şehir adını giriniz:");

        // Giriş boş değilse işleme devam et
        if (!string.IsNullOrWhiteSpace(sehirInput))
        {
            // HavaDurumuServisi üzerinden ismi büyük harf yapar ve Türkçe karakterleri temizler 
            string temizIsim = HavaDurumuServisi.NormalizeCityName(sehirInput);

            // Temizlenmiş ismi listeye ekler 
            Sehirler.Add(new SehirHavaDurumu { Name = temizIsim });

            // Listeyi dosyaya kaydeder
            Save();
        }
    }

    // Listeden bir şehri silmek için kullanılan metot
    private void Remove_Clicked(object sender, EventArgs e)
    {
        // Tıklanan butona bağlı olan şehir nesnesini yakalar
        if (sender is Button btn && btn.CommandParameter is SehirHavaDurumu sehir)
        {
            // Şehri listeden çıkarır
            Sehirler.Remove(sehir);
            // Güncel listeyi dosyaya kaydeder
            Save();
        }
    }

    // Şehir bilgisini listede tazelemek/güncellemek için kullanılan metot
    private void Update_Clicked(object sender, EventArgs e)
    {
        var sehir = (sender as Button).CommandParameter as SehirHavaDurumu;
        if (sehir != null)
        {
            // Şehrin listedeki konumunu bulur
            var index = Sehirler.IndexOf(sehir);
            // Listeden çıkarıp aynı yere tekrar ekleyerek UI'nın yenilenmesini tetikler
            Sehirler.RemoveAt(index);
            Sehirler.Insert(index, sehir);
        }
    }

    // Mevcut şehir listesini JSON formatına çevirip yerel dosyaya kaydeden metot
    private void Save()
    {
        string filePath = Path.Combine(FileSystem.AppDataDirectory, "sehirler.json");
        // Koleksiyonu JSON metnine dönüştürür ve dosyaya yazar
        File.WriteAllText(filePath, JsonSerializer.Serialize(Sehirler));
    }
}