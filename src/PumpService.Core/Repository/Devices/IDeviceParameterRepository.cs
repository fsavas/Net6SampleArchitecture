using PumpService.Core.Domain.Devices;

namespace PumpService.Core.Repository.Devices
{
    public partial interface IDeviceParameterRepository : IBaseRepository<DeviceParameter>
    {
        #region Methods

        List<DeviceParameter> GetAllDeviceParameters();

        IPagedList<DeviceParameter> SearchDeviceParameters(DeviceParameterSearch deviceParameterSearch);

        IList<DeviceParameter> SearchAllDeviceParameters(DeviceParameterSearch deviceParameterSearch);

        #endregion Methods
    }
}