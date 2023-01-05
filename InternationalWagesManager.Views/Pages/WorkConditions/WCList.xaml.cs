using InternationalWagesManager.Domain;
using InternationalWagesManager.WPFViewModels.WorkConditions;
using System.Windows.Controls;

namespace InternationalWagesManager.Views.Pages.WorkConditions
{
    /// <summary>
    /// Interaction logic for WCList.xaml
    /// </summary>
    public partial class WCList : Page
    {
        public WCList(EmployeeManager employeeManager)
        {
            var vm = new WCListVM(employeeManager);
            this.DataContext = vm;
            InitializeComponent();
        }
    }
}
