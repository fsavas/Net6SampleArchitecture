using PumpService.Services.Channel.Streams;

namespace PumpService.Services.Channel.Devices
{
    /// <remarks></remarks>
    public abstract class SerialDevice : Device, ISerialDevice
    {
        #region Fields

        protected TankAndPumpOps tankOperations;

        #endregion Fields

        #region Constructor

        public SerialDevice(SerialTransport transport)
            : base(transport)
        {
            tankOperations = new TankAndPumpOps();
        }

        #endregion Constructor

        #region Methods

        SerialTransport ISerialDevice.Transport
        {
            get
            {
                return (SerialTransport)Transport;
            }
        }

        public bool ReturnQueryData(byte slaveAddress, ushort data)
        {
            return true;
        }

        public abstract void Listen();

        #endregion Methods
    }
}