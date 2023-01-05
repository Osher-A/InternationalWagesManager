using AutoMapper;
using InternationalWagesManager.DAL;
using InternationalWagesManager.Domain;
using InternationalWagesManager.DTO;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeManager _employeeManager;
        public EmployeeController(IMapper mapper, IEmployeeRepository employeeRepository)
        {
            _employeeManager = new EmployeeManager(mapper, employeeRepository);
        }

        public async Task<IActionResult> Index()
        {
            var employees = await _employeeManager.GetEmployeesAsync() as IEnumerable<Employee>;
            return View(employees);
        }

        public async Task<IActionResult> Details([FromRoute] int id)
        {
            Employee employee = await GetEmployee(id);
            return View(employee);
        }

        public IActionResult Add()
        {
            var employee = new Employee();
            return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([FromForm] Employee employee)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            await _employeeManager.AddEmployeeAsync(employee);

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        [Route("employee/edit/{employeeId}")]
        public async Task<IActionResult> Edit([FromRoute] int employeeId)
        {
            Employee? employee = await GetEmployee(employeeId);

            if (employee == null)
                return NotFound();

            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromForm] Employee employee)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _employeeManager.UpdateEmployee(employee);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("{employeeId}")]
        public async Task<IActionResult> Delete(int employeeId)
        {
            Employee? employee = await GetEmployee(employeeId);

            if (employee == null)
                return NotFound();

            _employeeManager.DeleteEmployee(employee);

            return RedirectToAction(nameof(Index));
        }

        private async Task<Employee?> GetEmployee(int employeeId)
        {
            var employees = await _employeeManager.GetEmployeesAsync();
            var employee = employees.Find(e => e.Id == employeeId);
            return employee;
        }
    }
}
