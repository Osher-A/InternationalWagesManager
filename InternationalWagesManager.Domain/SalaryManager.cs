using AutoMapper;
using InternationalWagesManager.DAL;
using InternationalWagesManager.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace InternationalWagesManager.Domain
{
    public class SalaryManager
    {
        private readonly IMapper _mapper;
        private readonly ISalaryRepository _salaryRepo;
        private readonly WorkConditionsManager _workConditionsManager;
        private SalaryComponents _salaryComponents = new ();
        private WorkConditions _workConditions = new();
        private readonly Salary _salary = new ();
        private decimal _employeeCurrencyWage;
        private decimal _expenses;
        private string _baseUrl;
        public SalaryManager(IMapper mapper, ISalaryRepository salaryRepo, IWConditionsRepository wConditionsRepository)
        {
            _mapper = mapper;
            _salaryRepo = salaryRepo;
            _workConditionsManager = new WorkConditionsManager(mapper, wConditionsRepository);
        }

        public async Task AddSalary(DTO.SalaryComponents salaryComponents)
        {
            _salaryComponents = salaryComponents;
            _workConditions = GetWorkConditions(salaryComponents.EmployeeId, salaryComponents.Date);
            var employerCurrencyWage = await ApiExchangeRate(WageEndPoint(), _workConditions.PayCurrency.Name);
            var employerCurrencyExpenses = await ApiExchangeRate(ExpensesEndPoint(), _workConditions.PayCurrency.Name);
            SetUpSalaryData(employerCurrencyWage, employerCurrencyExpenses);
            AddSalaryToRepo();
        }
        private DTO.WorkConditions GetWorkConditions(int employeeId, DateTime? date) =>
            _workConditionsManager.WorkConditionsToDate(employeeId, date);
        
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
        private string WageEndPoint()
        {
            var workHours = _salaryComponents.TotalHours + _salaryComponents.BonusHours;
            _employeeCurrencyWage = (decimal)((workHours * _workConditions.PayRate) + _salaryComponents.BonusWage);
            string wageCurrency = _workConditions.WageCurrency.Name;
            string payCurrency = _workConditions.PayCurrency.Name;
            _baseUrl = $"http://api.frankfurter.app/{_salaryComponents.Date?.Date.ToString("yyyy-MM-dd")}";
            return $"?amount={_employeeCurrencyWage.ToString()}&from={wageCurrency}&to={payCurrency}";
        }
        private string ExpensesEndPoint()
        {
            _expenses = (decimal)_salaryComponents.Expenses;
            string expensesCurrency = _workConditions.ExpensesCurrency.Name;
            string payCurrency = _workConditions.PayCurrency.Name;
            return $"?amount={_expenses.ToString()}&from={expensesCurrency}&to{payCurrency}";
        }
        private void SetUpSalaryData(decimal employerCurrencyWages, decimal employerCurrencyExpenses)
        {
            decimal deductionAmount = employerCurrencyWages * (decimal)_workConditions.Deductions;
            _salary.Wage = employerCurrencyWages - deductionAmount;
            _salary.Expenses = employerCurrencyExpenses;
            _salary.NetPay = employerCurrencyExpenses + employerCurrencyWages - deductionAmount;
            _salary.GrossPay = employerCurrencyWages + employerCurrencyExpenses;
            _salary.WagesPayRate = _employeeCurrencyWage / employerCurrencyWages;
            _salary.ExpensesPayRate = _expenses / employerCurrencyExpenses;
        }
        private void AddSalaryToRepo()
        {
            var modelSalary = _mapper.Map<DTO.Salary, Models.Salary>(_salary);
            _salaryRepo.AddSalary(modelSalary);
        }
    }
}
