using ExamWorkerService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ExamWorkerService
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var cancellationTokenSource = new CancellationTokenSource();

            var serviceName = args.Length > 0 ? args[0] : "Examiner2"; // Задаем наименование службы

            var host = CreateHostBuilder(args).Build();

            await host.StartAsync(cancellationTokenSource.Token);

            Console.WriteLine($"Service '{serviceName}' started. Press any key to stop the service...");
            Console.ReadKey();

            cancellationTokenSource.Cancel();

            await host.StopAsync();

            cancellationTokenSource.Dispose();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                });
    }
}