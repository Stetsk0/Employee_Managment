namespace Employee_Managment.Security
{
    public class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool VerifyPassword(string enteredPassword, string storedHash)
        {
            if (enteredPassword == null) { return false; }
            return BCrypt.Net.BCrypt.Verify(enteredPassword, storedHash);
        }
    }
}
