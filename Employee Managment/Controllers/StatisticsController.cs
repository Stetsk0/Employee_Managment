using Microsoft.AspNetCore.Mvc;

namespace Employee_Managment.Controllers
{
    public class StatisticsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
