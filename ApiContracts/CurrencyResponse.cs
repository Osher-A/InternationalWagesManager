using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiContracts
{
    public record CurrencyResponse(
        int Id,
        string Name,
        string Description)
    {
    }
}
