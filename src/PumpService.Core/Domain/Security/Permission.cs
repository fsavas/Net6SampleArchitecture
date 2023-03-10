namespace PumpService.Core.Domain.Security
{
    public partial class Permission : BaseDomainEntity
    {
        private ICollection<RolePermissionMapping> _rolePermissionMappings;

        public string Code { get; set; }
        public string Name { get; set; }
        //public bool IsDeleted { get; set; }

        public virtual ICollection<RolePermissionMapping> RolePermissionMappings
        {
            get => _rolePermissionMappings ?? (_rolePermissionMappings = new List<RolePermissionMapping>());
            protected set => _rolePermissionMappings = value;
        }
    }
}