using InternationalWagesManager.DTO;

namespace MVC.ViewModels.WorkConditionsVM
{
    public class EditWorkConditionsVM
    {
        public WorkConditions WorkConditions { get; set; }
        public List<Currency> Currencies { get; set; }
    }
}
