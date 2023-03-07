using PumpService.Core.Domain.Security;

namespace PumpService.Core.Repository.Security
{
    public partial interface IPermissionRepository : IBaseRepository<Permission>
    {
        #region Methods

        IPagedList<Permission> SearchPermissions(PermissionSearch permissionSearch);

        List<Permission> GetAllPermissions();

        IList<Permission> SearchAllPermissions(PermissionSearch permissionSearch);

        #endregion Methods
    }
}