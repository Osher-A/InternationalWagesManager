using InternationalWagesManager.Models;
using Microsoft.EntityFrameworkCore;

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
            return await _db.Currencies.AsNoTracking().ToListAsync();
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
            _db.Entry(currency).State = EntityState.Detached;
        }

        public void DeleteCurrency(Currency currency)
        {
            _db.Currencies.Remove(currency);
            _db.SaveChanges();
        }
    }
}
