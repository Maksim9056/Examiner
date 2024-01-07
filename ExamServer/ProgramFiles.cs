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
using static System.Net.WebRequestMethods;

namespace ExamServer
{
    public class TcpServer
    {
        private TcpListener listener;
        private int Port = 9596;
        private string serverIpAddress = "192.168.1.204";

        public async Task StartServer(string IP_adres, int port)
        {
            try
            {
                serverIpAddress = IP_adres;
                Port = port;
                listener = new TcpListener(IPAddress.Parse(serverIpAddress), Port);
                listener.Start();

                while (true)
                {
                    TcpClient client = await listener.AcceptTcpClientAsync();
                    _ = Task.Run(() => HandleClientAsync(client));
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

                // Получение команды
                byte[] commandBytes = new byte[sizeof(int)];
                await stream.ReadAsync(commandBytes, 0, commandBytes.Length);
                int command = BitConverter.ToInt32(commandBytes, 0);
                Console.WriteLine($"Получена команда от клиента: {command}");

                // Получение размера файла
                byte[] sizeBytes = new byte[sizeof(long)];
                await stream.ReadAsync(sizeBytes, 0, sizeBytes.Length);
                long fileSize = BitConverter.ToInt64(sizeBytes, 0);

                switch (command)
                {
                    case Commands.UploadFile:

                        byte[] receivedData = await ReceiveDataAsync(stream, fileSize);

                        // Обработка полученных данных (receivedData) на сервере
                        Filles filles = await ProcessReceivedDataAsync(receivedData);

                        Console.WriteLine($"Отправлен файл от сервера: Id - {filles.Id}");

                        // Отправка ID файла обратно клиенту
                        await SendIdToClient(stream, filles.Id);
                        break;
                    case Commands.DownloadFile:
                        byte[] receivedDataClass = await ReceiveDataAsync(stream, fileSize);

                        // Обработка полученных данных (receivedData) на сервере
                        Filles vFiless = await ProcessReceivedDataAsync2(receivedDataClass);

                        // Отправка файла обратно клиенту
                        await SendFilesToClient(stream, vFiless);
                        break;
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Ошибка ввода-вывода: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                // Отправка информации об ошибке клиенту
                await SendErrorToClient(client.GetStream(), ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Необработанное исключение: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
            }
        }
        private async Task<byte[]> ReceiveDataAsync(NetworkStream stream, long fileSize)
        {
            using MemoryStream memoryStream = new MemoryStream();

            byte[] buffer = new byte[8192];
            int bytesRead;
            long bytesReceived = 0;

            while (bytesReceived < fileSize && (bytesRead = await stream.ReadAsync(buffer, 0, (int)Math.Min(buffer.Length, fileSize - bytesReceived))) > 0)
            {
                await memoryStream.WriteAsync(buffer, 0, bytesRead);
                bytesReceived += bytesRead;
            }

            return memoryStream.ToArray();
        }
        private async Task<Filles> ProcessReceivedDataAsync2(byte[] receivedData)
        {
            Filles filles = null;

            try
            {
                // Обработка полученных данных (receivedData) на сервере
                string vClassJSON = Encoding.Default.GetString(receivedData);
                Filles vFiless = JsonSerializer.Deserialize<Filles>(vClassJSON);
                GlobalClass @class = new GlobalClass();
                filles = await Task.Run(() => @class.SelectFromFilles(vFiless));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка обработки данных: {ex.Message}");
                // Можно добавить обработку ошибки обработки данных здесь
            }

            return filles;
        }


        private async Task<Filles> ProcessReceivedDataAsync(byte[] receivedData)
        {
            Filles filles = null;

            try
            {
                // Обработка полученных данных (receivedData) на сервере
                Filles vFiles = new Filles(0, receivedData);
                GlobalClass @class = new GlobalClass();
                filles = await Task.Run(() => @class.SaveUsersImage(vFiles));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка обработки данных: {ex.Message}");
                // Можно добавить обработку ошибки обработки данных здесь
            }

            return filles;
        }

        private async Task SendFilesToClient(NetworkStream stream, Filles vfile)
        {
            //byte[] intBytes = vfile.ConvertToBytes();
            MemoryStream memoryStream = new MemoryStream(vfile.Name);
            //JsonSerializer.Serialize<Filles>(memoryStream, vfile);
            byte[] intBytes = memoryStream.ToArray();
            byte[] fileSizeBytes = BitConverter.GetBytes(intBytes.Length);

            try
            {
                await stream.WriteAsync(fileSizeBytes, 0, fileSizeBytes.Length);
                await stream.WriteAsync(intBytes, 0, intBytes.Length);
                Console.WriteLine($"Отправлен файл обратно клиенту: {vfile.Name.Length}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка отправки : {ex.Message}");
            }
        }

        private async Task SendIdToClient(NetworkStream stream, int fileId)
        {
            byte[] intBytes = BitConverter.GetBytes(fileId);

            try
            {
                await stream.WriteAsync(intBytes, 0, intBytes.Length);
                Console.WriteLine("Отправлен ID файла обратно клиенту");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка отправки ID: {ex.Message}");
            }
        }

        private async Task SendErrorToClient(NetworkStream stream, string errorMessage)
        {
            byte[] errorBytes = System.Text.Encoding.UTF8.GetBytes(errorMessage);

            try
            {
                await stream.WriteAsync(errorBytes, 0, errorBytes.Length);
                Console.WriteLine("Ошибка отправлена клиенту");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка отправки ошибки клиенту: {ex.Message}");
            }
        }
    }
}
