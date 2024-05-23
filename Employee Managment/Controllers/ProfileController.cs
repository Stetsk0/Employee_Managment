using Employee_Managment.Models;
using Employee_Managment.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Managment.Controllers
{
    [Authorize(Policy = "Employee")]
    public class ProfileController : Controller
    {
        private readonly EmployeesRepository _employeesRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProfileController(EmployeesRepository employeesRepository, IWebHostEnvironment webHostEnvironment)
        {
            _employeesRepository = employeesRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index(int id)
        {
            var employee = _employeesRepository.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }
        //public IActionResult Index()
        //{
        //    var employee = _employeesRepository.GetEmployees();
        //    if (employee == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(employee);
        //}

        [HttpPost]
        public async Task<IActionResult> UploadAvatar(int id, IFormFile avatar)
        {
            if (avatar != null && avatar.Length > 0)
            {
                var employee = _employeesRepository.GetEmployeeById(id);
                if (employee == null)
                {
                    return NotFound();
                }

                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "avatars");
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + avatar.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await avatar.CopyToAsync(fileStream);
                }

                // Удаление старого аватара, если он существует
                if (!string.IsNullOrEmpty(employee.AvatarFileName))
                {
                    var oldFilePath = Path.Combine(uploadsFolder, employee.AvatarFileName);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                employee.AvatarFileName = uniqueFileName;
                _employeesRepository.UpdateEmployee(employee.Id, employee);

                
                return RedirectToAction(nameof(Index), new { id = employee.Id });
            }

            return RedirectToAction(nameof(Index), new { id });
        }
    }
}
