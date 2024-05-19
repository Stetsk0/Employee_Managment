using Microsoft.AspNetCore.Mvc;

namespace Employee_Managment.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
