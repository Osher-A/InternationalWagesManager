﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiContracts
{
    public record PaymentRequest(
       int EmployeeId,
       DateTime Date,
       decimal Amount,
       string Description);
}