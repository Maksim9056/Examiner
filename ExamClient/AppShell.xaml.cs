using System.Windows.Input;
using System.Collections.Generic;
using ExamClient.Resources.Resx;
using System.Globalization;
using Microsoft.Maui.Controls.StyleSheets;
using System.Reflection;

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
            AddStyle();

            InitializeComponent();
            RegisterRoutes();
            BindingContext = this;
        }
        public void AddStyle()
        {
            Ip_adress ip_Adress = new Ip_adress();
            ip_Adress.CheckOS();

            //ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
            //if (mergedDictionaries != null)
            //{
            //    mergedDictionaries.Clear();
            //    mergedDictionaries.Add(new ResourceDictionary { Source = new Uri("Resources/Styles/Styles.xaml") });
            //}
            ////Ресурсы приложения
            //var resourceDictionary = new ResourceDictionary();
            //resourceDictionary.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri("Resources/Styles/Colors.xaml", UriKind.Relative) });
            //resourceDictionary.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri("Resources/Styles/Styles2.xaml", UriKind.Relative) });
            //Application.Current.Resources = resourceDictionary;

            //Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri("Resources/Styles/Styles2.xaml", UriKind.Relative) });

            //Application.Current.Resources.Add(StyleSheet.FromResource
            //("Resources\\Styles\\Styles2.xaml", IntrospectionExtensions.GetTypeInfo(typeof(MainPage)).Assembly));


            //Application.Current.Resources.Remove("ButtonTextColor");
            //var primaryColor = Color.FromHex("#CD5C5C");
            //Application.Current.Resources.Add("ButtonTextColor", primaryColor);

            //Application.Current.Resources["Border2"] = Color.FromHex("#7bcc23");
            //Application.Current.Resources["Page2"] = Color.FromHex("#6fef5d");
            //Application.Current.Resources["ButtonTextColor"] = Color.FromHex("#7bcc23");

            string Button = "";
            string ShellTitle = "";
            string EntryTextColor = "";
            string CardBackgroundColor = "";
            string Colors = "";
            string Entrys = "";
            string RegUserEntry = "";
            string BackupButtonBackgroundColor = "";
            string CardBackgroundColors = "";
            switch (ip_Adress.ColorStyles)
            {
                case 1:
           
                    break;
                case 2:
                    //Application.Current.Resources["ButtonBackgroundColor"] = Color.FromHex("#77c063");
                    //Application.Current.Resources["PageBackgroundColor"] = Color.FromHex("#64A437");

                    //Application.Current.Resources["ButtonBackgroundColor"] = Color.FromHex("#b5de77");
                    // Application.Current.Resources["ButtonBackgroundColor"] =  Color.FromHex("#64A437");
                    Button = "#7BC266";
                    Colors = "#7FC676";
                    Entrys = "#ffffff";
                    RegUserEntry = "#92D08A";
                    CardBackgroundColor = "#67BE5B";
                    BackupButtonBackgroundColor = "#7BC266";

                    break;
                case 3:

                    Button = "#ffc5cb";
                    Colors = "#fee2e1";
                    Entrys = "#ffffff";
                    RegUserEntry = "#ffe6e6";
                    CardBackgroundColor = "#fff1f0";
                    BackupButtonBackgroundColor = "#7BC266";
                    //Application.Current.Resources["ButtonBackgroundColor"] = Color.FromHex("Black");
                    break;
            }
            if (Button == "")
            {

            }
            else
            {
                Application.Current.Resources["ButtonBackgroundColor"] = Color.FromHex(Button);
                Application.Current.Resources["ShellTitle"] = Color.FromHex(Button);
                Application.Current.Resources["EntryTextColor"] = Color.FromHex(Entrys);


                //Application.Current.Resources["BorderColor"]           = Color.FromHex("#669933
                //CardBackgroundColor  
                Application.Current.Resources["CardBackgroundColor"] = Color.FromHex(CardBackgroundColor);
                Application.Current.Resources["Grid"] = Color.FromHex(Colors);

                Application.Current.Resources["PageBackgroundColor"] = Color.FromHex(Colors);

                Application.Current.Resources["EntryBackgroundColor"] = Color.FromHex(Colors);

                Application.Current.Resources["BorderColor"] = Color.FromHex(Entrys);
                //RegUserPage
                Application.Current.Resources["RegUserPage"] = Color.FromHex(Colors);

                Application.Current.Resources["RegUserEntry"] = Color.FromHex(RegUserEntry);

                Application.Current.Resources["RegUserStackLayout"] = Color.FromHex(Colors);
                //SettingPage
                Application.Current.Resources["SettingPage"] = Color.FromHex(Colors);


                Application.Current.Resources["SettingEntry"] = Color.FromHex(RegUserEntry);

                //Backup
                Application.Current.Resources["PageBackup"] = Color.FromHex(Colors);

                Application.Current.Resources["BackupButtonBackgroundColor"] = Color.FromHex(Button);

                //PageResverveOfCopyBackup
                Application.Current.Resources["PageResverveOfCopyBackup"] = Color.FromHex(Colors);

                Application.Current.Resources["ButtonResverveOfCopyBackup"] = Color.FromHex(Button);

                //PageAnswerCreate
                Application.Current.Resources["PageAnswerCreate"] = Color.FromHex(Colors);

                Application.Current.Resources["PageAnswerCreateButton"] = Color.FromHex(Button);

                //PageAnswerEditor
                Application.Current.Resources["PageAnswerEditor"] = Color.FromHex(Colors);

                Application.Current.Resources["PageAnswerEditorButton"] = Color.FromHex(Button);
                //PageRefAnswerListPage

                Application.Current.Resources["PageRefAnswerListPage"] = Color.FromHex(Colors);
                Application.Current.Resources["PageRefAnswerListPageButton"] = Color.FromHex(Button);
                //PageRefAnswerListPage
                Application.Current.Resources["PageRefAnswerListPage"] = Color.FromHex(Colors);
                //PageExamsCreate
                Application.Current.Resources["PageExamsCreate"] = Color.FromHex(Colors);
                Application.Current.Resources["PageExamsCreateButton"] = Color.FromHex(Button);
                //PageExamsEditor
                Application.Current.Resources["PageExamsEditor"] = Color.FromHex(Colors);
                Application.Current.Resources["PageExamsEditorButton"] = Color.FromHex(Button);
                //PageExamsEditor
                Application.Current.Resources["PageRefExamsListPage"] = Color.FromHex(Colors);
                Application.Current.Resources["PageRefExamsListPageButton"] = Color.FromHex(Button);
                //PageQuestionCreate
                Application.Current.Resources["PageQuestionCreate"] = Color.FromHex(Colors);
                Application.Current.Resources["PageQuestionCreateButton"] = Color.FromHex(Button);
                //PageQuestionEditor  
                Application.Current.Resources["PageQuestionEditor"] = Color.FromHex(Colors);
                Application.Current.Resources["PageQuestionEditorButton"] = Color.FromHex(Button);
                //
                Application.Current.Resources["PageQuestionEditor"] = Color.FromHex(Colors);
                Application.Current.Resources["PageQuestionEditorButton"] = Color.FromHex(Button);
                //PageRefQuestionListButton
                Application.Current.Resources["PageRefQuestionList"] = Color.FromHex(Colors);
                Application.Current.Resources["PageRefQuestionListButton"] = Color.FromHex(Button);

                //PageRefTestList               
                Application.Current.Resources["PageRefTestList"] = Color.FromHex(Colors);
                Application.Current.Resources["PageRefTestListButton"] = Color.FromHex(Button);
                //PageTestCreate               
                Application.Current.Resources["PageTestCreate"] = Color.FromHex(Colors);
                Application.Current.Resources["PageTestCreateButton"] = Color.FromHex(Button);
                //  PageTestEditorButton
                Application.Current.Resources["PageTestEditor"] = Color.FromHex(Colors);
                Application.Current.Resources["PageTestEditorButton"] = Color.FromHex(Button);
                //  PageRefUserList
                Application.Current.Resources["PageRefUserList"] = Color.FromHex(Colors);
                Application.Current.Resources["PageRefUserListButton"] = Color.FromHex(Button);
                //PageUserCreate
                Application.Current.Resources["PageUserCreate"] = Color.FromHex(Colors);
                Application.Current.Resources["PageUserCreateButton"] = Color.FromHex(Button);
                //PageUserEditor
                Application.Current.Resources["PageUserEditor"] = Color.FromHex(Colors);
                Application.Current.Resources["PageUserEditorButton"] = Color.FromHex(Button);
                Application.Current.Resources["PageUserCreateButton1"] = Color.FromHex(Button);
                //PageUserCreate
                Application.Current.Resources["PageUserCreate"] = Color.FromHex(Colors);
                Application.Current.Resources["PageUserEditorButton"] = Color.FromHex(Button);
                Application.Current.Resources["PageUserCreate1"] = Color.FromHex(Colors);
                //PageDocExamTestList
                Application.Current.Resources["PageDocExamTestList"] = Color.FromHex(Colors);
                Application.Current.Resources["PageDocExamTestListButton"] = Color.FromHex(Button);
                //PageDocQuestionAnswer
                Application.Current.Resources["PageDocQuestionAnswer"] = Color.FromHex(Colors);
                Application.Current.Resources["PageDocQuestionAnswerButton"] = Color.FromHex(Button);
                //PageDocTestQuestion
                Application.Current.Resources["PageDocTestQuestion"] = Color.FromHex(Colors);
                Application.Current.Resources["PageDocTestQuestionButton"] = Color.FromHex(Button);
                //PageDocUserExam
                Application.Current.Resources["PageDocUserExam"] = Color.FromHex(Colors);
                Application.Current.Resources["PageDocUserExamButton"] = Color.FromHex(Button);
                //User
                Application.Current.Resources["User"] = Color.FromHex(Colors);
                Application.Current.Resources["UserButton"] = Color.FromHex(Button);
                //User

                //PageDocTheExamispersonal
                Application.Current.Resources["PageDocTheExamispersonal"] = Color.FromHex(Colors);
                //Application.Current.Resources["UserButton"] = Color.FromHex("#7BC266");
                //PageDocTestsFromQuestions
                Application.Current.Resources["PageDocTestsFromQuestions"] = Color.FromHex(Colors);
                Application.Current.Resources["PageDocTestsFromQuestionsButton"] = Color.FromHex(Button);
                //PageDocTestQuestionsTheAnswersMark
                Application.Current.Resources["PageDocTestQuestionsTheAnswersMark"] = Color.FromHex(Colors);
                Application.Current.Resources["PageDocTestQuestionsTheAnswersMarkButton"] = Color.FromHex(Button);
                //PageDocTestQuestionsTheAnswers
                Application.Current.Resources["PageDocTestQuestionsTheAnswers"] = Color.FromHex(Colors);
                Application.Current.Resources["PageDocTestQuestionsTheAnswersMarkButton"] = Color.FromHex(Button);

                //PageDocTestMenu
                Application.Current.Resources["PageDocTestMenu"] = Color.FromHex(Colors);
                Application.Current.Resources["PageDocTestMenuButton"] = Color.FromHex(Button);
                //PageDocStatisticsUserResult
                Application.Current.Resources["PageDocStatisticsUserResult"] = Color.FromHex(Colors);
                Application.Current.Resources["PageDocStatisticsUserResultButton"] = Color.FromHex(Button);
                //PageDocStatisticsUserResult
                Application.Current.Resources["PageDocStatisticsUserResult"] = Color.FromHex(Colors);
                Application.Current.Resources["PageDocStatisticsUserResultButton"] = Color.FromHex(Button);
                //PageDocPersonalAchievement
                Application.Current.Resources["PageDocPersonalAchievement"] = Color.FromHex(Colors);
                Application.Current.Resources["PageDocStatisticsUserResultButton"] = Color.FromHex(Button);
                //PageDocPersonalAchievementButton
                Application.Current.Resources["PageDocPersonalAchievement"] = Color.FromHex(Colors);
                Application.Current.Resources["PageDocPersonalAchievementButton"] = Color.FromHex(Button);
                //PageDocAnswerQuestins
                Application.Current.Resources["PageDocAnswerQuestins"] = Color.FromHex(Colors);
                Application.Current.Resources["PageDocAnswerQuestinsButton"] = Color.FromHex(Button);
                //Application.Current.Resources.Remove("ButtonBackgroundColor");
                //var primaryColor1 = Color.FromHex("#CD5C5C");
                //Application.Current.Resources.Add("ButtonBackgroundColor", primaryColor1);
            }


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