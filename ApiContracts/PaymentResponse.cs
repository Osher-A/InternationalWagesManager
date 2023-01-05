namespace ApiContracts
{
    public record PaymentResponse(
        int Id,
        int EmployeeId,
        DateTime Date,
        decimal Amount,
        string Description);
}
