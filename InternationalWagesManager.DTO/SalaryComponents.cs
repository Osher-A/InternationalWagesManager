using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternationalWagesManager.DTO
{
    public class SalaryComponents
    {
        public int Id { get; set; }
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }
        public DateTime Month { get; set; }
        public float TotalHours { get; set; }
        public float BonusHours { get; set; }
        public float BonusWage { get; set; }
        public float Expenses { get; set; }

    }
}
