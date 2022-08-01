using System.ComponentModel.DataAnnotations;

namespace InternationalWagesManager.DTO
{
    public class Employee 
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DOB { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string FullName
        {
            get
            {
                if (!String.IsNullOrWhiteSpace(FirstName) && !String.IsNullOrWhiteSpace(LastName))
                    return FirstName + " " + LastName;
                else
                    return null;
            }
        }
    }
}