using PumpService.Core;
using PumpService.Core.Domain.Security;

namespace PumpService.Services.Permissions
{
    public partial interface IPermissionService : IBaseService
    {
        void DeletePermission(long permissionId);

        List<Permission> GetAllPermissions();

        Permission GetPermissionById(long permissionId);

        void InsertPermission(Permission permission);

        void UpdatePermission(Permission permission);

        IPagedList<Permission> SearchPermissions(PermissionSearch permissionSearch);

        bool HavePermission(string permissionCode);

        List<string> GetPermissionsByPrefix(string permissionCode);

        string ExportPermissions(PermissionSearch permissionSearch);
    }
}