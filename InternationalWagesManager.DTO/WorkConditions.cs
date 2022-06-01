﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternationalWagesManager.DTO
{
    public class WorkConditions 
    {
        public int Id { get; set; }
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }
        public DateTime Date { get; set; }
        public float PayRate { get; set; }
        public Currency WageCurrency { get; set; }
        public Currency ExpensesCurrency { get; set; }
        public float Deductions { get; set; }

    }
}
