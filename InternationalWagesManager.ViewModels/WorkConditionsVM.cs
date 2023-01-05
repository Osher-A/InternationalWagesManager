using InternationalWagesManager.Domain;
using InternationalWagesManager.DTO;
using MyLibrary.Utilities;
using System.ComponentModel;
using System.Windows.Input;

namespace InternationalWagesManager.WPFViewModels
{
    public class WorkConditionsVM : INotifyPropertyChanged
    {
        private WorkConditionsManager _workConditionsManager;
        private CurrenciesManager _currenciesManager;
        private DTO.WorkConditions _workConditions = new DTO.WorkConditions { Date = null };
        private List<Currency> _modelCurrencies = new List<Currency>();
        private int _employeeId;

        public List<string> Currencies { get; set; }

        public DTO.WorkConditions WorkConditions
        {
            get { return _workConditions; }
            set
            {
                _workConditions = value;
                OnPropertyChanged(nameof(WorkConditions));
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

        public WorkConditionsVM(int employeeId, WorkConditionsManager workConditionsManager, CurrenciesManager currenciesManager)
        {
            _employeeId = employeeId;
            _workConditionsManager = workConditionsManager;
            _currenciesManager = currenciesManager;
            AddCommand = new CustomCommand(AddWorkConditions, CanAddWorkConditions);
            LoadData();
        }

        private void AddWorkConditions(object obj)
        {
            WorkConditions.EmployeeId = _employeeId;
            WorkConditions.WageCurrencyId = _modelCurrencies[int.Parse(WageCurrencyComboBoxSelectedIndex) - 1].Id;
            WorkConditions.ExpensesCurrencyId = _modelCurrencies[int.Parse(ExpensesCurrencyComboBoxSelectedIndex) - 1].Id;
            WorkConditions.PayCurrencyId = _modelCurrencies[int.Parse(PayCurrencyComboBoxSelectedIndex) - 1].Id;
            _workConditionsManager.AddWorkConditions(WorkConditions);
            WorkConditions = new DTO.WorkConditions { Date = null };
            WageCurrencyComboBoxSelectedIndex = "0"; ExpensesCurrencyComboBoxSelectedIndex = "0"; PayCurrencyComboBoxSelectedIndex = "0";
        }

        private bool CanAddWorkConditions(object obj)
        {
            if (WageCurrencyComboBoxSelectedIndex != "0"
                && ExpensesCurrencyComboBoxSelectedIndex != "0" && PayCurrencyComboBoxSelectedIndex != "0"
                && WorkConditions.Date != null && WorkConditions.PayRate != 0)
                return true;

            return false;
        }

        private async void LoadData()
        {
            Currencies = new List<string>() { "Select a currency!" };
            _modelCurrencies = (await _currenciesManager.GetAllCurrencies()).ToList();
            foreach (var currency in _modelCurrencies)
                Currencies.Add(currency.Name);
        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
