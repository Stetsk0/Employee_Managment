using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Employee_Managment.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Department { get; set; } = string.Empty;

        public string? Position { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; } = DateTime.Now;

        [Phone]
        [Display(Name = "Phone Number")]
        [StringLength(15, ErrorMessage = "Phone number cannot be longer than 15 characters.")]
        public string? PhoneNumber { get; set; } = string.Empty;

        [EmailAddress]
        [Display(Name = "Email")]
        public string? Email { get; set; } = string.Empty;

        public Credentials? Credentials { get; set; }

        public Statistics? Statistics { get; set; }
        public string? AvatarFileName { get; set; } = string.Empty;
    }
}
