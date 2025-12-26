using MauiApp4_odev3.Model;
using MauiApp4_odev3.Services;

namespace MauiApp4_odev3;

// Görev ekleme ve düzenleme iþlemlerinin yapýldýðý sayfanýn arka plan kodlarý
public partial class GorevEklePage : ContentPage
{
    // Firebase veritabaný iþlemlerini gerçekleþtirecek servis nesnesi
    TodoFirebase _service = new TodoFirebase();

    // Düzenleme modunda kullanýlacak mevcut görev nesnesi
    TodoTask _editTask;

    // Sayfanýn 'Yeni Ekleme' mi yoksa 'Düzenleme' modunda mý olduðunu takip eden bayrak 
    bool isEditMode = false;

    // Varsayýlan Yapýcý Metot: Yeni bir görev eklemek istendiðinde çalýþýr
    public GorevEklePage()
    {
        InitializeComponent();
        Title = "Yeni Görev Ekle"; 
    }

    // Parametreli Yapýcý Metot: Mevcut bir görevi düzenlemek istendiðinde çalýþýr
    public GorevEklePage(TodoTask task)
    {
        InitializeComponent();
        _editTask = task; // Gelen görev verisini düzenlenecek nesneye aktar
        isEditMode = true; // Modu düzenleme olarak iþaretle
        Title = "Görevi Düzenle";

        // Arayüzdeki giriþ alanlarýný (Entry/Picker) seçilen görevin verileriyle doldur
        entBaslik.Text = _editTask.Baslik;
        entDetay.Text = _editTask.Detay;
        dpTarih.Date = _editTask.Tarih;
        tpSaat.Time = _editTask.Saat;

        // Kullanýcýya iþlemin güncelleme olduðunu belirtmek için buton metnini deðiþtir
        btnKaydet.Text = "GÜNCELLE";
    }

    // Kaydet/Güncelle butonuna týklandýðýnda çalýþan olay (event)
    private async void OnKaydetClicked(object sender, EventArgs e)
    {
        // Eðer düzenleme modundaysak mevcut veriyi güncelle
        if (isEditMode)
        {
            // Arayüzdeki yeni deðerleri mevcut nesneye aktar
            _editTask.Baslik = entBaslik.Text;
            _editTask.Detay = entDetay.Text;
            _editTask.Tarih = dpTarih.Date;
            _editTask.Saat = tpSaat.Time;

            // Firebase servisini kullanarak güncelleme iþlemini baþlat
            await _service.UpdateTask(_editTask);
            await DisplayAlert("Bilgi", "Görev güncellendi!", "Tamam");
        }
        else
        {
            // Eðer yeni ekleme modundaysak yeni bir TodoTask nesnesi oluþtur
            var yeniGorev = new TodoTask
            {
                Baslik = entBaslik.Text,
                Detay = entDetay.Text,
                Tarih = dpTarih.Date,
                Saat = tpSaat.Time,
                IsDone = false // Yeni görevler varsayýlan olarak tamamlanmamýþ iþaretlenir
            };

            // Firebase servisini kullanarak yeni görevi veritabanýna ekle
            await _service.AddTask(yeniGorev);
            await DisplayAlert("Bilgi", "Görev eklendi!", "Tamam");
        }

        // Ýþlem tamamlandýktan sonra bir önceki sayfaya geri dön
        await Navigation.PopAsync();
    }
}