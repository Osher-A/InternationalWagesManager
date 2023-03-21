using AutoMapper;
using InternationalWagesManager.DAL;
using InternationalWagesManager.DTO;
using Newtonsoft.Json;

namespace InternationalWagesManager.Domain
{
    public class SalaryManager
    {
        private readonly IMapper _mapper;
        private readonly ISalaryRepository _salaryRepo;
        private readonly WorkConditionsManager _workConditionsManager;
        private readonly CurrenciesManager _currenciesManager;
        private SalaryComponents _salaryComponents = new();
        private WorkConditions _workConditions = new();
        private readonly Salary _salary = new();
        private decimal _employeeCurrencyWage;
        private decimal _expenses;
        private string _baseUrl;
        public SalaryManager(IMapper mapper, ISalaryRepository salaryRepo, IWConditionsRepository wConditionsRepository, ICurrenciesRepository currenciesRepository)
        {
            _mapper = mapper;
            _salaryRepo = salaryRepo;
            _workConditionsManager = new WorkConditionsManager(mapper, wConditionsRepository);
            _currenciesManager = new(mapper, currenciesRepository);
        }

        public async Task<List<Salary>> GetSalariesAsync(int employeeId, DateTime? fromDate = null, DateTime? toDate = null)
        {
            var allModelSalaries = _mapper.Map<List<Models.Salary>, List<DTO.Salary>>(await _salaryRepo.GetAllSalariesAsync(employeeId));
            if (fromDate != null && toDate != null)
                return allModelSalaries.Where(s => s.Month >= fromDate && s.Month <= toDate).ToList();
            else if (fromDate != null)
                return allModelSalaries.Where(s => s.Month >= fromDate).ToList();
            else if (toDate != null)
                return allModelSalaries.Where(s => s.Month <= toDate).ToList();

            return allModelSalaries;
        }

        public async Task<bool> AddSalaryAsync(DTO.SalaryComponents salaryComponents)
        {
            // TO DO: Validate that payment currency is not the same as wages or expenses currency

            _salaryComponents = salaryComponents;
            _workConditions = await GetWorkConditionsAsync(salaryComponents.EmployeeId, salaryComponents.Date);
            try
            {
                var employerCurrencyWage = await ApiExchangeRate(await WageEndPoint(), await GetCurrencyName(_workConditions.PayCurrencyId));
                var employerCurrencyExpenses = _salaryComponents.Expenses > 0 ? await ApiExchangeRate(await ExpensesEndPoint(), await GetCurrencyName(_workConditions.PayCurrencyId)) : 0;
                SetUpSalaryData(employerCurrencyWage, employerCurrencyExpenses);
                AddSalaryToRepo();
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            return true;
        }
        private async Task<DTO.WorkConditions> GetWorkConditionsAsync(int employeeId, DateTime? date) =>
           await _workConditionsManager.WorkConditionsToDateAsync(employeeId, date);

        private async Task<decimal> ApiExchangeRate(string endPoint, string payCurrency)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(_baseUrl + endPoint);
                var content = await response.Content.ReadAsStringAsync();
                var result = new ApiCurrency();
                if (response.IsSuccessStatusCode)
                    result = JsonConvert.DeserializeObject<ApiCurrency>(content);
                return ExtractRateProperty(result, payCurrency);
            }
        }
        private decimal ExtractRateProperty(ApiCurrency result, string payCurrency)
        {
            decimal exchangeRate = 0;
            foreach (var prop in result.Rates.GetType().GetProperties())
            {
                if (prop.Name == payCurrency)
                    exchangeRate = (decimal)prop.GetValue(result.Rates);
            }
            return exchangeRate;
        }
        private async Task<string> WageEndPoint()
        {
            var workHours = _salaryComponents.TotalHours + (_salaryComponents.BonusHours ?? 0);
            _employeeCurrencyWage = (decimal)((workHours * _workConditions.PayRate) + (_salaryComponents.BonusWage ?? 0));
            string wageCurrency = await GetCurrencyName(_workConditions.WageCurrencyId);
            string payCurrency = await GetCurrencyName(_workConditions.PayCurrencyId);
            _baseUrl = $"https://api.frankfurter.app/{_salaryComponents.Date?.Date.ToString("yyyy-MM-dd")}";
            return $"?amount={_employeeCurrencyWage.ToString()}&from={wageCurrency}&to={payCurrency}";
        }
        private async Task<string> ExpensesEndPoint()
        {
            _expenses = (decimal)(_salaryComponents.Expenses ?? 0);
            string expensesCurrency = await GetCurrencyName(_workConditions.ExpensesCurrencyId);
            string payCurrency = await GetCurrencyName(_workConditions.PayCurrencyId);
            return $"?amount={_expenses.ToString()}&from={expensesCurrency}&to{payCurrency}";
        }
        private void SetUpSalaryData(decimal employerCurrencyWages, decimal employerCurrencyExpenses)
        {
            _salary.EmployeeId = _salaryComponents.EmployeeId;
            _salary.Month = (DateTime)_salaryComponents.Date;
            decimal deductionAmount = employerCurrencyWages * (decimal)_workConditions.Deductions;
            _salary.Wage = employerCurrencyWages - deductionAmount;
            _salary.Expenses = employerCurrencyExpenses;
            _salary.NetPay = employerCurrencyExpenses + employerCurrencyWages - deductionAmount;
            _salary.GrossPay = employerCurrencyWages + employerCurrencyExpenses;
            _salary.WageRate = _employeeCurrencyWage / employerCurrencyWages;
            _salary.ExpensesRate = employerCurrencyExpenses > 0 ? _expenses / employerCurrencyExpenses : 0;
        }
        private void AddSalaryToRepo()
        {
            var modelSalary = _mapper.Map<DTO.Salary, Models.Salary>(_salary);
            _salaryRepo.AddSalaryAsync(modelSalary);
        }

        private async Task<string> GetCurrencyName(int? currencyId) =>
           await _currenciesManager.GetCurrencyName(currencyId);
    }
}
