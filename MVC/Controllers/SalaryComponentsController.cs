using AutoMapper;
using InternationalWagesManager.DAL;
using InternationalWagesManager.Domain;
using InternationalWagesManager.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC.ViewModels.SalaryComponentsVM;

namespace MVC.Controllers
{
    public class SalaryComponentsController : Controller
    {
        private EmployeeManager _employeeManager;
        private SalaryComponentsManager _salaryComponentsManager;
        public SalaryComponentsController(IMapper mapper, IEmployeeRepository employeeRepository, ISalaryComponentsRepository salaryComponentsRepository,
            ISalaryRepository salaryRepository, IWConditionsRepository wConditionsRepository, ICurrenciesRepository currenciesRepository)
        {
            _employeeManager = new EmployeeManager(mapper, employeeRepository);
            _salaryComponentsManager = new SalaryComponentsManager(mapper, salaryComponentsRepository, salaryRepository, wConditionsRepository, currenciesRepository);
        }
        // GET: SalaryComponentsController
        [HttpGet("SalaryComponents")]
        public async Task<ActionResult> Index()
        {
            var employees = await _employeeManager.GetEmployeesAsync();
            return View(employees);
        }

        // GET: SalaryComponentsController/Details/5
        public async Task<ActionResult> Details(int employeeId)
        {
            var viewModel = new EmployeeSalaryComponentsVM();
            viewModel.AllSalaryComponents = await _salaryComponentsManager.GetAllEmployeesSCAsync(employeeId);
            viewModel.EmployeeName = (await _employeeManager.GetEmployeesAsync()).SingleOrDefault(e => e.Id == employeeId)?.FullName!;
            return View(viewModel);
        }

        // GET: SalaryComponentsController/Create
        public ActionResult Add(int employeeId)
        {
            var salaryToAdd = new SalaryComponents { EmployeeId = employeeId };
            return View(salaryToAdd);
        }

        // POST: SalaryComponentsController/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(SalaryComponents formSalary)
        {
            try
            {
                await _salaryComponentsManager.AddSalaryComponentsAsync(formSalary);
                return RedirectToAction(nameof(Details), new { employeeId = formSalary.EmployeeId });
            }
            catch
            {
                return View();
            }
        }

        // GET: SalaryComponentsController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var salary = await _salaryComponentsManager.GetSalaryComponentsAsync(id);
            return View(salary);
        }

        // POST: SalaryComponentsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(SalaryComponents salaryComponents)
        {
            try
            {
                await _salaryComponentsManager.UpdateSalaryAsync(salaryComponents);
                return RedirectToAction(nameof(Details), new { employeeId = salaryComponents.EmployeeId });
            }
            catch
            {
                return View();
            }
        }

        // GET: SalaryComponentsController/Delete/5/5
        public async Task<ActionResult> Delete(int id, int employeeId)
        {
            var formSalary = new SalaryComponents { Id = id };
            try
            {
                await _salaryComponentsManager.DeletedSalarySuccessfullyAsync(formSalary);
                return RedirectToAction(nameof(Details), new { employeeId = employeeId });
            }
            catch
            {
                return View();
            }
        }

    }
}
