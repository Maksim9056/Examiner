﻿using ExamClient.Resources.Resx;
using ExamModels;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Compatibility;
using System.Net.Mail;
using System.Net;
using System.Windows.Input;
using static Microsoft.Maui.Controls.Device;

namespace Client.Users.Doc.DocStatisticsUserResult;

public partial class DocStatisticsUserResult : ContentPage
{

    //private UserExamsEditorViewModel viewModel;
    //private UserExamsManager viewModelManager;
    private ExamModels.User CurrrentUser;
    //public ExamModels.Exams vSelectedItem { get; set; }

    private CheckUsers command = new CheckUsers();

    public List<string> Commands = new List<string>();
    CheckStatickUserResult checkStatickUserResult = new CheckStatickUserResult();

    public ICommand TapCommand => new Command<string>(async (url) => await Launcher.OpenAsync(url));

    public CommandCL commands = new CommandCL();
    private ExamsTestEditorViewModel viewModels;
    private ExamsTestManager viewModelManagers;
    private ExamModels.Exams CurrrentExams;
    //   private ExamModels.User CurrrentUser;
    //   private CheckUsers commandS = new CheckUsers();
    //  public List<string> Commands = new List<string>();


  //  public CommandCL command = new CommandCL();
    //private ExamsTestEditorViewModel viewModel;
    //private ExamsTestManager viewModelManager;
    //private ExamModels.Exams CurrrentExams;
//    private ExamModels.User CurrrentUser;
    private CheckUsers commandS = new CheckUsers();
    public List<string> Commandss = new List<string>();
   // private ExamsTestManager viewModelManager;

    public DocStatisticsUserResult(User user)
    {
        InitializeComponent();
        Title = AppResources.Статистикаэкзаменов;

        CurrrentUser = user;
      List<Statictics> Result = GetUserExams(user);
   



        //Changes
        CreateTable(Result);
    }


