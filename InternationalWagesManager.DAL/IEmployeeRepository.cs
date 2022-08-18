using InternationalWagesManager.Models;

namespace InternationalWagesManager.DAL
{
    public interface IEmployeeRepository
    {
        Task<int> AddEmployeeAsync(Employee newEmployee);
        void DeleteEmployee(Employee employee);
        Task<Employee?> GetEmployeeAsync(int id);
        Task<List<Employee>> GetEmployeesAsync();
        void UpdateEmployee(Employee employee);
    }
}