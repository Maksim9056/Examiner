﻿using ExamModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Reflection;

namespace Client
{
    public class Ip_adress
    {

        public string Ip { get; set; }  
        public static string Ip_adresss { get; set; }

        public string Ip_adressss { get; set; }

        public int language { get; set; }

        public int Port { get; set; }
        public int ColorStyles { get; set; }

        public void CheckOS()
        {
            try
            {
                string Path = "";
                if (DeviceInfo.Platform == DevicePlatform.iOS)
                {
                    ////Path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    //Console.WriteLine("Операционная система: iOS");
                    //// Создание экземпляра класса Seting
                    //Seting setting = new Seting("192.168.1.170", 9595, 1);

                    //// Преобразование объекта Seting в JSON строку
                    //string json = JsonConvert.SerializeObject(setting);

                    //// Запись JSON строки в файл
                    //File.WriteAllText("Client.json", json);

                    //// Чтение файла и преобразование JSON строки в объект Seting
                    //string jsonFromFile = File.ReadAllText("Client.json");
                    //Seting settingsFromFile = JsonConvert.DeserializeObject<Seting>(jsonFromFile);

                 //   Ip_adressss = settingsFromFile.Ip_adress;

                    // Доступ к данным
                    //string ipAddress = settingsFromFile.Ip_adress;
                    //int port = settingsFromFile.Port;
                    //int typeSQL = settingsFromFile.TypeSQL;

                }
                else if (DeviceInfo.Platform == DevicePlatform.Android)
                {
                    Console.WriteLine("Операционная система: Android");
                    //Path = FileSystem.AppDataDirectory;
                
                    // Преобразование в JSON-строку
   

                    // Создание и запись в JSON-файл
                    string fileName = "Client.json";
                    string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), fileName);
                  

                    bool fileExists = File.Exists(path);

                    if (fileExists)
                    {
                  
                        //"File Existence", "The file exists"

                    }
                    else
                    {
                       //"File Existence", "The file does not exist.
                        Seting seting = new Seting("192.168.1.170", 9595, 1, 1);
                        string json = JsonConvert.SerializeObject(seting, Formatting.Indented);
                        File.WriteAllText(path, json);
                       
                    }
                    // Чтение JSON-файла
                    string jsonFromFile = File.ReadAllText(path);

                    // Преобразование JSON-строки в объект Seting
                    Seting setingFromFile = JsonConvert.DeserializeObject<Seting>(jsonFromFile);

                    // Вывод данных из объекта setingFromFile
                    Ip_adressss = setingFromFile.Ip_adress;
                    Ip = setingFromFile.Ip_adress;
                    Port  = setingFromFile.Port;
                    language = setingFromFile.TypeSQL;
                    ColorStyles = setingFromFile.ColorStyles;
                    Console.WriteLine($"Ip_adress: {setingFromFile.Ip_adress}");
                    Console.WriteLine($"Port: {setingFromFile.Port}");
                    Console.WriteLine($"TypeSQL: {setingFromFile.TypeSQL}");
                    Console.WriteLine($"ColorStyles: {setingFromFile.ColorStyles}");
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
                    //                   string appDirectory = System.AppContext.BaseDirectory;

                    //string currentDirectory = Environment.CurrentDirectory;
                    //Console.WriteLine($"Текущая рабочая директория: {currentDirectory}");
              
                    string appDirectory =  System.IO.Path.GetTempPath(); 

                    //string appDirectory = System.AppContext.BaseDirectory;
                    FileInfo fileInfo = new FileInfo(appDirectory + "\\Client.json");

                    // Если есть то загружаем настройки сервера если нет то создают
                    if (fileInfo.Exists)
                    {
                        using (FileStream fs = new FileStream(appDirectory + "\\Client.json", FileMode.OpenOrCreate))
                        {
                            Seting _aFile = System.Text.Json.JsonSerializer.Deserialize<Seting>(fs);

                            Ip = _aFile.Ip_adress;
                            Ip_adressss = _aFile.Ip_adress;
                            Port = _aFile.Port;
                            language = _aFile.TypeSQL;
                            ColorStyles = _aFile.ColorStyles;

                        }
                    }
                    else
                    {
                        using (FileStream fileStream = new FileStream(appDirectory + "\\Client.json", FileMode.OpenOrCreate))
                        {
                            Seting connect_Server_ = new Seting("192.168.1.171", 9595, 1, 1);
                            System.Text.Json.JsonSerializer.Serialize<Seting>(fileStream, connect_Server_);
                        }


                        using (FileStream fileStream = new FileStream(appDirectory + "\\Client.json", FileMode.OpenOrCreate))
                        {
                            Seting aFile = System.Text.Json.JsonSerializer.Deserialize<Seting>(fileStream);
                            Ip_adresss = aFile.Ip_adress;
                            Ip = aFile.Ip_adress;

                            Port = aFile.Port;
                            language = aFile.TypeSQL;
                            ColorStyles = aFile.ColorStyles;

                            Ip_adressss = Ip_adresss.ToString();
                        }

                    }
                }
                else if (DeviceInfo.Platform == DevicePlatform.macOS)
                {
                    Path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    Console.WriteLine("Операционная система: macOS");



                }
            }
            catch(Exception)
            {

            }
        }

        public void Update(string AddressEntrys, int PortEntrys,int settingLanguage,int Int, int Int2)
        {
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                Console.WriteLine("Операционная система: Android");
                Seting seting = new Seting(AddressEntrys, PortEntrys, Int, Int2);
                // Преобразование в JSON-строку
                string json = JsonConvert.SerializeObject(seting, Formatting.Indented);
                // Создание и запись в JSON-файл
                string fileName = "Client.json";
                string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), fileName);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                File.WriteAllText(path, json);
                // Чтение JSON-файла
                string jsonFromFile = File.ReadAllText(path);
                // Преобразование JSON-строки в объект Seting
                Seting setingFromFile = JsonConvert.DeserializeObject<Seting>(jsonFromFile);
                // Вывод данных из объекта setingFromFile
                Console.WriteLine($"Ip_adress: {setingFromFile.Ip_adress}");
                Console.WriteLine($"Port: {setingFromFile.Port}");
                Console.WriteLine($"TypeSQL: {setingFromFile.TypeSQL}");
                Console.WriteLine($"ColorStyles: {setingFromFile.ColorStyles}");
            }
            else if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                string appDirectory = System.IO.Path.GetTempPath();
                FileInfo fileInfo = new FileInfo(appDirectory + "\\Client.json");
                // Если есть то загружаем настройки сервера если нет то создают
                if (fileInfo.Exists)
                {
                    File.Delete(appDirectory + "\\Client.json");

                    using (FileStream fs = new FileStream(appDirectory + "\\Client.json", FileMode.OpenOrCreate))
                    {
                        Seting seting = new Seting(AddressEntrys, PortEntrys, settingLanguage, Int2);
                        System.Text.Json.JsonSerializer.Serialize<Seting>(fs, seting);
                    }
                }
                else
                {
                    using (FileStream fileStream = new FileStream(appDirectory + "\\Client.json", FileMode.OpenOrCreate))
                    {
                        Seting connect_Server_ = new Seting(AddressEntrys, PortEntrys, settingLanguage, Int2);
                        System.Text.Json.JsonSerializer.Serialize<Seting>(fileStream, connect_Server_);
                    }

                    using (FileStream fileStream = new FileStream(appDirectory + "\\Client.json", FileMode.OpenOrCreate))
                    {
                        Seting aFile = System.Text.Json.JsonSerializer.Deserialize<Seting>(fileStream);
                    }

                }
            }
        }


        public void UpdateColors(string AddressEntrys, int PortEntrys, int settingColor)
        {
            CheckOS();
            string fileName = "Client.json";

            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                Console.WriteLine("Операционная система: Android");
                Seting setting = new Seting(AddressEntrys, PortEntrys, language, settingColor);
                // Преобразование в JSON-строку
                string json = JsonConvert.SerializeObject(setting, Formatting.Indented);
                // Создание и запись в JSON-файл
                string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), fileName);
                File.WriteAllText(path, json);
                // Чтение JSON-файла для проверки
                string jsonFromFile = File.ReadAllText(path);
                // Преобразование JSON-строки в объект Seting
                Seting settingFromFile = JsonConvert.DeserializeObject<Seting>(jsonFromFile);
                // Вывод данных из объекта settingFromFile для проверки
                Console.WriteLine($"ColorStyles: {settingFromFile.ColorStyles}");
            }
            else if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                string appDirectory = System.IO.Path.GetTempPath();
                string filePath = Path.Combine(appDirectory, fileName);

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                Seting setting = new Seting(AddressEntrys, PortEntrys, language, settingColor);
                // Преобразование в JSON-строку
                string json = JsonConvert.SerializeObject(setting, Formatting.Indented);
                // Запись в файл
                File.WriteAllText(filePath, json);
                // Чтение JSON-файла для проверки
                string jsonFromFile = File.ReadAllText(filePath);
                // Преобразование JSON-строки в объект Seting
                Seting settingFromFile = JsonConvert.DeserializeObject<Seting>(jsonFromFile);
                // Вывод данных из объекта settingFromFile для проверки
                Console.WriteLine($"ColorStyles: {settingFromFile.ColorStyles}");
            }
        }



        //public void UpdateColors(string AddressEntrys, int PortEntrys, int settingColor)
        //{
        //    CheckOS();
        //    if (DeviceInfo.Platform == DevicePlatform.Android)
        //    {
        //        Console.WriteLine("Операционная система: Android");
        //        Seting seting = new Seting(AddressEntrys, PortEntrys, language, settingColor);
        //        // Преобразование в JSON-строку
        //        string json = JsonConvert.SerializeObject(seting, Formatting.Indented);
        //        // Создание и запись в JSON-файл
        //        string fileName = "Client.json";
        //        string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), fileName);
        //        if (File.Exists(path))
        //        {
        //            File.Delete(path);
        //        }
        //        File.WriteAllText(path, json);
        //        // Чтение JSON-файла
        //        string jsonFromFile = File.ReadAllText(path);
        //        // Преобразование JSON-строки в объект Seting
        //        Seting setingFromFile = JsonConvert.DeserializeObject<Seting>(jsonFromFile);
        //        // Вывод данных из объекта setingFromFile
        //        Console.WriteLine($"Ip_adress: {setingFromFile.Ip_adress}");
        //        Console.WriteLine($"Port: {setingFromFile.Port}");
        //        Console.WriteLine($"TypeSQL: {setingFromFile.TypeSQL}");
        //        Console.WriteLine($"ColorStyles: {setingFromFile.ColorStyles}");
        //    }
        //    else if (DeviceInfo.Platform == DevicePlatform.WinUI)
        //    {
        //        string appDirectory = System.AppContext.BaseDirectory;
        //        FileInfo fileInfo = new FileInfo(appDirectory + "\\Client.json");
        //        // Если есть то загружаем настройки сервера если нет то создают
        //        if (fileInfo.Exists)
        //        {
        //            File.Delete(appDirectory + "\\Client.json");

        //            using (FileStream fs = new FileStream(appDirectory + "\\Client.json", FileMode.OpenOrCreate))
        //            {
        //                Seting seting = new Seting(AddressEntrys, PortEntrys, language, settingColor);
        //                System.Text.Json.JsonSerializer.Serialize<Seting>(fs, seting);
        //            }
        //        }
        //        else
        //        {
        //            using (FileStream fileStream = new FileStream(appDirectory + "\\Client.json", FileMode.OpenOrCreate))
        //            {
        //                Seting connect_Server_ = new Seting(AddressEntrys, PortEntrys, language, settingColor);
        //                System.Text.Json.JsonSerializer.Serialize<Seting>(fileStream, connect_Server_);
        //            }

        //            using (FileStream fileStream = new FileStream(appDirectory + "\\Client.json", FileMode.OpenOrCreate))
        //            {
        //                Seting aFile = System.Text.Json.JsonSerializer.Deserialize<Seting>(fileStream);
        //            }

        //        }
        //    }
        //}
    }
}

    //string fileName = "example.txt";
    //string fileContent = "Hello, Maui!";

    //string fileName = "Client.json";
    //string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), fileName);

    //File.WriteAllText(path, fileName);
    ////string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), fileName);

    //using (StreamWriter writer = new StreamWriter(path))
    //{
    //    writer.
    //}

    // Файл успешно создан



        
        ///// <summary>
        ///// Загружаеться Ip_address и port
        ///// </summary>
        //static public void SaveOpen()
        //{
        //    try
        //    {
        //       ;



        //        //switch (DeviceInfo.Platform)
        //        //{
        //        //    case DevicePlatform.iOS:
        //        //        Console.WriteLine("Проект запущен на Android");
        //        //        break;

        //        //    //case :
        //        //    //    Console.WriteLine("Проект запущен на iOS");
        //        //    //    break;

        //        //    case "WinUI":
        //        //        Console.WriteLine("Проект запущен на UWP (Windows)");
        //        //        break;

        //        //    //case :
        //        //    //    Console.WriteLine("Проект запущен на macOS");
        //        //    //    break;

        //        //    default:
        //        //        Console.WriteLine("Проект запущен на неизвестной операционной системе");
        //        //        break;
        //        //}


              
            
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}


        /// <summary>
        /// Меняет   Ip_address  из пграфического измениния
        /// </summary>
        //static public void SaveOpen(string adress)
        //{
        //    try
        //    {

        //        string path = Environment.CurrentDirectory.ToString();
        //        FileInfo fileInfo = new FileInfo(path + "\\Client.json");

        //        //Если есть то загружаем настройки сервера если нет то создают
        //        if (fileInfo.Exists)
        //        {
        //            using (FileStream fs = new FileStream("Client.json", FileMode.Open))
        //            {
        //                Seting _aFile = JsonSerializer.Deserialize<Seting>(fs);
        //                Ip_adresss = _aFile.Ip_adress;
        //            }
        //        }
        //        else
        //        {
        //            using (FileStream fileStream = new FileStream("Client.json", FileMode.OpenOrCreate))
        //            {
        //                Seting connect_Server_ = new Seting(IPAddress.Loopback.ToString(), 9595, 1);
        //                JsonSerializer.Serialize<Seting>(fileStream, connect_Server_);
        //            }


        //            using (FileStream fileStream = new FileStream("Client.json", FileMode.OpenOrCreate))
        //            {
        //                Seting aFile = JsonSerializer.Deserialize<Seting>(fileStream);
        //                Ip_adresss = aFile.Ip_adress;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}



//    }
//}
