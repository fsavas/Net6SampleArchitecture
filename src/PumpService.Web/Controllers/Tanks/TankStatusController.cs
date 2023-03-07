using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PumpService.Core;
using PumpService.Core.Defaults;
using PumpService.Core.Domain.Tanks;
using PumpService.Services.Tanks;
using PumpService.Web.Core.Models;
using PumpService.Web.Core.Models.Tanks;

namespace PumpService.Web.Controllers.Tanks
{
    public class TankStatusController : BaseController
    {
        #region Fields

        private readonly ITankStatusService _tankStatusService;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        #endregion Fields

        #region Constructor

        public TankStatusController(ITankStatusService tankStatusService, IMapper mapper, IMemoryCache memoryCache)
        {
            _tankStatusService = tankStatusService;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        #endregion Constructor

        #region Base Methods

        // POST: api/TankStatus
        [HttpPost("Search")]
        public ServiceResult PostSearch([FromBody] TankStatusSearchModel value)
        {
            try
            {
                var tankStatusSearch = _mapper.Map<TankStatusSearch>(value);
                var tankStatusPagedList = (PagedList<TankStatus>)_tankStatusService.SearchTankStatuses(tankStatusSearch);
                var data = _mapper.Map<PagedList<TankStatus>, PagedList<TankStatusGridModel>>(tankStatusPagedList);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        // POST: api/TankStatus
        [HttpPost("Export")]
        public ServiceResult PostExport([FromBody] TankStatusSearchModel value)
        {
            try
            {
                var tankStatusSearch = _mapper.Map<TankStatusSearch>(value);
                var data = _tankStatusService.ExportTankStatuses(tankStatusSearch);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        //GET: api/TankStatus/5
        [HttpGet("{id}")]
        public ServiceResult Get(long id)
        {
            try
            {
                var tankStatus = _tankStatusService.GetTankStatusById(id);
                var data = _mapper.Map<TankStatusModel>(tankStatus);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        #endregion Base Methods

        #region Methods

        private TankStatusModel InitializeTankStatus()
        {
            return new TankStatusModel();
        }

        #endregion Methods
    }
}