namespace InternationalWagesManager.Models
{
    public class Salary
    {
        public int Id { get; set; }
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }
        public DateTime Month { get; set; }
        public decimal Wage { get; set; }
        public decimal Expenses { get; set; }
        public decimal GrossPay { get; set; }
        public decimal NetPay { get; set; }
        public decimal WageRate { get; set; }
        public decimal ExpensesRate { get; set; }
    }
}
