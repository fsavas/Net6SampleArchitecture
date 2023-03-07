namespace PumpService.Services.Channel.Pumps.Messages.Mepsan
{
    public class ResetMessage : IMessage
    {
        #region Fields

        private byte _ctrlNo;

        #endregion Fields

        #region Constructor

        public ResetMessage(byte _slaveAddress)
        {
            SlaveAddress = _slaveAddress;
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
                Byte[] data = new Byte[3];
                data[0] = 0x01;//transaction CD1
                data[1] = 0x01;//length of transaction
                data[2] = 0x05;//DCC - Pump control command - Reset
                return data;
            }
        }

        public ushort TransactionId
        {
            get { return StateEngine.SlaveTxNo; }
            set { int x = value; }
        }

        #endregion Properties
    }
}