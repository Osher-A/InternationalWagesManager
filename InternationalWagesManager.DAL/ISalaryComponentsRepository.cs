using InternationalWagesManager.Models;

namespace InternationalWagesManager.DAL
{
    public interface ISalaryComponentsRepository : IBaseRepository<SalaryComponents>
    {
        Task<List<SalaryComponents>> GetEmployeeSalaryComponentsAsync(int employeeId);
    }
}