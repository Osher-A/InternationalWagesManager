using AutoMapper;
using InternationalWagesManager.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            AllCurrencies = GetAllCurrencies();
        }   

        public List<DTO.Currency> GetAllCurrencies()
        {
            var dtoCurrencies = _mapper.Map<List<Models.Currency>, List<DTO.Currency>>(_currenciesRepo.GetAllCurrencies());
            return dtoCurrencies;
        }

        public string GetCurrencyName(int? CurrencyId) =>
            AllCurrencies.Find(c => c.Id == CurrencyId)?.Name ?? "";
    }
}
