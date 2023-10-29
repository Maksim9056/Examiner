using ExamWorkerService;   // пространство имен класса Worker
using System.Threading;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
{
services.AddHostedService<Worker>();
})
    .Build();

host.Run();

public class FileWatcherService : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

        ExamServer.Program program = new ExamServer.Program();

        Thread t1 = new Thread(new ThreadStart(program.Main));
        t1.IsBackground = true;
        t1.Start();

        // если операция не отменена, то выполняем задержку в 200 миллисекунд
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(200, stoppingToken);
        }
        await Task.CompletedTask;
    }
}