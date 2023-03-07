using PumpService.Core.Domain.Security;

namespace PumpService.Core.Domain.Users
{
    public partial class UserRoleMapping : BaseDomainEntity
    {
        public long UserId { get; set; }
        public long RoleId { get; set; }
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}