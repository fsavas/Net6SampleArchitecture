namespace PumpService.Services.Channel.Tanks.Messages
{
    public class MepsanResponseMessage : MepsanDDAMessage, IMessage
    {
        #region Fields

        private byte address = 0xc0;//192
        private byte[] _messageFrame;

        #endregion Fields

        #region Constructor

        public MepsanResponseMessage()
        {
            _messageFrame = new Byte[2];

            _messageFrame = new byte[] { address, 0x10 };
        }

        #endregion Constructor

        #region Methods

        public void Initialize(byte[] frame)
        {
            _messageFrame = new byte[frame.Length];
            frame.CopyTo(_messageFrame, 0);
        }

        #endregion Methods

        #region Properties

        public byte SlaveAddress
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
            }
        }

        public byte[] MessageFrame
        {
            get { return _messageFrame; }
        }

        public byte[] ProtocolDataUnit
        {
            get { return _messageFrame; }
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

        #endregion Properties
    }
}