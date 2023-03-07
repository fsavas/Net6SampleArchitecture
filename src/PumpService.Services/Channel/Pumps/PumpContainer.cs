using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using PumpService.Core;
using PumpService.Core.Defaults;
using PumpService.Core.Domain.Devices;
using PumpService.Core.Domain.Lookups;
using PumpService.Services.Channel.Streams;
using PumpService.Services.Devices;
using PumpService.Services.Lookups;
using PumpService.Services.Products;
using Serilog;

namespace PumpService.Services.Channel.Pumps
{
    public class PumpContainer
    {
        #region Fields

        private SerialPortAdapter _serialPortAdapter;
        private List<Device> _fillingPoints;
        private ILookupTableService _lookupTableService;// = (ILookupTableService)ServiceContainer.Scope.ServiceProvider.GetService(typeof(ILookupTableService));
        private IMemoryCache _memoryCache;// = (IMemoryCache)ServiceContainer.Scope.ServiceProvider.GetService(typeof(IMemoryCache));
        private ChannelData _channelData;// = (ChannelData)ServiceContainer.Scope.ServiceProvider.GetService(typeof(ChannelData));
        //private IDeviceService _deviceService;
        private IDeviceParameterService _deviceParameterService;// = (IDeviceParameterService)ServiceContainer.Scope.ServiceProvider.GetService(typeof(IDeviceParameterService));
        private IProductService _productService;

        #endregion Fields

        #region Constructor

        public PumpContainer(SerialPortAdapter serialPortAdapter, List<Device> fillingPoints)
        {
            _serialPortAdapter = serialPortAdapter;
            _fillingPoints = fillingPoints;
        }

        #endregion Constructor

        #region Methods

        public List<PumpSerialDevice> InitializePump()
        {
            var pumpSerialDevices = new List<PumpSerialDevice>();

            foreach (Device fillingPoint in _fillingPoints)
            {
                pumpSerialDevices.Add(new PumpSerialDevice(_serialPortAdapter, fillingPoint));
            }

            return pumpSerialDevices;
        }

        public void RunPumpThread(object info)
        {
            if (info != null)
            {
                var pumpContainer = (PumpContainer)((object[])info)[0];
                var serviceScopeFactory = (IServiceScopeFactory)((object[])info)[1];

                //Accessing Entity Framework context on the background. Create new scope because it disposes on the background thread
                using (var scope = serviceScopeFactory.CreateScope())
                {
                    ServiceContainer.Scope = scope;
                    _lookupTableService = (ILookupTableService)scope.ServiceProvider.GetService(typeof(ILookupTableService));
                    _memoryCache = (IMemoryCache)scope.ServiceProvider.GetService(typeof(IMemoryCache));
                    _channelData = (ChannelData)scope.ServiceProvider.GetService(typeof(ChannelData));
                    //_deviceService = (IDeviceService)ServiceContainer.Scope.ServiceProvider.GetService(typeof(IDeviceService));
                    _deviceParameterService = (IDeviceParameterService)ServiceContainer.Scope.ServiceProvider.GetService(typeof(IDeviceParameterService));
                    _productService = (IProductService)ServiceContainer.Scope.ServiceProvider.GetService(typeof(IProductService));

                    var pumpSerialDevices = pumpContainer.InitializePump();
                    _channelData.AddPumpSerialDevices(pumpSerialDevices);                    

                    pumpSerialDevices?.ForEach(pumpSerialDevice =>
                    {
                        SetPumpSerialDeviceProperties(ref pumpSerialDevice);                        
                    });

                    if (pumpSerialDevices != null)
                    {
                        while (true)
                        {
                            try
                            {
                                foreach (var pumpSerialDevice in pumpSerialDevices)
                                {
                                    pumpSerialDevice.ProcessPump();
                                }
                            }
                            catch (Exception e)
                            {
                                Log.Logger.ForContext("LogKey", LogKeys.ProcessPumpException).Error("Message=" + e.Message + " Stacktrace=" + e.StackTrace);
                            }
                        }
                    }
                    else
                    {
                        Log.Logger.ForContext("LogKey", LogKeys.PumpSerialDevicesNull).Warning("Message=" + LogKeys.PumpSerialDevicesNull);
                    }
                }
            }
            else
            {
                Log.Logger.ForContext("LogKey", LogKeys.PumpContainerInfoNull).Warning("Message=" + LogKeys.PumpContainerInfoNull);
            }
        }

