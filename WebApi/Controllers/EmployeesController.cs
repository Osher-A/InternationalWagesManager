using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InternationalWagesManager.Models;
using Microsoft.AspNetCore.Cors;
using ApiContracts;
using AutoMapper;
using InternationalWagesManager.DAL;
using ApiContracts.ResponseStatus;
using HtmlAgilityPack;

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
            if (await _employeeRepo.GetEmployeesAsync() == null)
                return Problem("No Data in database!", statusCode: StatusCodes.Status503ServiceUnavailable);
            return _mapper.Map<List<Employee>, List<EmployeeResponse>>(await _employeeRepo.GetEmployeesAsync());
        }
        catch (Exception e)
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
            var result = await _employeeRepo.GetEmployeeAsync(id);
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


    // POST: api/employees/AddEmployee
    [HttpPost("AddEmployee")]
    public async Task<ActionResult> AddEmployee([FromBody] EmployeeRequest employee)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        try
        {
            int employeeId = await _employeeRepo.AddEmployeeAsync(_mapper.Map<Employee>(employee));
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
            _employeeRepo.UpdateEmployee(modelEmployee);

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
            var employee = await _employeeRepo.GetEmployeeAsync(id);
            if (employee == null)
                return NotFound();

            _employeeRepo.DeleteEmployee(employee);
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
            return _employeeRepo.GetEmployeeAsync(id) != null;
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

    private ActionResult ServerErrorResponse()  => Problem("Server Error", statusCode: StatusCodes.Status500InternalServerError);
        
}