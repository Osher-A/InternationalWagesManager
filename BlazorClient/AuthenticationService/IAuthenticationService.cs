
using ApiContracts;

namespace BlazorClient.AuthenticationService
{
    public interface IAuthenticationService
    {
        Task<SignUpResponse> RegisterUser(SignUpRequest signUpRequest);
        Task<SignInResponse> Login(SignInRequest signInRequest);
        Task Logout();
    }
}
