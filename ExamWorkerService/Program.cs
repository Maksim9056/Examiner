using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ExamWorkerService
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            using var cancellationTokenSource = new CancellationTokenSource();

            var serviceName = args.Length > 0 ? args[0] : "Examiner"; // Задаем наименование службы

            var host = CreateHostBuilder(args).Build();

            await host.StartAsync(cancellationTokenSource.Token);

            Console.WriteLine($"Service '{serviceName}' started. Press any key to stop the service...");
            Console.ReadKey();

            cancellationTokenSource.Cancel();

            await host.StopAsync(TimeSpan.FromSeconds(10)); // Установите значение таймаута в секундах

            cancellationTokenSource.Dispose();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                //.UseSystemd() // Если вы работаете в Linux
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.Configure<HostOptions>(options =>
                    {
                        options.ShutdownTimeout = TimeSpan.FromSeconds(10); // Установите значение таймаута в секундах
                    });
                });
    }
}