using AutoMapper;
using InternationalWagesManager.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternationalWagesManager.Domain
{
    public class SalaryComponentsManager
    {
        private readonly IMapper _mapper;
        private readonly ISalaryComponentsRepository _salaryComponentsRepository;

        public SalaryComponentsManager(IMapper mapper, ISalaryComponentsRepository sComponentsRepository)
        {
            _mapper = mapper;
            _salaryComponentsRepository = sComponentsRepository;
        }

        public void AddSalaryComponents(DTO.SalaryComponents salaryComponents)
        {
            var modelSalaryComponents = _mapper.Map<DTO.SalaryComponents, Models.SalaryComponents>(salaryComponents);
            if (salaryComponents.EmployeeId != 0 && salaryComponents.TotalHours != 0)
                _salaryComponentsRepository.AddSalaryComponents(modelSalaryComponents);
        }

        public DTO.SalaryComponents GetSalaryComponents(int employeeId, DateTime date)
        {
            return _mapper.Map<Models.SalaryComponents, DTO.SalaryComponents>
                (_salaryComponentsRepository.GetSalaryComponents(employeeId, date));
        }
    }
}
