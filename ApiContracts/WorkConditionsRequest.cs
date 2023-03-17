using System.ComponentModel.DataAnnotations;

namespace ApiContracts
{
    public record WorkConditionsRequest(
        int Id,
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Employee's id needs to be set!")]
        int EmployeeId,
        [Required]
        DateTime Date,
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Pay rate needs to be set")]
        float PayRate,
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Wage currency id needs to be set")]
        int WageCurrencyId,
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Expenses currency id needs to be set")]
        int ExpensesCurrencyId,
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Pay currency id needs to be set")]
        int PayCurrencyId,
        decimal Deductions);

}
