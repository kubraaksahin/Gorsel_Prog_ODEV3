using MauiApp4_odev3.Model;
using MauiApp4_odev3.Services;

namespace MauiApp4_odev3;

// Yapýlacaklar listesini görüntüleyen, silen ve güncelleyen ana sayfa sýnýfý
partial class YapilacaklarPage : ContentPage
{
    // Firebase veritabaný iþlemlerini yürüten servis nesnesi
    TodoFirebase _firebaseService = new TodoFirebase();

    public YapilacaklarPage()
    {
        // XAML bileþenlerini yükler
        InitializeComponent();
        // Sayfa açýldýðýnda verileri Firebase'den çekmek için metodu çaðýrýr
        LoadTasks();
    }

    // Firebase'deki tüm görevleri çeken ve ekrandaki listeye baðlayan metot
    async void LoadTasks()
    {
        // Servisten gelen listeyi TasksCollection isimli arayüz öðesinin veri kaynaðýna aktarýr
        TasksCollection.ItemsSource = await _firebaseService.GetAllTasks();
    }

    // Silme (Çöp Kutusu) butonuna týklandýðýnda çalýþan metot
    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        // Týklanan butona baðlý olan görev nesnesini yakalar
        var task = (sender as ImageButton)?.CommandParameter as TodoTask;
        if (task == null) return;

        // Kullanýcýdan silme onayý isteyen bir pencere açar
        bool result = await DisplayAlert("Silinsin mi?", $"{task.Baslik} silinecek.", "Evet", "Hayýr");
        if (result)
        {
            // Onay verilirse Firebase üzerinden benzersiz Id ile görevi siler
            await _firebaseService.DeleteTask(task.Id);
            // Listeyi güncel tutmak için verileri yeniden yükler
            LoadTasks();
        }
    }

    // Yeni Görev Ekle butonuna týklandýðýnda çalýþan metot
    private async void OnAddClicked(object sender, EventArgs e)
    {
        // Kullanýcýyý yeni görev oluþturma sayfasýna yönlendirir
        await Navigation.PushAsync(new GorevEklePage());
    }

    // CheckBox  deðiþtiðinde çalýþan metot
    async void OnTaskStatusChanged(object sender, CheckedChangedEventArgs e)
    {
        var cb = sender as CheckBox;
        var task = cb.BindingContext as TodoTask;

        if (task != null)
        {
            // Görevin tamamlandý bilgisini CheckBox'ýn yeni deðerine göre günceller
            task.IsDone = e.Value;
            // Güncel durumu Firebase veritabanýna gönderir
            await _firebaseService.UpdateTask(task);
        }
    }

    // Yenileme butonuna týklandýðýnda listeyi tazeleyen metot
    private void OnRefreshClicked(object sender, EventArgs e)
    {
        // Firebase verilerini tekrar çeker
        LoadTasks();
    }

    // Düzenle butonuna týklandýðýnda çalýþan metot
    private async void OnEditClicked(object sender, EventArgs e)
    {
        var buton = sender as ImageButton;
        // Seçilen görevin verilerini yakalar
        var secilenGorev = buton.CommandParameter as TodoTask;

        if (secilenGorev != null)
        {
            // GorevEklePage'e seçili görevi "parametre" olarak göndererek düzenleme modunda açar
            await Navigation.PushAsync(new GorevEklePage(secilenGorev));
        }
    }
}