using InternationalWagesManager.Models;

namespace InternationalWagesManager.DAL
{
    public class SalaryComponentsRepository : ISalaryComponentsRepository
    {
        private MyDbContext _db;
        public SalaryComponentsRepository(MyDbContext dbContext)
        {
            _db = dbContext;
        }

        public void AddSalaryComponents(SalaryComponents newSC)
        {
            _db.SalariesComponents.Add(newSC);
            _db.SaveChanges();
        }

        public void UpdateSalaryComponents(SalaryComponents SC)
        {
            _db.SalariesComponents.Update(SC);
            _db.SaveChanges();
        }

        public void DeleteSalaryComponents(SalaryComponents SC)
        {
            _db.SalariesComponents.Remove(SC);
            _db.SaveChanges();
        }

        public List<SalaryComponents> GetEmployeeSalaryComponents(int employeeId)
        {
            var result = _db.SalariesComponents
                .Where(sc => sc.EmployeeId == employeeId).ToList();

            return result;
        }

    }
}
