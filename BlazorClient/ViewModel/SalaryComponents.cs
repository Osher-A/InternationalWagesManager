﻿using System.ComponentModel.DataAnnotations;

namespace BlazorClient.ViewModel
{
    public class SalaryComponents
    {
        public int Id { get; set; }
        public Employee Employee { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public DateTime? Date { get; set; }
        [Required]
        public float? TotalHours { get; set; }
        public float? BonusHours { get; set; }
        public float? BonusWage { get; set; }
        public float? Expenses { get; set; }

    }
}
