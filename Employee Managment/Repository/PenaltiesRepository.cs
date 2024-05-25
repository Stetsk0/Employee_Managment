using Employee_Managment.Data;
using Employee_Managment.Models;
using Microsoft.EntityFrameworkCore;

namespace Employee_Managment.Repository
{
    public class PenaltiesRepository
    {
        private readonly ApplicationDbContext _context;

        public PenaltiesRepository(ApplicationDbContext context)
        {
            _context = context;
        }
    
        public List<Penalty> GetPenaltiesByEmployeeId(int employeeId)
        {
            return _context.Penalties
                           .Where(p => p.EmployeeId == employeeId)
                           .ToList();
        }

        public Penalty? GetPenaltyById(int id)
        {
            return _context.Penalties
                           .Include(p => p.Employee)
                           .FirstOrDefault(p => p.Id == id);
        }

        public void AddPenalty(Penalty penalty)
        {
            _context.Penalties.Add(penalty);
            _context.SaveChanges();
        }

        public void UpdatePenalty(Penalty penalty)
        {
            _context.Penalties.Update(penalty);
            _context.SaveChanges();
        }

        public void DeletePenalty(int id)
        {
            var penalty = _context.Penalties.FirstOrDefault(p => p.Id == id);
            if (penalty != null)
            {
                _context.Penalties.Remove(penalty);
                _context.SaveChanges();
            }
        }
    }
}
