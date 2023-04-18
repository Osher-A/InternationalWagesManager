using ApiContracts;
using ApiContracts.ResponseStatus;
using AutoMapper;
using InternationalWagesManager.DAL;
using InternationalWagesManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SalaryComponentsController : ControllerBase
    {
        private readonly ISalaryComponentsRepository _scRepository;
        private readonly IMapper _mapper;
        private ILogger<SalaryComponentsController> _logger;

        public SalaryComponentsController(ISalaryComponentsRepository salaryComponentsRepository, IMapper mapper, ILogger<SalaryComponentsController> logger)
        {
            _scRepository = salaryComponentsRepository;
            _mapper = mapper;
            _logger = logger;
        }


        // GET: api/SalaryComponents/All/5
        [HttpGet("all/{employeeId}")]
        public async Task<ActionResult<IEnumerable<SalaryComponentsResponse>>> GetSalariesComponents(int employeeId)
        {
            var sc = _mapper.Map<IEnumerable<SalaryComponentsResponse>>(await _scRepository.GetEmployeeSalaryComponentsAsync(employeeId));
            if (sc == null)
                return NotFound();

            return Ok(sc);
        }

        // GET: api/SalaryComponents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SalaryComponentsResponse>> GetSalaryComponents(int id)
        {
            var sc = _mapper.Map<SalaryComponentsResponse>(await _scRepository.GetByIdAsync(id));
            if (sc == null)
                return NotFound();

            return Ok(sc);
        }

        // POST: api/SalaryComponents
        [HttpPost]
        public async Task<ActionResult<SalaryComponents>> AddSalaryComponents(SalaryComponentsRequest salaryComponents)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            int newId = 0;
            try
            {
                newId = await _scRepository.AddAsync(_mapper.Map<SalaryComponents>(salaryComponents));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ServerErrorResponse();
            }
            return CreatedAtAction(nameof(GetSalaryComponents), new { id = newId }, salaryComponents);
        }

        // PUT: api/SalaryComponents/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSalaryComponents(int id, SalaryComponentsRequest salaryComponents)
        {
            if (!ModelState.IsValid || !(await SalaryComponentsExists(id)))
                return BadRequest();

            try
            {
                await _scRepository.UpdateAsync(_mapper.Map<SalaryComponents>(salaryComponents));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ServerErrorResponse();
            }
            return Accepted();
        }


        // DELETE: api/SalaryComponents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalaryComponents(int id)
        {
            if (!(await SalaryComponentsExists(id)))
                return NotFound();

            try
            {
                var sc = new SalaryComponents() { Id = id };
                await _scRepository.DeleteAsync(sc);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ServerErrorResponse();
            }

            return NoContent();
        }

        private async Task<bool> SalaryComponentsExists(int id)
        {
            return await _scRepository.GetByIdAsync(id) != null;
        }
        private ActionResult BadRequestResponse()
        {
            return BadRequest(new ErrorResponse
            {
                ErrorMessage = "Invalid id",
                StatusCode = StatusCodes.Status400BadRequest
            });
        }
        private ActionResult ServerErrorResponse() => Problem("Server Error", statusCode: StatusCodes.Status500InternalServerError);

    }
}
