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
        private readonly EmployeesRepository _employeesRepository;
        public DashboardController(StatisticsRepository statisticsRepository, EmployeesRepository employeesRepository)
        {
            _statisticsRepository = statisticsRepository;
            _employeesRepository = employeesRepository;
        }

        public IActionResult Index(int id)
        {
            var statistics = _statisticsRepository.GetStatisticsById(id);
            var departmentSalaryStatistics = _employeesRepository.GetDepartmentAverageSalaryStatistics().ToDictionary(x => x.Key, x => (int)Math.Round(x.Value));

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
                CompletedTasks = (int)allStatistics.Average(s => s.CompletedTasks),
                AverageTaskCompletionTime = (int)allStatistics.Average(s => s.AverageTaskCompletionTime),
                OnTimeTaskCompletionPercentage = allStatistics.Average(s => s.OnTimeTaskCompletionPercentage)
            };

            var viewModel = new EmployeeStatisticsViewModel
            {
                EmployeeStatistics = statistics,
                AverageStatistics = averageStatistics,
                DepartmentSalaryStatistics = departmentSalaryStatistics
            };

            return View(viewModel);
        }

    }
}
