using InternationalWagesManager.Models;

namespace InternationalWagesManager.DAL
{
    public interface ISalaryComponentsRepository
    {
        Task AddSalaryComponentsAsync(SalaryComponents newSC);
        Task DeleteSalaryComponentsAsync(SalaryComponents SC);
        Task<List<SalaryComponents>> GetEmployeeSalaryComponentsAsync(int employeeId);
        Task<SalaryComponents> GetSalaryComponentsAsync(int id);
        Task UpdateSalaryComponentsAsync(SalaryComponents SC);
    }
}