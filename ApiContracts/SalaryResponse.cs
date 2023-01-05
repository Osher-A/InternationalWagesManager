namespace ApiContracts
{
    public record SalaryResponse(
        int Id,
        int EmployeeId,
        DateTime Month,
        decimal Wage,
        decimal Expeneses,
        decimal GrossPay,
        decimal NetPay,
        decimal WageRate,
        decimal ExpensesRate);
}
