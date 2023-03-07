using PumpService.Core;
using PumpService.Core.Domain.Devices;
using PumpService.Core.Domain.Lookups;

namespace PumpService.Services.Devices
{
    public partial interface IDeviceService : IBaseService
    {
        List<Device> GetAllDevices();

        Device GetDeviceById(long deviceId);

        List<Device> GetDevices(LookupTable deviceTypeGroup, LookupTable deviceTypeType);

        List<Device> GetChildDevices(Device parentDevice, LookupTable deviceTypeGroup, LookupTable deviceTypeType);

        void DeleteDevice(long deviceId);

        void InsertDevice(Device device);

        void UpdateDevice(Device device);

        IPagedList<Device> SearchDevices(DeviceSearch deviceSearch);

        string ExportDevices(DeviceSearch deviceSearch);
    }
}