using AutoMapper;
using InternationalWagesManager.DAL;
using InternationalWagesManager.DTO;
using System.Data;
using System.Transactions;

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

        public async Task<bool> AddSalaryComponentsSuccessAsync(DTO.SalaryComponents salaryComponents)
        {
            var modelSalaryComponents = _mapper.Map<DTO.SalaryComponents, Models.SalaryComponents>(salaryComponents);
            if (salaryComponents.EmployeeId != 0 && salaryComponents.TotalHours != 0)
            {
                // To avoid inserting SalaryComponents to db if the Salary insert would fail 
                using (TransactionScope tran = new TransactionScope())
                {
                    try
                    {
                        if ((await _salaryComponentsRepository.AddSalaryComponentsAsync(modelSalaryComponents)) > 0)
                            if (await _salaryManager.AddSalaryAsync(salaryComponents))
                            {
                                tran.Complete();
                                return true;
                            }
                    }
                    catch (Exception)
                    {
                        DataBaseErrorMessage();
                        throw;
                    }
                }
            }
            return false;
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
            try
            {
                await _salaryComponentsRepository.UpdateSalaryComponentsAsync(_mapper.Map<Models.SalaryComponents>(salaryComponents));
            }
            catch (Exception)
            {
                DataBaseErrorMessage();
                throw;
            }
        }
        public async Task<bool> DeletedSalarySuccessfullyAsync(SalaryComponents salaryComponents)
        {
            if (await MessagesManager.UserConfirmation("Are you sure you want to delete all the salary components!"))
            {
                try
                {
                    await _salaryComponentsRepository.DeleteSalaryComponentsAsync(_mapper.Map<Models.SalaryComponents>(salaryComponents));
                    MessagesManager.SuccessMessage?.Invoke("Successfully deleted!");
                }
                catch (Exception)
                {
                    DataBaseErrorMessage();
                    throw;
                }
                return true;
            }
            return false;

        }

        public async Task<List<SalaryComponents>> GetAllEmployeesSCAsync(int employeeId)
        {
            try
            {
                return _mapper.Map<List<Models.SalaryComponents>, List<DTO.SalaryComponents>>
                         (await _salaryComponentsRepository.GetEmployeeSalaryComponentsAsync(employeeId));
            }
            catch (Exception)
            {
                DataBaseErrorMessage();
                throw;
            }
        }

        private void DataBaseErrorMessage()
        {
            MessagesManager.ErrorMessage?.Invoke("DataBase Error!");
        }
    }
}
