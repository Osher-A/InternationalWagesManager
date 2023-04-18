using InternationalWagesManager.Models;
using Microsoft.EntityFrameworkCore;

namespace InternationalWagesManager.DAL
{
    public class PaymentsRepository : BaseRepository<Payment>, IPaymentsRepository
    {
        private MyDbContext _db;

        public PaymentsRepository(MyDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<List<Payment>> GetAllEmployeePaymentsAsync(int employeeId)
        {
            var result = await _db.Payments.Where(p => p.EmployeeId == employeeId).ToListAsync();
            return result;
        }
    }
}
