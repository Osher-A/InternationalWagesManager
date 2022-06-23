using InternationalWagesManager.Models;

namespace InternationalWagesManager.DAL
{
    public interface IEmployeeRepository
    {
        void AddEmployee(Employee newEmployee);
        void DeleteEmployee(Employee employee);
        Employee GetEmployee(string email);
        List<Employee> GetEmployees();
        void UpdateEmployee(Employee employee);
    }
}