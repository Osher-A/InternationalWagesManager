using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiContracts.ResponseStatus
{
    public class SuccessResponse
    {
        public int StatusCode { get; set; }
        public string SuccessMessage { get; set; }
    }
}
