﻿using InternationalWagesManager.Domain;
using InternationalWagesManager.DTO;
using MyLibrary.Utilities;
using System.ComponentModel;
using System.Windows.Input;

namespace InternationalWagesManager.WPFViewModels
{
    public class SalaryComponentsVM
    {
        private EmployeeManager _employeeManager;
        private SalaryComponentsManager _salaryComponentsManager;
        private DTO.SalaryComponents _salaryComponents = new DTO.SalaryComponents { Date = null };
        private List<Employee> _modelEmployees = new List<Employee>();

        public List<string> Employees { get; set; }

        public DTO.SalaryComponents SalaryComponents
        {
            get { return _salaryComponents; }
            set
            {
                _salaryComponents = value;
                OnPropertyChanged(nameof(SalaryComponents));
            }
        }
        private string _comboBoxSelectedIndex = "0";
        public string ComboBoxSelectedIndex
        {
            get { return _comboBoxSelectedIndex; }
            set
            {
                _comboBoxSelectedIndex = value;
                OnPropertyChanged(nameof(ComboBoxSelectedIndex));
            }
        }

        public ICommand AddCommand { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public SalaryComponentsVM(EmployeeManager employeeManager, SalaryComponentsManager salaryComponentsManager)
        {
            _employeeManager = employeeManager;
            _salaryComponentsManager = salaryComponentsManager;
            AddCommand = new CustomCommand(AddSalaryComponents, CanAddSalaryComponents);
            LoadData();
        }

        private void AddSalaryComponents(object obj)
        {
            int employeeId = _modelEmployees[int.Parse(ComboBoxSelectedIndex) - 1].Id;
            SalaryComponents.EmployeeId = employeeId;
            _salaryComponentsManager.AddSalaryComponentsSuccessAsync(SalaryComponents);
        }

        private bool CanAddSalaryComponents(object obj)
        {
            if (ComboBoxSelectedIndex != "0"
                && SalaryComponents.Date != null && SalaryComponents.TotalHours != null && SalaryComponents.TotalHours != 0)
                return true;

            return false;
        }

        private async void LoadData()
        {
            Employees = new List<string>() { "Select a employee!" };

            _modelEmployees = await _employeeManager.GetEmployeesAsync();
            foreach (var employee in _modelEmployees)
                Employees.Add(employee.FullName);
        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
