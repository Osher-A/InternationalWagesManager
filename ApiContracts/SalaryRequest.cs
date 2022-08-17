using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
