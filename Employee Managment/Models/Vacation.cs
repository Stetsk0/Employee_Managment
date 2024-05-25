using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employee_Managment.Models
{
    public class Vacation
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Description { get; set; }

        // Внешний ключ к Employee
        public Employee? Employee { get; set; }
        public int EmployeeId { get; set; }

    }
    public enum VacationType
    {
        Holiday,
        DayOff,
        Vacation,
        SickLeave
    }
}