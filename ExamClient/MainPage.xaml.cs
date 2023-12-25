using ExamModels;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using System.Text.Json;
using System.Text;
using System.Xml;
using System.Text.RegularExpressions;
using Microsoft.Maui.Controls;
using Microsoft.Maui;
using Microsoft.Maui.Platform;
using Client.Main;
using System.Windows.Input;
using System.Web;
using System.Net.NetworkInformation;
using System.Net;
using System.IO;
using System.Net.Mail;
using static System.Net.WebRequestMethods;
using System.Net.Sockets;
using static Client.MainPage;
using System.Globalization;
using ExamClient.Resources.Resx;
using Microsoft.Maui.Controls.StyleSheets;
using System.Reflection;

//using Microsoft.AspNetCore.Components.Navigation;


namespace Client
{

    public partial class MainPage : ContentPage
    {
        public ICommand TapCommand => new Command<string>(async (url) => await Launcher.OpenAsync(url));
        CheckPing checkPing = new CheckPing();
        public MainPage()
        {
            try
            {
                AddSettings();
                InitializeComponent();
                AddStyle();
                BindingContext = this;
            }
            catch
            {

            }
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
            switch (ip_Adress.ColorStyles)
            { 
                case 1:
                    break;
                case 2:
                    //Application.Current.Resources["ButtonBackgroundColor"] = Color.FromHex("#77c063");

                    //Application.Current.Resources["ButtonBackgroundColor"] = Color.FromHex("#b5de77");
                    // Application.Current.Resources["ButtonBackgroundColor"] =  Color.FromHex("#64A437");
                    Application.Current.Resources["ButtonBackgroundColor"] = Color.FromHex("#7BC266");
                    //Application.Current.Resources["PageBackgroundColor"] = Color.FromHex("#64A437");
                    Application.Current.Resources["ShellTitle"] = Color.FromHex("#7BC266");
                    Application.Current.Resources["EntryTextColor"] = Color.FromHex("#ffffff");

                    
                    //Application.Current.Resources["BorderColor"]           = Color.FromHex("#669933
                    //CardBackgroundColor  
                    Application.Current.Resources["CardBackgroundColor"]   = Color.FromHex("#67BE5B");
                    Application.Current.Resources["Grid"]                  = Color.FromHex("#7FC676");
                    
                    Application.Current.Resources["PageBackgroundColor"] = Color.FromHex("#7FC676");

                    Application.Current.Resources["EntryBackgroundColor"] = Color.FromHex("#7FC676");

                    Application.Current.Resources["BorderColor"] = Color.FromHex("#ffffff");
                    //RegUserPage
                    Application.Current.Resources["RegUserPage"] = Color.FromHex("#7FC676");

                    Application.Current.Resources["RegUserEntry"] = Color.FromHex("#92D08A");

                    Application.Current.Resources["RegUserStackLayout"] = Color.FromHex("#7FC676");

                 
                    //SettingPage
                    Application.Current.Resources["SettingPage"] = Color.FromHex("#7FC676");


                    Application.Current.Resources["SettingEntry"] = Color.FromHex("#92D08A");

                    //Backup
                    Application.Current.Resources["PageBackup"] = Color.FromHex("#7FC676");
                    
                    Application.Current.Resources["BackupButtonBackgroundColor"] = Color.FromHex("#7BC266");

                    //PageResverveOfCopyBackup
                    Application.Current.Resources["PageResverveOfCopyBackup"] = Color.FromHex("#7FC676");

                    Application.Current.Resources["ButtonResverveOfCopyBackup"] = Color.FromHex("#7BC266");

                    //PageAnswerCreate
                    Application.Current.Resources["PageAnswerCreate"] = Color.FromHex("#7FC676");

                    Application.Current.Resources["PageAnswerCreateButton"] = Color.FromHex("#7BC266");

                    //PageAnswerEditor
                    Application.Current.Resources["PageAnswerEditor"] = Color.FromHex("#7FC676");

                    Application.Current.Resources["PageAnswerEditorButton"] = Color.FromHex("#7BC266");
                    //PageRefAnswerListPage

                    Application.Current.Resources["PageRefAnswerListPage"] = Color.FromHex("#7FC676");
                    Application.Current.Resources["PageRefAnswerListPageButton"] = Color.FromHex("#7BC266");
                    //PageRefAnswerListPage
                    Application.Current.Resources["PageRefAnswerListPage"] = Color.FromHex("#7FC676");
                    //PageExamsCreate
                    Application.Current.Resources["PageExamsCreate"] = Color.FromHex("#7FC676");
                    Application.Current.Resources["PageExamsCreateButton"] = Color.FromHex("#7BC266");
                    //PageExamsEditor
                    Application.Current.Resources["PageExamsEditor"] = Color.FromHex("#7FC676");
                    Application.Current.Resources["PageExamsEditorButton"] = Color.FromHex("#7BC266");
                    //PageExamsEditor
                    Application.Current.Resources["PageRefExamsListPage"] = Color.FromHex("#7FC676");
                    Application.Current.Resources["PageRefExamsListPageButton"] = Color.FromHex("#7BC266");
                    //PageQuestionCreate
                    Application.Current.Resources["PageQuestionCreate"] = Color.FromHex("#7FC676");
                    Application.Current.Resources["PageQuestionCreateButton"] = Color.FromHex("#7BC266");
                    //PageQuestionEditor  
                    Application.Current.Resources["PageQuestionEditor"] = Color.FromHex("#7FC676");
                    Application.Current.Resources["PageQuestionEditorButton"] = Color.FromHex("#7BC266");
                    //
                    Application.Current.Resources["PageQuestionEditor"] = Color.FromHex("#7FC676");
                    Application.Current.Resources["PageQuestionEditorButton"] = Color.FromHex("#7BC266");
                    //PageRefQuestionListButton
                    Application.Current.Resources["PageRefQuestionList"] = Color.FromHex("#7FC676");
                    Application.Current.Resources["PageRefQuestionListButton"] = Color.FromHex("#7BC266");

                    //PageRefTestList               
                    Application.Current.Resources["PageRefTestList"] = Color.FromHex("#7FC676");
                    Application.Current.Resources["PageRefTestListButton"] = Color.FromHex("#7BC266");
                    //PageTestCreate               
                    Application.Current.Resources["PageTestCreate"] = Color.FromHex("#7FC676");
                    Application.Current.Resources["PageTestCreateButton"] = Color.FromHex("#7BC266");
                    //  PageTestEditorButton
                    Application.Current.Resources["PageTestEditor"] = Color.FromHex("#7FC676");
                    Application.Current.Resources["PageTestEditorButton"] = Color.FromHex("#7BC266");
                    //  PageRefUserList
                    Application.Current.Resources["PageRefUserList"] = Color.FromHex("#7FC676");
                    Application.Current.Resources["PageRefUserListButton"] = Color.FromHex("#7BC266");
                    //PageUserCreate
                    Application.Current.Resources["PageUserCreate"] = Color.FromHex("#7FC676");
                    Application.Current.Resources["PageUserCreateButton"] = Color.FromHex("#7BC266");
                    //PageUserEditor
                    Application.Current.Resources["PageUserEditor"] = Color.FromHex("#7FC676");
                    Application.Current.Resources["PageUserEditorButton"] = Color.FromHex("#7BC266");
                    Application.Current.Resources["PageUserCreateButton1"] = Color.FromHex("#7BC266");
                    //PageUserCreate
                    Application.Current.Resources["PageUserCreate"] = Color.FromHex("#7FC676");
                    Application.Current.Resources["PageUserEditorButton"] = Color.FromHex("#7BC266"); 
                    Application.Current.Resources["PageUserCreate1"] = Color.FromHex("#7FC676");
                    //PageDocExamTestList
                    Application.Current.Resources["PageDocExamTestList"] = Color.FromHex("#7FC676");
                    Application.Current.Resources["PageDocExamTestListButton"] = Color.FromHex("#7BC266");
                    //PageDocQuestionAnswer
                    Application.Current.Resources["PageDocQuestionAnswer"] = Color.FromHex("#7FC676");
                    Application.Current.Resources["PageDocQuestionAnswerButton"] = Color.FromHex("#7BC266");
                    //PageDocTestQuestion
                    Application.Current.Resources["PageDocTestQuestion"] = Color.FromHex("#7FC676");
                    Application.Current.Resources["PageDocTestQuestionButton"] = Color.FromHex("#7BC266");
                    //PageDocUserExam
                    Application.Current.Resources["PageDocUserExam"] = Color.FromHex("#7FC676");
                    Application.Current.Resources["PageDocUserExamButton"] = Color.FromHex("#7BC266");
                    //User
                    Application.Current.Resources["User"] = Color.FromHex("#7FC676");
                    Application.Current.Resources["UserButton"] = Color.FromHex("#7BC266");
                    //User

                    //PageDocTheExamispersonal
                    Application.Current.Resources["PageDocTheExamispersonal"] = Color.FromHex("#7FC676");
                    //Application.Current.Resources["UserButton"] = Color.FromHex("#7BC266");
                    //PageDocTestsFromQuestions
                    Application.Current.Resources["PageDocTestsFromQuestions"] = Color.FromHex("#7FC676");
                    Application.Current.Resources["PageDocTestsFromQuestionsButton"] = Color.FromHex("#7BC266");
                    //PageDocTestQuestionsTheAnswersMark
                    Application.Current.Resources["PageDocTestQuestionsTheAnswersMark"] = Color.FromHex("#7FC676");
                    Application.Current.Resources["PageDocTestQuestionsTheAnswersMarkButton"] = Color.FromHex("#7BC266");
                    //PageDocTestQuestionsTheAnswers
                    Application.Current.Resources["PageDocTestQuestionsTheAnswers"] = Color.FromHex("#7FC676");
                    Application.Current.Resources["PageDocTestQuestionsTheAnswersMarkButton"] = Color.FromHex("#7BC266");

                    //PageDocTestMenu
                    Application.Current.Resources["PageDocTestMenu"] = Color.FromHex("#7FC676");
                    Application.Current.Resources["PageDocTestMenuButton"] = Color.FromHex("#7BC266");
                    //PageDocStatisticsUserResult
                    Application.Current.Resources["PageDocStatisticsUserResult"] = Color.FromHex("#7FC676");
                    Application.Current.Resources["PageDocStatisticsUserResultButton"] = Color.FromHex("#7BC266");
                    //PageDocStatisticsUserResult
                    Application.Current.Resources["PageDocStatisticsUserResult"] = Color.FromHex("#7FC676");
                    Application.Current.Resources["PageDocStatisticsUserResultButton"] = Color.FromHex("#7BC266");
                    //PageDocPersonalAchievement
                    Application.Current.Resources["PageDocPersonalAchievement"] = Color.FromHex("#7FC676");
                    Application.Current.Resources["PageDocStatisticsUserResultButton"] = Color.FromHex("#7BC266");
                    //PageDocPersonalAchievementButton
                    Application.Current.Resources["PageDocPersonalAchievement"] = Color.FromHex("#7FC676");
                    Application.Current.Resources["PageDocPersonalAchievementButton"] = Color.FromHex("#7BC266");
                    //PageDocAnswerQuestins
                    Application.Current.Resources["PageDocAnswerQuestins"] = Color.FromHex("#7FC676");
                    Application.Current.Resources["PageDocAnswerQuestinsButton"] = Color.FromHex("#7BC266");
                    break;
                case 3:
                    Application.Current.Resources["ButtonBackgroundColor"] = Color.FromHex("Black");
                    break;
            }

            //Application.Current.Resources.Remove("ButtonBackgroundColor");
            //var primaryColor1 = Color.FromHex("#CD5C5C");
            //Application.Current.Resources.Add("ButtonBackgroundColor", primaryColor1);

        }


