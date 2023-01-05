using InternationalWagesManager.DAL;
using InternationalWagesManager.Models;
using Newtonsoft.Json;

namespace BlazorClient.ApiServices
{
    public class CurrencyApiRepo : ICurrenciesRepository
    {
        private readonly string _url;
        private readonly HttpClient _httpClient;

        public CurrencyApiRepo(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _url = configuration.GetSection("BaseAPIUrl").Value! + "/currencies";
        }

        public async Task<List<Currency>> GetAllCurrenciesAsync()
        {
            var response = await _httpClient.GetAsync(_url);
            var jsonContent = await response.Content.ReadAsStringAsync();
            var currencyList = new List<Currency>();
            if (response.IsSuccessStatusCode)
                currencyList = JsonConvert.DeserializeObject<List<Currency>>(jsonContent)!;
            return currencyList;
        }

        public void AddCurrency(Currency currency)
        {
            string endPoint = "addcurrency";
            var jsonContent = JsonConvert.SerializeObject(currency);
            var body = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
            var response = _httpClient.PostAsync(_url + endPoint, body);

            // todo: Validation
        }

        public async void DeleteCurrency(Currency currency)
        {
            string endPoint = $"delete/{currency.Id}";
            var response = await _httpClient.DeleteAsync(_url + endPoint);

            // to do: validation
        }

        public async void UpdateCurrency(Currency currency)
        {
            string endPoint = $"update/{currency.Id}";
            var jsonContent = JsonConvert.SerializeObject(currency);
            var body = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
            await _httpClient.PutAsync(_url + endPoint, body);

            //to do: validation
        }
    }
}
