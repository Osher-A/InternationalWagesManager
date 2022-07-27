using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternationalWagesManager.DTO
{
    public class Statement
    {
        public DateTime Date { get; set; }
        public decimal? SalaryPayable { get; set; }
        public decimal? Payment { get; set; }
        public decimal Balance { get; set; }
    }
}
