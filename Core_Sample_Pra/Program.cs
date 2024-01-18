using Core_Sample_Pra.IServer;
using Core_Sample_Pra.Models;
using Core_Sample_Pra.Repository;
using Core_Sample_Pra.ServiceModel;
using Core_Sample_Pra.ViewModel;
using System.Data;
using System.Data.SqlClient;

namespace Core_Sample_Pra
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            /*
             註冊 Service 有分三種方式：
            Transient (AddTransient)
            每次注入時，都重新 new 一個新的實體。
            Scoped (AddScoped)
            每個 Request 都重新 new 一個新的實體。
            Singleton (AddSingleton)
            程式啟動後會 new 一個實體。也就是運行期間只會有一個實體。
             */
            builder.Services.AddScoped<HomeService, HomeService>();
            builder.Services.AddScoped<IGSSWEBBOOK, HomeRepository>();
            builder.Services.AddScoped<IDbConnection>(c => new SqlConnection(configuration.GetConnectionString("DefaultConn")));
          
            var app = builder.Build();
           
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


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