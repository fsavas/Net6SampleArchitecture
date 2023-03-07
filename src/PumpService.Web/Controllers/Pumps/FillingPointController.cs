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
    public class FillingPointController : BaseController
    {
        #region Fields

        private readonly IFillingPointService _fillingPointService;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        #endregion Fields

        #region Constructor

        public FillingPointController(IFillingPointService fillingPointService, IMapper mapper, IMemoryCache memoryCache)
        {
            _fillingPointService = fillingPointService;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        #endregion Constructor

        #region Base Methods

        // POST: api/FillingPoint
        [HttpPost("Search")]
        public ServiceResult PostSearch([FromBody] FillingPointSearchModel value)
        {
            try
            {
                var fillingPointSearch = _mapper.Map<FillingPointSearch>(value);
                var fillingPointPagedList = (PagedList<FillingPoint>)_fillingPointService.SearchFillingPoints(fillingPointSearch);
                var data = _mapper.Map<PagedList<FillingPoint>, PagedList<FillingPointGridModel>>(fillingPointPagedList);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        // POST: api/FillingPoint
        [HttpPost("Export")]
        public ServiceResult PostExport([FromBody] FillingPointSearchModel value)
        {
            try
            {
                var fillingPointSearch = _mapper.Map<FillingPointSearch>(value);
                var data = _fillingPointService.ExportFillingPoints(fillingPointSearch);

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
                var data = InitializeFillingPoint();

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        //GET: api/FillingPoint/5
        [HttpGet("{id}")]
        public ServiceResult Get(long id)
        {
            try
            {
                var fillingPoint = _fillingPointService.GetFillingPointById(id);
                var data = _mapper.Map<FillingPointModel>(fillingPoint);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        // POST: api/FillingPoint
        [HttpPost]
        public ServiceResult Post([FromBody] FillingPointModel value)
        {
            try
            {
                var fillingPoint = _mapper.Map<FillingPoint>(value);

                if (fillingPoint.Id > 0)
                    _fillingPointService.UpdateFillingPoint(fillingPoint);
                else
                    _fillingPointService.InsertFillingPoint(fillingPoint);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = null };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        // DELETE: api/FillingPoint/5
        [HttpDelete("{id}")]
        public ServiceResult Delete(long id)
        {
            try
            {
                _fillingPointService.DeleteFillingPoint(id);

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

        private FillingPointModel InitializeFillingPoint()
        {
            return new FillingPointModel();
        }

        #endregion Methods
    }
}