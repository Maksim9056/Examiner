using ExamModels;
using ExamServerData;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ExamServer
{
    public class TcpServer
    {
        private TcpListener listener;
        private  int Port = 9596;
        private  string serverIpAddress = "192.168.1.204";

        public async Task StartServer(string IP_adres,int port)
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
                using MemoryStream memoryStream = new MemoryStream();
                byte[] buffer = new byte[8192];
                int bytesRead;

                while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    await memoryStream.WriteAsync(buffer, 0, bytesRead);
                    if (stream.DataAvailable == false)
                    {
                        break;
                    }
                }

                byte[] receivedData = memoryStream.ToArray();
                // Обработка полученных данных (receivedData) на сервере

                Filles vFiles = new Filles(0, receivedData);
                GlobalClass @class = new GlobalClass();
                Filles filles = @class.SaveUsersImage(vFiles);

                Console.WriteLine($"Отправлен файл от сервера: Id - {filles.Id}");

                int responseInt = filles.Id;
                byte[] intBytes = BitConverter.GetBytes(responseInt); 
                    

                // Проверяем, что соединение все еще открыто перед отправкой
                if (client.Connected && stream.CanWrite)
                {
                    await SendIdToClient(stream, intBytes);
                    Console.WriteLine($"Отправлен ID файла обратно клиенту: {responseInt}");
                }
                else
                {
                    Console.WriteLine("Ошибка отправки ID: Соединение закрыто или не может быть записано");
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

        private async Task SendIdToClient(NetworkStream stream, byte[] intBytes)
        {
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
            try
            {
                byte[] errorBytes = System.Text.Encoding.UTF8.GetBytes(errorMessage);
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
