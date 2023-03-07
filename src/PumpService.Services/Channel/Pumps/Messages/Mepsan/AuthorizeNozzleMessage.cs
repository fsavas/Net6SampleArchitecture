namespace PumpService.Services.Channel.Pumps.Messages.Mepsan
{
    public class AuthorizeNozzleMessage : IMessage
    {
        #region Constructor

        public AuthorizeNozzleMessage(byte _address)
        {
            SlaveAddress = _address;
        }

        #endregion Constructor

        #region Methods

        public void Initialize(byte[] frame)
        {
        }

        #endregion Methods

        #region Properties

        public byte SlaveAddress
        {
            get;
            set;
        }

        public byte[] MessageFrame
        {
            get;
            set;
        }

        public byte ControlNo
        {
            get
            {
                return 0x30;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public byte[] ProtocolDataUnit
        {
            get
            {
                Byte[] data = new Byte[3];
                data[0] = 0x01;//transaction CD1
                data[1] = 0x01;//length of transaction
                data[2] = 0x06;//DCC - Pump control command - Authorize
                return data;
            }
        }

        public ushort TransactionId
        {
            get
            {
                return StateEngine.SlaveTxNo;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion Properties
    }
}