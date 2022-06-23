using AutoMapper;
using InternationalWagesManager.DAL;
using InternationalWagesManager.Domain;
using InternationalWagesManager.Domain.Utilities;
using InternationalWagesManager.DTO;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MyLibrary.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InternationalWagesManager.ViewModels
{
    public class EmployeeDetailsVM : INotifyPropertyChanged
    {
        private EmployeeManager _employeeManager;
        private Employee _selectedEmployee = new Employee() { DOB = null };
        private List<Employee> _modelEmployees;
        public Employee SelectedEmployee
        {
            get { return _selectedEmployee; }
            set
            {
                _selectedEmployee = value;
                OnPropertyChanged(nameof(SelectedEmployee));
            }
        }
        private static string _editRowHeight = "0";
        public static string EditRowHeight
        {
            get { return _editRowHeight; }
            set
            { 
                _editRowHeight = value; 
                OnStaticPropertyChanged(nameof(EditRowHeight));
            }
        }
        private string _comboBoxSelectedIndex = "0";
        public string ComboBoxSelectedIndex 
        { 
            get { return _comboBoxSelectedIndex; }
            set
            {
                _comboBoxSelectedIndex = value;
                UpdateSelectedEmployee();
                OnPropertyChanged(nameof(ComboBoxSelectedIndex));
            }
        }

        public ObservableCollection<string> Employees { get; set; } = new() { "Select a employee!" };
        public ICommand AddAndEditCommand { get; set; }
        public ICommand EditCommand { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        public static event PropertyChangedEventHandler?  StaticPropertyChanged;

        public EmployeeDetailsVM(AutoMapper.IMapper mapper, IEmployeeRepository employeeRepository)
        {
            _employeeManager = new EmployeeManager(mapper, employeeRepository);
            AddAndEditCommand = new CustomCommand(AddOrEditEmployee, CanAddOrEditEmployee);
            LoadData();
        }

        private void AddOrEditEmployee(object obj)
        {
            if(EditRowHeight == "0")
            _employeeManager.AddEmployee(SelectedEmployee);
            else
                _employeeManager.UpdateEmployee(SelectedEmployee);

            LoadData();
        }

        private bool CanAddOrEditEmployee(object obj)
        {
            if (EditRowHeight == "0" && !string.IsNullOrWhiteSpace(SelectedEmployee.FullName) && !string.IsNullOrWhiteSpace(SelectedEmployee.Email))
                return true;
            else if (EditRowHeight != "0" && ComboBoxSelectedIndex != "0")
                return true;
            return false;
        }

        private void UpdateSelectedEmployee()
        {
            if (ComboBoxSelectedIndex != "0")
                SelectedEmployee = _modelEmployees[int.Parse(ComboBoxSelectedIndex) - 1];
            else
                SelectedEmployee = new Employee() { DOB = null };
        }

        private void LoadData()
        {
            // To ensure its empty when called after an update
            Employees = new ObservableCollection<string>() { "Select a employee!"};   

            _modelEmployees = _employeeManager.GetEmployees().ToList();
            foreach (var employee in _modelEmployees)
                Employees.Add(employee.FullName);
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private static void OnStaticPropertyChanged(string propertyName)
        {
            if(StaticPropertyChanged != null)
            StaticPropertyChanged.Invoke(null, new PropertyChangedEventArgs(propertyName));
        }
    }
}
