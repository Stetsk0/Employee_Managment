using Employee_Managment.Models;
using Employee_Managment.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Managment.Controllers
{
    [Authorize(Policy = "Admin")]
    public class CredentialsController : Controller
    {
        private readonly CredentialsRepository _credentialsRepository;

        public CredentialsController(CredentialsRepository credentialsRepository)
        {
            _credentialsRepository = credentialsRepository;
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var credentials = _credentialsRepository.GetCredentialsById(id.Value);
            if (credentials == null)
            {
                return NotFound();
            }
            credentials.Password = "";

            return View(credentials);
        }

        [HttpPost]
        public IActionResult Edit(Credentials credentials)
        {
            if (ModelState.IsValid)
            {
                _credentialsRepository.UpdateEmployeeCredentials(credentials.Id, credentials);
                return RedirectToAction("Index", "Employees");
            }

            return View(credentials);
        }
    }
}
