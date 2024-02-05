using ExamModels;
using Client.Main;
using Client.Users.Doc.DocPersonalAchievement;
//using Microsoft.Maui.Graphics.Platform;
using Microsoft.Maui.Storage;
using SkiaSharp;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Net.NetworkInformation;
using System.Windows.Input;
using System.Xml.Linq;
using Image = Microsoft.Maui.Controls.Image;
using ExamClient.Resources.Resx;
using Microsoft.Maui.Controls;
using static ExamModels.CommandCL;

namespace Client.Users;

public partial class Users : ContentPage
{
    //public static string NameUsers { get; set; }
    //public static int id { get; set; }
    public static User user { get; set; }
    public Regis_users regis_Users { get; set; }
    public ICommand TapCommand => new Command<string>(async (url) => await Launcher.OpenAsync(url));

    public Filles_Work filles_Work = new Filles_Work();

    public Filles files { get; set; }


     public string Connect()
    {
        try
        {
            Ip_adress ip_Adress = new Ip_adress();
            ip_Adress.CheckOS();

            Ping pingSender = new Ping();
            PingReply reply = pingSender.Send(ip_Adress.Ip_adressss, 50);
            //  string FileFS = "";
            if (reply.Status == IPStatus.Success)
            {
                //  Start(FileFS, ip_Adress);
               ExamModels.Ip_adress.Ip_adresss = ip_Adress.Ip_adressss;

               
            }
            else
            {
                //   FileFS = "";
                //   Start(FileFS, ip_Adress);

            }
            return ip_Adress.Ip_adressss;
        }
        catch (Exception ex)
        {
             DisplayAlert(AppResources.������,AppResources.��������� + ex.Message + "\n" + AppResources.������ + ex.HelpLink, AppResources.��);

        }
        return null;
    }
    
    //public void Images(Regis_users name, Filles filles)
    //{
    //    try
    //    {
    //        NameUser.Text = name.Name_Employee;
    //        string IPAdress = Connect();
    //        Filles file = filles_Work.SelectFromFilles(IPAdress, filles);

    //        if (file == null)
    //        {
    //            //Images(name, filles);
    //        }
    //        else
    //        {
    //            files = filles_Work.Filles;
    //            Image(file);
    //        }
    //    }
    //    catch(Exception)
    //    {

    //    }
    //}

    public  async Task Images(Regis_users user, Filles selectedFiles)
    {
        try
        {
            if (user != null && selectedFiles != null)
            {
                NameUser.Text = user.Name_Employee;
                string ipAddress = Connect();
                //Filles file = filles_Work.SelectFromFilles(ipAddress, selectedFiles);
                //Filles file = await filles_Work.SelectFromFillesAsync(ipAddress, selectedFiles);
                FilesTravels filesTravels = new FilesTravels();
                Filles file = await filesTravels.SelectFromFilles(ipAddress, selectedFiles,Commands.DownloadFile);


                if (file != null)
                {
                    files = filles_Work.Filles;
                    file.Id = user.Id;
                    Image(file);
                }
            }
            else
            {
                // ��������� ������, ����� user ��� selectedFiles ����� null
            }
        }
        catch (Exception ex)
        {
            // ����� ����� ���������������� ������ ��� ������� �� � �������/������ ��� ������������ �������
          await  DisplayAlert(AppResources.������, AppResources.��������� + ex.Message + "\n" + AppResources.������ + ex.HelpLink, AppResources.��);
        }
    }



    public Users(Regis_users name )
    {
        try
        {   

            InitializeComponent();

            regis_Users = name;
            Filles filles = new Filles { Id = name.Filles };
            user = new User
            {
                Id = name.Id,
                Password = name.Password,
                Name_Employee = name.Name_Employee ,
                Employee_Mail = name.Employee_Mail,
                Id_roles_users = name.Rechte.Id,
                Email = filles

            };

            Images(name,filles);




        }
        catch (Exception)
        {

        }
        //ImageUser.Source 
    }



