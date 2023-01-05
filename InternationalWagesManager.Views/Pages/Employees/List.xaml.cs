using InternationalWagesManager.Domain;
using InternationalWagesManager.WPFViewModels.Employees;
using System.Windows.Controls;

namespace InternationalWagesManager.Views.Pages.Employees
{
    /// <summary>
    /// Interaction logic for List.xaml
    /// </summary>
    public partial class List : Page
    {
        public List(EmployeeManager employeeManager)
        {
            var vm = new ListVM(employeeManager);
            this.DataContext = vm;

            InitializeComponent();
        }
    }
}
