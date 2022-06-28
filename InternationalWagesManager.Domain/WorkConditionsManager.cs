using AutoMapper;
using InternationalWagesManager.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternationalWagesManager.Domain
{
    public class WorkConditionsManager
    {
        private readonly IMapper _mapper;
        private readonly IWConditionsRepository _wCRepo;

        public WorkConditionsManager(IMapper mapper, IWConditionsRepository wCRepo)
        {
            _mapper = mapper;
            _wCRepo = wCRepo;
        }

        public void AddWorkConditions(DTO.WorkConditions workConditions)
        {
            var modelWorkConditions = _mapper.Map<DTO.WorkConditions, Models.WorkConditions>(workConditions);
            if (workConditions.EmployeeId != 0 && workConditions.PayRate != 0)
                _wCRepo.AddWorkConditions(modelWorkConditions);
        }

        public DTO.WorkConditions GetWorkConditions(int employeeId, DateTime date)
        {
            return _mapper.Map<Models.WorkConditions, DTO.WorkConditions>(_wCRepo.GetWorkConditions(employeeId, date));
        }

        public void UpdateWorkConditions(DTO.WorkConditions workConditions)
        {
            if (workConditions.EmployeeId != 0)
                _wCRepo.UpdateWorkConditions(_mapper.Map<DTO.WorkConditions, Models.WorkConditions>(workConditions));
        }
    }
}
