using InternationalWagesManager.Domain;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.WorkConditions
{
    public class DeleteModel : PageModel
    {
        private readonly WorkConditionsManager _workConditionsManager;
        public DeleteModel(WorkConditionsManager workConditionsManager)
        {
            _workConditionsManager = workConditionsManager;
        }

        public void OnGet(int id)
        {
            _workConditionsManager.DeleteWorkConditions(id);
        }
    }
}
