using InternationalWagesManager.DTO;
using System.ComponentModel.DataAnnotations;

namespace MVC.ViewModels
{
    public class WorkConditionsDetailsVM
    {
        public Employee Employee { get; set; }
        [Display(Description = "Work Conditions")]
        public List<WorkConditions> WorkConditions { get; set; }

    }
}
