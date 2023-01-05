namespace InternationalWagesManager.Domain
{
    public class MessagesManager
    {
        public static Func<string, Task<bool>> AlertFunc { get; set; }
        public static Action<string> SuccessMessage { get; set; }
        public static Action<string> ErrorMessage { get; set; }
        public static async Task<bool> UserConfirmation(string alertMessage)
        {
            if (AlertFunc == null)
                return false;
            return await AlertFunc(alertMessage);
        }
    }
}
