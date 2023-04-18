using AutoMapper;
using InternationalWagesManager.DAL;
using InternationalWagesManager.Models;

namespace InternationalWagesManager.Domain
{
    public class EmployeeManager
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepo;

        public EmployeeManager(IMapper mapper, IEmployeeRepository employeeRepo)
        {
            _mapper = mapper;
            _employeeRepo = employeeRepo;
        }
        public async Task AddEmployeeAsync(DTO.Employee employee)
        {
            if (ValidInput(employee))

                try
                {
                    await _employeeRepo.AddAsync(MapToModel(employee));
                    MessagesManager.SuccessMessage?.Invoke("Successfully added! ");
                }
                catch (Exception)
                {
                    DataBaseErrorMessage();
                    throw;
                }
        }

        public async void UpdateEmployee(DTO.Employee employee)
        {
            try
            {
                await _employeeRepo.UpdateAsync(MapToModel(employee));
                MessagesManager.SuccessMessage?.Invoke("Successful update! ");
            }
            catch (Exception)
            {
                DataBaseErrorMessage();
                throw;
            }
        }


        public async Task DeleteEmployeeAsync(DTO.Employee employee)
        {
            bool confirmed = await UserConfirmation();
            if (confirmed)
            {
                try
                {
                    await _employeeRepo.DeleteAsync(MapToModel(employee));
                    MessagesManager.SuccessMessage?.Invoke("Successfully Deleted");
                }
                catch (Exception)
                {
                    DataBaseErrorMessage();
                    throw;
                }
            }
        }

        public void DeleteEmployee(DTO.Employee employee)
        {
            try
            {
                _employeeRepo.DeleteAsync(MapToModel(employee));
            }
            catch (Exception)
            {
                DataBaseErrorMessage();
                throw;
            }
        }

        public async Task<List<DTO.Employee>> GetEmployeesAsync()
        {
            var listOfEmployees = new List<DTO.Employee>();
            try
            {
                var modelEmployees = await _employeeRepo.GetAllAsync();
                foreach (var employee in modelEmployees)
                {
                    var dtoEmployee = _mapper.Map<Models.Employee, DTO.Employee>(employee);
                    listOfEmployees.Add(dtoEmployee);
                }
                return listOfEmployees;
            }
            catch (Exception)
            {
                DataBaseErrorMessage();
                throw;
            }
        }

        private bool ValidInput(DTO.Employee employee)
        {
            return !string.IsNullOrWhiteSpace(employee.FirstName) && !string.IsNullOrWhiteSpace(employee.LastName)
                && !string.IsNullOrWhiteSpace(employee.Email);
        }
        private Employee MapToModel(DTO.Employee employee)
        {
            return _mapper.Map<Employee>(employee);
        }

        private async Task<bool> UserConfirmation()
        {
            if (MessagesManager.AlertFunc == null)
                return false;
            return await MessagesManager.AlertFunc("Are you sure you want to delete this Employee's details, with all associated data??")!;
        }
        private void DataBaseErrorMessage()
        {
            MessagesManager.ErrorMessage?.Invoke("DataBase Error!");
        }

    }
}