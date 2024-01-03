using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ExamModels
{
    public class SendFiles
    {
        private const int Port = 9596;
        private readonly string serverIpAddress = "192.168.1.204";

        public async Task<int> SendFile(string filePath)
        {
            try
            {
                using (TcpClient client = new TcpClient(serverIpAddress, Port))
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                using (NetworkStream stream = client.GetStream())
                {
                    byte[] buffer = new byte[8192];
                    int bytesRead;

                    Console.WriteLine("Отправка файла...");
                    while ((bytesRead = await fileStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                    {
                        await stream.WriteAsync(buffer, 0, bytesRead);
                    }
                    Console.WriteLine("Файл успешно отправлен.");

                    Console.WriteLine("Ожидание ID файла от сервера...");
                    // Чтение ID файла от сервера
                    int totalBytesReceived = 0;
                    int bytesReadId;
                    int receivedId = -1;
                    byte[] receivedIdBytes = new byte[sizeof(int)];

                    while (totalBytesReceived < receivedIdBytes.Length &&
                           (bytesReadId = await stream.ReadAsync(receivedIdBytes, totalBytesReceived, receivedIdBytes.Length - totalBytesReceived)) > 0)
                    {
                        totalBytesReceived += bytesReadId;
                    }

                    if (totalBytesReceived == receivedIdBytes.Length)
                    {
                        receivedId = BitConverter.ToInt32(receivedIdBytes, 0);
                        Console.WriteLine($"Получен ID файла от сервера: {receivedId}");
                    }
                    else
                    {
                        Console.WriteLine("Ошибка чтения ID файла от сервера: Недостаточно байт");
                    }

                    return receivedId;
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Ошибка ввода-вывода при передаче файла: {ex.Message}");
                return -1;
            }
            catch (SocketException ex)
            {
                Console.WriteLine($"Ошибка сокета: {ex.Message}");
                return -2;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Необработанное исключение при отправке файла: {ex.Message}");
                return -3;
            }
        }
    }
}
