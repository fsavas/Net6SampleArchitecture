using PumpService.Core;
using PumpService.Core.Domain.Devices;
using PumpService.Core.Repository.Devices;
using PumpService.Services.ExportImport;

namespace PumpService.Services.Devices
{
    public partial class DeviceParameterService : BaseService, IDeviceParameterService
    {
        #region Fields

        private readonly IDeviceParameterRepository _deviceParameterRepository;
        private readonly IExportManager<DeviceParameterGrid, DeviceParameter> _exportManager;

        #endregion Fields

        #region Constructor

        public DeviceParameterService(IUnitOfWork unitOfWork, IDeviceParameterRepository deviceParameterRepository, IExportManager<DeviceParameterGrid, DeviceParameter> exportManager)
            : base(unitOfWork)
        {
            _deviceParameterRepository = deviceParameterRepository;
            _exportManager = exportManager;
        }

        #endregion Constructor

        #region Base Methods

        public virtual void DeleteDeviceParameter(long deviceParameterId)
        {
            var deviceParameter = GetDeviceParameterById(deviceParameterId);

            if (deviceParameter == null)
                throw new ArgumentNullException(nameof(deviceParameter));

            //deviceParameter.IsDeleted = true;
            _deviceParameterRepository.Update(deviceParameter);
            _unitOfWork.SaveChanges();
        }

        public virtual void InsertDeviceParameter(DeviceParameter deviceParameter)
        {
            if (deviceParameter == null)
                throw new ArgumentNullException(nameof(deviceParameter));

            _deviceParameterRepository.Insert(deviceParameter);
            _unitOfWork.SaveChanges();
        }

        public virtual void UpdateDeviceParameter(DeviceParameter deviceParameter)
        {
            if (deviceParameter == null)
                throw new ArgumentNullException(nameof(deviceParameter));

            _deviceParameterRepository.Update(deviceParameter);
            _unitOfWork.SaveChanges();
        }

        public virtual List<DeviceParameter> GetAllDeviceParameters()
        {
            List<DeviceParameter> LoadDeviceParametersFunc()
            {
                var query = from s in _deviceParameterRepository.Table orderby s.Id select s;
                return query.ToList();
            }

            return LoadDeviceParametersFunc();
        }

        public virtual DeviceParameter GetDeviceParameterById(long deviceParameterId)
        {
            if (deviceParameterId == 0)
                return null;

            DeviceParameter LoadDeviceParameterFunc()
            {
                return _deviceParameterRepository.GetById(deviceParameterId);
            }

            return LoadDeviceParameterFunc();
        }

        #endregion Base Methods

        #region Methods

        public IPagedList<DeviceParameter> SearchDeviceParameters(DeviceParameterSearch deviceParameterSearch)
        {
            return _deviceParameterRepository.SearchDeviceParameters(deviceParameterSearch);
        }

        public string ExportDeviceParameters(DeviceParameterSearch deviceParameterSearch)
        {
            var list = (List<DeviceParameter>)_deviceParameterRepository.SearchAllDeviceParameters(deviceParameterSearch);

            return _exportManager.ExportToExcel(list);
        }

        public List<DeviceParameter> GetDeviceParameterByDevice(long deviceId)
        {
            List<DeviceParameter> LoadDeviceParametersFunc()
            {
                var query = from s in _deviceParameterRepository.Table where s.DeviceId == deviceId orderby s.Id select s;
                return query.ToList();
            }

            return LoadDeviceParametersFunc();
        }

        #endregion Methods
    }
}