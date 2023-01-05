using AutoMapper;
using InternationalWagesManager.DAL;
using InternationalWagesManager.DTO;

namespace InternationalWagesManager.Domain
{
    public class StatementManager
    {
        private readonly PaymentsManager _paymentsManager;
        private readonly SalaryManager _salaryManager;
        private List<Salary> _salaryList;
        private List<Payment> _paymentList;
        private List<Statement> _statementList = new List<Statement>();
        public StatementManager(IMapper mapper, IPaymentsRepository paymentsRepository, ISalaryRepository
            salaryRepository, IWConditionsRepository wConditionsRepository, ICurrenciesRepository currenciesRepository)
        {
            _paymentsManager = new PaymentsManager(mapper, paymentsRepository);
            _salaryManager = new SalaryManager(mapper, salaryRepository, wConditionsRepository, currenciesRepository);
        }

        public decimal GetCurrentBalance(int employeeId)
        {
            var sumOfSalaries = _salaryManager.GetSalaries(employeeId).Sum(s => s.NetPay);
            var sumOfPayments = _paymentsManager.GetAllPayments(employeeId).Sum(p => p.Amount);

            return sumOfSalaries - (decimal)sumOfPayments;
        }

        public List<Statement> GetStatements(int employeeId)
        {
            AddSalariesToStatement(employeeId);
            AddPaymentsToStatement(employeeId);
            SetStatementBallanceProp();
            return _statementList;
        }

        private void AddSalariesToStatement(int employeeId)
        {
            _salaryList = _salaryManager.GetSalaries(employeeId).OrderBy(s => s.Month).ToList();
            foreach (var salary in _salaryList)
            {
                Statement statement = new Statement() { Date = salary.Month, SalaryPayable = salary.NetPay };
                _statementList.Add(statement);
            }
        }

        private void AddPaymentsToStatement(int employeeId)
        {
            _paymentList = _paymentsManager.GetAllPayments(employeeId).OrderBy(p => p.Date).ToList();
            foreach (var payment in _paymentList)
            {
                Statement statement = new Statement() { Date = (DateTime)payment.Date, Payment = payment.Amount };
                _statementList.Add(statement);
            }
        }

        private void SetStatementBallanceProp()
        {
            _statementList = _statementList.OrderBy(s => s.Date).ToList();
            for (var i = 0; i < _statementList.Count; i++)
            {
                if (i == 0)
                    _statementList[i].Balance = (decimal)_statementList[i].SalaryPayable;
                else
                    _statementList[i].Balance = _statementList[i].Payment == null ? _statementList[i - 1].Balance + (decimal)_statementList[i].SalaryPayable :
                        _statementList[i - 1].Balance - (decimal)_statementList[i].Payment;
            }
        }


    }
}
