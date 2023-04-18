using AutoMapper;
using InternationalWagesManager.DAL;
using InternationalWagesManager.DTO;

namespace InternationalWagesManager.Domain;

public class WorkConditionsManager
{
    private readonly IMapper _mapper;
    private readonly IWConditionsRepository _wCRepo;

    public WorkConditionsManager(IMapper mapper, IWConditionsRepository wCRepo)
    {
        _mapper = mapper;
        _wCRepo = wCRepo;
    }

    public WorkConditions GetWorkConditions(int workConditionId)
    {
        var dtoWorkConditions = _mapper.Map<Models.WorkConditions, DTO.WorkConditions>(_wCRepo.GetByIdAsync(workConditionId).Result);
        return dtoWorkConditions;
    }

    public async Task<WorkConditions> GetWorkConditionsAsync(int workConditionId)
    {
        var dtoWorkConditions = _mapper.Map<Models.WorkConditions, DTO.WorkConditions>(await _wCRepo.GetByIdAsync(workConditionId));
        return dtoWorkConditions;
    }
    public async Task<DTO.WorkConditions> LatestWorkConditions(int employeeId)
    {
        return (await GetAllEmployeesWCAsync(employeeId)).OrderByDescending(wc => wc.Date).First();
    }

    public async Task<DTO.WorkConditions> WorkConditionsToDateAsync(int employeeId, DateTime? date)
    {
        var searchByDate = (await GetAllEmployeesWCAsync(employeeId)).FirstOrDefault(wc => wc.Date?.Date == date?.Date);
        if (searchByDate == null)
            searchByDate = (await GetAllEmployeesWCAsync(employeeId)).OrderByDescending(wc => wc.Date).FirstOrDefault(wc => wc.Date?.Year == date?.Year && wc.Date?.Month == date?.Month);
        if (searchByDate == null)
            searchByDate = (await GetAllEmployeesWCAsync(employeeId)).OrderByDescending(wc => wc.Date).FirstOrDefault(sc => sc.Date?.Year == date?.Year);
        return searchByDate ?? await LatestWorkConditions(employeeId);
    }

    public async Task<List<WorkConditions>> GetAllEmployeesWCAsync(int employeeId)
    {
        try
        {
            return _mapper.Map<List<Models.WorkConditions>, List<DTO.WorkConditions>>
                     (await _wCRepo.GetAllEmployeesWCAsync(employeeId));
        }
        catch (Exception)
        {
            DataBaseErrorMessage();
            throw;
        }

    }
    public void AddWorkConditions(DTO.WorkConditions workConditions)
    {
        ClearingAllRelatedObjects(workConditions);
        var modelWorkConditions = _mapper.Map<DTO.WorkConditions, Models.WorkConditions>(workConditions);
        try
        {
            if (workConditions.EmployeeId != 0 && workConditions.PayRate != 0
                && workConditions.PayCurrencyId != 0 && workConditions.WageCurrencyId != 0 && workConditions.ExpensesCurrencyId != 0)
            {
                _wCRepo.AddAsync(modelWorkConditions);
                MessagesManager.SuccessMessage?.Invoke("Successfully added! ");
            }

        }
        catch (Exception)
        {
            DataBaseErrorMessage();
            throw;
        }

    }
    public async Task AddWorkConditionsAsync(DTO.WorkConditions workConditions)
    {
        ClearingAllRelatedObjects(workConditions);
        var modelWorkConditions = _mapper.Map<DTO.WorkConditions, Models.WorkConditions>(workConditions);
        try
        {
            if (workConditions.EmployeeId != 0 && workConditions.PayRate != 0
                 && workConditions.PayCurrencyId != 0 && workConditions.WageCurrencyId != 0 && workConditions.ExpensesCurrencyId != 0)
            {
                await _wCRepo.AddAsync(modelWorkConditions);
                MessagesManager.SuccessMessage?.Invoke("Successfully added! ");
            }

        }
        catch (Exception)
        {
            DataBaseErrorMessage();
            throw;
        }

    }

    public async Task UpdateWorkConditionsAsync(DTO.WorkConditions workConditions)
    {
        try
        {
            if (workConditions.Id != 0)
            {
                await _wCRepo.UpdateAsync(_mapper.Map<Models.WorkConditions>(workConditions));
                MessagesManager.SuccessMessage?.Invoke("Successful update! ");
            }

        }
        catch (Exception e)
        {
            DataBaseErrorMessage();
            throw;
        }
    }

    public async Task<bool> DeleteWorkConditionsSuccesfulAsync(int id)
    {

        try
        {
            if (id != 0)
                if (await MessagesManager.UserConfirmation("Are you sure you want to delete these conditions?"))
                {
                    await _wCRepo.DeleteByIdAsync(id);
                    MessagesManager.SuccessMessage?.Invoke("Successfully Deleted");
                    return true;
                }
        }
        catch (Exception)
        {
            DataBaseErrorMessage();
            throw;
        }
        return false;
    }

    public async Task DeleteWorkConditionsAsync(int id)
    {
        try
        {
            if (id != 0)
                await _wCRepo.DeleteByIdAsync(id);
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void ClearingAllRelatedObjects(DTO.WorkConditions workConditions)
    {
        workConditions.Employee = null;
        workConditions.ExpensesCurrency = null;
        workConditions.WageCurrency = null;
        workConditions.PayCurrency = null;
        workConditions.ExpensesCurrency = null;

    }
    private void DataBaseErrorMessage()
    {
        MessagesManager.ErrorMessage?.Invoke("DataBase Error!");
    }
}
