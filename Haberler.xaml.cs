using MauiApp4_odev3.Model;
using MauiApp4_odev3.Services;

namespace MauiApp4_odev3;

// Haberlerin listelendiði ve kategorilere göre ayrýldýðý sayfa sýnýfý
public partial class Haberler : ContentPage
{
    public Haberler()
    {
        // XAML arayüz bileþenlerini yükler
        InitializeComponent();
        // Sayfa açýldýðýnda kategori butonlarýný oluþturur
        KategorileriOlustur();
    }

    // Haber kategorilerini ve RSS linklerini tanýmlayýp buton haline getiren metot
    private void KategorileriOlustur()
    {
        // Haber çekilecek kategorilerin listesi ve TRT Haber RSS baðlantýlarý
        var kategoriler = new List<NewsCategory>
        {
            new NewsCategory("Gündem", "https://www.trthaber.com/gundem_articles.rss"),
            new NewsCategory("Ekonomi", "https://www.trthaber.com/ekonomi_articles.rss"),
            new NewsCategory("Spor", "https://www.trthaber.com/spor_articles.rss"),
            new NewsCategory("Bilim Teknoloji", "https://www.trthaber.com/bilim_teknoloji_articles.rss")
        };

        // Liste içindeki her bir kategori için dinamik olarak bir buton oluþturur
        foreach (var kat in kategoriler)
        {
            var btn = new Button
            {
                Text = kat.Category, // Butonun üzerinde yazacak kategori adý
                Margin = new Thickness(5), // Butonlar arasý boþluk
                CommandParameter = kat, // Butona týklandýðýnda hangi kategorinin verisi olduðunu anlamak için saklanan nesne
                BackgroundColor = Color.FromArgb("#6200EE"), // Mor arka plan rengi
                TextColor = Colors.White, // Beyaz yazý rengi               
                CornerRadius = 10 // Yuvarlatýlmýþ köþeler
            };

            // Butona týklama olayýný baðlar
            btn.Clicked += KategoriTiklandi;
            // Oluþturulan butonu XAML'daki 'StackKategoriler' isimli kutunun içine ekler
            StackKategoriler.Children.Add(btn);
        }
    }

    // Bir kategori butonuna týklandýðýnda çalýþan metot
    private async void KategoriTiklandi(object sender, EventArgs e)
    {
        var btn = sender as Button;
        // Butona daha önce eklediðimiz kategori bilgisini geri alýr
        var kat = btn.CommandParameter as NewsCategory;

        // Haberleri NewsServices üzerinden internetten asenkron olarak çeker
        var haberler = await NewsServices.GetCategoryNews(kat);

        // Çekilen haber listesini CollectionView içine baðlar 
        lstHaberler.ItemsSource = haberler;
    }

    // Listeden bir habere týklandýðýnda çalýþan metot
    private async void OnHaberSelected(object sender, SelectionChangedEventArgs e)
    {
        // Seçilen öðeyi 'Item' modeline dönüþtürür
        if (e.CurrentSelection.FirstOrDefault() is Item seciliHaber)
        {
            // Seçimi temizler 
            ((CollectionView)sender).SelectedItem = null;

            // Seçilen haberi parametre olarak göndererek detay sayfasýna geçiþ yapar
            await Navigation.PushAsync(new HaberDetay(seciliHaber));
        }
    }
}