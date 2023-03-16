using AutoMapper;
using InternationalWagesManager.DAL;
using InternationalWagesManager.DTO;
using System.Data;

namespace InternationalWagesManager.Domain
{
    public class SalaryComponentsManager
    {
        private readonly IMapper _mapper;
        private readonly ISalaryComponentsRepository _salaryComponentsRepository;
        private SalaryManager _salaryManager;

        public SalaryComponentsManager(IMapper mapper, ISalaryComponentsRepository sComponentsRepository,
            ISalaryRepository salaryRepository, IWConditionsRepository wConditionsRepository, ICurrenciesRepository currenciesRepository)
        {
            _mapper = mapper;
            _salaryComponentsRepository = sComponentsRepository;
            _salaryManager = new(_mapper, salaryRepository, wConditionsRepository, currenciesRepository);
        }

        public async Task<DTO.SalaryComponents> GetSalaryComponentsAsync(int id)
        {
            return _mapper.Map<DTO.SalaryComponents>(await _salaryComponentsRepository.GetSalaryComponentsAsync(id));
        }

        public async Task AddSalaryComponentsAsync(DTO.SalaryComponents salaryComponents)
        {
            var modelSalaryComponents = _mapper.Map<DTO.SalaryComponents, Models.SalaryComponents>(salaryComponents);
            if (salaryComponents.EmployeeId != 0 && salaryComponents.TotalHours != 0)
            {
                await _salaryComponentsRepository.AddSalaryComponentsAsync(modelSalaryComponents);
                await _salaryManager.AddSalaryAsync(salaryComponents);
            }
        }

        public async Task<DTO.SalaryComponents> LatestSalaryComponentsAsync(int employeeId)
        {
            return (await GetAllEmployeesSCAsync(employeeId)).OrderByDescending(sc => sc.Date).First();
        }

        public async Task<DTO.SalaryComponents> SalaryComponentsToDateAsync(int employeeId, DateTime date)
        {
            var searchByDate = (await GetAllEmployeesSCAsync(employeeId)).FirstOrDefault(sc => sc.Date?.Date == date.Date);
            if (searchByDate == null)
                searchByDate = (await GetAllEmployeesSCAsync(employeeId)).FirstOrDefault(sc => sc.Date?.Year == date.Year && sc.Date?.Month == date.Month);
            if (searchByDate == null)
                searchByDate = (await GetAllEmployeesSCAsync(employeeId)).OrderByDescending(sc => sc.Date).FirstOrDefault(sc => sc.Date?.Year == date.Year);
            return searchByDate ?? new SalaryComponents();
        }
        public async Task UpdateSalaryAsync(SalaryComponents salaryComponents)
        {
            await _salaryComponentsRepository.UpdateSalaryComponentsAsync(_mapper.Map<Models.SalaryComponents>(salaryComponents));
        }
        public async Task<bool> DeletedSalarySuccessfullyAsync(SalaryComponents salaryComponents)
        {
            //TO DO: Users Confirmation
            await _salaryComponentsRepository.DeleteSalaryComponentsAsync(_mapper.Map<Models.SalaryComponents>(salaryComponents));
            return true;
        }

        public async Task<List<SalaryComponents>> GetAllEmployeesSCAsync(int employeeId)
        {
            return _mapper.Map<List<Models.SalaryComponents>, List<DTO.SalaryComponents>>
                 (await _salaryComponentsRepository.GetEmployeeSalaryComponentsAsync(employeeId));
        }
    }
}
