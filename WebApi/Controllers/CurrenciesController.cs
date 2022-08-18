using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InternationalWagesManager.Models;
using InternationalWagesManager.DAL;
using ApiContracts;
using AutoMapper;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrenciesController : ControllerBase
    {
        private IMapper _mapper;
        private ICurrenciesRepository _currenciesRepository;

        public CurrenciesController(IMapper mapper, ICurrenciesRepository currenciesRepository)
        {
            _mapper = mapper;
            _currenciesRepository = currenciesRepository;
        }

        // GET: api/Currencies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CurrencyResponse>>> GetCurrencies()
        {
          if (await _currenciesRepository.GetAllCurrenciesAsync() == null)
          {
                return Problem("Entity set 'MyDbContext.Currencies' is null.");
          }
            return _mapper.Map<List<CurrencyResponse>>(await _currenciesRepository.GetAllCurrenciesAsync());
        }

        // GET: api/Currencies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CurrencyResponse>> GetCurrency(int id)
        {
          if (await _currenciesRepository.GetAllCurrenciesAsync() == null)
          {
                return Problem("Entity set 'MyDbContext.Currencies' is null.");
            }
            var currency = await GetCurrencyResponse(id);

            if (currency == null)
            {
                return NotFound();
            }

            return currency;
        }

        // POST: api/Currencies/addcurrency
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("AddCurrency")]
        public ActionResult AddCurrency(CurrencyRequest currencyRequest)
        {
            var modelCurrency = GetModelCurrency(currencyRequest);
            _currenciesRepository.AddCurrency(modelCurrency);

            return Ok();
        }

        // PUT: api/Currencies/update/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateCurrency(int id, CurrencyRequest currencyRequest)
        {
            if (GetCurrencyResponse(id) == null)
            {
                return NotFound();
            }

            var modelCurrency = GetModelCurrency(currencyRequest);
            modelCurrency.Id = id;

            _currenciesRepository.UpdateCurrency(modelCurrency);

            return CreatedAtAction(nameof(GetCurrency), new {Id = id}, currencyRequest);
        }


        // DELETE: api/Currencies/delete/5
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCurrency(int id)
        {
           var modelCurrency = (await _currenciesRepository.GetAllCurrenciesAsync()).SingleOrDefault(cr => cr.Id == id);
            if (modelCurrency == null)
                return NotFound();

            _currenciesRepository.DeleteCurrency(modelCurrency);

            return NoContent();
        }

       

        private async Task<CurrencyResponse> GetCurrencyResponse(int id) =>
             _mapper.Map<CurrencyResponse>((await _currenciesRepository.GetAllCurrenciesAsync()).SingleOrDefault(cr => cr.Id == id));
        
        private Currency GetModelCurrency(CurrencyRequest currencyRequest) =>
            _mapper.Map<Currency>(currencyRequest);
    }
}
