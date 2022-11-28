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

        public WorkConditions GetWorkConditions(int workConditionId)
        {
            var dtoWorkConditions = _mapper.Map<Models.WorkConditions, DTO.WorkConditions>(_wCRepo.GetWorkConditionsAsync(workConditionId).Result);
            return dtoWorkConditions;
        }

        public async Task UpdateWorkConditions(DTO.WorkConditions workConditions)
        {
            if (workConditions.EmployeeId != 0)
               await _wCRepo.UpdateWorkConditionsAsync(_mapper.Map<DTO.WorkConditions, Models.WorkConditions>(workConditions));
        }

        public async Task<bool> DeleteWorkConditionsAsync(int id)
        {
            if (id != 0)
                if (await MessagesManager.UserConfirmation("Are you sure you want to delete these conditions?"))
                    _wCRepo.DeleteWorkConditions(id);

            return false;
        }

        public void DeleteWorkConditions(int id)
        {
            if (id != 0)
                _wCRepo.DeleteWorkConditions(id);
        }

        public async Task<DTO.WorkConditions> LatestWorkConditions(int employeeId)
        {
            return (await GetAllEmployeesWCAsync(employeeId)).OrderByDescending(wc => wc.Date).First();
        }

        public async Task<DTO.WorkConditions> WorkConditionsToDateAsync(int employeeId, DateTime? date)
        {
            var searchByDate = (await GetAllEmployeesWCAsync(employeeId)).FirstOrDefault(wc => wc.Date?.Date == date?.Date);
            if (searchByDate == null)
                searchByDate = (await GetAllEmployeesWCAsync(employeeId)).FirstOrDefault(wc => wc.Date?.Year == date?.Year && wc.Date?.Month == date?.Month);
            if (searchByDate == null)
                searchByDate = (await GetAllEmployeesWCAsync(employeeId)).OrderByDescending(wc => wc.Date).FirstOrDefault(sc => sc.Date?.Year == date?.Year);
            return searchByDate ?? new DTO.WorkConditions();
        }

        public async Task<List<WorkConditions>> GetAllEmployeesWCAsync(int employeeId)
        {
            return _mapper.Map<List<Models.WorkConditions>, List<DTO.WorkConditions>>
                 (await _wCRepo.GetAllEmployeesWCAsync(employeeId));
        }
    }
}
