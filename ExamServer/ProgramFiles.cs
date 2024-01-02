using ExamModels;
using ExamServerData;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Claims;
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
            listener = new TcpListener(IPAddress.Parse(serverIpAddress), Port);
            listener.Start();

            while (true)
            {
                TcpClient client = await listener.AcceptTcpClientAsync();
                _ = Task.Run(() => HandleClientAsync(client));
            }
        }

        private async Task HandleClientAsync(TcpClient client)
        {
            using NetworkStream stream = client.GetStream();

            // Используем MemoryStream для записи полученных данных в память
            using MemoryStream memoryStream = new MemoryStream();
            byte[] buffer = new byte[1024];
            int bytesRead;

            while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                await memoryStream.WriteAsync(buffer, 0, bytesRead);
            }

            // Получаем массив байтов из MemoryStream
            byte[] receivedData = memoryStream.ToArray();

            Filles vFiles = new Filles(0, receivedData);
            GlobalClass @class = new GlobalClass();
            Filles filles = @class.SaveUsersImage(vFiles); ;
            using (MemoryStream ms = new MemoryStream())
            {
                JsonSerializer.Serialize<Filles>(ms, filles);

                //await stream.WriteAsync(ms.ToArray(), 0, ms.ToArray().Length);
                //byte[] jsonResponseBytes = Encoding.UTF8.GetBytes(filles.ToString());
                byte[] jsonResponseBytes = ms.ToArray();
                await stream.WriteAsync(jsonResponseBytes, 0, jsonResponseBytes.Length);
            }

            // Можно использовать receivedData по вашему усмотрению
            Console.WriteLine($"Получены данные размером: {receivedData.Length} байт");

            client.Close();
        }
    }
}
