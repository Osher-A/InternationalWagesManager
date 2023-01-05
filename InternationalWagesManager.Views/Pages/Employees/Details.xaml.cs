using InternationalWagesManager.Domain;
using InternationalWagesManager.DTO;
using InternationalWagesManager.ViewModels.Employees;
using System.Windows.Controls;

namespace InternationalWagesManager.Views.Pages.Employees
{
    /// <summary>
    /// Interaction logic for Details.xaml
    /// </summary>
    public partial class Details : Page
    {
        public Details(Employee seletedEmployee, EmployeeManager employeeManager)
        {
            this.DataContext = new DetailsVM(seletedEmployee, employeeManager);
            InitializeComponent();
        }
    }
}
