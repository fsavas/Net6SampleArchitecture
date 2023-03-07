using PumpService.Core.Domain.Security;

namespace PumpService.Core.Repository.Security
{
    public partial interface IRoleRepository : IBaseRepository<Role>
    {
        #region Methods

        IPagedList<Role> SearchRoles(RoleSearch roleSearch);

        List<Role> GetAllRoles();

        IList<Role> SearchAllRoles(RoleSearch roleSearch);

        #endregion Methods
    }
}