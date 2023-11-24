using ExamClient.Resources.Resx;
using System.Globalization;

namespace Client
{
    public partial class App : Application
    {
        public App()
        {
            Ip_adress ip_Adress = new Ip_adress();
            ip_Adress.CheckOS();
            CultureInfo ci = null;
            switch (ip_Adress.language)
            {
                case 1:
                    ci = new CultureInfo("ru");
                    break;
                case 2:
                    ci = new CultureInfo("en-US");
                    break;

            }
            AppResources.Culture = ci; // Установите локализацию ресурса
            CultureInfo.CurrentUICulture = ci;
            CultureInfo.CurrentCulture = ci;

            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}