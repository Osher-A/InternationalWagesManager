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
            if(!string.IsNullOrWhiteSpace(employee.FirstName) && !string.IsNullOrWhiteSpace(employee.LastName)
                && !string.IsNullOrWhiteSpace(employee.Email))

                try
                {
                    await _employeeRepo.AddEmployeeAsync(_mapper.Map<DTO.Employee, Models.Employee>(employee));
                }
                catch (Exception e)
                {
                    ErrorMessage?.Invoke("Database Error! ");
                    throw;
                }
        }

        public void UpdateEmployee(DTO.Employee employee)
        {
            var modelEmployee = _mapper.Map<DTO.Employee, Models.Employee>(employee);
            try
            {
                _employeeRepo.UpdateEmployee(modelEmployee);
                SuccessMessage?.Invoke("Successful operation! ");
            }
            catch (Exception e)
            {
                ErrorMessage?.Invoke("DataBase Error!" );
            }
        }

        public async Task DeleteEmployeeAsync(DTO.Employee employee)
        {
            var modelEmployee = _mapper.Map<DTO.Employee, Models.Employee>(employee);
            var userConfirmation = await AlertFunc?.Invoke("Are you sure you want to delete this Employee's details, with all associated data??")!;
            if (userConfirmation == true)
            {
                try
                {
                    _employeeRepo.DeleteEmployee(modelEmployee);
                    SuccessMessage?.Invoke("Successful operation");
                }
                catch (Exception e)
                {
                    ErrorMessage?.Invoke("Database Error!" );
                    throw;
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
    }
}