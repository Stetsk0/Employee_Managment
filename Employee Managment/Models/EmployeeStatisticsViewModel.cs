﻿namespace Employee_Managment.Models
{
    public class EmployeeStatisticsViewModel
    {
        public Statistics EmployeeStatistics { get; set; }
        public Statistics AverageStatistics { get; set; }
        public Dictionary<string, int> DepartmentSalaryStatistics { get; set; }
    }
}
