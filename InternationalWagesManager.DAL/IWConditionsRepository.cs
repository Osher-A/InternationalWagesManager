using InternationalWagesManager.Models;

namespace InternationalWagesManager.DAL
{
    public interface IWConditionsRepository
    {
        void AddWorkConditions(WorkConditions workConditions);
        void DeleteWorkConditions(int id);
        List<WorkConditions> GetAllWorkConditions(int employeeId);
        WorkConditions GetWorkConditions(int employeeId, DateTime date);
        void UpdateWorkConditions(WorkConditions workConditions);
        WorkConditions GetWorkConditions(int workConditionId);
    }
}