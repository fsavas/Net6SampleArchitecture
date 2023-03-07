using PumpService.Core;
using PumpService.Core.Defaults;
using PumpService.Core.Domain.Lookups;
using PumpService.Core.Domain.Tanks;
using PumpService.Services.Channel.Devices;
using PumpService.Services.Channel.Streams;
using PumpService.Services.Channel.Tanks.Messages;
using PumpService.Services.Channel.Tanks.Probes.Transport;
using PumpService.Services.Tanks;
using Serilog;

namespace PumpService.Services.Channel.Tanks.Probes
{
    public class AsisProbe : SerialDevice, ITankMaster, IProbeMaster
    {
        #region Fields

        private Tank _tank;//probe'un ilişkili olduğu Tank
        private ITankService _tankService = (ITankService)ServiceContainer.Scope.ServiceProvider.GetService(typeof(ITankService));

        #endregion Fields

        #region Constructor

        public AsisProbe(SerialTransport transport, Tank tank)
            : base(transport)
        {
            _tank = tank;
        }

        #endregion Constructor

        #region Methods

        public static AsisProbe CreateAscii(SerialPortAdapter serialPortAdapter, Tank tank)
        {
            if (serialPortAdapter == null)
                throw new ArgumentNullException("serialPort");

            return Create(serialPortAdapter, tank);
        }

        public static AsisProbe Create(IStreamResource streamResource, Tank tank)
        {
            return new AsisProbe(new AsisV2TankTransport(streamResource), tank);
        }

        public override void Listen()
        {
            throw new NotImplementedException();
        }

        public SerialTransport Transport
        {
            get
            {
                return base.Transport;
            }
        }

