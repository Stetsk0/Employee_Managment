using Employee_Managment.Data;
using Employee_Managment.Models;
using Microsoft.EntityFrameworkCore;

namespace Employee_Managment.Repository
{
    public class VacationRepository
    {
        private readonly ApplicationDbContext _context;

        public VacationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddVacation(Vacation vacation)
        {
            _context.Vacations.Add(vacation);
            _context.SaveChanges();
        }

        public List<Vacation> GetVacations()
        {
            return _context.Vacations.ToList();
        }

        public Vacation? GetVacationById(int id)
        {
            return _context.Vacations.Find(id);
        }

        public void UpdateVacation(Vacation vacation)
        {
            _context.Vacations.Update(vacation);
            _context.SaveChanges();
        }

        public void DeleteVacation(int id)
        {
            var vacation = _context.Vacations.Find(id);
            if (vacation != null)
            {
                _context.Vacations.Remove(vacation);
                _context.SaveChanges();
            }
        }
    }
}
