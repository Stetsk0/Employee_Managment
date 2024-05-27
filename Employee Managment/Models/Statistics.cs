using System.ComponentModel.DataAnnotations;

namespace Employee_Managment.Models
{
    public class Statistics
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Вкажіть заробітну плату.")]
        [Display(Name = "Зарплата")]
        [Range(1, int.MaxValue, ErrorMessage = "Зарплата повинна бути більше нуля.")] 
        public int Salary { get; set; }
        
        [Required(ErrorMessage = "Вкажіть QA.")]
        [Display(Name = "QA")]
        [Range(1, 100, ErrorMessage = "Значення має бути в діапазоні від 1 до 100.")]
        public int QA { get; set; }

        [Display(Name = "Бонуси")]
        [Range(0, 100, ErrorMessage = "Значення має бути в діапазоні від 0 до 100.")]
        public int Bonus { get; set; }

        [Display(Name = "Кількість відпрацьованих годин")]
        [Required(ErrorMessage = "Вкажіть кількість відпрацьованих годин.")]
        public int HoursWorked { get; set; }

        [Display(Name = "Кількість понаднормових годин")]
        public int OvertimeHours { get; set; }

        [Display(Name = "Кількість виконаних завдань")]
        public int CompletedTasks { get; set; }

        [Display(Name = "Середній час виконання завдання")]
        public int AverageTaskCompletionTime { get; set; }

        [Display(Name = "Відсоток вчасно виконаних завдань ")]
        [Range(0, 100, ErrorMessage = "Значення має бути в діапазоні від 0 до 100.")]
        public double OnTimeTaskCompletionPercentage { get; set; }
    }
}