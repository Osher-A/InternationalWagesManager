using InternationalWagesManager.Domain;
using InternationalWagesManager.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Employees
{
    public class DetailsModel : PageModel
    {
        private EmployeeManager _employeeManager;
        public Employee Employee { get; set; }

        public DetailsModel(EmployeeManager employeeManager)
        {
            _employeeManager = employeeManager;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == 0)
                return BadRequest();
            var employees = await _employeeManager.GetEmployeesAsync();
            var employee = employees.FirstOrDefault(e => e.Id == id);

            if(employee == null)
                return NotFound();

            Employee = employee;

            return Page();
        }
    }
}
