using ExamModels;
using Client.Main;
using Newtonsoft.Json;
using ExamClient.Resources.Resx;
using System.Globalization;
using System.Net;

namespace Client.Project;

public partial class Settings : ContentPage
{

    public int PortEntrys { get; set; }
    public string AddressEntrys { get; set; }


    public Dictionary<string, int> SettingLanguage = new Dictionary<string, int>();

    public void Reg()
    {
        SettingLanguage.Add("Россия", 1);
        SettingLanguage.Add("Англиский", 2);
        SettingLanguage.Add("Russia", 1);
        SettingLanguage.Add("English", 2);
    }
    Ip_adress ip_Adress = new Ip_adress();


    public Settings()
    {

       

        InitializeComponent();

        Reg();
        ip_Adress.CheckOS();
        AddressEntry.Text = ip_Adress.Ip;
        PortEntry.Text = ip_Adress.Port.ToString();
        List<RefUser> refUser = new List<RefUser>();


        switch (ip_Adress.language)
        {
            case 1:
                RefUser refUser1 = new RefUser() { User = "Россия" };
                refUser.Add(refUser1);
                refUser1 = new RefUser() { User = "Англиский" };
                refUser.Add(refUser1);

                break;
            case 2:
                RefUser refUser2 = new RefUser() { User = "Russia" };
                refUser.Add(refUser2);
                refUser2 = new RefUser() { User = "English" };
                refUser.Add(refUser2);
                break;

        }
        SettingList.ItemsSource = refUser;
    }

