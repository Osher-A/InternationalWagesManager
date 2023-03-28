using InternationalWagesManager.Domain;
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
    /// Interaction logic for SCIndex.xaml
    /// </summary>
    public partial class SCIndex : Page
    {
        public SCIndex(EmployeeManager employeeManager)
        {
            this.DataContext = new SCIndexVM(employeeManager);
            InitializeComponent();
        }
    }
}
