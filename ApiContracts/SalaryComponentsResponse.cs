namespace ApiContracts
{
    public record SalaryComponentsResponse(
        int Id,
        int EmployeeId,
        DateTime Date,
        float TotalHours,
        float BonusHours,
        float BonusWage,
        float Expenses);
}