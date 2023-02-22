using InternationalWagesManager.Models;

namespace InternationalWagesManager.DAL
{
    public interface IWConditionsRepository
    {
        Task<int> AddWorkConditionsAsync(WorkConditions workConditions);
        Task DeleteWorkConditionsAsync(int id);
        Task UpdateWorkConditionsAsync(WorkConditions workConditions);
        Task<List<WorkConditions>> GetAllEmployeesWCAsync(int employeeId);
        Task<WorkConditions> GetWorkConditionsAsync(int workConditionId);
    }
}