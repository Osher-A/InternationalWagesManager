using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InternationalWagesManager.Domain;
using InternationalWagesManager.Domain.Utilities;
using InternationalWagesManager.DTO;
using InternationalWagesManager.WPFViewModels.Enums;
using MyLibrary.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InternationalWagesManager.WPFViewModels.SalaryComponents
{
    public partial class SCIndexVM : ObservableObject
    {
        private readonly EmployeeManager _employeeManager;
        [ObservableProperty]
        private ObservableCollection<Employee> _allEmployees;

        [ObservableProperty]
        private Employee _selectedEmployee = new Employee();

        [ObservableProperty]
        private string _dataGridVisibility = Visibility.Hidden.ToString();

        [ObservableProperty]
        private string _progressRingVisibility = Visibility.Visible.ToString();


        public static SalaryComponentsDetailsEvent DetailsWindowEvent { get; set; }
        public static AllSlaryComponentsEvent AllSalaryComponentsEvent { get; set; }

        public SCIndexVM(EmployeeManager employeeManager)
        {
            _employeeManager = employeeManager;
            LoadData();
        }
        [RelayCommand]
        private void Add(object obj)
        {
            DetailsWindowEvent?.Invoke(SelectedEmployee, ActionType.Add);
        }
        [RelayCommand]
        private void Details(object obj)
        {
            AllSalaryComponentsEvent?.Invoke(SelectedEmployee);
        }


        private async void LoadData()
        {
            var employees = await _employeeManager.GetEmployeesAsync();
            AllEmployees = employees.ToObservableCollection();
            DataGridVisibility = Visibility.Visible.ToString();
            ProgressRingVisibility = Visibility.Collapsed.ToString();
        }

    }

    public delegate void SalaryComponentsDetailsEvent(DTO.Employee employee, ActionType ActionType);
    public delegate void AllSlaryComponentsEvent(DTO.Employee employee);
}

