using AutoMapper;
using InternationalWagesManager.DAL;
using InternationalWagesManager.Domain;
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
    public class WorkConditionsVM : INotifyPropertyChanged
    {
        private EmployeeManager _employeeManager;
        private WorkConditionsManager _workConditionsManager;
        private WorkConditions _workConditions = new WorkConditions  { Date = null};
        private List<Employee> _modelEmployees = new List<Employee>();

        public List<string> Employees { get; set; } 

        public WorkConditions WorkConditions
        {
            get { return _workConditions; }
            set
            {
                _workConditions = value;
                OnPropertyChanged(nameof(WorkConditions));
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

        public WorkConditionsVM(AutoMapper.IMapper mapper, IEmployeeRepository employeeRepository, IWConditionsRepository wConditionsRepo)
        {
            _employeeManager = new EmployeeManager(mapper, employeeRepository);
            _workConditionsManager = new WorkConditionsManager(mapper, wConditionsRepo);
            AddCommand = new CustomCommand(AddWorkConditions, CanAddWorkConditions);
            LoadData();
        }

        private void AddWorkConditions(object obj)
        {
            int employeeId = _modelEmployees[int.Parse(ComboBoxSelectedIndex) - 1].Id;
            WorkConditions.EmployeeId = employeeId;
            _workConditionsManager.AddWorkConditions(WorkConditions);
        }

        private bool CanAddWorkConditions(object obj)
        {
            if( ComboBoxSelectedIndex != "0" 
                && WorkConditions.Date != null && WorkConditions.PayRate != 0)
                return true;

            return false;
        }

        private void LoadData()
        {
            Employees = new List<string>() { "Select a employee!" };

            _modelEmployees = _employeeManager.GetEmployees().ToList();
            foreach (var employee in _modelEmployees)
                Employees.Add(employee.FullName);
        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
