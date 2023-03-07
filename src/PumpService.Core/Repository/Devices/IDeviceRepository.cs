using PumpService.Core.Domain.Devices;
using PumpService.Core.Domain.Lookups;

namespace PumpService.Core.Repository.Devices
{
    public partial interface IDeviceRepository : IBaseRepository<Device>
    {
        #region Methods

        List<Device> GetAllDevices();

        List<Device> GetDevices(LookupTable deviceTypeGroup, LookupTable deviceTypeType);

        List<Device> GetChildDevices(Device parentDevice, LookupTable deviceTypeGroup, LookupTable deviceTypeType);

        IPagedList<Device> SearchDevices(DeviceSearch deviceSearch);

        IList<Device> SearchAllDevices(DeviceSearch deviceSearch);

        #endregion Methods
    }
}