    public void CreateTable(List<Statictics> statictics)
    {

        //var labelsAll = new List<List<string>>();

        //for (int i = 0; i < statictics.Count; i++)
        //{
        //    labelsAll[i] = new List<string> { statictics[i].Exams.Name_exam, statictics[i].Test.Name_Test, statictics[i].Count.ToString() };
        //}

        var labels = new List<List<string>>();
        var Lab = new List<string> {         AppResources.Экзамен, AppResources.Имятеста, AppResources.Оценка };
        labels.Insert(0, Lab);

        for (int i = 0; i < statictics.Count; i++)
        {
            var tt = new List<string> { statictics[i].Exams.Name_exam, statictics[i].Test.Name_Test, statictics[i].Count.ToString() };

            labels.Insert(i+1,tt);
            //labels[i].Add(statictics[i].Exams.Name_exam);
            //labels[i].Add(statictics[i].Test.Name_Test);
            //labels[i].Add(statictics[i].Count.ToString());
        }

        //var labels = new List<List<string>>
        //{
        //     new List<string> { "Имя", "Возраст", "Пол" },
        //     new List<string> { "Максим", "25", "Мужской" },
        //     new List<string> { "Полина", "30", "Женский" },
        //     new List<string> { "Полина3", "30", "Женский" },
        //     new List<string> { "Даниил", "22", "Мужской" },
        //     new List<string> { "Даниил2", "22", "Мужской" }
        //};


        var numRows = labels.Count;
        var numCols = labels[0].Count;

        for (int i = 0; i < numRows; i++)
        {
            RowDefinition row = new RowDefinition();
            row.Height = GridLength.Auto;
            DataTable.RowDefinitions.Add(row);

            for (int j = 0; j < numCols; j++)
            {
#pragma warning disable CS0618 // Тип или член устарел
                var label = new Label
                {
                    Text = labels[i][j],
                    TextColor = i == 0 ? Colors.White : Colors.Black,
                    BackgroundColor = i == 0 ? Colors.Gray : Colors.White,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Padding = new Thickness(6)
                };
#pragma warning restore CS0618 // Тип или член устарел

                DataTable.Add(label, j, i);
            }
        }





        // List<Statictics> statictics
        //DataTable.Add(nameLabel, 0, 0);
        //DataTable.Add(ageLabel, 1, 0);
        //DataTable.Add(genderLabel, 2, 0);
        //var labels = new string[statictics.Count(), statictics.Count()];

        //int k = 0;
        //int j1 = 1;
        //int j2 = 2;
        //for(int i = 0; i < statictics.Count; i++)
        //{

        //        labels[i, k] = statictics[i].Exams.Name_exam;
        //        labels[j1, k] = statictics[i].Test.Name_Test;
        //      labels[j2, k] = statictics[i].Count.ToString();

        //    k++;
        //}


        //var labelss = new string[,]
        //{
        //  { "Имя", "Возраст", "Пол" },
        //  { "Максим", "25", "Мужской" },
        //  { "Полина", "30", "Женский" },
        //  { "Полина3", "30", "Женский" },
        //  { "Даниил", "22", "Мужской" },
        //  { "Даниил2", "22", "Мужской" }
        //};


        ////Grid grid = new Grid();

        //var numRows = labels.GetLength(0);
        //var numCols = labels.GetLength(1);

        //for (int i = 0; i < numRows; i++)
        //{
        //    RowDefinition row = new RowDefinition();
        //    row.Height = GridLength.Auto;
        //    DataTable.RowDefinitions.Add(row);
        //    for (int j = 0; j < numCols; j++)
        //    {
        //        var label = new Label
        //        {
        //            Text = labels[i, j],
        //            TextColor = i == 0 ? Colors.White : Colors.Black,
        //            BackgroundColor = i == 0 ? Colors.Gray : Colors.White,
        //            HorizontalOptions = LayoutOptions.FillAndExpand,
        //            Padding = new Thickness(6)
        //        };
        //        DataTable.Add(label, j, i);
        //    }
        //}

        var frame = new Frame
        {
            Content = DataTable,
            BorderColor = Colors.Black,
            BackgroundColor = Colors.Transparent,
            HasShadow = false
        };

        //var nameLabel = new Label
        //{
        //    Text = "Имя",
        //    TextColor = Colors.White,
        //    BackgroundColor = Colors.Gray,
        //    HorizontalOptions = LayoutOptions.FillAndExpand,
        //    Padding = new Thickness(6)
        //};
        //var ageLabel = new Label
        //{
        //    Text = "Возраст",
        //    TextColor = Colors.White,
        //    BackgroundColor = Colors.Gray,
        //    HorizontalOptions = LayoutOptions.FillAndExpand,
        //    Padding = new Thickness(6)
        //};
        //var genderLabel = new Label
        //{
        //    Text = "Пол",
        //    TextColor = Colors.White,
        //    BackgroundColor = Colors.Gray,
        //    HorizontalOptions = LayoutOptions.FillAndExpand,
        //    Padding = new Thickness(6)
        //};
        //var johnNameLabel = new Label
        //{
        //    Text = "John",
        //    TextColor = Colors.Black,
        //    BackgroundColor = Colors.White,
        //    HorizontalOptions = LayoutOptions.FillAndExpand,
        //    Padding = new Thickness(6)
        //};
        //var johnAgeLabel = new Label
        //{
        //    Text = "25",
        //    TextColor = Colors.Black,
        //    BackgroundColor = Colors.White,
        //    HorizontalOptions = LayoutOptions.FillAndExpand,
        //    Padding = new Thickness(6)
        //};
        //var johnGenderLabel = new Label
        //{
        //    Text = "Male",
        //    TextColor = Colors.Black,
        //    BackgroundColor = Colors.White,
        //    HorizontalOptions = LayoutOptions.FillAndExpand,
        //    Padding = new Thickness(6)
        //};
        //var janeNameLabel = new Label
        //{
        //    Text = "Jane",
        //    TextColor = Colors.Black,
        //    BackgroundColor = Colors.White,
        //    HorizontalOptions = LayoutOptions.FillAndExpand,
        //    Padding = new Thickness(6)
        //};
        //var janeAgeLabel = new Label
        //{
        //    Text = "30",
        //    TextColor = Colors.Black,
        //    BackgroundColor = Colors.White,
        //    HorizontalOptions = LayoutOptions.FillAndExpand,
        //    Padding = new Thickness(6)
        //};
        //var janeGenderLabel = new Label
        //{
        //    Text = "Female",
        //    TextColor = Colors.Black,
        //    BackgroundColor = Colors.White,
        //    HorizontalOptions = LayoutOptions.FillAndExpand,
        //    Padding = new Thickness(6)
        //};

        ////var dataTable = new Grid();
        //DataTable.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        //DataTable.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        //DataTable.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

        //DataTable.Add(nameLabel, 0, 0);
        //DataTable.Add(ageLabel, 1, 0);
        //DataTable.Add(genderLabel, 2, 0);
        //DataTable.Add(johnNameLabel, 0, 1);
        //DataTable.Add(johnAgeLabel, 1, 1);
        //DataTable.Add(johnGenderLabel, 2, 1);
        //DataTable.Add(janeNameLabel, 0, 2);
        //DataTable.Add(janeAgeLabel, 1, 2);
        //DataTable.Add(janeGenderLabel, 2, 2);

        //var frame = new Frame
        //{
        //    Content = DataTable,
        //    BorderColor = Colors.Black,
        //    BackgroundColor = Colors.Transparent,
        //    HasShadow = false
        //};

        //// Заголовки столбцов
        //var nameLabel = new Label { Text = "Name", HorizontalOptions = LayoutOptions.Center };
        //var ageLabel = new Label { Text = "Age", HorizontalOptions = LayoutOptions.Center };
        //var genderLabel = new Label { Text = "Gender", HorizontalOptions = LayoutOptions.Center };

        //DataTable.Add(nameLabel, 0, 0);
        //DataTable.Add(ageLabel, 1, 0);
        //DataTable.Add(genderLabel, 2, 0);

        //// Данные таблицы
        //var johnNameLabel = new Label { Text = "John", HorizontalOptions = LayoutOptions.Center };
        //var johnAgeLabel = new Label { Text = "25", HorizontalOptions = LayoutOptions.Center };
        //var johnGenderLabel = new Label { Text = "Male", HorizontalOptions = LayoutOptions.Center };

        //DataTable.Add(johnNameLabel, 0, 1);
        //DataTable.Add(johnAgeLabel, 1, 1);
        //DataTable.Add(johnGenderLabel, 2, 1);

        //var janeNameLabel = new Label { Text = "Jane", HorizontalOptions = LayoutOptions.Center };
        //var janeAgeLabel = new Label { Text = "30", HorizontalOptions = LayoutOptions.Center };
        //var janeGenderLabel = new Label { Text = "Female", HorizontalOptions = LayoutOptions.Center };

        //DataTable.Add(janeNameLabel, 0, 2);
        //DataTable.Add(janeAgeLabel, 1, 2);
        //DataTable.Add(janeGenderLabel, 2, 2);
    }




