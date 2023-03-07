using PumpService.Core.Defaults;
using PumpService.Core.Domain.Lookups;
using PumpService.Core.Domain.Tanks;
using PumpService.Services.Channel.Devices;
using PumpService.Services.Channel.Streams;
using PumpService.Services.Channel.Tanks.Messages;
using PumpService.Services.Channel.Tanks.Probes.Transport;
using PumpService.Services.Channel.Utility;
using Serilog;

namespace PumpService.Services.Channel.Tanks.Probes
{
    public class MepsanProbe : SerialDevice, ITankMaster, IProbeMaster
    {
        #region Fields

        private Tank _tank;//probe'un ilişkili olduğu Tank

        #endregion Fields

        #region Constructor

        public MepsanProbe(SerialTransport transport, Tank tank)
            : base(transport)
        {
            _tank = tank;
        }

        #endregion Constructor

        #region Methods

        public static MepsanProbe CreateAscii(SerialPortAdapter serialPortAdapter, Tank tank)
        {
            if (serialPortAdapter == null)
                throw new ArgumentNullException("serialPortAdapter");

            return Create(serialPortAdapter, tank);
        }

        public static MepsanProbe Create(IStreamResource streamResource, Tank tank)
        {
            return new MepsanProbe(new MepsanProbeTransport(streamResource), tank);
        }

        public SerialTransport Transport
        {
            get { return base.Transport; }
        }

        public bool FindAndSetProbeSerialNumber()
        {
            return true;
        }

        public bool SetAddressOnProbe(int newAddress)
        {
            return true;
        }

        public int QueryProbe(LookupTable tankMeasurementReason)
        {
            int timeoutCounter = 0;
            double productLevelMm, interfaceLevelMm, temperatureValue;

            while (true)
            {
                MepsanMessage message = new MepsanMessage(0x2c, (byte)_tank.ProbeAddress);

                try
                {
                    MepsanResponseMessage response = Transport.UnicastMessage<MepsanResponseMessage>(message);

                    if (response != null && response.ProtocolDataUnit != null)
                    {
                        //if (Tank.DoLogging)
                        //    log.Error("For Tank:" + Tank.TankId + " Ölçüm Sebebi=" + pOlcumSebebi + " ProbeResponse=" + response.ProtocolDataUnit.Join(", ") + "timeoutCounter=" + timeoutCounter);

                        Log.Logger.ForContext("LogKey", LogKeys.TankMeasurementReason).Information("Tank=" + _tank.Code + " Message=" + tankMeasurementReason.Name + " ProbeResponse=" + response.ProtocolDataUnit.Join(", "));

                        productLevelMm = response.GetProductLevel(response.ProtocolDataUnit);
                        interfaceLevelMm = response.GetInterfaceLevel(response.ProtocolDataUnit);
                        temperatureValue = response.GetAverageTemperature(response.ProtocolDataUnit);

                        if (productLevelMm > 0 && interfaceLevelMm > 0 && temperatureValue < 60 && temperatureValue > -90)
                        {
                            tankOperations.ProcessTankStatus(pEventTime: DateTime.Now,
                                pFuelVolumeLt: 0,
                                pFuelHeightMm: (decimal)productLevelMm,
                                pWaterVolumeLt: 0,
                                pWaterHeightMm: (decimal)interfaceLevelMm,
                                pTemperature: (decimal)temperatureValue,
                                pTank: _tank,
                                pOlcumSebebi: tankMeasurementReason,
                                pSatisMiktari: 0,
                                pPumpSale: null,
                                pTankDolum: null);

                            //if (Tank.DoLogging)
                            //    log.Error("For Tank:" + Tank.TankId + "ProcessTankStatus Finished");

                            Log.Logger.ForContext("LogKey", LogKeys.ProcessTankStatusEnd).Information("Tank=" + _tank.Code + " Message=" + LogKeys.ProcessTankStatusEnd);

                            return 0;//success
                        }
                        else
                        {
                            //todo bu kurallar doğru mu sorulacak
                            if (productLevelMm <= 0)
                                Log.Logger.ForContext("LogKey", LogKeys.WrongTankMeasurementFuelLevel).Warning("Tank=" + _tank.Code + "FuelHeightMm=" + productLevelMm.ToString());
                            //taskService.SaveTarpetTaskLogs("MepsanProbe", "HATALI ÖLÇÜM " + "FuelHeightMm=" + ProductLevelMm.ToString(), (int)TarpetLogCodes.ProbeFuelLevelError);

                            if (interfaceLevelMm <= 0)
                                Log.Logger.ForContext("LogKey", LogKeys.WrongTankMeasurementWaterLevel).Warning("Tank=" + _tank.Code + "WaterHeightMm=" + interfaceLevelMm.ToString());
                            //taskService.SaveTarpetTaskLogs("MepsanProbe", "HATALI ÖLÇÜM " + "WaterHeightMm=" + interfaceLevelMm.ToString(), (int)TarpetLogCodes.ProbeWaterLevelError);

                            if (temperatureValue >= 60 || temperatureValue <= -90)
                                Log.Logger.ForContext("LogKey", LogKeys.WrongTankMeasurementTemperature).Warning("Tank=" + _tank.Code + "Temperature=" + temperatureValue.ToString());
                            //taskService.SaveTarpetTaskLogs("MepsanProbe", "HATALI ÖLÇÜM " + "Temperature=" + TemperatureValue.ToString(), (int)TarpetLogCodes.ProbeTemperatureError);
                        }
                    }
                }
                catch (Exception e)
                {
                    Log.Logger.Error("Tank=" + _tank.Code + " Message=" + e.Message + " StackTrace=" + e.StackTrace);
                    //log.Error("Got MepsanProbe Exception For Tank:" + Tank.TankId + "StackTrace=" + e.StackTrace + "Message=" + e.Message + "Source=" + e.Source + "Counter=" + timeoutCounter);
                }

                Thread.Sleep(50);

                if (timeoutCounter++ > 10)//eğer 10 kez denediğimiz halde cevap gelmezse failure döner
                {
                    return -1;//failure
                }
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public override void Listen()
        {
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}