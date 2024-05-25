namespace Employee_Managment.Models
{
    public class Penalty
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Reason { get; set; } = string.Empty;
        public decimal Amount { get; set; }

        // Внешний ключ к Employee
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
    }
}
