using InternationalWagesManager.DTO;
using MyLibrary.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InternationalWagesManager.ViewModels.Employees
{
    internal class AddVM
    {
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

        public ICommand CancelCommand { get; set; }
        public ICommand DeleteCommand { get; set; } 

        public static Action CancelAction { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;

        public AddVM(Employee selectedEmployee)
        {
            SelectedEmployee = selectedEmployee;
            CancelCommand = new CustomCommand(Cancel, CanCancel);
            DeleteCommand = new CustomCommand(Delete, CanDelete);
        }

        private bool CanDelete(object obj)
        {
            return true;
        }

        private void Delete(object obj)
        {
            throw new NotImplementedException();
        }

        private void Cancel(object obj)
        {
            CancelAction?.Invoke();
        }

        private bool CanCancel(object obj)
        {
            return true;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
