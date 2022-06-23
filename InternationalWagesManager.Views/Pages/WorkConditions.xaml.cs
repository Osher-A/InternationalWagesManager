using AutoMapper;
using InternationalWagesManager.DAL;
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
    /// Interaction logic for WorkConditions.xaml
    /// </summary>
    public partial class WorkConditions : Page
    {
        public WorkConditions(IMapper mapper, IEmployeeRepository employeeRepository, IWConditionsRepository wConditionsRepository)
        {
            this.DataContext = new WorkConditionsVM(mapper, employeeRepository, wConditionsRepository);

            InitializeComponent();
        }
    }
}
