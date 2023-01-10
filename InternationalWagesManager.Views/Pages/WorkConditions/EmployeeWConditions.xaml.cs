using InternationalWagesManager.Domain;
using InternationalWagesManager.WPFViewModels.WorkConditions;
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

namespace InternationalWagesManager.Views.Pages.WorkConditions
{
    /// <summary>
    /// Interaction logic for EmployeeWConditions.xaml
    /// </summary>
    public partial class EmployeeWConditions : Page
    {
        public EmployeeWConditions(WorkConditionsManager workConditionsManager, DTO.Employee employee)
        {
            this.DataContext = new EmployeeWConditonsVM(workConditionsManager, employee);
            InitializeComponent();
        }
    }
}
