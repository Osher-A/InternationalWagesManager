using InternationalWagesManager.Models;
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

        public List<Currency> GetAllCurrencies()
        {
            return _db.Currencies.ToList();
        }
    }
}
