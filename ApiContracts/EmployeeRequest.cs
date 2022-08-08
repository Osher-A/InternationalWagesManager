namespace ApiContracts
{
        public record EmployeeRequest(
            string FirstName,
            string LastName,
            DateTime DOB,
            string Phone,
            string Email);
}