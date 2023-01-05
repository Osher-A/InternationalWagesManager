namespace ApiContracts
{
    public record SalaryRequest(
        int EmployeeId,
        DateTime Month,
        decimal Wage,
        decimal Expenses,
        decimal GrossPay,
        decimal NetPay,
        decimal WageRate,
        decimal ExpensesRate);
}
