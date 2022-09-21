using InternationalWagesManager.Domain;
using InternationalWagesManager.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Employees
{
    public class DeleteModel : PageModel
    {
        private readonly EmployeeManager _employeeManager;
        public Employee? Employee { get; set; }
        public DeleteModel(EmployeeManager employeeManager)
        {
            _employeeManager = employeeManager;
        }

        public async Task<IActionResult> OnGetAsync(int employeeId)
        {
           var employees = await _employeeManager.GetEmployeesAsync();
           Employee = employees.Find(e => e.Id == employeeId);

            if(Employee == null)
                return NotFound();

            return Page();
        }

        public IActionResult OnPost(int employeeId)
        {
            if(Employee == null || employeeId == 0)
                return BadRequest();

            _employeeManager.DeleteEmployeeAsync(Employee);

            return RedirectToPage("./index");
        }
    }
}
