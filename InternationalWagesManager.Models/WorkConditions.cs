namespace InternationalWagesManager.Models
{
    public class WorkConditions
    {
        public int Id { get; set; }
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }
        public DateTime Date { get; set; }
        public float PayRate { get; set; }
        public Currency WageCurrency { get; set; }
        public int WageCurrencyId { get; set; }
        public Currency ExpensesCurrency { get; set; }
        public int? ExpensesCurrencyId { get; set; }
        public Currency PayCurrency { get; set; }
        public int? PayCurrencyId { get; set; }
        public decimal Deductions { get; set; }

    }
}
