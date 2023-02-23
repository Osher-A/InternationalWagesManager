using InternationalWagesManager.DAL;
using InternationalWagesManager.Models;

namespace BlazorClient.ApiServices
{
    public class SalaryComponentsApiRepo : ISalaryComponentsRepository
    {
        public Task AddSalaryComponentsAsync(SalaryComponents newSC)
        {
            throw new NotImplementedException();
        }

        public Task DeleteSalaryComponentsAsync(SalaryComponents SC)
        {
            throw new NotImplementedException();
        }

        public Task<List<SalaryComponents>> GetEmployeeSalaryComponentsAsync(int employeeId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateSalaryComponentsAsync(SalaryComponents SC)
        {
            throw new NotImplementedException();
        }
    }
}
