using InternationalWagesManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternationalWagesManager.DAL
{
    public class WConditionsRepository : IWConditionsRepository
    {
        private MyDbContext _db;

        public WConditionsRepository(MyDbContext db)
        {
            _db = db;
        }

        public async Task<int> AddWorkConditions(WorkConditions workConditions)
        {
            _db.WorkConditions.Add(workConditions);
            _db.SaveChangesAsync();
            return  workConditions.Id;
        }

        public void UpdateWorkConditions(WorkConditions workConditions)
        {
            _db.WorkConditions.Update(workConditions);
            _db.SaveChanges();
        }

        public void DeleteWorkConditions(int id)
        {
            _db.WorkConditions.Remove(_db.WorkConditions.Find(id));
            _db.SaveChanges();
        }

        public async Task<WorkConditions> GetWorkConditions(int employeeId, DateTime date)
        {
            var result = _db.WorkConditions
                 .Include(wc => wc.PayCurrency)
                .Include(wc => wc.WageCurrency)
                .Include(wc => wc.ExpensesCurrency)
                .FirstOrDefaultAsync(wc => wc.EmployeeId == employeeId
                && wc.Date.Year == date.Year
                && wc.Date.Month == date.Month);

            if(result == null)
                result = _db.WorkConditions
                 .Include(wc => wc.PayCurrency)
                .Include(wc => wc.WageCurrency)
                .Include(wc => wc.ExpensesCurrency).FirstOrDefaultAsync(wc => wc.EmployeeId == employeeId
                && wc.Date.Year == date.Year);

            return await result ?? new();
        }

        public async Task<List<WorkConditions>> GetAllWorkConditions(int employeeId)
        {
            return await _db.WorkConditions
                .Include(wc => wc.PayCurrency)
                .Include(wc => wc.WageCurrency)
                .Include(wc => wc.ExpensesCurrency)
                .Where(wc => wc.EmployeeId == employeeId)
                .ToListAsync() ?? new();
        }

        public async Task<WorkConditions> GetWorkConditions(int workConditionId)
        {
            var workConditions = _db.WorkConditions
                 .Include(wc => wc.PayCurrency)
                .Include(wc => wc.WageCurrency)
                .Include(wc => wc.ExpensesCurrency)
                .FirstOrDefaultAsync(wc => wc.Id == workConditionId);
            if (workConditions != null)
                return await workConditions;

            else
                return  new WorkConditions();
        }
    }
}
