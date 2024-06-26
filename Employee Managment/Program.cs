using Employee_Managment.Data;
using Employee_Managment.Repository;

namespace Employee_Managment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ApplicationDbContext>();
            builder.Services.AddScoped<EmployeesRepository>();
            builder.Services.AddScoped<CredentialsRepository>();
            builder.Services.AddScoped<StatisticsRepository>();
            builder.Services.AddScoped<VacationsRepository>();
            builder.Services.AddScoped<PenaltiesRepository>();

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddAuthentication().AddCookie("CookieAuth", options =>
            {
                options.Cookie.Name = "CookieAuth";
                options.LoginPath = "/access/login";
                options.AccessDeniedPath = "/access/error";
            });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
                options.AddPolicy("Employee", policy => policy.RequireRole("Employee"));
            });

            var app = builder.Build();

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
