using InternationalWagesManager.DTO;

namespace MVC.ViewModels.WorkConditionsVM
{
    public class CreateWorkConditionsVM
    {
        public WorkConditions WorkConditions { get; set; }
        public IEnumerable<Currency> Currencies { get; set; }
    }
}
