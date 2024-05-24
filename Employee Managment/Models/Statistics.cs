using System.ComponentModel.DataAnnotations;

namespace Employee_Managment.Models
{
    public class Statistics
    {
        public int Id { get; set; }
        
        [Required]
        [Display(Name = "Salary")]
        public string? Salary { get; set; } = string.Empty;
        
        [Required]
        [Display(Name = "QA")]
        public string? QA { get; set; } = string.Empty;

        [Display(Name = "Bonus")]
        public string? Bonus { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Worked hours")]
        public int WorkedHours { get; set; }

        public Vacation? Vacation { get; set; }
        //public List<string> TrainingCourses { get; set; } = new List<string>();
    }
}
