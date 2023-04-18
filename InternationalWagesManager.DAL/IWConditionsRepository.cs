using InternationalWagesManager.Models;

namespace InternationalWagesManager.DAL
{
    public interface IWConditionsRepository : IBaseRepository<WorkConditions>
    {
        Task DeleteByIdAsync(int id);
        Task<List<WorkConditions>> GetAllEmployeesWCAsync(int employeeId);
    }
}