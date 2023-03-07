using PumpService.Core;
using PumpService.Core.Domain.Devices;
using PumpService.Core.Repository.Devices;
using PumpService.Services.ExportImport;

namespace PumpService.Services.Devices
{
    public partial class DeviceTypeService : BaseService, IDeviceTypeService
    {
        #region Fields

        private readonly IDeviceTypeRepository _deviceTypeRepository;
        private readonly IExportManager<DeviceTypeGrid, DeviceType> _exportManager;

        #endregion Fields

        #region Constructor

        public DeviceTypeService(IUnitOfWork unitOfWork, IDeviceTypeRepository deviceTypeRepository, IExportManager<DeviceTypeGrid, DeviceType> exportManager)
            : base(unitOfWork)
        {
            _deviceTypeRepository = deviceTypeRepository;
            _exportManager = exportManager;
        }

        #endregion Constructor

        #region Base Methods

        public virtual void DeleteDeviceType(long deviceTypeId)
        {
            var deviceType = GetDeviceTypeById(deviceTypeId);

            if (deviceType == null)
                throw new ArgumentNullException(nameof(deviceType));

            //deviceType.IsDeleted = true;
            _deviceTypeRepository.Update(deviceType);
            _unitOfWork.SaveChanges();
        }

        public virtual void InsertDeviceType(DeviceType deviceType)
        {
            if (deviceType == null)
                throw new ArgumentNullException(nameof(deviceType));

            _deviceTypeRepository.Insert(deviceType);
            _unitOfWork.SaveChanges();
        }

        public virtual void UpdateDeviceType(DeviceType deviceType)
        {
            if (deviceType == null)
                throw new ArgumentNullException(nameof(deviceType));

            _deviceTypeRepository.Update(deviceType);
            _unitOfWork.SaveChanges();
        }

        public virtual List<DeviceType> GetAllDeviceTypes()
        {
            List<DeviceType> LoadDeviceTypesFunc()
            {
                var query = from s in _deviceTypeRepository.Table orderby s.Id select s;
                return query.ToList();
            }

            return LoadDeviceTypesFunc();
        }

        public virtual DeviceType GetDeviceTypeById(long deviceTypeId)
        {
            if (deviceTypeId == 0)
                return null;

            DeviceType LoadDeviceTypeFunc()
            {
                return _deviceTypeRepository.GetById(deviceTypeId);
            }

            return LoadDeviceTypeFunc();
        }

        #endregion Base Methods

        #region Methods

        public IPagedList<DeviceType> SearchDeviceTypes(DeviceTypeSearch deviceTypeSearch)
        {
            return _deviceTypeRepository.SearchDeviceTypes(deviceTypeSearch);
        }

        public string ExportDeviceTypes(DeviceTypeSearch deviceTypeSearch)
        {
            var list = (List<DeviceType>)_deviceTypeRepository.SearchAllDeviceTypes(deviceTypeSearch);

            return _exportManager.ExportToExcel(list);
        }

        #endregion Methods
    }
}