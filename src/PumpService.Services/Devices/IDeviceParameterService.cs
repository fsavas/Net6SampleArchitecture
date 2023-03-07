using PumpService.Core;
using PumpService.Core.Domain.Devices;

namespace PumpService.Services.Devices
{
    public partial interface IDeviceParameterService : IBaseService
    {
        List<DeviceParameter> GetAllDeviceParameters();

        DeviceParameter GetDeviceParameterById(long deviceParameterId);

        void DeleteDeviceParameter(long deviceParameterId);

        void InsertDeviceParameter(DeviceParameter deviceParameter);

        void UpdateDeviceParameter(DeviceParameter deviceParameter);

        IPagedList<DeviceParameter> SearchDeviceParameters(DeviceParameterSearch deviceParameterSearch);

        string ExportDeviceParameters(DeviceParameterSearch deviceParameterSearch);

        List<DeviceParameter> GetDeviceParameterByDevice(long deviceId);
    }
}