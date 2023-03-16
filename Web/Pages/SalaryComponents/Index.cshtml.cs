using InternationalWagesManager.Domain;
using InternationalWagesManager.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.SalaryComponents
{
    public class IndexModel : PageModel
    {
        public List<Employee> Employees { get; set; } = new();
        private EmployeeManager _employeeManager;
        public IndexModel(EmployeeManager employeeManager)
        {
            _employeeManager = employeeManager;
        }
        public async Task OnGetAsync()
        {
            Employees = await _employeeManager.GetEmployeesAsync();
        }
    }
}
