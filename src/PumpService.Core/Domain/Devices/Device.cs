namespace PumpService.Core.Domain.Devices
{
    public partial class Device : BaseDomainEntity
    {
        private ICollection<DeviceParameter> _deviceParameters;

        public virtual Device? ParentDevice { get; set; }
        public long? ParentDeviceId { get; set; }
        public virtual DeviceType DeviceType { get; set; }
        public long DeviceTypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<DeviceParameter> DeviceParameters
        {
            get => _deviceParameters ?? (_deviceParameters = new List<DeviceParameter>());
            protected set => _deviceParameters = value;
        }
    }
}