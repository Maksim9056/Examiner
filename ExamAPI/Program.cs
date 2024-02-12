using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ExamAPI.Data;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ExamAPIContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ExamAPIContext") ?? throw new InvalidOperationException("Connection string 'ExamAPIContext' not found.")));

builder.Services.AddControllers();

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
