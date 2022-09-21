﻿using InternationalWagesManager.Domain;
using InternationalWagesManager.Domain.Utilities;
using InternationalWagesManager.DTO;
using MyLibrary.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InternationalWagesManager.ViewModels.Employees
{
    public class ListVM : INotifyPropertyChanged
    {
        private readonly EmployeeManager _employeeManager;
        private ObservableCollection<Employee> _allEmployees;

        public ObservableCollection<Employee> AllEmployees
        {
            get { return _allEmployees; }
            set
            {
                _allEmployees = value;
                OnPropertyChanged(nameof(AllEmployees));
            }
        }
        private Employee _selectedEmployee;
        public Employee SelectedEmployee
        {
            get { return _selectedEmployee; }
            set
            {
                _selectedEmployee = value;
                OnPropertyChanged(nameof(SelectedEmployee));
            }
        }

        public static EmployeeEvent DetailsWindowEvent { get; set; }
        private string _dataGridVisibility = "Hidden";
        public string DataGridVisibility
        {
            get => _dataGridVisibility;
            set
            {
                _dataGridVisibility = value;
                OnPropertyChanged(nameof(DataGridVisibility));
            }
        }

        private string _progressRingVisibility = "Visible";
        public string ProgressRingVisibility
        {
            get => _progressRingVisibility;
            set
            {
                _progressRingVisibility = value;
                OnPropertyChanged(nameof(ProgressRingVisibility));
            }
        }


        public ICommand DetailsCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ListVM(EmployeeManager employeeManager)
        {
            _employeeManager = employeeManager;
            DetailsCommand = new CustomCommand(GetDetails, CanGetDetails);
            UpdateCommand = new CustomCommand(UpdateDetails, CanUpdateDetails);
            DeleteCommand = new CustomCommand(DeleteEmployee, CanDeleteEmployee);
            AddCommand = new CustomCommand(AddEmployee, CanAddEmployee);
            LoadData();
        }

        private void AddEmployee(object obj)
        {
            DetailsVM.ActionType = ActionType.Add;
            DetailsWindowEvent?.Invoke(SelectedEmployee);
        }

        private bool CanAddEmployee(object obj)
        {
            return true;
        }

        private void DeleteEmployee(object obj)
        {
            DetailsVM.ActionType = ActionType.Delete;
            DetailsWindowEvent?.Invoke(SelectedEmployee);
        }

        private bool CanDeleteEmployee(object obj)
        {
            return true;
        }

        private void UpdateDetails(object obj)
        {
            DetailsVM.ActionType = ActionType.Edit;
            DetailsWindowEvent?.Invoke(SelectedEmployee); 
        }

        private bool CanUpdateDetails(object obj)
        {
            return true;
        }

        private void GetDetails(object obj)
        {
            DetailsVM.ActionType = ActionType.Details;
            DetailsWindowEvent?.Invoke(SelectedEmployee);
        }

        private bool CanGetDetails(object obj)
        {
            return true;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
       
        private async void LoadData()
        {
            var employees = await _employeeManager.GetEmployeesAsync();
            AllEmployees = employees.ToObservableCollection();
            DataGridVisibility = "visible";
            ProgressRingVisibility = "collapsed";
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public delegate void EmployeeEvent(Employee employee);
}