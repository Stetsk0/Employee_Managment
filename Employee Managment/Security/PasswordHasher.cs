using System.Security.Cryptography;
using System.Text;

namespace Employee_Managment.Security
{
    public class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var x = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                return x;
            }
        }

        public static bool VerifyPassword(string enteredPassword, string storedHash)
        {
            var enteredPasswordHash = HashPassword(enteredPassword);
            return enteredPasswordHash == storedHash;
        }
    }
}
