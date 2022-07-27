using InternationalWagesManager.DAL;
using InternationalWagesManager.Domain;
using InternationalWagesManager.DTO;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MyLibrary.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InternationalWagesManager.ViewModels
{
    public class PaymentsVM : INotifyPropertyChanged
    {
        private EmployeeManager _employeeManager;
        private PaymentsManager _paymentsManager;
        private List<Employee> _modelEmployees = new List<Employee>();

        public List<string> Employees { get; set; }

        private Payment _payment = new Payment { Date = null};
        public Payment Payment
        {
            get => _payment;
            set
            {
                _payment = value;
                OnPropertyChanged(nameof(Payment));
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
        public PaymentsVM(EmployeeManager employeeManager, PaymentsManager paymentsManager)
        {
            _employeeManager = employeeManager;
            _paymentsManager = paymentsManager;
            AddCommand = new CustomCommand(AddPayment, CanAddPayment);
            LoadData();
        }

        private void AddPayment(object obj)
        {
            int employeeId = _modelEmployees[int.Parse(ComboBoxSelectedIndex) - 1].Id;
            Payment.EmployeeId = employeeId;
            _paymentsManager.AddPayment(Payment);
        }

        private bool CanAddPayment(object obj)
        {
            if(ComboBoxSelectedIndex != "0"
                && Payment.Date != null && Payment.Amount != null && Payment.Amount != 0)
                return true;

            return false;
        }

        private void LoadData()
        {
            Employees = new() { "Select a employee!" };
            _modelEmployees = _employeeManager.GetEmployees().ToList();

            foreach(var employee in _modelEmployees)
                Employees.Add(employee.FullName);
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
