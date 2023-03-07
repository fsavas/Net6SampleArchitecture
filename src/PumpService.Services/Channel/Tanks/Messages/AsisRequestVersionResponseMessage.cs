namespace PumpService.Services.Channel.Tanks.Messages
{
    public class AsisRequestVersionResponseMessage : IMessage
    {
        #region Fields

        private byte _slaveAddress;
        private string _serialnumber;

        #endregion Fields

        #region Methods

        public void Initialize(byte[] frame)
        {
            if (frame == null)
                return;

            string serialNumber = string.Format("{0:D}-{1:D}-{2:D}-{3:D}", frame[5], frame[6], frame[7], frame[8]);
            SerialNumber = serialNumber;
        }

        #endregion Methods

        #region Properties

        public byte SlaveAddress
        {
            get
            {
                return _slaveAddress;
            }
            set
            {
                _slaveAddress = value;
            }
        }

        public string SerialNumber
        {
            get
            {
                return _serialnumber;
            }
            set
            {
                _serialnumber = value;
            }
        }

        #endregion Properties

        #region NotImplemented

        public byte[] MessageFrame
        {
            get { throw new NotImplementedException(); }
        }

        public byte[] ProtocolDataUnit
        {
            get { throw new NotImplementedException(); }
        }

        public ushort TransactionId
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public byte ControlNo
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion NotImplemented
    }
}