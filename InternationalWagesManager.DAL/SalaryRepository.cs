using InternationalWagesManager.Models;
using Microsoft.EntityFrameworkCore;

namespace InternationalWagesManager.DAL
{
    public class SalaryRepository : ISalaryRepository
    {
        private MyDbContext _db;

        public SalaryRepository(MyDbContext db)
        {
            _db = db;
        }

        public async Task<int> AddSalaryAsync(Salary salary)
        {
            await _db.Salaries.AddAsync(salary);
            var newId = await _db.SaveChangesAsync();
            return newId;
        }

        public async Task DeleteSalaryAsync(Salary salary)
        {
            _db.Salaries.Remove(salary);
            await _db.SaveChangesAsync();
        }

        public async Task<Salary> GetSalaryAsync(int salaryId)
        {
            var salary = await _db.Salaries.FindAsync(salaryId);
            return salary!;
        }

        public async Task<List<Salary>> GetAllSalariesAsync(int employeeId)
        {
            return await _db.Salaries.Where(s => s.EmployeeId == employeeId).ToListAsync()!;
        }
    }
}