        public void AddSettings()
        {
            try
            {
                var flyoutItemseting = Shell.Current.Items.FirstOrDefault(item => item.Route.Equals("IMPL_seting"));
                if (flyoutItemseting == null)
                {
                   // Создание пунктов меню класса
                   var main = new ShellContent { Content = new Client.Project.Settings()};

                    // Добавление пунктов меню в класс
                    Shell.Current.Items.Add(new ShellSection { Title = AppResources.Настройки, Icon = "dotnet_bot.png", Route = "seting", Items = { main } });

                    // Обработчик события при нажатии на пункт меню
                    //main.PropertyChanged += async (sender, e) =>
                    //{
                    //    if (e.PropertyName == nameof(ShellContent.IsEnabled) && !main.IsEnabled)
                    //    {
                    //        // Переход обратно
                    //        await Shell.Current.GoToAsync("admin");
                    //    }
                    //};
                }
            }
            catch(Exception ex) 
            {
                DisplayAlert(AppResources.Ошибка, ex.Message, AppResources.Ок);
            }
        }
        //var rt = Shell.Current.CurrentState.Location.OriginalString;
        //var parameters = System.Web.HttpUtility.ParseQueryString(rt);
        //var sellValue = parameters.Get("sell");


        public CommandCL command = new CommandCL();
        public string Mail { get; set; }
        public string Password { get; set; }



