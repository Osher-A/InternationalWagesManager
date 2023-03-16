using InternationalWagesManager.Domain;
using InternationalWagesManager.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.SalaryComponents
{
    public class EditModel : PageModel
    {
        private SalaryComponentsManager _salaryComponentsManager;
        public InternationalWagesManager.DTO.SalaryComponents SalaryComponents { get; set; }
        public EditModel(SalaryComponentsManager salaryComponentsManager)
        {
            _salaryComponentsManager = salaryComponentsManager;
        }
        public async Task OnGetAsync(int id)
        {
            SalaryComponents = await _salaryComponentsManager.GetSalaryComponentsAsync(id);
        }

        public async Task<ActionResult> OnPost(InternationalWagesManager.DTO.SalaryComponents salaryComponents)
        {
            if (!ModelState.IsValid)
                return Page();
            await _salaryComponentsManager.UpdateSalaryAsync(salaryComponents);
            return RedirectToPage("./Details", new { employeeId = salaryComponents.EmployeeId });
        }
    }
}
