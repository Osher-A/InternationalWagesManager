using InternationalWagesManager.DAL;
using AutoMapper;
using InternationalWagesManager.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC.ViewModels;
using InternationalWagesManager.DTO;
using System.Security.Cryptography;

namespace MVC.Controllers
{
    public class WorkConditionsController : Controller
    {
        private WorkConditionsManager _workConditionsManager;
        private EmployeeManager _employeeManager;
        private CurrenciesManager _currenciesManager;

        public WorkConditionsController(IMapper mapper, IWConditionsRepository workConditonsRepository, IEmployeeRepository employeeRepository, ICurrenciesRepository currenciesRepository)
        {
            _workConditionsManager = new WorkConditionsManager(mapper, workConditonsRepository);
            _employeeManager = new EmployeeManager(mapper, employeeRepository);
            _currenciesManager = new CurrenciesManager(mapper, currenciesRepository); 
        }


        // GET: WorkConditionsController
        public async Task<ActionResult> Index()
        {
            return View(await _employeeManager.GetEmployeesAsync());
        }

        // GET: WorkConditionsController/Details/5
        public async Task<ActionResult> Details(int employeeId)
        {
            var viewModel = new WorkConditionsDetailsVM();

            var employee = (await _employeeManager.GetEmployeesAsync()).FirstOrDefault(e => e.Id == employeeId)!;
            if (employee == null)
                return NotFound();

            viewModel.Employee = employee;
            
            viewModel.WorkConditions = _workConditionsManager.GetAllEmployeesWCAsync(employeeId);
            return View(viewModel);
        }

        // GET: WorkConditionsController/Create/5
        public async Task<ActionResult> Create(int id)
        {
            var viewModel = new CreateWorkConditionsVM();
            viewModel.WorkConditions = new InternationalWagesManager.DTO.WorkConditions() { EmployeeId = id };
            viewModel.Currencies = await _currenciesManager.GetAllCurrencies();
            return View(viewModel);
        }

        // POST: WorkConditionsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateWorkConditionsVM vM)
        {
            try
            {
                _workConditionsManager.AddWorkConditions(vM.WorkConditions);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WorkConditionsController/Edit/5
        public ActionResult Edit(int id)
        {
            var vm = new EditWorkConditionsVM();
            vm.WorkConditions = _workConditionsManager.GetWorkConditions(id);
            vm.Currencies = _currenciesManager.GetAllCurrencies().Result;
            return View(vm);
        }

        // POST: WorkConditionsController/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromForm]WorkConditions workConditions)
        {
            try
            {
                _workConditionsManager.UpdateWorkConditions(workConditions);
               return  RedirectToAction(nameof(Details), new {employeeId = workConditions.EmployeeId});
            }
            catch
            {
                return RedirectToAction();
            }
        }


        // GET: WorkConditionsController/Delete/5
       
        public ActionResult Delete([FromRoute] int id)
        {
            try
            {
                _workConditionsManager.DeleteWorkConditions(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private int GetCurrencyId(string currencyName)
        {
           return _currenciesManager.GetCurrencyId(currencyName);
        }
    }
}

