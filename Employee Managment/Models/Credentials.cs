using System.ComponentModel.DataAnnotations;

namespace Employee_Managment.Models
{
    public class Credentials
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the user name.")]
        [Display(Name = "User Name")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter the password.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
