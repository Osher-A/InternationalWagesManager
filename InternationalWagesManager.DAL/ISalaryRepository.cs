using InternationalWagesManager.Models;

namespace InternationalWagesManager.DAL
{
    public interface ISalaryRepository
    {
        void AddSalary(Salary salary);
        void DeleteSalary(Salary salary);
        List<Salary> GetAllSalaries(int employeeId);
        Salary GetSalary(int salaryId);
        void UpdateSalary(Salary salary);
    }
}