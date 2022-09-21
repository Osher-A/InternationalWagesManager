using InternationalWagesManager.Domain;
using InternationalWagesManager.DTO;
using MyLibrary.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InternationalWagesManager.ViewModels.Employees
{
    public class DetailsVM : INotifyPropertyChanged
    {
        private EmployeeManager _employeeManager;
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

        private static string _buttonText;
        public static string ButtonText
        {
            get
            { 
                switch (ActionType)
                {
                    case ActionType.Details:
                        _buttonText = "Edit";
                        break;
                    case ActionType.Delete:
                        _buttonText = "Confirm";
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
        public static ActionType ActionType { get; set; }
        public static Action BackAction { get; set; }
        public ICommand BackCommand { get; set; }
        public ICommand SubmitCommand { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        public static event PropertyChangedEventHandler? StaticPropertyChanged;
        public DetailsVM(Employee selectedEmployee, EmployeeManager employeeManager)
        {
            SelectedEmployee = selectedEmployee;
            _employeeManager = employeeManager;
            BackCommand = new CustomCommand(BackToList, CanGoBackToList);
            SubmitCommand = new CustomCommand(Submit, CanSubmit);
        }

        private bool CanSubmit(object obj)
        {
            return SelectedEmployee != null && !string.IsNullOrWhiteSpace(SelectedEmployee.FullName)
                && !string.IsNullOrWhiteSpace(SelectedEmployee.Email);
        }

        private async void Submit(object obj)
        {
            switch (ActionType)
            {
                case ActionType.Details:
                    ActionType = ActionType.Edit;
                    ButtonText = "Submit Changes";
                    break;
                case ActionType.Add:
                  await  _employeeManager.AddEmployeeAsync(SelectedEmployee);
                       BackAction?.Invoke() ;
                    break;
                case ActionType.Edit:
                    _employeeManager.UpdateEmployee(SelectedEmployee);
                    BackAction?.Invoke();
                    break;
                case ActionType.Delete:
                    DeleteEmployee();
                    break;
                default:
                    break;
            } ;
        }

        private async void DeleteEmployee()
        {
          await _employeeManager.DeleteEmployeeAsync(SelectedEmployee);
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
    }
}
