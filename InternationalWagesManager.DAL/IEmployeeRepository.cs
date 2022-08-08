using InternationalWagesManager.Models;

namespace InternationalWagesManager.DAL
{
    public interface IEmployeeRepository
    {
        void AddEmployee(Employee newEmployee);
        void DeleteEmployee(Employee employee);
        Employee GetEmployee(string email);
        Task<List<Employee>> GetEmployeesAsync();
        void UpdateEmployee(Employee employee);
    }
}