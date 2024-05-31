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
            var employee = await _context.Employees
                .Include(e => e.Credentials)
                .FirstOrDefaultAsync(x => x.Credentials!.UserName == modelLogin.Credential.UserName);

            if (employee != null && PasswordHasher.VerifyPassword(modelLogin.Credential.Password, employee.Credentials!.Password))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, employee.Name),
                    new Claim(ClaimTypes.Role, "Employee"),
                    new Claim("EmployeeId", employee.Id.ToString()),
                };

                ClaimsIdentity identity = new ClaimsIdentity(claims, "CookieAuth");

                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("CookieAuth", claimsPrincipal);

                return RedirectToAction("Index", "Home");
            }

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
