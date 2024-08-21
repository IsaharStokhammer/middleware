using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using middleware.Data;
using static System.Net.Mime.MediaTypeNames;
using System;
namespace middleware
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<middlewareContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("middlewareContext") ?? throw new InvalidOperationException("Connection string 'middlewareContext' not found.")));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.Use(async(HttpContext context, Func < Task > next) =>
            {
                
                using (StreamWriter writer = new StreamWriter("logins.txt", append: true))
                {
                    writer.WriteLine(context.Request.QueryString);
                    writer.WriteLine(context.Request.Host);
                    writer.WriteLine(context.Request.ContentType);
                    writer.WriteLine(DateTime.Now);
                }
                    await next();
            });
            app.Use(async (HttpContext context, Func<Task> next) =>
            {

                using (StreamWriter writer = new StreamWriter("logins.txt", append: true))
                {
                    writer.WriteLine(context.Request.QueryString);
                    writer.WriteLine(DateTime.Now);
                }
                    await next();
            });
            app.UseHttpsRedirection();
            app.UseStaticFiles();
        app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
            
        }
    }
}
