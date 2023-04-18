using InternationalWagesManager.Models;
using Microsoft.EntityFrameworkCore;

namespace InternationalWagesManager.DAL
{
    public class WConditionsRepository : BaseRepository<WorkConditions>, IWConditionsRepository
    {
        private MyDbContext _db;

        public WConditionsRepository(MyDbContext db) : base(db)
        {
            _db = db;
        }
        public new async Task<WorkConditions> GetByIdAsync(int workConditionId)
        {
            return await _db.WorkConditions
                .Include(wc => wc.PayCurrency)
                .Include(wc => wc.WageCurrency)
                .Include(wc => wc.ExpensesCurrency)
                .FirstOrDefaultAsync(wc => wc.Id == workConditionId) ?? new WorkConditions();
        }

        public async Task<List<WorkConditions>> GetAllEmployeesWCAsync(int employeeId)
        {
            return await _db.WorkConditions
                .Include(wc => wc.PayCurrency)
                .Include(wc => wc.WageCurrency)
                .Include(wc => wc.ExpensesCurrency)
                .Where(wc => wc.EmployeeId == employeeId)
                .OrderByDescending(wc => wc.Date)
                .ToListAsync() ?? new();
        }

        public async Task DeleteByIdAsync(int id)
        {
            _db.WorkConditions.Remove(_db.WorkConditions.Find(id));
            await _db.SaveChangesAsync();
        }
    }
}

