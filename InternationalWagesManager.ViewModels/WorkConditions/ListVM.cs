﻿using InternationalWagesManager.Domain;
using InternationalWagesManager.DTO;
using InternationalWagesManager.ViewModels.Employees;
using MyLibrary.Extentions;
using MyLibrary.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InternationalWagesManager.WPFViewModels.WorkConditions
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
        private Employee _selectedEmployee = new Employee();
        public Employee SelectedEmployee
        {
            get { return _selectedEmployee; }
            set
            {
                _selectedEmployee = value;
                OnPropertyChanged(nameof(SelectedEmployee));
            }
        }

        public static WorkConditonsEvent DetailsWindowEvent { get; set; }
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
        public ICommand AddCommand { get; set; }
        public ListVM(EmployeeManager employeeManager)
        {
            _employeeManager = employeeManager;
            DetailsCommand = new CustomCommand(GetDetails, canExecute: (object obj) => true);
            AddCommand = new CustomCommand(AddWorkConditions, canExecute: (object obj) => true);
            LoadData();
        }

        private void AddWorkConditions(object obj)
        {
            DetailsVM.ActionType = ActionType.Add;
            DetailsWindowEvent?.Invoke(SelectedEmployee);
        }

        private void GetDetails(object obj)
        {
            DetailsVM.ActionType = ActionType.Details;
            DetailsWindowEvent?.Invoke(SelectedEmployee);
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

    public delegate void WorkConditonsEvent(Employee employee);
}
