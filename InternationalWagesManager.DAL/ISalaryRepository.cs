using InternationalWagesManager.Models;

namespace InternationalWagesManager.DAL
{
    public interface ISalaryRepository : IBaseRepository<Salary>
    {
        Task<List<Salary>> GetAllEmployeeSalariesAsync(int employeeId);
    }
}