using Microsoft.AspNetCore.Mvc;

namespace Employee_Managment.Models
{
    public class Login
    {
        [BindProperty]
        public Credentials Credential { get; set; } = new Credentials();
    }
}
