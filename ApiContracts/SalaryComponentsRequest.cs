namespace ApiContracts
{
    public record SalaryComponentsRequest(
        int EmployeeId,
        DateTime Date,
        float TotalHours,
        float BonusHours,
        float BonusWage,
        float Expenses);
}
