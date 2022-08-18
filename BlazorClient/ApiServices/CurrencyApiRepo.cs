using InternationalWagesManager.DAL;
using InternationalWagesManager.Models;
using System.Security.Policy;
using Newtonsoft.Json;

namespace BlazorClient.ApiServices
{
    public class CurrencyApiRepo : ICurrenciesRepository
    {
        private const string URL = "https://localhost:44364/api/currencies";
        private readonly HttpClient _httpClient;

        public CurrencyApiRepo(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task <List<Currency>> GetAllCurrenciesAsync()
        {
            var response = await _httpClient.GetAsync(URL);
            var jsonContent = await response.Content.ReadAsStringAsync();
            var currencyList = new List<Currency>();
            if(response.IsSuccessStatusCode)
                currencyList = JsonConvert.DeserializeObject<List<Currency>>(jsonContent)!;
            return currencyList;
        }

        public void AddCurrency(Currency currency)
        {
            string endPoint = "addcurrency";
            var jsonContent = JsonConvert.SerializeObject(currency);
            var body = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
            var response = _httpClient.PostAsync(URL + endPoint, body);

            // todo: Validation
        }

        public async void DeleteCurrency(Currency currency)
        {
            string endPoint = $"delete/{currency.Id}";
            var response = await _httpClient.DeleteAsync(URL + endPoint);

            // to do: validation
        }

        public async void UpdateCurrency(Currency currency)
        {
            string endPoint = $"update/{currency.Id}";
            var jsonContent = JsonConvert.SerializeObject(currency);
            var body = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
            await _httpClient.PutAsync(URL + endPoint, body);
           
            //to do: validation
        }
    }
}
