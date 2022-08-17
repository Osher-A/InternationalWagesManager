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

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private IMapper _mapper;
    private IEmployeeRepository _employeeRepo;
    public EmployeesController(IMapper mapper, IEmployeeRepository employeeRepository)
    {
        _mapper = mapper;
        _employeeRepo = employeeRepository;
    }

    // GET: api/Employees
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmployeeResponse>>> GetEmployees()
    {
        if (await _employeeRepo.GetEmployeesAsync() == null)
        {
            return Problem("Entity set 'MyDbContext.Employees'  is null.");
        }
        return _mapper.Map<List<Employee>, List<EmployeeResponse>>(await _employeeRepo.GetEmployeesAsync());
    }

    // GET: api/Employees/5
    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeResponse>> GetEmployee(int id)
    {
        if (id < 1)
            return BadRequest(new ErrorResponse()
            {
                ErrorMessage = "Invalid id",
                StatusCode = StatusCodes.Status400BadRequest
            });
        var result = await _employeeRepo.GetEmployeeAsync(id);

        if (result == null)
            return NotFound();

        return _mapper.Map<EmployeeResponse>(result);
    }


    // POST: api/employees/AddEmployee
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost ("AddEmployee")]
    public async Task<ActionResult<EmployeeResponse>> AddEmployee([FromBody] EmployeeRequest employee)
    {
      int employeeId =  await _employeeRepo.AddEmployee(_mapper.Map<Employee>(employee));

        var employeeResponse = new EmployeeResponse(employeeId, employee.FirstName,
            employee.LastName, employee.DOB, employee.Phone, employee.Email);
        
        return CreatedAtAction(nameof(GetEmployee), new { id = employeeId }, employee);

    }

    // PUT: api/Employees/5
    [HttpPut("{id}")]
    public IActionResult UpdateEmployee(int id, EmployeeRequest employee)
    {
        if (id < 1)
            return BadRequest();

        if (!EmployeeExists(id))
            return NotFound();

        Employee modelEmployee = _mapper.Map<Employee>(employee);
        modelEmployee.Id = id;
        _employeeRepo.UpdateEmployee(modelEmployee);

        return CreatedAtAction(nameof(GetEmployee), new { id = modelEmployee.Id }, employee);
    }

    // DELETE: api/Employees/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        var employee = await _employeeRepo.GetEmployeeAsync(id);

        if (employee == null)
            return NotFound();

       _employeeRepo.DeleteEmployee(employee);

        return NoContent();
    }

    private bool EmployeeExists(int id)
    {
        return  _employeeRepo.GetEmployeeAsync(id)  != null ;
    }
}
