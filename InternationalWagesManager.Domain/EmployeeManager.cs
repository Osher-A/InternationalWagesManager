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
                    await _employeeRepo.AddEmployeeAsync(MapToModel(employee));
                    MessagesManager.SuccessMessage?.Invoke("Successfully added! ");
                }
                catch (Exception)
                {
                    MessagesManager.ErrorMessage?.Invoke("Database Error! ");
                }
        }

        public void UpdateEmployee(DTO.Employee employee)
        {
            try
            {
                _employeeRepo.UpdateEmployee(MapToModel(employee));
                MessagesManager.SuccessMessage?.Invoke("Successful update! ");
            }
            catch (Exception)
            {
                MessagesManager.ErrorMessage?.Invoke("DataBase Error!");
            }
        }


        public async Task DeleteEmployeeAsync(DTO.Employee employee)
        {
            bool confirmed = await UserConfirmation();
            if (confirmed)
            {
                try
                {
                    _employeeRepo.DeleteEmployee(MapToModel(employee));
                    MessagesManager.SuccessMessage?.Invoke("Successfully Deleted");
                }
                catch (Exception)
                {
                    MessagesManager.ErrorMessage?.Invoke("Database Error!");
                }
            }
        }

        public void DeleteEmployee(DTO.Employee employee)
        {
            try
            {
                _employeeRepo.DeleteEmployee(MapToModel(employee));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<DTO.Employee>> GetEmployeesAsync()
        {
            var listOfEmployees = new List<DTO.Employee>();
            var modelEmployees = await _employeeRepo.GetEmployeesAsync();
            foreach (var employee in modelEmployees)
            {
                var dtoEmployee = _mapper.Map<Models.Employee, DTO.Employee>(employee);
                listOfEmployees.Add(dtoEmployee);
            }
            return listOfEmployees;
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

    }
}