using InternationalWagesManager.DTO;

namespace MVC.ViewModels.SalaryComponentsVM
{
    public class EmployeeSalaryComponentsVM
    {
        public IEnumerable<SalaryComponents> AllSalaryComponents { get; set; }
        public string EmployeeName { get; set; }
    }
}
