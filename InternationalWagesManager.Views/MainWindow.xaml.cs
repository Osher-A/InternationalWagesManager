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
using AutoMapper;
using InternationalWagesManager.DAL;
using InternationalWagesManager.ViewModels;
using InternationalWagesManager.Views.Pages;
using MahApps.Metro.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace InternationalWagesManager.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private IEmployeeRepository _employeeRepository;
        private IWConditionsRepository _wConditionsRepository;
        private ISalaryComponentsRepository _salaryComponentsRepository;
        private IMapper _mapper;
        public MainWindow(IMapper mapper,IEmployeeRepository employeeRepository, IWConditionsRepository wConditionsRepository, ISalaryComponentsRepository salaryComponentsRepository)
        {
            _mapper = mapper;
            _employeeRepository = employeeRepository;
            _wConditionsRepository = wConditionsRepository;
            _salaryComponentsRepository = salaryComponentsRepository;
            InitializeComponent();
        }

        private void MainWindowFrame_Loaded(object sender , RoutedEventArgs e)
        {
            // MainWindowFrame.Content = new HomePage(_employeeRepository, _mapper);
        }

        private void TreeViewAddEmployee_MouseEnter(object sender, MouseEventArgs e)
        {
            MainWindowFrame.Content = new EmployeeDetails(_mapper, _employeeRepository);
            EmployeeDetailsVM.EditRowHeight = "0";
        }
        private void TreeViewEditEmployee_MouseEnter(object sender, MouseEventArgs e)
        {
            MainWindowFrame.Content = new EmployeeDetails(_mapper, _employeeRepository);
            EmployeeDetailsVM.EditRowHeight = "Auto";
        }

        private void TreeViewAddWorkConditions_MouseEnter(object sender, MouseEventArgs e)
        {
            MainWindowFrame.Content = new WorkConditions(_mapper, _employeeRepository, _wConditionsRepository);
        }

        private void TreeViewAddSalaryComponents_MouseEnter(object sender, MouseEventArgs e)
        {
            MainWindowFrame.Content = new SalaryComponents(_mapper, _employeeRepository, _salaryComponentsRepository);
        }

        private void TreeViewAddPayment_MouseEnter(object sender, MouseEventArgs e)
        {
            MainWindowFrame.Content = new Payments();
        }
       
    }
}