        private void OnCounterClicked(object sender, EventArgs e)
        {

        }

        //
        private void OnButtonClicked(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// //////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void nameEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }

        private void CounterBtn_Clicked(object sender, EventArgs e)
        {
            

        }

        /// <summary>
        /// В данный момент смысла нету править не используеться
        ///
        /// </summary>
        /// <param name="FileFS"></param>
        /// <param name="ip_Adress"></param>
       public async void Start(string FileFS , Ip_adress ip_Adress)
       {     Галочка галочка1 = null;
            using (MemoryStream stream = new MemoryStream())  {
                Галочка галочка = new Галочка(1, $"{ip_Adress.Ip_adressss}");
                var Result = checkPing.CheckPingIp(галочка);
                галочка1 = Result;   }
            if (галочка1 == null)  {
                await DisplayAlert(AppResources.Уведомление,AppResources.Сервервыключенилинедоступен, AppResources.Ок); }
            else
            {  if (Mail == null) {
                    await DisplayAlert(AppResources.Уведомление, AppResources.Почтанепустая, AppResources.Ок);    }
                else
                { //Проверяет почту пуста или нет
                    if (string.IsNullOrEmpty(Mail)) {
                        await DisplayAlert(AppResources.Уведомление,AppResources.Почтанезаполнена ,AppResources.Ок);  }
                    else
                    { //Регеулярное выражение
                        string patern = "@.";
                        //Регулярное выражение
                        Regex regex = new Regex(patern);
                        //Проверяет в Mail есть ли в строке это @ почту
                        if (Regex.IsMatch(Mail, patern)) {
                            if (Password == null){
                                await DisplayAlert(AppResources.Уведомление, AppResources.Парольнезаполнен, AppResources.Ок);}
                            else
                            { if (string.IsNullOrEmpty(Password)){
                                    await DisplayAlert(AppResources.Уведомление, AppResources.Парольнезаполнен, AppResources.Ок);  }
                                else
                                {//Обьявляем MemoryStream 
                                    using (MemoryStream memoryStream = new MemoryStream())
                                    {//Заполняем класс CheckMail_and_Password для отправки на сервер
                                        CheckMail_and_Password ss = new CheckMail_and_Password(Password, Mail);
                                        //Серелизуем класс CheckMail_and_Password для отправки на сервер
                                        JsonSerializer.Serialize<CheckMail_and_Password>(memoryStream, ss);
                                        //Декодировали в строку  memoryStream    класс запоаковали в json строку
                                        FileFS = Encoding.Default.GetString(memoryStream.ToArray());
                                        //Отправляем на сервер  команду 003
                                        Task.Run(async () => await command.Check_User_Possword(ip_Adress.Ip_adressss, FileFS, "003")).Wait();
                                        //Значение почты пользователя  присваеваем по умолчанию почту для следущего входа пользователей
                                        Mail = null;
                                        //Значение пароля пользователя  присваеваем по умолчанию пароль для следущего входа пользователей 
                                        Password = null;
                                        nameEntry1.Text = null;
                                        nameEntry9.Text = null;
                                        //Ответ с сервера получаем 
                                        if (command.Travel_logout == null)
                                        {
                                            await DisplayAlert(AppResources.Уведомление,AppResources.Такогопользователянет , AppResources.Ок); }
                                        else   {
                                            if (string.IsNullOrEmpty(command.Travel_logout.Employee_Mail))
                                            {
                                                await DisplayAlert(AppResources.Уведомление, AppResources.Такогопользователянет , AppResources.Ок);   }
                                            else
                                            { if (command.Travel_logout.Id == 0)  {
                                                    await DisplayAlert(AppResources.Уведомление,AppResources.Парольвведенневерно, AppResources.Ок);}
                                                else
                                                {   var regis_Users = command.Travel_logout;
                                                    switch (regis_Users.Rechte.Id) {
                                                        case 1:
                                                            await Application.Current.MainPage.DisplayAlert(AppResources.Уведомление, AppResources.Помощь, AppResources.Ок);
                                                            await Navigation.PushAsync(new Client.Users.Users(regis_Users));
                                                            var flyoutItemhelp1 = (FlyoutItem)Shell.Current.Items.FirstOrDefault(item => item.Route.Equals("login"));
                                                            if (flyoutItemhelp1 != null) {
                                                                flyoutItemhelp1.IsVisible = false;}
                                                            var flyoutItemseting1 = Shell.Current.Items.FirstOrDefault(item => item.Route.Equals("IMPL_seting"));
                                                            if (flyoutItemseting1 != null){
                                                                flyoutItemseting1.IsVisible = false; }
                                                            //await Shell.Current.GoToAsync("user"); // Используйте URI для перехода к пользовательской странице
                                                            break;
                                                        case 2:
                                                            await Navigation.PushAsync(new Admin());
                                                            await Application.Current.MainPage.DisplayAlert(AppResources.Уведомление, AppResources.АдминистраторАвторизовался, AppResources.Ок);
                                                            //Admin admin = new Admin();
                                                            var flyoutItemhelp = (FlyoutItem)Shell.Current.Items.FirstOrDefault(item => item.Route.Equals("login"));
                                                            if (flyoutItemhelp != null){
                                                                flyoutItemhelp.IsVisible = false;}

                                                            var flyoutItemseting = Shell.Current.Items.FirstOrDefault(item => item.Route.Equals("IMPL_seting"));
                                                            if (flyoutItemseting != null) {
                                                                flyoutItemseting.IsVisible = false; }
                                                            //var adminPage = new ();
                                                            //var navigationPage2 = new NavigationPage(adminPage);
                                                            //   await Shell.Current.GoToAsync("admin");
                                                            // Используйте URI для перехода к административной странице
                                                            //Application.Current.MainPage = navigationPage2;
                                                            break;  } } }  } }}  }  }
                        else
                        {
                            //Не проходит на почту действетульную
                            await DisplayAlert(AppResources.Уведомление, AppResources.Ввелинепочту, AppResources.Ок);
                        }}}}}










        public async void Ping(string FileFS, Ip_adress ip_Address)
        {
            Галочка галочка = null;
            using (MemoryStream stream = new MemoryStream())
            {
                Галочка галочка1 = new Галочка(1, $"{ip_Address.Ip_adressss}");
                var Result = checkPing.CheckPingIp(галочка1);
                галочка = Result;
            }
            if (галочка == null)
            {
                await DisplayAlert(AppResources.Уведомление, AppResources.Сервервыключенилинедоступен, AppResources.Ок);
                return;
            }
            if (string.IsNullOrEmpty(Mail))
            {
                await DisplayAlert(AppResources.Уведомление,AppResources. Почтанезаполнена, AppResources.Ок);
                return;
            }
            string pattern = "@.";
            if (!Regex.IsMatch(Mail, pattern))
            {
                await DisplayAlert(AppResources.Уведомление, AppResources.Некорректныйформатадресаэлектроннойпочты, AppResources.Ок);
                return;
            }
            if (string.IsNullOrEmpty(Password))
            {
                await DisplayAlert(AppResources.Уведомление, AppResources.Парольнезаполнен, AppResources.Ок);
                return;
            }
            using (MemoryStream memoryStream = new MemoryStream())
            {
                CheckMail_and_Password ss = new CheckMail_and_Password(Password, Mail);
                await JsonSerializer.SerializeAsync<CheckMail_and_Password>(memoryStream, ss);
                FileFS = Encoding.Default.GetString(memoryStream.ToArray());
                await command.Check_User_Possword(ip_Address.Ip_adressss, FileFS, "003");
                Mail = null;
                Password = null;
                nameEntry1.Text = null;
                nameEntry9.Text = null;

                if (command.Travel_logout == null || string.IsNullOrEmpty(command.Travel_logout.Employee_Mail))
                {
                    await DisplayAlert(AppResources.Уведомление,AppResources.Такогопользователянет, AppResources.Ок);
                }
                else if (command.Travel_logout.Id == 0)
                {
                    await DisplayAlert(AppResources.Уведомление, AppResources.Парольвведенневерно, AppResources.Ок);
                }
                else
                {
                    var regis_Users = command.Travel_logout;
                    switch (regis_Users.Rechte.Id)
                    {
                        case 1:
                            await Application.Current.MainPage.DisplayAlert(AppResources.Уведомление, AppResources.ПользовательАвторизовался, AppResources.Ок);
                            await Navigation.PushAsync(new Client.Users.Users(regis_Users));
                            var flyoutItemhelp1 = (FlyoutItem)Shell.Current.Items.FirstOrDefault(item => item.Route.Equals("login"));
                            if (flyoutItemhelp1 != null)
                            {
                                flyoutItemhelp1.IsVisible = false;
                            }
                            var flyoutItemseting1 = Shell.Current.Items.FirstOrDefault(item => item.Route.Equals("IMPL_seting"));
                            if (flyoutItemseting1 != null)
                            {
                                flyoutItemseting1.IsVisible = false;
                            }
                            break;
                        case 2:
                            await Navigation.PushAsync(new Admin());
                            await Application.Current.MainPage.DisplayAlert(AppResources.Уведомление, AppResources.АдминистраторАвторизовался, AppResources.Ок);
                            var flyoutItemhelp = (FlyoutItem)Shell.Current.Items.FirstOrDefault(item => item.Route.Equals("login"));
                            if (flyoutItemhelp != null)
                            {
                                flyoutItemhelp.IsVisible = false;
                            }
                            var flyoutItemseting = Shell.Current.Items.FirstOrDefault(item => item.Route.Equals("IMPL_seting"));
                            if (flyoutItemseting != null)
                            {
                                flyoutItemseting.IsVisible = false;
                            }
                            break;
                    }}}}

  


        /// <summary>
        /// Пароль 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nameEntry1_TextChanged(object sender, TextChangedEventArgs e)
        {
            Password = nameEntry1.Text;
        }


 
        /// <summary>
        /// Вход  в учетную запись команда 003
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void CounterLog_Clicked(object sender, EventArgs e)
        {
            try
            {
                Ip_adress ip_Adress = new Ip_adress();
                ip_Adress.CheckOS();
                ExamModels.Ip_adress.Ip_adresss = ip_Adress.Ip_adressss;
                Ping pingSender = new Ping();

                //IPHostEntry hostEntry = System.Net.Dns.GetHostEntry(ip_Adress.Ip_adressss);
                //foreach (System.Net.IPAddress address in hostEntry.AddressList)
                //{
                //    ip_Adress.Ip_adressss = address.ToString();
                //    break;
                //}

                //IPAddress[] addresses = Dns.GetHostAddresses(ip_Adress.Ip_adressss);

                //foreach (IPAddress address in addresses)
                //{
                //    //Console.WriteLine("IP Address: " + address.ToString());
                //    ip_Adress.Ip_adressss = address.ToString();
                //    break;
                //}
                //string localIP;
                //using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
                //{
                //    socket.Connect(ip_Adress.Ip_adressss, 9595);
                //    IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                //    localIP = endPoint.Address.ToString();
                //}

                //string interfaceDescription = string.Empty;
                //var result = new List<IPAddress>();
                //var upAndNotLoopbackNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces().Where(n => n.NetworkInterfaceType != NetworkInterfaceType.Loopback
                //                                                                                              && n.OperationalStatus == OperationalStatus.Up);

                //foreach (var networkInterface in upAndNotLoopbackNetworkInterfaces)
                //{
                //    var iPInterfaceProperties = networkInterface.GetIPProperties();

                //    var unicastIpAddressInformation = iPInterfaceProperties.UnicastAddresses.FirstOrDefault(u => u.Address.AddressFamily == AddressFamily.InterNetwork);
                //    if (unicastIpAddressInformation == null) continue;

                //    result.Add(unicastIpAddressInformation.Address);

                //    interfaceDescription += networkInterface.Description + "---";
                //}

                //var address = NetworkInterface.GetAllNetworkInterfaces().Where(_ => _.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 &&
                //                                                            _.OperationalStatus == OperationalStatus.Up)
                //.SelectMany(_ => _.GetIPProperties().UnicastAddresses).ToList();

                //[assembly: Dependency(typeof(YourAppNamespace.Android.Android.DependencyServices.IPAddressManager))]

                //IPAddress[] addresses = Dns.GetHostAddresses(ip_Adress.Ip_adressss);

                //foreach (IPAddress address in addresses)
                //{
                //    if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                //    {
                //        Console.WriteLine("IP Address: " + address.ToString());
                //    }
                //}
                //string FileFS = "";

                //Ping(FileFS, ip_Adress);


                PingReply reply = pingSender.Send(ip_Adress.Ip_adressss, 50);
                string FileFS = "";
                if (reply.Status == IPStatus.Success)
                {
                    //  Start(FileFS, ip_Adress);
                    //IPHostEntry hostEntry = Dns.GetHostEntry(ip_Adress.Ip_adressss);
                    //foreach (IPAddress address in hostEntry.AddressList)
                    //{
                    //    ip_Adress.Ip_adressss = address.ToString();
                    //    break;
                    //}
                    Ping(FileFS, ip_Adress);

                }
                else
                {
                    FileFS = "";
                    //   Start(FileFS, ip_Adress);
                    Ping(FileFS, ip_Adress);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert(AppResources.Ошибка, AppResources.Сообщение + ex.Message + "\n" + AppResources.Помощь + ex.HelpLink, AppResources.Ок);
            }
        }


        /// <summary>
        /// Регистрация
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private   void CounterBtn_Clicked_1(object sender, EventArgs e)
        {
            try
            {

                // Создание NavigationPage с главной страницей
                //var mainPage = new ;
                //var navigationPage = new NavigationPage(mainPage);
                Ip_adress ip_Adress = new Ip_adress();
                ip_Adress.CheckOS();
                Ping pingSender = new Ping();
                PingReply reply = pingSender.Send(ip_Adress.Ip_adressss, 500);
                if (reply.Status == IPStatus.Success)
                {
                    RegUser(ip_Adress);
                }
                else
                {
                    RegUser(ip_Adress);
                }
            }
            catch
            {

            }
            //// Создание NavigationPage с главной страницей
            //var mainPage = new MainPage();
            //var navigationPage = new NavigationPage(mainPage);

            //// Установка NavigationPage в качестве главной страницы приложения

            //  вход_В_Учетную_Запись =new();
            // INavigation navigation = new.Navigation;

            // Переход на предыдущую страницу

            //   await navigation.PopAsync();

            //вход_В_Учетную_Запись.GetVisualElementWindow().Content.CaptureAsync();
            //Открывает
            //  вход_В_Учетную_Запись.DisplayAlert(Title,"Открывает","");
        }

        public async void RegUser(Ip_adress ip_Address)
        {
            try
            {
                Галочка галочка = null;
                using (MemoryStream stream = new MemoryStream())
                {
                    Галочка галочка1 = new Галочка(1, $"{ip_Address.Ip_adressss}");
                    var Result = checkPing.CheckPingIp(галочка1);
                    галочка = Result;
                }
                if (галочка == null)
                {
                    await DisplayAlert(AppResources.Уведомление, AppResources.Сервервыключенилинедоступен, AppResources.Ок);
                    return;
                }
                await Navigation.PushAsync(new RegUser());
            }
            catch (Exception ex)
            {
                await DisplayAlert(AppResources.Ошибка, AppResources.Сообщение + ex.Message + "\n" + AppResources.Помощь + ex.HelpLink, AppResources.Ок);

            }
        }
        private void nameEntry_TextChanged_1(object sender, TextChangedEventArgs e)
        {
        }

        private void nameEntry9_TextChanged(object sender, TextChangedEventArgs e)
        { 
            Mail = nameEntry9.Text;

        }

        private void ContentPage_Loaded(System.Object sender, System.EventArgs e)
        {
            try
            {


                //  Application.Current.MainPage.SetValue(NavigationPage.HasNavigationBarProperty, false);
                //    Application.Current.MainPage.SetValue(NavigationPage.HasBackButtonProperty, false);

                Application.Current.MainPage.Window.Width = 530.8d;
                Application.Current.MainPage.Window.Height = 650.8d;

                Application.Current.MainPage.Window.MinimumWidth = 530.8d;
                Application.Current.MainPage.Window.MinimumHeight = 650.8d;

                Application.Current.MainPage.Window.MaximumWidth = 530.8d;
                Application.Current.MainPage.Window.MaximumHeight = 650.8d;

                //nameEntry9.Text = "maks_nt@list.ru";
                //nameEntry1.Text = "1";
                //nameEntry9.Text = "Admin@Admin.ru";
                //nameEntry1.Text = "Admin";
                if (Shell.Current.CurrentState.Location.OriginalString.Contains("sell=admin"))
                {
                    nameEntry9.Text = "Admin@Admin.ru";
                    nameEntry1.Text = "Admin";
                    //nameEntry9.Text = "+";
                    //nameEntry1.Text = "1";
                }
                else if (Shell.Current.CurrentState.Location.OriginalString.Contains("sell=user"))
                {
                    // Handle user parameter
                    nameEntry9.Text = "c";
                    nameEntry1.Text = "1";
                }
                else if (Shell.Current.CurrentState.Location.OriginalString.Contains("sell=help"))
                {
                    // Handle help parameter
                }
                else if (Shell.Current.CurrentState.Location.OriginalString.Contains("//login"))
                {
                    var flyoutItem = (FlyoutItem)Shell.Current.Items.FirstOrDefault(item => item.Route.Equals("admin"));
                    if (flyoutItem != null)
                    {
                        flyoutItem.IsVisible = false;
                    }


                    var flyoutItemUser = (FlyoutItem)Shell.Current.Items.FirstOrDefault(item => item.Route.Equals("User"));
                    if (flyoutItemUser != null)
                    {
                        flyoutItemUser.IsVisible = false;
                    }
                    var flyoutItemhelp = (FlyoutItem)Shell.Current.Items.FirstOrDefault(item => item.Route.Equals("help"));
                    if (flyoutItemhelp != null)
                    {
                        flyoutItemhelp.IsVisible = false;
                    }

                    //nameEntry9.Text = "Admin@Admin.ru";
                    //nameEntry1.Text = "Admin";
                    //nameEntry9.Text = "Полина@Admin.ru";
                    //nameEntry1.Text = "12";


                }


            }
            catch
            {

            }

        }
    }
}
