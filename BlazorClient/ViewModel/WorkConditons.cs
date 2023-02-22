using System.ComponentModel.DataAnnotations;

namespace BlazorClient.ViewModel
{
    public class WorkConditions
    {
        public int Id { get; set; }
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }
        public DateTime? Date { get; set; }
        [Required(ErrorMessage = "Field Required")]
        public float? PayRate { get; set; }
        public Currency WageCurrency { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Field Required")]
        public int WageCurrencyId { get; set; }
        public Currency ExpensesCurrency { get; set; }
        [Required(ErrorMessage = "Field Required")]
        public int? ExpensesCurrencyId { get; set; }
        public Currency PayCurrency { get; set; }
        [Required(ErrorMessage = "Field Required")]
        public int? PayCurrencyId { get; set; }
        public float? Deductions { get; set; }
    }
}
