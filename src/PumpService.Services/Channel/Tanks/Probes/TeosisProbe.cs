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
    public class TeosisProbe : SerialDevice, ITankMaster, IProbeMaster
    {
        #region Fields

        private Tank _tank;//probe'un ilişkili olduğu Tank

        #endregion Fields

        #region Constructor

        public TeosisProbe(SerialTransport transport, Tank tank)
            : base(transport)
        {
            _tank = tank;
        }

        #endregion Constructor

        #region Methods

        public static TeosisProbe CreateAscii(SerialPortAdapter serialPortAdapter, Tank tank)
        {
            if (serialPortAdapter == null)
                throw new ArgumentNullException("serialPort");

            return Create(serialPortAdapter, tank);
        }

        public static TeosisProbe Create(IStreamResource streamResource, Tank tank)
        {
            return new TeosisProbe(new TeosisProbeTransport(streamResource), tank);
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
                TeosisMessage message = new TeosisMessage(0x2b, (byte)_tank.ProbeAddress);

                try
                {
                    TeosisResponseMessage response = Transport.UnicastMessage<TeosisResponseMessage>(message);

                    if (response != null && response.ProtocolDataUnit != null)
                    {
                        Log.Logger.ForContext("LogKey", LogKeys.TankMeasurementReason).Information("Tank=" + _tank.Code + " Message=" + tankMeasurementReason.Name + " ProbeResponse=" + response.ProtocolDataUnit.Join(", "));
                        //log.Warn("For Tank:" + Tank.TankId + " ProbeResponse=" + response.ProtocolDataUnit.Join(", ") + "timeoutCounter=" + timeoutCounter);

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

                            Log.Logger.ForContext("LogKey", LogKeys.ProcessTankStatusEnd).Information("Tank=" + _tank.Code + " Message=" + LogKeys.ProcessTankStatusEnd);

                            return 0;//success
                        }
                        else
                        {
                            //todo bu kurallar doğru mu sorulacak
                            if (productLevelMm <= 0)
                                Log.Logger.ForContext("LogKey", LogKeys.WrongTankMeasurementFuelLevel).Warning("Tank=" + _tank.Code + "FuelHeightMm=" + productLevelMm.ToString());
                            //taskService.SaveTarpetTaskLogs("TeosisProbe", "HATALI ÖLÇÜM " + "FuelHeightMm=" + ProductLevelMm.ToString(), (int)TarpetLogCodes.ProbeFuelLevelError);

                            if (interfaceLevelMm <= 0)
                                Log.Logger.ForContext("LogKey", LogKeys.WrongTankMeasurementWaterLevel).Warning("Tank=" + _tank.Code + "WaterHeightMm=" + interfaceLevelMm.ToString());
                            //taskService.SaveTarpetTaskLogs("TeosisProbe", "HATALI ÖLÇÜM " + "WaterHeightMm=" + interfaceLevelMm.ToString(), (int)TarpetLogCodes.ProbeWaterLevelError);

                            if (temperatureValue >= 60 || temperatureValue <= -90)
                                Log.Logger.ForContext("LogKey", LogKeys.WrongTankMeasurementTemperature).Warning("Tank=" + _tank.Code + "Temperature=" + temperatureValue.ToString());
                            //taskService.SaveTarpetTaskLogs("TeosisProbe", "HATALI ÖLÇÜM " + "Temperature=" + TemperatureValue.ToString(), (int)TarpetLogCodes.ProbeTemperatureError);
                        }
                    }
                }
                catch (Exception e)
                {
                    Log.Logger.Error("Tank=" + _tank.Code + " Message=" + e.Message + " StackTrace=" + e.StackTrace);
                    //log.Error("Got TeosisTank Exception For Tank:" + Tank.TankId + "StackTrace=" + e.StackTrace + "Message=" + e.Message + "Source=" + e.Source + "Counter=" + timeoutCounter);
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