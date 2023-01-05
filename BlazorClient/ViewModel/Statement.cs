namespace BlazorClient.ViewModel
{
    public class Statement
    {
        public DateTime Date { get; set; }
        public decimal? SalaryPayable { get; set; }
        public decimal? Payment { get; set; }
        public decimal Balance { get; set; }
    }
}
