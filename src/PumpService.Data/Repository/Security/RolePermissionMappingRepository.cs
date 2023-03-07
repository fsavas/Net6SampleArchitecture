using PumpService.Core.Domain.Security;
using PumpService.Core.Repository.Security;

namespace PumpService.Data.Repository.Security
{
    public class RolePermissionMappingRepository : BaseRepository<RolePermissionMapping>, IRolePermissionMappingRepository
    {
        #region Constructor

        public RolePermissionMappingRepository(IDbContext context)
            : base(context)
        {
        }

        #endregion Constructor
    }
}