using ExamModels;
using Microsoft.Maui.ApplicationModel.Communication;
using System.Text.Json;
using System.Text;
using TextChangedEventArgs = Microsoft.Maui.Controls.TextChangedEventArgs;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using System.IO.Pipes;
using static ExamModels.CommandCL;
using System.Collections;
using Microsoft.Maui.Controls.Shapes;
using static System.Net.WebRequestMethods;
using System.Net.NetworkInformation;
using SkiaSharp;
using System.Net.Sockets;
using Microsoft.Maui.Controls;
using System.Buffers.Text;
using ExamClient.Resources.Resx;

namespace Client.Main;

public partial class RegUser : ContentPage
{
    Filles_Work filles_Work = new Filles_Work();

    Filles_Work_ filles_Work_ = new Filles_Work_();

    public RegUser()
    {
        InitializeComponent();
    }


    public Filles Filles { get; set; } = new Filles();

    /// <summary>
    /// Экземпляр класса CommandCL
    /// </summary>
    public CommandCL command = new CommandCL();


    /// <summary>
    ///Почта пользователя
    /// </summary>
    public string Mail { get; set; }


    /// <summary>
    ///Имя 
    /// </summary>
    public string User_Name { get; set; }


    public string CodMail { get; set; }

    /// <summary>
    ///Пароль пользователя
    /// </summary>
    public string Password { get; set; }


    /// <summary>
    ///Пароль пользователя1
    /// </summary>
    public string Password1 { get; set; }


    /// <summary>
    ///Пароль пользователя
    /// </summary>
    public string Rechte { get; set; }


    public CheckPing checkPing = new CheckPing();

    public Roles Roles { get; set; }

    public ExamModels.Mail Mails = new ExamModels.Mail();



    /// <summary>
    /// Id картинки пользователя
    /// </summary>
    public  int Id_Filles { get; set; }

    public bool IsAdmin { get; set; } = false;



    public int confirmation_code { get; set; }
    async public   void Connect()
    {
        try
        {
            Ip_adress ip_Adress = new Ip_adress();
            ip_Adress.CheckOS();

            ExamModels.Ip_adress.Ip_adresss = ip_Adress.Ip_adressss;
            Ping pingSender = new Ping();
            PingReply reply = pingSender.Send(ip_Adress.Ip_adressss, 500);
          //  string FileFS = "";
            if (reply.Status == IPStatus.Success)
            {
                //  Start(FileFS, ip_Adress);

                
            }
            else
            {
             //   FileFS = "";
                //   Start(FileFS, ip_Adress);
              
            }
        }
        catch(Exception ex) 
        {
           await DisplayAlert(AppResources.Ошибка,AppResources.Сообщение + ex.Message + "\n" + AppResources.Помощь + ex.HelpLink,AppResources.Ок );

        }
      
    }
    /// <summary>
    ///Пароль пользователя присываеваем значение
    /// </summary>
    private void nameEntry1_TextChanged(object sender, TextChangedEventArgs e)
    {
        Password = nameEntry1.Text;

    }


    /// <summary>
    ///Почту пользователя присваеваем значение
    /// </summary>
    private void nameEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
   
