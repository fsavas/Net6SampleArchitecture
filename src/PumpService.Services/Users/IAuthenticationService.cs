using PumpService.Core.Domain.Users;

namespace PumpService.Services.Users
{
    public partial interface IAuthenticationService : IBaseService
    {
        bool Login(User user);

        bool Logout();
    }
}