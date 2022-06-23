using InternationalWagesManager.Models;
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

        public void DeleteWorkConditions(WorkConditions workConditions)
        {
            _db.WorkConditions.Remove(workConditions);
            _db.SaveChanges();
        }

        public WorkConditions GetWorkConditions(int employeeId, DateTime date)
        {
            var result = _db.WorkConditions
                .FirstOrDefault(wc => wc.EmployeeId == employeeId
                && wc.Date.Year == date.Year
                && wc.Date.Month == date.Month);

            if(result == null)
                result = _db.WorkConditions.FirstOrDefault(wc => wc.EmployeeId == employeeId
                && wc.Date.Year == date.Year);

            return result ?? new();
        }

        public List<WorkConditions> GetAllWorkConditions(int employeeId)
        {
            return _db.WorkConditions.Where(wc => wc.EmployeeId == employeeId).ToList() ?? new();
        }
    }
}
