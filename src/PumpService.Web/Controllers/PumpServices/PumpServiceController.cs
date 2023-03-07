using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.SignalR;
//using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using PumpService.Core.Defaults;
using PumpService.Core.Results;
using PumpService.Services.Channel;
using PumpService.Services.Channel.Pumps;
using PumpService.Services.Hubs;
using PumpService.Web.Core.Models;
using PumpService.Web.Core.Models.PumpServices;

namespace PumpService.Web.Controllers.PumpServices
{
    public class PumpServiceController : BaseController
    {
        #region Fields

        private readonly IMemoryCache _memoryCache;
        private readonly ChannelData _channelData;
        private readonly IPumpService _pumpService;
        //private readonly IHubContext<FillingInformationHub> _fillingInformationHub;
        //private readonly IDistributedCache _distributedCache;

        #endregion Fields

        #region Constructor

        public PumpServiceController(IMemoryCache memoryCache, ChannelData channelData, IPumpService pumpService)//, IHubContext<FillingInformationHub> fillingInformationHub, IDistributedCache distributedCache)
        {
            _memoryCache = memoryCache;
            _channelData = channelData;
            _pumpService = pumpService;
            //_fillingInformationHub = fillingInformationHub;
            //_distributedCache = distributedCache;
        }

        #endregion Constructor

        #region Base Methods

        [HttpPost("PumpStatus")]
        public ServiceResult PostPumpStatus()
        {
            try
            {
                List<PumpStatusResult>? data = null;

                if (_channelData.PumpStatuses != null)
                    data = _channelData.PumpStatuses.Values.ToList();

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        [HttpPost("Authorize")]
        public ServiceResult PostAuthorize([FromBody] AuthorizeModel model)
        {
            try
            {
                _channelData.IsAuthorize[string.Join(ChannelKeys.KeySeperator, model.AbuAddress.ToString(), model.CpuId.ToString())] = true;

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = null };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        [HttpPost("FillingInformation")]
        public ServiceResult PostFillingInformation([FromBody] FillingInformationModel model)
        {
            try
            {
                FillingInformationResult? data = null;

                if (_channelData.FillingInformations != null && _channelData.FillingInformations.ContainsKey(string.Join(ChannelKeys.KeySeperator, model.AbuAddress.ToString(), model.CpuId.ToString())))
                    data = _channelData.FillingInformations[string.Join(ChannelKeys.KeySeperator, model.AbuAddress.ToString(), model.CpuId.ToString())];
                
                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;                

                return new ServiceResult { Success = true, Message = successMessage, Data = data }; 
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        [HttpPost("ConfirmPay")]
        public ServiceResult PostConfirmPay([FromBody] ConfirmPayModel model)
        {
            try
            {
                _channelData.IsConfirmPay[string.Join(ChannelKeys.KeySeperator, model.AbuAddress.ToString(), model.CpuId.ToString())] = true;

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = null };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }


        [HttpPost("NozzleTotalizer")]
        public ServiceResult PostNozzleTotalizer([FromBody] NozzleTotalizerModel model)
        {
            try
            {
                var data = _pumpService.GetNozzleTotalizer((byte)model.AbuAddress, (byte)model.CpuId, (byte)model.NozzleId, model.Divide);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        //for(int i = 1; i <= 50; i++)
        //{
        //    if(_channelData.FillingInformations.ContainsKey(string.Join(ChannelKeys.KeySeperator, model.AbuAddress.ToString(), model.CpuId.ToString(), DateTime.Now.AddSeconds(-1 * i).ToString("dd/MM/yyyy HH:mm:ss"))))//_memoryCache.TryGetValue(string.Join(ChannelKeys.KeySeperator, model.AbuAddress.ToString(), model.CpuId.ToString(), DateTime.Now.AddSeconds(-1 * i).ToString("dd/MM/yyyy HH:mm:ss")), out FillingInformationResult fillingInformationResult))
        //    {
        //        data = _channelData.FillingInformations[string.Join(ChannelKeys.KeySeperator, model.AbuAddress.ToString(), model.CpuId.ToString(), DateTime.Now.AddSeconds(-1 * i).ToString("dd/MM/yyyy HH:mm:ss"))];
        //        break;
        //    }
        //}

        //var cacheData = _distributedCache.Get(string.Join(ChannelKeys.KeySeperator, model.AbuAddress.ToString(), model.CpuId.ToString()));

        //if (cacheData != null)
        //{
        //    var jsonToDeserialize = System.Text.Encoding.UTF8.GetString(cacheData);
        //    var cachedResult = JsonSerializer.Deserialize<FillingInformationResult>(jsonToDeserialize);

        //    if (cachedResult != null)
        //    {
        //        data = cachedResult;
        //    }
        //}

        //[HttpPost("FillingInformationWithTimer")]
        //public ServiceResult PostFillingInformationWithTimer([FromBody] FillingInformationModel model)
        //{
        //    try
        //    {
        //        var timer = new System.Timers.Timer(1000);
        //        timer.Elapsed += (sender, eventArgs) => LoadFillingInformationAsync(model.AbuAddress, model.CpuId);
        //        timer.Start();

        //        return new ServiceResult { Success = true, Message = successMessage, Data = null };
        //    }
        //    catch (Exception e)
        //    {
        //        return new ServiceResult { Success = false, Message = e.Message, Data = null };
        //    }
        //}

        //private void LoadFillingInformationAsync(int abuAddress, int cpuId)
        //{
        //    if (_channelData.FillingInformations != null && _channelData.FillingInformations.ContainsKey(string.Join(ChannelKeys.KeySeperator, abuAddress.ToString(), cpuId.ToString())))
        //    {
        //        var data = _channelData.FillingInformations[string.Join(ChannelKeys.KeySeperator, abuAddress.ToString(), cpuId.ToString())];

        //        _fillingInformationHub.Clients.All.SendAsync("ListenFillingInformation", data);//new FillingInformationResult() { Amount = new Random().NextDouble() });
        //    }
        //}

        //[HttpPost("UpdateUnitPrice")]
        //public ServiceResult PostUpdateUnitPrice([FromBody] UpdateUnitPriceModel model)
        //{
        //    try
        //    {
        //        var data = _pumpService.UpdateUnitPrice((byte)model.AbuAddress, (byte)model.CpuId, CreateNozzleIdPrices(model.NozzleIdPrices));

        //        if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
        //            successMessage = message;

        //        return new ServiceResult { Success = true, Message = successMessage, Data = null };
        //    }
        //    catch (Exception e)
        //    {
        //        return new ServiceResult { Success = false, Message = e.Message, Data = null };
        //    }
        //}        

        //private Dictionary<byte, decimal>? CreateNozzleIdPrices(IList<NozzleIdUnitPriceModel> nozzleIdPrices)
        //{
        //    if (nozzleIdPrices != null)
        //    {
        //        var keyValuePairs = new Dictionary<byte, decimal>();

        //        foreach (var item in nozzleIdPrices)
        //        {
        //            keyValuePairs.Add((byte)item.NozzleId, item.UnitPrice);
        //        }

        //        return keyValuePairs;
        //    }

        //    return null;
        //}

        #endregion Base Methods
    }
}