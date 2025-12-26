namespace MauiApp4_odev3;

using MauiApp4_odev3.Services;

// Yeni kullanıcıların hesap oluşturmasını sağlayan sayfa sınıfı
public partial class RegisterPage : ContentPage
{
    public RegisterPage()
    {
        // XAML tarafındaki görsel arayüz elemanlarını yükler
        InitializeComponent();
    }

    // "Giriş Sayfasına Git" butonuna tıklandığında çalışan metot
    private void OnGotoLoginPageButtonClicked(object sender, EventArgs e)
    {
        // Mevcut kayıt sayfasını kapatır ve bir önceki sayfaya döner
        Navigation.PopModalAsync();
    }

    // "İptal" butonuna tıklandığında çalışan metot
    private void CancelButtonClicked(object sender, EventArgs e)
    {
        // İşlemi iptal ederek kayıt penceresini kapatır
        Navigation.PopModalAsync();
    }

    // "Kayıt Ol" butonuna tıklandığında çalışan ana metot
    private async void OnRegisterButtonClicked(object sender, EventArgs e)
    {
        // Hata mesajlarını yakalamak için boş bir değişken oluşturulur
        string message = "";

        // FirebaseServices içindeki Register metoduna kullanıcı verilerini gönderir
        // UsernameEntry, EmailEntry ve PasswordEntry arayüzdeki kutucuklardır
        var res = await FirebaseServices.Register(UsernameEntry.Text, EmailEntry.Text, PasswordEntry.Text, ref message);

        // Kayıt işlemi başarılıysa 
        if (res == true)
        {
            // Kullanıcıya başarılı olduğuna dair bir bilgi mesajı gösterir
            await DisplayAlert("Kayıt Başarılı", "Hesabınız oluşturuldu.", "Tamam");
        }
        else
        {
            // Kayıt başarısızsa Firebase'den gelen hata mesajını ekrana yansıtır
            await DisplayAlert("Kayıt Başarısız", message, "Tamam");
        }
    }
}