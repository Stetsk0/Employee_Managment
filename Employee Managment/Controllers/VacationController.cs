using Employee_Managment.Models;
using Employee_Managment.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Employee_Managment.Controllers
{
    //[Authorize(Policy = "Employee")]
    //public class VacationController : Controller
    //{
    //    private readonly VacationRepository _vacationRepository;
    //    private readonly EmployeesRepository _employeesRepository;

    //    public VacationController(VacationRepository vacationRepository, EmployeesRepository employeesRepository)
    //    {
    //        _vacationRepository = vacationRepository;
    //        _employeesRepository = employeesRepository;
    //    }

    //    public IActionResult Index(int id)
    //    {
    //        var employee = _employeesRepository.GetEmployeeById(id);
    //        if (employee == null)
    //        {
    //            return NotFound();
    //        }

    //        var vacation = employee.Vacation;
    //        return View(vacation);
    //    }

    //    public IActionResult RequestVacation(int employeeId)
    //    {
    //        var employee = _employeesRepository.GetEmployeeById(employeeId);
    //        if (employee == null)
    //        {
    //            return NotFound();
    //        }

    //        ViewBag.AvailableVacationDays = employee.Statistics?.VacationDays ?? 0;
    //        ViewBag.EmployeeId = employee.Id;

    //        return View();
    //    }

    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public IActionResult RequestVacation(Vacation vacation)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            var employee = _employeesRepository.GetEmployeeById(vacation.Id);
    //            if (employee == null)
    //            {
    //                return NotFound();
    //            }

    //            var availableVacationDays = employee.Statistics?.VacationDays ?? 0;
    //            vacation.NumberOfDays = (vacation.EndDate - vacation.StartDate).Days + 1;
    //            if (vacation.NumberOfDays > availableVacationDays)
    //            {
    //                ModelState.AddModelError("", "You do not have enough vacation days available.");
    //                ViewBag.AvailableVacationDays = availableVacationDays;
    //                return View(vacation);
    //            }

    //            employee.Statistics.VacationDays -= vacation.NumberOfDays;
    //            employee.Id = vacation.Id; // Update the vacation property of employee
    //            _employeesRepository.UpdateEmployee(employee.Id, employee);

    //            return RedirectToAction(nameof(Index), new { employeeId = vacation.Id });
    //        }

    //        return View(nameof(Index));
    //    }
    //}

}
