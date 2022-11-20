using InternationalWagesManager.Models;

namespace InternationalWagesManager.DAL
{
    public interface IWConditionsRepository
    {
        Task<int> AddWorkConditions(WorkConditions workConditions);
        void DeleteWorkConditions(int id);
        void UpdateWorkConditions(WorkConditions workConditions);
        Task<List<WorkConditions>> GetAllEmployeesWCAsync(int employeeId);
        Task<WorkConditions> GetEmployeesWCToDateAsync(int employeeId, DateTime date);
        Task<WorkConditions> GetWorkConditionsAsync(int workConditionId);
    }
}