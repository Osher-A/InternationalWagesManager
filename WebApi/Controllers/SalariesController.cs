using ApiContracts.ResponseStatus;
using ApiContracts;
using AutoMapper;
using InternationalWagesManager.DAL;
using InternationalWagesManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalariesController : ControllerBase
    {
        private readonly ISalaryRepository _salaryRepository;
        private readonly IMapper _mapper;
        private ILogger<SalariesController> _logger;

        public SalariesController(ISalaryRepository salaryRepository, IMapper mapper, ILogger<SalariesController> logger)
        {
            _salaryRepository = salaryRepository;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Salaries/5
        [HttpGet("all/{employeeId}")]
        public async Task<ActionResult<IEnumerable<SalaryResponse>>> GetSalaries(int employeeId)
        {
            var salary = _mapper.Map<IEnumerable<SalaryResponse>>(await _salaryRepository.GetAllEmployeeSalariesAsync(employeeId));
            if (salary == null)
                return NotFound();

            return Ok(salary);
        }

        // GET: api/Salaries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SalaryResponse>> GetSalary(int id)
        {
            var salary = _mapper.Map<SalaryResponse>(await _salaryRepository.GetByIdAsync(id));
            if (salary == null)
                return NotFound();

            return Ok(salary);
        }

        // POST: api/Salaries
        [HttpPost]
        public async Task<ActionResult<Salary>> AddSalary(SalaryRequest salary)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            int newId = 0;
            try
            {
                newId = await _salaryRepository.AddAsync(_mapper.Map<Salary>(salary));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ServerErrorResponse();
            }
            return CreatedAtAction(nameof(GetSalary), new { id = newId }, salary);
        }


        // DELETE: api/Salary/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalary(int id)
        {
            if (!(await SalaryExists(id)))
                return NotFound();

            try
            {
                var salary = new Salary() { Id = id };
                await _salaryRepository.DeleteAsync(salary);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ServerErrorResponse();
            }

            return NoContent();
        }

        private async Task<bool> SalaryExists(int id)
        {
            return await _salaryRepository.GetByIdAsync(id) != null;
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