    public async void Image(Filles vfilles)
    {
        try
        {
            string path = System.AppContext.BaseDirectory + "����_�_�����.jpg";

            // �������� ������ �� ������ ������� ���� �����������

            if (DeviceInfo.Platform == DevicePlatform.iOS)
            {

                string paths = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "����_�_�����.jpg");


                if (System.IO.File.Exists(paths))
                {
                    // �������� �����
                    System.IO.File.Delete(paths);
                }
                using (MemoryStream memoryStream = new MemoryStream(vfilles.Name))
                {
                    // ��� ��� ���������
                    File.WriteAllText(paths, files.Name.ToString());
                }

                ImageUser.Source = File.ReadAllText(paths);

            }
            else if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                /*
                string filePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Files_"+ vfilles.Id + ".jpg");

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                using (FileStream stream = new FileStream(filePath, FileMode.OpenOrCreate))
                {
                    await stream.WriteAsync(vfilles.Name, 0, vfilles.Name.Length).ConfigureAwait(false);
                }

                // ��������� ���� � ����� ��� �������� ����������� ��� ImageUser
                //ImageUser.Source = ImageSource.FromFile(filePath);
                */

                var imageSource = ImageSource.FromStream(() => new MemoryStream(vfilles.Name));
                // ��������� ��������� �����������
                ImageUser.Source = imageSource;



                //string paths = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "����_�_�����.png");
                //string filePath = Path.Combine(Path.GetTempPath(), "����_�_�����.");

                // �������� ������� Bitmap �� MemoryStream

                //       Bitmap bitmap;


                // �������������� � ������ Bitmap

                //string filePaths = Path.Combine(Path.GetTempPath(), "image.jpg"); // 
                //string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "image.jpg");

                //if (System.IO.File.Exists(filePath))
                //    {
                //        // �������� �����
                //        System.IO.File.Delete(filePath);
                //    }

                //using (MemoryStream stream = new MemoryStream(vfilles.Name))
                //{
                //    // ������ ������ �� MemoryStream � ����
                //    System.IO.File.WriteAllBytes(filePath, stream.ToArray());

                //    // ��������� ���� � ����� ��� �������� ����������� ��� ImageUser
                //    ImageUser.Source = filePath;


                // image = Microsoft.Maui.Graphics.Platform.PlatformImage.FromStream(stream);
                //System.IO.File.WriteAllBytes(filePath, files.Name); 
                //ImageUser.Source  = filePath;
                //     bitmap = BitmapFactory.DecodeByteArray(ms.GetByteArray(), 0, ms.GetByteArray().Length); // ������������ �������� ������� Bitmap �� ������� ������

                //var image = new Image();
                //image.Source = ImageSource.FromFile(filePath);

                //   File.WriteAllText(paths, files.Name.ToString());
                //var sd   = File.ReadAllText(paths);
                //   ImageUser.Source = sd;

                //}
            }
            else if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                /*
                using (MemoryStream memoryStream = new MemoryStream(vfilles.Name))
                {
                    // ���������, ��� ���� � ����� ����������
                    //if (!System.IO.Directory.Exists(directoryPath))
                    //{
                    //    System.IO.Directory.CreateDirectory(directoryPath);
                    //}

                    // ���������� ����������� �� ����
                    string path2 = System.IO.Path.Combine(System.AppContext.BaseDirectory, "image.jpg");

                    // �������� �����, ���� �� ����������
                    if (System.IO.File.Exists(path2))
                    {
                        System.IO.File.Delete(path2);
                    }

                    using (FileStream fileStream = new FileStream(path2, FileMode.OpenOrCreate))
                    {
                        // ����������� ������ �� MemoryStream � FileStream
                        memoryStream.CopyTo(fileStream);
                    }


                    //ImageUser.Source = path2;
                }  
                */
                
                    var imageSource = ImageSource.FromStream(() => new MemoryStream(vfilles.Name));
                    // ��������� ��������� �����������
                    ImageUser.Source = imageSource;





            }
            else if (DeviceInfo.Platform == DevicePlatform.macOS)
            {

            }
        }
        catch(Exception ) 
        {
           // DisplayAlert("������", "���������" + ex.Message + "\n" + "������:" + ex.Data, "��");
        }
    }

   public void User_NAME(Regis_users name)
    {

   
    }
    private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem == null)
            return;

        //var selectedExamsTest = (RefExamsTest)e.SelectedItem;
        //await DisplayAlert("��������� ����", selectedExamsTest.ExamsTest.Exams.Name_exam, "OK");
        //((ListView)sender).SelectedItem = null;
    }

    private async void ExamButtonClicked(object sender, EventArgs e)
    {
        // User User =new  User() 

        //      Shell.Current.GoToAsync("examispersonal");
        await Navigation.PushAsync(new DocTheExamisPersonal(user));
        //await Shell.Current.GoToAsync(nameof(DocTheExamisPersonal), new Dictionary<string, object>
        //{
        //    { "user", user }
        //});
        //await Shell.Current.GoToAsync($"{nameof(DocTheExamisPersonal)}", new Dictionary<string, object>
        //{
        //     { nameof(User), user }
        //});

        // User
        //  ExamModels.User user
    }

    public async Task ReloadApplicationss()
    {

        // ������� ����� ��������� (�������� ���� ������� �� �����)
        await Navigation.PopToRootAsync();

        App.Current.MainPage = new AppShell();


    }
    private void GoBack(object sender, EventArgs e)
    {
        //if (Application.Current.MainPage is NavigationPage navigationPage)
        //{
        //    navigationPage.Navigation.PopAsync();
        //}
        //Shell.Current.GoToAsync("logout");
        ReloadApplicationss();
    }

    private async void AchievementsButtonClicked(object sender, EventArgs e)
    {
        //      Shell.Current.GoToAsync("achievement");
        await Navigation.PushAsync(new DocPersonalAchievement(user));

    }

    private void SettingsButtonClicked(object sender, EventArgs e)
    {

    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        //var flyoutItemUser = (FlyoutItem)Shell.Current.Items.FirstOrDefault(item => item.Route.Equals("User"));
        //if (flyoutItemUser != null)
        //{
        //    flyoutItemUser.IsVisible = true;
        //}

  


        var flyoutItemUser = Shell.Current.Items.FirstOrDefault(item => item.Route.Equals("IMPL_user2"));
        if (flyoutItemUser == null)
        {
            // �������� ������� ���� ������
            var main = new ShellContent { Content = new Client.Users.Users(regis_Users) };
            // ���������� ������� ���� � �����
            Shell.Current.Items.Add(new ShellSection { Title = AppResources.������������,
                Items = { main }, Icon = "dotnet_bot.png", Route = "user2" });

        }




    }
    private  async void Statictick_Users(object sender, EventArgs e)
    {

      //  Shell.Current.GoToAsync("statistics");
        await Navigation.PushAsync(new Client.Users.Doc.DocStatisticsUserResult.DocStatisticsUserResult (user));

    }
}