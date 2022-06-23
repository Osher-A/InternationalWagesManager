using InternationalWagesManager.Models;

namespace InternationalWagesManager.DAL
{
    public interface IWConditionsRepository
    {
        void AddWorkConditions(WorkConditions workConditions);
        void DeleteWorkConditions(WorkConditions workConditions);
        List<WorkConditions> GetAllWorkConditions(int employeeId);
        WorkConditions GetWorkConditions(int employeeId, DateTime date);
        void UpdateWorkConditions(WorkConditions workConditions);
    }
}