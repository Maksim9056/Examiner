using ExamClient.Resources.Resx;
using System.Globalization;

namespace Client
{
    public partial class App : Application
    {
        public App()
        {
            // Проверка ОС
            Ip_adress ip_Adress = new Ip_adress();
            ip_Adress.CheckOS();

            // Локализация
            CultureInfo ci;
            switch (ip_Adress.language)
            {
                case 1:
                    ci = new CultureInfo("ru");
                    break;
                case 2:
                    ci = new CultureInfo("en-US");
                    break;
                default:
                    ci = CultureInfo.InvariantCulture; // Если язык не соответствует, используйте инвариантную локализацию
                    break;
            }

            AppResources.Culture = ci; // Установка локализации ресурсов
            CultureInfo.CurrentUICulture = ci;
            CultureInfo.CurrentCulture = ci;

            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}