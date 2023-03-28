using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InternationalWagesManager.Domain;
using InternationalWagesManager.DTO;
using MyLibrary.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InternationalWagesManager.WPFViewModels.SalaryComponents
{
    public partial class SCDetailsVM : ObservableObject
    {
        private SalaryComponentsManager _salaryComponentsManager;

        [ObservableProperty]
        private DTO.SalaryComponents _salaryComponents = new DTO.SalaryComponents { Date = null };

        // the following id is either an employeeId or a salaryComponents id depending on ActionType
        private int _id;


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
        public SCDetailsVM(int id, ActionType actionType, SalaryComponentsManager salaryComponentsManager)
        {
            _id = id;
            _actionType = actionType;
            _salaryComponentsManager = salaryComponentsManager;
            LoadData();

        }

        private bool CanSubmit(object obj)
        {
            if (SalaryComponents.TotalHours > 0 && SalaryComponents.Date != null)
                return true;

            return false;
        }
        [RelayCommand(CanExecute = nameof(CanSubmit))]
        private async void Submit(object obj)
        {
            switch (_actionType)
            {
                case ActionType.Add:
                    AddSalaryComponents();
                    BackAction?.Invoke();
                    break;
                case ActionType.Edit:
                    await UpdateWorkConditions();
                    BackAction?.Invoke();
                    break;
                default:
                    break;
            };
        }
        [RelayCommand(CanExecute = nameof(CanGoBackToList))]
        private void BackToList(object obj)
        {
            BackAction?.Invoke();
        }

        private bool CanGoBackToList(object obj)
        {
            return true;
        }
        private async void AddSalaryComponents()
        {
            SalaryComponents.EmployeeId = _id;

            await _salaryComponentsManager.AddSalaryComponentsSuccessAsync(SalaryComponents);

            SalaryComponents = new DTO.SalaryComponents { Date = null };
        }

        private async Task UpdateWorkConditions()
        {
            SalaryComponents.Id = _id;

            await _salaryComponentsManager.UpdateSalaryAsync(SalaryComponents);
        }

        private static void OnStaticPropertyChanged(string propertyName)
        {
            if (StaticPropertyChanged != null)
                StaticPropertyChanged.Invoke(null, new PropertyChangedEventArgs(propertyName));
        }

        private async void LoadData()
        {
            if (_actionType == ActionType.Edit)
                await SetUIWorkConditions();
        }
        private async Task SetUIWorkConditions()
        {
            this.SalaryComponents = await _salaryComponentsManager.GetSalaryComponentsAsync(_id);
        }
    }
}

