using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InternationalWagesManager.Domain;
using InternationalWagesManager.DTO;
using InternationalWagesManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibrary.Extentions;
using InternationalWagesManager.WPFViewModels.Enums;

namespace InternationalWagesManager.WPFViewModels.WorkConditions
{
    public partial class EmployeeWConditonsVM : ObservableObject
    {
        private WorkConditionsManager _workConditionsManager;
        [ObservableProperty]
        private DTO.Employee _employee;
        [ObservableProperty]
        private ObservableCollection<DTO.WorkConditions> _allWorkConditions;

        [ObservableProperty]
        private DTO.WorkConditions _selectedWorkConditons;

        [ObservableProperty]
        private string _dataGridVisibility = Visibility.Collapsed.ToString();

        [ObservableProperty]
        private string _progressRingVisibility = Visibility.Visible.ToString();

        public static Action BackAction { get; set; }
        public static Action<int, ActionType> UpdateAction { get; set; }
       

        public EmployeeWConditonsVM(WorkConditionsManager workConditionsManager, DTO.Employee employee)
        {
            _workConditionsManager = workConditionsManager;
            _employee = employee;
            LoadData();
        }

        [RelayCommand(CanExecute = nameof(CanUpdate))]
        private async void Update()
        {
            UpdateAction?.Invoke(SelectedWorkConditons.Id, ActionType.Edit);
        }
        private bool CanUpdate()
        {
            return true;
        }

        [RelayCommand(CanExecute = nameof(CanDelete))]
        private async void Delete()
        {
            if (await MessagesManager.UserConfirmation("Are you sure you want to delete all these details?"))
                _workConditionsManager?.DeleteWorkConditions(_selectedWorkConditons.Id);
            LoadData();
        }
        private bool CanDelete()
        {
            return true;
        }

        [RelayCommand]
        private void GoBack()
        {
            BackAction?.Invoke();
        }

        private async void LoadData()
        {
            AllWorkConditions =  (await _workConditionsManager.GetAllEmployeesWCAsync(_employee.Id)).ToObservableCollection();
            ProgressRingVisibility = Visibility.Collapsed.ToString();
            DataGridVisibility = Visibility.Visible.ToString();
        }

    }
}
