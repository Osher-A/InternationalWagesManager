using InternationalWagesManager.Models;

namespace InternationalWagesManager.DAL
{
    public interface IPaymentsRepository : IBaseRepository<Payment>
    {
        Task<List<Payment>> GetAllEmployeePaymentsAsync(int employeeId);
    }
}