namespace PumpService.Services.Channel.Pumps.Messages.Mepsan
{
    public class UpdatePriceMessage : IMessage
    {
        #region Fields

        private byte _ctrlNo;
        private byte[] _protocolDataUnit;

        #endregion Fields

        #region Constructor

        public UpdatePriceMessage(byte _address, byte[] _unitPrice)
        {
            ProtocolDataUnit = _unitPrice;
            SlaveAddress = _address;
        }

        #endregion Constructor

        #region Methods

        public void Initialize(byte[] frame)
        {
            MessageFrame = new Byte[frame.Length];
            frame.CopyTo(MessageFrame, 0);
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
            get { return 0x30; }
            set { _ctrlNo = value; }
        }

        public byte[] ProtocolDataUnit
        {
            get
            {
                Byte[] data = new Byte[5];
                data[0] = 0x05;//transaction CD5
                data[1] = 0x03;//length of transaction
                data[2] = _protocolDataUnit[0];
                data[3] = _protocolDataUnit[1];
                data[4] = _protocolDataUnit[2];
                return data;
            }

            set { _protocolDataUnit = value; }
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