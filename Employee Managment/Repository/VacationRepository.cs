using Employee_Managment.Data;
using Employee_Managment.Models;
using Microsoft.EntityFrameworkCore;

namespace Employee_Managment.Repository
{
    public class VacationRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly EmployeesRepository _employeesRepository;

        public VacationRepository(ApplicationDbContext context, EmployeesRepository employeesRepository)
        {
            _context = context;
            _employeesRepository = employeesRepository;
        }

        public IEnumerable<Vacation> GetAllVacations()
        {
            return _context.Vacations.ToList();
        }

        public IEnumerable<Vacation> GetVacationsByEmployeeId(int employeeId)
        {
            return _context.Vacations.Where(v => v.Id == employeeId).ToList();
        }

        public Vacation GetVacation(int id)
        {
            return _context.Vacations.FirstOrDefault(v => v.Id == id);
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
            var vacation = GetVacation(id);
            if (vacation != null)
            {
                _context.Vacations.Remove(vacation);
                _context.SaveChanges();
            }
        }
    }
}
