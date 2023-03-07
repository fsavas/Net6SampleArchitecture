using PumpService.Core.Domain.Users;

namespace PumpService.Core.Repository.Users
{
    public partial interface IUserRepository : IBaseRepository<User>
    {
        #region Methods

        IPagedList<User> SearchUsers(UserSearch userSearch);

        List<User> GetAllUsers();

        User GetByUsername(string username);

        IList<User> SearchAllUsers(UserSearch userSearch);

        #endregion Methods
    }
}