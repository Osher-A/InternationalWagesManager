using AutoMapper;
using InternationalWagesManager.DAL;

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

        public void AddEmployee(DTO.Employee employee)
        {
            if(!string.IsNullOrWhiteSpace(employee.FirstName) && !string.IsNullOrWhiteSpace(employee.LastName)
                && !string.IsNullOrWhiteSpace(employee.Email))
                _employeeRepo.AddEmployee(_mapper.Map<DTO.Employee, Models.Employee>(employee));
        }

        public void UpdateEmployee(DTO.Employee employee)
        {
            var modelEmployee = _mapper.Map<DTO.Employee, Models.Employee>(employee);
            _employeeRepo.UpdateEmployee(modelEmployee);
        }
        
        public IEnumerable<DTO.Employee> GetEmployees()
        {
            var modelEmployees =_employeeRepo.GetEmployees();
            foreach (var employee in modelEmployees)
            {
                var dtoEmployee = _mapper.Map<Models.Employee, DTO.Employee>(employee);
                yield return dtoEmployee;
            }
        }
    }
}