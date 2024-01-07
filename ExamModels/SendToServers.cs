using System;
using System.IO;
using System.IO.Pipes;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ExamModels
{
    public class SendToServers
    {

        /// <summary>
        /// Куда, что там надо выполнить, что отправляем для выполнения
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="command"></param>
        /// <param name="vClassJSON"></param>
        /// <returns></returns>
        public async Task<object> SendToServer(string vServerIp, int command, string vClassJSON)
        {
            try
            {
                using (TcpClient client = new TcpClient(vServerIp, Ports.FilePort))
                using (NetworkStream stream = client.GetStream())
                {
                    object vReceivedObj = null;
                    await SendCommand(stream, command); // Отправка команды
                    switch (command)
                    {
                        case Commands.UploadFile:
                            using (FileStream fileStream = new FileStream(vClassJSON, FileMode.Open, FileAccess.Read))
                            {
                                long fileSize = fileStream.Length;
                                await SendSize(fileSize, stream); // Отправка размера файла
                                await SendFileContents(fileStream, stream); // Отправка содержимого файла
                                int receivedId = await ReceiveFileId(stream); // Получение ID файла
                                vReceivedObj = receivedId;
                            }
                            break;
                        case Commands.DownloadFile:
                            await SendSize(vClassJSON.Length, stream); // Отправка размера класса
                            byte[] vClassJSONByte = Encoding.UTF8.GetBytes(vClassJSON);
                            MemoryStream memoryStream = new MemoryStream(vClassJSONByte);
                            await SendContents(memoryStream, stream); // Отправка содержимого файла
                            object receivedObj = await ReceiveFile(stream); // Получение ID файла
                            vReceivedObj = receivedObj;
                            break;
                    }
                    return vReceivedObj;
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

        private async Task SendCommand(NetworkStream stream, int command)
        {
            try
            {
                byte[] commandBytes = BitConverter.GetBytes(command);

                Console.WriteLine("Отправка команды...");
                await stream.WriteAsync(commandBytes, 0, commandBytes.Length);
                Console.WriteLine("Команда успешно отправлена.");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Ошибка ввода-вывода при отправке команды: {ex.Message}");
            }
            catch (SocketException ex)
            {
                Console.WriteLine($"Ошибка сокета: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Необработанное исключение при отправке команды: {ex.Message}");
            }
        }

        private async Task SendSize(long StreamSize, NetworkStream stream)
        {
            try
            {
                byte[] fileSizeBytes = BitConverter.GetBytes(StreamSize);

                Console.WriteLine("Отправка размера сообщения ...");
                await stream.WriteAsync(fileSizeBytes, 0, fileSizeBytes.Length);
                Console.WriteLine("Размер сообщения успешно отправлен.");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Ошибка ввода-вывода при отправке размера сообщения: {ex.Message}");
            }
            catch (SocketException ex)
            {
                Console.WriteLine($"Ошибка сокета: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Необработанное исключение при отправке размера сообщения: {ex.Message}");
            }
        }
        private async Task SendContents(MemoryStream memoryStream, NetworkStream stream)
        {
            try
            {
                byte[] buffer = new byte[8192];
                int bytesRead;

                Console.WriteLine("Отправка сообщения...");
                while ((bytesRead = await memoryStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    await stream.WriteAsync(buffer, 0, bytesRead);
                }
                Console.WriteLine("сообщения успешно отправлен.");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Ошибка ввода-вывода при передаче сообщения: {ex.Message}");
            }
            catch (SocketException ex)
            {
                Console.WriteLine($"Ошибка сокета: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Необработанное исключение при отправке сообщения: {ex.Message}");
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

        private async Task<Filles> ReceiveFile(NetworkStream stream)
        {
            Console.WriteLine("Ожидание файл от сервера...");

            // Получение размера файла
            byte[] sizeBytes = new byte[sizeof(long)]; // размер int в байтах
            await stream.ReadAsync(sizeBytes, 0, sizeBytes.Length);
            long fileSize = BitConverter.ToInt64(sizeBytes, 0);
            MemoryStream memoryStream = new MemoryStream();

            byte[] buffer = new byte[8192];
            int bytesRead;
            long bytesReceived = 0;

            while (bytesReceived < fileSize && (bytesRead = await stream.ReadAsync(buffer, 0, (int)Math.Min(buffer.Length, fileSize - bytesReceived))) > 0)
            {
                await memoryStream.WriteAsync(buffer, 0, bytesRead);
                bytesReceived += bytesRead;
            }

            Filles receivedId = JsonSerializer.Deserialize<Filles>(memoryStream.ToArray());

            //Filles receivedId = Filles.ConvertFromBytes(memoryStream.ToArray());
            Console.WriteLine($"Получен файл от сервера: {receivedId}");
            return receivedId;
        }

    }
}
