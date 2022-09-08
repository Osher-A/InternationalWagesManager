using InternationalWagesManager.Domain;
using InternationalWagesManager.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Employees
{
    public class EditModel : PageModel
    {
        private readonly EmployeeManager _employeeManager;
        public Employee Employee { get; set; }

        public EditModel(EmployeeManager employeeManager)
        {
            _employeeManager = employeeManager;
        }

        public async void OnGetAsync(int id)
        {
            var employees = await _employeeManager.GetEmployeesAsync();
            Employee = employees.FirstOrDefault(e => e.Id == id)!; 
        }

        public IActionResult OnPostAsync(Employee employee)
        {
            if (!ModelState.IsValid)
                return Page();

            _employeeManager.UpdateEmployee(employee);

           return RedirectToPage("./index");
        }
    }
}
