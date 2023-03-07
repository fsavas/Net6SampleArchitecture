using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using PumpService.Core.Defaults;
using PumpService.Core.Domain.Devices;
using PumpService.Core.Domain.Lookups;
using PumpService.Core.Domain.Stations;
using PumpService.Services.Devices;
using PumpService.Services.Lookups;
using Serilog;
using System.IO.Ports;

namespace PumpService.Services.Channel.Pumps
{
    public class PumpChannel : IPumpChannel
    {
        #region Fields

        private ChannelData _channelData;
        private readonly IDeviceService _deviceService;
        private readonly IDeviceParameterService _deviceParameterService;
        private readonly IDeviceTypeService _deviceTypeService;
        private readonly ILookupTableService _lookupTableService;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IMemoryCache _memoryCache;

        #endregion Fields

        #region Constructor

        public PumpChannel(ChannelData channelData, IDeviceService deviceService, IDeviceParameterService deviceParameterService, IDeviceTypeService deviceTypeService, ILookupTableService lookupTableService, IMemoryCache memoryCache, IServiceScopeFactory serviceScopeFactory)
        {
            _channelData = channelData;
            _deviceService = deviceService;
            _deviceParameterService = deviceParameterService;
            _deviceTypeService = deviceTypeService;
            _lookupTableService = lookupTableService;
            _serviceScopeFactory = serviceScopeFactory;
            _memoryCache = memoryCache;
        }

        #endregion Constructor

        #region Methods

        public void StartPumps()
        {
            try
            {
                if (!_memoryCache.TryGetValue(string.Join(MemoryCacheKeys.KeySeperator, EnumClasses.LookupTypes.DeviceTypeGroups, MemoryCacheKeys.EnumClasses_LookupTypes_DeviceTypeGroups_Communication), out LookupTable communicationDeviceTypeGroup))
                    communicationDeviceTypeGroup = _lookupTableService.GetByTypeName(EnumClasses.LookupTypes.DeviceTypeGroups, MemoryCacheKeys.EnumClasses_LookupTypes_DeviceTypeGroups_Communication);

                if (!_memoryCache.TryGetValue(string.Join(MemoryCacheKeys.KeySeperator, EnumClasses.LookupTypes.DeviceTypeGroups, MemoryCacheKeys.EnumClasses_LookupTypes_DeviceTypeGroups_Other), out LookupTable otherDeviceTypeGroup))
                    otherDeviceTypeGroup = _lookupTableService.GetByTypeName(EnumClasses.LookupTypes.DeviceTypeGroups, MemoryCacheKeys.EnumClasses_LookupTypes_DeviceTypeGroups_Other);

                if (!_memoryCache.TryGetValue(string.Join(MemoryCacheKeys.KeySeperator, EnumClasses.LookupTypes.DeviceTypeTypes, MemoryCacheKeys.EnumClasses_LookupTypes_DeviceTypeTypes_Serial), out LookupTable serialDeviceTypeType))
                    serialDeviceTypeType = _lookupTableService.GetByTypeName(EnumClasses.LookupTypes.DeviceTypeTypes, MemoryCacheKeys.EnumClasses_LookupTypes_DeviceTypeTypes_Serial);

                if (!_memoryCache.TryGetValue(string.Join(MemoryCacheKeys.KeySeperator, EnumClasses.LookupTypes.DeviceTypeTypes, MemoryCacheKeys.EnumClasses_LookupTypes_DeviceTypeTypes_FillingPoint), out LookupTable fillingPointDeviceTypeType))
                    fillingPointDeviceTypeType = _lookupTableService.GetByTypeName(EnumClasses.LookupTypes.DeviceTypeTypes, MemoryCacheKeys.EnumClasses_LookupTypes_DeviceTypeTypes_FillingPoint);
                                
                if (communicationDeviceTypeGroup != null && serialDeviceTypeType != null)
                {
                    var devices = _deviceService.GetDevices(communicationDeviceTypeGroup, serialDeviceTypeType);//todo aktif ve notdeleted olanlar

                    if (devices != null && devices.Count > 0)
                    {
                        _channelData.AddDevices(devices);

                        foreach (var device in _channelData.Devices)
                        {
                            var serialPortDefinition = GenerateSerialPortDefinition(device);
                            _channelData.AddSerialPortDefinition(serialPortDefinition);

                            var fillingPoints = _deviceService.GetChildDevices(device, otherDeviceTypeGroup, fillingPointDeviceTypeType);
                            var serialPortAdapter = _channelData.SerialPortAdapter(serialPortDefinition.PortName);

                            if (serialPortAdapter != null)
                            {
                                if (fillingPoints != null)
                                {
                                    var pumpContainer = new PumpContainer(serialPortAdapter, fillingPoints);
                                    //_channelData.AddPumpContainer(pumpContainer);
                                    //var pumpSerialDevice = pumpContainer.InitializePump();
                                    ThreadPool.QueueUserWorkItem(pumpContainer.RunPumpThread, new object[] { pumpContainer, _serviceScopeFactory });
                                }
                                else
                                {
                                    Log.Logger.ForContext("LogKey", LogKeys.DeviceNotExists).Warning("Message=" + LogKeys.DeviceNotExists);
                                }
                            }
                            else
                            {
                                Log.Logger.ForContext("LogKey", LogKeys.SerialPortNotExists).Warning("Message=" + LogKeys.SerialPortNotExists);
                            }
                        }
                    }
                    else
                    {
                        Log.Logger.ForContext("LogKey", LogKeys.DeviceNotExists).Warning("Message=" + LogKeys.DeviceNotExists);
                    }
                }
                else
                {
                    Log.Logger.ForContext("LogKey", LogKeys.DeviceTypeLookupTableNull).Warning("Message=" + LogKeys.ComPortOpening);
                }
            }
            catch (Exception e)
            {
                Log.Logger.ForContext("LogKey", LogKeys.StartPumpsException).Error("Message=" + e.Message + " StackTrace=" + e.StackTrace);
            }
        }

