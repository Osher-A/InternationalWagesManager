namespace InternationalWagesManager.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public List<WorkConditions> WorkConditions { get; set; } = new List<WorkConditions>();
    }
}