using InternationalWagesManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternationalWagesManager.DAL
{
    public class SComponentsRepository : ISComponentsRepository
    {
        private MyDbContext _db;
        public SComponentsRepository(MyDbContext dbContext)
        {
            _db = dbContext;
        }

        public void AddScomponent(SalaryComponents newSC)
        {
            _db.SalariesComponents.Add(newSC);
            _db.SaveChanges();
        }

        public void UpdateSComponent(SalaryComponents SC)
        {
            _db.SalariesComponents.Update(SC);
            _db.SaveChanges();
        }

        public void DeleteSComponent(SalaryComponents SC)
        {
            _db.SalariesComponents.Remove(SC);
            _db.SaveChanges();
        }

        public SalaryComponents GetSComponent(SalaryComponents SC)
        {
            var result = _db.SalariesComponents
                .FirstOrDefault(sc => sc.EmployeeId == SC.EmployeeId && sc.Month.Month == SC.Month.Month);

            return result ?? new();
        }

    }
}
