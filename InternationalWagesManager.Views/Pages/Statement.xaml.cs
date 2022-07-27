using InternationalWagesManager.Domain;
using InternationalWagesManager.ViewModels;
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

namespace InternationalWagesManager.Views.Pages
{
    /// <summary>
    /// Interaction logic for Statement.xaml
    /// </summary>
    public partial class Statement : Page
    {
        public Statement(SalaryManager salaryManager, PaymentsManager paymentsManager, StatementManager balanceManager, EmployeeManager employeeManager)
        {
            this.DataContext = new StatementVM(salaryManager, paymentsManager, balanceManager, employeeManager);
            InitializeComponent();
        }
    }
}
