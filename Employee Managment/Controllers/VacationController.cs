using Employee_Managment.Models;
using Employee_Managment.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Managment.Controllers
{
    [Authorize(Policy = "Admin")]
    public class VacationController : Controller
    {
        private readonly VacationRepository _vacationRepository;
        private readonly EmployeesRepository _employeesRepository;

        public VacationController(VacationRepository vacationRepository, EmployeesRepository employeesRepository)
        {
            _vacationRepository = vacationRepository;
            _employeesRepository = employeesRepository;
        }

        //public IActionResult Index(int id)
        //{
        //    //var vacations = _vacationRepository.GetVacations(employeeId);

        //    //return View(vacations);
        //    //ViewBag.EmployeeId = employeeId;

        //    var vacations = _vacationRepository.GetVacationById(id);
        //    return View(vacations);
        //}

        //public IActionResult Add()
        //{
        //    //ViewBag.EmployeeId = employeeId;
        //    ViewBag.Action = "add";
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult Add(Vacation vacation)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _vacationRepository.AddVacation(vacation);
        //        return RedirectToAction(nameof(Index), new { vacation.Id });
        //    }
        //    //ViewBag.EmployeeId = vacation.EmployeeId;
        //    return View(vacation);
        //}

        //public IActionResult Edit(int id)
        //{
        //    ViewBag.Action = "edit";
        //    var vacation = _vacationRepository.GetVacationById(id);
        //    if (vacation == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(vacation);
        //}

        //[HttpPost]
        //public IActionResult Edit(Vacation vacation)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _vacationRepository.UpdateVacation(vacation.Id, vacation);
        //        return RedirectToAction(nameof(Index), new { id = vacation.Id });
        //    }
        //    return View(vacation);
        //}

        ////public IActionResult Delete(int id)
        ////{
        ////    var vacation = _vacationRepository.GetVacationById(id);
        ////    if (vacation == null)
        ////    {
        ////        return NotFound();
        ////    }
        ////    return View(vacation);
        ////}

        ////[HttpPost, ActionName("Delete")]
        ////public IActionResult DeleteConfirmed(int id)
        ////{
        ////    var vacation = _vacationRepository.GetVacationById(id);
        ////    if (vacation != null)
        ////    {
        ////        _vacationRepository.DeleteVacation(id);
        ////        return RedirectToAction(nameof(Index), new { employeeId = vacation.EmployeeId });
        ////    }
        ////    return NotFound();
        ////}
        //public IActionResult Delete(int employeeId)
        //{
        //    _vacationRepository.DeleteVacation(employeeId);

        //    return RedirectToAction(nameof(Index));
        //}
        //222222222222222
        //public IActionResult Index()
        //{
        //    var vacations = _vacationRepository.GetAllVacations();
        //    return View(vacations);
        //}

        //public IActionResult Add()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult Add(Vacation vacation)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _vacationRepository.AddVacation(vacation);
        //        return RedirectToAction("Index");
        //    }
        //    return View(vacation);
        //}

        //public IActionResult Edit(int id)
        //{
        //    var vacation = _vacationRepository.GetVacationsByEmployeeId(id);
        //    if (vacation == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(vacation);
        //}

        //[HttpPost]
        //public IActionResult Edit(Vacation vacation)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _vacationRepository.UpdateVacation(vacation);
        //        return RedirectToAction("Index");
        //    }
        //    return View(vacation);
        //}

        //public IActionResult Delete(int id)
        //{
        //    var vacation = _vacationRepository.GetVacationsByEmployeeId(id);
        //    if (vacation == null)
        //    {
        //        return NotFound();
        //    }
        //    _vacationRepository.DeleteVacation(id);
        //    return RedirectToAction("Index");
        //}

        public IActionResult Index(int employeeId)
        {
            IEnumerable<Vacation> vacations = _vacationRepository.GetVacationsByEmployeeId(employeeId);
            ViewBag.EmployeeId = employeeId;
            return View(vacations);
        }

        public IActionResult Add(int employeeId)
        {
            var employee = _employeesRepository.GetEmployeeById(employeeId);
            if (employee == null)
            {
                return NotFound();
            }

            var model = new Vacation { Employee = employee };
            ViewBag.EmployeeName = employee.Name;
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(Vacation vacation)
        {
            if (vacation.Employee == null || vacation.Employee.Id == 0)
            {
                ModelState.AddModelError("Employee", "Employee is required.");
            }

            if (ModelState.IsValid)
            {
                var employee = _employeesRepository.GetEmployeeById(vacation.Employee.Id);
                if (employee == null)
                {
                    ModelState.AddModelError("Employee", "Employee not found.");
                }
                else
                {
                    vacation.Employee = employee;
                    _vacationRepository.AddVacation(vacation);
                    return RedirectToAction("Index", new { employeeId = vacation.Employee.Id });
                }
            }
            ViewBag.EmployeeName = vacation.Employee?.Name;
            return View(vacation);
        }

        public IActionResult Edit(int id)
        {
            Vacation vacation = _vacationRepository.GetVacation(id);
            if (vacation == null)
            {
                return NotFound();
            }
            ViewBag.EmployeeName = vacation.Employee.Name;
            return View(vacation);
        }

        [HttpPost]
        public IActionResult Edit(Vacation vacation)
        {
            if (vacation.Employee == null || vacation.Employee.Id == 0)
            {
                ModelState.AddModelError("Employee", "Employee is required.");
            }

            if (ModelState.IsValid)
            {
                var employee = _employeesRepository.GetEmployeeById(vacation.Employee.Id);
                if (employee == null)
                {
                    ModelState.AddModelError("Employee", "Employee not found.");
                }
                else
                {
                    vacation.Employee = employee;
                    _vacationRepository.UpdateVacation(vacation);
                    return RedirectToAction("Index", new { employeeId = vacation.Employee.Id });
                }
            }
            ViewBag.EmployeeName = vacation.Employee?.Name;
            return View(vacation);
        }
    }
}
