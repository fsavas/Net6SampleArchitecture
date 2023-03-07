using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PumpService.Core;
using PumpService.Core.Defaults;
using PumpService.Core.Domain.Stations;
using PumpService.Services.Enums;
using PumpService.Services.Lookups;
using PumpService.Services.Stations;
using PumpService.Web.Core.Models;
using PumpService.Web.Core.Models.Stations;
using Serilog;
using System.IO.Ports;

namespace PumpService.Web.Controllers.Stations
{
    public class SerialPortDefinitionController : BaseController
    {
        #region Fields

        private readonly ISerialPortDefinitionService _serialPortDefinitionService;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly IEnumManager _enumManager;
        private readonly ILookupTableService _lookupTableService;

        #endregion Fields

        #region Constructor

        public SerialPortDefinitionController(ISerialPortDefinitionService serialPortDefinitionService, IMapper mapper, IMemoryCache memoryCache, IEnumManager enumManager, ILookupTableService lookupTableService)
        {
            _serialPortDefinitionService = serialPortDefinitionService;
            _mapper = mapper;
            _memoryCache = memoryCache;
            _enumManager = enumManager;
            _lookupTableService = lookupTableService;
        }

        #endregion Constructor

        #region Base Methods

        // POST: api/SerialPortDefinition
        [HttpPost("Search")]
        public ServiceResult PostSearch([FromBody] SerialPortDefinitionSearchModel value)
        {
            try
            {
                var serialPortDefinitionSearch = _mapper.Map<SerialPortDefinitionSearch>(value);
                var serialPortDefinitionPagedList = (PagedList<SerialPortDefinition>)_serialPortDefinitionService.SearchSerialPortDefinitions(serialPortDefinitionSearch);
                var data = _mapper.Map<PagedList<SerialPortDefinition>, PagedList<SerialPortDefinitionGridModel>>(serialPortDefinitionPagedList);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        // POST: api/SerialPortDefinition
        [HttpPost("Export")]
        public ServiceResult PostExport([FromBody] SerialPortDefinitionSearchModel value)
        {
            try
            {
                var serialPortDefinitionSearch = _mapper.Map<SerialPortDefinitionSearch>(value);
                var data = _serialPortDefinitionService.ExportSerialPortDefinitions(serialPortDefinitionSearch);

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
                var data = InitializeSerialPortDefinition();

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        //GET: api/SerialPortDefinition/5
        [HttpGet("{id}")]
        public ServiceResult Get(long id)
        {
            try
            {
                var serialPortDefinition = _serialPortDefinitionService.GetSerialPortDefinitionById(id);
                serialPortDefinition.PortTypes = GetPortTypes();
                serialPortDefinition.Parities = GetParities();
                serialPortDefinition.StopBitses = GetStopBitses();
                var data = _mapper.Map<SerialPortDefinitionModel>(serialPortDefinition);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        // POST: api/SerialPortDefinition
        [HttpPost]
        public ServiceResult Post([FromBody] SerialPortDefinitionModel value)
        {
            try
            {
                Log.ForContext("User", "Fatih").Information("Update SerialPortDefinition Controller Started");

                var serialPortDefinition = _mapper.Map<SerialPortDefinition>(value);

                if (serialPortDefinition.Id > 0)
                    _serialPortDefinitionService.UpdateSerialPortDefinition(serialPortDefinition);
                else
                    _serialPortDefinitionService.InsertSerialPortDefinition(serialPortDefinition);

                Log.ForContext("User", "Fatih").Information("Update SerialPortDefinition Controller Ended");

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = null };
            }
            catch (Exception e)
            {
                Log.ForContext("User", "Fatih").Information("Update SerialPortDefinition Exception");
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        // DELETE: api/SerialPortDefinition/5
        [HttpDelete("{id}")]
        public ServiceResult Delete(long id)
        {
            try
            {
                _serialPortDefinitionService.DeleteSerialPortDefinition(id);

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

        private SerialPortDefinitionModel InitializeSerialPortDefinition() => new SerialPortDefinitionModel()
        {
            PortTypes = _mapper.Map<List<SelectListItem>, List<SelectListItemModel>>(GetPortTypes()),
            Parities = _mapper.Map<List<SelectListItem>, List<SelectListItemModel>>(GetParities()),
            StopBitses = _mapper.Map<List<SelectListItem>, List<SelectListItemModel>>(GetStopBitses())
        };

        private List<SelectListItem> GetPortTypes() => _lookupTableService.GetByType(EnumClasses.LookupTypes.PortTypes);

        private List<SelectListItem> GetParities() => _enumManager.GetEnums<Parity>();

        private List<SelectListItem> GetStopBitses() => _enumManager.GetEnums<StopBits>();

        #endregion Methods
    }
}