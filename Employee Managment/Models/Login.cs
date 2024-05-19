using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Employee_Managment.Models
{
    public class Login
    {
        [BindProperty]
        public Credentials Credential { get; set; } = new Credentials();
    }
}