    private async void GoBack(object sender, EventArgs e)
    {
        //if (Application.Current.MainPage is NavigationPage navigationPage)
        //{
        //    navigationPage.Navigation.PopAsync();
        //}
        await Shell.Current.Navigation.PopAsync();
    }

    private List<Statictics> GetUserExams(ExamModels.User user)
    {

   
     var Result =    checkStatickUserResult.CheckStatickUserResults(user);
    
        return Result;

    }
  
    public class RefExamsTest
    {
        public ExamModels.ExamsTest ExamsTest { get; set; }
        public string EditCommand { get; set; }
        public Command DelCommand { get; set; }
    }

    public class RefTest
    {
        public ExamModels.Test Test { get; set; }
        public string EditCommand { get; set; }
        public Command DelCommand { get; set; }
    }

    public class RefUserExams
    {
        public ExamModels.UserExams UserExams { get; set; }
        public string EditCommand { get; set; }
        public Command DelCommand { get; set; }
    }

    private void SendResultsByEmail(object sender, EventArgs e)
    {
        TakeScreenshotAsync();


    }
    //public async Task TakeScreenshotAsync()
    //{
    //    if (Screenshot.Default.IsCaptureSupported)
    //    {
    //        IScreenshotResult screen = await Screenshot.Default.CaptureAsync();

    //        Stream stream = await screen.OpenReadAsync();

    //        //var imageSource = ImageSource.FromStream(() => stream);


    //        //SmtpClient smtpClient = new SmtpClient("46.39.245.154");//Адрес сервиса
    //        //                                                        //smtpClient.EnableSsl = true;
    //        //smtpClient.Credentials = new NetworkCredential("info@экзаменатор.москва", "951951Ss!");
    //        //MailMessage mailMessage = new MailMessage();
    //        //mailMessage.From = new MailAddress("info@экзаменатор.москва");//От кого сообщение
    //        //                                                              //mailMessage.To.Add("info@экзаменатор.москва");
    //        //mailMessage.To.Add(CurrrentUser.Employee_Mail);//Кому отправить
    //        //mailMessage.Subject = $"Результаты";//Тема

