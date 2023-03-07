namespace PumpService.Services.Channel.Pumps.Messages.Mepsan
{
    public class PollMessage : IMessage
    {
        #region Fields

        private int txNo;
        private byte _ctrlNo;

        #endregion Fields

        #region Constructor

        public PollMessage(int pompaAddress)
        {
            SlaveAddress = (byte)pompaAddress;
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

        public byte[] ProtocolDataUnit
        {
            get
            {
                byte[] data = new byte[] { SlaveAddress, 0x20, 0xFA };

                return null;
            }
        }

        public ushort TransactionId
        {
            get { return 0; }
            set { txNo = value; }
        }

        public byte ControlNo
        {
            get { return 0x20; }
            set { _ctrlNo = value; }
        }

        #endregion Properties
    }
}