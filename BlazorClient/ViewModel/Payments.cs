namespace BlazorClient.ViewModel
{
    public class Payment
    {
        public int Id { get; set; }
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }
        public DateTime? Date { get; set; }
        public decimal? Amount { get; set; }
        public string Description { get; set; }
    }
}
