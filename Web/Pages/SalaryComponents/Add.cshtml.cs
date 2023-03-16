using InternationalWagesManager;
using InternationalWagesManager.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.SalaryComponents
{
    public class AddModel : PageModel
    {
        private SalaryComponentsManager _salaryComponentsManager;
        public InternationalWagesManager.DTO.SalaryComponents SalaryComponents { get; set; } = new();
        public AddModel(SalaryComponentsManager salaryComponentsManager)
        {
            _salaryComponentsManager = salaryComponentsManager;
        }
        public void OnGet(int employeeId)
        {
            SalaryComponents.EmployeeId = employeeId;
        }

        public async Task<ActionResult> OnPost(InternationalWagesManager.DTO.SalaryComponents salaryComponents)
        {
            //if (!ModelState.IsValid)
            //    return Page();

            await _salaryComponentsManager.AddSalaryComponentsAsync(salaryComponents);
            return RedirectToPage("./details", new {employeeId = salaryComponents.EmployeeId});
        }
    }
}
