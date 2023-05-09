using InternationalWagesManager.DAL;
using InternationalWagesManager.Models;
using Newtonsoft.Json;
using System.Text;

namespace BlazorClient.ApiServices;

public class EmployeeApiRepo : IEmployeeRepository
{
    private readonly string _url;


    private readonly HttpClient _httpClient;
    public EmployeeApiRepo(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _url = configuration.GetSection("BaseAPIUrl").Value! + "/employees";
    }

    public async Task<Employee?> GetByIdAsync(int id)
    {
        string endPoint = $@"/{id}";
        var response = await ResponseStatusAndContent(_url + endPoint);
        var employee = new Employee();
        if (response.Item1)
            employee = JsonConvert.DeserializeObject<Employee>(response.Item2);
        return employee;
    }

    public async Task<List<Employee>> GetAllAsync()
    {
        var response = await ResponseStatusAndContent(_url);
        List<Employee> employees = new List<Employee>();
        if (response.Item1)
            employees = JsonConvert.DeserializeObject<List<Employee>>(response.Item2)!;
        return employees;
    }

    public async Task<int> AddAsync(Employee newEmployee)
    {
        var bodyContent = BodyForRequest(newEmployee);
        var response = await _httpClient.PostAsync(_url, bodyContent);
        string responseResult = response.Content.ReadAsStringAsync().Result; 

        if (response.IsSuccessStatusCode)
        {
            var result = JsonConvert.DeserializeObject<Employee>(responseResult)!;
            return result.Id;
        }
        return 0;
    }

    public async Task UpdateAsync(Employee employee)
    {
        var bodyContent = BodyForRequest(employee);
        string endPoint = "/" + employee.Id.ToString();
        await _httpClient.PutAsync(_url + endPoint, bodyContent);

    }

    public async Task DeleteAsync(Employee employee)
    {
        string endPoint = "/" + employee.Id.ToString();
        await _httpClient.DeleteAsync(_url + endPoint);

    }

    private StringContent BodyForRequest(Employee employee)
    {
        var jsonContent = JsonConvert.SerializeObject(employee);
        return new StringContent(jsonContent, Encoding.UTF8, "application/json");
    }

    private async Task<(bool, string)> ResponseStatusAndContent(string _url)
    {
        var response = await _httpClient.GetAsync(_url);
        return (response.IsSuccessStatusCode, await response.Content.ReadAsStringAsync());
    }

}