        private void SetPumpSerialDeviceProperties(ref PumpSerialDevice pumpSerialDevice)
        {
            var fillingPoint = pumpSerialDevice?.GetFillingPoint();

            if (fillingPoint != null)
            {
                int result;

                var fillingPointParameters = _deviceParameterService.GetDeviceParameterByDevice(fillingPoint.Id);

                if (fillingPointParameters != null)
                {
                    if (!_memoryCache.TryGetValue(string.Join(MemoryCacheKeys.KeySeperator, EnumClasses.LookupTypes.DeviceParameterNames, MemoryCacheKeys.EnumClasses_LookupTypes_DeviceParameterNames_AbuAddress), out LookupTable deviceParameterAbuAddress))
                    {
                        deviceParameterAbuAddress = _lookupTableService.GetByTypeName(EnumClasses.LookupTypes.DeviceParameterNames, MemoryCacheKeys.EnumClasses_LookupTypes_DeviceParameterNames_AbuAddress);
                    }

                    if (deviceParameterAbuAddress != null)
                    {
                        if (int.TryParse(fillingPointParameters.FirstOrDefault(x => x.Name.Id == deviceParameterAbuAddress.Id)?.Value, out result))
                            pumpSerialDevice.AbuAddress = (byte)result;
                    }

                    if (!_memoryCache.TryGetValue(string.Join(MemoryCacheKeys.KeySeperator, EnumClasses.LookupTypes.DeviceParameterNames, MemoryCacheKeys.EnumClasses_LookupTypes_DeviceParameterNames_CpuId), out LookupTable deviceParameterCpuId))
                    {
                        deviceParameterCpuId = _lookupTableService.GetByTypeName(EnumClasses.LookupTypes.DeviceParameterNames, MemoryCacheKeys.EnumClasses_LookupTypes_DeviceParameterNames_CpuId);
                    }

                    if (deviceParameterCpuId != null)
                    {
                        if (int.TryParse(fillingPointParameters.FirstOrDefault(x => x.Name.Id == deviceParameterCpuId.Id)?.Value, out result))
                            pumpSerialDevice.CpuId = (byte)result;
                    }

                    SetPumpSerialDeviceNozzleIdUnitPrices(ref pumpSerialDevice, fillingPointParameters);
                }
            }
        }

