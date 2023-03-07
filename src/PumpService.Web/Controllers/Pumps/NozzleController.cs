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
    public class NozzleController : BaseController
    {
        #region Fields

        private readonly INozzleService _nozzleService;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        #endregion Fields

        #region Constructor

        public NozzleController(INozzleService nozzleService, IMapper mapper, IMemoryCache memoryCache)
        {
            _nozzleService = nozzleService;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        #endregion Constructor

        #region Base Methods

        // POST: api/Nozzle
        [HttpPost("Search")]
        public ServiceResult PostSearch([FromBody] NozzleSearchModel value)
        {
            try
            {
                var nozzleSearch = _mapper.Map<NozzleSearch>(value);
                var nozzlePagedList = (PagedList<Nozzle>)_nozzleService.SearchNozzles(nozzleSearch);
                var data = _mapper.Map<PagedList<Nozzle>, PagedList<NozzleGridModel>>(nozzlePagedList);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        // POST: api/Nozzle
        [HttpPost("Export")]
        public ServiceResult PostExport([FromBody] NozzleSearchModel value)
        {
            try
            {
                var nozzleSearch = _mapper.Map<NozzleSearch>(value);
                var data = _nozzleService.ExportNozzles(nozzleSearch);

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
                var data = InitializeNozzle();

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        //GET: api/Nozzle/5
        [HttpGet("{id}")]
        public ServiceResult Get(long id)
        {
            try
            {
                var nozzle = _nozzleService.GetNozzleById(id);
                var data = _mapper.Map<NozzleModel>(nozzle);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        // POST: api/Nozzle
        [HttpPost]
        public ServiceResult Post([FromBody] NozzleModel value)
        {
            try
            {
                var nozzle = _mapper.Map<Nozzle>(value);

                if (nozzle.Id > 0)
                    _nozzleService.UpdateNozzle(nozzle);
                else
                    _nozzleService.InsertNozzle(nozzle);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = null };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        // DELETE: api/Nozzle/5
        [HttpDelete("{id}")]
        public ServiceResult Delete(long id)
        {
            try
            {
                _nozzleService.DeleteNozzle(id);

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

        private NozzleModel InitializeNozzle()
        {
            return new NozzleModel();
        }

        #endregion Methods
    }
}