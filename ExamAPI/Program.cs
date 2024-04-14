using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ExamAPI.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
// ���������, ��� ���������� �������� �� Linux
 if (Environment.OSVersion.Platform == PlatformID.Unix)
{
    // ������� ��� ������� ������ PostgreSQL � Linux � �������������� systemctl
    string command = "service postgresql start";

    // ������� ������� ��� ���������� ������� ������� ������ PostgreSQL
    Process process = new Process();
    process.StartInfo.FileName = "/bin/bash";
    process.StartInfo.Arguments = $"-c \"{command}\"";
    process.StartInfo.UseShellExecute = false;
    process.StartInfo.RedirectStandardOutput = true;
    process.StartInfo.RedirectStandardError = true;
    // ��������� �������
    process.Start();
    // ����, ���� ������� ���������� 
    process.WaitForExit();
}

 builder.Services.AddDbContext<ExamAPIContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ExamAPIContext") ?? throw new InvalidOperationException("Connection string 'ExamAPIContext' not found.")));


//using (var serviceScope = builder.Services.BuildServiceProvider().CreateScope())
//{
//    var migrationDbContext = serviceScope.ServiceProvider.GetRequiredService<ExamAPIContext>();
//    var migrator = migrationDbContext.Database.GetService<IMigrator>();

//    // ��������� SQL-������� ��� ������ ��� ��������� �������� "InitialCreate"
//    var script = migrator.GenerateScript();

//    // ����� SQL-������� � ������� ��� ���������� ������ �������� � ���
//    Console.WriteLine(script);

//    // ���������� ���� ��������� ��������
//    migrationDbContext.Database.Migrate();
//}
builder.Services.AddControllers();

//using (var serviceScope = builder.Services.BuildServiceProvider().CreateScope())
// {
//    var migrationDbContext = serviceScope.ServiceProvider.GetRequiredService<ExamAPIContext>();
//    var migrator = migrationDbContext.Database.GetService<IMigrator>();

//    // ��������� ������ ��������� ��������
//    var pendingMigrations = migrationDbContext.Database.GetPendingMigrations();

//    if (pendingMigrations.Any())
//    {
//        // ���� ���� ��������� ��������, �������� ���������� � ��� ��� ��������� ��������������� ��������.
//        Console.WriteLine("���� ��������� ��������:");
//        foreach (var migration in pendingMigrations)
//        {
//            Console.WriteLine(migration);
//            migrationDbContext.Database.Migrate();

//        }
//    }
//    else
//    {
//        Console.WriteLine("�������� �� ���������.");
//    }
//}
////Catalog catalog = new Catalog();
//catalog.AddCatalog();

// ��������� �������� CORS ��� ����������
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

// �������� CORS � ��������� ��� ����������
app.UseCors("AllowAll");

app.MapControllers();

app.Run();
