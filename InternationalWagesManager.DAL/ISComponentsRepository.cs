using InternationalWagesManager.Models;

namespace InternationalWagesManager.DAL
{
    public interface ISComponentsRepository
    {
        void AddScomponent(SalaryComponents newSC);
        void DeleteSComponent(SalaryComponents SC);
        SalaryComponents GetSComponent(SalaryComponents SC);
        void UpdateSComponent(SalaryComponents SC);
    }
}