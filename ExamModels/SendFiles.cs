using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ExamModels
{
    public class SendFiles
    {
        private const int Port = 9596;
        private  string serverIpAddress = "192.168.1.204";

        public async Task<int> SendFile(string filePath,string ip)
        {
            try
            {
                serverIpAddress = ip;
                using (TcpClient client = new TcpClient(serverIpAddress, Port))
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                using (NetworkStream stream = client.GetStream())
                {
                    await SendFileContents(fileStream, stream);
                    int receivedId = await ReceiveFileId(stream);
                    return receivedId;
                    //return -4;
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

        private async Task SendFileContents(FileStream fileStream, NetworkStream stream)
        {
            try
            {
                byte[] buffer = new byte[8192];
                int bytesRead;

                Console.WriteLine("Отправка файла...");
                while ((bytesRead = await fileStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    await stream.WriteAsync(buffer, 0, bytesRead);
                }
                Console.WriteLine("Файл успешно отправлен.");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Ошибка ввода-вывода при передаче файла: {ex.Message}");
            }
            catch (SocketException ex)
            {
                Console.WriteLine($"Ошибка сокета: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Необработанное исключение при отправке файла: {ex.Message}");
            }

        }

        private async Task<int> ReceiveFileId(NetworkStream stream)
        {
            Console.WriteLine("Ожидание ID файла от сервера...");

            // Чтение ID файла от сервера
            byte[] receivedIdBytes = new byte[sizeof(int)]; // размер int в байтах

            await stream.ReadAsync(receivedIdBytes, 0, receivedIdBytes.Length);

            int receivedId = BitConverter.ToInt32(receivedIdBytes, 0);
            Console.WriteLine($"Получен ID файла от сервера: {receivedId}");
            return receivedId;
        }
    }
}
