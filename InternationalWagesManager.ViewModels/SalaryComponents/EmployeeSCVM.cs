using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InternationalWagesManager.Domain;
using InternationalWagesManager.Domain.Utilities;
using InternationalWagesManager.DTO;
using InternationalWagesManager.WPFViewModels.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternationalWagesManager.WPFViewModels.SalaryComponents
{
    public partial class EmployeeSCVM : ObservableObject
    {
        private SalaryComponentsManager _salaryComponentsManager;
        [ObservableProperty]
        private DTO.Employee _employee;
        [ObservableProperty]
        private ObservableCollection<DTO.SalaryComponents> _allSalaryComponents;

        [ObservableProperty]
        private DTO.SalaryComponents _selectedSalaryComponents;

        [ObservableProperty]
        private string _dataGridVisibility = Visibility.Collapsed.ToString();

        [ObservableProperty]
        private string _progressRingVisibility = Visibility.Visible.ToString();

        public static Action BackAction { get; set; }
        public static Action<DTO.SalaryComponents, DTO.Employee, ActionType> UpdateAction { get; set; }


        public EmployeeSCVM(SalaryComponentsManager salaryComponentsManager, DTO.Employee employee)
        {
            _salaryComponentsManager = salaryComponentsManager;
            _employee = employee;
            LoadData();
        }

        [RelayCommand(CanExecute = nameof(CanUpdate))]
        private void Update()
        {
            UpdateAction?.Invoke(_selectedSalaryComponents, _employee, ActionType.Edit);
        }
        private bool CanUpdate()
        {
            return true;
        }

        [RelayCommand(CanExecute = nameof(CanDelete))]
        private async Task Delete()
        {
            await _salaryComponentsManager?.DeletedSalarySuccessfullyAsync(_selectedSalaryComponents);
            LoadData();
        }
        private bool CanDelete()
        {
            return true;
        }

        [RelayCommand]
        private void Back()
        {
            BackAction?.Invoke();
        }

        private async void LoadData()
        {
            AllSalaryComponents = (await _salaryComponentsManager.GetAllEmployeesSCAsync(_employee.Id)).ToObservableCollection();
            ProgressRingVisibility = Visibility.Collapsed.ToString();
            DataGridVisibility = Visibility.Visible.ToString();
        }

    }
}

