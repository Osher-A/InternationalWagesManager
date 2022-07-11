using InternationalWagesManager.Models;

namespace InternationalWagesManager.DAL
{
    public interface ISalaryComponentsRepository
    {
        void AddSalaryComponents(SalaryComponents newSC);
        void DeleteSalaryComponents(SalaryComponents SC);
        List<SalaryComponents> GetEmployeeSalaryComponents(int employeeId);
        void UpdateSalaryComponents(SalaryComponents SC);
    }
}