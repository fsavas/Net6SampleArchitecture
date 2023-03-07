using PumpService.Core.Domain.Lookups;

namespace PumpService.Core.Domain.Devices
{
    public partial class DeviceParameter : BaseDomainEntity
    {
        public virtual Device Device { get; set; }
        public long DeviceId { get; set; }
        public virtual LookupTable Name { get; set; }
        public long NameId { get; set; }
        public string Value { get; set; }
        public virtual LookupTable Type { get; set; }
        public long TypeId { get; set; }
    }
}