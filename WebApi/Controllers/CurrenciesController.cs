using ApiContracts;
using ApiContracts.ResponseStatus;
using AutoMapper;
using InternationalWagesManager.DAL;
using InternationalWagesManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrenciesController : ControllerBase
    {
        private ILogger<CurrenciesController> _logger;
        private IMapper _mapper;
        private ICurrenciesRepository _currenciesRepository;

        public CurrenciesController(ILogger<CurrenciesController> logger, IMapper mapper, ICurrenciesRepository currenciesRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _currenciesRepository = currenciesRepository;
        }

        // GET: api/Currencies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CurrencyResponse>>> GetCurrencies()
        {
            try
            {
                if (await _currenciesRepository.GetAllAsync() == null)
                    return Problem("There is no data in the database.");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return ServerErrorResponse();
            }

            return _mapper.Map<List<CurrencyResponse>>(await _currenciesRepository.GetAllAsync());
        }

        // GET: api/Currencies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CurrencyResponse>> GetCurrency(int id)
        {
            if (id == 0)
                return BadRequestResponse();

            try
            {
                if (await _currenciesRepository.GetAllAsync() == null)
                    return Problem("There is no data  in the database.");

                var currency = await GetCurrencyResponse(id);
                if (currency == null)
                    return NotFound();

                return currency;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return ServerErrorResponse();
        }

        // POST: api/Currencies/addcurrency
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("AddCurrency")]
        public async Task<ActionResult> AddCurrency(CurrencyRequest currencyRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var modelCurrency = GetModelCurrency(currencyRequest);
                await _currenciesRepository.AddAsync(modelCurrency);

                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return ServerErrorResponse();
        }

        // PUT: api/Currencies/update/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateCurrency(int id, CurrencyRequest currencyRequest)
        {
            if (id == 0)
                return BadRequestResponse();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                if (GetCurrencyResponse(id) == null)
                    return NotFound();

                var modelCurrency = GetModelCurrency(currencyRequest);
                modelCurrency.Id = id;
                await _currenciesRepository.UpdateAsync(modelCurrency);
                return CreatedAtAction(nameof(GetCurrency), new { Id = id }, currencyRequest);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return ServerErrorResponse();
        }


        // DELETE: api/Currencies/delete/5
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCurrency(int id)
        {
            if (id == 0)
                return BadRequestResponse();
            try
            {
                var modelCurrency = (await _currenciesRepository.GetAllAsync()).SingleOrDefault(cr => cr.Id == id);
                if (modelCurrency == null)
                    return NotFound();
                await _currenciesRepository.DeleteAsync(modelCurrency);
                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return ServerErrorResponse();
        }

        private async Task<CurrencyResponse> GetCurrencyResponse(int id) =>
            _mapper.Map<CurrencyResponse>((await _currenciesRepository.GetByIdAsync(id)));

        private Currency GetModelCurrency(CurrencyRequest currencyRequest) =>
            _mapper.Map<Currency>(currencyRequest);
        private ActionResult ServerErrorResponse() => Problem("Server Error", statusCode: StatusCodes.Status500InternalServerError);
        private ActionResult BadRequestResponse()
        {
            return BadRequest(new ErrorResponse
            {
                ErrorMessage = "Invalid id",
                StatusCode = StatusCodes.Status400BadRequest
            });
        }
    }
}
