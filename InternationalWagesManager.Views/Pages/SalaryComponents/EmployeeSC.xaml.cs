using InternationalWagesManager.Domain;
using InternationalWagesManager.DTO;
using InternationalWagesManager.WPFViewModels.SalaryComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InternationalWagesManager.Views.Pages.SalaryComponents
{
    /// <summary>
    /// Interaction logic for EmployeeSC.xaml
    /// </summary>
    public partial class EmployeeSC : Page
    {
        public EmployeeSC(SalaryComponentsManager salaryComponentsManager, Employee employee)
        {
            DataContext = new EmployeeSCVM(salaryComponentsManager, employee);
            InitializeComponent();
        }
    }
}
