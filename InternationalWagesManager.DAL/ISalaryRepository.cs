using InternationalWagesManager.Models;

namespace InternationalWagesManager.DAL
{
    public interface ISalaryRepository
    {
        Task<int> AddSalaryAsync(Salary salary);
        Task DeleteSalaryAsync(Salary salary);
        Task<List<Salary>> GetAllSalariesAsync(int employeeId);
        Task<Salary> GetSalaryAsync(int salaryId);
    }
}