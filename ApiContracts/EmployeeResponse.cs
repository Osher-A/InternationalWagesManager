using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiContracts
{
    public record EmployeeResponse(
        int Id,
        string FirstName,
        string LastName,
        DateTime DOB,
        string Phone,
        string Email);
   
}
