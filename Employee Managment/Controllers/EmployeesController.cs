using Employee_Managment.Models;
using Employee_Managment.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace Employee_Managment.Controllers
{
    [Authorize(Policy = "Admin")]
    public class EmployeesController : Controller
    {
        private readonly EmployeesRepository _employeesRepository;
        public EmployeesController(EmployeesRepository employeesRepository)
        {
            _employeesRepository = employeesRepository;
        }

        public ActionResult Index()
        {
            var employees = _employeesRepository.GetEmployees();

            return View(employees);
        }

        public ActionResult Add()
        {
            ViewBag.Action = "add";

            return View();
        }

        [HttpPost]
        public ActionResult Add(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _employeesRepository.AddEmployee(employee);
                return RedirectToAction(nameof(Index));
            }

            return View(employee);
        }

        public ActionResult Edit(int? id)
        {
            ViewBag.Action = "edit";

            //var employee = new Employee { Id = id.HasValue ? id.Value : 0 };
            var employee = _employeesRepository.GetEmployeeById(id.HasValue ? id.Value : 0);

            return View(employee);
        }

        [HttpPost]
        public ActionResult Edit(Employee employee)
        {

            if (ModelState.IsValid)
            {
                _employeesRepository.UpdateEmployee(employee.Id, employee);
                return RedirectToAction(nameof(Index));
            }

            return View(employee);

        }

        public ActionResult Delete(int employeeId)
        {
            _employeesRepository.DeleteEmployee(employeeId);

            return RedirectToAction(nameof(Index));
        }
    }
}
