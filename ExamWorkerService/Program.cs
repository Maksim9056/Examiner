using ExamWorkerService;   // пространство имен класса Worker
using System.Threading;


IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService()
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<Worker>();
    })
    .Build();


host.Run();

//public class FileWatcherService : BackgroundService
//{
//    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//    {

//        ExamServer.Program program = new ExamServer.Program();

//        Thread t1 = new Thread(new ThreadStart(program.Main));
//        t1.IsBackground = true;
//        t1.Start();

//        // если операция не отменена, то выполняем задержку в 200 миллисекунд
//        while (!stoppingToken.IsCancellationRequested)
//        {
//            await Task.Delay(200, stoppingToken);
//        }
//        await Task.CompletedTask;
//    }
//}

//public static IHostBuilder CreateHostBuilder(string[] args)
//  Host.CreateDefaultBuilder(args).ConfigureLogging((hostingContext, config)
//  { config.AddLog4Net(); }).UseWindowsService().ConfigureServices((hostContext, services)
//  { services.AddHostedService<Worker>(); });


//namespace ExamWorkerService
//{
//    public class Program
//    {
//        public static async Task Main(string[] args)
//        {
//            var builder = Host.CreateDefaultBuilder(args)
//                .UseWindowsService()
//                .ConfigureServices((hostContext, services) =>
//                {
//                    services.AddHostedService<Worker>();
//                });

//            await builder.RunConsoleAsync();

//            using (var host = builder.Build())
//            {
//                await host.RunAsync();
//            }
//        }
//    }
//}