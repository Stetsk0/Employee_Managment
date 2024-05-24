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

        //public void AddVacation(Vacation vacation)
        //{
        //    vacation.Id = _vacations.Count > 0 ? _vacations.Max(v => v.Id) + 1 : 1;
        //    _vacations.Add(vacation);
        //}

        //public List<Vacation> GetVacations()
        //{
        //    //return _context.Vacations.Where(v => v.EmployeeId == employeeId).ToList();
        //    return _context.Vacations.ToList();
        //}

        //public Vacation? GetVacationById(int employeeId)
        //{
        //    var vacations = _context.Vacations.FirstOrDefault(c => c.Id == employeeId);
        //    if (vacations != null)
        //    {
        //        return new Vacation { Id = employeeId, VacationDays = vacations.VacationDays, VacationType = vacations.VacationType};
        //    }

        //    return null;
        //    //return _context.Vacations.FirstOrDefault(s => s.Id == employeeId);
        //}

        //public void UpdateVacation(int employeeId, Vacation vacation)
        //{
        //    //_context.Vacations.Update(vacation);
        //    //_context.SaveChanges();
        //    GetVacationById(employeeId);

        //    var vacationsToUpdate = _context.Vacations.FirstOrDefault(c => c.Id == employeeId);
        //    if (vacationsToUpdate != null)
        //    {
        //        vacationsToUpdate.VacationDays = vacation.VacationDays;
        //        vacationsToUpdate.VacationType = vacation.VacationType;
        //    }

        //    _context.SaveChanges();
        //}


        //public void DeleteVacation(int employeeId)
        //{
        //    var vacation = _context.Vacations.Find(employeeId);
        //    if (vacation != null)
        //    {
        //        _context.Vacations.Remove(vacation);
        //        _context.SaveChanges();
        //    }
        //}
        public IEnumerable<Vacation> GetAllVacations()
        {
            return _context.Vacations.ToList();
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
