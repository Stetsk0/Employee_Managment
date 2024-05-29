using Employee_Managment.Data;
using Employee_Managment.Repository;
using NuGet.Protocol.Core.Types;

namespace Employee_Managment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ApplicationDbContext>();
            builder.Services.AddScoped<EmployeesRepository>();
            builder.Services.AddScoped<CredentialsRepository>();
            builder.Services.AddScoped<StatisticsRepository>();
            builder.Services.AddScoped<VacationsRepository>();
            builder.Services.AddScoped<PenaltiesRepository>();

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddAuthentication().AddCookie("MyCookieAuth", options =>
            {
                options.Cookie.Name = "MyCookieAuth";
                options.LoginPath = "/access/login";
                options.AccessDeniedPath = "/access/error";
            });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireClaim("Admin"));
                options.AddPolicy("Employee", policy => policy.RequireClaim("Employee"));
            });

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

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            //app.MapControllerRoute(
            //    name: "profile",
            //    pattern: "{controller=Profile}/{action=Index}/{id?}");
            app.Run();
        }
    }
}
