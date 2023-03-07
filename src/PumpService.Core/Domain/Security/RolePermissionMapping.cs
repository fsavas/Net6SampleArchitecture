namespace PumpService.Core.Domain.Security
{
    public partial class RolePermissionMapping : BaseDomainEntity
    {
        public long PermissionId { get; set; }
        public long RoleId { get; set; }
        public virtual Permission Permission { get; set; }
        public virtual Role Role { get; set; }
    }
}