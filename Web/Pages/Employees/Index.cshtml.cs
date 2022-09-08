using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using InternationalWagesManager.Domain;
using InternationalWagesManager.DTO;

namespace Web.Pages.Employees
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
