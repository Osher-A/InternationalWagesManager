using Blazorise;
using Microsoft.JSInterop;

namespace BlazorClient.Utilities
{
    public class BlazorMessages
    {
        private IMessageService _messageService;
        private IJSRuntime _jsRuntime;
        public BlazorMessages(IMessageService messageService, IJSRuntime jSRuntime)
        {
            _messageService = messageService;
            _jsRuntime = jSRuntime;
        }
        public async Task<bool> ShowConfirmMessage(string message)
        {
            if (await _messageService.Confirm(message, "Warning"))
            {
                return true;
            }
            return false;

        }

        public async void TostrSuccessMessage(string message)
        {
            try
            {
                await _jsRuntime.InvokeVoidAsync("ShowToastr", "Success", message);
            }
            catch (JSException e)
            {
                Console.WriteLine(e.Message.ToString());
            }
        }
        public async void TostrErrorMessage(string message)
        {
            try
            {
                await _jsRuntime.InvokeVoidAsync("ShowToastr", "Error", message);
            }
            catch (JSException)
            {
            }
        }
    }
}
