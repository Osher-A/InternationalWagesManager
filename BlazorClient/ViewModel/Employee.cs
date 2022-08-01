using System.ComponentModel.DataAnnotations;

namespace BlazorClient.ViewModel
{
    public class Employee
    {
        public int Id { get; set; }
        [Required (ErrorMessage = "This is a required field!")]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime? DOB { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
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
