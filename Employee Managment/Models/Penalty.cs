using System.ComponentModel.DataAnnotations;

namespace Employee_Managment.Models
{
    public class Penalty
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Вкажіть дату штрафу.")]
        [Display(Name = "Дата штрафу")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Вкажіть причину штрафу.")]
        [Display(Name = "Причина штрафу")]
        public string? Reason { get; set; }

        [Display(Name = "Сума штрафу")]
        public decimal Amount { get; set; }

        public int EmployeeId { get; set; }

        public Employee? Employee { get; set; }
    }
}
