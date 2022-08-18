using InternationalWagesManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternationalWagesManager.DAL
{
    public class CurrenciesRepository : ICurrenciesRepository
    {
        private MyDbContext _db;

        public CurrenciesRepository(MyDbContext db)
        {
            _db = db;
        }

        public async Task<List<Currency>> GetAllCurrenciesAsync()
        {
            return await _db.Currencies.ToListAsync();
        }

        public void AddCurrency(Currency currency)
        {
            _db.Currencies.Add(currency);
            _db.SaveChanges();
        }

        public void UpdateCurrency(Currency currency)
        {
            _db.Currencies.Update(currency);
            _db.SaveChanges();
        }

        public void DeleteCurrency(Currency currency)
        {
            _db.Currencies.Remove(currency);
            _db.SaveChanges();
        }
    }
}
