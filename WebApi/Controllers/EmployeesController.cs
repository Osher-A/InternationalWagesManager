using ApiContracts;
using ApiContracts.ResponseStatus;
using AutoMapper;
using InternationalWagesManager.DAL;
using InternationalWagesManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private ILogger<EmployeesController> _logger;
    private IMapper _mapper;
    private IEmployeeRepository _employeeRepo;
    public EmployeesController(ILogger<EmployeesController> logger, IMapper mapper, IEmployeeRepository employeeRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _employeeRepo = employeeRepository;
    }

    // GET: api/Employees
    [HttpGet]

    public async Task<ActionResult<IEnumerable<EmployeeResponse>>> GetEmployees()
    {
        try
        {
            if (await _employeeRepo.GetAllAsync() == null)
                return Problem("No Data in database!", statusCode: StatusCodes.Status503ServiceUnavailable);
            return _mapper.Map<List<Employee>, List<EmployeeResponse>>(await _employeeRepo.GetAllAsync());
        }
        catch (Exception)
        {
            _logger.LogError("Server Error");
        }
        return Problem("Server Error", statusCode: StatusCodes.Status500InternalServerError);
    }

    // GET: api/Employees/5
    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeResponse>> GetEmployee(int id)
    {
        if (id < 1)
            return BadRequestResponse();
        try
        {
            var result = await _employeeRepo.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            return _mapper.Map<EmployeeResponse>(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
        return Problem("Server Error", statusCode: StatusCodes.Status500InternalServerError);
    }


    // POST: api/employees
    [HttpPost]
    public async Task<ActionResult> AddEmployee([FromBody] EmployeeRequest employee)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        try
        {
            int employeeId = await _employeeRepo.AddAsync(_mapper.Map<Employee>(employee));
            return CreatedAtAction(nameof(GetEmployee), new { id = employeeId }, employee);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
        return ServerErrorResponse();
    }

    // PUT: api/Employees/5
    [HttpPut("{id}")]
    public IActionResult UpdateEmployee(int id, EmployeeRequest employee)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (id < 1)
            return BadRequestResponse();

        if (!EmployeeExists(id))
            return NotFound();

        try
        {
            Employee modelEmployee = _mapper.Map<Employee>(employee);
            modelEmployee.Id = id;
            _employeeRepo.UpdateAsync(modelEmployee);

            return CreatedAtAction(nameof(GetEmployee), new { id = modelEmployee.Id }, employee);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return ServerErrorResponse();
        }
    }

    // DELETE: api/Employees/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        try
        {
            var employee = await _employeeRepo.GetByIdAsync(id);
            if (employee == null)
                return NotFound();

            await _employeeRepo.DeleteAsync(employee);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return ServerErrorResponse();
        }
        return NoContent();
    }

    private bool EmployeeExists(int id)
    {
        try
        {
            return _employeeRepo.GetByIdAsync(id) != null;
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