        private SerialPortDefinition GenerateSerialPortDefinition(Device device)
        {
            var deviceParameters = device.DeviceParameters;
            var serialPortDefinition = new SerialPortDefinition();
            int result;
            Parity parity;
            StopBits stopBits;

            if (!_memoryCache.TryGetValue(string.Join(MemoryCacheKeys.KeySeperator, EnumClasses.LookupTypes.DeviceParameterNames, MemoryCacheKeys.EnumClasses_LookupTypes_DeviceParameterNames_Com), out LookupTable deviceParameterName))
            {
                deviceParameterName = _lookupTableService.GetByTypeName(EnumClasses.LookupTypes.DeviceParameterNames, MemoryCacheKeys.EnumClasses_LookupTypes_DeviceParameterNames_Com);
            }

            if (deviceParameterName != null)
            {
                serialPortDefinition.PortName = deviceParameters.FirstOrDefault(x => x.Name == deviceParameterName)?.Value;
            }

            if (!_memoryCache.TryGetValue(string.Join(MemoryCacheKeys.KeySeperator, EnumClasses.LookupTypes.DeviceParameterNames, MemoryCacheKeys.EnumClasses_LookupTypes_DeviceParameterNames_ReadTimeout), out LookupTable deviceParameterReadTimeout))
            {
                deviceParameterReadTimeout = _lookupTableService.GetByTypeName(EnumClasses.LookupTypes.DeviceParameterNames, MemoryCacheKeys.EnumClasses_LookupTypes_DeviceParameterNames_ReadTimeout);
            }

            if (deviceParameterReadTimeout != null)
            {
                if (int.TryParse(deviceParameters.FirstOrDefault(x => x.Name == deviceParameterReadTimeout)?.Value, out result))
                    serialPortDefinition.ReadTimeout = result;
            }

            if (!_memoryCache.TryGetValue(string.Join(MemoryCacheKeys.KeySeperator, EnumClasses.LookupTypes.DeviceParameterNames, MemoryCacheKeys.EnumClasses_LookupTypes_DeviceParameterNames_DataBits), out LookupTable deviceParameterDataBits))
            {
                deviceParameterDataBits = _lookupTableService.GetByTypeName(EnumClasses.LookupTypes.DeviceParameterNames, MemoryCacheKeys.EnumClasses_LookupTypes_DeviceParameterNames_DataBits);
            }

            if (deviceParameterDataBits != null)
            {
                if (int.TryParse(deviceParameters.FirstOrDefault(x => x.Name == deviceParameterDataBits)?.Value, out result))
                    serialPortDefinition.DataBits = result;
            }

            if (!_memoryCache.TryGetValue(string.Join(MemoryCacheKeys.KeySeperator, EnumClasses.LookupTypes.DeviceParameterNames, MemoryCacheKeys.EnumClasses_LookupTypes_DeviceParameterNames_BaudRate), out LookupTable deviceParameterBaudRate))
            {
                deviceParameterBaudRate = _lookupTableService.GetByTypeName(EnumClasses.LookupTypes.DeviceParameterNames, MemoryCacheKeys.EnumClasses_LookupTypes_DeviceParameterNames_BaudRate);
            }

            if (deviceParameterBaudRate != null)
            {
                if (int.TryParse(deviceParameters.FirstOrDefault(x => x.Name == deviceParameterBaudRate)?.Value, out result))
                    serialPortDefinition.BaudRate = result;
            }

            if (!_memoryCache.TryGetValue(string.Join(MemoryCacheKeys.KeySeperator, EnumClasses.LookupTypes.DeviceParameterNames, MemoryCacheKeys.EnumClasses_LookupTypes_DeviceParameterNames_WriteTimeout), out LookupTable deviceParameterWriteTimeout))
            {
                deviceParameterWriteTimeout = _lookupTableService.GetByTypeName(EnumClasses.LookupTypes.DeviceParameterNames, MemoryCacheKeys.EnumClasses_LookupTypes_DeviceParameterNames_WriteTimeout);
            }

            if (deviceParameterWriteTimeout != null)
            {
                if (int.TryParse(deviceParameters.FirstOrDefault(x => x.Name == deviceParameterWriteTimeout)?.Value, out result))
                    serialPortDefinition.WriteTimeout = result;
            }

            if (!_memoryCache.TryGetValue(string.Join(MemoryCacheKeys.KeySeperator, EnumClasses.LookupTypes.DeviceParameterNames, MemoryCacheKeys.EnumClasses_LookupTypes_DeviceParameterNames_Parity), out LookupTable deviceParameterParity))
            {
                deviceParameterParity = _lookupTableService.GetByTypeName(EnumClasses.LookupTypes.DeviceParameterNames, MemoryCacheKeys.EnumClasses_LookupTypes_DeviceParameterNames_Parity);
            }

            if (deviceParameterParity != null)
            {
                if (Enum.TryParse(deviceParameters.FirstOrDefault(x => x.Name == deviceParameterParity)?.Value, out parity))
                    serialPortDefinition.Parity = parity;
            }

            if (!_memoryCache.TryGetValue(string.Join(MemoryCacheKeys.KeySeperator, EnumClasses.LookupTypes.DeviceParameterNames, MemoryCacheKeys.EnumClasses_LookupTypes_DeviceParameterNames_StopBits), out LookupTable deviceParameterStopBits))
            {
                deviceParameterStopBits = _lookupTableService.GetByTypeName(EnumClasses.LookupTypes.DeviceParameterNames, MemoryCacheKeys.EnumClasses_LookupTypes_DeviceParameterNames_StopBits);
            }

            if (deviceParameterStopBits != null)
            {
                if (Enum.TryParse(deviceParameters.FirstOrDefault(x => x.Name == deviceParameterStopBits)?.Value, out stopBits))
                    serialPortDefinition.StopBits = stopBits;
            }

            return serialPortDefinition;
        }

        #endregion Methods
    }
}