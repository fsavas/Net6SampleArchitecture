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
    public class DeviceParameterController : BaseController
    {
        #region Fields

        private readonly IDeviceParameterService _deviceParameterService;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        #endregion Fields

        #region Constructor

        public DeviceParameterController(IDeviceParameterService deviceParameterService, IMapper mapper, IMemoryCache memoryCache)
        {
            _deviceParameterService = deviceParameterService;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        #endregion Constructor

        #region Base Methods

        // POST: api/DeviceParameter
        [HttpPost("Search")]
        public ServiceResult PostSearch([FromBody] DeviceParameterSearchModel value)
        {
            try
            {
                var deviceParameterSearch = _mapper.Map<DeviceParameterSearch>(value);
                var deviceParameterPagedList = (PagedList<DeviceParameter>)_deviceParameterService.SearchDeviceParameters(deviceParameterSearch);
                var data = _mapper.Map<PagedList<DeviceParameter>, PagedList<DeviceParameterGridModel>>(deviceParameterPagedList);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        // POST: api/DeviceParameter
        [HttpPost("Export")]
        public ServiceResult PostExport([FromBody] DeviceParameterSearchModel value)
        {
            try
            {
                var deviceParameterSearch = _mapper.Map<DeviceParameterSearch>(value);
                var data = _deviceParameterService.ExportDeviceParameters(deviceParameterSearch);

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
                var data = InitializeDeviceParameter();

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        //GET: api/DeviceParameter/5
        [HttpGet("{id}")]
        public ServiceResult Get(long id)
        {
            try
            {
                var deviceParameter = _deviceParameterService.GetDeviceParameterById(id);
                var data = _mapper.Map<DeviceParameterModel>(deviceParameter);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        // POST: api/DeviceParameter
        [HttpPost]
        public ServiceResult Post([FromBody] DeviceParameterModel value)
        {
            try
            {
                var deviceParameter = _mapper.Map<DeviceParameter>(value);

                if (deviceParameter.Id > 0)
                    _deviceParameterService.UpdateDeviceParameter(deviceParameter);
                else
                    _deviceParameterService.InsertDeviceParameter(deviceParameter);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = null };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        // DELETE: api/DeviceParameter/5
        [HttpDelete("{id}")]
        public ServiceResult Delete(long id)
        {
            try
            {
                _deviceParameterService.DeleteDeviceParameter(id);

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

        private DeviceParameterModel InitializeDeviceParameter()
        {
            return new DeviceParameterModel();
        }

        #endregion Methods
    }
}