using AutoMapper;
using InternationalWagesManager.DAL;
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
    /// Interaction logic for SalaryComponents.xaml
    /// </summary>
    public partial class SalaryComponents : Page
    {
        public SalaryComponents(IMapper mapper, IEmployeeRepository employeeRepository, ISalaryComponentsRepository salaryComponentsRepository,
            ISalaryRepository salaryRepository, IWConditionsRepository wConditionsRepository, ICurrenciesRepository currenciesRepository)
        {
            this.DataContext = new ViewModels.SalaryComponentsVM(mapper, employeeRepository, salaryComponentsRepository, salaryRepository, wConditionsRepository, currenciesRepository);
            InitializeComponent();
        }
    }
}
