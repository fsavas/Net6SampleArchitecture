using PumpService.Core.Domain.Users;
using PumpService.Core.Repository.Users;

namespace PumpService.Data.Repository.Users
{
    public class UserRoleMappingRepository : BaseRepository<UserRoleMapping>, IUserRoleMappingRepository
    {
        #region Constructor

        public UserRoleMappingRepository(IDbContext context)
            : base(context)
        {
        }

        #endregion Constructor
    }
}