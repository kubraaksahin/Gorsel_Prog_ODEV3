using MauiApp4_odev3.Services;

namespace MauiApp4_odev3;

// Döviz kurlarının gösterildiği sayfanın arka plan kodları
public partial class DovizPage : ContentPage
{
    public DovizPage()
    {
        // XAML arayüz bileşenlerini belleğe yükler
        InitializeComponent();

        try
        {
            // KurServisi üzerinden TCMB'nin resmi XML adresinden günlük verileri çeker
            // Bu metot geriye hem bülten tarihini hem de kurların listesini döndürür
            var data = KurServisi.KurlariYukle("https://www.tcmb.gov.tr/kurlar/today.xml");

            // Eğer veriler başarıyla çekildiyse ve liste boş değilse
            if (data.kurlar != null)
            {
                // XAML tarafındaki CollectionView veya ListView  nesnesine verileri bağlar
                KurlarList.ItemsSource = data.kurlar;

                // Sayfanın üstündeki etikete , servisten gelen güncel tarihi yazar
                // $ işareti değişkenleri metin içine doğrudan gömmeyi sağlar
                lblDate.Text = $"Döviz Kurları Tarih: {data.tarih}";
            }
        }
        catch (Exception ex)
        {
            // İnternet yoksa veya XML yapısı hatalıysa kullanıcıya bilgi verir
            lblDate.Text = "Veri yüklenemedi!";

            // Hatanın detayını geliştiriciye göstermek için konsola yazdırır
            Console.WriteLine(ex.Message);
        }
    }
}