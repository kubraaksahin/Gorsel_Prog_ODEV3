using MauiApp4_odev3.Model;

namespace MauiApp4_odev3;

// Seçilen haberin detaylarýný ve web içeriðini gösteren sayfa sýnýfý
public partial class HaberDetay : ContentPage
{
    // Bir önceki sayfadan gönderilen seçili haber verisini tutan deðiþken
    Item _seciliHaber;

    // Yapýcý Metot: Haberler sayfasýndan seçilen 'Item' nesnesini parametre olarak alýr
    public HaberDetay(Item haber)
    {
        // XAML bileþenlerini baþlatýr
        InitializeComponent();

        // Gelen haber verisini yerel deðiþkene atar
        _seciliHaber = haber;

        // XAML tarafýndaki WebView bileþenine haberin orijinal web linkini yükler
        webViewHaber.Source = _seciliHaber.link;
    }

    // Haberi paylaþ butonuna týklandýðýnda çalýþan metot
    private async void ShareNews_Clicked(object sender, EventArgs e)
    {
        // Eðer haber verisi bir þekilde yüklenmemiþse iþlemi iptal et
        if (_seciliHaber == null) return;

        // Cihazýn yerel paylaþým menüsünü açar 
        await Share.Default.RequestAsync(new ShareTextRequest
        {
            // Paylaþýlacak olan haberin web baðlantýsý
            Uri = _seciliHaber.link,
            // Paylaþým penceresinin baþlýðý
            Title = _seciliHaber.title,
            // Paylaþýlacak olan mesaj metni 
            Text = _seciliHaber.title
        });
    }
}