using System.ComponentModel.DataAnnotations;

namespace ApiContracts
{
    public record SalaryComponentsRequest(
        [Required]
        [Range(1, int.MaxValue)]
        int EmployeeId,
        [Required]
        DateTime Date,
        [Required]
        [Range (1, 744)]
        float? TotalHours,
        float? BonusHours,
        float? BonusWage,
        float? Expenses);
}