        private void SetPumpSerialDeviceNozzleIdUnitPrices(ref PumpSerialDevice pumpSerialDevice, List<DeviceParameter> fillingPointParameters)
        {
            var nozzleIdUnitPrices = new Dictionary<byte, decimal>();

            if (!_memoryCache.TryGetValue(string.Join(MemoryCacheKeys.KeySeperator, EnumClasses.LookupTypes.DeviceParameterNames, MemoryCacheKeys.EnumClasses_LookupTypes_DeviceParameterNames_NozzleId1), out LookupTable deviceParameterNozzleId1))
            {
                deviceParameterNozzleId1 = _lookupTableService.GetByTypeName(EnumClasses.LookupTypes.DeviceParameterNames, MemoryCacheKeys.EnumClasses_LookupTypes_DeviceParameterNames_NozzleId1);
            }

            if (deviceParameterNozzleId1 != null)
            {
                int result;

                if (int.TryParse(fillingPointParameters.FirstOrDefault(x => x.Name.Id == deviceParameterNozzleId1.Id)?.Value, out result))
                {
                    var nozzleId = (byte)result;

                    if (!_memoryCache.TryGetValue(string.Join(MemoryCacheKeys.KeySeperator, EnumClasses.LookupTypes.DeviceParameterNames, MemoryCacheKeys.EnumClasses_LookupTypes_DeviceParameterNames_NozzleId1_FuelType), out LookupTable deviceParameterNozzleId1FuelType))
                    {
                        deviceParameterNozzleId1FuelType = _lookupTableService.GetByTypeName(EnumClasses.LookupTypes.DeviceParameterNames, MemoryCacheKeys.EnumClasses_LookupTypes_DeviceParameterNames_NozzleId1_FuelType);
                    }

                    if (deviceParameterNozzleId1FuelType != null)
                    {
                        if (int.TryParse(fillingPointParameters.FirstOrDefault(x => x.Name.Id == deviceParameterNozzleId1FuelType.Id)?.Value, out result))
                        {
                            var product = _productService.GetProductById(result);
                            nozzleIdUnitPrices[nozzleId] = product.UnitPrice;                            
                        }
                    }
                }
            }

            if (!_memoryCache.TryGetValue(string.Join(MemoryCacheKeys.KeySeperator, EnumClasses.LookupTypes.DeviceParameterNames, MemoryCacheKeys.EnumClasses_LookupTypes_DeviceParameterNames_NozzleId2), out LookupTable deviceParameterNozzleId2))
            {
                deviceParameterNozzleId2 = _lookupTableService.GetByTypeName(EnumClasses.LookupTypes.DeviceParameterNames, MemoryCacheKeys.EnumClasses_LookupTypes_DeviceParameterNames_NozzleId2);
            }

            if (deviceParameterNozzleId2 != null)
            {
                int result;

                if (int.TryParse(fillingPointParameters.FirstOrDefault(x => x.Name.Id == deviceParameterNozzleId2.Id)?.Value, out result))
                {
                    var nozzleId = (byte)result;

                    if (!_memoryCache.TryGetValue(string.Join(MemoryCacheKeys.KeySeperator, EnumClasses.LookupTypes.DeviceParameterNames, MemoryCacheKeys.EnumClasses_LookupTypes_DeviceParameterNames_NozzleId2_FuelType), out LookupTable deviceParameterNozzleId2FuelType))
                    {
                        deviceParameterNozzleId2FuelType = _lookupTableService.GetByTypeName(EnumClasses.LookupTypes.DeviceParameterNames, MemoryCacheKeys.EnumClasses_LookupTypes_DeviceParameterNames_NozzleId2_FuelType);
                    }

                    if (deviceParameterNozzleId2FuelType != null)
                    {
                        if (int.TryParse(fillingPointParameters.FirstOrDefault(x => x.Name.Id == deviceParameterNozzleId2FuelType.Id)?.Value, out result))
                        {
                            var product = _productService.GetProductById(result);
                            nozzleIdUnitPrices[nozzleId] = product.UnitPrice;
                        }
                    }
                }
            }

            if (!_memoryCache.TryGetValue(string.Join(MemoryCacheKeys.KeySeperator, EnumClasses.LookupTypes.DeviceParameterNames, MemoryCacheKeys.EnumClasses_LookupTypes_DeviceParameterNames_NozzleId3), out LookupTable deviceParameterNozzleId3))
            {
                deviceParameterNozzleId3 = _lookupTableService.GetByTypeName(EnumClasses.LookupTypes.DeviceParameterNames, MemoryCacheKeys.EnumClasses_LookupTypes_DeviceParameterNames_NozzleId3);
            }

            if (deviceParameterNozzleId3 != null)
            {
                int result;

                if (int.TryParse(fillingPointParameters.FirstOrDefault(x => x.Name.Id == deviceParameterNozzleId3.Id)?.Value, out result))
                {
                    var nozzleId = (byte)result;

                    if (!_memoryCache.TryGetValue(string.Join(MemoryCacheKeys.KeySeperator, EnumClasses.LookupTypes.DeviceParameterNames, MemoryCacheKeys.EnumClasses_LookupTypes_DeviceParameterNames_NozzleId3_FuelType), out LookupTable deviceParameterNozzleId3FuelType))
                    {
                        deviceParameterNozzleId3FuelType = _lookupTableService.GetByTypeName(EnumClasses.LookupTypes.DeviceParameterNames, MemoryCacheKeys.EnumClasses_LookupTypes_DeviceParameterNames_NozzleId3_FuelType);
                    }

                    if (deviceParameterNozzleId3FuelType != null)
                    {
                        if (int.TryParse(fillingPointParameters.FirstOrDefault(x => x.Name.Id == deviceParameterNozzleId3FuelType.Id)?.Value, out result))
                        {
                            var product = _productService.GetProductById(result);
                            nozzleIdUnitPrices[nozzleId] = product.UnitPrice;
                        }
                    }
                }
            }

            if (!_memoryCache.TryGetValue(string.Join(MemoryCacheKeys.KeySeperator, EnumClasses.LookupTypes.DeviceParameterNames, MemoryCacheKeys.EnumClasses_LookupTypes_DeviceParameterNames_NozzleId4), out LookupTable deviceParameterNozzleId4))
            {
                deviceParameterNozzleId4 = _lookupTableService.GetByTypeName(EnumClasses.LookupTypes.DeviceParameterNames, MemoryCacheKeys.EnumClasses_LookupTypes_DeviceParameterNames_NozzleId4);
            }

            if (deviceParameterNozzleId4 != null)
            {
                int result;

                if (int.TryParse(fillingPointParameters.FirstOrDefault(x => x.Name.Id == deviceParameterNozzleId4.Id)?.Value, out result))
                {
                    var nozzleId = (byte)result;

                    if (!_memoryCache.TryGetValue(string.Join(MemoryCacheKeys.KeySeperator, EnumClasses.LookupTypes.DeviceParameterNames, MemoryCacheKeys.EnumClasses_LookupTypes_DeviceParameterNames_NozzleId4_FuelType), out LookupTable deviceParameterNozzleId4FuelType))
                    {
                        deviceParameterNozzleId4FuelType = _lookupTableService.GetByTypeName(EnumClasses.LookupTypes.DeviceParameterNames, MemoryCacheKeys.EnumClasses_LookupTypes_DeviceParameterNames_NozzleId4_FuelType);
                    }

                    if (deviceParameterNozzleId4FuelType != null)
                    {
                        if (int.TryParse(fillingPointParameters.FirstOrDefault(x => x.Name.Id == deviceParameterNozzleId4FuelType.Id)?.Value, out result))
                        {
                            var product = _productService.GetProductById(result);
                            nozzleIdUnitPrices[nozzleId] = product.UnitPrice;
                        }
                    }
                }
            }

            if (!_memoryCache.TryGetValue(string.Join(MemoryCacheKeys.KeySeperator, EnumClasses.LookupTypes.DeviceParameterNames, MemoryCacheKeys.EnumClasses_LookupTypes_DeviceParameterNames_NozzleId5), out LookupTable deviceParameterNozzleId5))
            {
                deviceParameterNozzleId5 = _lookupTableService.GetByTypeName(EnumClasses.LookupTypes.DeviceParameterNames, MemoryCacheKeys.EnumClasses_LookupTypes_DeviceParameterNames_NozzleId5);
            }

            if (deviceParameterNozzleId5 != null)
            {
                int result;

                if (int.TryParse(fillingPointParameters.FirstOrDefault(x => x.Name.Id == deviceParameterNozzleId5.Id)?.Value, out result))
                {
                    var nozzleId = (byte)result;

                    if (!_memoryCache.TryGetValue(string.Join(MemoryCacheKeys.KeySeperator, EnumClasses.LookupTypes.DeviceParameterNames, MemoryCacheKeys.EnumClasses_LookupTypes_DeviceParameterNames_NozzleId5_FuelType), out LookupTable deviceParameterNozzleId5FuelType))
                    {
                        deviceParameterNozzleId5FuelType = _lookupTableService.GetByTypeName(EnumClasses.LookupTypes.DeviceParameterNames, MemoryCacheKeys.EnumClasses_LookupTypes_DeviceParameterNames_NozzleId5_FuelType);
                    }

                    if (deviceParameterNozzleId5FuelType != null)
                    {
                        if (int.TryParse(fillingPointParameters.FirstOrDefault(x => x.Name.Id == deviceParameterNozzleId5FuelType.Id)?.Value, out result))
                        {
                            var product = _productService.GetProductById(result);
                            nozzleIdUnitPrices[nozzleId] = product.UnitPrice;
                        }
                    }
                }
            }

            pumpSerialDevice.NozzleIdUnitPrices = CreateNozzleIdUnitPriceDictionary(ref nozzleIdUnitPrices);
        }

        private Dictionary<byte, decimal> CreateNozzleIdUnitPriceDictionary(ref Dictionary<byte, decimal> nozzleIdUnitPrices)
        {
            for (int i = 1; i <= 5; i++)
            {
                if (!nozzleIdUnitPrices.ContainsKey((byte)i))
                {
                    nozzleIdUnitPrices.Add((byte)i, 0.0m);
                }                  
            }

            return nozzleIdUnitPrices;
        }

        #endregion Methods

        #region Properties

        public List<Device> GetFillingPoints()
        {
            return _fillingPoints;
        }

        public SerialPortAdapter GetSerialPortAdapter()
        {
            return _serialPortAdapter;
        }

        #endregion Properties
    }
}