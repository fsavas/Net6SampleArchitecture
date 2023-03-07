namespace PumpService.Services.Channel.Tanks.Messages
{
    internal class MepsanMessage : IMessage
    {
        #region Fields

        private byte _command = 0x00;
        private byte address;// 0xc0;192
        private byte[] _messageFrame;

        #endregion Fields

        #region Constructor

        public MepsanMessage()
        {
            _messageFrame = new Byte[2];

            _messageFrame = new byte[] { address, _command };
        }

        public MepsanMessage(byte command, byte paddress)
        {
            _command = command;
            address = paddress;

            _messageFrame = new byte[] { address, _command };
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