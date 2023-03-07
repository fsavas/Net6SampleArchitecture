namespace PumpService.Services.Channel.Pumps.Messages.Mepsan
{
    public class AlloweNozzleNumberMessage : IMessage
    {
        #region Fields

        private byte _ctrlNo;

        #endregion Fields

        #region Constructor

        public AlloweNozzleNumberMessage(byte _address)
        {
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
            get { return 0x20; }
            set { _ctrlNo = value; }
        }

        public byte[] ProtocolDataUnit
        {
            get { byte[] data = { 0x02, 0x01, 0x01 }; return data; }
        }

        public ushort TransactionId
        {
            get { return StateEngine.SlaveTxNo++; }
            set { int x = value; }
        }

        #endregion Properties
    }
}