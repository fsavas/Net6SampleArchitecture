using Microsoft.Extensions.Caching.Memory;
using PumpService.Core;
using PumpService.Core.Defaults;
using PumpService.Core.Domain.Lookups;
using PumpService.Services.Devices;
using PumpService.Services.Lookups;

namespace PumpService.Services.Channel.Pumps
{
    public partial class PumpService : BaseService, IPumpService
    {
        #region Fields

        private readonly IMemoryCache _memoryCache;
        private readonly ChannelData _channelData;
        private readonly ILookupTableService _lookupTableService;
        private IDeviceParameterService _deviceParameterService;

        #endregion Fields

        #region Constructor

        public PumpService(IUnitOfWork unitOfWork, IMemoryCache memoryCache, ChannelData channelData, ILookupTableService lookupTableService, IDeviceParameterService deviceParameterService)
            : base(unitOfWork)
        {
            _memoryCache = memoryCache;
            _channelData = channelData;
            _lookupTableService = lookupTableService;
            _deviceParameterService = deviceParameterService;
        }

        #endregion Constructor

        #region Methods

        public bool UpdateUnitPrice(byte abuAddress, byte cpuId, Dictionary<byte, decimal>? nozzleIdPrices)
        {
            if (nozzleIdPrices != null)
            {
                var pumpSerialDevice = GetPumpSerialDevice(abuAddress, cpuId);

                if (pumpSerialDevice != null)
                {
                    return pumpSerialDevice.UpdateUnitPrice(abuAddress, cpuId, nozzleIdPrices);
                }
            }

            return false;
        }

        public decimal? GetNozzleTotalizer(byte abuAddress, byte cpuId, byte nozzleId, int? divide)
        {
            var pumpSerialDevice = GetPumpSerialDevice(abuAddress, cpuId);

            if (pumpSerialDevice != null)
            {
                return pumpSerialDevice.GetNozzleTotalizer(abuAddress, cpuId, nozzleId, divide);
            }

            return null;
        }

        private PumpSerialDevice? GetPumpSerialDevice(byte abuAddress, byte cpuId)
        {
            PumpSerialDevice? pumpSerialDevice = null;

            if (_channelData.PumpSerialDevices != null)
            {
                int abuAddressResult = 0, cpuIdResult = 0;

                foreach (var item in _channelData.PumpSerialDevices)
                {
                    var fillingPoint = item.GetFillingPoint();

                    if (fillingPoint != null)
                    {
                        var deviceParameters = _deviceParameterService.GetDeviceParameterByDevice(fillingPoint.Id);

                        if (deviceParameters != null)
                        {
                            if (!_memoryCache.TryGetValue(string.Join(MemoryCacheKeys.KeySeperator, EnumClasses.LookupTypes.DeviceParameterNames, MemoryCacheKeys.EnumClasses_LookupTypes_DeviceParameterNames_AbuAddress), out LookupTable deviceParameterAbuAddress))
                            {
                                deviceParameterAbuAddress = _lookupTableService.GetByTypeName(EnumClasses.LookupTypes.DeviceParameterNames, MemoryCacheKeys.EnumClasses_LookupTypes_DeviceParameterNames_AbuAddress);
                            }

                            if (deviceParameterAbuAddress != null)
                            {
                                int.TryParse(deviceParameters.FirstOrDefault(x => x.Name.Id == deviceParameterAbuAddress.Id)?.Value, out abuAddressResult);
                            }

                            if (!_memoryCache.TryGetValue(string.Join(MemoryCacheKeys.KeySeperator, EnumClasses.LookupTypes.DeviceParameterNames, MemoryCacheKeys.EnumClasses_LookupTypes_DeviceParameterNames_CpuId), out LookupTable deviceParameterCpuId))
                            {
                                deviceParameterCpuId = _lookupTableService.GetByTypeName(EnumClasses.LookupTypes.DeviceParameterNames, MemoryCacheKeys.EnumClasses_LookupTypes_DeviceParameterNames_CpuId);
                            }

                            if (deviceParameterCpuId != null)
                            {
                                int.TryParse(deviceParameters.FirstOrDefault(x => x.Name.Id == deviceParameterCpuId.Id)?.Value, out cpuIdResult);
                            }

                            //todo check also nozzleids(same cpuid fillingpoints)
                            if (abuAddress == abuAddressResult && cpuId == cpuIdResult)
                            {
                                pumpSerialDevice = item;
                                break;
                            }
                        }
                    }
                }
            }

            return pumpSerialDevice;
        }

        //public bool UpdateUnitPrice(byte abuAddress, byte cpuId, Dictionary<byte, decimal> nozzleIdPrices)
        //{
        //    if (nozzleIdPrices != null)
        //    {
        //        if (_channelData.PumpSerialDevices != null)
        //        {
        //            PumpSerialDevice pumpSerialDevice = null;
        //            int abuAddressResult = 0, cpuIdResult = 0;

        //            foreach (var item in _channelData.PumpSerialDevices)
        //            {
        //                var fillingPoints = item.GetFillingPoint();

        //                if (fillingPoints != null)
        //                {
        //                    foreach (var fillingPoint in fillingPoints)
        //                    {
        //                        if (fillingPoint != null)
        //                        {
        //                            var deviceParameters = _deviceParameterService.GetDeviceParameterByDevice(fillingPoint.Id);

        //                            if (deviceParameters != null)
        //                            {
        //                                if (!_memoryCache.TryGetValue(string.Join(MemoryCacheKeys.KeySeperator, EnumClasses.LookupTypes.DeviceParameterNames, MemoryCacheKeys.EnumClasses_LookupTypes_DeviceParameterNames_AbuAddress), out LookupTable deviceParameterAbuAddress))
        //                                {
        //                                    deviceParameterAbuAddress = _lookupTableService.GetByTypeName(EnumClasses.LookupTypes.DeviceParameterNames, MemoryCacheKeys.EnumClasses_LookupTypes_DeviceParameterNames_AbuAddress);
        //                                }

        //                                if (deviceParameterAbuAddress != null)
        //                                {
        //                                    int.TryParse(deviceParameters.FirstOrDefault(x => x.Name.Id == deviceParameterAbuAddress.Id)?.Value, out abuAddressResult);
        //                                }

        //                                if (!_memoryCache.TryGetValue(string.Join(MemoryCacheKeys.KeySeperator, EnumClasses.LookupTypes.DeviceParameterNames, MemoryCacheKeys.EnumClasses_LookupTypes_DeviceParameterNames_CpuId), out LookupTable deviceParameterCpuId))
        //                                {
        //                                    deviceParameterCpuId = _lookupTableService.GetByTypeName(EnumClasses.LookupTypes.DeviceParameterNames, MemoryCacheKeys.EnumClasses_LookupTypes_DeviceParameterNames_CpuId);
        //                                }

        //                                if (deviceParameterCpuId != null)
        //                                {
        //                                    int.TryParse(deviceParameters.FirstOrDefault(x => x.Name.Id == deviceParameterCpuId.Id)?.Value, out cpuIdResult);
        //                                }

        //                                if (abuAddress == abuAddressResult && cpuId == cpuIdResult)
        //                                {
        //                                    pumpContainer = item;
        //                                    break;
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }

        //            if (pumpContainer != null)
        //            {
        //                var pumpSerialDevices = pumpContainer.InitializePump();
        //                return pumpContainer.UpdateUnitPrice(abuAddress, cpuId, nozzleIdPrices);
        //            }
        //        }
        //    }

        //    return false;
        //}

        #endregion Methods
    }
}