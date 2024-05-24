using System.ComponentModel.DataAnnotations;

namespace Employee_Managment.Models
{
    public class Vacation
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Vacation Days")]
        public int VacationDays { get; set; }

        [Required]
        [Display(Name = "Vacation Type")]
        public VacationType VacationType { get; set; }
    }
    public enum VacationType
    {
        Holiday,
        DayOff,
        Vacation,
        SickLeave
    }
}