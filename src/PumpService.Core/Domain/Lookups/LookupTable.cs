using PumpService.Core.Defaults;

namespace PumpService.Core.Domain.Lookups
{
    public partial class LookupTable : BaseDomainEntity
    {
        public EnumClasses.LookupTypes LookupType { get; set; }
        public string Name { get; set; }

        //public int Value { get; set; }
        public string Description { get; set; }

        public bool IsActive { get; set; }
        //public bool IsDeleted { get; set; }
    }
}