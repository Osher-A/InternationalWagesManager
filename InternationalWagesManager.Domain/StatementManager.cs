using AutoMapper;
using InternationalWagesManager.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternationalWagesManager.Domain
{
    public class StatementManager
    {
        private PaymentsManager paymentsManager;

        public PaymentsManager(IMapper mapper, IPaymentsRepository paymentsRepository)
        public decimal GetCurrentBalance(int employeeId)
        {
            decimal balance = 0;


            return balance;
        }
    }
}
