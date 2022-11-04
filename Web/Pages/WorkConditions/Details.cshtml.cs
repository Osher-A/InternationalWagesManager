using InternationalWagesManager.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.WorkConditions
{
    public class DetailsModel : PageModel
    {
        private WorkConditionsManager _workConditionsManager;
        private EmployeeManager _employeeManager;
        public List<InternationalWagesManager.DTO.WorkConditions> WorkConditions { get; set; }
        public InternationalWagesManager.DTO.Employee Employee { get; set; }
        public DetailsModel(WorkConditionsManager workConditionsManager, EmployeeManager employeeManager)
        {
            _workConditionsManager = workConditionsManager;
            _employeeManager = employeeManager;
        }
        public async Task<IActionResult> OnGet(int employeeId)
        {
            Employee = (await _employeeManager.GetEmployeesAsync()).FirstOrDefault(e => e.Id == employeeId)!;
            if (Employee == null)
                return NotFound("Employee does not exist");
           WorkConditions = _workConditionsManager.GetAllEmployeesWC(employeeId);
           return Page(); 

        }
    }
}
