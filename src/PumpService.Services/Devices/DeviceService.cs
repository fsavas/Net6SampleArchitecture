using PumpService.Core;
using PumpService.Core.Domain.Devices;
using PumpService.Core.Domain.Lookups;
using PumpService.Core.Repository.Devices;
using PumpService.Services.ExportImport;

namespace PumpService.Services.Devices
{
    public partial class DeviceService : BaseService, IDeviceService
    {
        #region Fields

        private readonly IDeviceRepository _deviceRepository;
        private readonly IExportManager<DeviceGrid, Device> _exportManager;

        #endregion Fields

        #region Constructor

        public DeviceService(IUnitOfWork unitOfWork, IDeviceRepository deviceRepository, IExportManager<DeviceGrid, Device> exportManager)
            : base(unitOfWork)
        {
            _deviceRepository = deviceRepository;
            _exportManager = exportManager;
        }

        #endregion Constructor

        #region Base Methods

        public virtual void DeleteDevice(long deviceId)
        {
            var device = GetDeviceById(deviceId);

            if (device == null)
                throw new ArgumentNullException(nameof(device));

            //device.IsDeleted = true;
            _deviceRepository.Update(device);
            _unitOfWork.SaveChanges();
        }

        public virtual void InsertDevice(Device device)
        {
            if (device == null)
                throw new ArgumentNullException(nameof(device));

            _deviceRepository.Insert(device);
            _unitOfWork.SaveChanges();
        }

        public virtual void UpdateDevice(Device device)
        {
            if (device == null)
                throw new ArgumentNullException(nameof(device));

            _deviceRepository.Update(device);
            _unitOfWork.SaveChanges();
        }

        public virtual List<Device> GetAllDevices()
        {
            List<Device> LoadDevicesFunc()
            {
                var query = from s in _deviceRepository.Table orderby s.Id select s;
                return query.ToList();
            }

            return LoadDevicesFunc();
        }

        public virtual Device GetDeviceById(long deviceId)
        {
            if (deviceId == 0)
                return null;

            Device LoadDeviceFunc()
            {
                return _deviceRepository.GetById(deviceId);
            }

            return LoadDeviceFunc();
        }

        #endregion Base Methods

        #region Methods

        public IPagedList<Device> SearchDevices(DeviceSearch deviceSearch)
        {
            return _deviceRepository.SearchDevices(deviceSearch);
        }

        public string ExportDevices(DeviceSearch deviceSearch)
        {
            var list = (List<Device>)_deviceRepository.SearchAllDevices(deviceSearch);

            return _exportManager.ExportToExcel(list);
        }

        public virtual List<Device> GetDevices(LookupTable deviceTypeGroup, LookupTable deviceTypeType)
        {
            return _deviceRepository.GetDevices(deviceTypeGroup, deviceTypeType);
        }

        public List<Device> GetChildDevices(Device parentDevice, LookupTable deviceTypeGroup, LookupTable deviceTypeType)
        {
            return _deviceRepository.GetChildDevices(parentDevice, deviceTypeGroup, deviceTypeType);
        }

        #endregion Methods
    }
}