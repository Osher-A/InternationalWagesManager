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
        public EmployeeManager(IMapper mapper, IEmployeeRepository employeeRepo)
        {
            _mapper = mapper;

             _employeeRepo = employeeRepo;
        }

        public async Task AddEmployeeAsync(DTO.Employee employee)
        {
            if(!string.IsNullOrWhiteSpace(employee.FirstName) && !string.IsNullOrWhiteSpace(employee.LastName)
                && !string.IsNullOrWhiteSpace(employee.Email))
               await _employeeRepo.AddEmployeeAsync(_mapper.Map<DTO.Employee, Models.Employee>(employee));
        }

        public void UpdateEmployee(DTO.Employee employee)
        {
            var modelEmployee = _mapper.Map<DTO.Employee, Models.Employee>(employee);
            _employeeRepo.UpdateEmployee(modelEmployee);
        }

        public void DeleteEmployee(DTO.Employee employee)
        {
            var modelEmployee = _mapper.Map<DTO.Employee, Models.Employee>(employee);
            _employeeRepo.DeleteEmployee(modelEmployee);
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