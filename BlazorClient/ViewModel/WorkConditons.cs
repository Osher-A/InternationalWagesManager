﻿using System.ComponentModel.DataAnnotations;

namespace BlazorClient.ViewModel
{
    public class WorkConditons
    {
        public int Id { get; set; }
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }
        public DateTime? Date { get; set; }
        [Required]
        public float? PayRate { get; set; }
        public Currency WageCurrency { get; set; }
        [Required]
        public int WageCurrencyId { get; set; }
        public Currency ExpensesCurrency { get; set; }
        [Required]
        public int? ExpensesCurrencyId { get; set; }
        public Currency PayCurrency { get; set; }
        [Required]
        public int? PayCurrencyId { get; set; }
        public decimal? Deductions { get; set; }
    }
}