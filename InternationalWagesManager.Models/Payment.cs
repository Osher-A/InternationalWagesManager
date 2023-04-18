namespace InternationalWagesManager.Models
{
    public class Payment : BaseClass
    {
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
    }
}
