namespace PumpService.Services.Channel.Tanks.Messages
{
    public class AsisSetAddressResponseMessage : IMessage
    {
        #region Fields

        private byte _slaveAddress;

        #endregion Fields

        #region Methods

        public void Initialize(byte[] frame)
        {
            if (frame == null)
                return;
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