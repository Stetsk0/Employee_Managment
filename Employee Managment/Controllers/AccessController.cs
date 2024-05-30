using Employee_Managment.Data;
using Employee_Managment.Models;
using Employee_Managment.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Employee_Managment.Controllers
{
    public class AccessController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;
        public AccessController(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login modelLogin)
        {
            //Пошук співробітника за ім'ям користувача
            var employee = await _context.Employees
                .Include(e => e.Credentials)
                .FirstOrDefaultAsync(x => x.Credentials!.UserName == modelLogin.Credential.UserName);

            // 1. Перевірка облікових даних співробітника
            if (employee != null && PasswordHasher.VerifyPassword(modelLogin.Credential.Password, employee.Credentials!.Password))
            {
                // 2. Створення заяв
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, employee.Name),
                    new Claim(ClaimTypes.Role, "Employee"),
                    new Claim("EmployeeId", employee.Id.ToString()),
                };

                // 3. Створення особи на основі заяв
                ClaimsIdentity identity = new ClaimsIdentity(claims, "CookieAuth");

                // 4. Створення об'єкта ClaimsPrincipal для представлення користувача
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                // 5. Аутентифікація користувача за допомогою збереження інформації в cookie
                await HttpContext.SignInAsync("CookieAuth", claimsPrincipal);

                // 6. Перенаправлення користувача після успішної аутентифікації
                return RedirectToAction("Index", "Home");
            }

            // Перевірка облікових даних адміністратора
            var adminPassword = _config.GetValue<string>("Admin:Password");
            var adminLogin = _config.GetValue<string>("Admin:UserName");

            if (modelLogin.Credential.UserName == adminLogin && modelLogin.Credential.Password == adminPassword)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "Admin"),
                    new Claim(ClaimTypes.Role, "Admin"),
                };

                ClaimsIdentity identity = new ClaimsIdentity(claims, "CookieAuth");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("CookieAuth", claimsPrincipal);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Неправильна спроба входу.");
            return View(modelLogin);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("CookieAuth");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Error()
        {
            return View();
        }

    }
}
