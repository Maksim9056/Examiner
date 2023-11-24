
using Microsoft.Extensions.Hosting.Systemd;
namespace ExamWorkerService
{
    /// <summary>
    /// Microsoft.Extensions.Hosting.Systemd 7.0
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            var isWindowsService = !(args.Length > 0 && args[0] == "--console");
            var hostBuilder = CreateHostBuilder(args);

            if (isWindowsService)
            {
                hostBuilder.UseWindowsService();
            }
            else
            {
                hostBuilder.UseSystemd();
            }

            var host = hostBuilder.Build();

            if (isWindowsService)
                host.Run();
            else
                host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                //.ConfigureLogging(loggerFactory => loggerFactory.AddEventLog())
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                });
    }
}


//IHost host = Host.CreateDefaultBuilder(args)
//    .UseWindowsService()
//    .ConfigureLogging(loggerFactory => loggerFactory.AddEventLog())
//    .ConfigureServices((hostContext, services) =>
//    {
//        services.AddHostedService<Worker>();
//    })
//    .Build();


//host.Run();

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

//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.Logging;

//namespace YourNamespace
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            var isWindowsService = !(args.Length > 0 && args[0] == "--console");
//            var hostBuilder = CreateHostBuilder(args);

//            if (isWindowsService)
//                hostBuilder.UseWindowsService();

//            var host = hostBuilder.Build();

//            if (isWindowsService)
//                host.RunAsService();
//            else
//                host.Run();
//        }

//        public static IHostBuilder CreateHostBuilder(string[] args) =>
//            Host.CreateDefaultBuilder(args)
//                .ConfigureServices((hostContext, services) =>
//                {
//                    services.AddHostedService<Worker>();
//                });
//    }
//}