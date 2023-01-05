using ApiContracts;
using ApiContracts.ResponseStatus;
using AutoMapper;
using InternationalWagesManager.DAL;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkConditionsController : ControllerBase
    {
        private IWConditionsRepository _wcRepository;
        private IMapper _mapper;
        private ILogger<WorkConditionsController> _logger;
        public WorkConditionsController(ILogger<WorkConditionsController> logger, IMapper mapper, IWConditionsRepository wConditionsRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _wcRepository = wConditionsRepository;
        }

        // GET: api/WorkConditions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<WorkConditionsResponse>>> GetWorkConditions([FromRoute] int id)
        {
            if (id == 0)
                return BadRequestResponse();
            try
            {
                var data = _mapper.Map<WorkConditionsResponse>(await _wcRepository.GetWorkConditionsAsync(id));
                if (data == null || data?.Id == 0)
                    return NotFound();
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return Problem("Server Error", statusCode: StatusCodes.Status500InternalServerError);
        }


        // GET: api/WorkConditions/employee/5
        [HttpGet("employee/{employeeId}")]
        public async Task<ActionResult<WorkConditionsResponse>> GetEmployeesWC(int employeeId)
        {
            if (employeeId == 0)
                return BadRequestResponse();

            try
            {
                var data = _mapper.Map<List<WorkConditionsResponse>>(await _wcRepository.GetAllEmployeesWCAsync(employeeId));
                if (data?.Count < 1)
                    return NotFound();


                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return ServerErrorResponse();

        }

        // PUT: api/WorkConditions/5
        [HttpPut("{employeeId}")]
        public async Task<IActionResult> UpdateWorkConditions(int employeeId, WorkConditionsRequest workConditions)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (employeeId != workConditions.EmployeeId || !await WorkConditionsExists(workConditions.Id))
                return BadRequestResponse();

            try
            {
                await _wcRepository.UpdateWorkConditionsAsync(_mapper.Map<InternationalWagesManager.Models.WorkConditions>(workConditions));
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e.Message);
                return ServerErrorResponse();
            }
            return NoContent();
        }

        // POST: api/WorkConditions
        [HttpPost]
        public async Task<ActionResult<WorkConditionsRequest>> PostWorkConditions(WorkConditionsRequest workConditions)
        {
            int newId;
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                newId = await _wcRepository.AddWorkConditions(_mapper.Map<InternationalWagesManager.Models.WorkConditions>(workConditions));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return ServerErrorResponse();
            }

            return CreatedAtAction(nameof(GetEmployeesWC), new { id = newId }, workConditions);
        }

        // DELETE: api/WorkConditions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkConditions(int id)
        {
            if (!(await WorkConditionsExists(id)))
                return BadRequestResponse();

            try
            {
                _wcRepository.DeleteWorkConditions(id);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return ServerErrorResponse();
            }

            return NoContent();
        }

        private async Task<bool> WorkConditionsExists(int id)
        {
            try
            {
                return (await _wcRepository.GetWorkConditionsAsync(id)).Id != 0;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return false;
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
