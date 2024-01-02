using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ExamModels
{
    public class SendFiles
    {
        private const int Port = 9596; // Порт, к которому будет подключаться клиент
        private readonly string serverIpAddress = "192.168.1.204"; // IP-адрес сервера

        public async Task SendFile(string filePath)
        {
            try
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (TcpClient client = new TcpClient())
                    {
                        await client.ConnectAsync(serverIpAddress, Port);

                        using (NetworkStream stream = client.GetStream())
                        {

                            byte[] buffer = new byte[1024];
                            int bytesRead;

                            while ((bytesRead = await fileStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                            {
                                await stream.WriteAsync(buffer, 0, bytesRead);
                            }

                            Console.WriteLine("Файл успешно отправлен.");

                            //Получение JSON-ответа от сервера
                            //using (MemoryStream memoryStream = new MemoryStream())
                            //{
                            //    while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                            //    {
                            //        await memoryStream.WriteAsync(buffer, 0, bytesRead);
                            //    }

                            //    byte[] jsonBytes = memoryStream.ToArray();
                            //    string json = Encoding.UTF8.GetString(jsonBytes);

                            //    // Десериализация JSON в объект Filles
                            //    Filles receivedFile = JsonSerializer.Deserialize<Filles>(json);

                            //    // Можно использовать объект receivedFile по вашему усмотрению
                            //    Console.WriteLine($"Получен файл от сервера: Id - {receivedFile.Id}, Name - {receivedFile.Name.Length} bytes");
                            //}
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка отправки файла: {ex.Message}");
            }
        }
    }
}

