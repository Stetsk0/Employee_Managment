using Employee_Managment.Models;
using Employee_Managment.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Managment.Controllers
{
    [Authorize]
    public class PenaltiesController : Controller
    {
        private readonly PenaltiesRepository _penaltiesRepository;
        private readonly EmployeesRepository _employeesRepository;

        public PenaltiesController(PenaltiesRepository penaltiesRepository, EmployeesRepository employeesRepository)
        {
            _penaltiesRepository = penaltiesRepository;
            _employeesRepository = employeesRepository;
        }

        public IActionResult Index(int employeeId)
        {
            var employee = _employeesRepository.GetEmployeeById(employeeId);
            if (employee == null) return NotFound();

            ViewBag.Employee = employee;
            var penalties = _penaltiesRepository.GetPenaltiesByEmployeeId(employeeId);
            return View(penalties);
        }

        [Authorize(Policy = "Admin")]
        public IActionResult Create(int employeeId)
        {
            var employee = _employeesRepository.GetEmployeeById(employeeId);
            if (employee == null) return NotFound();

            var penalty = new Penalty { EmployeeId = employeeId };
            return View(penalty);
        }

        [Authorize(Policy = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Penalty penalty)
        {
            if (ModelState.IsValid)
            {
                _penaltiesRepository.AddPenalty(penalty);
                return RedirectToAction(nameof(Index), new { employeeId = penalty.EmployeeId });
            }
            return View(penalty);
        }

        [Authorize(Policy = "Admin")]
        public IActionResult Edit(int id)
        {
            var penalty = _penaltiesRepository.GetPenaltyById(id);
            if (penalty == null) return NotFound();

            return View(penalty);
        }

        [Authorize(Policy = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Penalty penalty)
        {
            if (ModelState.IsValid)
            {
                _penaltiesRepository.UpdatePenalty(penalty);
                return RedirectToAction(nameof(Index), new { employeeId = penalty.EmployeeId });
            }
            return View(penalty);
        }

        [Authorize(Policy = "Admin")]
        public IActionResult Delete(int id)
        {
            var penalty = _penaltiesRepository.GetPenaltyById(id);
            if (penalty == null) return NotFound();

            return View(penalty);
        }

        [Authorize(Policy = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var penalty = _penaltiesRepository.GetPenaltyById(id);
            if (penalty != null)
            {
                _penaltiesRepository.DeletePenalty(id);
                return RedirectToAction(nameof(Index), new { employeeId = penalty.EmployeeId });
            }
            return NotFound();
        }
    }
}
