using MauiApp4_odev3.Services;

namespace MauiApp4_odev3;

// Kullanýcýnýn uygulamaya giriþ yapmasýný saðlayan sayfa sýnýfý
public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        // XAML tarafýndaki görsel bileþenleri baþlatýr
        InitializeComponent();
    }

    // Giriþ yap butonuna týklandýðýnda çalýþan metot
    private async void OnLoginButtonClicked(object sender, EventArgs e)
    {
        string message = "";

        // FirebaseServices içindeki Login metoduna kullanýcý bilgilerini gönderir
        // 'ref message' sayesinde hata oluþursa hata detayýný servis içinden alýr
        var res = await FirebaseServices.Login(EmailEntry.Text, PasswordEntry.Text, ref message);

        // Giriþ iþlemi baþarýlý ise 
        if (res)
        {
            // Kullanýcý arayüzü güncellemelerini ana kanalda  yapmak güvenlidir.
            // Giriþ baþarýlý olduðu için uygulamayý Shell yapýsýna geçiriyoruz.
            // Bu iþlemden sonra alt menüler ve navigasyon aktifleþir.
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Application.Current.MainPage = new AppShell();
            });
        }
        else
        {
            // Giriþ baþarýsýzsa Firebase'den gelen hata mesajýný kullanýcýya gösterir
            await DisplayAlert("Hata", "Giriþ baþarýsýz: " + message, "Tamam");
        }
    }

    // Kayýt ol butonuna týklandýðýnda çalýþan metot
    private async void OnRegisterButtonClicked(object sender, EventArgs e)
    {
        // Kullanýcýyý yeni bir pencere olarak Kayýt Sayfasýna yönlendirir
        // PushModalAsync kullanýmý, sayfanýn mevcut navigasyonun üzerine açýlmasýný saðlar
        await Navigation.PushModalAsync(new RegisterPage());
    }
}