    //        //mailMessage.Body = "Код подтверждения 1";//Текс тписьма
    //        //                                         // Добавление файла в качестве вложения
    //        ////Attachment attachment = new Attachment(filePath);
    //        ////mailMessage.Attachments.Add(attachment);
    //        //try
    //        //{
    //        //    smtpClient.Send(mailMessage);//Отправляем письмо
    //        //}
    //        //catch (Exception)
    //        //{
    //        //}
    //        TakeScreenshotAsync();
    //    }
    public ExamModels.Mail Mails = new ExamModels.Mail();
    Ip_adress ip_Adress = new Ip_adress();
    public async Task TakeScreenshotAsync()
    {
        try
        {
            var SFales = new ExamModels.SendToServers();
            ip_Adress.CheckOS();
            if (Screenshot.Default.IsCaptureSupported)
            {
                IScreenshotResult screen = await Screenshot.Default.CaptureAsync();

                Stream stream = await screen.OpenReadAsync();
                var imageSource = ImageSource.FromStream(() => stream);
                //Imafe.Source = imageSource;
                string appDirectory = "";
                ///Блок для уникальный даты сейчас
                List<string> list = new List<string>();
                DateTime dateTime = DateTime.Now;

                string DATE = "";
                var date = dateTime.ToString();
                var dates = date.Replace('.', '_');

                var DA = dates.Replace(':', '_');
                for (int i = 0; i < DA.Length; i++)
                {
                    if (DA[i].ToString() == "13")
                    {
                        DATE += "_";
                    }
                    else
                    {
                        list.Add(DA[i].ToString());
                    }
                }
                DATE = "";
                list.RemoveAt(10);
                list.Insert(10, "_");
                for (int i = 0; i < list.Count(); i++)
                {
                    if (list[i] == "")
                    {

                    }
                    else
                    {
                        DATE += list[i].ToString();
                    }
                }
                ///Заканчиваеться 
                

                //Блок с проверкой
                if (DeviceInfo.Platform == DevicePlatform.Android)
                {
#if ANDROID
//Для пути в dowload и там для сохранения
                    appDirectory = Android.OS.Environment.GetExternalStoragePublicDirectory(type: Android.OS.Environment.DirectoryDownloads).AbsolutePath;
#endif
                }
                else if (DeviceInfo.Platform == DevicePlatform.WinUI)
                {
                    appDirectory = System.IO.Path.GetTempPath();
                    string path2 = System.IO.Path.Combine(appDirectory, $"{Guid.NewGuid().ToString()}дата_{DATE}.jpg");
                }
                System.Diagnostics.Debug.WriteLine("Download Path: " + appDirectory);
                appDirectory = Path.Combine(appDirectory, "Examiner");
                if (!Directory.Exists(appDirectory))
                {
                    Directory.CreateDirectory(appDirectory);
                    Console.WriteLine("Папка Downloads успешно создана.");
                }

                string downloadsPath = Path.Combine(appDirectory, $"{Guid.NewGuid().ToString()}дата_{DATE}.jpg");

                if (File.Exists(downloadsPath))
                {
                    File.Delete(downloadsPath);
                    Console.WriteLine("Файл успешно удален.");
                }
                else
                {
                    Console.WriteLine("Файл не существует.");
                }
                //Используем память  для конвертации типа удобно
                MemoryStream memoryStream = new MemoryStream();
                stream.CopyTo(memoryStream);

                //Сохраняем скриншот
                File.WriteAllBytes(downloadsPath, memoryStream.ToArray());

                //Сохраняем скриншот  на сервер  для сохранения
                object fileIdObj = await SFales.SendToServer(ip_Adress.Ip_adressss,ExamModels. Commands.UploadFile, downloadsPath);
                //Получаем id скриншота с результатми
                int fileId = (int)fileIdObj;

                //Отправляем на сервер
                TravelMailResult(fileId, ip_Adress.Ip_adressss);

                //Filles.Id = fileId;
            }
        }
        catch(Exception )
        {

        }
    }  

    /// <summary>
    /// Для отправки на  сервер для почты 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="Ip_adressss"></param>
    public void TravelMailResult(int id, string Ip_adressss)
    {
        try
        {
            CurrrentUser.Email.Id = id;
            Mails.UserTravelMailResult(CurrrentUser, Ip_adressss);
        }
        catch (Exception)
        {

        }
    }
}
