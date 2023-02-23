using InternationalWagesManager.Models;
using Microsoft.EntityFrameworkCore;

namespace InternationalWagesManager.DAL
{
    public class SalaryComponentsRepository : ISalaryComponentsRepository
    {
        private MyDbContext _db;
        public SalaryComponentsRepository(MyDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task AddSalaryComponentsAsync(SalaryComponents newSC)
        {
            _db.SalariesComponents.Add(newSC);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateSalaryComponentsAsync(SalaryComponents SC)
        {
            _db.SalariesComponents.Update(SC);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteSalaryComponentsAsync(SalaryComponents SC)
        {
            _db.SalariesComponents.Remove(SC);
            await _db.SaveChangesAsync();
        }

        public async Task<List<SalaryComponents>> GetEmployeeSalaryComponentsAsync(int employeeId)
        {
            var result = await _db.SalariesComponents
                .Where(sc => sc.EmployeeId == employeeId).ToListAsync();

            return result;
        }

    }
}
