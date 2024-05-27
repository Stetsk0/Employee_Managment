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

        //public IActionResult Index(int id)
        //{
        //    var statistics = _statisticsRepository.GetStatisticsById(id);

        //    if (statistics == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(statistics);
        //}
        public IActionResult Index(int id)
        {
            var statistics = _statisticsRepository.GetStatisticsById(id);

            if (statistics == null)
            {
                return NotFound();
            }

            var allStatistics = _statisticsRepository.GetStatistics();
            var averageStatistics = new Statistics
            {
                Salary = (int)allStatistics.Average(s => s.Salary),
                QA = (int)allStatistics.Average(s => s.QA),
                Bonus = (int)allStatistics.Average(s => s.Bonus),
                HoursWorked = (int)allStatistics.Average(s => s.HoursWorked),
                OvertimeHours = (int)allStatistics.Average(s => s.OvertimeHours),
                CompletedTasks = (int)allStatistics.Average(s => s.CompletedTasks),
                AverageTaskCompletionTime = (int)allStatistics.Average(s => s.AverageTaskCompletionTime),
                OnTimeTaskCompletionPercentage = allStatistics.Average(s => s.OnTimeTaskCompletionPercentage)
            };

            var viewModel = new EmployeeStatisticsViewModel
            {
                EmployeeStatistics = statistics,
                AverageStatistics = averageStatistics
            };

            return View(viewModel);
        }

    }
}
