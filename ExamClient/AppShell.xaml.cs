using System.Windows.Input;
using System.Collections.Generic;
using ExamClient.Resources.Resx;
using System.Globalization;

namespace Client
{
    public partial class AppShell : Shell
    {
        public Dictionary<string, Type> Routes { get; private set; } = new Dictionary<string, Type>();
        public ICommand HelpCommand => new Command<string>(async (url) => await Launcher.OpenAsync(url));

        public AppShell()

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
            RegisterRoutes();
            BindingContext = this;
        }

        void RegisterRoutes()
        {
            Routes.Add("login", typeof(MainPage));
            //Routes.Add("admin", typeof(Client.Main.Admin));
            //Routes.Add("user", typeof(Client.Users.Users));
            Routes.Add("logout", typeof(Client.MainPage));
            Routes.Add("achievement", typeof(Client.Users.Doc.DocPersonalAchievement.DocPersonalAchievement));
            Routes.Add("examispersonal", typeof(Client.Users.DocTheExamisPersonal));
            Routes.Add("examfromtests", typeof(Client.Users.Users));
            Routes.Add("DocTheExamisPersonal", typeof(Client.Users.DocTheExamisPersonal));
           // Routes.Add("RefUserListPage", typeof(Project.RefUserListPage));
            Routes.Add("statistics", typeof(Client.Users.Doc.DocStatisticsUserResult.DocStatisticsUserResult));


            foreach (var item in Routes)
            {
                Routing.RegisterRoute(item.Key, item.Value);
            }
        }
    }
}