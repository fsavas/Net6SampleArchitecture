using PumpService.Core;
using PumpService.Core.Domain.Users;

namespace PumpService.Services.Users
{
    public partial interface IUserService : IBaseService
    {
        void DeleteUser(long userId);

        List<User> GetAllUsers();

        User GetUserById(long userId);

        void InsertUser(User user);

        void UpdateUser(User user);

        IPagedList<User> SearchUsers(UserSearch userSearch);

        string ExportUsers(UserSearch userSearch);
    }
}