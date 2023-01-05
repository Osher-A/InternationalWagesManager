using InternationalWagesManager.Domain;
using System.Windows.Controls;

namespace InternationalWagesManager.Views.Pages
{
    /// <summary>
    /// Interaction logic for SalaryComponents.xaml
    /// </summary>
    public partial class SalaryComponents : Page
    {
        public SalaryComponents(EmployeeManager employeeManager, SalaryComponentsManager salaryComponentsManager)
        {
            this.DataContext = new WPFViewModels.SalaryComponentsVM(employeeManager, salaryComponentsManager);
            InitializeComponent();
        }
    }
}
