using InternationalWagesManager.Models;

namespace InternationalWagesManager.DAL
{
    public interface IWConditionsRepository
    {
        Task<int> AddWorkConditions(WorkConditions workConditions);
        void DeleteWorkConditions(int id);
        Task UpdateWorkConditionsAsync(WorkConditions workConditions);
        Task<List<WorkConditions>> GetAllEmployeesWCAsync(int employeeId);
        Task<WorkConditions> GetWorkConditionsAsync(int workConditionId);
    }
}