using AutoMapper;
using InternationalWagesManager.DAL;

namespace InternationalWagesManager.Domain
{
    public class CurrenciesManager
    {
        private IMapper _mapper;
        private ICurrenciesRepository _currenciesRepo;
        public List<DTO.Currency> AllCurrencies { get; private set; }
        public CurrenciesManager(IMapper mapper, ICurrenciesRepository currenciesRepo)
        {
            _mapper = mapper;
            _currenciesRepo = currenciesRepo;
        }

        public async Task<List<DTO.Currency>> GetAllCurrencies()
        {
            var dtoCurrencies = _mapper.Map<List<Models.Currency>, List<DTO.Currency>>(await _currenciesRepo.GetAllCurrenciesAsync());
            return dtoCurrencies;
        }

        public async Task<string> GetCurrencyName(int? CurrencyId) =>
           (await GetAllCurrencies()).Find(c => c.Id == CurrencyId)?.Name ?? "";

        public int GetCurrencyId(string currencyName)
        {
            return GetAllCurrencies().Result.FirstOrDefault(c => c.Name == currencyName)!.Id;
        }
    }
}
