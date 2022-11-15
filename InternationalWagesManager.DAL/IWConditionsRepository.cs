using InternationalWagesManager.Models;

namespace InternationalWagesManager.DAL
{
    public interface IWConditionsRepository
    {
        Task<int> AddWorkConditions(WorkConditions workConditions);
        void DeleteWorkConditions(int id);
        Task<List<WorkConditions>> GetAllWorkConditions(int employeeId);
        Task<WorkConditions> GetWorkConditions(int employeeId, DateTime date);
        void UpdateWorkConditions(WorkConditions workConditions);
        Task<WorkConditions> GetWorkConditions(int workConditionId);
    }
}