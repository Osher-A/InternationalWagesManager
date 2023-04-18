using InternationalWagesManager.Models;
using Microsoft.EntityFrameworkCore;

namespace InternationalWagesManager.DAL
{
    public class SalaryComponentsRepository : BaseRepository<SalaryComponents>, ISalaryComponentsRepository
    {
        private MyDbContext _db;
        public SalaryComponentsRepository(MyDbContext dbContext) : base(dbContext)
        {
            _db = dbContext;
        }

        public async Task<List<SalaryComponents>> GetEmployeeSalaryComponentsAsync(int employeeId)
        {
            var result = await _db.SalariesComponents
                .Where(sc => sc.EmployeeId == employeeId).ToListAsync();
            return result;
        }

    }
}
