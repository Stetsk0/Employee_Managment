using Microsoft.AspNetCore.Mvc;

namespace Employee_Managment.Controllers
{
    public class AccessController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
