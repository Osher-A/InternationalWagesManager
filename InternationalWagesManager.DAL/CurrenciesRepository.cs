using InternationalWagesManager.Models;
using Microsoft.EntityFrameworkCore;

namespace InternationalWagesManager.DAL
{
    public class CurrenciesRepository : BaseRepository<Currency>, ICurrenciesRepository
    {
        private MyDbContext _db;

        public CurrenciesRepository(MyDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
