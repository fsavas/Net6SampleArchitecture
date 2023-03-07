using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PumpService.Core;
using PumpService.Core.Defaults;
using PumpService.Core.Domain.Devices;
using PumpService.Services.Devices;
using PumpService.Web.Core.Models;
using PumpService.Web.Core.Models.Devices;

namespace PumpService.Web.Controllers.Pumps
{
    public class DeviceTypeController : BaseController
    {
        #region Fields

        private readonly IDeviceTypeService _deviceTypeService;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        #endregion Fields

        #region Constructor

        public DeviceTypeController(IDeviceTypeService deviceTypeService, IMapper mapper, IMemoryCache memoryCache)
        {
            _deviceTypeService = deviceTypeService;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        #endregion Constructor

        #region Base Methods

        // POST: api/DeviceType
        [HttpPost("Search")]
        public ServiceResult PostSearch([FromBody] DeviceTypeSearchModel value)
        {
            try
            {
                var deviceTypeSearch = _mapper.Map<DeviceTypeSearch>(value);
                var deviceTypePagedList = (PagedList<DeviceType>)_deviceTypeService.SearchDeviceTypes(deviceTypeSearch);
                var data = _mapper.Map<PagedList<DeviceType>, PagedList<DeviceTypeGridModel>>(deviceTypePagedList);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        // POST: api/DeviceType
        [HttpPost("Export")]
        public ServiceResult PostExport([FromBody] DeviceTypeSearchModel value)
        {
            try
            {
                var deviceTypeSearch = _mapper.Map<DeviceTypeSearch>(value);
                var data = _deviceTypeService.ExportDeviceTypes(deviceTypeSearch);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        // POST: api/Language
        [HttpPost("New")]
        public ServiceResult PostNew()
        {
            try
            {
                var data = InitializeDeviceType();

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        //GET: api/DeviceType/5
        [HttpGet("{id}")]
        public ServiceResult Get(long id)
        {
            try
            {
                var deviceType = _deviceTypeService.GetDeviceTypeById(id);
                var data = _mapper.Map<DeviceTypeModel>(deviceType);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        // POST: api/DeviceType
        [HttpPost]
        public ServiceResult Post([FromBody] DeviceTypeModel value)
        {
            try
            {
                var deviceType = _mapper.Map<DeviceType>(value);

                if (deviceType.Id > 0)
                    _deviceTypeService.UpdateDeviceType(deviceType);
                else
                    _deviceTypeService.InsertDeviceType(deviceType);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = null };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        // DELETE: api/DeviceType/5
        [HttpDelete("{id}")]
        public ServiceResult Delete(long id)
        {
            try
            {
                _deviceTypeService.DeleteDeviceType(id);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = null };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        #endregion Base Methods

        #region Methods

        private DeviceTypeModel InitializeDeviceType()
        {
            return new DeviceTypeModel();
        }

        #endregion Methods
    }
}