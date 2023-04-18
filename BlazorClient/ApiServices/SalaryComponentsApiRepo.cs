using InternationalWagesManager.DAL;
using InternationalWagesManager.Models;
using Newtonsoft.Json;
using System.Text;

namespace BlazorClient.ApiServices
{
    public class SalaryComponentsApiRepo : ISalaryComponentsRepository
    {
        private HttpClient _httpClient;
        private string _url;
        public SalaryComponentsApiRepo(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _url = configuration.GetSection("BaseAPIUrl").Value! + "/salaryComponents";
        }

        public async Task<List<SalaryComponents>> GetEmployeeSalaryComponentsAsync(int employeeId)
        {
            var allSalaryComponents = new List<SalaryComponents>();
            string endPoint = $@"/all/{employeeId}";
            var response = await _httpClient.GetAsync(_url + endPoint);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                allSalaryComponents = JsonConvert.DeserializeObject<List<SalaryComponents>>(content);
            }
            return allSalaryComponents!;
        }

        public async Task<SalaryComponents> GetByIdAsync(int id)
        {
            var salaryComponents = new SalaryComponents();
            string endPoint = $@"/{id}";
            var response = await _httpClient.GetAsync(_url + endPoint);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                salaryComponents = JsonConvert.DeserializeObject<SalaryComponents>(content);
            }
            return salaryComponents!;
        }
        public async Task<int> AddAsync(SalaryComponents newSC)
        {
            var json = JsonConvert.SerializeObject(newSC);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_url, content);
            if (response.IsSuccessStatusCode)
            {
                string responseResult = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<SalaryComponents>(responseResult);
                return result.Id;
            }
            return 0;
        }

        public async Task UpdateAsync(SalaryComponents SC)
        {
            string endPoint = $"/{SC.Id}";
            var jsonBody = JsonConvert.SerializeObject(SC);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(_url + endPoint, content);

        }

        public async Task DeleteAsync(SalaryComponents SC)
        {
            string endPoint = $"/{SC.Id}";
            var response = await _httpClient.DeleteAsync(_url + endPoint);

        }

        public Task<List<SalaryComponents>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
