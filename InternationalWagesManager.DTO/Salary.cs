using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternationalWagesManager.DTO
{
    public class Salary 
    {
        public int Id { get; set; }
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }
        public DateTime Month { get; set; }
        public decimal GrossPay { get; set; }
        public decimal NetPay { get; set; }
        public Currency Currency { get; set; }
        public int CurrencyId { get; set; }
        public float WagesPayRate { get; set; }
        public float ExpensesPayRate { get; set; }
    }
}
