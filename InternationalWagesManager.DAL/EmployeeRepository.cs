using InternationalWagesManager.Models;

namespace InternationalWagesManager.DAL
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private MyDbContext _db;
        public EmployeeRepository(MyDbContext dbContext)
        {
            _db = dbContext;
        }

        public void AddEmployee(Employee newEmployee)
        {
            _db.Employees.Add(newEmployee);
            _db.SaveChanges();
        }

        public void UpdateEmployee(Employee employee)
        {
            _db.Employees.Update(employee);
            _db.SaveChanges();
        }

        public void DeleteEmployee(Employee employee)
        {
            _db.Employees.Remove(employee);
            _db.SaveChanges();
        }

        public Employee GetEmployee(string email)
        {
            var employee = _db.Employees.FirstOrDefault(e => e.Email.Trim() == email.Trim());
            return employee ?? new Employee();
        }

        public List<Employee> GetEmployees()
        {
            return _db.Employees.ToList();
        }
    }
}