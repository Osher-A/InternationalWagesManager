using InternationalWagesManager.Models;
using System;
using System.Linq;

namespace InternationalWagesManager.DAL
{
    public interface ICurrenciesRepository
    {
        List<Currency> GetAllCurrencies();
    }
}
