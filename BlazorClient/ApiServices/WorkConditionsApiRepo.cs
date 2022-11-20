using InternationalWagesManager.DAL;
using InternationalWagesManager.Models;
using Newtonsoft.Json;
using System.Text;

namespace BlazorClient.ApiServices
{
    public class WorkConditionsApiRepo : IWConditionsRepository
    {
        private readonly string _url;


        private readonly HttpClient _httpClient;
        public WorkConditionsApiRepo(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _url = configuration.GetSection("BaseAPIUrl").Value! + "/workConditions";
        }

        public async Task<WorkConditions?> GetWorkConditionsAsync(int workConditionId)
        {
            string endPoint = $@"/{workConditionId}";
            var response = await ResponseStatusAndContent(_url + endPoint);
            var workConditions = new WorkConditions();
            if (response.Item1)
                workConditions = JsonConvert.DeserializeObject<WorkConditions>(response.Item2);
            return workConditions;
        }
        public async Task<WorkConditions> GetEmployeesWCToDateAsync(int employeeId, DateTime date)
        {
            throw new NotImplementedException();
        }

        public async Task<List<WorkConditions>> GetAllEmployeesWCAsync(int employeeId)
        {
            string endPoint = $"/employee/{employeeId}";
            var response = await ResponseStatusAndContent(_url + endPoint);
            List<WorkConditions> workConditions = new List<WorkConditions>();
            if (response.Item1)
                workConditions = JsonConvert.DeserializeObject<List<WorkConditions>>(response.Item2)!;
            return workConditions;
        }

        public async Task<int>? AddWorkConditions(WorkConditions workConditions)
        {
            var bodyContent = BodyForRequest(workConditions);
            var response = await _httpClient.PostAsync(_url, bodyContent);
            string responseResult = response.Content.ReadAsStringAsync().Result;

            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<WorkConditions>(responseResult)!;
                return result.Id;
            }
            return 0;
        }

        public async void UpdateWorkConditions(WorkConditions workConditions)
        {
            var bodyContent = BodyForRequest(workConditions);
            string endPoint = "/" + workConditions.Id.ToString();
            await _httpClient.PutAsync(_url + endPoint, bodyContent);

            // to do: validation 

        }

        public async void DeleteWorkConditions(int id)
        {
            string endPoint = "/" + id.ToString();
            await _httpClient.DeleteAsync(_url + endPoint);

            // to do: validation 
        }

        private StringContent BodyForRequest(WorkConditions workConditions)
        {
            var jsonContent = JsonConvert.SerializeObject(workConditions);
            return new StringContent(jsonContent, Encoding.UTF8, "application/json");
        }

        private async Task<(bool, string)> ResponseStatusAndContent(string _url)
        {
            var response = await _httpClient.GetAsync(_url);
            return (response.IsSuccessStatusCode, await response.Content.ReadAsStringAsync());
        }


    }
}
