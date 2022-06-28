using InternationalWagesManager.Models;

namespace InternationalWagesManager.DAL
{
    public interface ISalaryComponentsRepository
    {
        void AddSalaryComponents(SalaryComponents newSC);
        void DeleteSalaryComponents(SalaryComponents SC);
        SalaryComponents GetSalaryComponents(int employeeId, DateTime date);
        void UpdateSalaryComponents(SalaryComponents SC);
    }
}