using BlazorClient.AuthenticationService;
using Microsoft.AspNetCore.Components;

namespace BlazorClient.Pages.Authentication
{
    public partial class Logout
    {
        [Inject]
        public IAuthenticationService _authSerivce { get; set; }
        [Inject]
        public NavigationManager _navigationManager { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await _authSerivce.Logout();
            _navigationManager.NavigateTo("/", forceLoad: true); ;
        }
    }
}
