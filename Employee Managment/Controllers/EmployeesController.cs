using Employee_Managment.Models;
using Employee_Managment.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace Employee_Managment.Controllers
{
    //[Authorize(Policy = "AdminOnly")]
    public class EmployeesController : Controller
    {
        private readonly EmployeesRepository _employeesRepository;
        public EmployeesController(EmployeesRepository employeesRepository)
        {
            _employeesRepository = employeesRepository;
        }
        // GET: EmployeeController
        public ActionResult Index()
        {
            var employees = _employeesRepository.GetEmployees();

            return View(employees);
        }

        // GET: EmployeeController/Add
        public ActionResult Add()
        {
            ViewBag.Action = "add";

            return View();
        }

        // POST: EmployeeController/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _employeesRepository.AddEmployee(employee);
                return RedirectToAction(nameof(Index));
            }

            return View(employee);
        }

        // GET: EmployeeController/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.Action = "edit";

            //var employee = new Employee { Id = id.HasValue ? id.Value : 0 };
            var employee = _employeesRepository.GetEmployeeById(id.HasValue ? id.Value : 0);

            return View(employee);
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee employee)
        {

            if (ModelState.IsValid)
            {
                _employeesRepository.UpdateEmployee(employee.Id, employee);
                return RedirectToAction(nameof(Index));
            }

            return View(employee);

        }

        // POST: EmployeeController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int employeeId)
        {
            _employeesRepository.DeleteEmployee(employeeId);

            return RedirectToAction(nameof(Index));
        }
    }
}
