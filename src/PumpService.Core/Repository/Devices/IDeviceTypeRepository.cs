using PumpService.Core.Domain.Devices;

namespace PumpService.Core.Repository.Devices
{
    public partial interface IDeviceTypeRepository : IBaseRepository<DeviceType>
    {
        #region Methods

        List<DeviceType> GetAllDeviceTypes();

        IPagedList<DeviceType> SearchDeviceTypes(DeviceTypeSearch deviceTypeSearch);

        IList<DeviceType> SearchAllDeviceTypes(DeviceTypeSearch deviceTypeSearch);

        #endregion Methods
    }
}