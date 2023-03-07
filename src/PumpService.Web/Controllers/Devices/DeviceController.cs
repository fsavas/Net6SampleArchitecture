using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PumpService.Core;
using PumpService.Core.Defaults;
using PumpService.Core.Domain.Devices;
using PumpService.Services.Devices;
using PumpService.Web.Core.Models;
using PumpService.Web.Core.Models.Devices;

namespace PumpService.Web.Controllers.Devices
{
    public class DeviceController : BaseController
    {
        #region Fields

        private readonly IDeviceService _deviceService;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        #endregion Fields

        #region Constructor

        public DeviceController(IDeviceService deviceService, IMapper mapper, IMemoryCache memoryCache)
        {
            _deviceService = deviceService;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        #endregion Constructor

        #region Base Methods

        // POST: api/Device
        [HttpPost("Search")]
        public ServiceResult PostSearch([FromBody] DeviceSearchModel value)
        {
            try
            {
                var deviceSearch = _mapper.Map<DeviceSearch>(value);
                var devicePagedList = (PagedList<Device>)_deviceService.SearchDevices(deviceSearch);
                var data = _mapper.Map<PagedList<Device>, PagedList<DeviceGridModel>>(devicePagedList);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        // POST: api/Device
        [HttpPost("Export")]
        public ServiceResult PostExport([FromBody] DeviceSearchModel value)
        {
            try
            {
                var deviceSearch = _mapper.Map<DeviceSearch>(value);
                var data = _deviceService.ExportDevices(deviceSearch);

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
                var data = InitializeDevice();

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        //GET: api/Device/5
        [HttpGet("{id}")]
        public ServiceResult Get(long id)
        {
            try
            {
                var device = _deviceService.GetDeviceById(id);
                var data = _mapper.Map<DeviceModel>(device);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        // POST: api/Device
        [HttpPost]
        public ServiceResult Post([FromBody] DeviceModel value)
        {
            try
            {
                var device = _mapper.Map<Device>(value);

                if (device.Id > 0)
                    _deviceService.UpdateDevice(device);
                else
                    _deviceService.InsertDevice(device);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = null };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        // DELETE: api/Device/5
        [HttpDelete("{id}")]
        public ServiceResult Delete(long id)
        {
            try
            {
                _deviceService.DeleteDevice(id);

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

        private DeviceModel InitializeDevice()
        {
            return new DeviceModel();
        }

        #endregion Methods
    }
}