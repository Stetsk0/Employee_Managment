using Employee_Managment.Data;
using Employee_Managment.Models;
using Employee_Managment.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Employee_Managment.Repository
{
    public class EmployeesRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddEmployee(Employee employee)
        {
            employee.Credentials!.Password = PasswordHasher.HashPassword(employee.Credentials.Password);
            var statistics = new Statistics();
            //{
            //    Salary = 0,
            //    QA = 0,
            //    Bonus = 0,
            //    HoursWorked = 0
            //};

            employee.Statistics = statistics;

            _context.Employees.Add(employee);

            _context.SaveChanges();
        }

        public List<Employee> GetEmployees()
        {
             return _context.Employees.ToList();
             
        }

        public Employee? GetEmployeeById(int employeeId)
        {
            var employee = _context.Employees.FirstOrDefault(c => c.Id == employeeId);
            if (employee != null)
            {
                return new Employee { Id = employee.Id, Name = employee.Name, Department = employee.Department, DateOfBirth = employee.DateOfBirth, PhoneNumber = employee.PhoneNumber, Email = employee.Email, Position = employee.Position, AvatarFileName = employee.AvatarFileName };
            }

            return null;
        }

        public void UpdateEmployee(int employeeId, Employee employee) 
        {
            if (employeeId != employee.Id) return;

            var employeeToUpdate = _context.Employees.Include(e => e.Credentials).FirstOrDefault(e => e.Id == employeeId);

            if (employeeToUpdate != null)
            {
                employeeToUpdate.Name = employee.Name;
                employeeToUpdate.Department = employee.Department;
                employeeToUpdate.DateOfBirth = employee.DateOfBirth;
                employeeToUpdate.Email = employee.Email;
                employeeToUpdate.PhoneNumber = employee.PhoneNumber;
                employeeToUpdate.Position = employee.Position;
                employeeToUpdate.AvatarFileName = employee.AvatarFileName;

                _context.SaveChanges();
            }

        }

        public void DeleteEmployee(int employeeId)
        {
            var employee = _context.Employees.Include(e => e.Credentials).Include(s => s.Statistics).FirstOrDefault(e => e.Id == employeeId);
            var penalties = _context.Penalties.FirstOrDefault(p => p.EmployeeId == employeeId);
            var vacations = _context.Vacations.FirstOrDefault(v => v.EmployeeId == employeeId);

            if (employee != null)
            {
                if (employee.Credentials != null)
                {
                    _context.Credentials.Remove(employee.Credentials);
                }

                if (employee.Statistics != null)
                {
                    _context.Statistics.Remove(employee.Statistics);
                }

                if (penalties != null)
                {
                    _context.Remove(penalties);
                }

                if (vacations != null)
                {
                    _context.Remove(vacations);
                }
                
                _context.Employees.Remove(employee);

                _context.SaveChanges();
            }
        }
        public Dictionary<string, int> GetDepartmentSalaryStatistics()
        {
            return _context.Employees
                           .Where(e => e.Statistics != null)
                           .GroupBy(e => e.Department)
                           .Select(g => new { Department = g.Key, TotalSalary = g.Sum(e => e.Statistics!.Salary) })
                           .ToDictionary(x => x.Department, x => x.TotalSalary);
        }
    }
}
