using InternationalWagesManager.DAL;
using InternationalWagesManager.Domain;
using InternationalWagesManager.DTO;
using MyLibrary.Utilities;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace InternationalWagesManager.ViewModels
{
    public class WorkConditionsVM : INotifyPropertyChanged
    {
        private EmployeeManager _employeeManager;
        private WorkConditionsManager _workConditionsManager;
        private CurrenciesManager _currenciesManager;
        private WorkConditions _workConditions = new WorkConditions { Date = null  };
        private List<Employee> _modelEmployees = new List<Employee>();
        private List<Currency> _modelCurrencies = new List<Currency>(); 

        public List<string> Employees { get; set; }
        public List<string> Currencies { get; set; }

        public WorkConditions WorkConditions
        {
            get { return _workConditions; }
            set
            {
                _workConditions = value;
                OnPropertyChanged(nameof(WorkConditions));
            }
        }
        private string _employeeComboBoxSelectedIndex = "0";
        public string EmployeeComboBoxSelectedIndex
        {
            get { return _employeeComboBoxSelectedIndex; }
            set
            {
                _employeeComboBoxSelectedIndex = value;
                OnPropertyChanged(nameof(EmployeeComboBoxSelectedIndex));
            }
        }
        private string _wageCurrencyComboBoxSelectedIndex = "0";
        public string WageCurrencyComboBoxSelectedIndex
        {
            get { return _wageCurrencyComboBoxSelectedIndex; }
            set
            {
                _wageCurrencyComboBoxSelectedIndex = value;
                OnPropertyChanged(nameof(WageCurrencyComboBoxSelectedIndex));
            }
        }

        private string _expensesCurrencyComboBoxSelectedIndex = "0";
        public string ExpensesCurrencyComboBoxSelectedIndex
        {
            get { return _expensesCurrencyComboBoxSelectedIndex; }
            set
            {
                _expensesCurrencyComboBoxSelectedIndex = value;
                OnPropertyChanged(nameof(ExpensesCurrencyComboBoxSelectedIndex));
            }
        }

        private string _payCurrencyComboBoxSelectedIndex = "0";
        public string PayCurrencyComboBoxSelectedIndex
        {
            get { return _payCurrencyComboBoxSelectedIndex; }
            set
            {
                _payCurrencyComboBoxSelectedIndex = value;
                OnPropertyChanged(nameof(PayCurrencyComboBoxSelectedIndex));
            }
        }

        public ICommand AddCommand { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public WorkConditionsVM(EmployeeManager employeeManager, WorkConditionsManager workConditionsManager, CurrenciesManager currenciesManager)
        {
            _employeeManager = employeeManager;
            _workConditionsManager = workConditionsManager;
            _currenciesManager = currenciesManager;
            AddCommand = new CustomCommand(AddWorkConditions, CanAddWorkConditions);
            LoadData();
        }

        private void AddWorkConditions(object obj)
        {
            int employeeId = _modelEmployees[int.Parse(EmployeeComboBoxSelectedIndex) - 1].Id;
            WorkConditions.EmployeeId = employeeId;
            WorkConditions.WageCurrencyId = _modelCurrencies[int.Parse(WageCurrencyComboBoxSelectedIndex) - 1].Id;
            WorkConditions.ExpensesCurrencyId = _modelCurrencies[int.Parse(ExpensesCurrencyComboBoxSelectedIndex) - 1].Id;
            WorkConditions.PayCurrencyId = _modelCurrencies[int.Parse(PayCurrencyComboBoxSelectedIndex) - 1].Id;
            _workConditionsManager.AddWorkConditions(WorkConditions);
            WorkConditions = new WorkConditions { Date = null };
            EmployeeComboBoxSelectedIndex = "0"; WageCurrencyComboBoxSelectedIndex = "0"; ExpensesCurrencyComboBoxSelectedIndex = "0"; PayCurrencyComboBoxSelectedIndex = "0";
        }

        private bool CanAddWorkConditions(object obj)
        {
            if (EmployeeComboBoxSelectedIndex != "0" && WageCurrencyComboBoxSelectedIndex != "0" 
                && ExpensesCurrencyComboBoxSelectedIndex != "0" && PayCurrencyComboBoxSelectedIndex != "0"
                && WorkConditions.Date != null && WorkConditions.PayRate != 0)
                return true;

            return false;
        }

        private async void LoadData()
        {
            Employees = new List<string>() { "Select a employee!" };

            _modelEmployees = await _employeeManager.GetEmployees();
            foreach (var employee in _modelEmployees)
                Employees.Add(employee.FullName);

            Currencies = new List<string>() { "Select a currency!" };
            _modelCurrencies = _currenciesManager.GetAllCurrencies().ToList();
            foreach(var currency in _modelCurrencies)
                Currencies.Add(currency.Name);
        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
