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

        public void AddWorkConditions(WorkConditions workConditions)
        {
            _db.WorkConditions.Add(workConditions);
            _db.SaveChanges();
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

        public WorkConditions GetWorkConditions(int employeeId, DateTime date)
        {
            var result = _db.WorkConditions
                 .Include(wc => wc.PayCurrency)
                .Include(wc => wc.WageCurrency)
                .Include(wc => wc.ExpensesCurrency)
                .FirstOrDefault(wc => wc.EmployeeId == employeeId
                && wc.Date.Year == date.Year
                && wc.Date.Month == date.Month);

            if(result == null)
                result = _db.WorkConditions
                 .Include(wc => wc.PayCurrency)
                .Include(wc => wc.WageCurrency)
                .Include(wc => wc.ExpensesCurrency).FirstOrDefault(wc => wc.EmployeeId == employeeId
                && wc.Date.Year == date.Year);

            return result ?? new();
        }

        public List<WorkConditions> GetAllWorkConditions(int employeeId)
        {
            return _db.WorkConditions
                .Include(wc => wc.PayCurrency)
                .Include(wc => wc.WageCurrency)
                .Include(wc => wc.ExpensesCurrency)
                .Where(wc => wc.EmployeeId == employeeId)
                .ToList() ?? new();
        }

        public WorkConditions GetWorkConditions(int workConditionId)
        {
            var workConditions = _db.WorkConditions
                 .Include(wc => wc.PayCurrency)
                .Include(wc => wc.WageCurrency)
                .Include(wc => wc.ExpensesCurrency)
                .FirstOrDefault(wc => wc.Id == workConditionId);
            if (workConditions != null)
                return workConditions;

            else
                return new WorkConditions();
        }
    }
}
