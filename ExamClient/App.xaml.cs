using ExamClient.Resources.Resx;
using Microsoft.Maui.Hosting;
using System.Globalization;

namespace Client
{
    public partial class App : Application
    {
        public App()
        {
            //// Проверка ОС
            //Ip_adress ip_Adress = new Ip_adress();
            //ip_Adress.CheckOS();

            //// Локализация
            //CultureInfo ci;
            //switch (ip_Adress.language)
            //{
            //    case 1:
            //        ci = new CultureInfo("ru");
            //        break;
            //    case 2:
            //        ci = new CultureInfo("en-US");
            //        break;
            //    default:
            //        ci = CultureInfo.InvariantCulture; // Если язык не соответствует, используйте инвариантную локализацию
            //        break;
            //}

            //AppResources.Culture = ci; // Установка локализации ресурсов
            //CultureInfo.CurrentUICulture = ci;
            //CultureInfo.CurrentCulture = ci;

            InitializeComponent();

            //Application.Current.Resources.Remove("ButtonBackgroundColor");
            //var primaryColor1 = Color.FromHex("#CD5C5C");
            //Application.Current.Resources.Add("ButtonBackgroundColor", primaryColor1); 

            // Стиль для страницы
            var pageStyle = new Style(typeof(ContentPage))
            {
                ApplyToDerivedTypes = true,
                Setters = {
                    new Setter { Property = VisualElement.BackgroundColorProperty, Value = "#D1E9D1" }
                    // Добавьте другие свойства стиля, если необходимо
                }
            };


            MainPage = new AppShell();
        }
    }
}