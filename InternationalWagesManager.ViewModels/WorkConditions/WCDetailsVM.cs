using InternationalWagesManager.Domain;
using InternationalWagesManager.Domain.Utilities;
using InternationalWagesManager.DTO;
using MyLibrary.Utilities;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace InternationalWagesManager.ViewModels.WorkConditons
{
    public class WCDetailsVM : INotifyPropertyChanged
    {
        private WorkConditionsManager _workConditionsManager;
        private CurrenciesManager _currenciesManager;
        private DTO.WorkConditions _workConditions = new DTO.WorkConditions { Date = null };
        private List<Currency> _modelCurrencies = new List<Currency>();

        // the following id is either an employeeId or a workConditions id depending on ActionType
        private int _id; 

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


        private ObservableCollection<WorkConditions> _allWorkConditions;

        private static string _buttonText;
        public static string ButtonText
        {
            get
            {
                switch (_actionType)
                {
                    case ActionType.Details:
                        _buttonText = "Edit";
                        break;
                    case ActionType.Edit:
                        _buttonText = "Submit Changes";
                        break;
                    default:
                        _buttonText = "Submit";
                        break;
                }
                return _buttonText;
            }
            private set
            {
                _buttonText = value;
                OnStaticPropertyChanged(nameof(ButtonText));
            }
        }
        private static ActionType _actionType { get; set; }
        public static Action BackAction { get; set; }
        public ICommand BackCommand { get; set; }
        public ICommand SubmitCommand { get; set; }
        public ICommand AddCommand { get; set;}

        public event PropertyChangedEventHandler? PropertyChanged;
        public static event PropertyChangedEventHandler? StaticPropertyChanged;
        public WCDetailsVM(int id, ActionType actionType,  WorkConditionsManager workConditionsManager, CurrenciesManager currenciesManager)
        {
            _id = id;
            _actionType = actionType;
            _workConditionsManager = workConditionsManager;
            _currenciesManager = currenciesManager;
            LoadData();
            BackCommand = new CustomCommand(BackToList, CanGoBackToList);
            SubmitCommand = new CustomCommand(Submit, CanSubmit);
            AddCommand = new CustomCommand(AddWorkConditions, CanAddWorkConditions);
        }
        private void AddWorkConditions(object obj)
        {
            WorkConditions.EmployeeId = _id;
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
        private bool CanSubmit(object obj)
        {
            return WorkConditions != null;
        }

        private async void Submit(object obj)
        {
            switch (_actionType)
            {
                case ActionType.Details:
                    _actionType = ActionType.Edit;
                    ButtonText = "Submit Changes";
                    break;
                case ActionType.Add:
                    _workConditionsManager.AddWorkConditions(WorkConditions);
                    BackAction?.Invoke();
                    break;
                case ActionType.Edit:
                    await _workConditionsManager.UpdateWorkConditions(WorkConditions);
                    BackAction?.Invoke();
                    break;
                default:
                    break;
            };
        }

        private void DeleteEmployee()
        {
            _workConditionsManager.DeleteWorkConditions(WorkConditions.Id);
            BackAction?.Invoke();
        }

        private void BackToList(object obj)
        {
            BackAction?.Invoke();
        }

        private bool CanGoBackToList(object obj)
        {
            return true;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private static void OnStaticPropertyChanged(string propertyName)
        {
            if (StaticPropertyChanged != null)
                StaticPropertyChanged.Invoke(null, new PropertyChangedEventArgs(propertyName));
        }

        private async void LoadData()
        {
            Currencies = new List<string>() { "Select a currency!" };
            _modelCurrencies = (await _currenciesManager.GetAllCurrencies()).ToList();
            foreach (var currency in _modelCurrencies)
                Currencies.Add(currency.Name);

            if(_actionType == ActionType.Edit)
                this.WorkConditions = _workConditionsManager.GetWorkConditions(_id);
        }
    }
}
