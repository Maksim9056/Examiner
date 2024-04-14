using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ExamAPI.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
// Проверяем, что приложение запущено на Linux
 if (Environment.OSVersion.Platform == PlatformID.Unix)
{
    // Команда для запуска службы PostgreSQL в Linux с использованием systemctl
    string command = "service postgresql start";

    // Создаем процесс для выполнения команды запуска службы PostgreSQL
    Process process = new Process();
    process.StartInfo.FileName = "/bin/bash";
    process.StartInfo.Arguments = $"-c \"{command}\"";
    process.StartInfo.UseShellExecute = false;
    process.StartInfo.RedirectStandardOutput = true;
    process.StartInfo.RedirectStandardError = true;
    // Запускаем процесс
    process.Start();
    // Ждем, пока процесс завершится 
    process.WaitForExit();
}

 builder.Services.AddDbContext<ExamAPIContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ExamAPIContext") ?? throw new InvalidOperationException("Connection string 'ExamAPIContext' not found.")));


//using (var serviceScope = builder.Services.BuildServiceProvider().CreateScope())
//{
//    var migrationDbContext = serviceScope.ServiceProvider.GetRequiredService<ExamAPIContext>();
//    var migrator = migrationDbContext.Database.GetService<IMigrator>();

//    // Генерация SQL-скрипта для только что созданной миграции "InitialCreate"
//    var script = migrator.GenerateScript();

//    // Вывод SQL-скрипта в консоль или выполнение других действий с ним
//    Console.WriteLine(script);

//    // Применение всех ожидающих миграций
//    migrationDbContext.Database.Migrate();
//}
builder.Services.AddControllers();

//using (var serviceScope = builder.Services.BuildServiceProvider().CreateScope())
// {
//    var migrationDbContext = serviceScope.ServiceProvider.GetRequiredService<ExamAPIContext>();
//    var migrator = migrationDbContext.Database.GetService<IMigrator>();

//    // Получение списка ожидающих миграций
//    var pendingMigrations = migrationDbContext.Database.GetPendingMigrations();

//    if (pendingMigrations.Any())
//    {
//        // Если есть ожидающие миграции, выведите информацию о них или выполните соответствующие действия.
//        Console.WriteLine("Есть ожидающие миграции:");
//        foreach (var migration in pendingMigrations)
//        {
//            Console.WriteLine(migration);
//            migrationDbContext.Database.Migrate();

//        }
//    }
//    else
//    {
//        Console.WriteLine("Миграция не требуется.");
//    }
//}
////Catalog catalog = new Catalog();
//catalog.AddCatalog();

// Добавляем политику CORS без блокировок
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Включаем CORS с политикой без блокировок
app.UseCors("AllowAll");

app.MapControllers();

app.Run();
