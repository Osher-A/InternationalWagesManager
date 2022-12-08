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
using InternationalWagesManager.Domain;
using InternationalWagesManager.DTO;
using InternationalWagesManager.ViewModels.Employees;
using InternationalWagesManager.ViewModels.Employees.UpsertVM;
using InternationalWagesManager.Views.Pages;
using InternationalWagesManager.Views.Pages.Employees;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Extensions.DependencyInjection;
using ToastNotifications.Messages.Warning;
using MyLibrary.Utilities;

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
        private IPaymentsRepository _paymentsRepository;
        private ISalaryRepository _salaryRepository;
        private ICurrenciesRepository _currenciesRepository;
        private AutoMapper.IMapper _mapper;
        private EmployeeManager _employeeManager;
        private WorkConditionsManager _workConditionsManager;
        private SalaryComponentsManager _salaryComponentsManager;
        private SalaryManager _salaryManager;
        private PaymentsManager _paymentsManager;
        private CurrenciesManager _currenciesManager;
        private StatementManager _balanceManager;
        public MainWindow(EmployeeManager employeeManager, WorkConditionsManager workConditionsManager, SalaryComponentsManager salaryComponentsManager, 
            SalaryManager salaryManager, CurrenciesManager currenciesManager, PaymentsManager paymentsManager, StatementManager balanceManager)
        {
            _employeeManager = employeeManager;
            _workConditionsManager = workConditionsManager;
            _salaryComponentsManager = salaryComponentsManager;
            _salaryManager = salaryManager;
            _currenciesManager = currenciesManager;
            _paymentsManager = paymentsManager;
            _balanceManager = balanceManager;
            ListVM.DetailsWindowEvent += (Employee selectedEmployee) => MainWindowFrame.Content = new Pages.Employees.Details(selectedEmployee, employeeManager);
            DetailsVM.BackAction += () => MainWindowFrame.Content = new Pages.Employees.List(_employeeManager);
            MessagesManager.AlertFunc += WarningMessageBox;
            MessagesManager.SuccessMessage += SuccessToastr;
            MessagesManager.ErrorMessage += ErrorToastr;
            InitializeComponent();
        }

        private void MainWindowFrame_Loaded(object sender , RoutedEventArgs e)
        {
            // MainWindowFrame.Content = new HomePage(_employeeRepository, _mapper);
            //  MainWindowFrame.Content = new Upsert(_employeeManager);
            MainWindowFrame.Content = new Pages.Employees.List(_employeeManager);

            UpsertVM.EditRowHeight = "0";
        }

        private void TreeViewViewEmployees_MouseDown(object sender, EventArgs e) =>
            MainWindowFrame.Content = new Pages.Employees.List(_employeeManager);

        private void TreeViewAddEmployee_MouseDown(object sender, EventArgs e)
        {
            MainWindowFrame.Content = new Upsert(_employeeManager);
            UpsertVM.EditRowHeight = "0";
        }
        private void TreeViewEditEmployee_MouseDown(object sender, EventArgs e)
        {
            MainWindowFrame.Content = new Upsert(_employeeManager);
            UpsertVM.EditRowHeight = "Auto";
        }

        private void TreeViewAddWorkConditions_MouseDown(object sender, EventArgs e) =>
            MainWindowFrame.Content = new Pages.WorkConditions.List(_employeeManager);

        private void TreeViewAddSalaryComponents_MouseEnter(object sender, EventArgs e) =>
            MainWindowFrame.Content = new Pages.SalaryComponents(_employeeManager, _salaryComponentsManager);

        private void TreeViewAddPayment_MouseDown(object sender, EventArgs e) => 
            MainWindowFrame.Content = new Payments(_employeeManager, _paymentsManager);

        private void TreeViewStatement_MouseDown(object sender, EventArgs e) => 
            MainWindowFrame.Content = new Pages.Statement(_salaryManager, _paymentsManager, _balanceManager, _employeeManager);

        private async Task<bool> WarningMessageBox(string message)
        {
           var result = await this.ShowMessageAsync("Warning!", message, MessageDialogStyle.AffirmativeAndNegative);
            if (result == MessageDialogResult.Affirmative)
                return true;
            return false;
        }

        private void SuccessToastr(string message)
        {
            var toastr = new ToastViewModel();
            toastr.ShowSuccess(message);
        }

        private void ErrorToastr(string message)
        {
            var toastr = new ToastViewModel();
            toastr.ShowError(message);
        }

       
    }
}
