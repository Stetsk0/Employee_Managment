using Employee_Managment.Data;
using Employee_Managment.Models;
using Microsoft.EntityFrameworkCore;

namespace Employee_Managment.Repository
{
    public class VacationsRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly EmployeesRepository _employeesRepository;

        public VacationsRepository(ApplicationDbContext context, EmployeesRepository employeesRepository)
        {
            _context = context;
            _employeesRepository = employeesRepository;
        }

        public List<Vacation> GetVacationsByEmployeeId(int employeeId)
        {
            return _context.Vacations
                           .Where(v => v.EmployeeId == employeeId)
                           .ToList();
        }

        public Vacation? GetVacationById(int id)
        {
            return _context.Vacations
                           .Include(v => v.Employee)
                           .FirstOrDefault(v => v.Id == id);
        }

        public void AddVacation(Vacation vacation)
        {
            _context.Vacations.Add(vacation);
            _context.SaveChanges();
        }

        public void UpdateVacation(Vacation vacation)
        {
            _context.Vacations.Update(vacation);
            _context.SaveChanges();
        }

        public void DeleteVacation(int id)
        {
            var vacation = _context.Vacations.FirstOrDefault(v => v.Id == id);
            if (vacation != null)
            {
                _context.Vacations.Remove(vacation);
                _context.SaveChanges();
            }
        }
    }
}
