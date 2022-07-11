using AutoMapper;
using InternationalWagesManager.DAL;
using InternationalWagesManager.DTO;
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

        public void UpdateWorkConditions(DTO.WorkConditions workConditions)
        {
            if (workConditions.EmployeeId != 0)
                _wCRepo.UpdateWorkConditions(_mapper.Map<DTO.WorkConditions, Models.WorkConditions>(workConditions));
        }

        public DTO.WorkConditions LatestWorkConditions(int employeeId)
        {
            return GetAllEmployeesWC(employeeId).OrderByDescending(wc => wc.Date).First();
        }

        public DTO.WorkConditions WorkConditionsToDate(int employeeId, DateTime? date)
        {
            var searchByDate = GetAllEmployeesWC(employeeId).FirstOrDefault(wc => wc.Date?.Date == date?.Date);
            if (searchByDate == null)
                searchByDate = GetAllEmployeesWC(employeeId).FirstOrDefault(wc => wc.Date?.Year == date?.Year && wc.Date?.Month == date?.Month);
            if (searchByDate == null)
                searchByDate = GetAllEmployeesWC(employeeId).OrderByDescending(wc => wc.Date).FirstOrDefault(sc => sc.Date?.Year == date?.Year);
            return searchByDate ?? new DTO.WorkConditions();
        }

        private List<WorkConditions> GetAllEmployeesWC(int employeeId)
        {
            return _mapper.Map<List<Models.WorkConditions>, List<DTO.WorkConditions>>
                 (_wCRepo.GetAllWorkConditions(employeeId));
        }
    }
}
