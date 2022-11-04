using InternationalWagesManager.Domain;
using InternationalWagesManager.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.WorkConditions
{
    public class EditModel : PageModel
    {
        public List<Currency> Currencies { get; set; }
        public InternationalWagesManager.DTO.WorkConditions WorkConditions { get; set; } = new InternationalWagesManager.DTO.WorkConditions();
        private readonly CurrenciesManager _currenciesManager;
        private readonly WorkConditionsManager _workConditionsManager;

        public EditModel( CurrenciesManager currenciesManager, WorkConditionsManager workConditionsManager)
        {
            _currenciesManager = currenciesManager;
            _workConditionsManager = workConditionsManager;
        }

        public void OnGet(int id)
        {
            Currencies = _currenciesManager.GetAllCurrencies().Result;
            WorkConditions = _workConditionsManager.GetWorkConditions(id);
        }

        public IActionResult OnPost(InternationalWagesManager.DTO.WorkConditions workConditions)
        {
            if (workConditions.Id == 0 || workConditions.EmployeeId == 0)
                return RedirectToPage(new {id = workConditions.Id});

          _workConditionsManager.UpdateWorkConditions(workConditions);
         return RedirectToPage("./Details", new { employeeId = workConditions.EmployeeId });
        }
    }
}
