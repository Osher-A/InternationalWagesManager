﻿using InternationalWagesManager.Domain;
using InternationalWagesManager.DTO;
using InternationalWagesManager.ViewModels.Employees;
using InternationalWagesManager.ViewModels.WorkConditons;
using InternationalWagesManager.Views.Pages;
using InternationalWagesManager.WPFViewModels.Employees;
using InternationalWagesManager.WPFViewModels.SalaryComponents;
using InternationalWagesManager.WPFViewModels.WorkConditions;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MyLibrary.Utilities;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace InternationalWagesManager.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
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
            WCListVM.DetailsWindowEvent += (int employeeId, ActionType actionType) => MainWindowFrame.Content = new Pages.WorkConditions.WCDetails(employeeId, actionType, workConditionsManager, currenciesManager);
            DetailsVM.BackAction += () => MainWindowFrame.Content = new Pages.Employees.List(_employeeManager);
            WCListVM.AllWorkConditionsEvent += (DTO.Employee employee) => MainWindowFrame.Content = new Pages.WorkConditions.EmployeeWConditions(workConditionsManager, employee);
            WCDetailsVM.BackAction += () => MainWindowFrame.Content = new Pages.WorkConditions.WCList(employeeManager);
            EmployeeWConditonsVM.BackAction += () => MainWindowFrame.Content = new Pages.WorkConditions.WCList(employeeManager);
            EmployeeWConditonsVM.UpdateAction += (int workConditionsId, ActionType actionType) => MainWindowFrame.Content = new Pages.WorkConditions.WCDetails(workConditionsId, actionType, workConditionsManager, currenciesManager);
            SCIndexVM.AllWorkConditionsEvent += (DTO.Employee employee) => MainWindowFrame.Content = new Pages.SalaryComponents.EmployeeSC(salaryComponentsManager, employee);
            SCIndexVM.DetailsWindowEvent += (int id, ActionType actionType) => MainWindowFrame.Content = new Pages.SalaryComponents.SCDetails(id, actionType, salaryComponentsManager);
            EmployeeSCVM.BackAction += () => MainWindowFrame.Content = new Pages.SalaryComponents.SCIndex(employeeManager);
            EmployeeSCVM.UpdateAction += (int id, ActionType actionType) => MainWindowFrame.Content = new Pages.SalaryComponents.SCDetails(id, actionType, salaryComponentsManager);
            SCDetailsVM.BackAction += () => MainWindowFrame.Content = new Pages.SalaryComponents.SCIndex(employeeManager);
            MessagesManager.AlertFunc += WarningMessageBox;
            MessagesManager.SuccessMessage += SuccessToastr;
            MessagesManager.ErrorMessage += ErrorToastr;
            InitializeComponent();
        }

        private void MainWindowFrame_Loaded(object sender, RoutedEventArgs e)
        {
            // MainWindowFrame.Content = new HomePage(_employeeRepository, _mapper);
            MainWindowFrame.Content = new Pages.Employees.List(_employeeManager);
        }
        private void TreeViewViewEmployees_MouseDown(object sender, EventArgs e) =>
            MainWindowFrame.Content = new Pages.Employees.List(_employeeManager);

        private void TreeViewAddWorkConditions_MouseDown(object sender, EventArgs e) =>
            MainWindowFrame.Content = new Pages.WorkConditions.WCList(_employeeManager);

        private void TreeViewAddSalaryComponents_MouseEnter(object sender, EventArgs e) =>
            MainWindowFrame.Content = new Pages.SalaryComponents.SCIndex(_employeeManager);

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
