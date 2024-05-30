using Employee_Managment.Models;
using Employee_Managment.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Managment.Controllers
{
    [Authorize]
    public class VacationController : Controller
    {
        private readonly VacationsRepository _vacationsRepository;
        private readonly EmployeesRepository _employeesRepository;

        public VacationController(VacationsRepository vacationRepository, EmployeesRepository employeesRepository)
        {
            _vacationsRepository = vacationRepository;
            _employeesRepository = employeesRepository;
        }

        
        public IActionResult Index(int employeeId)
        {

            var employee = _employeesRepository.GetEmployeeById(employeeId);
            if (employee == null) return NotFound();

            ViewBag.Employee = employee;
            var vacations = _vacationsRepository.GetVacationsByEmployeeId(employeeId);
            return View(vacations);

        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create(int employeeId)
        {
            var employee = _employeesRepository.GetEmployeeById(employeeId);
            if (employee == null) return NotFound();

            var vacation = new Vacation { EmployeeId = employeeId };
            return View(vacation);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Vacation vacation)
        {
            if (ModelState.IsValid)
            {
                _vacationsRepository.AddVacation(vacation);
                return RedirectToAction(nameof(Index), new { employeeId = vacation.EmployeeId });
            }
            return View(vacation);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var vacation = _vacationsRepository.GetVacationById(id);
            if (vacation == null) return NotFound();

            return View(vacation);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Vacation vacation)
        {
            if (ModelState.IsValid)
            {
                _vacationsRepository.UpdateVacation(vacation);
                return RedirectToAction(nameof(Index), new { employeeId = vacation.EmployeeId });
            }
            return View(vacation);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var vacation = _vacationsRepository.GetVacationById(id);
            if (vacation == null) return NotFound();

            return View(vacation);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var vacation = _vacationsRepository.GetVacationById(id);

            if (vacation != null)
            {
                _vacationsRepository.DeleteVacation(id);
            }

            return RedirectToAction(nameof(Index), new { employeeId = vacation!.EmployeeId });
        }
    }
}

