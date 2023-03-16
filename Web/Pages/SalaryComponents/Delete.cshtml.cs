using InternationalWagesManager.Domain;
using InternationalWagesManager.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.SalaryComponents
{
    public class DeleteModel : PageModel
    {
        private readonly SalaryComponentsManager _salaryComponentsManager;
        public DeleteModel(SalaryComponentsManager salaryComponentsManager)
        {
            _salaryComponentsManager = salaryComponentsManager;
        }
        public void OnGet()
        {
        }

        public async Task<ActionResult> OnPost(int id, int employeeId)
        {
            var salaryComp = new InternationalWagesManager.DTO.SalaryComponents() { Id = id, EmployeeId = employeeId };
            await _salaryComponentsManager.DeletedSalarySuccessfullyAsync(salaryComp);
            return RedirectToPage("./Details", new { employeeId = salaryComp.EmployeeId });
        }

    }
}
