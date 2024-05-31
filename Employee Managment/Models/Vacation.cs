using System.ComponentModel.DataAnnotations;

namespace Employee_Managment.Models
{
    public class Vacation
    {
        public int Id { get; set; }

        [Display(Name = "Початок відпуски")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Кінець відпуски")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Опис")]
        public string? Description { get; set; }

        public Employee? Employee { get; set; }
        public int EmployeeId { get; set; }
    }
}