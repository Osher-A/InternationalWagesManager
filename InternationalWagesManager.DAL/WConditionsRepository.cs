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
                .FirstOrDefaultAsync(wc => wc.Id == workConditionId) ?? new WorkConditions() ;
        }

        public async Task<WorkConditions> GetEmployeesWCToDateAsync(int employeeId, DateTime date)
        {
            var result = _db.WorkConditions
                 .Include(wc => wc.PayCurrency)
                .Include(wc => wc.WageCurrency)
                .Include(wc => wc.ExpensesCurrency)
                .FirstOrDefaultAsync(wc => wc.EmployeeId == employeeId
                && wc.Date.Year == date.Year
                && wc.Date.Month == date.Month);

            if (result == null)
                result = _db.WorkConditions
                 .Include(wc => wc.PayCurrency)
                .Include(wc => wc.WageCurrency)
                .Include(wc => wc.ExpensesCurrency).FirstOrDefaultAsync(wc => wc.EmployeeId == employeeId
                && wc.Date.Year == date.Year);

            return await result ?? new WorkConditions();
        }

        public async Task<List<WorkConditions>> GetAllEmployeesWCAsync(int employeeId)
        {
            return await _db.WorkConditions
                .Include(wc => wc.PayCurrency)
                .Include(wc => wc.WageCurrency)
                .Include(wc => wc.ExpensesCurrency)
                .Where(wc => wc.EmployeeId == employeeId)
                .ToListAsync() ?? new();
        }

        public async Task<int> AddWorkConditions(WorkConditions workConditions)
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

        public void DeleteWorkConditions(int id)
        {
            _db.WorkConditions.Remove(_db.WorkConditions.Find(id));
            _db.SaveChanges();
        }
    }
}

