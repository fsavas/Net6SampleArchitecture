namespace PumpService.Services.Channel.Tanks.Messages
{
    public class AsisRequestTankStatusMessage : IMessage
    {
        #region Fields

        private byte[] _messageFrame;

        #endregion Fields

        #region Constructor

        public AsisRequestTankStatusMessage()
        {
            _messageFrame = new Byte[18];

            _messageFrame = new byte[] {
                0xFA, 0x00, 0x00, 0x00, 0x32, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff
             };
        }

        #endregion Constructor

        #region Methods

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

        public void Initialize(byte[] frame)
        {
            frame.CopyTo(_messageFrame, 0);
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

        #endregion Methods
    }
}