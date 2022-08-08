using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