    private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem == null)
            return;
        var selectedTestQuestion = (RefUser)e.SelectedItem;
        int Int = 0;
        if (SettingLanguage.TryGetValue(selectedTestQuestion.User, out var settingLanguage))
        {
            Int = settingLanguage;
            ip_Adress.Update(AddressEntry.Text, int.Parse(PortEntry.Text), settingLanguage, Int);
        }
        else
        {
        }

           App s = new App();
        
       ((ListView)sender).SelectedItem = null;
    }



    public void SaveButtonClicked(object sender, EventArgs e)
    {
        try
        {
            string Path = "";

            if (DeviceInfo.Platform == DevicePlatform.iOS)
            {
                //Path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                Console.WriteLine("Операционная система: iOS");
                // Создание экземпляра класса Seting
                Seting setting = new Seting(AddressEntrys, PortEntrys, 1);

                // Преобразование объекта Seting в JSON строку
                string json = JsonConvert.SerializeObject(setting);

                // Запись JSON строки в файл
                File.WriteAllText("Client.json", json);

                // Чтение файла и преобразование JSON строки в объект Seting
                string jsonFromFile = File.ReadAllText("Client.json");

                Seting settingsFromFile = JsonConvert.DeserializeObject<Seting>(jsonFromFile);

                //  Ip_adressss = settingsFromFile.Ip_adress;

                // Доступ к данным
                string ipAddress = settingsFromFile.Ip_adress;
                //     PortEntry.Text = settingsFromFile.Port.ToString();
                int typeSQL = settingsFromFile.TypeSQL;

            }
            else if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                Console.WriteLine("Операционная система: Android");
                //Path = FileSystem.AppDataDirectory;
           

                // Создание и запись в JSON-файл
                string fileName = "Client.json";
                string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), fileName);
                int end = 0;
                if (File.Exists(path))
                {
                    ip_Adress.CheckOS();
                    end = ip_Adress.language;
                    File.Delete(path);
                }


               
                Seting seting = new Seting(AddressEntrys, PortEntrys, end);
                // Преобразование в JSON-строку
                string json = JsonConvert.SerializeObject(seting, Formatting.Indented);
      
                File.WriteAllText(path, json);
                // Чтение JSON-файла
                //     string jsonFromFile = File.ReadAllText(path);
                // Преобразование JSON-строки в объект Seting
                // Seting setingFromFile = JsonConvert.DeserializeObject<Seting>(jsonFromFile);

                // Вывод данных из объекта setingFromFile
                //  Ip_adressss = setingFromFile.Ip_adress;
                //    AddressEntry.Text = setingFromFile.Ip_adress.ToString();

                //      PortEntry.Text = setingFromFile.Port.ToString();
                //   Console.WriteLine($"Ip_adress: {setingFromFile.Ip_adress}");
                //     Console.WriteLine($"Port: {setingFromFile.Port}");
                //   Console.WriteLine($"TypeSQL: {setingFromFile.TypeSQL}");
            }
            else if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                //string assemblyPath = Assembly.GetEntryAssembly().Location;
                //Path = System.IO.Path.GetDirectoryName(assemblyPath);
                //     Path = System.IO.Path.Combine(Environment.CurrentDirectory, "..", "..", "MyProject");
                //     Path = Environment.CurrentDirectory.ToString();



                //string[] args = Environment.GetCommandLineArgs();
                //string assemblyPath = args[0];
                ////Path = Path.GetDirectoryName(assemblyPath);
                //string path = Directory.GetCurrentDirectory();
                ////var d = Environment.ProcessPath;
                //string currentDirectory = Environment.CurrentDirectory;
                //Console.WriteLine($"Текущая рабочая директория: {currentDirectory}");
                string appDirectory = System.AppContext.BaseDirectory;

                FileInfo fileInfo = new FileInfo(appDirectory + "\\Client.json");
                ip_Adress.CheckOS();
                // Если есть то загружаем настройки сервера если нет то создают
                if (fileInfo.Exists)
                {
                    File.Delete(appDirectory + "\\Client.json");
                    using (FileStream fs = new FileStream(appDirectory + "\\Client.json", FileMode.OpenOrCreate))
                    {
                        Seting seting = new Seting(AddressEntrys, PortEntrys, ip_Adress.language);
                        System.Text.Json.JsonSerializer.Serialize<Seting>(fs, seting);
                        //      Ip_adressss = _aFile.Ip_adress;

                        //AddressEntry.Text = _aFile.Ip_adress.ToString();
                        //PortEntry.Text = _aFile.Port.ToString();

                    }
                }
                else
                {
                    using (FileStream fileStream = new FileStream(appDirectory + "\\Client.json", FileMode.OpenOrCreate))
                    {
                        Seting connect_Server_ = new Seting(AddressEntrys, PortEntrys, 1);
                        System.Text.Json.JsonSerializer.Serialize<Seting>(fileStream, connect_Server_);
                    }


                    using (FileStream fileStream = new FileStream(appDirectory + "\\Client.json", FileMode.OpenOrCreate))
                    {
                        Seting aFile = System.Text.Json.JsonSerializer.Deserialize<Seting>(fileStream);

                        //    AddressEntry.Text = aFile.Ip_adress.ToString();
                        //    PortEntry.Text = aFile.Port.ToString();
                        //           Ip_adresss = aFile.Ip_adress;
                        //      Ip_adressss = Ip_adresss.ToString();
                    }

                }
            }
            else if (DeviceInfo.Platform == DevicePlatform.macOS)
            {
                Path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                Console.WriteLine("Операционная система: macOS");
            }
        }
        catch (Exception ex)
        {
            DisplayAlert(AppResources.Ошибка, ex.Message, AppResources.Ок);
        }


        Navigation.PushAsync(new MainPage());
    }

    public async void CancelButtonClicked(object sender, EventArgs e)
    {

        await Navigation.PushAsync(new MainPage());
        //await Navigation.PopAsync();
    }

    private async void PortEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(PortEntry.Text))
            {
         
                PortEntrys = 0;
            }
            else
            {
                PortEntrys = Convert.ToInt32(PortEntry.Text);

            }
        }
        catch (Exception ex)
        {

           await DisplayAlert("Ошибка", ex.Message.ToString(), "ОК");
        }
    }

    private void AddressEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        AddressEntrys = AddressEntry.Text;
    }


    public class RefUser
    {
        public string User { get; set; }
    }

}