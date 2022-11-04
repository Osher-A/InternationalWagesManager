using InternationalWagesManager.Domain;
using InternationalWagesManager.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Employees
{
    public class AddModel : PageModel
    {
        private readonly EmployeeManager _employeeManager;
        public Employee Employee { get; set; } = new Employee();

        public AddModel(EmployeeManager employeeManager)
        {
            _employeeManager = employeeManager;
        }

        public void OnGet()
        {
           
        }

        public async Task<IActionResult> OnPostAsync(Employee Employee)
        {

           await _employeeManager.AddEmployeeAsync(Employee);

            return RedirectToPage("index");
        }
    }
}

