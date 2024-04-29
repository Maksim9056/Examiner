using ExamModels;
using ExamWeb.Client.Pages;
using ExamWeb.Components;
using ExamWeb.Components.Pages;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;
using System.Text;

namespace ExamWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddRazorPages().AddSessionStateTempDataProvider();

            builder.Services.AddControllersWithViews()
                                .AddSessionStateTempDataProvider();
            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents()
                .AddInteractiveWebAssemblyComponents();
            builder.Services.AddHttpClient();
            builder.Services.AddCors();
            builder.Services.AddScoped<ExamWeb.Components.Pages.Home>(); 
            builder.Services.AddScoped<ExamWeb.Components.Pages.Weather>();
            builder.Services.AddScoped<ExamWeb.Components.Pages.Authorization>();

            //Для будущих  страниц
            //builder.Services.AddScoped<.GroupChats>();
            //builder.Services.AddScoped<ListChatsAll>();
            //builder.Services.AddScoped<Friends>();
            builder.Services.AddScoped<Component>();

            builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    // Конфигурация параметров валидации токена JWT
                    ValidateIssuer = true,
                    ValidIssuer = AuthOptions.ISSUER,
                    ValidateAudience = true,
                    ValidAudience = AuthOptions.AUDIENCE,
                    ValidateLifetime = true,
                    IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true,
                };
            });
            //    builder.Services.AddDistributedMemoryCache();
            //    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //.AddCookie(options =>
            //{
            //    options.Cookie.HttpOnly = true;
            //    options.Cookie.IsEssential = true; // Если вы хотите сохранить сеанс даже при отключенных куки
            //});
            //    builder.Services.AddSession(options =>
            //    {
            //        options.IdleTimeout = TimeSpan.MaxValue;  //you can change the session expired time.  
            //        options.Cookie.HttpOnly = true;
            //        options.Cookie.IsEssential = true;
            //    });
            //    builder.Services.AddSession();

            var app = builder.Build();
            // Передаем значение для параметра Название
          
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

     
            app.UseHttpsRedirection();
            app.UseAuthorization();
            // добавление middleware аутентификации​
            app.UseStaticFiles();
            app.UseAntiforgery();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode()
                .AddInteractiveWebAssemblyRenderMode()
                .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);
            app.UseSession();
            app.Run();
        }
    }
}

