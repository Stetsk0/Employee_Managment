using Employee_Managment.Models;
using Employee_Managment.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Managment.Controllers
{
    [Authorize(Policy = "Admin")]
    public class StatisticsController : Controller
    {
        private readonly StatisticsRepository _statisticsRepository;

        public StatisticsController(StatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statistics = _statisticsRepository.GetStatisticsById(id.Value);
            if (statistics == null)
            {
                return NotFound();
            }

            return View(statistics);
        }

        [HttpPost]
        public IActionResult Edit(int id, Statistics statistics)
        {
            if (id != statistics.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _statisticsRepository.UpdateStatistics(statistics);
                return RedirectToAction("Index", "Employees");
            }

            return View(statistics);
        }
    }
}
