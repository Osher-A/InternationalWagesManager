using AutoMapper;
using InternationalWagesManager.DAL;
using InternationalWagesManager.Models;
using Microsoft.EntityFrameworkCore;

namespace InternationalWagesManager.Domain
{
    public class EmployeeManager
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepo;

        public Func<string, Task<bool>> AlertFunc { get; set; }
        public Action<string> SuccessMessage { get; set; }
        public Action<string> ErrorMessage { get; set; }
        public EmployeeManager(IMapper mapper, IEmployeeRepository employeeRepo)
        {
            _mapper = mapper;
            _employeeRepo = employeeRepo;
        }

        public async Task AddEmployeeAsync(DTO.Employee employee)
        {
            if(ValidInput(employee))

                try
                {
                    await _employeeRepo.AddEmployeeAsync(MapToModel(employee));
                    SuccessMessage?.Invoke("Successfully added! ");
                }
                catch (Exception e)
                {
                    ErrorMessage?.Invoke("Database Error! ");
                }
        }

        public void UpdateEmployee(DTO.Employee employee)
        {
            try
            {
                _employeeRepo.UpdateEmployee(MapToModel(employee));
                SuccessMessage?.Invoke("Successful update! ");
            }
            catch (Exception e)
            {
                ErrorMessage?.Invoke("DataBase Error!");
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
                    SuccessMessage?.Invoke("Successfully Deleted");
                }
                catch (Exception e)
                {
                    ErrorMessage?.Invoke("Database Error!");
                }
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
            if(AlertFunc == null)
                return false;
            return await AlertFunc("Are you sure you want to delete this Employee's details, with all associated data??")!;
        }

    }
}