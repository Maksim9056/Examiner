using ExamWorkerService;   // ������������ ���� ������ Worker
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

        //// ���������� ���������
        //watcher.Changed += async (o, e) => await File.AppendAllTextAsync(filePath, $"{DateTime.Now} Changed: {e.FullPath}\n");
        //// ���������� ������ � �������� ������ � �����
        //watcher.Created += async (o, e) => await File.AppendAllTextAsync(filePath, $"{DateTime.Now} Created: {e.FullPath}\n");
        //// ���������� ������ �� �������� ������ � �����
        //watcher.Deleted += async (o, e) => await File.AppendAllTextAsync(filePath, $"{DateTime.Now} Deleted: {e.FullPath}\n");
        //// ���������� ������ � ��������������
        //watcher.Renamed += async (o, e) => await File.AppendAllTextAsync(filePath, $"{DateTime.Now} Renamed: {e.OldFullPath} to {e.FullPath}\n");
        //// ���������� ������ �� �������
        //watcher.Error += async (o, e) => await File.AppendAllTextAsync(filePath, $"{DateTime.Now} Error: {e.GetException().Message}\n");

        //watcher.IncludeSubdirectories = true; // ����������� ��������� � ������������
        //watcher.EnableRaisingEvents = true;    // �������� �������


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