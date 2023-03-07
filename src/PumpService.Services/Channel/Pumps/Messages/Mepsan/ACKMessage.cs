namespace PumpService.Services.Channel.Pumps.Messages.Mepsan
{
    public class ACKMessage : IMessage
    {
        #region Fields

        public byte SlaveAddress { get; set; }
        public byte ControlSecondPart { get; set; }

        #endregion Fields

        #region Constructor

        public ACKMessage(byte _slaveAddress, byte _controlSecondPart)
        {
            SlaveAddress = _slaveAddress;
            ControlSecondPart = _controlSecondPart;
        }

        #endregion Constructor

        #region Properties

        public byte[] MessageFrame
        {
            get;
            set;
        }

        public byte[] ProtocolDataUnit
        {
            get
            {
                byte[] data = new byte[] { SlaveAddress, 0x0c, 0xFA };
                return null;
            }
        }

        public ushort TransactionId
        {
            get { return StateEngine.SlaveTxNo; }
            set { int x = value; }
        }

        public void Initialize(byte[] frame)
        {
        }

        public byte ControlNo
        {
            get
            {
                return (byte)(0xC0 + ControlSecondPart); // ACK
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion Properties
    }
}