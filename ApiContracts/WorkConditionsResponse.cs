namespace ApiContracts
{
    public record WorkConditionsResponse(
        int Id,
        int EmployeeId,
        DateTime Date,
        float PayRate,
        CurrencyResponse WageCurrency,
        CurrencyResponse ExpensesCurrency,
        CurrencyResponse PayCurrency,
        decimal Deductions);
}
