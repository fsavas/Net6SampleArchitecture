using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PumpService.Core;
using PumpService.Core.Defaults;
using PumpService.Core.Domain.Lookups;
using PumpService.Core.Domain.Tanks;
using PumpService.Services.Channel;
using PumpService.Services.Channel.Tanks.Probes;
using PumpService.Services.Lookups;
using PumpService.Services.Tanks;
using PumpService.Web.Core.Models;
using PumpService.Web.Core.Models.Tanks;
using Serilog;

namespace PumpService.Web.Controllers.Tanks
{
    public class TankController : BaseController
    {
        #region Fields

        private readonly ITankService _tankService;
        private readonly ILookupTableService _lookupTableService;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly ChannelData _channelData;

        #endregion Fields

        #region Constructor

        public TankController(ITankService tankService, ILookupTableService lookupTableService, IMapper mapper, IMemoryCache memoryCache, ChannelData channelData)
        {
            _tankService = tankService;
            _lookupTableService = lookupTableService;
            _mapper = mapper;
            _memoryCache = memoryCache;
            _channelData = channelData;
        }

        #endregion Constructor

        #region Base Methods

        // POST: api/Tank
        [HttpPost("Search")]
        public ServiceResult PostSearch([FromBody] TankSearchModel value)
        {
            try
            {
                var tankSearch = _mapper.Map<TankSearch>(value);
                var tankPagedList = (PagedList<Tank>)_tankService.SearchTanks(tankSearch);
                var data = _mapper.Map<PagedList<Tank>, PagedList<TankGridModel>>(tankPagedList);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        // POST: api/Tank
        [HttpPost("Export")]
        public ServiceResult PostExport([FromBody] TankSearchModel value)
        {
            try
            {
                var tankSearch = _mapper.Map<TankSearch>(value);
                var data = _tankService.ExportTanks(tankSearch);

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
                var data = InitializeTank();

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        //GET: api/Tank/5
        [HttpGet("{id}")]
        public ServiceResult Get(long id)
        {
            try
            {
                var tank = _tankService.GetTankById(id);
                var data = _mapper.Map<TankModel>(tank);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        // POST: api/Tank
        [HttpPost]
        public ServiceResult Post([FromBody] TankModel value)
        {
            try
            {
                Log.ForContext("User", "Fatih").Information("Update Tank Controller Started");

                var tank = _mapper.Map<Tank>(value);

                if (tank.Id > 0)
                    _tankService.UpdateTank(tank);
                else
                    _tankService.InsertTank(tank);

                Log.ForContext("User", "Fatih").Information("Update Tank Controller Ended");

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = null };
            }
            catch (Exception e)
            {
                Log.ForContext("User", "Fatih").Information("Update Tank Exception");
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        // DELETE: api/Tank/5
        [HttpDelete("{id}")]
        public ServiceResult Delete(long id)
        {
            try
            {
                _tankService.DeleteTank(id);

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

        private TankModel InitializeTank()
        {
            return new TankModel();
        }

        [HttpPost("FindAndSetProbeSerialNumber")]
        public ServiceResult PostFindAndSetProbeSerialNumber([FromBody] TankModel value)
        {
            try
            {
                var tank = _mapper.Map<Tank>(value);

                if (!_memoryCache.TryGetValue(MemoryCacheKeys.EnumClasses_LookupTypes_ProbeTypes_Asis, out LookupTable probeType))
                    probeType = _lookupTableService.GetByTypeName(EnumClasses.LookupTypes.ProbeTypes, nameof(EnumClasses.ProbeTypes.Asis));

                if (probeType != null && tank.ProbeType == probeType)
                {
                    foreach (var channelTank in _channelData.Tanks)
                    {
                        if (tank.Id == channelTank.Id)
                        {
                            if (channelTank.Probe != null)
                            {
                                var probe = (IProbeMaster)channelTank.Probe;

                                if (probe.FindAndSetProbeSerialNumber())
                                {
                                    if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                                        successMessage = message;

                                    return new ServiceResult { Success = true, Message = successMessage, Data = null };
                                    //RadMessageBox.Show("Seri Numarası bulundu ve Tank tanımına kaydedildi");
                                }
                            }
                        }
                    }
                }
                //else
                //{
                //    RadMessageBox.Show("Asis Probe'lar için yapılabilir bu işlem");
                //}

                return new ServiceResult { Success = true, Message = MemoryCacheKeys.SerialNumberCanBeAppliedToAsisProbe, Data = null };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        #endregion Methods
    }
}