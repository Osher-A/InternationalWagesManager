using InternationalWagesManager.Domain;
using InternationalWagesManager.WPFViewModels;
using System.Windows.Controls;

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
