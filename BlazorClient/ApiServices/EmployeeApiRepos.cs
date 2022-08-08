using InternationalWagesManager.DAL;
using InternationalWagesManager.Models;
using Newtonsoft.Json;
using System.Reflection.Metadata;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;

namespace BlazorClient.ApiServices
{
    public class EmployeeApiRepo : IEmployeeRepository
    {
        private const string URL = "https://localhost:7194/api/employees";
       
        public void AddEmployee(Employee newEmployee)
        {
            throw new NotImplementedException();
        }

        public void DeleteEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public Employee GetEmployee(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
           return await GetEmployees();
        }

        public void UpdateEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        private async Task<List<Employee>> GetEmployees()
        {
            using HttpClient client = new HttpClient();
            var response = await client.GetAsync(URL);
            var content = await response.Content.ReadAsStringAsync();
            List<Employee> employees = new List<Employee>();
            if (response.IsSuccessStatusCode)
                employees = JsonConvert.DeserializeObject<List<Employee>>(content);

            return employees;
        }



       
    }
          
}

        
    

