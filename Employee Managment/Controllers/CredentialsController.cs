using Employee_Managment.Models;
using Employee_Managment.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Managment.Controllers
{
    public class CredentialsController : Controller
    {
        private readonly CredentialsRepository _credentialsRepository;

        public CredentialsController(CredentialsRepository credentialsRepository)
        {
            _credentialsRepository = credentialsRepository;
        }

        // GET: Credentials/Edit/5
        public ActionResult Edit(int? id)
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

            return View(credentials);
        }

        // POST: Credentials/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Credentials credentials)
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
