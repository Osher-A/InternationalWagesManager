using InternationalWagesManager.Models;
using System;
using System.Linq;

namespace InternationalWagesManager.DAL
{
    public interface ICurrenciesRepository
    {
        void AddCurrency(Currency currency);
        void DeleteCurrency(Currency currency);
        Task <List<Currency>> GetAllCurrenciesAsync();
        void UpdateCurrency(Currency currency);
    }
}
