using ExamWorkerService;   // пространство имен класса Worker
using System.Threading;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
{
services.AddHostedService<FileWatcherService2>();
})
    .Build();

host.Run();

public class FileWatcherService2 : IHostedService
{
    FileSystemWatcher? watcher;
    //string filePath = "folderEvents.txt";
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        //watcher = new FileSystemWatcher("C:\\Temp");

        //// записываем изменения
        //watcher.Changed += async (o, e) => await File.AppendAllTextAsync(filePath, $"{DateTime.Now} Changed: {e.FullPath}\n");
        //// записываем данные о создании файлов и папок
        //watcher.Created += async (o, e) => await File.AppendAllTextAsync(filePath, $"{DateTime.Now} Created: {e.FullPath}\n");
        //// записываем данные об удалении файлов и папок
        //watcher.Deleted += async (o, e) => await File.AppendAllTextAsync(filePath, $"{DateTime.Now} Deleted: {e.FullPath}\n");
        //// записываем данные о переименовании
        //watcher.Renamed += async (o, e) => await File.AppendAllTextAsync(filePath, $"{DateTime.Now} Renamed: {e.OldFullPath} to {e.FullPath}\n");
        //// записываем данные об ошибках
        //watcher.Error += async (o, e) => await File.AppendAllTextAsync(filePath, $"{DateTime.Now} Error: {e.GetException().Message}\n");

        //watcher.IncludeSubdirectories = true; // отслеживаем изменения в подкаталогах
        //watcher.EnableRaisingEvents = true;    // включаем события


        //MyThread mt = new MyThread();
        ExamServer.Program program = new ExamServer.Program();

        Thread t1 = new Thread(new ThreadStart(program.Main));
        t1.IsBackground = true;
        t1.Start();

        //ExamServer.Program program = new ExamServer.Program();
        //program.Main();

        await Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        //watcher?.Dispose();
        await Task.CompletedTask;
    }
}