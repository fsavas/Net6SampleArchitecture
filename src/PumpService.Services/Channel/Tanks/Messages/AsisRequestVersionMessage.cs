namespace PumpService.Services.Channel.Tanks.Messages
{
    public class AsisRequestVersionMessage : IMessage
    {
        #region Fields

        private byte[] _messageFrame;

        #endregion Fields

        #region Constructor

        public AsisRequestVersionMessage()
        {
            _messageFrame = new Byte[18];

            _messageFrame = new byte[] {
            0xFA, 0x00, 0x02, 0x00, 0x22, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };
        }

        #endregion Constructor

        #region Properties

        public byte SlaveAddress
        {
            get
            {
                return _messageFrame[2];
            }
            set
            {
                _messageFrame[2] = value;
            }
        }

        public byte[] MessageFrame
        {
            get { return _messageFrame; }
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

        #endregion Properties

        #region Methods

        public void Initialize(byte[] frame)
        {
            frame.CopyTo(_messageFrame, 0);
        }

        #endregion Methods
    }
}