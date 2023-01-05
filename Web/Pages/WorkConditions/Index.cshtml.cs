using InternationalWagesManager.Domain;
using InternationalWagesManager.DTO;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.WorkConditions
{
    public class IndexModel : PageModel
    {
        public List<Employee> Employees { get; set; }
        private readonly EmployeeManager _employeeManager;
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
