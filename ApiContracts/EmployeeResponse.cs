namespace ApiContracts
{
    public record EmployeeResponse(
        int Id,
        string FirstName,
        string LastName,
        DateTime DOB,
        string Phone,
        string Email);

}
