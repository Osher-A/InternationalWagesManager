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
    /// Interaction logic for SCDetails.xaml
    /// </summary>
    public partial class SCDetails : Page
    {
        public SCDetails(int id, DTO.ActionType actionType, SalaryComponentsManager salaryComponentsManager)
        {
            DataContext = new SCDetailsVM(id, actionType, salaryComponentsManager);
            InitializeComponent();
        }
    }
}
