using InternationalWagesManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternationalWagesManager.DAL
{
    public class SalaryRepository : ISalaryRepository
    {
        private MyDbContext _db;

        public SalaryRepository(MyDbContext db)
        {
            _db = db;
        }

        public void AddSalary(Salary salary)
        {
            _db.Salaries.Add(salary);
            _db.SaveChanges();
        }

        public void UpdateSalary(Salary salary)
        {
            _db.Salaries.Update(salary);
            _db.SaveChanges();
        }

        public void DeleteSalary(Salary salary)
        {
            _db.Salaries.Remove(salary);
            _db.SaveChanges();
        }

        public Salary GetSalary(int salaryId)
        {
            var salary = _db.Salaries.Find(salaryId);
            return salary ?? new();
        }

        public List<Salary> GetAllSalaries(int employeeId)
        {
            return _db.Salaries.Where(s => s.EmployeeId == employeeId).ToList() ?? new();
        }
    }
}
