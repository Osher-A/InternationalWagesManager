using System.ComponentModel.DataAnnotations;

namespace InternationalWagesManager.DTO
{
    public class WorkConditions
    {
        public int Id { get; set; }
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }
        [Required]
        public DateTime? Date { get; set; }
        [Required]
        public float? PayRate { get; set; }
        public Currency WageCurrency { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please select currency for wages")]
        public int WageCurrencyId { get; set; }
        public Currency ExpensesCurrency { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a currency for expenses")]
        public int? ExpensesCurrencyId { get; set; }
        public Currency PayCurrency { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a currency for payments")]
        public int? PayCurrencyId { get; set; }
        public decimal? Deductions { get; set; }

    }
}
