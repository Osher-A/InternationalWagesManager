using InternationalWagesManager.Domain;
using InternationalWagesManager.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.WorkConditions
{
    public class CreateModel : PageModel
    {
        public InternationalWagesManager.DTO.WorkConditions WorkConditions { get; set; } = new();
        public List<Currency> Currencies { get; set; }
        private readonly CurrenciesManager _currenciesManager;
        private readonly WorkConditionsManager _workConditionsManager;
        public CreateModel(CurrenciesManager currenciesManager, WorkConditionsManager workConditionsManager)
        {
            _currenciesManager = currenciesManager;
            _workConditionsManager = workConditionsManager;
        }
        public void OnGet(int employeeId)
        {
            WorkConditions = new InternationalWagesManager.DTO.WorkConditions();
            WorkConditions.EmployeeId = employeeId;
            Currencies = _currenciesManager.GetAllCurrencies().Result;
        }

        public IActionResult OnPost(InternationalWagesManager.DTO.WorkConditions workConditions)
        {
            if (workConditions.EmployeeId == 0)
                return Page();
            _workConditionsManager.AddWorkConditions(workConditions);

            return RedirectToPage("./index");
        }
    }
}
