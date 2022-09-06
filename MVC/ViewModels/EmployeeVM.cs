using InternationalWagesManager.DTO;

namespace MVC.ViewModels
{
    public class EmployeeVM
    {
        public Employee SelectedEmployee { get; set; } = new InternationalWagesManager.DTO.Employee();
        public IEnumerable<Employee> Employees { get; set; } = Enumerable.Empty<Employee>();
    }
}
