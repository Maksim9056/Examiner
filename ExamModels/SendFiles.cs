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
        
        // Клиентская функция

        public async Task SendFile(string filePath)
        {
            try
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (TcpClient client = new TcpClient(serverIpAddress, Port))
                    {
                        //await client.ConnectAsync(serverIpAddress, Port);

                        using (NetworkStream stream = client.GetStream())
                        {

                            byte[] buffer = new byte[1024];
                            int bytesRead;
                                byte[] intBytes = new byte[4];

                            while ((bytesRead = await fileStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                            {
                                await stream.WriteAsync(buffer, 0, bytesRead);
                                
                            }
                                

                            Console.WriteLine("Файл успешно отправлен.");
                            
                            //Не работает
                        //Task.Run( async () =>  await stream.ReadAsync(intBytes, 0, intBytes.Length )).Wait();




                            //// Преобразовать массив байтов в int
                            int responseInt = BitConverter.ToInt32(intBytes, 0);

                            Console.WriteLine($"Получен ответ от сервера: {responseInt}");
                            //Console.WriteLine($"Получен ответ от сервера: {receivedInt}");

                            //int receivedInt = BitConverter.ToInt32(intBuffer, 0);
                            //Console.WriteLine($"Получен ответ от сервера: {receivedInt}");
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

