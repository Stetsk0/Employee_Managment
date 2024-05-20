using Employee_Managment.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Managment.Controllers
{
    [Authorize(Policy = "Employee")]
    public class DashboardController : Controller
    {
        private readonly StatisticsRepository _statisticsRepository;
        public DashboardController(StatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }

        public IActionResult Index()
        {
            var statistics = _statisticsRepository.GetStatistics();

            return View(statistics);
        }
    }
}
