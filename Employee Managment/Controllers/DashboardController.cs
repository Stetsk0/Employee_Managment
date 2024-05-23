using Employee_Managment.Models;
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

        public IActionResult Index(int id)
        {
            var statistics = _statisticsRepository.GetStatisticsById(id);

            if (statistics == null)
            {
                return NotFound();
            }

            return View(statistics);
        }

    }
}
