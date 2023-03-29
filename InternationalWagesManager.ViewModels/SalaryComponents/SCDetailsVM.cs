using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InternationalWagesManager.Domain;
using InternationalWagesManager.DTO;
using MyLibrary.Utilities;
using System.ComponentModel;
using System.Windows.Input;

namespace InternationalWagesManager.WPFViewModels.SalaryComponents
{
    public partial class SCDetailsVM : ObservableObject
    {
        private SalaryComponentsManager _salaryComponentsManager;
        private EmployeeManager _employeeManager;

        [ObservableProperty]
        private DTO.SalaryComponents _salaryComponents = new DTO.SalaryComponents { Date = null };
        [ObservableProperty]
        private Employee _employee;


        private static string _buttonText;
        public static string ButtonText
        {
            get
            {
                switch (_actionType)
                {
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

        public event PropertyChangedEventHandler? PropertyChanged;
        public static event PropertyChangedEventHandler? StaticPropertyChanged;

        public ICommand SubmitCommand { get; set; }
        public SCDetailsVM(ActionType actionType, SalaryComponentsManager salaryComponentsManager, EmployeeManager employeeManager, DTO.Employee employee, DTO.SalaryComponents salaryComponents)
        {
            _employee = employee;
            _salaryComponents = salaryComponents != null ? salaryComponents : _salaryComponents;
            _actionType = actionType;
            _salaryComponentsManager = salaryComponentsManager;
            _employeeManager = employeeManager;
            SubmitCommand = new CustomCommand(Submit, CanSubmit);
        }

        private bool CanSubmit(object obj)
        {
            if (SalaryComponents.TotalHours > 0 && SalaryComponents.Date != null)
                return true;

            return false;
        }
        private async void Submit(object obj)
        {
            switch (_actionType)
            {
                case ActionType.Add:

                    await AddSalaryComponents();
                    BackAction?.Invoke();         //Since its within a void method it'll invoke before task completion, causing a database error
                    break;
                case ActionType.Edit:
                    await UpdateSalaryComponents();
                    BackAction?.Invoke();
                    break;
                default:
                    break;
            };
        }
        [RelayCommand(CanExecute = nameof(CanGoBackToList))]
        private void Back(object obj)
        {
            BackAction?.Invoke();
        }

        private bool CanGoBackToList(object obj)
        {
            return true;
        }
        private async Task AddSalaryComponents()
        {
            SalaryComponents.EmployeeId = _employee.Id;

            await _salaryComponentsManager.AddSalaryComponentsSuccessAsync(SalaryComponents);

            SalaryComponents = new DTO.SalaryComponents();
        }

        private async Task UpdateSalaryComponents()
        {
            await _salaryComponentsManager.UpdateSalaryAsync(SalaryComponents);
        }

        private static void OnStaticPropertyChanged(string propertyName)
        {
            if (StaticPropertyChanged != null)
                StaticPropertyChanged.Invoke(null, new PropertyChangedEventArgs(propertyName));
        }


    }
}

