using System.ComponentModel.DataAnnotations;

namespace Employee_Managment.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Вкажіть прізвище та ім'я.")]
        [Display(Name = "Прізвище та ім'я")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Відділ")]
        public string? Department { get; set; } = string.Empty;

        [Display(Name = "Посада")]
        public string? Position { get; set; } = string.Empty;

        [Display(Name = "Дата народження")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Вкажіть номер телефону.")]
        [Phone(ErrorMessage = "Некоректний номер телефону.")]
        [Display(Name = "Номер телефону")]
        [StringLength(13)]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Вкажіть електронну пошту.")]
        [EmailAddress(ErrorMessage = "Некоректна пошта.")]
        [Display(Name = "Електронна пошта")]
        public string Email { get; set; } = string.Empty;

        public Credentials? Credentials { get; set; }

        public Statistics? Statistics { get; set; }

        public string? AvatarFileName { get; set; } = string.Empty;
    }
}
