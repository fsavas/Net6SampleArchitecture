using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PumpService.Core;
using PumpService.Core.Defaults;
using PumpService.Core.Domain.Stations;
using PumpService.Services.Stations;
using PumpService.Web.Core.Models;
using PumpService.Web.Core.Models.Stations;
using Serilog;

namespace PumpService.Web.Controllers.Stations
{
    public class StationController : BaseController
    {
        #region Fields

        private readonly IStationService _stationService;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        #endregion Fields

        #region Constructor

        public StationController(IStationService stationService, IMapper mapper, IMemoryCache memoryCache)
        {
            _stationService = stationService;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        #endregion Constructor

        #region Base Methods

        // POST: api/Station
        [HttpPost("Search")]
        public ServiceResult PostSearch([FromBody] StationSearchModel value)
        {
            try
            {
                var stationSearch = _mapper.Map<StationSearch>(value);
                var stationPagedList = (PagedList<Station>)_stationService.SearchStations(stationSearch);
                var data = _mapper.Map<PagedList<Station>, PagedList<StationGridModel>>(stationPagedList);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        // POST: api/Station
        [HttpPost("Export")]
        public ServiceResult PostExport([FromBody] StationSearchModel value)
        {
            try
            {
                var stationSearch = _mapper.Map<StationSearch>(value);
                var data = _stationService.ExportStations(stationSearch);

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
                var data = InitializeStation();

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        //GET: api/Station/5
        [HttpGet("{id}")]
        public ServiceResult Get(long id)
        {
            try
            {
                var station = _stationService.GetStationById(id);
                var data = _mapper.Map<StationModel>(station);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        // POST: api/Station
        [HttpPost]
        public ServiceResult Post([FromBody] StationModel value)
        {
            try
            {
                Log.ForContext("User", "Fatih").Information("Update Station Controller Started");

                var station = _mapper.Map<Station>(value);

                if (station.Id > 0)
                    _stationService.UpdateStation(station);
                else
                    _stationService.InsertStation(station);

                Log.ForContext("User", "Fatih").Information("Update Station Controller Ended");

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = null };
            }
            catch (Exception e)
            {
                Log.ForContext("User", "Fatih").Information("Update Station Exception");
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        // DELETE: api/Station/5
        [HttpDelete("{id}")]
        public ServiceResult Delete(long id)
        {
            try
            {
                _stationService.DeleteStation(id);

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

        private StationModel InitializeStation()
        {
            return new StationModel();
        }

        #endregion Methods
    }
}