using InternationalWagesManager.Domain;
using InternationalWagesManager.WPFViewModels;
using System.Windows.Controls;

namespace InternationalWagesManager.Views.Pages
{
    /// <summary>
    /// Interaction logic for Payments.xaml
    /// </summary>
    public partial class Payments : Page
    {
        public Payments(EmployeeManager employeeManager, PaymentsManager paymentsManager)
        {
            this.DataContext = new PaymentsVM(employeeManager, paymentsManager);
            InitializeComponent();
        }
    }
}
