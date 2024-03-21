using KriaHubTest.Data;
using KriaHubTest.Repositories;
using KriaHubTest.Repositories.Interfaces;
using KriaHubTest.Services;
using KriaHubTest.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace KriaHubTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddEntityFrameworkSqlServer()
                .AddDbContext<BancoContext>(
                    o => o.UseSqlServer(builder.Configuration.GetConnectionString("DataBase")));

            builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            builder.Services.AddScoped<IRepositorioRepository, RepositorioRepository>();
            builder.Services.AddScoped<IUsuarioRepositorioRepository, UsuarioRepositorioRepository>();

            builder.Services.AddScoped<IContaService, ContaService>();
            builder.Services.AddScoped<IRepositorioService, RepositorioService>();
            builder.Services.AddScoped<IUsuarioRepositorioService, UsuarioRepositorioService>();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Conta/Login"; // Rota para a página de login
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Conta}/{action=Login}/{id?}");

            app.Run();
        }
    }
}