        Mail = nameEntry.Text;
    }

    /// <summary>
    ///Пароль подтвердеждения пользователя присываеваем значение
    /// </summary>
    private void nameEntry2_TextChanged(object sender, TextChangedEventArgs e)
    {
        Password1 = nameEntry2.Text;
    }

    public async void Reg(object sender, EventArgs e)
    {
        try
        {
            Ip_adress ip_Adress = new Ip_adress();
            ip_Adress.CheckOS();
            Галочка галочка = null;
            using (MemoryStream stream = new MemoryStream())
            {
                Галочка галочка1 = new Галочка(1, $"{ip_Adress.Ip_adressss}");
                var Result = checkPing.CheckPingIp(галочка1);
                галочка = Result;
            }
            if (галочка == null)
            {
                await DisplayAlert(AppResources.Уведомление, AppResources.Сервервыключенилинедоступен, AppResources.Ок);
                return;
            }
            if (Password1 == null || Password1 == "")
            {
                await DisplayAlert(AppResources.Уведомление, AppResources.Парольнаподтверждениенезаполнен, AppResources.Ок);
            }
            if (Password == null || Password == "")
            {
                await DisplayAlert(AppResources.Уведомление, AppResources.Парольнезаполнен, AppResources.Ок);
            }
            if (User_Name == null || User_Name == "")
            {
                await DisplayAlert(AppResources.Уведомление, AppResources.Имянезаполнено, AppResources.Ок);
            }
            if (Roles == null)
            {
                await DisplayAlert(AppResources.Уведомление, AppResources.Незаполненоразрешение, AppResources.Ок);
            }


            if (Filles == null)
            {
                await DisplayAlert(AppResources.Уведомление, AppResources.Вынесфоткалисьзайдитезаногонарегистрациюисфоткайтесь, AppResources.Ок);

            }
            else
            {

                
                if (Mail == null || Mail == "")
                {

                    await DisplayAlert(AppResources.Уведомление, AppResources.Почтанезаполнена, AppResources.Ок);
                }
                if (!Regex.IsMatch(Mail, "@."))
                {
                    await DisplayAlert(AppResources.Уведомление, AppResources.Некорректныйадресэлектроннойпочты, AppResources.Ок);
                }
                else
                {

                    if (IsAdmin == false)
                    {



                        ExamModels.User user = new ExamModels.User() { Employee_Mail = Mail, Name_Employee = User_Name };

                        User users = Mails.RegUserMail(user, ip_Adress.Ip_adressss);
                        confirmation_code = Convert.ToInt32(users.Name_Employee);
                        IsAdmin = true;
                    }

                    if (CodMail == "")
                    {
                        await DisplayAlert(AppResources.Уведомление, AppResources.Пустаяпочта, AppResources.Ок);

                    }
                    else
                    {
                        if (confirmation_code == Convert.ToInt32(CodMail))
                        {

                            using (MemoryStream Reg_user_Dispons = new MemoryStream())
                            {
                                CommandCL command = new CommandCL();
                                string FileFS = "";
                                using (MemoryStream fs = new MemoryStream())
                                {
                                    int Fi = 0;
                                    if (Filles != null)
                                    {
                                        Fi = Filles.Id;
                                    }

                                    Regis_users tom = new Regis_users(0, User_Name, Password, Roles, Mail, Filles.Id);
                                    JsonSerializer.Serialize<Regis_users>(fs, tom);
                                    FileFS = Encoding.Default.GetString(fs.ToArray());
                                }
                                Task.Run(async () => await command.Reg_User(ip_Adress.Ip_adressss, FileFS, "002")).Wait();
                                var Message = CommandCL.Travel_Regis_users_message;
                                // Остальной код для фильтрации по имени
                                if (Message == null)
                                {
                                    await DisplayAlert(AppResources.Уведомление, AppResources.Вынезарегистрировались, AppResources.Ок);
                                }
                                else
                                {
                                    await DisplayAlert(AppResources.Уведомление, AppResources.Вызарегистрировались, AppResources.Ок);


                                }
                                nameEntry3.Text = null;
                                nameEntry.Text = null;
                                nameEntry1.Text = null;
                                nameEntry2.Text = null;
                                await Navigation.PopAsync();
                            }
                        }
                        else
                        {
                            await DisplayAlert(AppResources.Уведомление, AppResources.Кодневерныйпосмотритекоднапочтеивведитезаногоподтверждения, AppResources.Ок);


                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert(AppResources.Ошибка, AppResources.Сообщение + ex.Message + "\n" + AppResources.Помощь + ex.HelpLink, AppResources.Ок);

        }
    }
    /// <summary>
    ///Регестрация и проверки 
    /// </summary>
    private  void Button_Clicked(object sender, EventArgs e)
    {
        Reg(sender, e);
        //Пароль сравниваем если пароли одинаковые то отправляем на сервер

      
    }

    private void nameEntry1_TextChanged_1(object sender, TextChangedEventArgs e)
    {

    }


    /// <summary>
    ///Имя пользователя присываеваем значение
    /// </summary>
    private void nameEntry1_TextChanged_2(object sender, TextChangedEventArgs e)
    {
        User_Name = nameEntry3.Text;
    }

    /// <summary>
    ///Подверждения пользователя пользователя присываеваем значение
    /// </summary>
    private void nameEntry4_TextChanged(object sender, TextChangedEventArgs e)
    {
        //Исправим вместо 1 будет список должностей а затем id получит!
       // Rechte = nameEntry4.Text;
    }

    private  void header_SizeChanged(object sender, EventArgs e)
    {
      
    }

    private void languagePicker_SelectedIndexChanged(object sender, EventArgs e)
    {

        
    }

    private  void languagePicker_ParentChanged(object sender, EventArgs e)
    {
    
    }

    //private async void languagePicker1_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //}

    private  List<RefExamsTest> Picker()
    {
        try
        {
            List<RefExamsTest> refExamsTests = new List<RefExamsTest>();
            Ip_adress ip_Adress = new Ip_adress();
            ip_Adress.CheckOS();
            Task.Run(async () => await command.Get_Image(ip_Adress.Ip_adressss, "", "006")).Wait();
            Roles Client = CommandCL.roles_Accept_Client;
            //    
            //   Client.
  
            if (Client == null)
            {
                 DisplayAlert(AppResources.Уведомление, AppResources.Ролейнет, AppResources.Ок);
            }
            else
            {
                //string[] strings = new string[CommandCL.Roles_Accept_Client.Test.Count()];
                //for (int i = 0; i < strings.Length; i++)
                //{
                //    strings[i] = CommandCL.Roles_Accept_Client.Test[i].ToString();
                //}

                for (int i = 0; i < 1; i++)
                {
                    var refExamsTest = new RefExamsTest { ExamsTest = Client};
                    refExamsTests.Add(refExamsTest);
                }
                

                
             //   languagePicker1.Items.Add("");


                //       await DisplayAlert("Уведомление", "Роль есть", "ОK");
            }
            return refExamsTests;
        }
        catch(Exception ex)
        {
             DisplayAlert(AppResources.Ошибка,  AppResources.Сообщение+ ex.Message + "\n" + AppResources.Помощь+ ex.HelpLink, AppResources.Ок);

        }
        return null;
    }
    private void ContentPage_Loaded(System.Object sender, System.EventArgs e)
    {
        try
        {
            TestList.ItemsSource = Picker();



            //Application.Current.MainPage.Window.Width = 413.8d;
            //Application.Current.MainPage.Window.Height = 520.8d;

            //Application.Current.MainPage.Window.MinimumWidth = 413.8d;
            //Application.Current.MainPage.Window.MinimumHeight = 520.8d;

            //Application.Current.MainPage.Window.MaximumWidth = 413.8d;
            //Application.Current.MainPage.Window.MaximumHeight = 520.8d;
        }
        catch(Exception s)
        {
            DisplayAlert(AppResources.Ошибка, s.Message, AppResources.Ок);
        }
    }



    public class RefExamsTest
    {
        public ExamModels.Roles ExamsTest { get; set; }
        public Command EditCommand { get; set; }
        public Command DelCommand { get; set; }
    }

    private async void Button_Clicked_1(System.Object sender, System.EventArgs e)
    {
        // Создание NavigationPage с главной страницей
        //var mainPage = new MainPage();
        //var navigationPage = new NavigationPage(mainPage);

        //Application.Current.MainPage = navigationPage;
        await Navigation.PushAsync(new MainPage());

    }

    private void languagePicker1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private async void TestList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {


            if (e.SelectedItem == null)
                return;

            var selectedExamsTest = (RefExamsTest)e.SelectedItem;
            await DisplayAlert(AppResources.Выбраннаяроль, selectedExamsTest.ExamsTest.Name_roles, AppResources.Ок);

            ((ListView)sender).SelectedItem = null;
            Roles = selectedExamsTest.ExamsTest;

        } 
        catch (Exception ex)
        {
            await DisplayAlert(AppResources.Ошибка, AppResources.Сообщение + ex.Message + "\n" + AppResources.Помощь + ex.HelpLink, AppResources.Ок);

        }
    }

    private async void GoBack(object sender, EventArgs e)
    {
        await Navigation.PopAsync();

    }

    private void SaveButtonClick(object sender, EventArgs e)
    {
        Reg(sender,e);
    }



    public async Task<byte[]> ResizePhotoAsync()
    {
        // Захват фотографии
        var photo = await MediaPicker.CapturePhotoAsync();

        using (var inputStream = await photo.OpenReadAsync())
        using (var outputStream = new MemoryStream())
        {
            // Загрузка изображения с использованием библиотеки SkiaSharp
            var skBitmap = SKBitmap.Decode(inputStream);

            // Установка новых размеров
            var newWidth = 800; // Новая ширина фотографии
            var newHeight = 600; // Новая высота фотографии

            // Изменение размера изображения
            var resizedBitmap = skBitmap.Resize(new SKImageInfo(newWidth, newHeight), SKFilterQuality.Low);

            // Сохранение измененного изображения в поток
            using (var skOutputStream = new SKManagedWStream(outputStream))
            {
                resizedBitmap.Encode(skOutputStream, SKEncodedImageFormat.Png, 80);
            }

            // Возвращение измененной фотографии в виде массива байтов
            return outputStream.ToArray();
        }
    }

    private const int BufferSize = 8192; // Размер буфера для чтения фотографии

    public  void Main23(string pFile)
    {
        // Создаем экземпляр TcpClient и подключаемся к серверу
        using (TcpClient client = new TcpClient("192.168.1.170", 9595)) // Замени IP-адрес и порт на свои значения
        {
            try
            {
                // Получаем сетевой поток для отправки и чтения данных
                NetworkStream stream = client.GetStream();

                // Запускаем поток, в котором будет производиться захват фотографии и ее отправка
                Thread captureThread = new Thread(() =>
                {
                    // Захватываем фотографию с помощью камеры
                    byte[] photoBytes = CapturePhoto(pFile);

                    // Отправляем фотографию на сервер
                    SendPhoto(stream, photoBytes);

                    client.Close();
                });

                captureThread.Start();
                captureThread.Join();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{AppResources.Произошлаошибка} {ex.Message}");
            }
        }
    }

    private static byte[] CapturePhoto(string pFile)
    {
        // Здесь необходимо использовать библиотеки для работы с камерой
        // и получение фотографии в виде массива байтов
        // В данном примере мы просто считываем фотографию из файла "photo.jpg"

        //string photoPath = "photo.jpg"; // Путь к файлу с фотографией

        return System.IO.File.ReadAllBytes(pFile);
    }

    private static void SendPhoto(NetworkStream stream, byte[] photoBytes)
    {
        // Отправляем размер фотографии на сервер
        byte[] sizeBytes = BitConverter.GetBytes(photoBytes.Length);
        stream.Write(sizeBytes, 0, sizeBytes.Length);

        // Отправляем фотографию на сервер
        stream.Write(photoBytes, 0, photoBytes.Length);
    }




    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void Image_Loaded(object sender, EventArgs e)
    {
        try
        {
            Ip_adress ip_Adress = new Ip_adress();
            ip_Adress.CheckOS();

            if (MediaPicker.Default.IsCaptureSupported)
            {
                FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

                if (photo != null)
                {
                    var SFales = new ExamModels.SendToServers();


                    //byte[] imageBytes = System.IO.File.ReadAllBytes(photo.FullPath);

                    //int fileId =  Task.Run(async () => await SFales.SendFile(photo.FullPath)).Wait();
                    object fileIdObj = await SFales.SendToServer(ip_Adress.Ip_adressss, Commands.UploadFile, photo.FullPath);
                    int fileId = (int)fileIdObj;
                    await DisplayAlert(AppResources.Уведомление, fileId.ToString(), AppResources.Ок);
                    Filles.Id = fileId;

                    /*
                    //      Image_Loadeds(sender, e);
                    // save the file into local storage
                    byte[] imageBytes = System.IO.File.ReadAllBytes(photo.FullPath);


                    // рабочий
                    //byte[] imageBytes = await ResizePhotoAsync();

                    var utf8String = Convert.ToBase64String(imageBytes);

                    imageBytes = Convert.FromBase64String(utf8String);

                    Filles filles = new Filles(0, imageBytes);

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        Filles[] json_List_Friends = new Filles[1];
                        json_List_Friends[0] = filles;

                        JsonSerializer.Serialize<Filles[]>(memoryStream, json_List_Friends);
                        memoryStream.Position = 0; // Сбросить позицию потока перед чтением данных
                        using (StreamReader reader = new StreamReader(memoryStream, Encoding.UTF8))
                        {
                            string st = reader.ReadToEnd();
                            using (TcpClient client = new TcpClient(ip_Adress.Ip_adressss, 9595))
                            {
                                client.SendTimeout = 10000;
                                client.SendBufferSize = 100000;
                                using (NetworkStream stream = client.GetStream())
                                {
                                    byte[] data = Encoding.UTF8.GetBytes("057" + st);
                                    await stream.WriteAsync(data, 0, data.Length);


                                    StringBuilder completeMessage = new StringBuilder();
                                    byte[] readingData = new byte[256];
                                    int numberOfBytesRead = 0;
                                    do
                                    {
                                        numberOfBytesRead = await stream.ReadAsync(readingData, 0, readingData.Length);
                                        completeMessage.Append(Encoding.UTF8.GetString(readingData, 0, numberOfBytesRead));
                                    }
                                    while (stream.DataAvailable);

                                    string responseDat = completeMessage.ToString();

                                    Filles exams_Check = JsonSerializer.Deserialize<Filles>(responseDat);

                                    Filles = exams_Check;

                                }

                            }
                        }
                   
                    }


                    //

                    //Main23(photo.FullPath);

                    //Filles = filles_Work.FillesSave(filles, ip_Adress.Ip_adressss);


                    //string localFilePath = System.IO.Path.Combine(FileSystem.CacheDirectory, photo.FileName);
                    //using FileStream localFileStream = System.IO.File.OpenWrite(localFilePath);
                    //using Stream sourceStream = await photo.OpenReadAsync();
                    //await sourceStream.CopyToAsync(localFileStream);
                    //Images.Source = localFileStream.Name;
                    
                    */
                    Images.Source = photo.FullPath;
                    
                }
                else
                {
                    var SFales = new ExamModels.SendToServers();
                    photo = await MediaPicker.Default.PickPhotoAsync();
                    //await SFales.SendFile(photo.FullPath);
                    object fileIdObj = await SFales.SendToServer( ip_Adress.Ip_adressss, Commands.UploadFile, photo.FullPath);
                    int fileId = (int)fileIdObj;
                    Images.Source = photo.FullPath;
                    DisplayAlert(AppResources.Уведомление, fileId.ToString(), AppResources.Ок);
                    Filles.Id = fileId;

                    /*                    
                    photo = await MediaPicker.Default.PickPhotoAsync();
                    byte[] imageBytes = System.IO.File.ReadAllBytes(photo.FullPath);
                    using (MemoryStream memoryStreams = new MemoryStream())
                    {
                        Filles filles = new Filles(0,imageBytes);

                        Connect();
                        //Застывает и весит
                        Filles = filles_Work.FillesSave(filles, ip_Adress.Ip_adressss);
                        if (Filles == null)
                        {
                            Image_Loaded(sender, e);
                        }
                        else
                        {
                            string path = System.AppContext.BaseDirectory + "\\путь_к_файлу.jpg";


                            if (System.IO.File.Exists(path))
                            {
                                // Удаление файла
                                System.IO.File.Delete(path);
                            }
                            // Создание потока на основе массива байт изображения
                            using (MemoryStream memoryStream = new MemoryStream(Filles.Name))
                            {
                                // Сохранение изображения на диск
                                using (FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate))
                                {
                                    memoryStream.CopyTo(fileStream);
                                }
                            }
                            Images.Source = path;
                        }
                    }
                    */
                }
                photo = null;
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert(AppResources.Ошибка,AppResources.Сообщение  + ex.Message + "\n" + AppResources.Помощь + ex.Data, AppResources.Ок);
            //Image_Loaded(sender, e);
        }
    }


    //public async void Image_Loadeds(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        Ip_adress ip_Adress = new Ip_adress();
    //        ip_Adress.CheckOS();

    //        if (MediaPicker.Default.IsCaptureSupported)
    //        {
    //            FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

    //            if (photo != null)
    //            {
    //                // save the file into local storage
    //                byte[] imageBytes = System.IO.File.ReadAllBytes(photo.FullPath);

    //                var utf8String = Convert.ToBase64String(imageBytes);

    //                imageBytes = Convert.FromBase64String(utf8String);

    //                Filles filles = new Filles(0, imageBytes);

    //                using (MemoryStream memoryStream = new MemoryStream())
    //                {
    //                    Filles[] json_List_Friends = new Filles[1];
    //                    json_List_Friends[0] = filles;

    //                    JsonSerializer.Serialize<Filles[]>(memoryStream, json_List_Friends);
    //                    memoryStream.Position = 0;
    //                    using (StreamReader reader = new StreamReader(memoryStream, Encoding.UTF8))
    //                    {
    //                        string st = reader.ReadToEnd();
    //                        using (TcpClient client = new TcpClient(ip_Adress.Ip_adressss, 9595))
    //                        {
    //                            client.SendTimeout = 10000;
    //                            client.SendBufferSize = 100000;
    //                            using (NetworkStream stream = client.GetStream())
    //                            {
    //                                byte[] data = Encoding.UTF8.GetBytes("057" + st);
    //                                await stream.WriteAsync(data, 0, data.Length);

    //                                //StringBuilder completeMessage = new StringBuilder();
    //                                //byte[] readingData = new byte[256];
    //                                //int numberOfBytesRead = 0;
    //                                //do
    //                                //{
    //                                //    numberOfBytesRead = await stream.ReadAsync(readingData, 0, readingData.Length);
    //                                //    completeMessage.Append(Encoding.UTF8.GetString(readingData, 0, numberOfBytesRead));
    //                                //}
    //                                //while (stream.DataAvailable);

    //                                //string responseDat = completeMessage.ToString();

    //                                //Filles exams_Check = JsonSerializer.Deserialize<Filles>(responseDat);

    //                                //// Correct mistake here (Mistake)
    //                                //Filles = exams_Check;

    //                            }
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        // Handle exception
    //        Console.WriteLine(ex.Message);

    //    }
    //}

    private void nameEntry9_TextChanged(object sender, TextChangedEventArgs e)
    {
        CodMail = nameEntry9.Text;
    }
}

