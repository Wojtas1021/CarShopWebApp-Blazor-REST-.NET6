using CarShopApp.Blazor.Server.UI.Services.Base;

namespace CarShopApp.Blazor.Server.UI.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<bool> AuthenticateAsync(LoginUserDto loginModel);
        public Task Logout();
    }
}
