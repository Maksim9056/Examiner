using ExamModels;
using ExamServerData;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ExamServer
{
    public class TcpServer
    {
        private TcpListener listener;
        private const int Port = 9596; // Порт, который будет прослушиваться
        private readonly string serverIpAddress = "192.168.1.204"; // IP-адрес сервера


        public async Task StartServer()
        {
            try
            {
               listener = new TcpListener(IPAddress.Parse(serverIpAddress), Port);
              listener.Start();

                while (true)
                {

                    TcpClient client = await listener.AcceptTcpClientAsync();
                    _ = Task.Run(() => HandleClientAsync(client));

                    //Task.Run(() => HandleClientAsync(client)).Wait();
                }
            } 
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }


        private async Task HandleClientAsync(TcpClient client)
        {
            try
            {
                using NetworkStream stream = client.GetStream();

                // Используем MemoryStream для записи полученных данных в память
                using MemoryStream memoryStream = new MemoryStream();
                byte[] buffer = new byte[8192];
                int bytesRead;

                while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    await memoryStream.WriteAsync(buffer, 0, bytesRead);
                }

                // Получаем массив байтов из MemoryStream
                byte[] receivedData = memoryStream.ToArray();

                Filles vFiles = new Filles(0, receivedData);
                GlobalClass @class = new GlobalClass();
                Filles filles = @class.SaveUsersImage(vFiles);

                Console.WriteLine($"Отправлен файл от сервера: Id - {filles.Id}");

                // Предположим, что responseInt - это ваше число типа int
                int responseInt = filles.Id; // Например, какое-то число

                // Преобразуем int в массив байтов
                byte[] intBytes = BitConverter.GetBytes(responseInt);

                // Отправляем массив байтов обратно клиенту
                await stream.WriteAsync(intBytes, 0, intBytes.Length);



                // Можно использовать receivedData по вашему усмотрению
                Console.WriteLine($"Получены данные размером: {receivedData.Length} байт");

                //client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
}
