using InternationalWagesManager.Models;
using Microsoft.EntityFrameworkCore;

namespace InternationalWagesManager.DAL
{
    public class SalaryRepository : BaseRepository<Salary>, ISalaryRepository
    {
        private MyDbContext _db;

        public SalaryRepository(MyDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<List<Salary>> GetAllEmployeeSalariesAsync(int employeeId)
        {
            return await _db.Salaries.Where(s => s.EmployeeId == employeeId).ToListAsync()!;
        }
    }
}
