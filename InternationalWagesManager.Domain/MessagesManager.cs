using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternationalWagesManager.Domain
{
    public class MessagesManager
    {
        public static Func<string, Task<bool>> AlertFunc { get; set; }
        public Action<string> SuccessMessage { get; set; }
        public Action<string> ErrorMessage { get; set; }
        public static async Task<bool> UserConfirmation(string alertMessage)
        {
            if (AlertFunc == null)
                return false;
            return await AlertFunc(alertMessage);
        }
    }
}
