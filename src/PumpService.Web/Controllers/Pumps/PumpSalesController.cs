using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PumpService.Core;
using PumpService.Core.Defaults;
using PumpService.Core.Domain.Pumps;
using PumpService.Services.Pumps;
using PumpService.Web.Core.Models;
using PumpService.Web.Core.Models.Pumps;

namespace PumpService.Web.Controllers.Pumps
{
    public class PumpSalesController : BaseController
    {
        #region Fields

        private readonly IPumpSalesService _pumpSalesService;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        #endregion Fields

        #region Constructor

        public PumpSalesController(IPumpSalesService pumpSalesService, IMapper mapper, IMemoryCache memoryCache)
        {
            _pumpSalesService = pumpSalesService;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        #endregion Constructor

        #region Base Methods

        // POST: api/PumpSales
        [HttpPost("Search")]
        public ServiceResult PostSearch([FromBody] PumpSalesSearchModel value)
        {
            try
            {
                var pumpSalesSearch = _mapper.Map<PumpSalesSearch>(value);
                var pumpSalesPagedList = (PagedList<PumpSales>)_pumpSalesService.SearchPumpSaless(pumpSalesSearch);
                var data = _mapper.Map<PagedList<PumpSales>, PagedList<PumpSalesGridModel>>(pumpSalesPagedList);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        // POST: api/PumpSales
        [HttpPost("Export")]
        public ServiceResult PostExport([FromBody] PumpSalesSearchModel value)
        {
            try
            {
                var pumpSalesSearch = _mapper.Map<PumpSalesSearch>(value);
                var data = _pumpSalesService.ExportPumpSaless(pumpSalesSearch);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        //GET: api/PumpSales/5
        [HttpGet("{id}")]
        public ServiceResult Get(long id)
        {
            try
            {
                var pumpSales = _pumpSalesService.GetPumpSalesById(id);
                var data = _mapper.Map<PumpSalesModel>(pumpSales);

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
    }
}