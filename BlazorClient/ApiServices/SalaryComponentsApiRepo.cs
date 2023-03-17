﻿using InternationalWagesManager.DAL;
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
            string endPoint = $@"/AllSalaryComponents/{employeeId}";
            var response = await _httpClient.GetAsync(_url + endPoint);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                allSalaryComponents = JsonConvert.DeserializeObject<List<SalaryComponents>>(content);
            }
            return allSalaryComponents!;
        }

        public async Task<SalaryComponents> GetSalaryComponentsAsync(int id)
        {
            var salaryComponents = new SalaryComponents();
            string endPoint = $@"/GetSalaryComponents/{id}";
            var response = await _httpClient.GetAsync(_url + endPoint);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                salaryComponents = JsonConvert.DeserializeObject<SalaryComponents>(content);
            }
            return salaryComponents!;
        }
        public async Task<int> AddSalaryComponentsAsync(SalaryComponents newSC)
        {
            string endPoint = "/PostSalaryComponents";
            var json = JsonConvert.SerializeObject(newSC);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_url + endPoint, content);
            if (response.IsSuccessStatusCode)
            {
                string responseResult = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<SalaryComponents>(responseResult);
                return result.Id;
            }
            return 0;
        }

        public async Task UpdateSalaryComponentsAsync(SalaryComponents SC)
        {
            string endPoint = $"/PutSalaryComponents/{SC.Id}";
            var jsonBody = JsonConvert.SerializeObject(SC);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(_url + endPoint, content);

            // To do: Validation Messages
        }

        public async Task DeleteSalaryComponentsAsync(SalaryComponents SC)
        {
            string endPoint = $"/DeleteSalaryComponents/{SC.Id}";
            var response = await _httpClient.DeleteAsync(_url + endPoint);

            // To do: Validation Messages
        }
    }
}
