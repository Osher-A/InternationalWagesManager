using InternationalWagesManager.Domain;
using InternationalWagesManager.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.SalaryComponents
{
    public class DetailsModel : PageModel
    {
        private readonly SalaryComponentsManager _salaryComponentsManager;
        private readonly EmployeeManager _employeeManager;
        public List<InternationalWagesManager.DTO.SalaryComponents> AllSalaryComponents { get; set; } = new();
        public Employee Employee { get; set; }
        public DetailsModel(SalaryComponentsManager salaryComponentsManager, EmployeeManager employeeManager)
        {
            _salaryComponentsManager = salaryComponentsManager;
            _employeeManager = employeeManager;
        }

        public async Task<IActionResult> OnGetAsync([FromRoute] int employeeId)
        {
            Employee = (await _employeeManager.GetEmployeesAsync()).SingleOrDefault(e => e.Id == employeeId) ?? new();
            AllSalaryComponents = await _salaryComponentsManager.GetAllEmployeesSCAsync(employeeId);
            return Page();
        } 
    }
}
