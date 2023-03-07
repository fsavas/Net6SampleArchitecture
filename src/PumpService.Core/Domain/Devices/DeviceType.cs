using PumpService.Core.Domain.Lookups;

namespace PumpService.Core.Domain.Devices
{
    public partial class DeviceType : BaseDomainEntity
    {
        public string Name { get; set; }
        public virtual LookupTable Type { get; set; }
        public long TypeId { get; set; }
        public virtual LookupTable Group { get; set; }
        public long GroupId { get; set; }
    }
}