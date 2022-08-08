using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
