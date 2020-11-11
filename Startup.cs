using System;
using Kanban.Common;
using Kanban.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Kanban
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
            services.Configure<AppSettings>(Configuration);
            services.AddControllersWithViews();
            services.AddDbContext<KanbanContext>(options =>
            {
                var appSettings = Configuration.Get<AppSettings>();
                var sqlProvider = appSettings.SqlProvider; // Configuration.GetValue("SqlProvider", "sqlserver");
                Console.WriteLine($"appsettings.SqlProvider: {sqlProvider}");
                if (sqlProvider == "sqlite")
                {
                    options.UseSqlite(Configuration.GetConnectionString($"KanbanContext_{sqlProvider}"));
                }
                else
                {
                    sqlProvider = "sqlserver";
                    options.UseSqlServer(Configuration.GetConnectionString($"KanbanContext_{sqlProvider}"));
                }
                Console.WriteLine($" '{sqlProvider}' database provider is used");

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Project}/{action=Index}/{id?}");
            });
        }
    }
}
