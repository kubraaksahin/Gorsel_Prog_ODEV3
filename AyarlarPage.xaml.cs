namespace MauiApp4_odev3;

public partial class AyarlarPage : ContentPage
{
	public AyarlarPage()
	{
		InitializeComponent();
	}

    private void OnThemeToggled(object sender, ToggledEventArgs e)
    {
        if (e.Value) // KOYU TEMA AÇIK
        {
            Application.Current.Resources["ArkaPlanRengi"] = Color.FromArgb("#121212");
            Application.Current.Resources["YaziRengi"] = Colors.White;
            Application.Current.Resources["KartRengi"] = Color.FromArgb("#1E1E1E");

            // Üst barý da karartmak istersen 
            Application.Current.UserAppTheme = AppTheme.Dark;
        }
        else // AÇIK TEMA
        {
            Application.Current.Resources["ArkaPlanRengi"] = Color.FromArgb("#F3E5F5");
            Application.Current.Resources["YaziRengi"] = Color.FromArgb("#4A148C");
            Application.Current.Resources["KartRengi"] = Colors.White;

            Application.Current.UserAppTheme = AppTheme.Light;
        }
    }
}