using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using WebApplicationEWX.Repo;

namespace WebApplicationEWX
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string connectionString = @"Data Source=ServerName;Database=DatabaseName;User ID=UserName;Trust Server Certificate=True;Password=password";
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddTransient(typeof(SqlConnection), (service) => { return new SqlConnection(connectionString); })
            .AddTransient<IEmployeesRepo, EmployeesRepo>();
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
                pattern: "{controller=Employee}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
