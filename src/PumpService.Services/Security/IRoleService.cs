using PumpService.Core;
using PumpService.Core.Domain.Security;

namespace PumpService.Services.Roles
{
    public partial interface IRoleService : IBaseService
    {
        void DeleteRole(long roleId);

        List<Role> GetAllRoles();

        Role GetRoleById(long roleId);

        void InsertRole(Role role);

        void UpdateRole(Role role);

        IPagedList<Role> SearchRoles(RoleSearch roleSearch);

        string ExportRoles(RoleSearch roleSearch);
    }
}