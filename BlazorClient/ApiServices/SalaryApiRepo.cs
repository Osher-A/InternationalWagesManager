using InternationalWagesManager.DAL;
using InternationalWagesManager.Models;
using Newtonsoft.Json;
using System.Text;

namespace BlazorClient.ApiServices
{
    public class SalaryApiRepo : ISalaryRepository
    {
        private HttpClient _httpClient;
        private string _url;
        public SalaryApiRepo(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _url = configuration.GetSection("BaseAPIUrl").Value! + "/salaries";
        }
        public async Task<int> AddSalaryAsync(Salary salary)
        {
            var json = JsonConvert.SerializeObject(salary);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_url , content);
            if (response.IsSuccessStatusCode)
            {
                string responseResult = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Salary>(responseResult);
                return result.Id;
            }
            return 0; 
        }

        public async Task DeleteSalaryAsync(Salary salary)
        {
            string endPoint = $"{salary.Id}";
            var response = await _httpClient.DeleteAsync(_url + endPoint);
        }

        public async Task<List<Salary>> GetAllSalariesAsync(int employeeId)
        {
            var allSalaries = new List<Salary>();
            string endPoint = $@"/all/{employeeId}";
            var response = await _httpClient.GetAsync(_url + endPoint);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                allSalaries = JsonConvert.DeserializeObject<List<Salary>>(content);
            }
            return allSalaries!;
        }

        public async Task<Salary> GetSalaryAsync(int id)
        {
            var salary = new Salary();
            string endPoint = $@"/{id}";
            var response = await _httpClient.GetAsync(_url + endPoint);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                salary = JsonConvert.DeserializeObject<Salary>(content);
            }
            return salary!;
        }
    }
}
