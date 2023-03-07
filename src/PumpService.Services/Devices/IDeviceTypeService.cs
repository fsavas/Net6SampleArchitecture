using PumpService.Core;
using PumpService.Core.Domain.Devices;

namespace PumpService.Services.Devices
{
    public partial interface IDeviceTypeService : IBaseService
    {
        List<DeviceType> GetAllDeviceTypes();

        DeviceType GetDeviceTypeById(long deviceTypeId);

        void DeleteDeviceType(long deviceTypeId);

        void InsertDeviceType(DeviceType deviceType);

        void UpdateDeviceType(DeviceType deviceType);

        IPagedList<DeviceType> SearchDeviceTypes(DeviceTypeSearch deviceTypeSearch);

        string ExportDeviceTypes(DeviceTypeSearch deviceTypeSearch);
    }
}