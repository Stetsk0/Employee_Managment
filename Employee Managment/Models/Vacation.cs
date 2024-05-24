using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employee_Managment.Models
{
    public class Vacation
    {
        public int Id { get; set; }

        [Display(Name = "Vacation Days")]
        public int VacationDays { get; set; }

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