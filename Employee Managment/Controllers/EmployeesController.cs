using Employee_Managment.Models;
using Employee_Managment.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Managment.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EmployeesController : Controller
    {
        private readonly EmployeesRepository _employeesRepository;
        public EmployeesController(EmployeesRepository employeesRepository)
        {
            _employeesRepository = employeesRepository;
        }

        public IActionResult Index()
        {
            var employees = _employeesRepository.GetEmployees();

            return View(employees);
        }

        public IActionResult Add()
        {
            ViewBag.Action = "add";

            return View();
        }

        [HttpPost]
        public IActionResult Add(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _employeesRepository.AddEmployee(employee);
                return RedirectToAction(nameof(Index));
            }

            return View(employee);
        }

        public IActionResult Edit(int? id)
        {
            ViewBag.Action = "edit";

            var employee = _employeesRepository.GetEmployeeById(id.HasValue ? id.Value : 0);

            return View(employee);
        }

        [HttpPost]
        public IActionResult Edit(Employee employee)
        {

            if (ModelState.IsValid)
            {
                _employeesRepository.UpdateEmployee(employee.Id, employee);
                return RedirectToAction(nameof(Index));
            }

            return View(employee);

        }

        public IActionResult Delete(int employeeId)
        {
            _employeesRepository.DeleteEmployee(employeeId);

            return RedirectToAction(nameof(Index));
        }
    }
}
