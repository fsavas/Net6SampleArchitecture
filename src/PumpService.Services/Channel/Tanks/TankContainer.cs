using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using PumpService.Core;
using PumpService.Core.Defaults;
using PumpService.Core.Domain.Lookups;
using PumpService.Core.Domain.Tanks;
using PumpService.Core.Repository.Lookups;
using PumpService.Services.Channel.Streams;
using PumpService.Services.Channel.Tanks.Probes;
using Serilog;

namespace PumpService.Services.Channel.Tanks
{
    public class TankContainer
    {
        #region Fields

        private Tank _tank;
        private ILookupTableRepository _lookupTableRepository = (ILookupTableRepository)ServiceContainer.Scope.ServiceProvider.GetService(typeof(ILookupTableRepository));
        private IMemoryCache _memoryCache = (IMemoryCache)ServiceContainer.Scope.ServiceProvider.GetService(typeof(IMemoryCache));
        private ChannelData _channelData = (ChannelData)ServiceContainer.Scope.ServiceProvider.GetService(typeof(ChannelData));

        #endregion Fields

        #region Constructor

        public TankContainer(Tank tank)
        {
            _tank = tank;
        }

        #endregion Constructor

        #region Methods

        public object InitializeTank()
        {
            IProbeMaster probe = null;

            //Tank ile ilgili portların açık olup olmadığı kontrol edilir
            SerialPortAdapter serialportAdapter = _tank.SerialPortDefinition != null && !string.IsNullOrEmpty(_tank.SerialPortDefinition.PortName) ?
                _channelData.SerialPortAdapter(_tank.SerialPortDefinition.PortName) : null;

            if (serialportAdapter == null)
            {
                Log.Logger.ForContext("LogKey", LogKeys.TankComPortNotFound).Error("Tank=" + _tank.Code + " Message=" + LogKeys.TankComPortNotFound);
                return null;
            }

            if (serialportAdapter.IsOpen())
            {
                if (_tank.ProbeType != null)
                {
                    if (_tank.ProbeType.Name.Equals(nameof(EnumClasses.ProbeTypes.Mepsan)))
                    {
                        Log.Logger.ForContext("LogKey", LogKeys.TankComPortConnected).Information("Tank=" + _tank.Code + " Message=" + LogKeys.TankComPortConnected);
                        probe = MepsanProbe.CreateAscii(serialportAdapter, _tank);//ilgili probe oluşturulur
                    }
                    else if (_tank.ProbeType.Name.Equals(nameof(EnumClasses.ProbeTypes.Teosis)))
                    {
                        Log.Logger.ForContext("LogKey", LogKeys.TankComPortConnected).Information("Tank=" + _tank.Code + " Message=" + LogKeys.TankComPortConnected);
                        probe = TeosisProbe.CreateAscii(serialportAdapter, _tank);//ilgili probe oluşturulur
                    }
                    else if (_tank.ProbeType.Name.Equals(nameof(EnumClasses.ProbeTypes.Asis)))
                    {
                        Log.Logger.ForContext("LogKey", LogKeys.TankComPortConnected).Information("Tank=" + _tank.Code + " Message=" + LogKeys.TankComPortConnected);
                        probe = TeosisProbe.CreateAscii(serialportAdapter, _tank);//ilgili probe oluşturulur
                    }

                    _tank.Probe = probe;
                }
                //_tank.Probe = probe;//oluşturulan probe tank bünyesinde saklanır
            }
            else
            {
                Log.Logger.ForContext("LogKey", LogKeys.TankComPortNotOpen).Error("Tank=" + _tank.Code + " Message=" + LogKeys.TankComPortNotOpen);
            }

            return probe;
        }

        public void RunTankThread(object info)
        {
            if (info != null)
            {
                var probe = (IProbeMaster)((object[])info)[0];

                if (probe != null)
                {
                    var serviceScopeFactory = (IServiceScopeFactory)((object[])info)[1];

                    //Accessing Entity Framework context on the background. Create new scope because it disposes on the background thread
                    using (var scope = serviceScopeFactory.CreateScope())
                    {
                        ServiceContainer.Scope = scope;
                        _lookupTableRepository = (ILookupTableRepository)scope.ServiceProvider.GetService(typeof(ILookupTableRepository));
                        _memoryCache = (IMemoryCache)scope.ServiceProvider.GetService(typeof(IMemoryCache));
                        _channelData = (ChannelData)scope.ServiceProvider.GetService(typeof(ChannelData));

                        int sleepTime = 1000;
                        int tankStatusu;
                        bool connectionLostFlag = false;

                        if (_tank.MeasurementPeriod != null)
                            sleepTime = _tank.MeasurementPeriod.Value * 1000;

                        if (!_memoryCache.TryGetValue(MemoryCacheKeys.EnumClasses_LookupTypes_MeasurementReasons_Periodic, out LookupTable tankMeasurementReason))
                            tankMeasurementReason = _lookupTableRepository.GetByTypeName(EnumClasses.LookupTypes.TankMeasurementReasons, nameof(EnumClasses.TankMeasurementReasons.Periodic));

                        if (tankMeasurementReason != null)
                        {
                            //first read
                            try
                            {
                                var tankStatus = probe.QueryProbe(tankMeasurementReason);

                                if (tankMeasurementReason != null)
                                {
                                    if (tankStatus == 0)
                                    {
                                        connectionLostFlag = false;
                                        //todo signalr
                                        //DomainEventPublisher.Raise<TankConnectionResumedEvent>(new TankConnectionResumedEvent() { EventTime = DateTime.Now, Tank = Tank });
                                    }
                                    else if (tankStatus == -1)
                                    {
                                        connectionLostFlag = true;
                                        //todo signalr
                                        //DomainEventPublisher.Raise<TankConnectionLostEvent>(new TankConnectionLostEvent() { EventTime = DateTime.Now, Tank = Tank });
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                Log.Logger.Error("Tank=" + _tank.Code + " Message=" + e.Message + " Stacktrace=" + e.StackTrace);
                            }

                            Thread.Sleep(sleepTime);

                            while (true)
                            {
                                try
                                {
                                    tankStatusu = probe.QueryProbe(tankMeasurementReason);

                                    if (tankStatusu == 0 && connectionLostFlag)
                                    {
                                        connectionLostFlag = false;
                                        //todo signalr
                                        //DomainEventPublisher.Raise<TankConnectionResumedEvent>(new TankConnectionResumedEvent() { EventTime = DateTime.Now, Tank = Tank });
                                    }
                                    else if (tankStatusu == -1 && !connectionLostFlag)
                                    {
                                        connectionLostFlag = true;
                                        //todo signalr
                                        //DomainEventPublisher.Raise<TankConnectionLostEvent>(new TankConnectionLostEvent() { EventTime = DateTime.Now, Tank = Tank });
                                    }
                                }
                                catch (Exception e)
                                {
                                    Log.Logger.Error("Tank=" + _tank.Code + " Message=" + e.Message + " Stacktrace=" + e.StackTrace);
                                }

                                Thread.Sleep(sleepTime);
                            }
                        }
                        else
                        {
                            Log.Logger.ForContext("LogKey", LogKeys.TankMeasurementReasonNotFound).Error("Tank=" + _tank.Code + " Message=" + LogKeys.TankMeasurementReasonNotFound);
                        }
                    }
                }
                else
                {
                    Log.Logger.ForContext("LogKey", LogKeys.IProbeMasterNotFound).Error("Tank=" + _tank.Code + " Message=" + LogKeys.IProbeMasterNotFound);
                }
            }
        }

        #endregion Methods
    }
}