using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
