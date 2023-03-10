using AutoMapper;
using InternationalWagesManager.DAL;
using InternationalWagesManager.Domain;
using InternationalWagesManager.DTO;
using Microsoft.AspNetCore.Mvc;
using MVC.ViewModels;
using MVC.ViewModels.WorkConditionsVM;

namespace MVC.Controllers
{
    public class WorkConditionsController : Controller
    {
        private WorkConditionsManager _workConditionsManager;
        private EmployeeManager _employeeManager;
        private CurrenciesManager _currenciesManager;
        private static int _employeeId;

        public WorkConditionsController(IMapper mapper, IWConditionsRepository workConditonsRepository, IEmployeeRepository employeeRepository, ICurrenciesRepository currenciesRepository)
        {
            _workConditionsManager = new WorkConditionsManager(mapper, workConditonsRepository);
            _employeeManager = new EmployeeManager(mapper, employeeRepository);
            _currenciesManager = new CurrenciesManager(mapper, currenciesRepository);
        }


        // GET: WorkConditions
        [HttpGet("WorkConditions")]
        public async Task<ActionResult> Index()
        {
            return View(await _employeeManager.GetEmployeesAsync());
        }

        // GET: WorkConditions/Details/5
        public async Task<ActionResult> Details(int employeeId)
        {
            //need to set _employeeId to be used later by the Delete Method to be able to return to this Action
            _employeeId = employeeId;

            var viewModel = new WorkConditionsDetailsVM();

            var employee = (await _employeeManager.GetEmployeesAsync()).FirstOrDefault(e => e.Id == employeeId)!;
            if (employee == null)
                return NotFound();

            viewModel.Employee = employee;

            viewModel.WorkConditions = await _workConditionsManager.GetAllEmployeesWCAsync(employeeId);
            return View(viewModel);
        }

        // GET: WorkConditions/Create/5
        [HttpGet]
        public async Task<ActionResult> Add(int id)
        {
            var viewModel = new CreateWorkConditionsVM();
            viewModel.WorkConditions = new InternationalWagesManager.DTO.WorkConditions() { EmployeeId = id };
            viewModel.Currencies = await _currenciesManager.GetAllCurrencies();
            return View(viewModel);
        }

        // POST: WorkConditions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(CreateWorkConditionsVM vM)
        {
            try
            {
                await _workConditionsManager.AddWorkConditionsAsync(vM.WorkConditions);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WorkConditions/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var vm = new EditWorkConditionsVM();
            vm.WorkConditions = _workConditionsManager.GetWorkConditions(id);
            vm.Currencies = _currenciesManager.GetAllCurrencies().Result;
            return View(vm);
        }

        // POST: WorkConditions/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([FromForm] WorkConditions workConditions)
        {
            try
            {
                await _workConditionsManager.UpdateWorkConditionsAsync(workConditions);
                return RedirectToAction(nameof(Details), new { employeeId = workConditions.EmployeeId });
            }
            catch
            {
                return RedirectToAction();
            }
        }


        // GET: WorkConditions/Delete/5

        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await _workConditionsManager.DeleteWorkConditionsAsync(id);
                return RedirectToAction(nameof(Details), new { employeeId = _employeeId });
            }
            catch
            {
                return RedirectToAction(nameof(Details), new { employeeId = _employeeId });
            }
        }

        private int GetCurrencyId(string currencyName)
        {
            return _currenciesManager.GetCurrencyId(currencyName);
        }
    }
}

