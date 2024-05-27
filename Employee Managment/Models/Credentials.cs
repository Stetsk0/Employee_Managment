using System.ComponentModel.DataAnnotations;

namespace Employee_Managment.Models
{
    public class Credentials
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Будь ласка, введіть логін.")]
        [Display(Name = "Логін")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Будь ласка, введіть пароль.")]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}