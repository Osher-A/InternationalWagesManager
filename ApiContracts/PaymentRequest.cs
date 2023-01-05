namespace ApiContracts
{
    public record PaymentRequest(
       int EmployeeId,
       DateTime Date,
       decimal Amount,
       string Description);
}
