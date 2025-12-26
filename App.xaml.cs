namespace MauiApp4_odev3
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new LoginPage());
            this.UserAppTheme = AppTheme.Light;


        }

      
    }
}