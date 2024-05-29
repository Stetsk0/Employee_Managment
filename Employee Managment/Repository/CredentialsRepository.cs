using Employee_Managment.Data;
using Employee_Managment.Models;
using Employee_Managment.Security;

namespace Employee_Managment.Repository
{
    public class CredentialsRepository
    {
        private readonly ApplicationDbContext _context;

        public CredentialsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void UpdateEmployeeCredentials(int employeeId, Credentials credentials)
        {
            GetCredentialsById(employeeId);

            if (employeeId != credentials.Id) return;

            var credentialsToUpdate = _context.Credentials.FirstOrDefault(c => c.Id == employeeId);

            if (credentialsToUpdate != null)
            {
                credentialsToUpdate.UserName = credentials.UserName;
                    
                if (credentialsToUpdate.Password != credentials.Password)
                {
                    credentialsToUpdate.Password = PasswordHasher.HashPassword(credentials.Password);
                }

                _context.SaveChanges();
            }
        }

        public Credentials? GetCredentialsById(int employeeId)
        {
            var credentials = _context.Credentials.FirstOrDefault(c => c.Id == employeeId);
            if (credentials != null)
            {
                return new Credentials { Id = employeeId, Password = credentials.Password, UserName = credentials.UserName };
            }

            return null;
        }
    }
}
