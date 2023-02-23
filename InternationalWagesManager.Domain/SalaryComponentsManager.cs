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

        public async Task AddSalaryComponentsAsync(DTO.SalaryComponents salaryComponents)
        {
            var modelSalaryComponents = _mapper.Map<DTO.SalaryComponents, Models.SalaryComponents>(salaryComponents);
            if (salaryComponents.EmployeeId != 0 && salaryComponents.TotalHours != 0)
            {
                await _salaryComponentsRepository.AddSalaryComponentsAsync(modelSalaryComponents);
                await _salaryManager.AddSalaryAsync(salaryComponents);
            }
        }

        public async Task<DTO.SalaryComponents> LatestSalaryComponents(int employeeId)
        {
            return (await GetAllEmployeesSC(employeeId)).OrderByDescending(sc => sc.Date).First();
        }

        public async Task<DTO.SalaryComponents> SalaryComponentsToDate(int employeeId, DateTime date)
        {
            var searchByDate = (await GetAllEmployeesSC(employeeId)).FirstOrDefault(sc => sc.Date?.Date == date.Date);
            if (searchByDate == null)
                searchByDate = (await GetAllEmployeesSC(employeeId)).FirstOrDefault(sc => sc.Date?.Year == date.Year && sc.Date?.Month == date.Month);
            if (searchByDate == null)
                searchByDate = (await GetAllEmployeesSC(employeeId)).OrderByDescending(sc => sc.Date).FirstOrDefault(sc => sc.Date?.Year == date.Year);
            return searchByDate ?? new SalaryComponents();
        }
        public async Task UpdateSalaryAsync(SalaryComponents salaryComponents)
        {

        }
        public async Task<bool> DeletedSalarySuccessfullyAsync(int id)
        {
            return true;
        }

        private async Task<List<SalaryComponents>> GetAllEmployeesSC(int employeeId)
        {
            return _mapper.Map<List<Models.SalaryComponents>, List<DTO.SalaryComponents>>
                 (await _salaryComponentsRepository.GetEmployeeSalaryComponentsAsync(employeeId));
        }
    }
}
