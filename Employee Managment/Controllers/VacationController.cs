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

        public VacationController(VacationRepository vacationRepository)
        {
            _vacationRepository = vacationRepository;
        }

        public IActionResult Index()
        {
            var vacations = _vacationRepository.GetVacations();
            return View(vacations);
        }

        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            return View(new Vacation());
        }

        [HttpPost]
        public IActionResult Add(Vacation vacation)
        {
            if (ModelState.IsValid)
            {
                _vacationRepository.AddVacation(vacation);
                return RedirectToAction(nameof(Index));
            }
            return View(vacation);
        }

        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            var vacation = _vacationRepository.GetVacationById(id);
            if (vacation == null)
            {
                return NotFound();
            }
            return View(vacation);
        }

        [HttpPost]
        public IActionResult Edit(Vacation vacation)
        {
            if (ModelState.IsValid)
            {
                _vacationRepository.UpdateVacation(vacation);
                return RedirectToAction(nameof(Index));
            }
            return View(vacation);
        }

        public IActionResult Delete(int id)
        {
            var vacation = _vacationRepository.GetVacationById(id);
            if (vacation == null)
            {
                return NotFound();
            }
            return View(vacation);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            _vacationRepository.DeleteVacation(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
