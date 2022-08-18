using InternationalWagesManager.DAL;
using InternationalWagesManager.Models;
using Newtonsoft.Json;
using System.Reflection.Metadata;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace BlazorClient.ApiServices;

public class EmployeeApiRepo : IEmployeeRepository
{
    private const string URL = "https://localhost:44364/api/employees";
    private readonly HttpClient _httpClient;
    public EmployeeApiRepo(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
   
    public async Task<Employee?> GetEmployeeAsync(int id)
    {
        string endPoint = $@"/{id}";
        var response = await ResponseStatusAndContent(URL + endPoint);
        var employee = new Employee();
        if (response.Item1)
            employee = JsonConvert.DeserializeObject<Employee>(response.Item2);
        return employee;
    }

    public async Task<List<Employee>> GetEmployeesAsync()
    {
        var response = await ResponseStatusAndContent(URL);
        List<Employee> employees = new List<Employee>();
        if (response.Item1)
            employees = JsonConvert.DeserializeObject<List<Employee>>(response.Item2)!;
        return employees;
    }

    public async Task<int> AddEmployeeAsync(Employee newEmployee)
    {
        var bodyContent = BodyForRequest(newEmployee);
        string endPoint = "/AddEmployee";
        var response = await _httpClient.PostAsync(URL + endPoint, bodyContent);
        string  responseResult = response.Content.ReadAsStringAsync().Result;

        if (response.IsSuccessStatusCode)
        {
            var result = JsonConvert.DeserializeObject<Employee>(responseResult)!;
            return result.Id;
        }
        return 0;
    }

    public async void UpdateEmployee(Employee employee)
    {
        var bodyContent = BodyForRequest(employee);
        string endPoint = "/" + employee.Id.ToString();
        await _httpClient.PutAsync(URL + endPoint, bodyContent);
       
        // to do: validation 
        
    }

    public async void DeleteEmployee(Employee employee)
    {
        string endPoint = "/" + employee.Id.ToString();
       await  _httpClient.DeleteAsync(URL + endPoint);

        // to do: validation 
    }

    private StringContent BodyForRequest(Employee employee)
    {
        var jsonContent = JsonConvert.SerializeObject(employee);
        return  new StringContent(jsonContent, Encoding.UTF8, "application/json");
    }
   
    private async Task<(bool, string)> ResponseStatusAndContent(string url)
    {
        var response = await _httpClient.GetAsync(url);
        return (response.IsSuccessStatusCode, await response.Content.ReadAsStringAsync());
    }



   
}
      

    