        public int QueryProbe(LookupTable tankMeasurementReason)
        {
            int timeoutCounter = 0;

            double vFuelHeightMm, vWaterHeightMm, vTemperature;

            while (true)
            {
                AsisRequestTankStatusMessage request = new AsisRequestTankStatusMessage();
                request.SlaveAddress = (byte)_tank.ProbeAddressAsis;

                try
                {
                    AsisRequestTankStatusResponseMessage message = Transport.UnicastMessage<AsisRequestTankStatusResponseMessage>(request);

                    if (message != null)
                    {
                        Log.Logger.ForContext("LogKey", LogKeys.TankMeasurementReason).Information("Tank=" + _tank.Code + " Message=" + tankMeasurementReason.Name + " ProbeResponse=" + message);
                        //log.Warn("For Tank:" + Tank.TankId + " ProbeResponse=" + message + "timeoutCounter=" + timeoutCounter);

                        if (message.FuelRawHeight != null && message.WaterRawHeight != null && message.FuelRawTemperature != null)
                        {
                            try
                            {
                                vFuelHeightMm = Convert.ToDouble(message.FuelRawHeight);
                            }
                            catch (Exception)
                            {
                                vFuelHeightMm = -1;
                                Log.Logger.ForContext("LogKey", LogKeys.WrongTankMeasurementFuelLevel).Warning("Tank=" + _tank.Code + "FuelHeightMm=" + vFuelHeightMm.ToString());
                                //taskService.SaveTarpetTaskLogs("AsisProbe", "HATALI ÖLÇÜM " + "FuelHeightMm=" + vFuelHeightMm.ToString(), (int)TarpetLogCodes.ProbeFuelLevelReadError);
                            }

                            try
                            {
                                vWaterHeightMm = Convert.ToDouble(message.WaterRawHeight);
                            }
                            catch (Exception)
                            {
                                vWaterHeightMm = -1;
                                Log.Logger.ForContext("LogKey", LogKeys.WrongTankMeasurementWaterLevel).Warning("Tank=" + _tank.Code + "WaterHeightMm=" + vWaterHeightMm.ToString());
                                //taskService.SaveTarpetTaskLogs("AsisProbe", "HATALI ÖLÇÜM " + "WaterHeightMm=" + vWaterHeightMm.ToString(), (int)TarpetLogCodes.ProbeWaterLevelReadError);
                            }

                            try
                            {
                                vTemperature = Convert.ToDouble(message.FuelRawTemperature);
                            }
                            catch (Exception)
                            {
                                vTemperature = -99;
                                Log.Logger.ForContext("LogKey", LogKeys.WrongTankMeasurementTemperature).Warning("Tank=" + _tank.Code + "Temperature=" + vTemperature.ToString());
                                //taskService.SaveTarpetTaskLogs("AsisProbe", "HATALI ÖLÇÜM " + "Temperature=" + vTemperature.ToString(), (int)TarpetLogCodes.ProbeTemperatureReadError);
                            }

                            if (vFuelHeightMm > 0 && vWaterHeightMm > 0 && vTemperature < 60 && vTemperature > -90)
                            {
                                tankOperations.ProcessTankStatus(pEventTime: DateTime.Now,
                                                                    pFuelVolumeLt: 0,
                                                                    pFuelHeightMm: (decimal)vFuelHeightMm,
                                                                    pWaterVolumeLt: 0,
                                                                    pWaterHeightMm: (decimal)vWaterHeightMm,
                                                                    pTemperature: (decimal)vTemperature,
                                                                    pTank: _tank,
                                                                    pOlcumSebebi: tankMeasurementReason,
                                                                    pSatisMiktari: 0,
                                                                    pPumpSale: null,
                                                                    pTankDolum: null);

                                return 0;//success
                            }
                            else
                            {
                                //todo bu kurallar doğru mu sorulacak
                                if (vFuelHeightMm <= 0)
                                    Log.Logger.ForContext("LogKey", LogKeys.WrongTankMeasurementFuelLevel).Warning("Tank=" + _tank.Code + "FuelHeightMm=" + vFuelHeightMm.ToString());
                                //taskService.SaveTarpetTaskLogs("AsisProbe", "HATALI ÖLÇÜM " + "FuelHeightMm=" + vFuelHeightMm.ToString(), (int)TarpetLogCodes.ProbeFuelLevelError);

                                if (vWaterHeightMm <= 0)
                                    Log.Logger.ForContext("LogKey", LogKeys.WrongTankMeasurementWaterLevel).Warning("Tank=" + _tank.Code + "WaterHeightMm=" + vWaterHeightMm.ToString());
                                //taskService.SaveTarpetTaskLogs("AsisProbe", "HATALI ÖLÇÜM " + "WaterHeightMm=" + vWaterHeightMm.ToString(), (int)TarpetLogCodes.ProbeWaterLevelError);

                                if (vTemperature >= 60 || vTemperature <= -90)
                                    Log.Logger.ForContext("LogKey", LogKeys.WrongTankMeasurementTemperature).Warning("Tank=" + _tank.Code + "Temperature=" + vTemperature.ToString());
                                //taskService.SaveTarpetTaskLogs("AsisProbe", "HATALI ÖLÇÜM " + "Temperature=" + vTemperature.ToString(), (int)TarpetLogCodes.ProbeTemperatureError);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Log.Logger.Error("Tank=" + _tank.Code + " Message=" + e.Message + " StackTrace=" + e.StackTrace);
                    //log.Error("Got AsisTank Exception During QueryProbe For Tank:" + Tank.TankId + "StackTrace=" + e.StackTrace + "Message=" + e.Message + "Source=" + e.Source + "Counter=" + timeoutCounter);
                }

                Thread.Sleep(50);

                if (timeoutCounter++ > 10)//eğer 10 kez denediğimiz halde cevap gelmezse failure döner
                {
                    return -1;//failure
                }
            }
        }

        //Ekrana girilen ProbeAddressAsis bilgisini kullanarak Asis probe'un RS485 adresini  probe'a mesaj göndererek değiştirir.
        //Probe'a mesaj gönderirken ProbeAddressAsis bilgisi mesajda kullanıldığı için, yeni adres saklanmadan butona basılmalıdır.

        public bool SetAddressOnProbe(int newAddress)
        {
            //if (newAddress == null)//was ProbeAddressAsis
            //    return false;

            AsisSetAddressMessage request = new AsisSetAddressMessage();

            if (!String.IsNullOrEmpty(_tank.ProbeSerialNumber))
            {
                int firsthypen = _tank.ProbeSerialNumber.IndexOf("-");
                int secondhypen = _tank.ProbeSerialNumber.IndexOf("-", firsthypen + 1);
                int thirdhypen = _tank.ProbeSerialNumber.IndexOf("-", secondhypen + 1);

                int serialNumber1 = Convert.ToInt16(_tank.ProbeSerialNumber.Substring(0, firsthypen));
                int serialNumber2 = Convert.ToInt16(_tank.ProbeSerialNumber.Substring(firsthypen + 1, secondhypen - firsthypen - 1));
                int serialNumber3 = Convert.ToInt16(_tank.ProbeSerialNumber.Substring(secondhypen + 1, thirdhypen - secondhypen - 1));
                int serialNumber4 = Convert.ToInt16(_tank.ProbeSerialNumber.Substring(thirdhypen + 1, _tank.ProbeSerialNumber.Length - thirdhypen - 1));

                request.SerialNumberPart1 = (byte)serialNumber1;
                request.SerialNumberPart2 = (byte)serialNumber2;
                request.SerialNumberPart3 = (byte)serialNumber3;
                request.SerialNumberPart4 = (byte)serialNumber4;

                //request.SlaveAddress = Convert.ToByte(Tank.ProbeAddressAsis);
                request.SlaveAddress = Convert.ToByte(newAddress);
            }
            else
                return false;

            try
            {
                AsisSetAddressMessage message = Transport.UnicastMessage<AsisSetAddressMessage>(request);

                if (message != null)
                {
                    //Tank.ProbeAddressAsis = Convert.ToInt16(Tank.ProbeAddressAsis);
                    _tank.ProbeAddressAsis = (short)newAddress;
                    _tank.ProbeSerialNumberApplied = true;
                    _tankService.UpdateTank(_tank);// was ts.UpdateTank(Tank);

                    return true;
                }
            }
            catch (Exception e)
            {
                Log.Logger.Error("Tank=" + _tank.Code + " Message=" + e.Message + " StackTrace=" + e.StackTrace);
                //log.Error("Got AsisTank Exception During SetAddress For Tank:" + Tank.TankId + "StackTrace=" + e.StackTrace + "Message=" + e.Message + "Source=" + e.Source);
            }

            return (false);
        }

        public bool FindAndSetProbeSerialNumber()
        {
            bool sonuc = SetSerialNumber();
            return sonuc;
        }

        public bool SetSerialNumber()
        {
            AsisRequestVersionMessage request = new AsisRequestVersionMessage();

            request.SlaveAddress = (byte)_tank.ProbeAddressAsis;

            try
            {
                AsisRequestVersionResponseMessage message = Transport.UnicastMessage<AsisRequestVersionResponseMessage>(request);

                if (message != null)
                {
                    _tank.ProbeSerialNumber = message.SerialNumber;
                    _tankService.UpdateTank(_tank);// was ts.UpdateTank(Tank);

                    return true;
                }
                else
                {
                    Log.Logger.ForContext("LogKey", LogKeys.ProbeSerialNumberNotFound).Warning("Tank=" + _tank.Code + "Message=" + LogKeys.ProbeSerialNumberNotFound);
                    //log.Error("Probe Seri Numarası Bulunamadı");
                }
            }
            catch (Exception e)
            {
                Log.Logger.Error("Tank=" + _tank.Code + " Message=" + e.Message + " StackTrace=" + e.StackTrace);
                //log.Error("Got AsisTank Exception During SetSerialNumber for Tank:" + Tank.TankId + "StackTrace=" + e.StackTrace + "Message=" + e.Message + "Source=" + e.Source);
            }

            return (false);
        }

        #endregion Methods
    }
}