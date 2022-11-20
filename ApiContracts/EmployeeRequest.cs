using System.ComponentModel.DataAnnotations;

namespace ApiContracts
{
        public record EmployeeRequest(
            [Required]
            string FirstName,
            [Required]
            string LastName,
            DateTime DOB,
            string Phone,
            [Required]
            string Email);
}