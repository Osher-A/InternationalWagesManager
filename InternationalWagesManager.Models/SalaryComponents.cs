namespace InternationalWagesManager.Models
{
    public class SalaryComponents : BaseClass
    {
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }
        public DateTime Date { get; set; }
        public float TotalHours { get; set; }
        public float? BonusHours { get; set; }
        public float? BonusWage { get; set; }
        public float? Expenses { get; set; }

    }
}
