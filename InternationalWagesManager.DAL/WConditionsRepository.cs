using InternationalWagesManager.Models;
using Microsoft.EntityFrameworkCore;

namespace InternationalWagesManager.DAL
{
    public class WConditionsRepository : IWConditionsRepository
    {
        private MyDbContext _db;

        public WConditionsRepository(MyDbContext db)
        {
            _db = db;
        }
        public async Task<WorkConditions> GetWorkConditionsAsync(int workConditionId)
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

        public async Task<int> AddWorkConditionsAsync(WorkConditions workConditions)
        {
            await _db.WorkConditions.AddAsync(workConditions);
            await _db.SaveChangesAsync();
            return workConditions.Id;
        }

        public async Task UpdateWorkConditionsAsync(WorkConditions workConditions)
        {
            _db.Update<WorkConditions>(workConditions);


            await _db.SaveChangesAsync();
            _db.Entry(workConditions).State = EntityState.Detached;
        }

        public async Task DeleteWorkConditionsAsync(int id)
        {
            _db.WorkConditions.Remove(_db.WorkConditions.Find(id));
            await _db.SaveChangesAsync();
        }
    }
}

