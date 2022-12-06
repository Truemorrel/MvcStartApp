using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MvcStartApp.Middlewares;
using MvcStartApp.Models.DB;
using MvcStartApp.Models.LogRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcStartApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("DefaultConnection");
            string logConnection = Configuration.GetConnectionString("LogConnection");

            services.AddDbContext<BlogContext>(options => options.UseSqlServer(connection), 
                contextLifetime: ServiceLifetime.Singleton, 
                optionsLifetime: ServiceLifetime.Singleton);
            services.AddDbContext<LogContext>(options => options.UseSqlServer(logConnection),
                contextLifetime: ServiceLifetime.Singleton,
                optionsLifetime: ServiceLifetime.Singleton);

            // регистрация сервиса репозитория для взаимодействия с базой данных
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<ILogRepository, LogRepository>();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogRepository logRepository)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
                        
            // Подключаем логирвоание с использованием ПО промежуточного слоя
            app.UseMiddleware<LoggingMiddleware>(logRepository);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Logs}/{action=Index}/{id?}");
            });
        }
    }
}
