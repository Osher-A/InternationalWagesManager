using InternationalWagesManager.Domain;
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

namespace InternationalWagesManager.ViewModels
{
    public class StatementVM : INotifyPropertyChanged
    {
        private readonly SalaryManager _salaryManager;
        private readonly PaymentsManager _paymentsManager;
        private readonly StatementManager _balanceManager;
        private readonly EmployeeManager _employeeManager;
        private List<Employee> _employees;

        ObservableCollection<Statement> statements;
        public ObservableCollection<Statement> Statements
        {
            get => statements;
            set
            {
                if (statements == value)
                {
                    return;
                }

                statements = value;
                OnPropertyChanged(nameof(Statements));
            }
        }

        ObservableCollection<Salary> salaries;
        public ObservableCollection<Salary> Salaries
        {
            get => salaries;
            private set
            {
                salaries = value;
                OnPropertyChanged(nameof(Salaries));
            }
        }
        private ObservableCollection<Payment> _payments;
        public ObservableCollection<Payment> Payments
        {
            get => _payments;
            private set
            {
                _payments = value;
                OnPropertyChanged(nameof(Payments));
            }
        }
        decimal balance;
        public decimal Balance
        {
            get => balance;
            private set
            {
                if (balance == value)
                {
                    return;
                }

                balance = value;
                OnPropertyChanged(nameof(Balance));
            }
        }

        public List<string> Employees { get; private set; }

        private string _comboBoxSelectedIndex = "0";

        public event PropertyChangedEventHandler? PropertyChanged;

        public string ComboBoxSelectedIndex
        {
            get { return _comboBoxSelectedIndex; }
            set
            {
                _comboBoxSelectedIndex = value;
                OnPropertyChanged(nameof(ComboBoxSelectedIndex));
            }
        }
        public ICommand GetFullStatementCommand { get; set; }

        public StatementVM(SalaryManager salaryManager, PaymentsManager paymentsManager, StatementManager balanceManager, EmployeeManager employeeManager)
        {
            _salaryManager = salaryManager;
            _paymentsManager = paymentsManager;
            _balanceManager = balanceManager;
            GetFullStatementCommand = new CustomCommand(GetFullStatement, CanGetFullStatement);
            _employeeManager = employeeManager;

            LoadData();
        }

        private void GetFullStatement(object obj)
        {
            int employeeId = _employees[int.Parse(ComboBoxSelectedIndex) - 1].Id;
            Salaries = _salaryManager.GetSalaries(employeeId).ToObservableCollection();
            Payments = _paymentsManager.GetAllPayments(employeeId).ToObservableCollection();
            Balance = _balanceManager.GetCurrentBalance(employeeId);
            Statements = _balanceManager.GetStatements(employeeId).ToObservableCollection();
        }
        
        private bool CanGetFullStatement(object obj)
        {
           return ComboBoxSelectedIndex != "0";
        }
        private async void LoadData()
        {
            Employees = new() { "Select a employee!" };
            _employees = await _employeeManager.GetEmployeesAsync();

            foreach (var employee in _employees)
                Employees.Add(employee.FullName);
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
