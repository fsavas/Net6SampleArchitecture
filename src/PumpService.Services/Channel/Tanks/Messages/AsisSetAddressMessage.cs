namespace PumpService.Services.Channel.Tanks.Messages
{
    public class AsisSetAddressMessage : IMessage
    {
        #region Fields

        private byte[] _messageFrame;

        #endregion Fields

        #region Constructor

        public AsisSetAddressMessage()
        {
            _messageFrame = new Byte[18];

            _messageFrame = new byte[] {
            0xFA, 0x00, 0xFF, 0x00, 0xB2, 3, 11, 20, 120, 0x04, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF , 0xFF };
        }

        #endregion Constructor

        #region Methods

        public void Initialize(byte[] frame)
        {
            frame.CopyTo(_messageFrame, 0);
        }

        #endregion Methods

        #region Properties

        public byte SlaveAddress
        {
            get
            {
                return _messageFrame[9];
            }
            set
            {
                _messageFrame[9] = value;
            }
        }

        public byte SerialNumberPart1
        {
            get
            {
                return _messageFrame[5];
            }
            set
            {
                _messageFrame[5] = value;
            }
        }

        public byte SerialNumberPart2
        {
            get
            {
                return _messageFrame[6];
            }
            set
            {
                _messageFrame[6] = value;
            }
        }

        public byte SerialNumberPart3
        {
            get
            {
                return _messageFrame[7];
            }
            set
            {
                _messageFrame[7] = value;
            }
        }

        public byte SerialNumberPart4
        {
            get
            {
                return _messageFrame[8];
            }
            set
            {
                _messageFrame[8] = value;
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
    }
}