using InternationalWagesManager.Domain;
using InternationalWagesManager.DTO;
using InternationalWagesManager.ViewModels.Employees;
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
